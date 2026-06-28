# Backend API Reference

## Users
### POST /api/users/login
Authenticates a user with email and password. Optional role can be supplied.

### POST /api/users
Creates a new user.

### GET /api/users
Gets all users.

### GET /api/users/{userid}
Gets a user by ID.

### PUT /api/users/{userid}/profile
Updates a user's profile details such as name, gender, and phone number.

## Quizzes
### GET /api/quizzes
Gets all quizzes.

### GET /api/quizzes/{id}
Gets a quiz by ID.

### POST /api/quizzes
Creates a quiz.

### PUT /api/quizzes/{id}
Updates a quiz.

### DELETE /api/quizzes/{id}
Deletes a quiz.

## Questions
### GET /api/questions/{quizId}
Gets questions for a quiz.

### POST /api/questions
Creates a question.

### PUT /api/questions/{id}
Updates a question.

### DELETE /api/questions/{id}
Deletes a question.

## Attempts
### POST /api/attempts
Submits a quiz attempt.

### GET /api/attempts
Gets attempts.

### GET /api/attempts/{id}
Gets an attempt by ID.

### GET /api/attempts/quiz/{quizId}
Gets attempts for a quiz.
