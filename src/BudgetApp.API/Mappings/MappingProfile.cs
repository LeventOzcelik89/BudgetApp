namespace BudgetApp.API.Mappings;

using AutoMapper;
using BudgetApp.API.Models;
using BudgetApp.API.DTOs.Category;
using BudgetApp.API.DTOs.Transaction;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Category, CategoryDto>();
        CreateMap<CreateCategoryDto, Category>();
        CreateMap<UpdateCategoryDto, Category>();
        CreateMap<Transaction, TransactionDto>()
            .ForMember(d => d.CategoryName, opt => opt.MapFrom(s => s.Category != null ? s.Category.Name : null));
        CreateMap<CreateTransactionDto, Transaction>();
        CreateMap<UpdateTransactionDto, Transaction>();
    }
} 