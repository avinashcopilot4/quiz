# Authentication and Routes

## User Roles

This app supports two roles:

- `admin`
- `employee`

Each role has a separate route experience and UI navigation.

## Authentication Flow

- The login screen uses a role toggle to load the correct email for the selected user role.
- The mock auth API resolves a user by email from `src/data/mockData.ts`.
- Auth state is stored in React Context and persisted to `localStorage`.
- On login, the app redirects based on the resolved user role.

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
  - if `allowedRoles` is defined and the current user role is not allowed, redirect to `/login`

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
