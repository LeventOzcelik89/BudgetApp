using BudgetApp.API.Models.Enums;
namespace BudgetApp.API.DTOs.Category;

public class CategoryDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public CategoryType Type { get; set; }
}

public class CreateCategoryDto
{
    public string Name { get; set; }
    public string Description { get; set; }
    public CategoryType Type { get; set; }
}

public class UpdateCategoryDto
{
    public string Name { get; set; }
    public string Description { get; set; }
    public CategoryType Type { get; set; }
} 