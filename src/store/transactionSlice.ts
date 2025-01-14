import { createSlice, createAsyncThunk } from '@reduxjs/toolkit';
import { transactionApi } from '../api/transactions';
import { Transaction, CreateTransaction } from '../types';

interface TransactionState {
  transactions: Transaction[];
  isLoading: boolean;
  error: string | null;
}

const initialState: TransactionState = {
  transactions: [],
  isLoading: false,
  error: null,
};

export const fetchTransactions = createAsyncThunk(
  'transactions/fetchAll',
  async () => {
    const response = await transactionApi.getAll();
    return response.data;
  }
);

export const createTransaction = createAsyncThunk(
  'transactions/create',
  async (data: CreateTransaction) => {
    const response = await transactionApi.create(data);
    return response.data;
  }
);

export const updateTransaction = createAsyncThunk(
  'transactions/update',
  async ({ id, data }: { id: number; data: Partial<Transaction> }) => {
    const response = await transactionApi.update(id, data);
    return response.data;
  }
);

export const deleteTransaction = createAsyncThunk(
  'transactions/delete',
  async (id: number) => {
    await transactionApi.delete(id);
    return id;
  }
);

const transactionSlice = createSlice({
  name: 'transactions',
  initialState,
  reducers: {},
  extraReducers: (builder) => {
    builder
      // Fetch transactions
      .addCase(fetchTransactions.pending, (state) => {
        state.isLoading = true;
      })
      .addCase(fetchTransactions.fulfilled, (state, action) => {
        state.isLoading = false;
        state.transactions = action.payload;
      })
      .addCase(fetchTransactions.rejected, (state, action) => {
        state.isLoading = false;
        state.error = action.error.message || null;
      })
      // Create transaction
      .addCase(createTransaction.fulfilled, (state, action) => {
        state.transactions.unshift(action.payload);
      })
      // Update transaction
      .addCase(updateTransaction.fulfilled, (state, action) => {
        const index = state.transactions.findIndex(t => t.id === action.payload.id);
        if (index !== -1) {
          state.transactions[index] = action.payload;
        }
      })
      // Delete transaction
      .addCase(deleteTransaction.fulfilled, (state, action) => {
        state.transactions = state.transactions.filter(t => t.id !== action.payload);
      });
  },
});

export default transactionSlice.reducer; 