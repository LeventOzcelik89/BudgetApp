import React, { useState } from 'react';
import { View, StyleSheet } from 'react-native';
import { TextInput, Button, SegmentedButtons } from 'react-native-paper';
import { useDispatch } from 'react-redux';
import { createCategory } from '../../store/categorySlice';
import { CategoryType } from '../../types';

export const AddCategoryScreen = ({ navigation }) => {
  const dispatch = useDispatch();
  const [name, setName] = useState('');
  const [description, setDescription] = useState('');
  const [type, setType] = useState<CategoryType>(CategoryType.Expense);

  const handleSubmit = async () => {
    try {
      await dispatch(createCategory({
        name,
        description,
        type,
      })).unwrap();
      navigation.goBack();
    } catch (error) {
      console.error('Error creating category:', error);
    }
  };

  return (
    <View style={styles.container}>
      <TextInput
        label="Kategori Adı"
        value={name}
        onChangeText={setName}
        style={styles.input}
      />

      <TextInput
        label="Açıklama"
        value={description}
        onChangeText={setDescription}
        style={styles.input}
      />

      <SegmentedButtons
        value={type.toString()}
        onValueChange={(value) => setType(parseInt(value))}
        buttons={[
          { value: CategoryType.Expense.toString(), label: 'Gider' },
          { value: CategoryType.Income.toString(), label: 'Gelir' },
          { value: CategoryType.Both.toString(), label: 'Her İkisi' },
        ]}
        style={styles.segmentedButtons}
      />

      <Button
        mode="contained"
        onPress={handleSubmit}
        style={styles.button}
        disabled={!name}
      >
        Kategori Oluştur
      </Button>
    </View>
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
  segmentedButtons: {
    marginBottom: 16,
  },
  button: {
    marginTop: 16,
  },
});

export default AddCategoryScreen; 