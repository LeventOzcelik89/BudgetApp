import React, { useReducer } from 'react';
import { View, StyleSheet, KeyboardAvoidingView, Platform, ScrollView } from 'react-native';
import { Text, useTheme } from 'react-native-paper';
import Input from '../../components/common/Input';
import Button from '../../components/common/Button';
import { authApi } from '../../services/auth';

const initialState = {
  firstName: '',
  lastName: '',
  email: '',
  password: '',
  confirmPassword: '',
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

const validateForm = (state) => {
  if (!state.firstName || !state.lastName || !state.email || !state.password || !state.confirmPassword) {
    return 'Lütfen tüm alanları doldurun';
  }
  if (state.password !== state.confirmPassword) {
    return 'Şifreler eşleşmiyor';
  }
  return '';
};

export const RegisterScreen = ({ navigation }) => {
  const [state, dispatch] = useReducer(reducer, initialState);
  const theme = useTheme();

  const handleRegister = async () => {
    const validationError = validateForm(state);
    if (validationError) {
      dispatch({ type: 'SET_ERROR', value: validationError });
      return;
    }

    try {
      dispatch({ type: 'SET_LOADING', value: true });
      dispatch({ type: 'SET_ERROR', value: '' });
      const response = await authApi.register({
        firstName: state.firstName,
        lastName: state.lastName,
        email: state.email,
        password: state.password,
        confirmPassword: state.confirmPassword
      });
      navigation.navigate('LoginScreen');
    } catch (error) {
      dispatch({ type: 'SET_ERROR', value: error.message || 'Registration failed' });
    } finally {
      dispatch({ type: 'SET_LOADING', value: false });
    }
  };

  return (
    <KeyboardAvoidingView behavior={Platform.OS === 'ios' ? 'padding' : 'height'} style={styles.container}>
      <ScrollView contentContainerStyle={styles.scrollContent}>
        <View style={styles.content}>
          <Text style={styles.title}>Create Account</Text>

          {state.error && (
            <Text style={[styles.errorText, { color: theme.colors.error }]}>
              {state.error}
            </Text>
          )}

          <Input
            label="First Name"
            value={state.firstName}
            onChangeText={(value) => dispatch({ type: 'SET_FIELD', field: 'firstName', value })}
            autoCapitalize="words"
          />

          <Input
            label="Last Name"
            value={state.lastName}
            onChangeText={(value) => dispatch({ type: 'SET_FIELD', field: 'lastName', value })}
            autoCapitalize="words"
          />

          <Input
            label="Email"
            value={state.email}
            onChangeText={(value) => dispatch({ type: 'SET_FIELD', field: 'email', value })}
            keyboardType="email-address"
            autoCapitalize="none"
          />

          <Input
            label="Password"
            value={state.password}
            onChangeText={(value) => dispatch({ type: 'SET_FIELD', field: 'password', value })}
            secureTextEntry
          />

          <Input
            label="Confirm Password"
            value={state.confirmPassword}
            onChangeText={(value) => dispatch({ type: 'SET_FIELD', field: 'confirmPassword', value })}
            secureTextEntry
          />

          <Button
            mode="contained"
            onPress={handleRegister}
            loading={state.isLoading}
            disabled={state.isLoading}
            style={styles.registerButton}
          >
            Register
          </Button>

        </View>
      </ScrollView>
    </KeyboardAvoidingView>
  );
};

const styles = StyleSheet.create({
  container: { flex: 1, backgroundColor: '#fff' },
  scrollContent: { flexGrow: 1 },
  content: { flex: 1, padding: 20, justifyContent: 'center' },
  title: { fontSize: 24, fontWeight: 'bold', marginBottom: 32, textAlign: 'center' },
  errorText: { textAlign: 'center', marginBottom: 16 },
  registerButton: { marginTop: 16, marginBottom: 8 },
});

export default RegisterScreen;