import type { QuizModel } from '../types/quiz.types'
import { mockQuizzes } from '../data/mockData'

export async function fetchPublishedQuizzes(): Promise<QuizModel[]> {
  return mockQuizzes.filter((quiz) => quiz.isPublished)
}

export async function fetchAllQuizzes(): Promise<QuizModel[]> {
  return mockQuizzes
}

export async function fetchQuizById(quizId: string): Promise<QuizModel | undefined> {
  return mockQuizzes.find((quiz) => quiz.id === quizId)
}
