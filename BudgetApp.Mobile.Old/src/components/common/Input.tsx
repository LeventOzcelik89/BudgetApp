import React from 'react';
import { StyleSheet } from 'react-native';
import { TextInput } from 'react-native-paper';
import { theme } from '../../theme';

interface InputProps {
  label: string;
  value: string;
  onChangeText: (text: string) => void;
  error?: string;
  secureTextEntry?: boolean;
  keyboardType?: 'default' | 'email-address' | 'numeric' | 'phone-pad';
  autoCapitalize?: 'none' | 'sentences' | 'words' | 'characters';
  disabled?: boolean;
}

export const Input = ({
  label,
  value,
  onChangeText,
  error,
  secureTextEntry = false,
  keyboardType = 'default',
  autoCapitalize = 'none',
  disabled = false,
}: InputProps) => {
  return (
    <TextInput
      label={label}
      value={value}
      onChangeText={onChangeText}
      error={!!error}
      secureTextEntry={secureTextEntry}
      keyboardType={keyboardType}
      autoCapitalize={autoCapitalize}
      disabled={disabled}
      mode="outlined"
      style={styles.input}
    />
  );
};

const styles = StyleSheet.create({
  input: {
    marginBottom: 16,
    backgroundColor: theme.colors.surface,
  },
}); 