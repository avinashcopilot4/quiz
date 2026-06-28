import { BrowserRouter, Navigate, Route, Routes } from 'react-router-dom'
import { AppShell } from '../components/layout/AppShell'
import { LoginPage } from '../features/auth/LoginPage'
import { SignupPage } from '../features/auth/SignupPage'
import { ProtectedRoute } from '../components/ProtectedRoute'
import { QuizListPage as EmployeeQuizListPage } from '../features/employee/quizzes/QuizListPage'
import { AttemptPage } from '../features/employee/attempt/AttemptPage'
import { MyResultsPage } from '../features/employee/results/MyResultsPage'
import { ResultDetail } from '../features/employee/results/ResultDetail'
import { QuizListPage as AdminQuizListPage } from '../features/admin/quizzes/QuizListPage'
import { QuizEditorPage } from '../features/admin/quizzes/QuizEditorPage'
import { AdminResultsPage } from '../features/admin/results/AdminResultsPage'
import { AdminProfilePage } from '../features/admin/profile/AdminProfilePage'
import type { UserRole } from '../types/user.types'
import { useAuth } from '../features/auth/useAuth'

function HomeRedirect() {
  const { user } = useAuth()

  if (!user) {
    return <Navigate to="/login" replace />
  }

  return <Navigate to={user.activeRole === 'admin' ? '/admin/quizzes' : '/quizzes'} replace />
}

function Layout({ children }: { children: React.ReactNode }) {
  return <AppShell>{children}</AppShell>
}

export function AppRoutes() {
  return (
    <BrowserRouter>
      <Routes>
        <Route path="/login" element={<LoginPage />} />
        <Route path="/signup" element={<SignupPage />} />

        <Route
          path="/admin/*"
          element={
            <ProtectedRoute allowedRoles={['admin'] as UserRole[]}>
              <Layout>
                <Routes>
                  <Route index element={<AdminQuizListPage />} />
                  <Route path="quizzes" element={<AdminQuizListPage />} />
                  <Route path="quizzes/new" element={<QuizEditorPage />} />
                  <Route path="quizzes/:id/edit" element={<QuizEditorPage />} />
                  <Route path="quizzes/:id/results" element={<AdminResultsPage />} />
                  <Route path="profile" element={<AdminProfilePage />} />
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

        <Route path="/" element={<HomeRedirect />} />
        <Route path="*" element={<Navigate to="/" replace />} />
      </Routes>
    </BrowserRouter>
  )
}
