import { createContext, useEffect, useMemo, useState } from 'react'
import type { User } from '../../types/user.types'
import type { LoginCredentials } from '../../api/authApi'
import { login as authLogin, normalizeUserRole, normalizeUserRoles } from '../../api/authApi'

interface AuthContextValue {
  user: User | null
  login: (credentials: LoginCredentials) => Promise<User>
  logout: () => void
  switchRole: (role: User['activeRole']) => void
  updateUser: (nextUser: User) => void
}

const storedUser = typeof window !== 'undefined' ? window.localStorage.getItem('quiz-app-user') : null

function normalizeStoredUser(user: User | null): User | null {
  if (!user) {
    return null
  }

  return {
    ...user,
    roles: normalizeUserRoles(user.roles),
    activeRole: normalizeUserRole(user.activeRole),
  }
}

export const AuthContext = createContext<AuthContextValue | null>(null)

export function AuthProvider({ children }: { children: React.ReactNode }) {
  const [user, setUser] = useState<User | null>(storedUser ? normalizeStoredUser(JSON.parse(storedUser)) : null)

  useEffect(() => {
    if (user) {
      window.localStorage.setItem('quiz-app-user', JSON.stringify(user))
    } else {
      window.localStorage.removeItem('quiz-app-user')
    }
  }, [user])

  const value = useMemo(
    () => ({
      user,
      login: async (credentials: LoginCredentials) => {
        const nextUser = await authLogin(credentials)
        setUser(nextUser)
        return nextUser
      },
      logout: () => {
        setUser(null)
      },
      switchRole: (role: User['activeRole']) => {
        setUser((current) => {
          if (!current) {
            return current
          }

          const normalizedRole = normalizeUserRole(role)
          return current.roles.includes(normalizedRole)
            ? { ...current, activeRole: normalizedRole }
            : current
        })
      },
      updateUser: (nextUser: User) => {
        setUser(nextUser)
      },
    }),
    [user],
  )

  return <AuthContext.Provider value={value}>{children}</AuthContext.Provider>
}
