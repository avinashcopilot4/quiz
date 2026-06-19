export type QuizStatus = 'draft' | 'published' | 'terminated'

export interface QuizModel {
  id: string;
  title: string;
  description: string;
  createdBy: string;
  createdAt: string;
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
