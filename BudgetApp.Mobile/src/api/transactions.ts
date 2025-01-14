import axios from 'axios';
import { Transaction } from '../types';

const BASE_URL = 'http://localhost:5000/api';

export const transactionsApi = {
  getAll: async () => {
    const response = await axios.get<Transaction[]>(`${BASE_URL}/transactions`);
    return response.data;
  },

  getById: async (id: number) => {
    const response = await axios.get<Transaction>(`${BASE_URL}/transactions/${id}`);
    return response.data;
  },

  create: async (transaction: Omit<Transaction, 'id'>) => {
    const response = await axios.post<Transaction>(`${BASE_URL}/transactions`, transaction);
    return response.data;
  },

  update: async (id: number, transaction: Partial<Transaction>) => {
    const response = await axios.put<Transaction>(`${BASE_URL}/transactions/${id}`, transaction);
    return response.data;
  },

  delete: async (id: number) => {
    await axios.delete(`${BASE_URL}/transactions/${id}`);
  }
}; 