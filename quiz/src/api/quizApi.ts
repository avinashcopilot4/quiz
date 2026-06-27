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
      correctOptionIndex: (() => {
        const value = Number(question.correctOptionIndex ?? 0)
        return value > 0 ? value - 1 : 0
      })(),
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
      text: question.text,
      options: [
        question.options[0] ?? '',
        question.options[1] ?? '',
        question.options[2] ?? '',
        question.options[3] ?? '',
      ],
      correctOptionIndex: (question.correctOptionIndex ?? 0) + 1,
    })),
  })
  return mapQuiz(created)
}

export async function saveQuiz(quiz: QuizModel): Promise<QuizModel> {
  const payload = {
    id: String(quiz.id),
    title: quiz.title,
    description: quiz.description,
    status: quiz.status,
    questions: quiz.questions.map((question) => ({
      id: Number(question.id) || 0,
      text: question.text,
      options: [
        question.options[0] ?? '',
        question.options[1] ?? '',
        question.options[2] ?? '',
        question.options[3] ?? '',
      ],
      correctOptionIndex: (question.correctOptionIndex ?? 0) + 1,
    })),
  }

  const updated = await apiClient.put<any>(`/quizzes?quizid=${encodeURIComponent(quiz.id)}`, payload)

  if (!updated) {
    return {
      ...quiz,
      updatedAt: new Date().toISOString(),
      questionCount: quiz.questions.length,
    }
  }

  return mapQuiz(updated)
}

export async function removeQuiz(quizId: string): Promise<void> {
  await apiClient.delete(`/quizzes/${quizId}`)
}
