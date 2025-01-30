import React from 'react';
import { StyleSheet } from 'react-native';
import { Card, Text } from 'react-native-paper';
import { format } from 'date-fns';
import { tr } from 'date-fns/locale';
import { Transaction, TransactionType } from '../../types';
import { theme } from '../../theme';

interface TransactionCardProps {
  transaction: Transaction;
  onPress: () => void;
}

export const TransactionCard = ({ transaction, onPress }: TransactionCardProps) => {
  const isExpense = transaction.type === TransactionType.Expense;

  return (
    <Card style={styles.card} onPress={onPress}>
      <Card.Content style={styles.content}>
        <Text style={styles.description}>{transaction.description}</Text>
        <Text
          style={[
            styles.amount,
            { color: isExpense ? theme.colors.error : theme.colors.success },
          ]}
        >
          {isExpense ? '-' : '+'}
          {transaction.amount}
          {transaction.currencyCode}
        </Text>
        <Text style={styles.category}>{transaction.categoryName || 'Kategorisiz'}</Text>
        <Text style={styles.date}>
          {format(new Date(transaction.transactionDate), 'dd MMMM yyyy', { locale: tr })}
        </Text>
      </Card.Content>
    </Card>
  );
};

const styles = StyleSheet.create({
  card: {
    marginHorizontal: theme.spacing.m,
    marginVertical: theme.spacing.s,
  },
  content: {
    flexDirection: 'row',
    alignItems: 'center',
    justifyContent: 'space-between',
  },
  description: {
    flex: 1,
    marginRight: theme.spacing.s,
  },
  amount: {
    fontSize: 16,
    fontWeight: 'bold',
  },
  category: {
    fontSize: 12,
    color: theme.colors.text,
    opacity: 0.7,
  },
  date: {
    fontSize: 12,
    color: theme.colors.text,
    opacity: 0.5,
  },
}); 