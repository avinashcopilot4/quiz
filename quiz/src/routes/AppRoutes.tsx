import { BrowserRouter, Navigate, Route, Routes } from 'react-router-dom'
import Box from '@mui/material/Box'
import Container from '@mui/material/Container'
import { LoginPage } from '../features/auth/LoginPage'
import { ProtectedRoute } from '../components/ProtectedRoute'
import { QuizListPage as EmployeeQuizListPage } from '../features/employee/quizzes/QuizListPage'
import { AttemptPage } from '../features/employee/attempt/AttemptPage'
import { MyResultsPage } from '../features/employee/results/MyResultsPage'
import { ResultDetail } from '../features/employee/results/ResultDetail'
import { QuizListPage as AdminQuizListPage } from '../features/admin/quizzes/QuizListPage'
import { AdminResultsPage } from '../features/admin/results/AdminResultsPage'
import type { UserRole } from '../types/user.types'

function Layout({ children }: { children: React.ReactNode }) {
  return (
    <Container maxWidth="lg" sx={{ py: 4 }}>
      <Box sx={{ mb: 4 }}>{children}</Box>
    </Container>
  )
}

export function AppRoutes() {
  return (
    <BrowserRouter>
      <Routes>
        <Route path="/login" element={<LoginPage />} />

        <Route
          path="/admin/*"
          element={
            <ProtectedRoute allowedRoles={['admin'] as UserRole[]}>
              <Layout>
                <Routes>
                  <Route index element={<AdminQuizListPage />} />
                  <Route path="quizzes" element={<AdminQuizListPage />} />
                  <Route path="quizzes/new" element={<div>Admin quiz creation page</div>} />
                  <Route path="quizzes/:id/edit" element={<div>Edit quiz page</div>} />
                  <Route path="quizzes/:id/results" element={<AdminResultsPage />} />
                </Routes>
              </Layout>
            </ProtectedRoute>
          }
        />

        <Route
          path="/quizzes/*"
          element={
            <ProtectedRoute allowedRoles={['employee'] as UserRole[]}>
              <Layout>
                <Routes>
                  <Route index element={<EmployeeQuizListPage />} />
                  <Route path=":id/attempt" element={<AttemptPage />} />
                  <Route path=":id/result/:attemptId" element={<ResultDetail />} />
                </Routes>
              </Layout>
            </ProtectedRoute>
          }
        />

        <Route
          path="/my-results"
          element={
            <ProtectedRoute allowedRoles={['employee'] as UserRole[]}>
              <Layout>
                <MyResultsPage />
              </Layout>
            </ProtectedRoute>
          }
        />

        <Route path="/" element={<Navigate to="/quizzes" replace />} />
        <Route path="*" element={<Navigate to="/" replace />} />
      </Routes>
    </BrowserRouter>
  )
}
