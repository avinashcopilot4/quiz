import { useEffect, useMemo, useState } from 'react'
import { useNavigate, useParams } from 'react-router-dom'
import Box from '@mui/material/Box'
import Card from '@mui/material/Card'
import CardContent from '@mui/material/CardContent'
import CardActions from '@mui/material/CardActions'
import Button from '@mui/material/Button'
import Typography from '@mui/material/Typography'
import Radio from '@mui/material/Radio'
import RadioGroup from '@mui/material/RadioGroup'
import FormControl from '@mui/material/FormControl'
import FormControlLabel from '@mui/material/FormControlLabel'
import Stack from '@mui/material/Stack'
import Divider from '@mui/material/Divider'
import Alert from '@mui/material/Alert'
import type { QuizModel } from '../../../types/quiz.types'
import { calculateScore } from '../../../utils/scoring'
import { QuestionStepper } from './QuestionStepper'
import { fetchQuizById } from '../../../api/quizApi'
import { submitAttempt } from '../../../api/attemptApi'
import { useAuth } from '../../auth/useAuth'

export function AttemptPage() {
  const { id } = useParams()
  const navigate = useNavigate()
  const { user } = useAuth()
  const [quiz, setQuiz] = useState<QuizModel | null>(null)
  const [currentIndex, setCurrentIndex] = useState(0)
  const [answers, setAnswers] = useState<Record<string, number>>({})
  const [reviewed, setReviewed] = useState<Record<string, boolean>>({})
  const [loading, setLoading] = useState(true)
  const [error, setError] = useState<string | null>(null)
  const startedAt = useMemo(() => new Date().toISOString(), [])

  useEffect(() => {
    const loadQuiz = async () => {
      if (!id) {
        setLoading(false)
        return
      }

      const fetchedQuiz = await fetchQuizById(id)
      setQuiz(fetchedQuiz ?? null)
      setLoading(false)
    }

    loadQuiz()
  }, [id])

  if (loading) {
    return <Typography>Loading quiz...</Typography>
  }

  if (!quiz) {
    return <Typography>Quiz not found.</Typography>
  }

  const currentQuestion = quiz.questions[currentIndex]
  const selectedValue = answers[currentQuestion.id]

  const handleOptionChange = (value: number) => {
    setAnswers((prev) => ({ ...prev, [currentQuestion.id]: value }))
  }

  const handleToggleReview = () => {
    setReviewed((prev) => ({
      ...prev,
      [currentQuestion.id]: !prev[currentQuestion.id],
    }))
  }

  const handlePrevious = () => {
    setCurrentIndex((index) => Math.max(index - 1, 0))
  }

  const handleNext = () => {
    setCurrentIndex((index) => Math.min(index + 1, quiz.questions.length - 1))
  }

  const handleSubmit = async () => {
    if (quiz.questions.some((question) => answers[question.id] === undefined)) {
      setError('Please answer every question before submitting.')
      return
    }

    if (!user) {
      setError('You must be signed in to submit this quiz.')
      return
    }

    const attempt = {
      id: `attempt-${Date.now()}-${Math.random().toString(36).slice(2)}`,
      quizId: quiz.id,
      employeeId: user.id,
      answers: quiz.questions.map((question) => ({
        questionId: question.id,
        selectedOptionIndex: answers[question.id] ?? 0,
      })),
      score: calculateScore(quiz.questions, answers),
      startedAt,
      submittedAt: new Date().toISOString(),
    }

    const createdAttempt = await submitAttempt(attempt)
    navigate(`/quizzes/${quiz.id}/result/${createdAttempt.id}`)
  }

  return (
    <Stack spacing={3}>
      {error && <Alert severity="error">{error}</Alert>}

      <Box>
        <Typography variant="h5" gutterBottom>
          {quiz.title}
        </Typography>
        <Typography variant="body2" color="text.secondary">
          {quiz.description}
        </Typography>
      </Box>

      <Box
        sx={{
          display: 'grid',
          gap: 3,
          gridTemplateColumns: { xs: '1fr', md: '1.3fr 0.7fr' },
          alignItems: 'start',
        }}
      >
        <Card variant="outlined">
          <CardContent>
          <Stack spacing={2}>
            <Box>
              <Typography variant="subtitle1" gutterBottom>
                Question {currentIndex + 1} of {quiz.questions.length}
              </Typography>
              <Typography variant="h6">{currentQuestion.text}</Typography>
            </Box>

            <FormControl component="fieldset">
              <RadioGroup
                value={selectedValue !== undefined ? String(selectedValue) : ''}
                onChange={(event) => handleOptionChange(Number(event.target.value))}
              >
                {currentQuestion.options.map((option, index) => (
                  <FormControlLabel
                    key={option}
                    value={String(index)}
                    control={<Radio />}
                    label={option}
                  />
                ))}
              </RadioGroup>
            </FormControl>
          </Stack>
        </CardContent>

        <Divider />

        <CardActions sx={{ flexWrap: 'wrap', gap: 1 }}>
          <Button onClick={handlePrevious} disabled={currentIndex === 0}>
            Previous
          </Button>
          <Button
            onClick={handleNext}
            disabled={currentIndex === quiz.questions.length - 1}
          >
            Next
          </Button>
          <Button
            variant={reviewed[currentQuestion.id] ? 'contained' : 'outlined'}
            color={reviewed[currentQuestion.id] ? 'warning' : 'inherit'}
            onClick={handleToggleReview}
          >
            {reviewed[currentQuestion.id] ? 'Unmark review' : 'Mark for review'}
          </Button>
          <Box sx={{ flexGrow: 1 }} />
          <Button variant="contained" onClick={handleSubmit}>
            Submit
          </Button>
        </CardActions>
      </Card>

      <Card variant="outlined" sx={{ height: 'fit-content' }}>
        <CardContent>
          <QuestionStepper
            questions={quiz.questions}
            currentIndex={currentIndex}
            onSelect={setCurrentIndex}
            answers={answers}
            reviewStatus={reviewed}
          />
        </CardContent>
      </Card>
    </Box>

      <Button variant="outlined" onClick={() => navigate('/quizzes')}>
        Back to quiz list
      </Button>
    </Stack>
  )
}
