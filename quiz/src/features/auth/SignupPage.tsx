import { useState } from 'react'
import { Link as RouterLink, useNavigate } from 'react-router-dom'
import { useForm } from 'react-hook-form'
import Box from '@mui/material/Box'
import Button from '@mui/material/Button'
import TextField from '@mui/material/TextField'
import Typography from '@mui/material/Typography'
import Alert from '@mui/material/Alert'
import Link from '@mui/material/Link'
import { register as registerUser } from '../../api/authApi'
import type { SignUpCredentials } from '../../api/authApi'

export function SignupPage() {
  const navigate = useNavigate()
  const [error, setError] = useState<string | null>(null)
  const { register, handleSubmit } = useForm<SignUpCredentials>()

  const onSubmit = async (values: SignUpCredentials) => {
    setError(null)

    try {
      await registerUser(values)
      navigate('/login')
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
        Employee sign up
      </Typography>

      {error && (
        <Alert severity="error" sx={{ mb: 2 }}>
          {error}
        </Alert>
      )}

      <TextField
        label="Name"
        fullWidth
        margin="normal"
        {...register('name', { required: 'Name is required' })}
      />

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

      <TextField
        label="Gender"
        fullWidth
        margin="normal"
        {...register('gender')}
      />

      <TextField
        label="Phone number"
        fullWidth
        margin="normal"
        {...register('phoneNumber')}
      />

      <Button type="submit" variant="contained" fullWidth sx={{ mt: 2 }}>
        Create account
      </Button>

      <Typography variant="body2" sx={{ mt: 2, textAlign: 'center' }}>
        Already have an account?{' '}
        <Link component={RouterLink} to="/login">
          Sign in
        </Link>
      </Typography>
    </Box>
  )
}
