import React, { useState } from 'react';
import { View, StyleSheet } from 'react-native';
import { List, Modal, Portal, RadioButton, Text } from 'react-native-paper';
import { CurrencyInfo } from '../../types';

interface CurrencySelectorProps {
  value: string;
  onSelect: (currency: string) => void;
}

const currencies: CurrencyInfo[] = [
  { code: 'TRY', name: 'Türk Lirası', symbol: '₺', flag: '🇹🇷' },
  { code: 'USD', name: 'Amerikan Doları', symbol: '$', flag: '🇺🇸' },
  { code: 'EUR', name: 'Euro', symbol: '€', flag: '🇪🇺' },
  { code: 'GBP', name: 'İngiliz Sterlini', symbol: '£', flag: '🇬🇧' },
];

export const CurrencySelector = ({ value, onSelect }: CurrencySelectorProps) => {
  const [visible, setVisible] = useState(false);
  const selectedCurrency = currencies.find(c => c.code === value);

  return (
    <>
      <List.Item
        title="Para Birimi"
        description={`${selectedCurrency?.flag} ${selectedCurrency?.name} (${selectedCurrency?.symbol})`}
        onPress={() => setVisible(true)}
        right={props => <List.Icon {...props} icon="chevron-right" />}
      />

      <Portal>
        <Modal
          visible={visible}
          onDismiss={() => setVisible(false)}
          contentContainerStyle={styles.modal}
        >
          <Text style={styles.title}>Para Birimi Seçin</Text>
          <RadioButton.Group onValueChange={value => {
            onSelect(value);
            setVisible(false);
          }} value={value}>
            {currencies.map(currency => (
              <RadioButton.Item
                key={currency.code}
                label={`${currency.flag} ${currency.name} (${currency.symbol})`}
                value={currency.code}
              />
            ))}
          </RadioButton.Group>
        </Modal>
      </Portal>
    </>
  );
};

const styles = StyleSheet.create({
  modal: {
    backgroundColor: 'white',
    padding: 20,
    margin: 20,
    borderRadius: 8,
  },
  title: {
    fontSize: 18,
    fontWeight: 'bold',
    marginBottom: 16,
  },
}); 