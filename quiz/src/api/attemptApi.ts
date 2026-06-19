import type { Attempt } from '../types/attempt.types'
import { addMockAttempt, getMockAttemptById, getAttemptsByEmployee, getAttemptsByQuiz } from '../data/mockData'

export async function fetchAttemptsByEmployee(employeeId: string): Promise<Attempt[]> {
  return getAttemptsByEmployee(employeeId)
}

export async function fetchAttemptsByQuiz(quizId: string): Promise<Attempt[]> {
  return getAttemptsByQuiz(quizId)
}

export async function fetchAttemptById(attemptId: string): Promise<Attempt | undefined> {
  return getMockAttemptById(attemptId)
}

export async function submitAttempt(attempt: Attempt): Promise<Attempt> {
  addMockAttempt(attempt)
  return attempt
}
