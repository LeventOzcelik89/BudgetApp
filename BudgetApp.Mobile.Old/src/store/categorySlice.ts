import { createSlice, createAsyncThunk } from '@reduxjs/toolkit';
import { categoryApi } from '../api/categories';
import { Category, CreateCategoryDto, UpdateCategoryDto } from '../types';

interface CategoryState {
  categories: Category[];
  isLoading: boolean;
  error: string | null;
}

const initialState: CategoryState = {
  categories: [],
  isLoading: false,
  error: null,
};

export const fetchCategories = createAsyncThunk(
  'categories/fetchAll',
  async () => {
    const response = await categoryApi.getAll();
    return response.data;
  }
);

export const createCategory = createAsyncThunk(
  'categories/create',
  async (data: CreateCategoryDto) => {
    const response = await categoryApi.create(data);
    return response.data;
  }
);

export const updateCategory = createAsyncThunk(
  'categories/update',
  async ({ id, data }: { id: number; data: UpdateCategoryDto }) => {
    const response = await categoryApi.update(id, data);
    return response.data;
  }
);

export const deleteCategory = createAsyncThunk(
  'categories/delete',
  async (id: number) => {
    await categoryApi.delete(id);
    return id;
  }
);

const categorySlice = createSlice({
  name: 'categories',
  initialState,
  reducers: {},
  extraReducers: (builder) => {
    builder
      .addCase(fetchCategories.pending, (state) => {
        state.isLoading = true;
      })
      .addCase(fetchCategories.fulfilled, (state, action) => {
        state.isLoading = false;
        state.categories = action.payload;
      })
      .addCase(fetchCategories.rejected, (state, action) => {
        state.isLoading = false;
        state.error = action.error.message || null;
      })
      .addCase(createCategory.fulfilled, (state, action) => {
        state.categories.push(action.payload);
      })
      .addCase(updateCategory.fulfilled, (state, action) => {
        const index = state.categories.findIndex(c => c.id === action.payload.id);
        if (index !== -1) {
          state.categories[index] = action.payload;
        }
      })
      .addCase(deleteCategory.fulfilled, (state, action) => {
        state.categories = state.categories.filter(c => c.id !== action.payload);
      });
  },
});

export default categorySlice.reducer; 