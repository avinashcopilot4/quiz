import { useMemo } from 'react'
import { Link as RouterLink, useNavigate } from 'react-router-dom'
import AppBar from '@mui/material/AppBar'
import Box from '@mui/material/Box'
import Button from '@mui/material/Button'
import Stack from '@mui/material/Stack'
import Toolbar from '@mui/material/Toolbar'
import Typography from '@mui/material/Typography'
import { useAuth } from '../../features/auth/useAuth'

export function Navbar() {
  const { user, logout } = useAuth()
  const navigate = useNavigate()

  const navItems = useMemo(
    () => {
      if (!user) {
        return []
      }

      if (user.role === 'admin') {
        return [
          { label: 'Quizzes', to: '/admin/quizzes' },
          { label: 'Results', to: '/admin/quizzes/quiz-001/results' },
        ]
      }

      return [
        { label: 'Quizzes', to: '/quizzes' },
        { label: 'My Results', to: '/my-results' },
      ]
    },
    [user],
  )

  return (
    <AppBar position="static" color="primary" enableColorOnDark>
      <Toolbar sx={{ justifyContent: 'space-between' }}>
        <Stack direction="row" alignItems="center" spacing={2}>
          <Typography variant="h6" component={RouterLink} to="/" sx={{ color: 'inherit', textDecoration: 'none' }}>
            Quiz App
          </Typography>

          {navItems.map((item) => (
            <Button
              key={item.to}
              component={RouterLink}
              to={item.to}
              color="inherit"
              size="small"
            >
              {item.label}
            </Button>
          ))}
        </Stack>

        <Stack direction="row" alignItems="center" spacing={2}>
          {user ? (
            <>
              <Typography sx={{ color: 'inherit' }}>
                {user.name} ({user.role})
              </Typography>
              <Button
                color="inherit"
                size="small"
                onClick={() => {
                  logout()
                  navigate('/login')
                }}
              >
                Logout
              </Button>
            </>
          ) : (
            <Button component={RouterLink} to="/login" color="inherit" size="small">
              Login
            </Button>
          )}
        </Stack>
      </Toolbar>
    </AppBar>
  )
}
