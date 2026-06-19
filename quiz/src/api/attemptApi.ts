import type { Attempt } from '../types/attempt.types'
import { mockAttempts } from '../data/mockData'

export async function fetchAttemptsByEmployee(employeeId: string): Promise<Attempt[]> {
  return mockAttempts.filter((attempt) => attempt.employeeId === employeeId)
}

export async function fetchAttemptsByQuiz(quizId: string): Promise<Attempt[]> {
  return mockAttempts.filter((attempt) => attempt.quizId === quizId)
}

export async function submitAttempt(attempt: Attempt): Promise<Attempt> {
  mockAttempts.push(attempt)
  return attempt
}
