import axios from 'axios';
import AsyncStorage from '@react-native-async-storage/async-storage';

const BASE_URL = 'http://10.0.2.2:5007/api'; // For Android Emulator
// const BASE_URL = 'http://localhost:5007/api'; // For iOS Simulator

const api = axios.create({
  baseURL: BASE_URL,
  headers: {
    'Content-Type': 'application/json',
  },
  timeout: 10000, // 10 seconds timeout
});

api.interceptors.request.use(
  async (config) => {
    try {
      const token = await AsyncStorage.getItem('token');
      if (token) {
        config.headers.Authorization = `Bearer ${token}`;
      }
      return config;
    } catch (error) {
      console.error('Error setting auth token:', error);
      return config;
    }
  },
  (error) => {
    console.error('Request interceptor error:', error);
    return Promise.reject(error);
  }
);

api.interceptors.response.use(
  (response) => response,
  async (error) => {
    if (error.response?.status === 401) {
      await AsyncStorage.removeItem('token');
      // You might want to dispatch a logout action here
    }
    
    // Network error
    if (!error.response) {
      console.error('Network error:', error);
      throw new Error('Sunucuya bağlanılamıyor. İnternet bağlantınızı kontrol edin.');
    }

    // Server error
    if (error.response?.status >= 500) {
      console.error('Server error:', error);
      throw new Error('Sunucu hatası. Lütfen daha sonra tekrar deneyin.');
    }

    return Promise.reject(error);
  }
);

export default api; 