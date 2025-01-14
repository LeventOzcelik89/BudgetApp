import React from 'react';
import { StyleSheet } from 'react-native';
import { Button as PaperButton } from 'react-native-paper';
import { theme } from '../../theme';

interface ButtonProps {
  mode?: 'text' | 'outlined' | 'contained';
  onPress: () => void;
  style?: object;
  children: React.ReactNode;
  loading?: boolean;
  disabled?: boolean;
}

export const Button = ({
  mode = 'contained',
  onPress,
  style,
  children,
  loading = false,
  disabled = false,
}: ButtonProps) => {
  return (
    <PaperButton
      mode={mode}
      onPress={onPress}
      style={[styles.button, style]}
      loading={loading}
      disabled={disabled}
    >
      {children}
    </PaperButton>
  );
};

const styles = StyleSheet.create({
  button: {
    marginVertical: 10,
    paddingVertical: 2,
    backgroundColor: theme.colors.primary,
  },
}); 