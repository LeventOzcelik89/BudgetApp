import axios from 'axios';

const BASE_URL = 'http://localhost:5000/api';

export const authApi = {
  login: async (email: string, password: string) => {
    const response = await axios.post(`${BASE_URL}/auth/login`, { email, password });
    return response.data;
  },
  register: async (email: string, password: string) => {
    const response = await axios.post(`${BASE_URL}/auth/register`, { email, password });
    return response.data;
  }
}; 