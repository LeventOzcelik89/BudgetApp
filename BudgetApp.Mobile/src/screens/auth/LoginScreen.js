import React, { useReducer } from 'react';
import { View, StyleSheet, KeyboardAvoidingView, Platform, ScrollView } from 'react-native';
import { Text, useTheme, HelperText } from 'react-native-paper';
import Input from '../../components/common/Input';
import Button from '../../components/common/Button';
import { authApi } from '../../services/auth'; // authApi'yi doğrudan kullanacağız

const initialState = {
  email: '',
  password: '',
  isLoading: false,
  error: ''
};

const reducer = (state, action) => {
  switch (action.type) {
    case 'SET_FIELD':
      return { ...state, [action.field]: action.value };
    case 'SET_LOADING':
      return { ...state, isLoading: action.value };
    case 'SET_ERROR':
      return { ...state, error: action.value };
    default:
      return state;
  }
};

const validateEmail = (email) => {
  return /^[^\s@]+@[^\s@]+\.[^\s@]+$/.test(email);
};

export const LoginScreen = ({ navigation }) => {
  const [state, dispatch] = useReducer(reducer, initialState);
  const theme = useTheme();

  const handleLogin = async () => {
    try {
      dispatch({ type: 'SET_ERROR', value: '' });
      
      // Validation
      if (!state.email || !state.password) {
        dispatch({ type: 'SET_ERROR', value: 'Lütfen tüm alanları doldurun' });
        return;
      }

      if (!validateEmail(state.email)) {
        dispatch({ type: 'SET_ERROR', value: 'Geçerli bir e-posta adresi girin' });
        return;
      }

      if (state.password.length < 6) {
        dispatch({ type: 'SET_ERROR', value: 'Şifre en az 6 karakter olmalıdır' });
        return;
      }

      dispatch({ type: 'SET_LOADING', value: true });
      const response = await authApi.login({ email: state.email, password: state.password });
      navigation.navigate('BudgetsScreen');
    } catch (error) {
      console.log(error);
      if (error.response?.status === 400) {
        dispatch({ type: 'SET_ERROR', value: 'E-posta veya şifre hatalı' });
      } else if (error.response?.status === 404) {
        dispatch({ type: 'SET_ERROR', value: 'User not found' });
      } else if (!error.response) {
        dispatch({ type: 'SET_ERROR', value: 'Sunucuya bağlanılamıyor. İnternet bağlantınızı kontrol edin.' });
      } else {
        dispatch({ type: 'SET_ERROR', value: 'Giriş yapılırken bir hata oluştu. Lütfen tekrar deneyin.' });
      }
      console.error('Login error:', error);
    } finally {
      dispatch({ type: 'SET_LOADING', value: false });
    }
  };

  return (
    <KeyboardAvoidingView
      behavior={Platform.OS === 'ios' ? 'padding' : 'height'}
      style={styles.container}
    >
      <ScrollView contentContainerStyle={styles.scrollContent}>
        <View style={styles.content}>
          <Text style={styles.title}>BudgetApp'e Hoş Geldiniz</Text>
          
          {state.error ? (
            <HelperText type="error" visible={!!state.error}>
              {state.error}
            </HelperText>
          ) : null}

          <Input
            label="E-posta"
            value={state.email}
            onChangeText={(text) => {
              dispatch({ type: 'SET_FIELD', field: 'email', value: text });
              dispatch({ type: 'SET_ERROR', value: '' });
            }}
            keyboardType="email-address"
            autoCapitalize="none"
            error={state.error && !validateEmail(state.email)}
          />

          <Input
            label="Şifre"
            value={state.password}
            onChangeText={(text) => {
              dispatch({ type: 'SET_FIELD', field: 'password', value: text });
              dispatch({ type: 'SET_ERROR', value: '' });
            }}
            secureTextEntry
            error={state.error && state.password.length < 6}
          />

          <Button
            mode="contained"
            onPress={handleLogin}
            loading={state.isLoading}
            disabled={state.isLoading || !state.email || !state.password}
          >
            Login
          </Button>

          <Button
            mode="text"
            onPress={() => navigation.navigate('RegisterScreen')}
          >
            Hesabınız yok mu? Kayıt olun
          </Button>
        </View>
      </ScrollView>
    </KeyboardAvoidingView>
  );
};

const styles = StyleSheet.create({
  container: {
    flex: 1,
    backgroundColor: '#fff',
  },
  scrollContent: {
    flexGrow: 1,
  },
  content: {
    flex: 1,
    padding: 20,
    justifyContent: 'center',
  },
  title: {
    fontSize: 24,
    fontWeight: 'bold',
    marginBottom: 32,
    textAlign: 'center',
  },
});

export default LoginScreen;