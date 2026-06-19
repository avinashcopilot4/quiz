import Box from '@mui/material/Box'
import Button from '@mui/material/Button'
import Typography from '@mui/material/Typography'
import type { SxProps, Theme } from '@mui/system'

interface QuestionStepperProps {
  questions: {
    id: string
    text: string
  }[]
  currentIndex: number
  onSelect: (index: number) => void
  answers: Record<string, number>
  reviewStatus: Record<string, boolean>
}

const buttonStyles: SxProps<Theme> = {
  width: '100%',
  aspectRatio: '1 / 1',
  minWidth: 0,
  p: 0,
  fontSize: '0.75rem',
  borderRadius: '50%',
}

export function QuestionStepper({ questions, currentIndex, onSelect, answers, reviewStatus }: QuestionStepperProps) {
  const answeredCount = Object.keys(answers).length
  const reviewCount = Object.values(reviewStatus).filter(Boolean).length
  const unansweredCount = questions.length - answeredCount

  return (
    <Box sx={{ mb: 2 }}>
      <Typography variant="subtitle2" gutterBottom>
        Question navigator
      </Typography>

      <Box sx={{ display: 'flex', flexWrap: 'wrap', gap: 1, mb: 2, alignItems: 'center' }}>
        <Typography variant="body2" color="text.secondary" sx={{ mr: 2 }}>
          {questions.length} questions total
        </Typography>
        <Box sx={{ display: 'flex', flexWrap: 'wrap', gap: 1, alignItems: 'center' }}>
          <Typography variant="caption" color="success.main">
            {answeredCount} answered
          </Typography>
          <Typography variant="caption" color="warning.main">
            {reviewCount} review
          </Typography>
          <Typography variant="caption" color="text.secondary">
            {unansweredCount} unanswered
          </Typography>
        </Box>
      </Box>

      <Box
        sx={{
          display: 'grid',
          gridTemplateColumns: { xs: 'repeat(5, minmax(0, 1fr))', sm: 'repeat(4, minmax(0, 1fr))' },
          gap: 1,
        }}
      >
        {questions.map((question, index) => {
          const answered = answers[question.id] !== undefined
          const reviewed = reviewStatus[question.id]
          const selected = index === currentIndex
          const bg = selected
            ? 'primary.main'
            : reviewed
            ? 'warning.light'
            : answered
            ? 'success.light'
            : 'background.paper'
          const color = selected ? 'primary.contrastText' : 'text.primary'
          const borderColor = selected ? 'primary.main' : reviewed ? 'warning.main' : answered ? 'success.main' : 'divider'

          return (
            <Button
              key={question.id}
              size="small"
              variant="outlined"
              onClick={() => onSelect(index)}
              sx={{
                ...buttonStyles,
                bgcolor: bg,
                color,
                borderColor,
                '&:hover': {
                  bgcolor: selected ? 'primary.dark' : 'action.hover',
                },
              }}
            >
              {index + 1}
            </Button>
          )
        })}
      </Box>

      <Box sx={{ mt: 2, display: 'flex', flexDirection: 'column', gap: 1 }}>
        <Box sx={{ display: 'flex', alignItems: 'center', gap: 1 }}>
          <Box sx={{ width: 10, height: 10, borderRadius: '50%', bgcolor: 'success.main' }} />
          <Typography variant="caption">Answered</Typography>
        </Box>
        <Box sx={{ display: 'flex', alignItems: 'center', gap: 1 }}>
          <Box sx={{ width: 10, height: 10, borderRadius: '50%', bgcolor: 'warning.main' }} />
          <Typography variant="caption">Marked for review</Typography>
        </Box>
        <Box sx={{ display: 'flex', alignItems: 'center', gap: 1 }}>
          <Box sx={{ width: 10, height: 10, borderRadius: '50%', bgcolor: 'text.disabled' }} />
          <Typography variant="caption">Unanswered</Typography>
        </Box>
      </Box>
    </Box>
  )
}
