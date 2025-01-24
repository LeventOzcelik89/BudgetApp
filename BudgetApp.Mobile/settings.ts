import api from './axios';
import { Settings } from '../types';

export const settingsApi = {
  getSettings: () => 
    api.get<Settings>('/settings'),
  
  updateSettings: (data: Partial<Settings>) => 
    api.put<Settings>('/settings', data),
}; 