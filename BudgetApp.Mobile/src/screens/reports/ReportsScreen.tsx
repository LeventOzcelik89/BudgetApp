import React, { useEffect, useState } from 'react';
import { View, ScrollView, StyleSheet } from 'react-native';
import { Card, Text, SegmentedButtons, useTheme } from 'react-native-paper';
import { useDispatch, useSelector } from 'react-redux';
import { LineChart, PieChart } from 'react-native-chart-kit';
import { Dimensions } from 'react-native';
import { RootState } from '../../store';
import { fetchMonthlyReport, fetchYearlyReport } from '../../store/reportSlice';
import { Loading } from '../../components/common';

const screenWidth = Dimensions.get('window').width;

type ReportType = 'monthly' | 'yearly';

export const ReportsScreen = () => {
  const dispatch = useDispatch();
  const theme = useTheme();
  const [reportType, setReportType] = useState<ReportType>('monthly');
  const { monthlyReport, yearlyReport, isLoading } = useSelector(
    (state: RootState) => state.reports
  );

  useEffect(() => {
    if (reportType === 'monthly') {
      const date = new Date();
      dispatch(fetchMonthlyReport({ year: date.getFullYear(), month: date.getMonth() + 1 }));
    } else {
      dispatch(fetchYearlyReport(new Date().getFullYear()));
    }
  }, [dispatch, reportType]);

  if (isLoading) return <Loading />;

  const currentReport = reportType === 'monthly' ? monthlyReport : yearlyReport;

  return (
    <ScrollView style={styles.container}>
      <SegmentedButtons
        value={reportType}
        onValueChange={(value) => setReportType(value as ReportType)}
        buttons={[
          { value: 'monthly', label: 'Aylık' },
          { value: 'yearly', label: 'Yıllık' },
        ]}
        style={styles.segmentedButtons}
      />

      {/* Gelir/Gider Özeti */}
      <Card style={styles.card}>
        <Card.Content>
          <Text style={styles.cardTitle}>Özet</Text>
          <View style={styles.summaryContainer}>
            <View style={styles.summaryItem}>
              <Text style={styles.summaryLabel}>Gelir</Text>
              <Text style={[styles.summaryAmount, { color: theme.colors.success }]}>
                {currentReport?.totalIncome}₺
              </Text>
            </View>
            <View style={styles.summaryItem}>
              <Text style={styles.summaryLabel}>Gider</Text>
              <Text style={[styles.summaryAmount, { color: theme.colors.error }]}>
                {currentReport?.totalExpense}₺
              </Text>
            </View>
            <View style={styles.summaryItem}>
              <Text style={styles.summaryLabel}>Bakiye</Text>
              <Text style={[styles.summaryAmount, { 
                color: currentReport?.balance >= 0 ? theme.colors.success : theme.colors.error 
              }]}>
                {currentReport?.balance}₺
              </Text>
            </View>
          </View>
        </Card.Content>
      </Card>

      {/* Trend Grafiği */}
      <Card style={styles.card}>
        <Card.Content>
          <Text style={styles.cardTitle}>Trend</Text>
          <LineChart
            data={{
              labels: reportType === 'monthly' 
                ? ['1.Hafta', '2.Hafta', '3.Hafta', '4.Hafta']
                : ['Oca', 'Şub', 'Mar', 'Nis', 'May', 'Haz', 'Tem', 'Ağu', 'Eyl', 'Eki', 'Kas', 'Ara'],
              datasets: [
                {
                  data: currentReport?.trends || [],
                  color: (opacity = 1) => theme.colors.primary,
                },
              ],
            }}
            width={screenWidth - 48}
            height={220}
            chartConfig={{
              backgroundColor: theme.colors.surface,
              backgroundGradientFrom: theme.colors.surface,
              backgroundGradientTo: theme.colors.surface,
              decimalPlaces: 0,
              color: (opacity = 1) => theme.colors.primary,
              labelColor: (opacity = 1) => theme.colors.text,
              style: {
                borderRadius: 16,
              },
              propsForDots: {
                r: '6',
                strokeWidth: '2',
                stroke: theme.colors.primary,
              },
            }}
            bezier
            style={styles.chart}
          />
        </Card.Content>
      </Card>

      {/* Kategori Dağılımı */}
      <Card style={styles.card}>
        <Card.Content>
          <Text style={styles.cardTitle}>Kategori Dağılımı</Text>
          <PieChart
            data={currentReport?.categoryDistribution?.map(category => ({
              name: category.name,
              population: category.amount,
              color: category.color,
              legendFontColor: theme.colors.text,
              legendFontSize: 12,
            })) || []}
            width={screenWidth - 48}
            height={220}
            chartConfig={{
              color: (opacity = 1) => theme.colors.text,
            }}
            accessor="population"
            backgroundColor="transparent"
            paddingLeft="15"
            absolute
          />
        </Card.Content>
      </Card>
    </ScrollView>
  );
};

const styles = StyleSheet.create({
  container: {
    flex: 1,
    backgroundColor: '#f5f5f5',
  },
  segmentedButtons: {
    margin: 16,
  },
  card: {
    margin: 16,
    marginTop: 0,
    elevation: 2,
  },
  cardTitle: {
    fontSize: 18,
    fontWeight: 'bold',
    marginBottom: 16,
  },
  summaryContainer: {
    flexDirection: 'row',
    justifyContent: 'space-between',
    marginTop: 8,
  },
  summaryItem: {
    alignItems: 'center',
    flex: 1,
  },
  summaryLabel: {
    fontSize: 14,
    opacity: 0.7,
    marginBottom: 4,
  },
  summaryAmount: {
    fontSize: 18,
    fontWeight: 'bold',
  },
  chart: {
    marginVertical: 8,
    borderRadius: 16,
  },
});

export default ReportsScreen; 