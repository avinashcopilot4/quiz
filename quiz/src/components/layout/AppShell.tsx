import Box from '@mui/material/Box'
import Container from '@mui/material/Container'
import { Navbar } from './Navbar'

export function AppShell({ children }: { children: React.ReactNode }) {
  return (
    <Box>
      <Navbar />
      <Container maxWidth="lg" sx={{ py: 4 }}>
        {children}
      </Container>
    </Box>
  )
}
