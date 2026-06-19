# Admin Management Guide

## Goal
Provide a simple admin experience for creating, editing, and publishing quizzes.

## Admin Pages

### `QuizListPage`

- Lists all quizzes from the mock API
- Shows quiz title, description, status, and question count
- Includes actions for edit, results, and delete
- Has a `New Quiz` button to open the editor

### `QuizEditorPage`

- Edits quiz details and questions
- Supports:
  - title
  - description
  - published toggle
  - adding and removing questions
  - editing options and correct answer index
- Saves via `createQuiz` or `saveQuiz`

### `AdminResultsPage`

- currently a placeholder component
- intended for future implementation of quiz result dashboards

## Data API

- `src/api/quizApi.ts`
  - `fetchAllQuizzes()`
  - `fetchQuizById(quizId)`
  - `createQuiz(quiz)`
  - `saveQuiz(quiz)`
  - `removeQuiz(quizId)`

- `src/data/mockData.ts`
  - in-memory quiz store
  - helper functions for CRUD operations

## How to Extend

1. Add validation for required question fields.
2. Add result aggregation in `AdminResultsPage`.
3. Add publishing state and quiz visibility controls.
4. Connect to a real API when ready.
