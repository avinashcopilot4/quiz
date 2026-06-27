import type { Attempt } from '../types/attempt.types'
import { apiClient } from './client'

function mapAttempt(attempt: any): Attempt {
  return {
    id: attempt.id,
    quizId: attempt.quizId,
    employeeId: attempt.employeeId,
    answers: (attempt.answers ?? []).map((answer: any) => ({
      questionId: answer.questionId,
      selectedOptionIndex: answer.selectedOptionIndex,
    })),
    score: attempt.score ?? 0,
    startedAt: attempt.startedAt ?? new Date().toISOString(),
    submittedAt: attempt.submittedAt ?? new Date().toISOString(),
  }
}

export async function fetchAttemptsByEmployee(employeeId: string): Promise<Attempt[]> {
  const attempts = await apiClient.get<any[]>(`/attempts/by-employee/${employeeId}`)
  return attempts.map(mapAttempt)
}

export async function fetchAttemptsByQuiz(quizId: string): Promise<Attempt[]> {
  const attempts = await apiClient.get<any[]>(`/attempts/by-quiz/${quizId}`)
  return attempts.map(mapAttempt)
}

export async function fetchAttemptById(attemptId: string): Promise<Attempt | undefined> {
  return apiClient.get<any>(`/attempts/${attemptId}`).then(mapAttempt).catch(() => undefined)
}

export async function submitAttempt(attempt: Attempt): Promise<Attempt> {
  const created = await apiClient.post<any>('/attempts', {
    userId: Number(attempt.employeeId),
    quizId: Number(attempt.quizId),
    score: attempt.score,
    maxPossibleScore: 0,
    attemptedQuestions: attempt.answers.length,
    completedDate: attempt.submittedAt,
    attemptDetails: attempt.answers.map((answer) => ({
      questionId: Number(answer.questionId),
      userAnswer: answer.selectedOptionIndex,
      correctAnswer: 1,
      isCorrect: false,
    })),
  })
  return mapAttempt(created)
}
