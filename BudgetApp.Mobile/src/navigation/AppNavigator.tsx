import React from 'react';
import { createBottomTabNavigator } from '@react-navigation/bottom-tabs';
import { createStackNavigator } from '@react-navigation/stack';
import Icon from 'react-native-vector-icons/MaterialCommunityIcons';
import { useTheme } from 'react-native-paper';

// Import screens
import DashboardScreen from '../screens/dashboard/DashboardScreen';
import TransactionsScreen from '../screens/transactions/TransactionsScreen';
import TransactionDetailScreen from '../screens/transactions/TransactionDetailScreen';
import AddTransactionScreen from '../screens/transactions/AddTransactionScreen';
import BudgetsScreen from '../screens/budgets/BudgetsScreen';
import BudgetDetailScreen from '../screens/budgets/BudgetDetailScreen';
import AddBudgetScreen from '../screens/budgets/AddBudgetScreen';
import CategoriesScreen from '../screens/categories/CategoriesScreen';
import AddCategoryScreen from '../screens/categories/AddCategoryScreen';
import ReportsScreen from '../screens/reports/ReportsScreen';
import SettingsScreen from '../screens/settings/SettingsScreen';

const Tab = createBottomTabNavigator();
const Stack = createStackNavigator();

const TransactionsStack = () => (
  <Stack.Navigator>
    <Stack.Screen name="TransactionsList" component={TransactionsScreen} options={{ title: 'İşlemler' }} />
    <Stack.Screen name="TransactionDetail" component={TransactionDetailScreen} options={{ title: 'İşlem Detayı' }} />
    <Stack.Screen name="AddTransaction" component={AddTransactionScreen} options={{ title: 'Yeni İşlem' }} />
  </Stack.Navigator>
);

const BudgetsStack = () => (
  <Stack.Navigator>
    <Stack.Screen name="BudgetsList" component={BudgetsScreen} options={{ title: 'Bütçeler' }} />
    <Stack.Screen name="BudgetDetail" component={BudgetDetailScreen} options={{ title: 'Bütçe Detayı' }} />
    <Stack.Screen name="AddBudget" component={AddBudgetScreen} options={{ title: 'Yeni Bütçe' }} />
  </Stack.Navigator>
);

const SettingsStack = () => (
  <Stack.Navigator>
    <Stack.Screen name="SettingsList" component={SettingsScreen} options={{ title: 'Ayarlar' }} />
    <Stack.Screen name="Categories" component={CategoriesScreen} options={{ title: 'Kategoriler' }} />
    <Stack.Screen name="AddCategory" component={AddCategoryScreen} options={{ title: 'Yeni Kategori' }} />
  </Stack.Navigator>
);

export const AppNavigator = () => {
  const theme = useTheme();

  return (
    <Tab.Navigator
      screenOptions={({ route }) => ({
        tabBarIcon: ({ focused, color, size }) => {
          let iconName;

          switch (route.name) {
            case 'Dashboard':
              iconName = 'view-dashboard';
              break;
            case 'Transactions':
              iconName = 'swap-horizontal';
              break;
            case 'Budgets':
              iconName = 'wallet';
              break;
            case 'Reports':
              iconName = 'chart-bar';
              break;
            case 'Settings':
              iconName = 'cog';
              break;
            default:
              iconName = 'circle';
          }

          return <Icon name={iconName} size={size} color={color} />;
        },
        tabBarActiveTintColor: theme.colors.primary,
        tabBarInactiveTintColor: theme.colors.disabled,
      })}
    >
      <Tab.Screen 
        name="Dashboard" 
        component={DashboardScreen} 
        options={{ title: 'Ana Sayfa' }}
      />
      <Tab.Screen 
        name="Transactions" 
        component={TransactionsStack} 
        options={{ headerShown: false, title: 'İşlemler' }}
      />
      <Tab.Screen 
        name="Budgets" 
        component={BudgetsStack}
        options={{ headerShown: false, title: 'Bütçeler' }}
      />
      <Tab.Screen 
        name="Reports" 
        component={ReportsScreen}
        options={{ title: 'Raporlar' }}
      />
      <Tab.Screen 
        name="Settings" 
        component={SettingsStack}
        options={{ headerShown: false, title: 'Ayarlar' }}
      />
    </Tab.Navigator>
  );
}; 