import { useEffect, useState } from 'react'
import { useNavigate } from 'react-router-dom'
import Box from '@mui/material/Box'
import Typography from '@mui/material/Typography'
import { fetchPublishedQuizzes } from '../../../api/quizApi'
import { QuizCard } from './QuizCard'
import type { QuizModel } from '../../../types/quiz.types'

export function QuizListPage() {
  const navigate = useNavigate()
  const [quizzes, setQuizzes] = useState<QuizModel[]>([])
  const [loading, setLoading] = useState(true)

  useEffect(() => {
    const loadQuizzes = async () => {
      const published = await fetchPublishedQuizzes()
      setQuizzes(published)
      setLoading(false)
    }

    loadQuizzes()
  }, [])

  const handleStartQuiz = (selectedQuiz: QuizModel) => {
    navigate(`/quizzes/${selectedQuiz.id}/attempt`)
  }

  return (
    <Box>
      <Typography variant="h5" gutterBottom>
        Available Quizzes
      </Typography>
      <Typography variant="body2" color="text.secondary" component="p" gutterBottom>
        Choose a quiz and answer the questions using the new Material UI experience.
      </Typography>

      {loading ? (
        <Typography>Loading quizzes...</Typography>
      ) : quizzes.length === 0 ? (
        <Typography>No quizzes are published yet.</Typography>
      ) : (
        <Box
          sx={{
            display: 'grid',
            gap: 3,
            gridTemplateColumns: {
              xs: '1fr',
              sm: 'repeat(2, minmax(0, 1fr))',
              md: 'repeat(3, minmax(0, 1fr))',
            },
          }}
        >
          {quizzes.map((quiz) => (
            <Box key={quiz.id}>
              <QuizCard quiz={quiz} onStart={handleStartQuiz} />
            </Box>
          ))}
        </Box>
      )}
    </Box>
  )
}
