import { createSlice, createAsyncThunk } from '@reduxjs/toolkit';
import { authApi } from '../api/auth';
import AsyncStorage from '@react-native-async-storage/async-storage';
import { AuthState, LoginData, RegisterData } from '../types';

const initialState: AuthState = {
  user: null,
  token: null,
  isLoading: true,
  error: null,
};

export const login = createAsyncThunk(
  'auth/login',
  async (data: LoginData, { rejectWithValue }) => {
    try {
      const response = await authApi.login(data);
      await AsyncStorage.setItem('token', response.data.token);
      return response.data;
    } catch (error: any) {
      if (error.response?.data?.message) {
        return rejectWithValue(error.response.data.message);
      }
      if (!error.response) {
        return rejectWithValue('Sunucuya bağlanılamıyor. İnternet bağlantınızı kontrol edin.');
      }
      return rejectWithValue('Giriş yapılırken bir hata oluştu');
    }
  }
);

export const register = createAsyncThunk(
  'auth/register',
  async (data: RegisterData, { rejectWithValue }) => {
    try {
      const response = await authApi.register(data);
      await AsyncStorage.setItem('token', response.data.token);
      return response.data;
    } catch (error: any) {
      if (error.response?.data?.message) {
        return rejectWithValue(error.response.data.message);
      }
      if (!error.response) {
        return rejectWithValue('Sunucuya bağlanılamıyor. İnternet bağlantınızı kontrol edin.');
      }
      return rejectWithValue('Kayıt olurken bir hata oluştu');
    }
  }
);

export const restoreToken = createAsyncThunk(
  'auth/restoreToken',
  async (token: string | null, { rejectWithValue }) => {
    try {
      console.log('Token restore işlemi başlatıldı:', token);
      
      if (!token) {
        console.log('Token bulunamadı, null dönülüyor');
        return null;
      }

      // Token doğrulama işlemi burada yapılabilir
      return token;
    } catch (error) {
      console.error('Token restore hatası:', error);
      return rejectWithValue('Token restore hatası');
    }
  }
);

const authSlice = createSlice({
  name: 'auth',
  initialState,
  reducers: {
    logout: (state) => {
      state.user = null;
      state.token = null;
      state.isLoading = false;
      AsyncStorage.removeItem('token');
    },
  },
  extraReducers: (builder) => {
    builder
      .addCase(restoreToken.pending, (state) => {
        console.log('restoreToken.pending');
        state.isLoading = true;
      })
      .addCase(restoreToken.fulfilled, (state, action) => {
        console.log('restoreToken.fulfilled:', action.payload);
        state.token = action.payload;
        state.isLoading = false;
      })
      .addCase(restoreToken.rejected, (state, action) => {
        console.log('restoreToken.rejected:', action.error);
        state.token = null;
        state.isLoading = false;
      })
      .addCase(login.pending, (state) => {
        state.isLoading = true;
        state.error = null;
      })
      .addCase(login.fulfilled, (state, action) => {
        state.isLoading = false;
        state.user = action.payload.user;
        state.token = action.payload.token;
      })
      .addCase(login.rejected, (state, action) => {
        state.isLoading = false;
        state.error = action.error.message;
      })
      .addCase(register.pending, (state) => {
        state.isLoading = true;
        state.error = null;
      })
      .addCase(register.fulfilled, (state, action) => {
        state.isLoading = false;
        state.user = action.payload.user;
        state.token = action.payload.token;
      })
      .addCase(register.rejected, (state, action) => {
        state.isLoading = false;
        state.error = action.error.message;
      });
  },
});

export const { logout } = authSlice.actions;
export default authSlice.reducer;