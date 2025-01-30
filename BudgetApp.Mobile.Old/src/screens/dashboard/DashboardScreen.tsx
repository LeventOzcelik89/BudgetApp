import React, { useEffect } from 'react';
import { View, ScrollView, StyleSheet } from 'react-native';
import { Card, Text, useTheme } from 'react-native-paper';
import { useDispatch, useSelector } from 'react-redux';
import { LineChart, PieChart } from 'react-native-chart-kit';
import { Dimensions } from 'react-native';
import { RootState } from '../../store';
import { fetchDashboardData } from '../../store/dashboardSlice';
import { Loading } from '../../components/common';

const screenWidth = Dimensions.get('window').width;

export const DashboardScreen = () => {
  const dispatch = useDispatch();
  const theme = useTheme();
  const { data, isLoading } = useSelector((state: RootState) => state.dashboard);

  useEffect(() => {
    dispatch(fetchDashboardData());
  }, [dispatch]);

  if (isLoading) return <Loading />;

  return (
    <ScrollView style={styles.container}>
      {/* Özet Kartları */}
      <View style={styles.summaryCards}>
        <Card style={[styles.card, styles.incomeCard]}>
          <Card.Content>
            <Text style={styles.cardLabel}>Toplam Gelir</Text>
            <Text style={[styles.amount, { color: theme.colors.success }]}>
              {data?.totalIncome}₺
            </Text>
          </Card.Content>
        </Card>

        <Card style={[styles.card, styles.expenseCard]}>
          <Card.Content>
            <Text style={styles.cardLabel}>Toplam Gider</Text>
            <Text style={[styles.amount, { color: theme.colors.error }]}>
              {data?.totalExpense}₺
            </Text>
          </Card.Content>
        </Card>
      </View>

      {/* Aylık Trend Grafiği */}
      <Card style={styles.chartCard}>
        <Card.Content>
          <Text style={styles.chartTitle}>Aylık Trend</Text>
          <LineChart
            data={data?.weeklyTrends.map((value, index) => ({ x: index + 1, y: value }))}
            width={screenWidth - 32}
            height={220}
            chartConfig={{
              backgroundColor: '#e26a00',
              backgroundGradientFrom: '#fb8c00',
              backgroundGradientTo: '#ffa726',
              decimalPlaces: 2,
              color: (opacity = 1) => `rgba(255, 255, 255, ${opacity})`,
              style: {
                borderRadius: 16,
              },
            }}
            bezier
          />
        </Card.Content>
      </Card>

      {/* Kategori Dağılımı */}
      <Card style={styles.chartCard}>
        <Card.Content>
          <Text style={styles.chartTitle}>Kategori Dağılımı</Text>
          <PieChart
            data={data?.categoryDistribution?.map(item => ({
              name: item.name,
              amount: item.amount,
              color: item.color,
              legendFontColor: '#7F7F7F',
              legendFontSize: 15,
              x: 10,
              y: 10,
              padding: 10,
              paddingRight: 30,
              chartConfig: {
                color: (opacity = 1) => `rgba(255, 255, 255, ${opacity})`,
                labelColor: (opacity = 1) => `rgba(255, 255, 255, ${opacity})`,
                style: {
                  borderRadius: 16,
                  padding: 8,
                },
                propsForDots: {
                  r: '6',
                  strokeWidth: '2',
                  stroke: '#ffa726',
                },
              },
            }))}
            width={screenWidth - 32}
            height={220}
            chartConfig={{
              backgroundColor: '#e26a00',
              backgroundGradientFrom: '#fb8c00',
              backgroundGradientTo: '#ffa726',
              decimalPlaces: 2,
              color: (opacity = 1) => `rgba(255, 255, 255, ${opacity})`,
              style: {
                borderRadius: 16,
              },
            }}
            accessor="amount"
            backgroundColor="transparent"
            paddingLeft="15"
            center={[10, 50]}
            absolute
          />
        </Card.Content>
      </Card>

      {/* Son İşlemler */}
      <Card style={styles.transactionsCard}>
        <Card.Content>
          <Text style={styles.sectionTitle}>Son İşlemler</Text>
          {data?.recentTransactions?.map((transaction) => (
            <View key={transaction.id} style={styles.transactionItem}>
              <View>
                <Text style={styles.transactionDescription}>
                  {transaction.description}
                </Text>
                <Text style={styles.transactionCategory}>
                  {transaction.categoryName}
                </Text>
              </View>
              <Text
                style={[
                  styles.transactionAmount,
                  { color: transaction.type === 'expense' ? theme.colors.error : theme.colors.success }
                ]}
              >
                {transaction.type === 'expense' ? '-' : '+'}
                {transaction.amount}₺
              </Text>
            </View>
          ))}
        </Card.Content>
      </Card>
    </ScrollView>
  );
};

const styles = StyleSheet.create({
  container: {
    flex: 1,
    padding: 16,
    backgroundColor: '#f5f5f5',
  },
  summaryCards: {
    flexDirection: 'row',
    justifyContent: 'space-between',
    marginBottom: 16,
  },
  card: {
    flex: 1,
    marginHorizontal: 4,
  },
  incomeCard: {
    backgroundColor: '#e8f5e9',
  },
  expenseCard: {
    backgroundColor: '#ffebee',
  },
  cardLabel: {
    fontSize: 14,
    opacity: 0.8,
  },
  amount: {
    fontSize: 20,
    fontWeight: 'bold',
    marginTop: 4,
  },
  chartCard: {
    marginBottom: 16,
  },
  chartTitle: {
    fontSize: 16,
    fontWeight: 'bold',
    marginBottom: 16,
  },
  chart: {
    marginVertical: 8,
    borderRadius: 16,
  },
  transactionsCard: {
    marginBottom: 16,
  },
  sectionTitle: {
    fontSize: 16,
    fontWeight: 'bold',
    marginBottom: 12,
  },
  transactionItem: {
    flexDirection: 'row',
    justifyContent: 'space-between',
    alignItems: 'center',
    paddingVertical: 12,
    borderBottomWidth: 1,
    borderBottomColor: '#eee',
  },
  transactionDescription: {
    fontSize: 14,
    fontWeight: '500',
  },
  transactionCategory: {
    fontSize: 12,
    opacity: 0.6,
    marginTop: 2,
  },
  transactionAmount: {
    fontSize: 14,
    fontWeight: 'bold',
  },
});

export default DashboardScreen; 