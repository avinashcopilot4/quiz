import { useEffect, useMemo, useState } from 'react'
import { useNavigate, useParams } from 'react-router-dom'
import Box from '@mui/material/Box'
import Button from '@mui/material/Button'
import TextField from '@mui/material/TextField'
import Typography from '@mui/material/Typography'
import Stack from '@mui/material/Stack'
import Checkbox from '@mui/material/Checkbox'
import FormControlLabel from '@mui/material/FormControlLabel'
import Card from '@mui/material/Card'
import CardContent from '@mui/material/CardContent'
import Divider from '@mui/material/Divider'
import { fetchQuizById, createQuiz, saveQuiz } from '../../../api/quizApi'
import type { QuizModel, QuestionModel } from '../../../types/quiz.types'
import { useAuth } from '../../auth/useAuth'

const createEmptyQuestion = (): QuestionModel => ({
  id: `q-${Date.now()}-${Math.random().toString(36).slice(2)}`,
  text: '',
  options: ['', '', '', ''],
  correctOptionIndex: 0,
})

const createNewQuiz = (createdBy: string): QuizModel => ({
  id: `quiz-${Date.now()}-${Math.random().toString(36).slice(2)}`,
  title: '',
  description: '',
  createdBy,
  createdAt: new Date().toISOString(),
  questions: [createEmptyQuestion()],
  isPublished: false,
})

export function QuizEditorPage() {
  const { id } = useParams()
  const navigate = useNavigate()
  const { user } = useAuth()
  const [quiz, setQuiz] = useState<QuizModel | null>(null)
  const [loading, setLoading] = useState(true)

  useEffect(() => {
    const init = async () => {
      if (id) {
        const existingQuiz = await fetchQuizById(id)
        setQuiz(existingQuiz ?? null)
      } else {
        setQuiz(createNewQuiz(user?.name ?? 'admin'))
      }
      setLoading(false)
    }

    init()
  }, [id, user])

  const isNew = useMemo(() => !id, [id])

  const handleFieldChange = (field: keyof Pick<QuizModel, 'title' | 'description'>, value: string) => {
    setQuiz((current) => (current ? { ...current, [field]: value } : current))
  }

  const handleTogglePublished = () => {
    setQuiz((current) => (current ? { ...current, isPublished: !current.isPublished } : current))
  }

  const handleQuestionChange = (questionId: string, field: keyof QuestionModel, value: string | number) => {
    setQuiz((current) =>
      current
        ? {
            ...current,
            questions: current.questions.map((question) =>
              question.id === questionId ? { ...question, [field]: value } : question,
            ),
          }
        : current,
    )
  }

  const handleOptionChange = (questionId: string, index: number, value: string) => {
    setQuiz((current) =>
      current
        ? {
            ...current,
            questions: current.questions.map((question) =>
              question.id === questionId
                ? {
                    ...question,
                    options: question.options.map((option, idx) => (idx === index ? value : option)),
                  }
                : question,
            ),
          }
        : current,
    )
  }

  const handleAddQuestion = () => {
    const question = createEmptyQuestion()
    setQuiz((current) => (current ? { ...current, questions: [...current.questions, question] } : current))
  }

  const handleDeleteQuestion = (questionId: string) => {
    setQuiz((current) =>
      current
        ? { ...current, questions: current.questions.filter((question) => question.id !== questionId) }
        : current,
    )
  }

  const handleSave = async () => {
    if (!quiz) return
    if (isNew) {
      await createQuiz(quiz)
    } else {
      await saveQuiz(quiz)
    }
    navigate('/admin/quizzes')
  }

  if (loading || !quiz) {
    return <Typography>Loading editor...</Typography>
  }

  return (
    <Box>
      <Stack direction="row" spacing={2} sx={{ alignItems: 'center', justifyContent: 'space-between', mb: 3 }}>
        <Box>
          <Typography variant="h5">{isNew ? 'New Quiz' : 'Edit Quiz'}</Typography>
          <Typography color="text.secondary">Manage quiz details and questions.</Typography>
        </Box>
        <Button variant="outlined" onClick={() => navigate('/admin/quizzes')}>
          Cancel
        </Button>
      </Stack>

      <Stack spacing={3}>
        <Card variant="outlined">
          <CardContent>
            <Stack spacing={2}>
              <TextField
                label="Title"
                value={quiz.title}
                fullWidth
                onChange={(event) => handleFieldChange('title', event.target.value)}
              />
              <TextField
                label="Description"
                value={quiz.description}
                fullWidth
                multiline
                minRows={3}
                onChange={(event) => handleFieldChange('description', event.target.value)}
              />
              <FormControlLabel
                control={<Checkbox checked={quiz.isPublished} onChange={handleTogglePublished} />}
                label="Published"
              />
            </Stack>
          </CardContent>
        </Card>

        {quiz.questions.map((question, questionIndex) => (
          <Card key={question.id} variant="outlined">
            <CardContent>
              <Stack spacing={2}>
                <Stack direction="row" spacing={2} sx={{ alignItems: 'center', justifyContent: 'space-between' }}>
                  <Typography variant="subtitle1">Question {questionIndex + 1}</Typography>
                  <Button color="error" onClick={() => handleDeleteQuestion(question.id)}>
                    Delete question
                  </Button>
                </Stack>
                <TextField
                  label="Question text"
                  value={question.text}
                  fullWidth
                  onChange={(event) => handleQuestionChange(question.id, 'text', event.target.value)}
                />
                {question.options.map((option, optionIndex) => (
                  <TextField
                    key={optionIndex}
                    label={`Option ${optionIndex + 1}`}
                    value={option}
                    fullWidth
                    onChange={(event) => handleOptionChange(question.id, optionIndex, event.target.value)}
                  />
                ))}
                <TextField
                  label="Correct option index"
                  type="number"
                  value={question.correctOptionIndex}
                  slotProps={{
                    input: {
                      inputProps: { min: 0, max: question.options.length - 1 },
                    },
                  }}
                  onChange={(event) =>
                    handleQuestionChange(question.id, 'correctOptionIndex', Number(event.target.value))
                  }
                />
              </Stack>
            </CardContent>
          </Card>
        ))}

        <Button variant="outlined" onClick={handleAddQuestion}>
          Add question
        </Button>
        <Divider />
        <Button variant="contained" onClick={handleSave}>
          Save Quiz
        </Button>
      </Stack>
    </Box>
  )
}
