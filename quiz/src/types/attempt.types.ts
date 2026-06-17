export interface Answer {
  questionId: string;
  selectedOptionIndex: number;
}

export interface Attempt {
  id: string;
  quizId: string;
  employeeId: string;
  answers: Answer[];
  score: number;
  startedAt: string;
  submittedAt: string;
}
