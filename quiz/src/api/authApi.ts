import type { User, UserRole } from '../types/user.types'
import { apiClient } from './client'

export interface LoginCredentials {
  email: string
  password: string
  role?: UserRole
}

export function normalizeUserRole(role?: string | null): UserRole {
  if (!role) {
    return 'employee'
  }

  return role.trim().toLowerCase() === 'admin' ? 'admin' : 'employee'
}

export function normalizeUserRoles(roles?: Array<string | null | undefined>): UserRole[] {
  if (!roles?.length) {
    return ['employee']
  }

  return Array.from(new Set(roles.map((role) => normalizeUserRole(role))))
}

export async function login({ email, password, role }: LoginCredentials): Promise<User> {
  const user = await apiClient.post<{ id: string; name: string; email: string; roles: string[]; activeRole: string }>(
    '/users/login',
    { email, password, role },
  )

  return {
    id: user.id,
    name: user.name,
    email: user.email,
    roles: normalizeUserRoles(user.roles),
    activeRole: normalizeUserRole(user.activeRole),
  }
}

export async function fetchUserById(id: string): Promise<User | undefined> {
  const user = await apiClient.get<{ id: string; name: string; email: string; roles: string[]; activeRole: string }>(`/users/${id}`)
  if (!user) {
    return undefined
  }

  return {
    id: user.id,
    name: user.name,
    email: user.email,
    roles: normalizeUserRoles(user.roles),
    activeRole: normalizeUserRole(user.activeRole),
  }
}
