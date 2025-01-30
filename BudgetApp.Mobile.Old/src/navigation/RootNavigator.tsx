import React, { useEffect } from 'react';
import { createStackNavigator } from '@react-navigation/stack';
import { useDispatch, useSelector } from 'react-redux';
import AsyncStorage from '@react-native-async-storage/async-storage';
import { RootState } from '../store';
import { restoreToken } from '../store/authSlice';
import { AuthNavigator } from './AuthNavigator';
import { AppNavigator } from './AppNavigator';
import { Loading } from '../components/common';

const Stack = createStackNavigator();

export const RootNavigator = () => {
  const dispatch = useDispatch();
  const { token, isLoading } = useSelector((state: RootState) => state.auth);

  useEffect(() => {
    const bootstrapAsync = async () => {
      try {
        console.log('=== Bootstrap başlatılıyor ===');
        console.log('AsyncStorage kontrol ediliyor...');
        
        const storedToken = await AsyncStorage.getItem('token');
        console.log('Bulunan token:', storedToken);

        const result = await dispatch(restoreToken(storedToken)).unwrap();
        console.log('Token restore sonucu:', result);
        
      } catch (error) {
        console.error('Bootstrap hatası:', error);
        await AsyncStorage.removeItem('token');
        dispatch(restoreToken(null));
      } finally {
        console.log('=== Bootstrap tamamlandı ===');
      }
    };

    bootstrapAsync();
  }, []);

  console.log('Render durumu:', { isLoading, token });

  if (isLoading) {
    return <Loading />;
  }

  return (
    <Stack.Navigator screenOptions={{ headerShown: false }}>
      {token ? (
        <Stack.Screen name="App" component={AppNavigator} />
      ) : (
        <Stack.Screen name="Auth" component={AuthNavigator} />
      )}
    </Stack.Navigator>
  );
}; 