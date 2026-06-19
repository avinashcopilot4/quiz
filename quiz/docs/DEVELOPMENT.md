# Development Guide

## Setup

Install dependencies with:

```bash
npm install
```

## Run

Start the development server:

```bash
npm run dev
```

Open the app at the local Vite URL shown in terminal.

## Build

```bash
npm run build
```

## Useful Scripts

- `npm run dev` — start Vite dev server
- `npm run build` — compile the project with TypeScript and bundle with Vite
- `npm run lint` — run ESLint across the repo

## Notes

- MUI v9 uses `sx` styles and `slotProps` for advanced input prop control.
- TypeScript is configured with `jsx: react-jsx`, so React imports are not required in TSX files.
- The app uses mock APIs and local in-memory data, so no backend is required for local development.
