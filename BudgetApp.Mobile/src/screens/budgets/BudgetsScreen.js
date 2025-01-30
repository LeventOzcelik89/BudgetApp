import React, { useEffect, useReducer } from 'react';
import { View, ScrollView, StyleSheet } from 'react-native';
import { Card, Text, ProgressBar, FAB, useTheme } from 'react-native-paper';
import { Loading } from '../../components/common';
import { budgetApi } from '../../services/budget';

const initialState = {
  data: null,
  isLoading: true,
  error: null,
};

const reducer = (state, action) => {
  switch (action.type) {
    case 'FETCH_SUCCESS':
      return { ...state, data: action.payload, isLoading: false };
    case 'FETCH_ERROR':
      return { ...state, error: action.payload, isLoading: false };
    default:
      return state;
  }
};

const fetchBudgets = async (dispatch) => {
  try {

    try {

      let month = Math.floor(Math.random() * 12) + 1;
      let day = Math.floor(Math.random() * 27) + 1;

      month = month < 10 ? "0" + month : month;
      day = day < 10 ? "0" + day : day;

      let d1 = "2025-02-" + day + "T10:00:00.000Z";
      let d2 = "2025-02-" + day + "T22:00:00.000Z";

      let _data = {
        "categoryId": 1,
        "plannedAmount": 2000,
        "startDate": d1,
        "endDate": d2
      };

      const result = await budgetApi.createBudget(_data);
      console.log(result);

    } catch (error) {
      console.log('Budget Create Ex:' + error);
    }

    // try {

    //   const resp = await budgetApi.getBudgets();
    //   console.log(resp);
    //   //  dispatch({ type: 'FETCH_SUCCESS', payload: resp.data });

    // } catch (error) {
    //   console.log('Budget List Ex:' + error);
    // }

    try {

      let resp = await budgetApi.getSummary('2025-02-03T14:00:00');
      console.log(resp);
      debugger;
      //  dispatch({ type: 'FETCH_SUCCESS', payload: resp.data });
      dispatch({ type: 'FETCH_SUCCESS', payload: resp.data });
    } catch (error) {
      console.log('Budget Summary Ex:' + error);
    }

    try {

    } catch (error) {

    }

  } catch (error) {
    console.log(error);
    dispatch({ type: 'FETCH_ERROR', payload: error.message });
  }
};

export const BudgetsScreen = ({ navigation }) => {
  const [state, dispatch] = useReducer(reducer, initialState);
  const theme = useTheme();

  useEffect(() => {
    fetchBudgets(dispatch);
  }, []);

  if (state.isLoading) return <Loading />;

  const totalBudget = state.data.budgets?.reduce((sum, budget) => sum + budget.plannedAmount, 0) || 0;
  const totalSpent = state.data.totalSpentAmount;//state.budgets?.reduce((sum, budget) => sum + budget.spentAmount, 0) || 0;
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
        {state.data.budgets?.map((budget) => (
          <Card
            key={budget.id}
            style={styles.budgetCard}
            onPress={() => navigation.navigate('BudgetDetailScreen', { id: budget.id })}
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
    backgroundColor: '#6200ee',
  },
});

export default BudgetsScreen;