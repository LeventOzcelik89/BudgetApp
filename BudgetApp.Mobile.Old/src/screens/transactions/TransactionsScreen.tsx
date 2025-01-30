import React, { useEffect, useState } from 'react';
import { View, FlatList, StyleSheet } from 'react-native';
import { Searchbar, FAB, Chip, Portal, Modal } from 'react-native-paper';
import { useDispatch, useSelector } from 'react-redux';
import { TransactionCard } from '../../components/transaction/TransactionCard';
import { Loading } from '../../components/common';
import { theme } from '../../theme';
import { RootState } from '../../store';
import { fetchTransactions } from '../../store/transactionSlice';
import { Transaction, TransactionType } from '../../types';

export const TransactionsScreen = ({ navigation }) => {
  const dispatch = useDispatch();
  const { transactions, isLoading } = useSelector((state: RootState) => state.transactions);
  const [searchQuery, setSearchQuery] = useState('');
  const [selectedType, setSelectedType] = useState<TransactionType | null>(null);
  const [showFilters, setShowFilters] = useState(false);

  useEffect(() => {
    dispatch(fetchTransactions());
  }, [dispatch]);

  const filteredTransactions = transactions?.filter((transaction: Transaction) => {
    const matchesSearch = transaction.description.toLowerCase().includes(searchQuery.toLowerCase());
    const matchesType = selectedType ? transaction.type === selectedType : true;
    return matchesSearch && matchesType;
  });

  const renderHeader = () => (
    <View style={styles.header}>
      <View style={styles.searchContainer}>
        <Searchbar
          placeholder="İşlem ara..."
          onChangeText={setSearchQuery}
          value={searchQuery}
          style={styles.searchbar}
        />
      </View>
      <View style={styles.filterChips}>
        <Chip
          selected={selectedType === TransactionType.Income}
          onPress={() => setSelectedType(
            selectedType === TransactionType.Income ? null : TransactionType.Income
          )}
          style={styles.chip}
        >
          Gelir
        </Chip>
        <Chip
          selected={selectedType === TransactionType.Expense}
          onPress={() => setSelectedType(
            selectedType === TransactionType.Expense ? null : TransactionType.Expense
          )}
          style={styles.chip}
        >
          Gider
        </Chip>
      </View>
    </View>
  );

  if (isLoading) return <Loading />;

  return (
    <View style={styles.container}>
      {renderHeader()}
      
      <FlatList
        data={filteredTransactions}
        keyExtractor={(item) => item.id.toString()}
        renderItem={({ item }) => (
          <TransactionCard
            transaction={item}
            onPress={() => navigation.navigate('TransactionDetail', { id: item.id })}
          />
        )}
      />

      <FAB
        style={styles.fab}
        icon="plus"
        onPress={() => navigation.navigate('AddTransaction')}
      />
    </View>
  );
};

const styles = StyleSheet.create({
  container: {
    flex: 1,
    backgroundColor: theme.colors.background,
  },
  header: {
    padding: 16,
    backgroundColor: theme.colors.surface,
    elevation: 4,
  },
  searchContainer: {
    flexDirection: 'row',
    marginBottom: 8,
  },
  searchbar: {
    flex: 1,
  },
  filterChips: {
    flexDirection: 'row',
    marginTop: 8,
  },
  chip: {
    marginRight: 8,
  },
  fab: {
    position: 'absolute',
    margin: 16,
    right: 0,
    bottom: 0,
    backgroundColor: theme.colors.primary,
  },
});

export default TransactionsScreen; 