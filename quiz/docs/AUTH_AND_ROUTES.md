# Authentication and Routes

## User Roles

This app supports two roles:

- `admin`
- `employee`

A user can hold multiple roles at once. In that case, the `user` object contains:

- `roles`: an array of available roles
- `activeRole`: the currently selected role

Each role has a separate route experience and UI navigation based on `activeRole`.

## Authentication Flow

- The login screen uses a role toggle to select the active role for this session.
- The mock auth API resolves a user by email from `src/data/mockData.ts`.
- Auth state is stored in React Context and persisted to `localStorage`.
- On login, the app redirects based on the selected `activeRole`.
- If a user has multiple roles, the navbar displays the current role and allows switching roles from a vertical list.

## Auth Components

- `src/features/auth/AuthContext.tsx`
  - creates `AuthContext`
  - stores `user`
  - persists session to `localStorage`
- `src/features/auth/useAuth.ts`
  - custom hook for consuming auth context
- `src/features/auth/LoginPage.tsx`
  - role toggle
  - form with email/password
  - role-based default email

## Protected Routing

- `src/components/ProtectedRoute.tsx`
- Guard logic:
  - if user is not signed in, redirect to `/login`
  - if `allowedRoles` is defined and the current `activeRole` is not allowed, redirect to `/`

## Route Structure

- `/login` - login page for all users
- `/` - root redirects to role-based home

### Admin Routes

- `/admin/quizzes`
- `/admin/quizzes/new`
- `/admin/quizzes/:id/edit`
- `/admin/quizzes/:id/results`

### Employee Routes

- `/quizzes`
- `/quizzes/:id/attempt`
- `/quizzes/:id/result/:attemptId`
- `/my-results`

## Navigation

- `src/components/layout/Navbar.tsx`
- Admin nav includes admin quiz management links
- Employee nav includes quiz list and personal results
