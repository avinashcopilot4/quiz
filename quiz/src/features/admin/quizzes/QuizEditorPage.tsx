import { useEffect, useMemo, useState } from 'react'
import { useNavigate, useParams } from 'react-router-dom'
import Box from '@mui/material/Box'
import Button from '@mui/material/Button'
import TextField from '@mui/material/TextField'
import Typography from '@mui/material/Typography'
import Stack from '@mui/material/Stack'
import MenuItem from '@mui/material/MenuItem'
import FormControl from '@mui/material/FormControl'
import FormLabel from '@mui/material/FormLabel'
import FormHelperText from '@mui/material/FormHelperText'
import Radio from '@mui/material/Radio'
import RadioGroup from '@mui/material/RadioGroup'
import Card from '@mui/material/Card'
import CardContent from '@mui/material/CardContent'
import Divider from '@mui/material/Divider'
import { fetchQuizById, createQuiz, saveQuiz } from '../../../api/quizApi'
import type { QuizModel, QuestionModel } from '../../../types/quiz.types'
import { useAuth } from '../../auth/useAuth'

interface QuestionErrors {
  text?: string
  options: (string | undefined)[]
  correctAnswer?: string
}

interface QuizEditorErrors {
  title?: string
  questions: Record<string, QuestionErrors>
}

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
  topic: 'General',
  language: 'English',
  difficulty: 'Intermediate',
  tags: [],
  createdBy,
  author: createdBy,
  createdAt: new Date().toISOString(),
  updatedAt: new Date().toISOString(),
  questionCount: 1,
  timeLimit: 10,
  passingScore: 70,
  questions: [createEmptyQuestion()],
  status: 'draft',
})

export function QuizEditorPage() {
  const { id } = useParams()
  const navigate = useNavigate()
  const { user } = useAuth()
  const [quiz, setQuiz] = useState<QuizModel | null>(null)
  const [loading, setLoading] = useState(true)
  const [errors, setErrors] = useState<QuizEditorErrors>({ questions: {} })

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

  const handleFieldChange = (
    field: keyof Pick<QuizModel, 'title' | 'description' | 'topic' | 'language' | 'difficulty' | 'author'>,
    value: string,
  ) => {
    setQuiz((current) => (current ? { ...current, [field]: value } : current))
  }

  const handleNumberFieldChange = (
    field: keyof Pick<QuizModel, 'timeLimit' | 'passingScore'>,
    value: number | undefined,
  ) => {
    setQuiz((current) => (current ? { ...current, [field]: value } : current))
  }

  const handleTagsChange = (value: string) => {
    setQuiz((current) =>
      current ? { ...current, tags: value.split(',').map((tag) => tag.trim()).filter(Boolean) } : current,
    )
  }

  const handleStatusChange = (status: QuizModel['status']) => {
    setQuiz((current) => (current ? { ...current, status } : current))
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

    const nextErrors: QuizEditorErrors = { questions: {} }
    let hasError = false

    if (!quiz.title.trim()) {
      nextErrors.title = 'Quiz title is required'
      hasError = true
    }

    quiz.questions.forEach((question) => {
      const questionErrors: QuestionErrors = { options: [], correctAnswer: undefined }

      if (!question.text.trim()) {
        questionErrors.text = 'Question text is required'
        hasError = true
      }

      question.options.forEach((option, index) => {
        if (!option.trim()) {
          questionErrors.options[index] = 'Option text is required'
          hasError = true
        }
      })

      const selectedOptionText = question.options[question.correctOptionIndex]?.trim()
      if (selectedOptionText === undefined || selectedOptionText === '') {
        questionErrors.correctAnswer = 'Select a valid correct answer'
        hasError = true
      }

      if (questionErrors.text || questionErrors.options.some(Boolean) || questionErrors.correctAnswer) {
        nextErrors.questions[question.id] = questionErrors
      }
    })

    if (hasError) {
      setErrors(nextErrors)
      return
    }

    setErrors({ questions: {} })

    const quizToSave: QuizModel = {
      ...quiz,
      questionCount: quiz.questions.length,
      updatedAt: new Date().toISOString(),
    }

    if (isNew) {
      await createQuiz(quizToSave)
    } else {
      await saveQuiz(quizToSave)
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
              <TextField
                label="Topic"
                value={quiz.topic}
                fullWidth
                onChange={(event) => handleFieldChange('topic', event.target.value)}
              />
              <TextField
                label="Language"
                value={quiz.language}
                fullWidth
                onChange={(event) => handleFieldChange('language', event.target.value)}
              />
              <TextField
                select
                label="Difficulty"
                value={quiz.difficulty}
                fullWidth
                onChange={(event) => handleFieldChange('difficulty', event.target.value)}
              >
                <MenuItem value="Beginner">Beginner</MenuItem>
                <MenuItem value="Intermediate">Intermediate</MenuItem>
                <MenuItem value="Advanced">Advanced</MenuItem>
              </TextField>
              <TextField
                label="Tags (comma separated)"
                value={quiz.tags.join(', ')}
                fullWidth
                onChange={(event) => handleTagsChange(event.target.value)}
              />
              <Stack direction={{ xs: 'column', sm: 'row' }} spacing={2}>
                <TextField
                  label="Time limit (minutes)"
                  type="number"
                  value={quiz.timeLimit ?? ''}
                  fullWidth
                  onChange={(event) =>
                    handleNumberFieldChange(
                      'timeLimit',
                      event.target.value === '' ? undefined : Number(event.target.value),
                    )
                  }
                />
                <TextField
                  label="Passing score (%)"
                  type="number"
                  value={quiz.passingScore ?? ''}
                  fullWidth
                  onChange={(event) =>
                    handleNumberFieldChange(
                      'passingScore',
                      event.target.value === '' ? undefined : Number(event.target.value),
                    )
                  }
                />
              </Stack>
              <TextField
                label="Author"
                value={quiz.author}
                fullWidth
                onChange={(event) => handleFieldChange('author', event.target.value)}
              />
              <TextField
                select
                label="Status"
                value={quiz.status}
                fullWidth
                onChange={(event) => handleStatusChange(event.target.value as QuizModel['status'])}
              >
                <MenuItem value="draft">Draft</MenuItem>
                <MenuItem value="published">Published</MenuItem>
                <MenuItem value="terminated">Terminated</MenuItem>
              </TextField>
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
                  error={Boolean(errors.questions[question.id]?.text)}
                  helperText={errors.questions[question.id]?.text}
                  onChange={(event) => handleQuestionChange(question.id, 'text', event.target.value)}
                />
                <FormControl
                  component="fieldset"
                  error={Boolean(errors.questions[question.id]?.correctAnswer)}
                >
                  <FormLabel component="legend" sx={{ mb: 2 }}>Correct answer</FormLabel>
                  <RadioGroup
                    value={question.correctOptionIndex.toString()}
                    onChange={(event) =>
                      handleQuestionChange(question.id, 'correctOptionIndex', Number(event.target.value))
                    }
                    sx={{ gap: 2 }}
                  >
                    {question.options.map((option, optionIndex) => (
                      <Stack key={optionIndex} direction="row" spacing={2} sx={{ alignItems: 'center' }}>
                        <Radio value={optionIndex.toString()} />
                        <TextField
                          fullWidth
                          label={`Option ${optionIndex + 1}`}
                          value={option}
                          error={Boolean(errors.questions[question.id]?.options[optionIndex])}
                          helperText={errors.questions[question.id]?.options[optionIndex]}
                          onChange={(event) => handleOptionChange(question.id, optionIndex, event.target.value)}
                        />
                      </Stack>
                    ))}
                  </RadioGroup>
                  {errors.questions[question.id]?.correctAnswer ? (
                    <FormHelperText>{errors.questions[question.id]?.correctAnswer}</FormHelperText>
                  ) : null}
                </FormControl>
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
