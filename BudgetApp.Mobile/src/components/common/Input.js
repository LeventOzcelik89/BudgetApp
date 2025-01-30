import React from 'react';
import { StyleSheet } from 'react-native';
import { TextInput } from 'react-native-paper';

const Input = ({
  label,
  value,
  onChangeText,
  error,
  secureTextEntry = false,
  keyboardType = 'default',
  autoCapitalize = 'none',
  disabled = false,
}) => {
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
  },
});

export default Input;