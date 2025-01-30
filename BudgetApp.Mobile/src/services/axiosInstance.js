import axios from 'axios';
import AsyncStorage from '@react-native-async-storage/async-storage';
import Config from '../services/config';

const axiosInstance = axios.create({
  baseURL: Config.SERVER_ADDRESS,
  headers: {
    'Content-Type': 'application/json',
  },
});

axiosInstance.interceptors.request.use(
  async (config) => {
    const token = await AsyncStorage.getItem('authToken');
    if (token) {
      config.headers['Authorization'] = `Bearer ${token}`;
    }
    return config;
  },
  (error) => {
    return Promise.reject(error);
  }
);

export default axiosInstance;