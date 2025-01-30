import axios from 'axios';
import { Budget } from '../types';

const BASE_URL = 'http://localhost:5000/api';

export const budgetsApi = {
  getAll: async () => {
    const response = await axios.get(`${BASE_URL}/budgets`);
    return response.data;
  },

  getById: async (id: number) => {
    const response = await axios.get(`${BASE_URL}/budgets/${id}`);
    return response.data;
  },

  create: async (budget: Omit<Budget, 'id'>) => {
    const response = await axios.post(`${BASE_URL}/budgets`, budget);
    return response.data;
  },

  update: async (id: number, budget: Partial<Budget>) => {
    const response = await axios.put(`${BASE_URL}/budgets/${id}`, budget);
    return response.data;
  },

  delete: async (id: number) => {
    await axios.delete(`${BASE_URL}/budgets/${id}`);
  }
}; 