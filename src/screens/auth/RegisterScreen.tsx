import React from 'react';
import { View, StyleSheet } from 'react-native';
import { Text } from 'react-native-paper';
import { useDispatch } from 'react-redux';
import { Formik } from 'formik';
import * as Yup from 'yup';
import { Input, Button } from '../../components/common';
import { register } from '../../store/authSlice';
import { RegisterData } from '../../types';

const validationSchema = Yup.object().shape({
  firstName: Yup.string().required('Ad zorunludur'),
  lastName: Yup.string().required('Soyad zorunludur'),
  email: Yup.string()
    .email('Geçerli bir email adresi giriniz')
    .required('Email zorunludur'),
  password: Yup.string()
    .min(6, 'Şifre en az 6 karakter olmalıdır')
    .required('Şifre zorunludur'),
  confirmPassword: Yup.string()
    .oneOf([Yup.ref('password')], 'Şifreler eşleşmiyor')
    .required('Şifre tekrarı zorunludur'),
});

export const RegisterScreen = ({ navigation }) => {
  const dispatch = useDispatch();

  const handleRegister = async (values: RegisterData) => {
    try {
      await dispatch(register(values)).unwrap();
    } catch (error) {
      console.error('Register error:', error);
    }
  };

  return (
    <View style={styles.container}>
      <Text style={styles.title}>Kayıt Ol</Text>
      <Formik
        initialValues={{
          firstName: '',
          lastName: '',
          email: '',
          password: '',
          confirmPassword: '',
        }}
        validationSchema={validationSchema}
        onSubmit={handleRegister}
      >
        {({ handleChange, handleSubmit, values, errors, touched }) => (
          <>
            <Input
              label="Ad"
              value={values.firstName}
              onChangeText={handleChange('firstName')}
              error={touched.firstName && errors.firstName}
              autoCapitalize="words"
            />
            <Input
              label="Soyad"
              value={values.lastName}
              onChangeText={handleChange('lastName')}
              error={touched.lastName && errors.lastName}
              autoCapitalize="words"
            />
            <Input
              label="Email"
              value={values.email}
              onChangeText={handleChange('email')}
              error={touched.email && errors.email}
              keyboardType="email-address"
            />
            <Input
              label="Şifre"
              value={values.password}
              onChangeText={handleChange('password')}
              error={touched.password && errors.password}
              secureTextEntry
            />
            <Input
              label="Şifre Tekrar"
              value={values.confirmPassword}
              onChangeText={handleChange('confirmPassword')}
              error={touched.confirmPassword && errors.confirmPassword}
              secureTextEntry
            />
            <Button onPress={handleSubmit}>Kayıt Ol</Button>
          </>
        )}
      </Formik>
      <Button mode="text" onPress={() => navigation.navigate('Login')}>
        Zaten hesabınız var mı? Giriş yapın
      </Button>
    </View>
  );
};

const styles = StyleSheet.create({
  container: {
    flex: 1,
    padding: 20,
    justifyContent: 'center',
  },
  title: {
    fontSize: 24,
    marginBottom: 20,
    textAlign: 'center',
  },
});

export default RegisterScreen; 