import type { User } from '../types/user.types'
import { getMockUserByEmail, getMockUserById } from '../data/mockData'

export interface LoginCredentials {
  email: string
  password: string
}

export async function login({ email }: LoginCredentials): Promise<User> {
  const user = getMockUserByEmail(email)

  if (!user) {
    throw new Error('Invalid email or password')
  }

  return user
}

export async function fetchUserById(id: string): Promise<User | undefined> {
  return getMockUserById(id)
}
