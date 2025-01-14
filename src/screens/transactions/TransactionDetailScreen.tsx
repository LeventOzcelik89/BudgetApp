import React, { useEffect, useState } from 'react';
import { View, ScrollView, StyleSheet } from 'react-native';
import { Card, Text, Button, useTheme } from 'react-native-paper';
import { useSelector } from 'react-redux';
import { format } from 'date-fns';
import { tr } from 'date-fns/locale';
import { Loading } from '../../components/common';
import { RootState } from '../../store';
import { Transaction, TransactionType } from '../../types';

export const TransactionDetailScreen = ({ route, navigation }) => {
  const { id } = route.params;
  const theme = useTheme();
  const { transactions } = useSelector((state: RootState) => state.transactions);
  const [transaction, setTransaction] = useState<Transaction | null>(null);

  useEffect(() => {
    const found = transactions.find(t => t.id === id);
    if (found) {
      setTransaction(found);
    }
  }, [id, transactions]);

  if (!transaction) return <Loading />;

  const isExpense = transaction.type === TransactionType.Expense;

  return (
    <ScrollView style={styles.container}>
      <Card style={styles.card}>
        <Card.Content>
          <Text style={styles.label}>Açıklama</Text>
          <Text style={styles.value}>{transaction.description}</Text>

          <Text style={styles.label}>Tutar</Text>
          <Text
            style={[
              styles.amount,
              { color: isExpense ? theme.colors.error : theme.colors.success },
            ]}
          >
            {isExpense ? '-' : '+'}
            {transaction.amount} {transaction.currencyCode}
          </Text>

          <Text style={styles.label}>Kategori</Text>
          <Text style={styles.value}>{transaction.categoryName || 'Kategorisiz'}</Text>

          <Text style={styles.label}>Tarih</Text>
          <Text style={styles.value}>
            {format(new Date(transaction.transactionDate), 'dd MMMM yyyy', { locale: tr })}
          </Text>

          {transaction.exchangeRate !== 1 && (
            <>
              <Text style={styles.label}>Orijinal Tutar</Text>
              <Text style={styles.value}>
                {transaction.originalAmount} {transaction.currencyCode}
              </Text>
              <Text style={styles.label}>Döviz Kuru</Text>
              <Text style={styles.value}>{transaction.exchangeRate}</Text>
            </>
          )}
        </Card.Content>
      </Card>

      <View style={styles.actions}>
        <Button
          mode="contained"
          onPress={() => navigation.navigate('EditTransaction', { id })}
          style={styles.button}
        >
          Düzenle
        </Button>
        <Button
          mode="outlined"
          onPress={() => navigation.goBack()}
          style={styles.button}
        >
          Geri
        </Button>
      </View>
    </ScrollView>
  );
};

const styles = StyleSheet.create({
  container: {
    flex: 1,
    padding: 16,
  },
  card: {
    marginBottom: 16,
  },
  label: {
    fontSize: 14,
    opacity: 0.6,
    marginTop: 16,
  },
  value: {
    fontSize: 16,
    marginTop: 4,
  },
  amount: {
    fontSize: 24,
    fontWeight: 'bold',
    marginTop: 4,
  },
  actions: {
    flexDirection: 'row',
    justifyContent: 'space-between',
  },
  button: {
    flex: 1,
    marginHorizontal: 8,
  },
});

export default TransactionDetailScreen; 