import React, { useEffect } from 'react';
import { View, ScrollView, StyleSheet } from 'react-native';
import { List, Switch, Divider, Button, useTheme } from 'react-native-paper';
import { useDispatch, useSelector } from 'react-redux';
import { Loading } from '../../components/common';
import { RootState } from '../../store';
import { logout } from '../../store/authSlice';
import { updateSettings } from '../../store/settingsSlice';
import { CurrencySelector } from '../../components/settings/CurrencySelector';

export const SettingsScreen = ({ navigation }) => {
  const dispatch = useDispatch();
  const theme = useTheme();
  const { settings, isLoading } = useSelector((state: RootState) => state.settings);
  const { user } = useSelector((state: RootState) => state.auth);

  const handleLogout = () => {
    dispatch(logout());
  };

  const handleToggleSetting = (key: string) => {
    const newValue = !settings.notificationPreferences[key];
    dispatch(updateSettings({
      notificationPreferences: {
        ...settings.notificationPreferences,
        [key]: newValue,
      }
    }));
  };

  if (isLoading) return <Loading />;

  return (
    <ScrollView style={styles.container}>
      <List.Section>
        <List.Subheader>Hesap</List.Subheader>
        <List.Item
          title={`${user?.firstName} ${user?.lastName}`}
          description={user?.email}
          left={props => <List.Icon {...props} icon="account" />}
        />
      </List.Section>

      <Divider />

      <List.Section>
        <List.Subheader>Para Birimi</List.Subheader>
        <CurrencySelector
          value={settings.currencyCode}
          onSelect={(currency) => {
            dispatch(updateSettings({ currencyCode: currency }));
          }}
        />
      </List.Section>

      <Divider />

      <List.Section>
        <List.Subheader>Bildirimler</List.Subheader>
        <List.Item
          title="Bütçe Aşımı Bildirimleri"
          left={props => <List.Icon {...props} icon="bell" />}
          right={() => (
            <Switch
              value={settings.notificationPreferences.notifyOnBudgetExceeded}
              onValueChange={() => handleToggleSetting('notifyOnBudgetExceeded')}
            />
          )}
        />
        <List.Item
          title="Hedef İlerlemesi Bildirimleri"
          left={props => <List.Icon {...props} icon="target" />}
          right={() => (
            <Switch
              value={settings.notificationPreferences.notifyOnGoalProgress}
              onValueChange={() => handleToggleSetting('notifyOnGoalProgress')}
            />
          )}
        />
        <List.Item
          title="Büyük İşlem Bildirimleri"
          left={props => <List.Icon {...props} icon="cash" />}
          right={() => (
            <Switch
              value={settings.notificationPreferences.notifyOnLargeTransactions}
              onValueChange={() => handleToggleSetting('notifyOnLargeTransactions')}
            />
          )}
        />
      </List.Section>

      <View style={styles.buttonContainer}>
        <Button
          mode="contained"
          onPress={handleLogout}
          style={styles.logoutButton}
          buttonColor={theme.colors.error}
        >
          Çıkış Yap
        </Button>
      </View>
    </ScrollView>
  );
};

const styles = StyleSheet.create({
  container: {
    flex: 1,
  },
  buttonContainer: {
    padding: 16,
  },
  logoutButton: {
    marginTop: 16,
  },
});

export default SettingsScreen; 