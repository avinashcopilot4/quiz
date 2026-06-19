export interface QuizModel {
  id: string;
  title: string;
  description: string;
  createdBy: string;
  createdAt: string;
  questions: QuestionModel[];
  isPublished: boolean;
}

export interface QuestionModel {
  id: string;
  text: string;
  options: string[];
  correctOptionIndex: number;
}

export type QuizJson = QuizModel;
export type QuestionJson = QuestionModel;
