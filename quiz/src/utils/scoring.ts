import type { QuestionModel } from '../types/quiz.types';

export function calculateScore(
  questions: QuestionModel[],
  answers: { [questionId: string]: number },
): number {
  return questions.reduce(
    (score, question) => score + (answers[question.id] === question.correctOptionIndex ? 1 : 0),
    0,
  );
}
