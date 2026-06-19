import sampleQuizData from './sample-questions.json'
import type { QuizModel } from '../types/quiz.types'
import type { User, UserRole } from '../types/user.types'
import type { Attempt } from '../types/attempt.types'

export const mockQuizzes: QuizModel[] = [sampleQuizData]

export const mockUsers: User[] = [
  {
    id: 'admin-1',
    name: 'Admin User',
    email: 'admin@example.com',
    role: 'admin',
  },
  {
    id: 'employee-1',
    name: 'Employee User',
    email: 'employee@example.com',
    role: 'employee',
  },
]

export const mockAttempts: Attempt[] = [
  {
    id: 'attempt-1',
    quizId: mockQuizzes[0].id,
    employeeId: mockUsers[1].id,
    answers: [
      { questionId: mockQuizzes[0].questions[0].id, selectedOptionIndex: 0 },
      { questionId: mockQuizzes[0].questions[1].id, selectedOptionIndex: 1 },
      { questionId: mockQuizzes[0].questions[2].id, selectedOptionIndex: 0 },
    ],
    score: 3,
    startedAt: new Date(Date.now() - 1000 * 60 * 10).toISOString(),
    submittedAt: new Date(Date.now() - 1000 * 60 * 5).toISOString(),
  },
]

export function getMockUserByEmail(email: string): User | undefined {
  return mockUsers.find((user) => user.email.toLowerCase() === email.toLowerCase())
}

export function getMockUserById(id: string): User | undefined {
  return mockUsers.find((user) => user.id === id)
}

export function getMockQuizById(id: string): QuizModel | undefined {
  return mockQuizzes.find((quiz) => quiz.id === id)
}

export function getAttemptsByEmployee(employeeId: string): Attempt[] {
  return mockAttempts.filter((attempt) => attempt.employeeId === employeeId)
}

export function getAttemptsByQuiz(quizId: string): Attempt[] {
  return mockAttempts.filter((attempt) => attempt.quizId === quizId)
}
