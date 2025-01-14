import React, { useState } from 'react';
import { View, StyleSheet, KeyboardAvoidingView, Platform, ScrollView } from 'react-native';
import { Text, useTheme, HelperText } from 'react-native-paper';
import { useDispatch } from 'react-redux';
import { login } from '../../store/authSlice';
import { Input, Button } from '../../components/common';

export const LoginScreen = ({ navigation }) => {
  const dispatch = useDispatch();
  const theme = useTheme();
  const [email, setEmail] = useState('');
  const [password, setPassword] = useState('');
  const [isLoading, setIsLoading] = useState(false);
  const [error, setError] = useState('');

  const validateEmail = (email: string) => {
    return /^[^\s@]+@[^\s@]+\.[^\s@]+$/.test(email);
  };

  const handleLogin = async () => {
    try {
      setError('');
      
      // Validation
      if (!email || !password) {
        setError('Lütfen tüm alanları doldurun');
        return;
      }

      if (!validateEmail(email)) {
        setError('Geçerli bir e-posta adresi girin');
        return;
      }

      if (password.length < 6) {
        setError('Şifre en az 6 karakter olmalıdır');
        return;
      }

      setIsLoading(true);
      await dispatch(login({ email, password })).unwrap();
    } catch (error: any) {
      if (error.response?.status === 400) {
        setError('E-posta veya şifre hatalı');
      } else if (error.response?.status === 404) {
        setError('Kullanıcı bulunamadı');
      } else if (!error.response) {
        setError('Sunucuya bağlanılamıyor. İnternet bağlantınızı kontrol edin.');
      } else {
        setError('Giriş yapılırken bir hata oluştu. Lütfen tekrar deneyin.');
      }
      console.error('Login error:', error);
    } finally {
      setIsLoading(false);
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
          
          {error ? (
            <HelperText type="error" visible={!!error}>
              {error}
            </HelperText>
          ) : null}

          <Input
            label="E-posta"
            value={email}
            onChangeText={(text) => {
              setEmail(text);
              setError('');
            }}
            keyboardType="email-address"
            autoCapitalize="none"
            error={error && !validateEmail(email)}
          />

          <Input
            label="Şifre"
            value={password}
            onChangeText={(text) => {
              setPassword(text);
              setError('');
            }}
            secureTextEntry
            error={error && password.length < 6}
          />

          <Button
            mode="contained"
            onPress={handleLogin}
            loading={isLoading}
            disabled={isLoading || !email || !password}
          >
            Giriş Yap
          </Button>

          <Button
            mode="text"
            onPress={() => navigation.navigate('Register')}
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