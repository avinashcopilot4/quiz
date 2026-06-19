import { useEffect, useState } from 'react'
import { useNavigate } from 'react-router-dom'
import Box from '@mui/material/Box'
import Button from '@mui/material/Button'
import Card from '@mui/material/Card'
import CardContent from '@mui/material/CardContent'
import Typography from '@mui/material/Typography'
import Stack from '@mui/material/Stack'
import Chip from '@mui/material/Chip'
import { fetchAllQuizzes, removeQuiz } from '../../../api/quizApi'
import type { QuizModel } from '../../../types/quiz.types'

export function QuizListPage() {
  const [quizzes, setQuizzes] = useState<QuizModel[]>([])
  const [loading, setLoading] = useState(true)
  const navigate = useNavigate()

  useEffect(() => {
    const load = async () => {
      const allQuizzes = await fetchAllQuizzes()
      setQuizzes(allQuizzes)
      setLoading(false)
    }

    load()
  }, [])

  const handleDelete = async (quizId: string) => {
    await removeQuiz(quizId)
    setQuizzes((current) => current.filter((quiz) => quiz.id !== quizId))
  }

  return (
    <Box>
      <Stack direction="row" spacing={2} sx={{ alignItems: 'center', justifyContent: 'space-between', mb: 3 }}>
        <Box>
          <Typography variant="h5">Admin Quiz Management</Typography>
          <Typography color="text.secondary">Create, edit, and publish quizzes for employees.</Typography>
        </Box>
        <Button variant="contained" onClick={() => navigate('/admin/quizzes/new')}>
          New Quiz
        </Button>
      </Stack>

      {loading ? (
        <Typography>Loading quizzes...</Typography>
      ) : quizzes.length === 0 ? (
        <Typography>No quizzes available yet.</Typography>
      ) : (
        <Stack spacing={2}>
          {quizzes.map((quiz) => (
            <Card key={quiz.id} variant="outlined">
              <CardContent>
                <Stack direction="row" spacing={2} sx={{ alignItems: 'center', justifyContent: 'space-between' }}>
                  <Box>
                    <Typography variant="h6">{quiz.title}</Typography>
                    <Typography color="text.secondary" sx={{ mt: 0.5 }}>
                      {quiz.description}
                    </Typography>
                    <Typography variant="caption" color="text.secondary">
                      Created by {quiz.createdBy} · {quiz.questions.length} questions
                    </Typography>
                  </Box>
                  <Stack direction="row" spacing={1} sx={{ alignItems: 'center' }}>
                    <Chip
                      label={
                        quiz.status === 'published'
                          ? 'Published'
                          : quiz.status === 'draft'
                          ? 'Draft'
                          : 'Terminated'
                      }
                      color={
                        quiz.status === 'published'
                          ? 'success'
                          : quiz.status === 'draft'
                          ? 'default'
                          : 'error'
                      }
                    />
                    <Button size="small" onClick={() => navigate(`/admin/quizzes/${quiz.id}/edit`)}>
                      Edit
                    </Button>
                    <Button size="small" onClick={() => navigate(`/admin/quizzes/${quiz.id}/results`)}>
                      Results
                    </Button>
                    <Button size="small" color="error" onClick={() => handleDelete(quiz.id)}>
                      Delete
                    </Button>
                  </Stack>
                </Stack>
              </CardContent>
            </Card>
          ))}
        </Stack>
      )}
    </Box>
  )
}
