import api from './axios';
import { Budget } from '../types';

export const budgetApi = {
  getAll: () => 
    api.get<Budget[]>('/budgets'),
  
  getById: (id: number) => 
    api.get<Budget>(`/budgets/${id}`),
  
  create: (data: any) => 
    api.post<Budget>('/budgets', data),
  
  update: (id: number, data: any) => 
    api.put<Budget>(`/budgets/${id}`, data),
  
  delete: (id: number) => 
    api.delete(`/budgets/${id}`),
  
  getSummary: (date: string) => 
    api.get('/budgets/summary', { params: { date } })
}; 