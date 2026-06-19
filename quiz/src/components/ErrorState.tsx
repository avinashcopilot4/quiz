export function ErrorState({ message }: { message?: string }) {
  return <div>{message ?? 'Something went wrong.'}</div>;
}
