import api from './axios';
import { Transaction, CreateTransaction, TransactionSummary } from '../types';

export const transactionApi = {
  getAll: () => 
    api.get<Transaction[]>('/transactions'),
  
  getById: (id: number) => 
    api.get<Transaction>(`/transactions/${id}`),
  
  create: (data: CreateTransaction) => 
    api.post<Transaction>('/transactions', data),
  
  update: (id: number, data: Partial<Transaction>) => 
    api.put<Transaction>(`/transactions/${id}`, data),
  
  delete: (id: number) => 
    api.delete(`/transactions/${id}`),
  
  getSummary: (startDate: string, endDate: string) => 
    api.get<TransactionSummary>('/transactions/summary', {
      params: { startDate, endDate }
    })
}; 