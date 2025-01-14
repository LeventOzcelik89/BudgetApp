namespace BudgetApp.API.Data;

using Microsoft.EntityFrameworkCore;
using BudgetApp.API.Models;
using BudgetApp.API.Models.Common;
using BudgetApp.API.Services;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<User> Users { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<Transaction> Transactions { get; set; }
    public DbSet<Budget> Budgets { get; set; }
    public DbSet<Goal> Goals { get; set; }
    public DbSet<Device> Devices { get; set; }
    public DbSet<UserSettings> UserSettings { get; set; }
    public DbSet<Currency> Currencies { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // User configurations
        modelBuilder.Entity<User>()
            .HasIndex(u => u.Email)
            .IsUnique();

        // Category configurations
        modelBuilder.Entity<Category>()
            .HasOne(c => c.User)
            .WithMany()
            .HasForeignKey(c => c.UserId)
            .OnDelete(DeleteBehavior.Restrict);

        // Transaction configurations
        modelBuilder.Entity<Transaction>()
            .HasOne(t => t.User)
            .WithMany()
            .HasForeignKey(t => t.UserId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Transaction>()
            .HasOne(t => t.Category)
            .WithMany(c => c.Transactions)
            .HasForeignKey(t => t.CategoryId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Transaction>()
            .HasOne(t => t.Currency)
            .WithMany()
            .HasForeignKey(t => t.CurrencyCode)
            .HasPrincipalKey(c => c.Code)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Transaction>()
            .Property(t => t.OriginalAmount)
            .HasColumnType("decimal(18,2)");

        modelBuilder.Entity<Transaction>()
            .Property(t => t.ConvertedAmount)
            .HasColumnType("decimal(18,2)");

        modelBuilder.Entity<Transaction>()
            .Property(t => t.ExchangeRate)
            .HasColumnType("decimal(18,6)");

        // Budget configurations
        modelBuilder.Entity<Budget>()
            .HasOne(b => b.User)
            .WithMany()
            .HasForeignKey(b => b.UserId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Budget>()
            .HasOne(b => b.Category)
            .WithMany()
            .HasForeignKey(b => b.CategoryId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Budget>()
            .Property(b => b.PlannedAmount)
            .HasColumnType("decimal(18,2)");

        // Goal configurations
        modelBuilder.Entity<Goal>()
            .HasOne(g => g.User)
            .WithMany()
            .HasForeignKey(g => g.UserId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Goal>()
            .HasOne(g => g.Category)
            .WithMany()
            .HasForeignKey(g => g.CategoryId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Goal>()
            .Property(g => g.TargetAmount)
            .HasColumnType("decimal(18,2)");

        modelBuilder.Entity<Goal>()
            .Property(g => g.CurrentAmount)
            .HasColumnType("decimal(18,2)");

        // Device configurations
        modelBuilder.Entity<Device>()
            .HasOne(d => d.User)
            .WithMany()
            .HasForeignKey(d => d.UserId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Device>()
            .HasIndex(d => d.DeviceToken)
            .IsUnique();

        // UserSettings configurations
        modelBuilder.Entity<UserSettings>()
            .HasOne(s => s.User)
            .WithOne()
            .HasForeignKey<UserSettings>(s => s.UserId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<UserSettings>()
            .Property(s => s.NotificationPreferencesJson)
            .HasColumnType("nvarchar(max)");

        modelBuilder.Entity<UserSettings>()
            .Property(s => s.BudgetPreferencesJson)
            .HasColumnType("nvarchar(max)");

        // Currency configurations
        modelBuilder.Entity<Currency>()
            .HasIndex(c => c.Code)
            .IsUnique();

        modelBuilder.Entity<User>().HasData(
            new User
            {
                Id = 1,
                FirstName = "Admin",
                LastName = "Admin",
                Email = "admin@admin.com",
                PasswordHash = "0RliDGV36n4zhibHsZF/wcGBPTAdGpjYhHVMZ3uM4MR4o0UKkhbtY8KrdIw8PeQiRpbo9VOMvvSkKyX1u3n/cA==",
                PasswordSalt = "grAyrNbHlCLK7ioqCs20kbCrDuqzh6u8FnNPZ4Ulcz3u2ynYNJN5FGerGDlxYVKE8DduF0KGa5k6GKDxVl94RT/0+UhKxHksl/wbfnJqU2zHU/clKI7iLTJAW1K6HQwATBRk240aK1RbGhByR5v62jsCELJOz46bNtrhuxmGi50=",
                CreatedAt = DateTime.Now,
                EmailConfirmed = true,
                IsDeleted = false
            }
        );

        modelBuilder.Entity<UserSettings>().HasData(
            new UserSettings
            {
                Id = 1,
                UserId = 1,
                CurrencyCode = "TRY",
                Language = "tr",
                TimeZone = "Europe/Istanbul",
                NotificationPreferencesJson = @"{""EnablePushNotifications"":true,""EnableEmailNotifications"":true,""NotifyOnBudgetExceeded"":true,""NotifyOnGoalProgress"":true,""NotifyOnLargeTransactions"":true,""LargeTransactionThreshold"":1000}",
                BudgetPreferencesJson = @"{""AutomaticallyCategorizeTransactions"":true,""WarnWhenBudgetExceeds"":true,""WarnAtPercentage"":80,""RolloverUnusedBudget"":false}",
                CreatedAt = DateTime.Now,
                IsDeleted = false
            }
        );

        // Seed data for currencies
        modelBuilder.Entity<Currency>().HasData(
            new Currency { Id = 1, Code = "USD", Name = "US Dollar", Symbol = "$", Flag = "🇺🇸", DisplayOrder = 1 },
            new Currency { Id = 2, Code = "EUR", Name = "Euro", Symbol = "€", Flag = "🇪🇺", DisplayOrder = 2 },
            new Currency { Id = 3, Code = "TRY", Name = "Turkish Lira", Symbol = "₺", Flag = "🇹🇷", DisplayOrder = 3 },
            new Currency { Id = 4, Code = "GBP", Name = "British Pound", Symbol = "£", Flag = "🇬🇧", DisplayOrder = 4 },
            new Currency { Id = 5, Code = "JPY", Name = "Japanese Yen", Symbol = "¥", Flag = "🇯🇵", DisplayOrder = 5 },
            new Currency { Id = 6, Code = "CNY", Name = "Chinese Yuan", Symbol = "¥", Flag = "🇨🇳", DisplayOrder = 6 },
            new Currency { Id = 7, Code = "AUD", Name = "Australian Dollar", Symbol = "$", Flag = "🇦🇺", DisplayOrder = 7 },
            new Currency { Id = 8, Code = "CAD", Name = "Canadian Dollar", Symbol = "$", Flag = "🇨🇦", DisplayOrder = 8 },
            new Currency { Id = 9, Code = "CHF", Name = "Swiss Franc", Symbol = "Fr", Flag = "🇨🇭", DisplayOrder = 9 },
            new Currency { Id = 10, Code = "HKD", Name = "Hong Kong Dollar", Symbol = "$", Flag = "🇭🇰", DisplayOrder = 10 },
            new Currency { Id = 11, Code = "NZD", Name = "New Zealand Dollar", Symbol = "$", Flag = "🇳🇿", DisplayOrder = 11 },
            new Currency { Id = 12, Code = "SEK", Name = "Swedish Krona", Symbol = "kr", Flag = "🇸🇪", DisplayOrder = 12 },
            new Currency { Id = 13, Code = "KRW", Name = "South Korean Won", Symbol = "₩", Flag = "🇰🇷", DisplayOrder = 13 },
            new Currency { Id = 14, Code = "SGD", Name = "Singapore Dollar", Symbol = "$", Flag = "🇸🇬", DisplayOrder = 14 },
            new Currency { Id = 15, Code = "NOK", Name = "Norwegian Krone", Symbol = "kr", Flag = "🇳🇴", DisplayOrder = 15 },
            new Currency { Id = 16, Code = "MXN", Name = "Mexican Peso", Symbol = "$", Flag = "🇲🇽", DisplayOrder = 16 },
            new Currency { Id = 17, Code = "INR", Name = "Indian Rupee", Symbol = "₹", Flag = "🇮🇳", DisplayOrder = 17 },
            new Currency { Id = 18, Code = "RUB", Name = "Russian Ruble", Symbol = "₽", Flag = "🇷🇺", DisplayOrder = 18 },
            new Currency { Id = 19, Code = "ZAR", Name = "South African Rand", Symbol = "R", Flag = "🇿🇦", DisplayOrder = 19 },
            new Currency { Id = 20, Code = "BRL", Name = "Brazilian Real", Symbol = "R$", Flag = "🇧🇷", DisplayOrder = 20 }
        );

        // UserSettings configurations
        modelBuilder.Entity<UserSettings>()
            .HasOne(s => s.CurrencyInfo)
            .WithMany()
            .HasForeignKey(s => s.CurrencyCode)
            .HasPrincipalKey(c => c.Code)
            .OnDelete(DeleteBehavior.Restrict);
    }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        var entries = ChangeTracker
            .Entries()
            .Where(e => e.Entity is BaseEntity && (
                e.State == EntityState.Added
                || e.State == EntityState.Modified));

        foreach (var entityEntry in entries)
        {
            var entity = (BaseEntity)entityEntry.Entity;

            if (entityEntry.State == EntityState.Added)
            {
                entity.CreatedAt = DateTime.UtcNow;
            }
            else
            {
                entity.UpdatedAt = DateTime.UtcNow;
            }
        }

        return base.SaveChangesAsync(cancellationToken);
    }
}