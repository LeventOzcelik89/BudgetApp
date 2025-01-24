import axios from 'axios';
import { MonthlyReport, YearlyReport } from '../types';

const BASE_URL = 'http://localhost:5000/api';

export const reportsApi = {
  getMonthlyReport: async (year: number, month: number) => {
    const response = await axios.get(`${BASE_URL}/reports/monthly/${year}/${month}`);
    return response.data;
  },

  getYearlyReport: async (year: number) => {
    const response = await axios.get(`${BASE_URL}/reports/yearly/${year}`);
    return response.data;
  }
}; 