import React from 'react';
import { View, StyleSheet, Image, KeyboardAvoidingView, Platform, ScrollView } from 'react-native';
import { Text, useTheme } from 'react-native-paper';
import { useDispatch, useSelector } from 'react-redux';
import { Formik } from 'formik';
import * as Yup from 'yup';
import { Input, Button } from '../../components/common';
import { login } from '../../store/authSlice';
import { LoginData } from '../../types';
import { RootState } from '../../store';

const validationSchema = Yup.object().shape({
  email: Yup.string()
    .email('Geçerli bir email adresi giriniz')
    .required('Email adresi zorunludur'),
  password: Yup.string()
    .min(6, 'Şifre en az 6 karakter olmalıdır')
    .required('Şifre zorunludur'),
});

export const LoginScreen = ({ navigation }) => {
  const dispatch = useDispatch();
  const theme = useTheme();
  const { isLoading, error } = useSelector((state: RootState) => state.auth);

  const handleLogin = async (values: LoginData) => {
    try {
      await dispatch(login(values)).unwrap();
    } catch (error) {
      // Error handling will be managed by the slice
    }
  };

  return (
    <KeyboardAvoidingView 
      behavior={Platform.OS === 'ios' ? 'padding' : 'height'}
      style={styles.container}
    >
      <ScrollView contentContainerStyle={styles.scrollContent}>
        <View style={styles.logoContainer}>
          <Image 
            source={require('../../assets/logo.png')} 
            style={styles.logo}
            resizeMode="contain"
          />
          <Text style={styles.title}>BudgetApp</Text>
          <Text style={styles.subtitle}>Finansal Özgürlüğünüzü Yönetin</Text>
        </View>

        {error && (
          <Text style={[styles.errorText, { color: theme.colors.error }]}>
            {error}
          </Text>
        )}

        <Formik
          initialValues={{ email: '', password: '' }}
          validationSchema={validationSchema}
          onSubmit={handleLogin}
        >
          {({ handleChange, handleSubmit, values, errors, touched }) => (
            <View style={styles.form}>
              <Input
                label="Email"
                value={values.email}
                onChangeText={handleChange('email')}
                error={touched.email && errors.email}
                keyboardType="email-address"
                autoCapitalize="none"
              />
              <Input
                label="Şifre"
                value={values.password}
                onChangeText={handleChange('password')}
                error={touched.password && errors.password}
                secureTextEntry
              />
              <Button 
                onPress={handleSubmit}
                loading={isLoading}
                disabled={isLoading}
                style={styles.loginButton}
              >
                Giriş Yap
              </Button>
            </View>
          )}
        </Formik>

        <View style={styles.footer}>
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
    padding: 20,
  },
  logoContainer: {
    alignItems: 'center',
    marginVertical: 40,
  },
  logo: {
    width: 120,
    height: 120,
    marginBottom: 16,
  },
  title: {
    fontSize: 28,
    fontWeight: 'bold',
    marginBottom: 8,
  },
  subtitle: {
    fontSize: 16,
    opacity: 0.7,
    textAlign: 'center',
  },
  form: {
    marginTop: 20,
  },
  loginButton: {
    marginTop: 20,
  },
  errorText: {
    textAlign: 'center',
    marginBottom: 16,
  },
  footer: {
    marginTop: 'auto',
    alignItems: 'center',
  },
});

export default LoginScreen; 