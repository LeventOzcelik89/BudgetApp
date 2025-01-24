import { createSlice, createAsyncThunk } from '@reduxjs/toolkit';
import { budgetApi } from '../api/budgets';
import { Budget } from '../types';

interface BudgetState {
  budgets: Budget[];
  isLoading: boolean;
  error: string | null;
}

const initialState: BudgetState = {
  budgets: [],
  isLoading: false,
  error: null,
};

export const fetchBudgets = createAsyncThunk(
  'budgets/fetchAll',
  async () => {
    const response = await budgetApi.getAll();
    return response.data;
  }
);

export const createBudget = createAsyncThunk(
  'budgets/create',
  async (data: any) => {
    const response = await budgetApi.create(data);
    return response.data;
  }
);

export const updateBudget = createAsyncThunk(
  'budgets/update',
  async ({ id, data }: { id: number; data: any }) => {
    const response = await budgetApi.update(id, data);
    return response.data;
  }
);

export const deleteBudget = createAsyncThunk(
  'budgets/delete',
  async (id: number) => {
    await budgetApi.delete(id);
    return id;
  }
);

const budgetSlice = createSlice({
  name: 'budgets',
  initialState,
  reducers: {},
  extraReducers: (builder) => {
    builder
      .addCase(fetchBudgets.pending, (state) => {
        state.isLoading = true;
      })
      .addCase(fetchBudgets.fulfilled, (state, action) => {
        state.isLoading = false;
        state.budgets = action.payload;
      })
      .addCase(fetchBudgets.rejected, (state, action) => {
        state.isLoading = false;
        state.error = action.error.message || null;
      })
      .addCase(createBudget.fulfilled, (state, action) => {
        state.budgets.push(action.payload);
      })
      .addCase(updateBudget.fulfilled, (state, action) => {
        const index = state.budgets.findIndex(b => b.id === action.payload.id);
        if (index !== -1) {
          state.budgets[index] = action.payload;
        }
      })
      .addCase(deleteBudget.fulfilled, (state, action) => {
        state.budgets = state.budgets.filter(b => b.id !== action.payload);
      });
  },
});

export default budgetSlice.reducer; 