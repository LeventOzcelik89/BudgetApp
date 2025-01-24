import React, { useState } from 'react';
import { View, ScrollView, StyleSheet } from 'react-native';
import { TextInput, Button } from 'react-native-paper';
import { useDispatch } from 'react-redux';
import DateTimePicker from '@react-native-community/datetimepicker';
import { createBudget } from '../../store/budgetSlice';
import { CategorySelector } from '../../components/budget/CategorySelector';

export const AddBudgetScreen = ({ navigation }) => {
  const dispatch = useDispatch();
  const [amount, setAmount] = useState('');
  const [categoryId, setCategoryId] = useState<number | null>(null);
  const [startDate, setStartDate] = useState(new Date());
  const [endDate, setEndDate] = useState(new Date());

  const handleSubmit = async () => {
    try {
      await dispatch(createBudget({
        plannedAmount: parseFloat(amount),
        categoryId,
        startDate: startDate.toISOString(),
        endDate: endDate.toISOString(),
      })).unwrap();
      navigation.goBack();
    } catch (error) {
      console.error('Error creating budget:', error);
    }
  };

  return (
    <ScrollView style={styles.container}>
      <TextInput
        label="Planlanan Tutar"
        value={amount}
        onChangeText={setAmount}
        keyboardType="numeric"
        style={styles.input}
      />

      <CategorySelector
        selectedCategoryId={categoryId}
        onSelect={setCategoryId}
      />

      <View style={styles.dateContainer}>
        <DateTimePicker
          value={startDate}
          mode="date"
          display="default"
          onChange={(event, selectedDate) => {
            setStartDate(selectedDate || startDate);
          }}
        />

        <DateTimePicker
          value={endDate}
          mode="date"
          display="default"
          onChange={(event, selectedDate) => {
            setEndDate(selectedDate || endDate);
          }}
        />
      </View>

      <Button
        mode="contained"
        onPress={handleSubmit}
        style={styles.button}
        disabled={!amount || !categoryId}
      >
        Bütçe Oluştur
      </Button>
    </ScrollView>
  );
};

const styles = StyleSheet.create({
  container: {
    flex: 1,
    padding: 16,
  },
  input: {
    marginBottom: 16,
  },
  dateContainer: {
    marginVertical: 16,
  },
  button: {
    marginTop: 16,
  },
});

export default AddBudgetScreen; 