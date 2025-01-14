import api from './axios';
import { MonthlyReportDto, YearlyReportDto } from '../types';

export const reportApi = {
  getMonthlyReport: (year: number, month: number) => 
    api.get<MonthlyReportDto>(`/reports/monthly/${year}/${month}`),
  
  getYearlyReport: (year: number) => 
    api.get<YearlyReportDto>(`/reports/yearly/${year}`),
  
  getTrends: (startDate: string, endDate: string) => 
    api.get('/reports/trends', {
      params: { startDate, endDate }
    })
}; 