import { HttpErrorResponse } from '@angular/common/http';

export function extractErrorMessage(err: HttpErrorResponse, fallback: string): string {
  const validationErrors = err.error?.errors;
  if (validationErrors) {
    const firstError = Object.values(validationErrors)[0] as string[];
    return firstError[0];
  }
  return err.error?.detail ?? fallback;
}