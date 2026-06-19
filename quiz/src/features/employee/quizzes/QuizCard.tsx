import Card from '@mui/material/Card'
import CardContent from '@mui/material/CardContent'
import CardActions from '@mui/material/CardActions'
import Button from '@mui/material/Button'
import Typography from '@mui/material/Typography'
import Chip from '@mui/material/Chip'
import Stack from '@mui/material/Stack'
import type { QuizModel } from '../../../types/quiz.types'

interface QuizCardProps {
  quiz: QuizModel
  onStart: (quiz: QuizModel) => void
}

export function QuizCard({ quiz, onStart }: QuizCardProps) {
  return (
    <Card variant="outlined" sx={{ display: 'flex', flexDirection: 'column', height: '100%' }}>
      <CardContent sx={{ flexGrow: 1 }}>
        <Stack direction="row" spacing={2} sx={{ alignItems: 'center', justifyContent: 'space-between', mb: 1 }}>
          <Typography variant="h6">{quiz.title}</Typography>
          <Chip
            label={
              quiz.status === 'published'
                ? 'Published'
                : quiz.status === 'draft'
                ? 'Draft'
                : 'Terminated'
            }
            color={quiz.status === 'published' ? 'success' : quiz.status === 'draft' ? 'default' : 'error'}
            size="small"
          />
        </Stack>
        <Typography variant="body2" color="text.secondary" component="p" gutterBottom>
          {quiz.description}
        </Typography>
        <Typography variant="caption" color="text.secondary">
          {quiz.questions.length} question{quiz.questions.length === 1 ? '' : 's'} · created by {quiz.createdBy}
        </Typography>
      </CardContent>

      <CardActions>
        <Button fullWidth variant="contained" onClick={() => onStart(quiz)}>
          Start Quiz
        </Button>
      </CardActions>
    </Card>
  )
}
