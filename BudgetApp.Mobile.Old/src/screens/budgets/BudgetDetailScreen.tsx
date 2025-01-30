import React, { useEffect, useState } from 'react';
import { View, ScrollView, StyleSheet } from 'react-native';
import { Card, Text, Button, ProgressBar, useTheme } from 'react-native-paper';
import { useSelector } from 'react-redux';
import { format } from 'date-fns';
import { tr } from 'date-fns/locale';
import { Loading } from '../../components/common';
import { RootState } from '../../store';
import { Budget } from '../../types';

export const BudgetDetailScreen = ({ route, navigation }) => {
  const { id } = route.params;
  const theme = useTheme();
  const { budgets } = useSelector((state: RootState) => state.budgets);
  const [budget, setBudget] = useState<Budget | null>(null);

  useEffect(() => {
    const found = budgets.find(b => b.id === id);
    if (found) {
      setBudget(found);
    }
  }, [id, budgets]);

  if (!budget) return <Loading />;

  const isOverBudget = budget.spentPercentage > 100;

  return (
    <ScrollView style={styles.container}>
      <Card style={styles.card}>
        <Card.Content>
          <Text style={styles.categoryName}>{budget.categoryName}</Text>
          
          <View style={styles.amountContainer}>
            <View style={styles.amountBox}>
              <Text style={styles.label}>Planlanan</Text>
              <Text style={styles.amount}>{budget.plannedAmount}₺</Text>
            </View>
            <View style={styles.amountBox}>
              <Text style={styles.label}>Harcanan</Text>
              <Text style={[styles.amount, isOverBudget && styles.overBudget]}>
                {budget.spentAmount}₺
              </Text>
            </View>
          </View>

          <View style={styles.progressContainer}>
            <ProgressBar
              progress={budget.spentPercentage / 100}
              color={isOverBudget ? theme.colors.error : theme.colors.primary}
              style={styles.progressBar}
            />
            <Text style={[styles.percentage, isOverBudget && styles.overBudget]}>
              {budget.spentPercentage}%
            </Text>
          </View>

          <View style={styles.dateContainer}>
            <Text style={styles.label}>Başlangıç Tarihi</Text>
            <Text style={styles.value}>
              {format(new Date(budget.startDate), 'dd MMMM yyyy', { locale: tr })}
            </Text>
            <Text style={styles.label}>Bitiş Tarihi</Text>
            <Text style={styles.value}>
              {format(new Date(budget.endDate), 'dd MMMM yyyy', { locale: tr })}
            </Text>
          </View>
        </Card.Content>
      </Card>

      <View style={styles.actions}>
        <Button
          mode="contained"
          onPress={() => navigation.navigate('EditBudget', { id })}
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
  categoryName: {
    fontSize: 24,
    fontWeight: 'bold',
    marginBottom: 16,
  },
  amountContainer: {
    flexDirection: 'row',
    justifyContent: 'space-between',
    marginBottom: 16,
  },
  amountBox: {
    flex: 1,
    alignItems: 'center',
  },
  label: {
    fontSize: 14,
    opacity: 0.6,
  },
  amount: {
    fontSize: 20,
    fontWeight: 'bold',
    marginTop: 4,
  },
  overBudget: {
    color: 'red',
  },
  progressContainer: {
    marginBottom: 16,
  },
  progressBar: {
    height: 8,
    borderRadius: 4,
  },
  percentage: {
    textAlign: 'right',
    marginTop: 4,
    fontWeight: 'bold',
  },
  dateContainer: {
    marginTop: 16,
  },
  value: {
    fontSize: 16,
    marginTop: 4,
    marginBottom: 16,
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

export default BudgetDetailScreen; 