import type { QuizModel } from '../types/quiz.types'
import { addMockQuiz, deleteMockQuiz, getMockQuizById, mockQuizzes, updateMockQuiz } from '../data/mockData'

export async function fetchPublishedQuizzes(): Promise<QuizModel[]> {
  return mockQuizzes.filter((quiz) => quiz.isPublished)
}

export async function fetchAllQuizzes(): Promise<QuizModel[]> {
  return mockQuizzes
}

export async function fetchQuizById(quizId: string): Promise<QuizModel | undefined> {
  return getMockQuizById(quizId)
}

export async function createQuiz(quiz: QuizModel): Promise<QuizModel> {
  addMockQuiz(quiz)
  return quiz
}

export async function saveQuiz(quiz: QuizModel): Promise<QuizModel> {
  updateMockQuiz(quiz)
  return quiz
}

export async function removeQuiz(quizId: string): Promise<void> {
  deleteMockQuiz(quizId)
}
