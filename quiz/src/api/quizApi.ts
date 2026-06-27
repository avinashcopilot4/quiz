import type { QuizModel } from '../types/quiz.types'
import { apiClient } from './client'

function mapQuiz(quiz: any): QuizModel {
  return {
    id: quiz.id,
    title: quiz.title,
    description: quiz.description ?? '',
    topic: quiz.topic ?? 'General',
    language: quiz.language ?? 'English',
    difficulty: quiz.difficulty ?? 'Intermediate',
    tags: quiz.tags ?? [],
    createdBy: quiz.createdBy ?? 'admin',
    author: quiz.author ?? 'Admin',
    createdAt: quiz.createdAt ?? new Date().toISOString(),
    updatedAt: quiz.updatedAt,
    questionCount: quiz.questionCount ?? quiz.questions?.length ?? 0,
    timeLimit: quiz.timeLimit,
    passingScore: quiz.passingScore,
    questions: (quiz.questions ?? []).map((question: any) => ({
      id: question.id,
      text: question.text,
      options: question.options ?? [],
      correctOptionIndex: question.correctOptionIndex ?? 0,
    })),
    status: (quiz.status ?? 'draft').toLowerCase() as QuizModel['status'],
  }
}

export async function fetchPublishedQuizzes(): Promise<QuizModel[]> {
  const quizzes = await apiClient.get<any[]>('/quizzes?status=published')
  return quizzes.map(mapQuiz)
}

export async function fetchAllQuizzes(): Promise<QuizModel[]> {
  const quizzes = await apiClient.get<any[]>('/quizzes')
  return quizzes.map(mapQuiz)
}

export async function fetchQuizById(quizId: string): Promise<QuizModel | undefined> {
  return apiClient.get<any>(`/quizzes/${quizId}`).then(mapQuiz).catch(() => undefined)
}

export async function createQuiz(quiz: QuizModel): Promise<QuizModel> {
  const created = await apiClient.post<any>('/quizzes', {
    title: quiz.title,
    description: quiz.description,
    status: quiz.status,
    questions: quiz.questions.map((question) => ({
      questionText: question.text,
      option1: question.options[0],
      option2: question.options[1],
      option3: question.options[2],
      option4: question.options[3],
      correctOption: question.correctOptionIndex + 1,
    })),
  })
  return mapQuiz(created)
}

export async function saveQuiz(quiz: QuizModel): Promise<QuizModel> {
  const updated = await apiClient.put<any>(`/quizzes/${quiz.id}`, {
    quizId: Number(quiz.id),
    title: quiz.title,
    description: quiz.description,
    status: quiz.status,
    questions: quiz.questions.map((question) => ({
      questionId: Number(question.id),
      questionText: question.text,
      option1: question.options[0],
      option2: question.options[1],
      option3: question.options[2],
      option4: question.options[3],
      correctOption: question.correctOptionIndex + 1,
    })),
  })
  return mapQuiz(updated)
}

export async function removeQuiz(quizId: string): Promise<void> {
  await apiClient.delete(`/quizzes/${quizId}`)
}
