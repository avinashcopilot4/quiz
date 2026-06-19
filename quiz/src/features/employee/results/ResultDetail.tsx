import { useEffect, useMemo, useState } from 'react'
import { useParams, useNavigate } from 'react-router-dom'
import Box from '@mui/material/Box'
import Button from '@mui/material/Button'
import Card from '@mui/material/Card'
import CardContent from '@mui/material/CardContent'
import Typography from '@mui/material/Typography'
import Stack from '@mui/material/Stack'
import Chip from '@mui/material/Chip'
import Divider from '@mui/material/Divider'
import { fetchAttemptById } from '../../../api/attemptApi'
import { fetchQuizById } from '../../../api/quizApi'
import type { Attempt } from '../../../types/attempt.types'
import type { QuizModel } from '../../../types/quiz.types'

export function ResultDetail() {
  const { id, attemptId } = useParams()
  const navigate = useNavigate()
  const [attempt, setAttempt] = useState<Attempt | null>(null)
  const [quiz, setQuiz] = useState<QuizModel | null>(null)
  const [loading, setLoading] = useState(true)

  useEffect(() => {
    const loadResult = async () => {
      if (!attemptId || !id) {
        setLoading(false)
        return
      }

      const fetchedAttempt = await fetchAttemptById(attemptId)
      const fetchedQuiz = await fetchQuizById(id)

      setAttempt(fetchedAttempt ?? null)
      setQuiz(fetchedQuiz ?? null)
      setLoading(false)
    }

    loadResult()
  }, [attemptId, id])

  const correctCount = useMemo(
    () =>
      attempt && quiz
        ? attempt.answers.filter((answer) => {
            const question = quiz.questions.find((q) => q.id === answer.questionId)
            return question?.correctOptionIndex === answer.selectedOptionIndex
          }).length
        : 0,
    [attempt, quiz],
  )

  if (loading) {
    return <Typography>Loading result...</Typography>
  }

  if (!attempt || !quiz) {
    return <Typography>Result not found.</Typography>
  }

  return (
    <Stack spacing={3}>
      <Box>
        <Typography variant="h5" gutterBottom>
          {quiz.title}
        </Typography>
        <Typography variant="body2" color="text.secondary">
          Your score: {attempt.score} / {quiz.questions.length}
        </Typography>
      </Box>

      <Card variant="outlined">
        <CardContent>
          <Stack spacing={2}>
            <Typography variant="subtitle1">Attempt details</Typography>
            <Typography>Started: {new Date(attempt.startedAt).toLocaleString()}</Typography>
            <Typography>Submitted: {new Date(attempt.submittedAt).toLocaleString()}</Typography>
            <Chip label={`${correctCount} correct`} color="success" size="small" />
          </Stack>
        </CardContent>
      </Card>

      <Stack spacing={2}>
        {quiz.questions.map((question, index) => {
          const answer = attempt.answers.find((item) => item.questionId === question.id)
          const selectedOption = answer?.selectedOptionIndex
          const isCorrect = selectedOption === question.correctOptionIndex

          return (
            <Card key={question.id} variant="outlined">
              <CardContent>
                <Stack spacing={1}>
                  <Stack direction="row" spacing={1} sx={{ alignItems: 'center' }}>
                    <Typography variant="subtitle1">Question {index + 1}</Typography>
                    <Chip label={isCorrect ? 'Correct' : 'Incorrect'} color={isCorrect ? 'success' : 'error'} size="small" />
                  </Stack>
                  <Typography>{question.text}</Typography>
                  <Divider />
                  {question.options.map((option, optionIndex) => {
                    const selected = optionIndex === selectedOption
                    const correct = optionIndex === question.correctOptionIndex

                    return (
                      <Box key={optionIndex} sx={{ display: 'flex', alignItems: 'center', gap: 1 }}>
                        <Chip
                          label={selected ? 'Your answer' : correct ? 'Correct answer' : ''}
                          color={selected ? 'primary' : correct ? 'success' : 'default'}
                          size="small"
                          sx={{ minWidth: 110 }}
                        />
                        <Typography sx={{ color: selected && !correct ? 'error.main' : undefined }}>
                          {option}
                        </Typography>
                      </Box>
                    )
                  })}
                </Stack>
              </CardContent>
            </Card>
          )
        })}
      </Stack>

      <Button variant="outlined" onClick={() => navigate('/my-results')}>
        Back to my results
      </Button>
    </Stack>
  )
}
