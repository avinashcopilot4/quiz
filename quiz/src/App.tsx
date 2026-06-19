import { useState } from 'react'
import Box from '@mui/material/Box'
import Container from '@mui/material/Container'
import Typography from '@mui/material/Typography'
import { QuizListPage } from './features/employee/quizzes/QuizListPage'
import { AttemptPage } from './features/employee/attempt/AttemptPage'
import type { QuizModel } from './types/quiz.types'

function App() {
  const [activeQuiz, setActiveQuiz] = useState<QuizModel | null>(null)

  return (
    <Container maxWidth="lg" sx={{ py: 4 }}>
      <Box mb={4}>
        <Typography variant="h4" component="h1" gutterBottom>
          Quiz App
        </Typography>
        <Typography color="text.secondary">
          Select a quiz and answer the questions using the Material UI interface.
        </Typography>
      </Box>

      {activeQuiz ? (
        <AttemptPage quiz={activeQuiz} onExit={() => setActiveQuiz(null)} />
      ) : (
        <QuizListPage onStartQuiz={setActiveQuiz} />
      )}
    </Container>
  )
}

export default App
