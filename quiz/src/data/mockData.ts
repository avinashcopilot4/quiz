import sampleQuizData from './sample-questions.json'
import type { QuizModel } from '../types/quiz.types'
import type { User } from '../types/user.types'
import type { Attempt } from '../types/attempt.types'

const initialSampleQuiz = sampleQuizData as QuizModel

export const mockUsers: User[] = [
  {
    id: 'admin-1',
    name: 'Admin User',
    email: 'admin@example.com',
    roles: ['admin', 'employee'],
    activeRole: 'admin',
  },
  {
    id: 'employee-1',
    name: 'Employee User',
    email: 'employee@example.com',
    roles: ['employee'],
    activeRole: 'employee',
  },
]

const QUIZZES_SESSION_KEY = 'quiz-app-quizzes'
const ATTEMPTS_SESSION_KEY = 'quiz-app-attempts'

const defaultMockQuizzes: QuizModel[] = [initialSampleQuiz]

export function getStoredQuizzes(): QuizModel[] {
  if (typeof window === 'undefined') {
    return [...defaultMockQuizzes]
  }

  const stored = window.sessionStorage.getItem(QUIZZES_SESSION_KEY)
  if (!stored) {
    window.sessionStorage.setItem(QUIZZES_SESSION_KEY, JSON.stringify(defaultMockQuizzes))
    return [...defaultMockQuizzes]
  }

  try {
    return JSON.parse(stored) as QuizModel[]
  } catch {
    window.sessionStorage.setItem(QUIZZES_SESSION_KEY, JSON.stringify(defaultMockQuizzes))
    return [...defaultMockQuizzes]
  }
}

function saveStoredQuizzes(quizzes: QuizModel[]) {
  if (typeof window === 'undefined') return
  window.sessionStorage.setItem(QUIZZES_SESSION_KEY, JSON.stringify(quizzes))
}

const defaultMockAttempts: Attempt[] = [
  {
    id: 'attempt-1',
    quizId: initialSampleQuiz.id,
    employeeId: mockUsers[1].id,
    answers: [
      { questionId: initialSampleQuiz.questions[0].id, selectedOptionIndex: 0 },
      { questionId: initialSampleQuiz.questions[1].id, selectedOptionIndex: 1 },
      { questionId: initialSampleQuiz.questions[2].id, selectedOptionIndex: 0 },
    ],
    score: 3,
    startedAt: new Date(Date.now() - 1000 * 60 * 10).toISOString(),
    submittedAt: new Date(Date.now() - 1000 * 60 * 5).toISOString(),
  },
]

function getStoredAttempts(): Attempt[] {
  if (typeof window === 'undefined') {
    return [...defaultMockAttempts]
  }

  const stored = window.sessionStorage.getItem(ATTEMPTS_SESSION_KEY)
  if (!stored) {
    window.sessionStorage.setItem(ATTEMPTS_SESSION_KEY, JSON.stringify(defaultMockAttempts))
    return [...defaultMockAttempts]
  }

  try {
    return JSON.parse(stored) as Attempt[]
  } catch {
    window.sessionStorage.setItem(ATTEMPTS_SESSION_KEY, JSON.stringify(defaultMockAttempts))
    return [...defaultMockAttempts]
  }
}

function saveStoredAttempts(attempts: Attempt[]) {
  if (typeof window === 'undefined') return
  window.sessionStorage.setItem(ATTEMPTS_SESSION_KEY, JSON.stringify(attempts))
}

export function getAttemptsByEmployee(employeeId: string): Attempt[] {
  return getStoredAttempts().filter((attempt) => attempt.employeeId === employeeId)
}

export function getAttemptsByQuiz(quizId: string): Attempt[] {
  return getStoredAttempts().filter((attempt) => attempt.quizId === quizId)
}

export function getMockAttemptById(id: string): Attempt | undefined {
  return getStoredAttempts().find((attempt) => attempt.id === id)
}

export function addMockAttempt(attempt: Attempt): void {
  const stored = getStoredAttempts()
  stored.push(attempt)
  saveStoredAttempts(stored)
}

export function getMockUserByEmail(email: string): User | undefined {
  return mockUsers.find((user) => user.email.toLowerCase() === email.toLowerCase())
}

export function getMockUserById(id: string): User | undefined {
  return mockUsers.find((user) => user.id === id)
}

export function getMockQuizById(id: string): QuizModel | undefined {
  return getStoredQuizzes().find((quiz) => quiz.id === id)
}

export function addMockQuiz(quiz: QuizModel): void {
  const quizzes = getStoredQuizzes()
  quizzes.push(quiz)
  saveStoredQuizzes(quizzes)
}

export function updateMockQuiz(quiz: QuizModel): void {
  const quizzes = getStoredQuizzes()
  const index = quizzes.findIndex((item) => item.id === quiz.id)
  if (index >= 0) {
    quizzes[index] = quiz
    saveStoredQuizzes(quizzes)
  }
}

export function deleteMockQuiz(id: string): void {
  const quizzes = getStoredQuizzes().filter((quiz) => quiz.id !== id)
  saveStoredQuizzes(quizzes)
}
