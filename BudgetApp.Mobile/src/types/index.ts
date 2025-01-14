// Auth types
export interface User {
  id: number;
  firstName: string;
  lastName: string;
  email: string;
}

export interface LoginData {
  email: string;
  password: string;
}

export interface RegisterData {
  firstName: string;
  lastName: string;
  email: string;
  password: string;
  confirmPassword: string;
}

export interface AuthResponse {
  user: User;
  token: string;
}

export interface AuthState {
  user: User | null;
  token: string | null;
  isLoading: boolean;
  error: string | null;
}

// Transaction types
export interface Transaction {
  id: number;
  amount: number;
  description: string;
  transactionDate: string;
  type: TransactionType;
  categoryId?: number;
  categoryName?: string;
  currencyCode: string;
  originalAmount: number;
  convertedAmount: number;
  exchangeRate: number;
}

export interface CreateTransaction {
  amount: number;
  description: string;
  transactionDate: string;
  type: TransactionType;
  categoryId?: number;
  currencyCode: string;
}

export interface TransactionSummary {
  totalIncome: number;
  totalExpense: number;
  balance: number;
  currency: CurrencyInfo;
  categorySummaries: CategorySummary[];
}

// Category types
export interface Category {
  id: number;
  name: string;
  description: string;
  type: CategoryType;
}

export interface CategorySummary {
  categoryId: number;
  categoryName: string;
  amount: number;
  percentage: number;
}

// Budget types
export interface Budget {
  id: number;
  categoryId: number;
  categoryName: string;
  plannedAmount: number;
  spentAmount: number;
  remainingAmount: number;
  spentPercentage: number;
  startDate: string;
  endDate: string;
}

// Enums
export enum TransactionType {
  Income = 1,
  Expense = 2
}

export enum CategoryType {
  Income = 1,
  Expense = 2,
  Both = 3
}

// Currency types
export interface CurrencyInfo {
  code: string;
  name: string;
  symbol: string;
  flag: string;
}

// Report types
export interface MonthlyReportDto {
  year: number;
  month: number;
  totalIncome: number;
  totalExpense: number;
  balance: number;
  weeklyTrends: number[];
  categories: CategoryReportDto[];
  budgetComparisons: BudgetComparisonDto[];
}

export interface YearlyReportDto {
  year: number;
  totalIncome: number;
  totalExpense: number;
  balance: number;
  monthlyTrends: number[];
  topCategories: CategoryReportDto[];
}

export interface CategoryReportDto {
  categoryId: number;
  categoryName: string;
  amount: number;
  percentage: number;
  change?: number;
}

export interface BudgetComparisonDto {
  categoryId: number;
  categoryName: string;
  plannedAmount: number;
  actualAmount: number;
  difference: number;
  progress: number;
}

// Category types
export interface CreateCategoryDto {
  name: string;
  description: string;
  type: CategoryType;
}

export interface UpdateCategoryDto {
  name: string;
  description: string;
  type: CategoryType;
}

export interface Settings {
  currencyCode: string;
  notificationPreferences: {
    notifyOnBudgetExceeded: boolean;
    notifyOnGoalProgress: boolean;
    notifyOnLargeTransactions: boolean;
  };
} 