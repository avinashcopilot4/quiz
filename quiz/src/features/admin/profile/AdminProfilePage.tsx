import { useEffect, useState } from 'react'
import { useForm } from 'react-hook-form'
import { useNavigate } from 'react-router-dom'
import Box from '@mui/material/Box'
import Button from '@mui/material/Button'
import TextField from '@mui/material/TextField'
import Typography from '@mui/material/Typography'
import Alert from '@mui/material/Alert'
import Stack from '@mui/material/Stack'
import { useAuth } from '../../auth/useAuth'
import { updateUserProfile } from '../../../api/authApi'
import type { UserProfileUpdate } from '../../../api/authApi'

export function AdminProfilePage() {
  const { user, updateUser } = useAuth()
  const navigate = useNavigate()
  const [serverError, setServerError] = useState<string | null>(null)
  const [successMessage, setSuccessMessage] = useState<string | null>(null)
  const { register, handleSubmit, reset } = useForm<UserProfileUpdate>()

  useEffect(() => {
    if (user) {
      reset({
        name: user.name,
        gender: user.gender ?? '',
        phoneNumber: user.phoneNumber ?? '',
      })
    }
  }, [user, reset])

  const onSubmit = async (values: UserProfileUpdate) => {
    if (!user) {
      return
    }

    setServerError(null)
    setSuccessMessage(null)

    try {
      const updated = await updateUserProfile(user.id, values)
      updateUser({
        ...updated,
        roles: user.roles,
        activeRole: user.activeRole,
      })
      setSuccessMessage('Profile saved successfully.')
    } catch (error) {
      setServerError((error as Error).message)
    }
  }

  if (!user) {
    return <Typography>Loading user profile...</Typography>
  }

  return (
    <Box sx={{ maxWidth: 560, mx: 'auto', mt: 6, px: 2 }}>
      <Stack spacing={2}>
        <Typography variant="h4">Admin Profile</Typography>
        <Typography variant="body2" color="text.secondary">
          Update your profile information. Your email address cannot be changed here.
        </Typography>

        {serverError && <Alert severity="error">{serverError}</Alert>}
        {successMessage && <Alert severity="success">{successMessage}</Alert>}

        <TextField
          label="Name"
          fullWidth
          {...register('name', { required: 'Name is required' })}
        />

        <TextField
          label="Email"
          fullWidth
          value={user.email}
          disabled
        />

        <TextField
          label="Gender"
          fullWidth
          {...register('gender')}
        />

        <TextField
          label="Phone Number"
          fullWidth
          {...register('phoneNumber')}
        />

        <Stack direction="row" spacing={2}>
          <Button variant="contained" onClick={handleSubmit(onSubmit)}>
            Save profile
          </Button>
          <Button variant="outlined" onClick={() => navigate(user.activeRole === 'admin' ? '/admin/quizzes' : '/quizzes')}>
            Back
          </Button>
        </Stack>
      </Stack>
    </Box>
  )
}
