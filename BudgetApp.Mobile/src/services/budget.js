import axiosInstance from './axiosInstance'

export const budgetApi = {
  getBudgets: () => axiosInstance.get('/Budget'),
  getBudget: (id) => axiosInstance.get(`/Budget/${id}`),
  createBudget: (data) => axiosInstance.post('/Budget', data),
  updateBudget: (id, data) => axiosInstance.put(`/Budget/${id}`, data),
  deleteBudget: (id) => axiosInstance.delete(`/Budget/${id}`),
  getSummary: (date) => axiosInstance.get(`/Budget/summary?date=${date}`),
}

export default budgetApi;