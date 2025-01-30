import React from 'react';
import { NavigationContainer } from '@react-navigation/native';
import { createNativeStackNavigator } from '@react-navigation/native-stack';
import RegisterScreen from './src/screens/auth/RegisterScreen';
import LoginScreen from './src/screens/auth/LoginScreen';
import DashboardScreen from './src/screens/dashboard/DashboardScreen';
import BudgetsScreen from './src/screens/budgets/BudgetsScreen';
import BudgetDetailScreen from './src/screens/budgets/BudgetDetailScreen';

// Stack Navigator
const Stack = createNativeStackNavigator();

const App = () => {
  return (
    <NavigationContainer>
      <Stack.Navigator initialRouteName="LoginScreen">
        <Stack.Screen name="RegisterScreen" component={RegisterScreen} />
        <Stack.Screen name="LoginScreen" component={LoginScreen} />
        <Stack.Screen name="DashboardScreen" component={DashboardScreen} />
        <Stack.Screen name="BudgetsScreen" component={BudgetsScreen} />
        <Stack.Screen name="BudgetDetailScreen" component={BudgetDetailScreen} />
      </Stack.Navigator>
    </NavigationContainer>
  );
};

export default App;