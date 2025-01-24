import React, { useState } from 'react';
import { View, StyleSheet, KeyboardAvoidingView, Platform, ScrollView } from 'react-native';
import { Text, useTheme } from 'react-native-paper';
import { useDispatch } from 'react-redux';
import { register } from '../../store/authSlice';
import { Input, Button } from '../../components/common';

export const RegisterScreen = ({ navigation }) => {
  const dispatch = useDispatch();
  const theme = useTheme();
  const [firstName, setFirstName] = useState('');
  const [lastName, setLastName] = useState('');
  const [email, setEmail] = useState('');
  const [password, setPassword] = useState('');
  const [confirmPassword, setConfirmPassword] = useState('');
  const [isLoading, setIsLoading] = useState(false);
  const [error, setError] = useState('');

  const handleRegister = async () => {
    if (password !== confirmPassword) {
      setError('Şifreler eşleşmiyor');
      return;
    }

    try {
      setIsLoading(true);
      setError('');
      await dispatch(register({
        firstName,
        lastName,
        email,
        password,
        confirmPassword
      })).unwrap();
    } catch (error) {
      setError(error.message || 'Kayıt işlemi başarısız oldu');
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
          <Text style={styles.title}>Hesap Oluştur</Text>

          {error && (
            <Text style={[styles.errorText, { color: theme.colors.error }]}>
              {error}
            </Text>
          )}

          <Input
            label="Ad"
            value={firstName}
            onChangeText={setFirstName}
            autoCapitalize="words"
          />

          <Input
            label="Soyad"
            value={lastName}
            onChangeText={setLastName}
            autoCapitalize="words"
          />

          <Input
            label="E-posta"
            value={email}
            onChangeText={setEmail}
            keyboardType="email-address"
            autoCapitalize="none"
          />

          <Input
            label="Şifre"
            value={password}
            onChangeText={setPassword}
            secureTextEntry
          />

          <Input
            label="Şifre Tekrar"
            value={confirmPassword}
            onChangeText={setConfirmPassword}
            secureTextEntry
          />

          <Button
            mode="contained"
            onPress={handleRegister}
            loading={isLoading}
            disabled={isLoading || !email || !password || !firstName || !lastName || !confirmPassword}
            style={styles.registerButton}
          >
            Kayıt Ol
          </Button>

          <Button
            mode="text"
            onPress={() => navigation.navigate('Login')}
          >
            Zaten hesabınız var mı? Giriş yapın
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
  errorText: {
    textAlign: 'center',
    marginBottom: 16,
  },
  registerButton: {
    marginTop: 16,
    marginBottom: 8,
  },
});

export default RegisterScreen; 