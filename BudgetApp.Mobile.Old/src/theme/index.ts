import { MD3Theme, DefaultTheme } from 'react-native-paper';

interface CustomColors {
  success: string;
  text: string;
}

export interface CustomTheme extends MD3Theme {
  colors: MD3Theme['colors'] & CustomColors;
}

export const theme: CustomTheme = {
  ...DefaultTheme,
  colors: {
    ...DefaultTheme.colors,
    success: '#4CAF50',
    text: '#000000'
  }
}; 