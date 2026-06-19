import { useMemo, useState } from 'react'
import { Link as RouterLink, useNavigate } from 'react-router-dom'
import AppBar from '@mui/material/AppBar'
import Button from '@mui/material/Button'
import Menu from '@mui/material/Menu'
import MenuItem from '@mui/material/MenuItem'
import Stack from '@mui/material/Stack'
import Toolbar from '@mui/material/Toolbar'
import Typography from '@mui/material/Typography'
import { useAuth } from '../../features/auth/useAuth'
import type { UserRole } from '../../types/user.types'

export function Navbar() {
  const { user, logout, switchRole } = useAuth()
  const navigate = useNavigate()
  const [roleAnchor, setRoleAnchor] = useState<HTMLElement | null>(null)

  const navItems = useMemo(
    () => {
      if (!user) {
        return []
      }

      if (user.activeRole === 'admin') {
        return [
          { label: 'Quizzes', to: '/admin/quizzes' },
        ]
      }

      return [
        { label: 'Quizzes', to: '/quizzes' },
        { label: 'My Results', to: '/my-results' },
      ]
    },
    [user],
  )

  const handleRoleMenuOpen = (event: React.MouseEvent<HTMLButtonElement>) => {
    setRoleAnchor(event.currentTarget)
  }

  const handleRoleMenuClose = () => {
    setRoleAnchor(null)
  }

  const handleRoleSelect = (role: UserRole) => {
    if (user?.roles.includes(role)) {
      switchRole(role)
      navigate(role === 'admin' ? '/admin/quizzes' : '/quizzes')
    }
    handleRoleMenuClose()
  }

  return (
    <AppBar position="static" color="primary" enableColorOnDark>
      <Toolbar sx={{ justifyContent: 'space-between' }}>
        <Stack direction="row" spacing={2} sx={{ alignItems: 'center' }}>
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

        <Stack direction="row" spacing={2} sx={{ alignItems: 'center' }}>
          {user ? (
            <>
              {user.roles.length > 1 ? (
                <>
                  <Button
                    color="inherit"
                    size="small"
                    onClick={handleRoleMenuOpen}
                    sx={{ textTransform: 'none' }}
                  >
                    Role: {user.activeRole === 'admin' ? 'Admin' : 'Employee'}
                  </Button>
                  <Menu
                    anchorEl={roleAnchor}
                    open={Boolean(roleAnchor)}
                    onClose={handleRoleMenuClose}
                    anchorOrigin={{ vertical: 'bottom', horizontal: 'right' }}
                    transformOrigin={{ vertical: 'top', horizontal: 'right' }}
                  >
                    {user.roles.map((role) => (
                      <MenuItem
                        key={role}
                        selected={role === user.activeRole}
                        onClick={() => handleRoleSelect(role)}
                      >
                        {role === 'admin' ? 'Admin' : 'Employee'}
                      </MenuItem>
                    ))}
                  </Menu>
                </>
              ) : (
                <Typography sx={{ color: 'inherit' }}>
                  {user.name} ({user.activeRole})
                </Typography>
              )}

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
