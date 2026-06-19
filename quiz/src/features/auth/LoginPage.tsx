import { useEffect, useState } from 'react'
import { useNavigate } from 'react-router-dom'
import { useForm } from 'react-hook-form'
import Box from '@mui/material/Box'
import Button from '@mui/material/Button'
import TextField from '@mui/material/TextField'
import ToggleButton from '@mui/material/ToggleButton'
import ToggleButtonGroup from '@mui/material/ToggleButtonGroup'
import Typography from '@mui/material/Typography'
import Alert from '@mui/material/Alert'
import type { LoginCredentials } from '../../api/authApi'
import type { UserRole } from '../../types/user.types'
import { useAuth } from './useAuth'

interface LoginFormValues extends LoginCredentials {}

const defaultRole: UserRole = 'employee'
const roleEmailLookup: Record<UserRole, string> = {
  admin: 'admin@example.com',
  employee: 'employee@example.com',
}

export function LoginPage() {
  const navigate = useNavigate()
  const { login } = useAuth()
  const [error, setError] = useState<string | null>(null)
  const [selectedRole, setSelectedRole] = useState<UserRole>(defaultRole)
  const { register, handleSubmit, setValue } = useForm<LoginFormValues>({
    defaultValues: { email: roleEmailLookup[defaultRole], password: '' },
  })

  useEffect(() => {
    setValue('email', roleEmailLookup[selectedRole])
  }, [selectedRole, setValue])

  const onSubmit = async (values: LoginFormValues) => {
    setError(null)

    try {
      const user = await login({ ...values, role: selectedRole })
      navigate(user.activeRole === 'admin' ? '/admin/quizzes' : '/quizzes')
    } catch (err) {
      setError((err as Error).message)
    }
  }

  return (
    <Box
      component="form"
      onSubmit={handleSubmit(onSubmit)}
      sx={{ maxWidth: 420, mx: 'auto', mt: 8, px: 2 }}
    >
      <Typography variant="h4" gutterBottom>
        Sign in
      </Typography>

      {error && (
        <Alert severity="error" sx={{ mb: 2 }}>
          {error}
        </Alert>
      )}

      <ToggleButtonGroup
        value={selectedRole}
        exclusive
        onChange={(_, value) => {
          if (value) {
            setSelectedRole(value)
          }
        }}
        aria-label="Select role"
        sx={{ mb: 2 }}
      >
        <ToggleButton value="employee">Employee</ToggleButton>
        <ToggleButton value="admin">Admin</ToggleButton>
      </ToggleButtonGroup>

      <TextField
        label="Email"
        type="email"
        fullWidth
        margin="normal"
        {...register('email', { required: 'Email is required' })}
      />

      <TextField
        label="Password"
        type="password"
        fullWidth
        margin="normal"
        {...register('password', { required: 'Password is required' })}
      />

      <Button type="submit" variant="contained" fullWidth sx={{ mt: 2 }}>
        Continue
      </Button>
    </Box>
  )
}
