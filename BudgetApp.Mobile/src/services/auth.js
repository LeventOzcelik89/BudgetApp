import AsyncStorage from '@react-native-async-storage/async-storage';
import axiosInstance from './axiosInstance';

export const setAuthToken = async (token) => {
  if (token) {
    await AsyncStorage.setItem('authToken', token);
    axiosInstance.defaults.headers.common['Authorization'] = `Bearer ${token}`;
  } else {
    await AsyncStorage.removeItem('authToken');
    delete axiosInstance.defaults.headers.common['Authorization'];
  }
};

export const authApi = {
  login: async (data) => {
    const response = await axiosInstance.post('/auth/login', data);
    const { token } = response.data;
    await setAuthToken(token); // Token'ı ayarla
    return response;
  },
  register: (data) => axiosInstance.post('/auth/register', data),
  verifyToken: () => axiosInstance.get('/auth/verify'),
  logout: async () => {
    await AsyncStorage.removeItem('authToken');
    return axiosInstance.post('/auth/logout');
  },
};

export default axiosInstance;