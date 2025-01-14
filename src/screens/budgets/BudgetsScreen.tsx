import React, { useEffect } from 'react';
import { View, ScrollView, StyleSheet } from 'react-native';
import { Card, Text, ProgressBar, FAB, useTheme } from 'react-native-paper';
import { useDispatch, useSelector } from 'react-redux';
import { RootState } from '../../store';
import { fetchBudgets } from '../../store/budgetSlice';
import { Loading } from '../../components/common';

export const BudgetsScreen = ({ navigation }) => {
  const dispatch = useDispatch();
  const theme = useTheme();
  const { budgets, isLoading } = useSelector((state: RootState) => state.budgets);

  useEffect(() => {
    dispatch(fetchBudgets());
  }, [dispatch]);

  if (isLoading) return <Loading />;

  const totalBudget = budgets?.reduce((sum, budget) => sum + budget.plannedAmount, 0) || 0;
  const totalSpent = budgets?.reduce((sum, budget) => sum + budget.spentAmount, 0) || 0;
  const totalProgress = totalBudget > 0 ? (totalSpent / totalBudget) * 100 : 0;

  return (
    <View style={styles.container}>
      <ScrollView>
        {/* Toplam Bütçe Özeti */}
        <Card style={styles.summaryCard}>
          <Card.Content>
            <Text style={styles.summaryTitle}>Toplam Bütçe Durumu</Text>
            <View style={styles.amountRow}>
              <Text style={styles.totalAmount}>{totalSpent}₺</Text>
              <Text style={styles.totalBudget}>/ {totalBudget}₺</Text>
            </View>
            <ProgressBar
              progress={totalProgress / 100}
              color={totalProgress > 100 ? theme.colors.error : theme.colors.primary}
              style={styles.totalProgress}
            />
            <Text style={[
              styles.percentageText,
              { color: totalProgress > 100 ? theme.colors.error : theme.colors.text }
            ]}>
              {totalProgress.toFixed(1)}%
            </Text>
          </Card.Content>
        </Card>

        {/* Kategori Bazlı Bütçeler */}
        {budgets?.map((budget) => (
          <Card
            key={budget.id}
            style={styles.budgetCard}
            onPress={() => navigation.navigate('BudgetDetail', { id: budget.id })}
          >
            <Card.Content>
              <View style={styles.budgetHeader}>
                <Text style={styles.categoryName}>{budget.categoryName}</Text>
                <Text style={[
                  styles.percentageText,
                  { color: budget.spentPercentage > 100 ? theme.colors.error : theme.colors.text }
                ]}>
                  {budget.spentPercentage.toFixed(1)}%
                </Text>
              </View>

              <ProgressBar
                progress={budget.spentPercentage / 100}
                color={budget.spentPercentage > 100 ? theme.colors.error : theme.colors.primary}
                style={styles.progressBar}
              />

              <View style={styles.amountRow}>
                <Text style={styles.spentAmount}>{budget.spentAmount}₺</Text>
                <Text style={styles.plannedAmount}>/ {budget.plannedAmount}₺</Text>
              </View>

              <Text style={styles.dateRange}>
                {new Date(budget.startDate).toLocaleDateString('tr-TR')} - 
                {new Date(budget.endDate).toLocaleDateString('tr-TR')}
              </Text>
            </Card.Content>
          </Card>
        ))}
      </ScrollView>

      <FAB
        style={styles.fab}
        icon="plus"
        onPress={() => navigation.navigate('AddBudget')}
      />
    </View>
  );
};

const styles = StyleSheet.create({
  container: {
    flex: 1,
    backgroundColor: '#f5f5f5',
  },
  summaryCard: {
    margin: 16,
    elevation: 4,
  },
  summaryTitle: {
    fontSize: 18,
    fontWeight: 'bold',
    marginBottom: 12,
  },
  budgetCard: {
    marginHorizontal: 16,
    marginBottom: 12,
    elevation: 2,
  },
  budgetHeader: {
    flexDirection: 'row',
    justifyContent: 'space-between',
    alignItems: 'center',
    marginBottom: 8,
  },
  categoryName: {
    fontSize: 16,
    fontWeight: '500',
  },
  progressBar: {
    height: 8,
    borderRadius: 4,
    marginVertical: 8,
  },
  totalProgress: {
    height: 12,
    borderRadius: 6,
    marginVertical: 12,
  },
  amountRow: {
    flexDirection: 'row',
    alignItems: 'baseline',
    marginTop: 4,
  },
  spentAmount: {
    fontSize: 16,
    fontWeight: '500',
  },
  plannedAmount: {
    fontSize: 14,
    opacity: 0.7,
    marginLeft: 4,
  },
  totalAmount: {
    fontSize: 24,
    fontWeight: 'bold',
  },
  totalBudget: {
    fontSize: 18,
    opacity: 0.7,
    marginLeft: 8,
  },
  percentageText: {
    fontSize: 14,
    fontWeight: '500',
  },
  dateRange: {
    fontSize: 12,
    opacity: 0.6,
    marginTop: 8,
  },
  fab: {
    position: 'absolute',
    margin: 16,
    right: 0,
    bottom: 0,
    backgroundColor: theme.colors.primary,
  },
});

export default BudgetsScreen; 