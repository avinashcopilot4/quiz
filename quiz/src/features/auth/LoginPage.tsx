import { useState } from 'react'
import { useNavigate } from 'react-router-dom'
import { useForm } from 'react-hook-form'
import Box from '@mui/material/Box'
import Button from '@mui/material/Button'
import TextField from '@mui/material/TextField'
import Typography from '@mui/material/Typography'
import Alert from '@mui/material/Alert'
import type { LoginCredentials } from '../../api/authApi'
import { useAuth } from './useAuth'

interface LoginFormValues extends LoginCredentials {}

export function LoginPage() {
  const navigate = useNavigate()
  const { login } = useAuth()
  const [error, setError] = useState<string | null>(null)
  const { register, handleSubmit, formState } = useForm<LoginFormValues>({
    defaultValues: { email: 'employee@example.com', password: '' },
  })

  const onSubmit = async (values: LoginFormValues) => {
    setError(null)

    try {
      await login(values)
      navigate('/quizzes')
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
