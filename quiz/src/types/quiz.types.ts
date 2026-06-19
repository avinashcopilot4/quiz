export type QuizStatus = 'draft' | 'published' | 'terminated'
export type QuizDifficulty = 'Beginner' | 'Intermediate' | 'Advanced'

export interface QuizModel {
  id: string;
  title: string;
  description: string;
  topic: string;
  language: string;
  difficulty: QuizDifficulty;
  tags: string[];
  createdBy: string;
  author: string;
  createdAt: string;
  updatedAt?: string;
  questionCount: number;
  timeLimit?: number;
  passingScore?: number;
  questions: QuestionModel[];
  status: QuizStatus;
}

export interface QuestionModel {
  id: string;
  text: string;
  options: string[];
  correctOptionIndex: number;
}

export type QuizJson = QuizModel;
export type QuestionJson = QuestionModel;
