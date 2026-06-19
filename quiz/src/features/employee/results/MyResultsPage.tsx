import { useEffect, useState } from 'react'
import { useNavigate } from 'react-router-dom'
import Box from '@mui/material/Box'
import Button from '@mui/material/Button'
import Card from '@mui/material/Card'
import CardContent from '@mui/material/CardContent'
import Typography from '@mui/material/Typography'
import Stack from '@mui/material/Stack'
import { useAuth } from '../../auth/useAuth'
import { fetchAttemptsByEmployee } from '../../../api/attemptApi'
import { fetchQuizById } from '../../../api/quizApi'
import type { Attempt } from '../../../types/attempt.types'

export function MyResultsPage() {
  const navigate = useNavigate()
  const { user } = useAuth()
  const [attempts, setAttempts] = useState<Attempt[]>([])
  const [quizTitles, setQuizTitles] = useState<Record<string, string>>({})
  const [loading, setLoading] = useState(true)

  useEffect(() => {
    const loadAttempts = async () => {
      if (!user) {
        setLoading(false)
        return
      }

      const results = await fetchAttemptsByEmployee(user.id)
      setAttempts(results)

      const titles: Record<string, string> = {}
      await Promise.all(
        results.map(async (attempt) => {
          const quiz = await fetchQuizById(attempt.quizId)
          titles[attempt.quizId] = quiz?.title ?? 'Unknown quiz'
        }),
      )

      setQuizTitles(titles)
      setLoading(false)
    }

    loadAttempts()
  }, [user])

  return (
    <Box>
      <Typography variant="h5" gutterBottom>
        My Results
      </Typography>
      <Typography variant="body2" color="text.secondary" gutterBottom>
        Review quizzes you have completed and inspect your score details.
      </Typography>

      {loading ? (
        <Typography>Loading results...</Typography>
      ) : attempts.length === 0 ? (
        <Typography>You have not completed any quizzes yet.</Typography>
      ) : (
        <Stack spacing={2}>
          {attempts.map((attempt) => (
            <Card key={attempt.id} variant="outlined">
              <CardContent>
                <Typography variant="h6">{quizTitles[attempt.quizId] ?? 'Quiz'}</Typography>
                <Typography color="text.secondary" sx={{ mb: 1 }}>
                  Score: {attempt.score}
                </Typography>
                <Typography variant="body2" color="text.secondary">
                  Completed on {new Date(attempt.submittedAt).toLocaleString()}
                </Typography>
                <Button
                  size="small"
                  sx={{ mt: 1 }}
                  onClick={() => navigate(`/quizzes/${attempt.quizId}/result/${attempt.id}`)}
                >
                  View details
                </Button>
              </CardContent>
            </Card>
          ))}
        </Stack>
      )}
    </Box>
  )
}
