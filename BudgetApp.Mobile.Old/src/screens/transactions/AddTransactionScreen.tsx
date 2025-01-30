import React, { useState } from 'react';
import { View, ScrollView, StyleSheet } from 'react-native';
import { TextInput, Button, SegmentedButtons, useTheme } from 'react-native-paper';
import { useDispatch, useSelector } from 'react-redux';
import DateTimePicker from '@react-native-community/datetimepicker';
import { createTransaction } from '../../store/transactionSlice';
import { RootState } from '../../store';
import { TransactionType } from '../../types';
import { CategorySelector } from '../../components/transaction/CategorySelector';

export const AddTransactionScreen = ({ navigation }) => {
  const dispatch = useDispatch();
  const theme = useTheme();
  const [amount, setAmount] = useState('');
  const [description, setDescription] = useState('');
  const [type, setType] = useState<TransactionType>(TransactionType.Expense);
  const [categoryId, setCategoryId] = useState<number | null>(null);
  const [date, setDate] = useState(new Date());

  const handleSubmit = async () => {
    try {
      await dispatch(createTransaction({
        amount: parseFloat(amount),
        description,
        type,
        categoryId,
        transactionDate: date.toISOString(),
      })).unwrap();
      navigation.goBack();
    } catch (error) {
      console.error('Error creating transaction:', error);
    }
  };

  return (
    <ScrollView style={styles.container}>
      <SegmentedButtons
        value={type.toString()}
        onValueChange={(value) => setType(parseInt(value))}
        buttons={[
          { value: TransactionType.Expense.toString(), label: 'Gider' },
          { value: TransactionType.Income.toString(), label: 'Gelir' },
        ]}
        style={styles.segmentedButtons}
      />

      <TextInput
        label="Tutar"
        value={amount}
        onChangeText={setAmount}
        keyboardType="numeric"
        style={styles.input}
      />

      <TextInput
        label="Açıklama"
        value={description}
        onChangeText={setDescription}
        style={styles.input}
      />

      <CategorySelector
        selectedCategoryId={categoryId}
        onSelect={setCategoryId}
        transactionType={type}
      />

      <DateTimePicker
        value={date}
        mode="date"
        display="default"
        onChange={(event, selectedDate) => {
          setDate(selectedDate || date);
        }}
      />

      <Button
        mode="contained"
        onPress={handleSubmit}
        style={styles.button}
        disabled={!amount || !description}
      >
        Kaydet
      </Button>
    </ScrollView>
  );
};

const styles = StyleSheet.create({
  container: {
    flex: 1,
    padding: 16,
  },
  segmentedButtons: {
    marginBottom: 16,
  },
  input: {
    marginBottom: 16,
  },
  button: {
    marginTop: 16,
  },
});

export default AddTransactionScreen; 