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
  description: string;
  amount: number;
  type: TransactionType;
  categoryId: number;
  categoryName: string;
  date: string;
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
  type: TransactionType;
  color: string;
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
  startDate: string;
  endDate: string;
  spentPercentage: number;
}

// Enums
export enum TransactionType {
  Income = 'income',
  Expense = 'expense'
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
export interface MonthlyReport {
  totalIncome: number;
  totalExpense: number;
  balance: number;
  trends: number[];
  categoryDistribution: Array<{
    name: string;
    amount: number;
    color: string;
  }>;
}

export interface YearlyReport extends MonthlyReport {}

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