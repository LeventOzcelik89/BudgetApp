import { createSlice, createAsyncThunk } from '@reduxjs/toolkit';
import { transactionsApi } from '../api/transactions';

interface DashboardState {
  data: {
    totalIncome: number;
    totalExpense: number;
    weeklyTrends: number[];
    categoryDistribution: Array<{
      name: string;
      amount: number;
      color: string;
    }>;
    recentTransactions: Array<{
      id: number;
      description: string;
      amount: number;
      type: 'income' | 'expense';
      categoryName: string;
    }>;
  } | null;
  isLoading: boolean;
  error: string | null;
}

const initialState: DashboardState = {
  data: null,
  isLoading: false,
  error: null
};

export const fetchDashboardData = createAsyncThunk(
  'dashboard/fetchData',
  async () => {
    const transactions = await transactionsApi.getAll();
    // Burada verileri işleyip dashboard formatına dönüştürebilirsiniz
    return {
      totalIncome: 0,
      totalExpense: 0,
      weeklyTrends: [0, 0, 0, 0],
      categoryDistribution: [],
      recentTransactions: []
    };
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
        state.error = null;
      })
      .addCase(fetchDashboardData.fulfilled, (state, action) => {
        state.isLoading = false;
        state.data = action.payload;
      })
      .addCase(fetchDashboardData.rejected, (state, action) => {
        state.isLoading = false;
        state.error = action.error.message || 'Bir hata oluştu';
      });
  }
});

export default dashboardSlice.reducer; 