import { configureStore } from '@reduxjs/toolkit';
import authReducer from './authSlice';
import transactionReducer from './transactionSlice';
import categoryReducer from './categorySlice';
import budgetReducer from './budgetSlice';
import dashboardReducer from './dashboardSlice';
import reportReducer from './reportSlice';
import settingsReducer from './settingsSlice';

export const store = configureStore({
  reducer: {
    auth: authReducer,
    transactions: transactionReducer,
    categories: categoryReducer,
    budgets: budgetReducer,
    dashboard: dashboardReducer,
    reports: reportReducer,
    settings: settingsReducer,
  },
});

export type RootState = ReturnType<typeof store.getState>;
export type AppDispatch = typeof store.dispatch; 