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
import type { QuizModel } from '../../../types/quiz.types'
import { calculateScore } from '../../../utils/scoring'
import { QuestionStepper } from './QuestionStepper'
import { fetchQuizById } from '../../../api/quizApi'

export function AttemptPage() {
  const { id } = useParams()
  const navigate = useNavigate()
  const [quiz, setQuiz] = useState<QuizModel | null>(null)
  const [currentIndex, setCurrentIndex] = useState(0)
  const [answers, setAnswers] = useState<Record<string, number>>({})
  const [submitted, setSubmitted] = useState(false)
  const [loading, setLoading] = useState(true)

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

  const quizQuestions = quiz?.questions ?? []
  const score = useMemo(
    () => (submitted ? calculateScore(quizQuestions, answers) : 0),
    [submitted, answers, quizQuestions],
  )

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

  const handlePrevious = () => {
    setCurrentIndex((index) => Math.max(index - 1, 0))
  }

  const handleNext = () => {
    setCurrentIndex((index) => Math.min(index + 1, quiz.questions.length - 1))
  }

  const handleSubmit = () => {
    setSubmitted(true)
  }

  return (
    <Stack spacing={3}>
      <Box>
        <Typography variant="h5" gutterBottom>
          {quiz.title}
        </Typography>
        <Typography variant="body2" color="text.secondary">
          {quiz.description}
        </Typography>
      </Box>

      <QuestionStepper
        questions={quiz.questions}
        currentIndex={currentIndex}
        onSelect={setCurrentIndex}
      />

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

        <CardActions>
          <Button onClick={handlePrevious} disabled={currentIndex === 0}>
            Previous
          </Button>
          <Button
            onClick={handleNext}
            disabled={currentIndex === quiz.questions.length - 1}
          >
            Next
          </Button>
          <Box sx={{ flexGrow: 1 }} />
          <Button
            variant="contained"
            onClick={handleSubmit}
            disabled={submitted}
          >
            Submit
          </Button>
        </CardActions>
      </Card>

      {submitted && (
        <Card variant="outlined" sx={{ p: 2 }}>
          <Typography variant="h6">Quiz Completed</Typography>
          <Typography sx={{ mt: 1 }}>
            Your score: {score} / {quiz.questions.length}
          </Typography>
          <Typography color="text.secondary" sx={{ mt: 1 }}>
            {score === quiz.questions.length
              ? 'Perfect score — great job!'
              : 'Review your answers and try again when you are ready.'}
          </Typography>
        </Card>
      )}

      <Button variant="outlined" onClick={() => navigate('/quizzes')}>
        Back to quiz list
      </Button>
    </Stack>
  )
}
