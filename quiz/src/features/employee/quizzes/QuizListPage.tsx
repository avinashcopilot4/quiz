import Box from '@mui/material/Box'
import Grid from '@mui/material/Grid'
import Typography from '@mui/material/Typography'
import sampleQuizData from '../../../data/sample-questions.json'
import { QuizCard } from './QuizCard'
import type { QuizModel } from '../../../types/quiz.types'

interface QuizListPageProps {
  onStartQuiz: (quiz: QuizModel) => void
}

export function QuizListPage({ onStartQuiz }: QuizListPageProps) {
  const quiz = sampleQuizData as QuizModel

  return (
    <Box>
      <Typography variant="h5" gutterBottom>
        Available Quizzes
      </Typography>
      <Typography variant="body2" color="text.secondary" component="p" gutterBottom>
        Choose a quiz and answer the questions using the new Material UI experience.
      </Typography>

      <Grid container spacing={3}>
        <Grid item xs={12} sm={6} md={4}>
          <QuizCard quiz={quiz} onStart={onStartQuiz} />
        </Grid>
      </Grid>
    </Box>
  )
}
