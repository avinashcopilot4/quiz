import type { User, UserRole } from '../types/user.types'
import { apiClient } from './client'

export interface LoginCredentials {
  email: string
  password: string
  role?: UserRole
}

export interface SignUpCredentials {
  name: string
  email: string
  password: string
  gender?: string
  phoneNumber?: string
}

export interface UserProfileUpdate {
  name: string
  gender?: string
  phoneNumber?: string
}

interface UserResponse {
  id: string
  name: string
  email: string
  roles: string[]
  activeRole: string
  gender?: string
  phoneNumber?: string
}

function mapUserResponse(user: UserResponse): User {
  return {
    id: user.id,
    name: user.name,
    email: user.email,
    roles: normalizeUserRoles(user.roles),
    activeRole: normalizeUserRole(user.activeRole),
    gender: user.gender,
    phoneNumber: user.phoneNumber,
  }
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
  const user = await apiClient.post<UserResponse>('/users/login', { email, password, role })
  return mapUserResponse(user)
}

export async function register({ name, email, password, gender, phoneNumber }: SignUpCredentials): Promise<void> {
  await apiClient.post('/users', {
    userName: name,
    email,
    password,
    gender,
    phoneNumber,
    isActive: true,
  })
}

export async function fetchUserById(id: string): Promise<User | undefined> {
  const user = await apiClient.get<UserResponse>(`/users/${id}`)
  if (!user) {
    return undefined
  }

  return mapUserResponse(user)
}

export async function updateUserProfile(id: string, profile: UserProfileUpdate): Promise<User> {
  const user = await apiClient.put<UserResponse>(`/users/${id}/profile`, profile)
  return mapUserResponse(user)
}
