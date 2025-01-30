import React from 'react';
import { StyleSheet } from 'react-native';
import { Button as PaperButton } from 'react-native-paper';

export const Button = ({
  mode = 'contained',
  onPress,
  children,
  loading = false,
  disabled = false,
  style,
}) => {
  return (
    <PaperButton
      mode={mode}
      onPress={onPress}
      loading={loading}
      disabled={disabled}
      style={[styles.button, style]}
    >
      {children}
    </PaperButton>
  );
};

const styles = StyleSheet.create({
  button: {
    marginVertical: 8,
    paddingVertical: 6,
  },
});

export default Button;
