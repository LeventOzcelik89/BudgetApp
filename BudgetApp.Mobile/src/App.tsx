import React from 'react';
import { Provider as PaperProvider } from 'react-native-paper';
import { Provider as StoreProvider } from 'react-redux';
import { NavigationContainer } from '@react-navigation/native';
import { SafeAreaProvider } from 'react-native-safe-area-context';
import { store } from './store';
import { theme } from './theme';
import { Navigation } from './navigation';

export default function App() {
  return (
    <StoreProvider store={store}>
      <PaperProvider theme={theme}>
        <SafeAreaProvider>
          <NavigationContainer>
            <Navigation />
          </NavigationContainer>
        </SafeAreaProvider>
      </PaperProvider>
    </StoreProvider>
  );
} 