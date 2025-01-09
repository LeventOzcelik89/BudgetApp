namespace BudgetApp.API.Mappings;

using AutoMapper;
using BudgetApp.API.Models;
using BudgetApp.API.DTOs.Category;
using BudgetApp.API.DTOs.Transaction;
using BudgetApp.API.DTOs.Budget;
using BudgetApp.API.DTOs.Goals;
using BudgetApp.API.DTOs.Devices;
using BudgetApp.API.DTOs.Currency;
using BudgetApp.API.DTOs.Settings;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Category, CategoryDto>();
        CreateMap<CreateCategoryDto, Category>();
        CreateMap<UpdateCategoryDto, Category>();
        CreateMap<Transaction, TransactionDto>()
            .ForMember(d => d.CategoryName, opt => opt.MapFrom(s => s.Category != null ? s.Category.Name : null))
            .ForMember(d => d.Currency, opt => opt.MapFrom(s => s.Currency));
        CreateMap<CreateTransactionDto, Transaction>();
        CreateMap<UpdateTransactionDto, Transaction>();
        CreateMap<Budget, BudgetDto>()
            .ForMember(d => d.CategoryName, opt => opt.MapFrom(s => s.Category.Name));
        CreateMap<CreateBudgetDto, Budget>();
        CreateMap<UpdateBudgetDto, Budget>();
        CreateMap<Goal, GoalDto>()
            .ForMember(d => d.CategoryName, opt => opt.MapFrom(s => s.Category != null ? s.Category.Name : null));
        CreateMap<CreateGoalDto, Goal>();
        CreateMap<UpdateGoalDto, Goal>();
        CreateMap<Device, DeviceDto>();
        CreateMap<Currency, CurrencyDto>();
        CreateMap<UserSettings, UserSettingsDto>()
            .ForMember(d => d.Currency, opt => opt.Ignore());
        CreateMap<Currency, CurrencyInfo>();
    }
} 