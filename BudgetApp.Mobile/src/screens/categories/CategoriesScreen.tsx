import React, { useEffect } from 'react';
import { View, FlatList, StyleSheet } from 'react-native';
import { FAB, List, IconButton, useTheme } from 'react-native-paper';
import { useDispatch, useSelector } from 'react-redux';
import { Loading } from '../../components/common';
import { RootState } from '../../store';
import { fetchCategories, deleteCategory } from '../../store/categorySlice';
import { CategoryType } from '../../types';

export const CategoriesScreen = ({ navigation }) => {
  const dispatch = useDispatch();
  const { categories, isLoading } = useSelector((state: RootState) => state.categories);
  const theme = useTheme();

  useEffect(() => {
    dispatch(fetchCategories());
  }, [dispatch]);

  const getCategoryIcon = (type: CategoryType) => {
    switch (type) {
      case CategoryType.Income:
        return 'arrow-up-circle';
      case CategoryType.Expense:
        return 'arrow-down-circle';
      default:
        return 'swap-horizontal-circle';
    }
  };

  const handleDelete = (id: number) => {
    dispatch(deleteCategory(id));
  };

  if (isLoading) return <Loading />;

  return (
    <View style={styles.container}>
      <FlatList
        data={categories}
        keyExtractor={(item) => item.id.toString()}
        renderItem={({ item }) => (
          <List.Item
            title={item.name}
            description={item.description}
            left={(props) => (
              <List.Icon
                {...props}
                icon={getCategoryIcon(item.type)}
                color={
                  item.type === CategoryType.Income
                    ? theme.colors.success
                    : theme.colors.error
                }
              />
            )}
            right={(props) => (
              <View style={styles.actions}>
                <IconButton
                  icon="pencil"
                  onPress={() => navigation.navigate('EditCategory', { id: item.id })}
                />
                <IconButton
                  icon="delete"
                  onPress={() => handleDelete(item.id)}
                />
              </View>
            )}
          />
        )}
      />
      <FAB
        style={styles.fab}
        icon="plus"
        onPress={() => navigation.navigate('AddCategory')}
      />
    </View>
  );
};

const styles = StyleSheet.create({
  container: {
    flex: 1,
  },
  actions: {
    flexDirection: 'row',
  },
  fab: {
    position: 'absolute',
    margin: 16,
    right: 0,
    bottom: 0,
  },
});

export default CategoriesScreen; 