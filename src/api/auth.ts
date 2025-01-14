import api from './axios';
import { LoginData, RegisterData, AuthResponse } from '../types';

export const authApi = {
  login: (data: LoginData) => 
    api.post<AuthResponse>('/auth/login', data),
  
  register: (data: RegisterData) => 
    api.post<AuthResponse>('/auth/register', data),
  
  logout: () => 
    api.post('/auth/logout')
}; 