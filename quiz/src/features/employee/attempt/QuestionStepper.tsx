import Box from '@mui/material/Box'
import Stepper from '@mui/material/Stepper'
import Step from '@mui/material/Step'
import StepButton from '@mui/material/StepButton'
import Typography from '@mui/material/Typography'

interface QuestionStepperProps {
  questions: {
    id: string
    text: string
  }[]
  currentIndex: number
  onSelect: (index: number) => void
}

export function QuestionStepper({ questions, currentIndex, onSelect }: QuestionStepperProps) {
  return (
    <Box sx={{ mb: 2 }}>
      <Typography variant="subtitle2" gutterBottom>
        Question navigation
      </Typography>
      <Stepper activeStep={currentIndex} alternativeLabel>
        {questions.map((question, index) => (
          <Step key={question.id}>
            <StepButton onClick={() => onSelect(index)}>
              {index + 1}
            </StepButton>
          </Step>
        ))}
      </Stepper>
    </Box>
  )
}
