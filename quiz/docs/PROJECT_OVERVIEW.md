# Quiz App Project Overview

## Purpose
This repository contains a mock-first quiz application built with React, TypeScript, Vite, and Material UI. It is designed to support:

- role-based authentication for `admin` and `employee`
- admin quiz creation and editing
- employee quiz selection and attempt flow
- protected routes and UI navigation
- local mock data for quizzes, users, and attempts

## Tech Stack

- React 19
- TypeScript 6
- Vite
- MUI v9
- React Router DOM v7
- React Hook Form

## Key Concepts

- `mockData.ts`: in-memory quiz/user/attempt storage used by the app API layer
- `AuthContext`: keeps session state in React Context and localStorage
- `ProtectedRoute`: guards route access by authentication and role
- `AppRoutes`: defines admin and employee route trees

## Folder Layout

- `src/api/` - mock API layer for auth, quizzes, and attempts
- `src/components/` - shared UI components and layout wrappers
- `src/features/auth/` - login flow and auth context
- `src/features/admin/` - admin quiz and results pages
- `src/features/employee/` - employee quiz and attempt experience
- `src/types/` - shared TypeScript models for users, quizzes, and attempts
- `src/data/` - sample data and mock state helpers
- `src/routes/` - route definitions and application routing

## Current App Flow

1. User opens `/login`
2. User picks a role and signs in
3. Role-based redirect sends employees to `/quizzes` and admins to `/admin/quizzes`
4. Employees can start quizzes and view results
5. Admins can manage quizzes and eventually review results
