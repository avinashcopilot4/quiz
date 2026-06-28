# Database Schema Notes

## Core Tables
- Quizzes
- Questions
- Users
- UserRoles
- QuizAttempts
- AttemptDetails
- AuditLog

## Important Schema Changes
- Users now support Gender and PhoneNumber fields.
- UserRoles is used for multi-role support (admin and employee).
- Quiz entities include metadata such as Topic, Language, Difficulty, TimeLimit, PassingScore, and Tags.
- QuizAttempts and AttemptDetails store attempt results and per-question answers.

## Notes
The original creation script is available in [QuizDb.txt](../../QuizDb.txt). The migration script for later schema changes is available in [update_db_schema.sql](../../update_db_schema.sql).
