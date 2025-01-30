import { createSlice, createAsyncThunk } from '@reduxjs/toolkit';
import { settingsApi } from '../api/settings';

interface NotificationPreferences {
  notifyOnBudgetExceeded: boolean;
  notifyOnGoalProgress: boolean;
  notifyOnLargeTransactions: boolean;
}

interface Settings {
  currencyCode: string;
  notificationPreferences: NotificationPreferences;
}

interface SettingsState {
  settings: Settings;
  isLoading: boolean;
  error: string | null;
}

const initialState: SettingsState = {
  settings: {
    currencyCode: 'TRY',
    notificationPreferences: {
      notifyOnBudgetExceeded: true,
      notifyOnGoalProgress: true,
      notifyOnLargeTransactions: true,
    },
  },
  isLoading: false,
  error: null,
};

export const fetchSettings = createAsyncThunk(
  'settings/fetch',
  async () => {
    const response = await settingsApi.getSettings();
    return response.data;
  }
);

export const updateSettings = createAsyncThunk(
  'settings/update',
  async (data: Partial<Settings>) => {
    const response = await settingsApi.updateSettings(data);
    return response.data;
  }
);

const settingsSlice = createSlice({
  name: 'settings',
  initialState,
  reducers: {},
  extraReducers: (builder) => {
    builder
      .addCase(fetchSettings.pending, (state) => {
        state.isLoading = true;
      })
      .addCase(fetchSettings.fulfilled, (state, action) => {
        state.isLoading = false;
        state.settings = action.payload;
      })
      .addCase(fetchSettings.rejected, (state, action) => {
        state.isLoading = false;
        state.error = action.error.message || null;
      })
      .addCase(updateSettings.fulfilled, (state, action) => {
        state.settings = { ...state.settings, ...action.payload };
      });
  },
});

export default settingsSlice.reducer; 