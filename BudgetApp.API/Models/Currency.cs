namespace BudgetApp.API.Models;

using BudgetApp.API.Models.Common;

public class Currency : BaseEntity
{
    public string Code { get; set; }  // USD, EUR, TRY gibi
    public string Name { get; set; }  // US Dollar, Euro, Turkish Lira gibi
    public string Symbol { get; set; } // $, €, ₺ gibi
    public string Flag { get; set; }   // 🇺🇸, 🇪🇺, 🇹🇷 gibi
    public bool IsActive { get; set; } = true;
    public int DisplayOrder { get; set; }
} 