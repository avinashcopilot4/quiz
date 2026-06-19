import type { User } from '../types/user.types'
import { getMockUserByEmail } from '../data/mockData'

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
