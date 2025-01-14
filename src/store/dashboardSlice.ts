import { createSlice, createAsyncThunk } from '@reduxjs/toolkit';
import { transactionApi } from '../api/transactions';

interface DashboardState {
  data: {
    totalIncome: number;
    totalExpense: number;
    monthlyTrends: number[];
    recentTransactions: any[];
  } | null;
  isLoading: boolean;
  error: string | null;
}

const initialState: DashboardState = {
  data: null,
  isLoading: false,
  error: null,
};

export const fetchDashboardData = createAsyncThunk(
  'dashboard/fetchData',
  async () => {
    const startDate = new Date(new Date().getFullYear(), 0, 1).toISOString();
    const endDate = new Date().toISOString();
    const response = await transactionApi.getSummary(startDate, endDate);
    return response.data;
  }
);

const dashboardSlice = createSlice({
  name: 'dashboard',
  initialState,
  reducers: {},
  extraReducers: (builder) => {
    builder
      .addCase(fetchDashboardData.pending, (state) => {
        state.isLoading = true;
      })
      .addCase(fetchDashboardData.fulfilled, (state, action) => {
        state.isLoading = false;
        state.data = action.payload;
      })
      .addCase(fetchDashboardData.rejected, (state, action) => {
        state.isLoading = false;
        state.error = action.error.message || null;
      });
  },
});

export default dashboardSlice.reducer; 