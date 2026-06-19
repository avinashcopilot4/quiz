import sampleQuizData from './sample-questions.json'
import type { QuizModel } from '../types/quiz.types'
import type { User } from '../types/user.types'
import type { Attempt } from '../types/attempt.types'

const initialSampleQuiz = sampleQuizData as QuizModel

export const mockUsers: User[] = [
  {
    id: 'admin-1',
    name: 'Admin User',
    email: 'admin@example.com',
    roles: ['admin', 'employee'],
    activeRole: 'admin',
  },
  {
    id: 'employee-1',
    name: 'Employee User',
    email: 'employee@example.com',
    roles: ['employee'],
    activeRole: 'employee',
  },
]

const QUIZZES_SESSION_KEY = 'quiz-app-quizzes'
const ATTEMPTS_SESSION_KEY = 'quiz-app-attempts'

const defaultMockQuizzes: QuizModel[] = [
  initialSampleQuiz,
  {
    id: 'quiz-002',
    title: 'JavaScript Fundamentals',
    description: 'A quiz covering core JavaScript concepts and syntax.',
    topic: 'JavaScript',
    language: 'English',
    difficulty: 'Beginner',
    tags: ['javascript', 'syntax', 'basics'],
    createdBy: 'admin',
    author: 'Admin User',
    createdAt: '2026-06-18T08:30:00.000Z',
    updatedAt: '2026-06-18T08:30:00.000Z',
    questionCount: 4,
    timeLimit: 8,
    passingScore: 70,
    status: 'published',
    questions: [
      {
        id: 'q2-1',
        text: 'What is the result of `typeof null` in JavaScript?',
        options: ['object', 'null', 'undefined', 'number'],
        correctOptionIndex: 0,
      },
      {
        id: 'q2-2',
        text: 'Which keyword creates a block-scoped variable?',
        options: ['var', 'let', 'const', 'define'],
        correctOptionIndex: 1,
      },
      {
        id: 'q2-3',
        text: 'Which method converts JSON to an object?',
        options: ['JSON.stringify', 'JSON.parse', 'Object.fromJSON', 'JSON.object'],
        correctOptionIndex: 1,
      },
      {
        id: 'q2-4',
        text: 'What does `===` compare in JavaScript?',
        options: ['Value only', 'Type only', 'Value and type', 'Reference only'],
        correctOptionIndex: 2,
      },
    ],
  },
  {
    id: 'quiz-003',
    title: 'TypeScript Basics',
    description: 'Test your understanding of TypeScript types and tooling.',
    topic: 'TypeScript',
    language: 'English',
    difficulty: 'Intermediate',
    tags: ['typescript', 'types', 'static typing'],
    createdBy: 'admin',
    author: 'Admin User',
    createdAt: '2026-06-17T10:45:00.000Z',
    updatedAt: '2026-06-17T10:45:00.000Z',
    questionCount: 4,
    timeLimit: 12,
    passingScore: 75,
    status: 'published',
    questions: [
      {
        id: 'q3-1',
        text: 'What type does `Array<string>` represent?',
        options: ['Object', 'String', 'Array of strings', 'Tuple'],
        correctOptionIndex: 2,
      },
      {
        id: 'q3-2',
        text: 'Which keyword defines a contract for object shapes?',
        options: ['type', 'interface', 'class', 'namespace'],
        correctOptionIndex: 1,
      },
      {
        id: 'q3-3',
        text: 'How do you make a property optional?',
        options: ['prop?', 'optional prop', 'prop|undefined', 'prop!'],
        correctOptionIndex: 0,
      },
      {
        id: 'q3-4',
        text: 'What does `unknown` represent?',
        options: ['Any value', 'Values that are not known yet', 'Safe alternative to any', 'A string value'],
        correctOptionIndex: 2,
      },
    ],
  },
  {
    id: 'quiz-004',
    title: 'HTML & CSS Essentials',
    description: 'A review of semantic HTML and stylesheet fundamentals.',
    topic: 'Web Design',
    language: 'English',
    difficulty: 'Beginner',
    tags: ['html', 'css', 'web'],
    createdBy: 'admin',
    author: 'Admin User',
    createdAt: '2026-06-16T09:15:00.000Z',
    updatedAt: '2026-06-16T09:15:00.000Z',
    questionCount: 3,
    timeLimit: 7,
    passingScore: 70,
    status: 'published',
    questions: [
      {
        id: 'q4-1',
        text: 'Which HTML tag is used for the largest heading?',
        options: ['<h1>', '<heading>', '<title>', '<header>'],
        correctOptionIndex: 0,
      },
      {
        id: 'q4-2',
        text: 'What CSS property changes text color?',
        options: ['font-color', 'text-color', 'color', 'fill'],
        correctOptionIndex: 2,
      },
      {
        id: 'q4-3',
        text: 'Which CSS display value makes an element a block?',
        options: ['inline', 'block', 'flex', 'grid'],
        correctOptionIndex: 1,
      },
    ],
  },
  {
    id: 'quiz-005',
    title: 'React Hooks Deep Dive',
    description: 'Questions about hooks, state, and lifecycle in React.',
    topic: 'React',
    language: 'English',
    difficulty: 'Intermediate',
    tags: ['react', 'hooks', 'state'],
    createdBy: 'admin',
    author: 'Admin User',
    createdAt: '2026-06-15T14:00:00.000Z',
    updatedAt: '2026-06-15T14:00:00.000Z',
    questionCount: 4,
    timeLimit: 15,
    passingScore: 75,
    status: 'published',
    questions: [
      {
        id: 'q5-1',
        text: 'Which hook is used for side effects in React?',
        options: ['useMemo', 'useEffect', 'useCallback', 'useState'],
        correctOptionIndex: 1,
      },
      {
        id: 'q5-2',
        text: 'What does `useState` return?',
        options: ['Two values: state and setter', 'A single state value', 'A function only', 'An object'],
        correctOptionIndex: 0,
      },
      {
        id: 'q5-3',
        text: 'Which hook memoizes a function?',
        options: ['useMemo', 'useCallback', 'useRef', 'useReducer'],
        correctOptionIndex: 1,
      },
      {
        id: 'q5-4',
        text: 'How do you preserve a value across renders?',
        options: ['useState', 'useRef', 'useMemo', 'useEffect'],
        correctOptionIndex: 1,
      },
    ],
  },
  {
    id: 'quiz-006',
    title: 'Node.js Backend Basics',
    description: 'Assessment of server-side JavaScript and Node runtime concepts.',
    topic: 'Node.js',
    language: 'English',
    difficulty: 'Intermediate',
    tags: ['node', 'backend', 'javascript'],
    createdBy: 'admin',
    author: 'Admin User',
    createdAt: '2026-06-14T11:20:00.000Z',
    updatedAt: '2026-06-14T11:20:00.000Z',
    questionCount: 4,
    timeLimit: 12,
    passingScore: 70,
    status: 'published',
    questions: [
      {
        id: 'q6-1',
        text: 'Which module is used to create an HTTP server in Node?',
        options: ['http', 'server', 'express', 'network'],
        correctOptionIndex: 0,
      },
      {
        id: 'q6-2',
        text: 'What does `npm` stand for?',
        options: ['Node Package Manager', 'Node Process Manager', 'New Product Model', 'Network Package Manager'],
        correctOptionIndex: 0,
      },
      {
        id: 'q6-3',
        text: 'Which value is returned by `fs.readFileSync`?',
        options: ['Callback', 'Promise', 'Buffer or string', 'Array'],
        correctOptionIndex: 2,
      },
      {
        id: 'q6-4',
        text: 'Which file is commonly used to declare Node project dependencies?',
        options: ['package.json', 'tsconfig.json', '.env', 'README.md'],
        correctOptionIndex: 0,
      },
    ],
  },
  {
    id: 'quiz-007',
    title: 'Database Fundamentals',
    description: 'Questions about SQL, data modeling, and query basics.',
    topic: 'Databases',
    language: 'English',
    difficulty: 'Beginner',
    tags: ['database', 'sql', 'data'],
    createdBy: 'admin',
    author: 'Admin User',
    createdAt: '2026-06-13T09:50:00.000Z',
    updatedAt: '2026-06-13T09:50:00.000Z',
    questionCount: 3,
    timeLimit: 10,
    passingScore: 70,
    status: 'published',
    questions: [
      {
        id: 'q7-1',
        text: 'What does SQL stand for?',
        options: ['Structured Query Language', 'Simple Query Language', 'Sequential Query Language', 'Standard Query Link'],
        correctOptionIndex: 0,
      },
      {
        id: 'q7-2',
        text: 'Which clause filters rows from a query result?',
        options: ['ORDER BY', 'GROUP BY', 'WHERE', 'HAVING'],
        correctOptionIndex: 2,
      },
      {
        id: 'q7-3',
        text: 'What is a primary key?',
        options: ['A unique row identifier', 'A column to sort by', 'A related table key', 'A text index'],
        correctOptionIndex: 0,
      },
    ],
  },
  {
    id: 'quiz-008',
    title: 'Git & Version Control',
    description: 'A quiz on Git commands, branches, and workflows.',
    topic: 'DevOps',
    language: 'English',
    difficulty: 'Beginner',
    tags: ['git', 'version control', 'workflow'],
    createdBy: 'admin',
    author: 'Admin User',
    createdAt: '2026-06-12T08:00:00.000Z',
    updatedAt: '2026-06-12T08:00:00.000Z',
    questionCount: 4,
    timeLimit: 12,
    passingScore: 70,
    status: 'published',
    questions: [
      {
        id: 'q8-1',
        text: 'Which command creates a new Git branch?',
        options: ['git new branch', 'git branch', 'git checkout', 'git init'],
        correctOptionIndex: 1,
      },
      {
        id: 'q8-2',
        text: 'How do you stage files for commit?',
        options: ['git stage', 'git add', 'git commit', 'git push'],
        correctOptionIndex: 1,
      },
      {
        id: 'q8-3',
        text: 'Which command shows the commit history?',
        options: ['git status', 'git log', 'git history', 'git show'],
        correctOptionIndex: 1,
      },
      {
        id: 'q8-4',
        text: 'What is a merge conflict?',
        options: ['A failed push', 'A branch error', 'Overlapping changes to the same file', 'A network issue'],
        correctOptionIndex: 2,
      },
    ],
  },
  {
    id: 'quiz-009',
    title: 'Testing JavaScript Apps',
    description: 'A quick quiz on writing tests and verifying app behavior.',
    topic: 'Testing',
    language: 'English',
    difficulty: 'Intermediate',
    tags: ['testing', 'javascript', 'qa'],
    createdBy: 'admin',
    author: 'Admin User',
    createdAt: '2026-06-11T13:20:00.000Z',
    updatedAt: '2026-06-11T13:20:00.000Z',
    questionCount: 3,
    timeLimit: 10,
    passingScore: 75,
    status: 'draft',
    questions: [
      {
        id: 'q9-1',
        text: 'Which library is commonly used for unit testing React components?',
        options: ['Jest', 'Chai', 'Enzyme', 'React Test Library'],
        correctOptionIndex: 3,
      },
      {
        id: 'q9-2',
        text: 'What does E2E testing validate?',
        options: ['Individual functions', 'User workflows across the app', 'CSS styles only', 'Network latency'],
        correctOptionIndex: 1,
      },
      {
        id: 'q9-3',
        text: 'What is a mock in testing?',
        options: ['A fake dependency', 'A real database', 'A build script', 'A lint rule'],
        correctOptionIndex: 0,
      },
    ],
  },
  {
    id: 'quiz-010',
    title: 'Web Performance Optimization',
    description: 'Questions about making web apps faster and more responsive.',
    topic: 'Performance',
    language: 'English',
    difficulty: 'Advanced',
    tags: ['performance', 'web', 'optimization'],
    createdBy: 'admin',
    author: 'Admin User',
    createdAt: '2026-06-10T15:10:00.000Z',
    updatedAt: '2026-06-10T15:10:00.000Z',
    questionCount: 4,
    timeLimit: 15,
    passingScore: 80,
    status: 'published',
    questions: [
      {
        id: 'q10-1',
        text: 'Which technique helps reduce initial page load time?',
        options: ['Bundling all files into one large asset', 'Lazy loading non-critical resources', 'Using inline styles everywhere', 'Disabling caching'],
        correctOptionIndex: 1,
      },
      {
        id: 'q10-2',
        text: 'What does the browser cache help with?',
        options: ['Server-side rendering', 'Slower updates', 'Faster repeat loads', 'Database queries'],
        correctOptionIndex: 2,
      },
      {
        id: 'q10-3',
        text: 'Which image format typically offers the best compression for the web?',
        options: ['BMP', 'PNG', 'GIF', 'WebP'],
        correctOptionIndex: 3,
      },
      {
        id: 'q10-4',
        text: 'What is one benefit of code splitting?',
        options: ['More CSS files', 'Larger initial download', 'Smaller initial bundle', 'Slower route transitions'],
        correctOptionIndex: 2,
      },
    ],
  },
]

export function getStoredQuizzes(): QuizModel[] {
  if (typeof window === 'undefined') {
    return [...defaultMockQuizzes]
  }

  const stored = window.sessionStorage.getItem(QUIZZES_SESSION_KEY)
  if (!stored) {
    window.sessionStorage.setItem(QUIZZES_SESSION_KEY, JSON.stringify(defaultMockQuizzes))
    return [...defaultMockQuizzes]
  }

  try {
    return JSON.parse(stored) as QuizModel[]
  } catch {
    window.sessionStorage.setItem(QUIZZES_SESSION_KEY, JSON.stringify(defaultMockQuizzes))
    return [...defaultMockQuizzes]
  }
}

function saveStoredQuizzes(quizzes: QuizModel[]) {
  if (typeof window === 'undefined') return
  window.sessionStorage.setItem(QUIZZES_SESSION_KEY, JSON.stringify(quizzes))
}

const defaultMockAttempts: Attempt[] = [
  {
    id: 'attempt-1',
    quizId: initialSampleQuiz.id,
    employeeId: mockUsers[1].id,
    answers: [
      { questionId: initialSampleQuiz.questions[0].id, selectedOptionIndex: 0 },
      { questionId: initialSampleQuiz.questions[1].id, selectedOptionIndex: 1 },
      { questionId: initialSampleQuiz.questions[2].id, selectedOptionIndex: 0 },
    ],
    score: 3,
    startedAt: new Date(Date.now() - 1000 * 60 * 10).toISOString(),
    submittedAt: new Date(Date.now() - 1000 * 60 * 5).toISOString(),
  },
]

function getStoredAttempts(): Attempt[] {
  if (typeof window === 'undefined') {
    return [...defaultMockAttempts]
  }

  const stored = window.sessionStorage.getItem(ATTEMPTS_SESSION_KEY)
  if (!stored) {
    window.sessionStorage.setItem(ATTEMPTS_SESSION_KEY, JSON.stringify(defaultMockAttempts))
    return [...defaultMockAttempts]
  }

  try {
    return JSON.parse(stored) as Attempt[]
  } catch {
    window.sessionStorage.setItem(ATTEMPTS_SESSION_KEY, JSON.stringify(defaultMockAttempts))
    return [...defaultMockAttempts]
  }
}

function saveStoredAttempts(attempts: Attempt[]) {
  if (typeof window === 'undefined') return
  window.sessionStorage.setItem(ATTEMPTS_SESSION_KEY, JSON.stringify(attempts))
}

export function getAttemptsByEmployee(employeeId: string): Attempt[] {
  return getStoredAttempts().filter((attempt) => attempt.employeeId === employeeId)
}

export function getAttemptsByQuiz(quizId: string): Attempt[] {
  return getStoredAttempts().filter((attempt) => attempt.quizId === quizId)
}

export function getMockAttemptById(id: string): Attempt | undefined {
  return getStoredAttempts().find((attempt) => attempt.id === id)
}

export function addMockAttempt(attempt: Attempt): void {
  const stored = getStoredAttempts()
  stored.push(attempt)
  saveStoredAttempts(stored)
}

export function getMockUserByEmail(email: string): User | undefined {
  return mockUsers.find((user) => user.email.toLowerCase() === email.toLowerCase())
}

export function getMockUserById(id: string): User | undefined {
  return mockUsers.find((user) => user.id === id)
}

export function getMockQuizById(id: string): QuizModel | undefined {
  return getStoredQuizzes().find((quiz) => quiz.id === id)
}

export function addMockQuiz(quiz: QuizModel): void {
  const quizzes = getStoredQuizzes()
  quizzes.push(quiz)
  saveStoredQuizzes(quizzes)
}

export function updateMockQuiz(quiz: QuizModel): void {
  const quizzes = getStoredQuizzes()
  const index = quizzes.findIndex((item) => item.id === quiz.id)
  if (index >= 0) {
    quizzes[index] = quiz
    saveStoredQuizzes(quizzes)
  }
}

export function deleteMockQuiz(id: string): void {
  const quizzes = getStoredQuizzes().filter((quiz) => quiz.id !== id)
  saveStoredQuizzes(quizzes)
}
