import { useEffect, useState } from 'react'
import { useNavigate, useParams } from 'react-router-dom'
import Box from '@mui/material/Box'
import Button from '@mui/material/Button'
import Card from '@mui/material/Card'
import CardContent from '@mui/material/CardContent'
import Typography from '@mui/material/Typography'
import Stack from '@mui/material/Stack'
import { fetchAttemptsByQuiz } from '../../../api/attemptApi'
import { fetchQuizById } from '../../../api/quizApi'
import { fetchUserById } from '../../../api/authApi'
import type { Attempt } from '../../../types/attempt.types'
import type { QuizModel } from '../../../types/quiz.types'

export function AdminResultsPage() {
  const { id } = useParams()
  const navigate = useNavigate()
  const [quiz, setQuiz] = useState<QuizModel | null>(null)
  const [attempts, setAttempts] = useState<Attempt[]>([])
  const [employeeNames, setEmployeeNames] = useState<Record<string, string>>({})
  const [loading, setLoading] = useState(true)

  useEffect(() => {
    const loadResults = async () => {
      if (!id) {
        setLoading(false)
        return
      }

      const [fetchedQuiz, results] = await Promise.all([
        fetchQuizById(id),
        fetchAttemptsByQuiz(id),
      ])

      const names: Record<string, string> = {}
      await Promise.all(
        results.map(async (attempt) => {
          const employee = await fetchUserById(attempt.employeeId)
          if (employee) {
            names[attempt.employeeId] = employee.name
          }
        }),
      )

      setQuiz(fetchedQuiz ?? null)
      setAttempts(results)
      setEmployeeNames(names)
      setLoading(false)
    }

    loadResults()
  }, [id])

  return (
    <Box>
      <Typography variant="h5" gutterBottom>
        Admin Results
      </Typography>
      <Typography variant="body2" color="text.secondary" gutterBottom>
        Review employee quiz attempts for this quiz.
      </Typography>

      {loading ? (
        <Typography>Loading results...</Typography>
      ) : !quiz ? (
        <Typography>Quiz not found.</Typography>
      ) : attempts.length === 0 ? (
        <Typography>No attempts have been submitted for this quiz yet.</Typography>
      ) : (
        <Stack spacing={2}>
          {attempts.map((attempt) => (
            <Card key={attempt.id} variant="outlined">
              <CardContent>
                <Typography variant="h6">{employeeNames[attempt.employeeId] ?? 'Employee'}</Typography>
                <Typography color="text.secondary">
                  Score: {attempt.score} / {quiz.questions.length}
                </Typography>
                <Typography variant="body2" color="text.secondary">
                  Submitted: {new Date(attempt.submittedAt).toLocaleString()}
                </Typography>
                <Button
                  size="small"
                  sx={{ mt: 1 }}
                  onClick={() => navigate(`/admin/quizzes/${quiz.id}/result/${attempt.id}`)}
                >
                  View attempt
                </Button>
              </CardContent>
            </Card>
          ))}
        </Stack>
      )}
    </Box>
  )
}
