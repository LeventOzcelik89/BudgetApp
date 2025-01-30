import { createSlice, createAsyncThunk } from '@reduxjs/toolkit';
import { reportApi } from '../api/reports';

interface ReportState {
  monthlyReport: any;
  yearlyReport: any;
  isLoading: boolean;
  error: string | null;
}

const initialState: ReportState = {
  monthlyReport: null,
  yearlyReport: null,
  isLoading: false,
  error: null,
};

export const fetchMonthlyReport = createAsyncThunk(
  'reports/fetchMonthly',
  async ({ year, month }: { year: number; month: number }) => {
    const response = await reportApi.getMonthlyReport(year, month);
    return response.data;
  }
);

export const fetchYearlyReport = createAsyncThunk(
  'reports/fetchYearly',
  async (year: number) => {
    const response = await reportApi.getYearlyReport(year);
    return response.data;
  }
);

const reportSlice = createSlice({
  name: 'reports',
  initialState,
  reducers: {},
  extraReducers: (builder) => {
    builder
      .addCase(fetchMonthlyReport.pending, (state) => {
        state.isLoading = true;
      })
      .addCase(fetchMonthlyReport.fulfilled, (state, action) => {
        state.isLoading = false;
        state.monthlyReport = action.payload;
      })
      .addCase(fetchMonthlyReport.rejected, (state, action) => {
        state.isLoading = false;
        state.error = action.error.message || null;
      })
      .addCase(fetchYearlyReport.pending, (state) => {
        state.isLoading = true;
      })
      .addCase(fetchYearlyReport.fulfilled, (state, action) => {
        state.isLoading = false;
        state.yearlyReport = action.payload;
      })
      .addCase(fetchYearlyReport.rejected, (state, action) => {
        state.isLoading = false;
        state.error = action.error.message || null;
      });
  },
});

export default reportSlice.reducer; 