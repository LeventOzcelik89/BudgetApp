import React from 'react';
import { View, StyleSheet } from 'react-native';
import { ActivityIndicator, Text } from 'react-native-paper';

export const Loading = () => {
  console.log('Loading component rendered');
  return (
    <View style={styles.container}>
      <ActivityIndicator animating={true} size="large" />
      <Text style={styles.text}>Yükleniyor...</Text>
    </View>
  );
};

const styles = StyleSheet.create({
  container: {
    flex: 1,
    justifyContent: 'center',
    alignItems: 'center',
    backgroundColor: '#fff',
  },
  text: {
    marginTop: 16,
    fontSize: 16,
  },
}); 