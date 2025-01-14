import api from './axios';
import { Category, CreateCategoryDto, UpdateCategoryDto } from '../types';

export const categoryApi = {
  getAll: () => 
    api.get<Category[]>('/categories'),
  
  getById: (id: number) => 
    api.get<Category>(`/categories/${id}`),
  
  create: (data: CreateCategoryDto) => 
    api.post<Category>('/categories', data),
  
  update: (id: number, data: UpdateCategoryDto) => 
    api.put<Category>(`/categories/${id}`, data),
  
  delete: (id: number) => 
    api.delete(`/categories/${id}`)
}; 