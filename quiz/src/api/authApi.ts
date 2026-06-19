import type { User, UserRole } from '../types/user.types'
import { getMockUserByEmail, getMockUserById } from '../data/mockData'

export interface LoginCredentials {
  email: string
  password: string
  role?: UserRole
}

export async function login({ email, role }: LoginCredentials): Promise<User> {
  const user = getMockUserByEmail(email)

  if (!user) {
    throw new Error('Invalid email or password')
  }

  return {
    ...user,
    activeRole: role && user.roles.includes(role) ? role : user.activeRole,
  }
}

export async function fetchUserById(id: string): Promise<User | undefined> {
  return getMockUserById(id)
}
