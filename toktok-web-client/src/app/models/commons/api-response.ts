import { Metadata } from './metadata';
export interface BaseResponse<T> {
  isSuccess: boolean;
  data: T;
  errorMessage: string;
  metadata: Metadata;
}
