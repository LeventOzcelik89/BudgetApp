import axios from 'axios';
import { Category } from '../types';

const BASE_URL = 'http://localhost:5000/api';

export const categoriesApi = {
  getAll: async () => {
    const response = await axios.get(`${BASE_URL}/categories`);
    return response.data;
  },

  getById: async (id: number) => {
    const response = await axios.get(`${BASE_URL}/categories/${id}`);
    return response.data;
  },

  create: async (category: Omit<Category, 'id'>) => {
    const response = await axios.post(`${BASE_URL}/categories`, category);
    return response.data;
  },

  update: async (id: number, category: Partial<Category>) => {
    const response = await axios.put(`${BASE_URL}/categories/${id}`, category);
    return response.data;
  },

  delete: async (id: number) => {
    await axios.delete(`${BASE_URL}/categories/${id}`);
  }
}; 