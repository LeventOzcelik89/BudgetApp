import api from './axios';
import { LoginData, RegisterData, AuthResponse } from '../types';

export const authApi = {
  login: (data: LoginData) => 
    api.post<AuthResponse>('/Auth/login', data),
  
  register: (data: RegisterData) => 
    api.post<AuthResponse>('/Auth/register', data),
  
  verifyToken: () => 
    api.get<AuthResponse>('/Auth/verify'),
  
  logout: () => 
    api.post('/Auth/logout')
}; 