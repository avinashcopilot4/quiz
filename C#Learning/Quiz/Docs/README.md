# Backend Documentation

## Overview
This backend is an ASP.NET Core Web API for the quiz application. It exposes endpoints for quizzes, questions, users, attempts, and audit logging.

## Project Structure
- Controllers: API endpoints for quizzes, attempts, users, and questions.
- BusinessCore: service layer for business rules.
- Repositories: data access layer with EF Core.
- Entity: EF Core models and DbContext.
- Common: shared DTOs.

## Main Features
- Quiz CRUD and question management
- Employee quiz attempts and scoring
- Admin results review
- User authentication and role-based access
- Profile view/edit support for users

## Running the Backend
From the project folder:

```bash
dotnet build
dotnet run
```

The API will run on the configured local ports from launch settings.
