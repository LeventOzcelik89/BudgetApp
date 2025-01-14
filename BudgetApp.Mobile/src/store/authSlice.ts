import { createSlice, createAsyncThunk } from '@reduxjs/toolkit';
import { authApi } from '@/api/auth';

interface AuthState {
  token: string | null;
  loading: boolean;
  error: string | null;
}

const initialState: AuthState = {
  token: null,
  loading: false,
  error: null
};

export const login = createAsyncThunk(
  'auth/login',
  async ({ email, password }: { email: string; password: string }) => {
    const response = await authApi.login(email, password);
    return response;
  }
);

const authSlice = createSlice({
  name: 'auth',
  initialState,
  reducers: {
    logout: (state) => {
      state.token = null;
    }
  },
  extraReducers: (builder) => {
    builder
      .addCase(login.pending, (state) => {
        state.loading = true;
        state.error = null;
      })
      .addCase(login.fulfilled, (state, action) => {
        state.loading = false;
        state.token = action.payload.token;
      })
      .addCase(login.rejected, (state, action) => {
        state.loading = false;
        state.error = action.error.message || 'Login failed';
      });
  }
});

export const { logout } = authSlice.actions;
export default authSlice.reducer; 