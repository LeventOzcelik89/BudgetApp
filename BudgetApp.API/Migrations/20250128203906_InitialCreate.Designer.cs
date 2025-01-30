﻿// <auto-generated />
using System;
using BudgetApp.API.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace BudgetApp.API.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20250128203906_InitialCreate")]
    partial class InitialCreate
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("BudgetApp.API.Models.Budget", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("CategoryId")
                        .HasColumnType("int");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("EndDate")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<decimal>("PlannedAmount")
                        .HasColumnType("decimal(18,2)");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("CategoryId");

                    b.HasIndex("UserId");

                    b.ToTable("Budgets");
                });

            modelBuilder.Entity("BudgetApp.API.Models.Category", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Type")
                        .HasColumnType("int");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Categories");
                });

            modelBuilder.Entity("BudgetApp.API.Models.Currency", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<int>("DisplayOrder")
                        .HasColumnType("int");

                    b.Property<string>("Flag")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Symbol")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("Code")
                        .IsUnique();

                    b.ToTable("Currencies");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Code = "USD",
                            CreatedAt = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            DisplayOrder = 1,
                            Flag = "🇺🇸",
                            IsActive = true,
                            IsDeleted = false,
                            Name = "US Dollar",
                            Symbol = "$"
                        },
                        new
                        {
                            Id = 2,
                            Code = "EUR",
                            CreatedAt = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            DisplayOrder = 2,
                            Flag = "🇪🇺",
                            IsActive = true,
                            IsDeleted = false,
                            Name = "Euro",
                            Symbol = "€"
                        },
                        new
                        {
                            Id = 3,
                            Code = "TRY",
                            CreatedAt = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            DisplayOrder = 3,
                            Flag = "🇹🇷",
                            IsActive = true,
                            IsDeleted = false,
                            Name = "Turkish Lira",
                            Symbol = "₺"
                        },
                        new
                        {
                            Id = 4,
                            Code = "GBP",
                            CreatedAt = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            DisplayOrder = 4,
                            Flag = "🇬🇧",
                            IsActive = true,
                            IsDeleted = false,
                            Name = "British Pound",
                            Symbol = "£"
                        },
                        new
                        {
                            Id = 5,
                            Code = "JPY",
                            CreatedAt = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            DisplayOrder = 5,
                            Flag = "🇯🇵",
                            IsActive = true,
                            IsDeleted = false,
                            Name = "Japanese Yen",
                            Symbol = "¥"
                        },
                        new
                        {
                            Id = 6,
                            Code = "CNY",
                            CreatedAt = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            DisplayOrder = 6,
                            Flag = "🇨🇳",
                            IsActive = true,
                            IsDeleted = false,
                            Name = "Chinese Yuan",
                            Symbol = "¥"
                        },
                        new
                        {
                            Id = 7,
                            Code = "AUD",
                            CreatedAt = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            DisplayOrder = 7,
                            Flag = "🇦🇺",
                            IsActive = true,
                            IsDeleted = false,
                            Name = "Australian Dollar",
                            Symbol = "$"
                        },
                        new
                        {
                            Id = 8,
                            Code = "CAD",
                            CreatedAt = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            DisplayOrder = 8,
                            Flag = "🇨🇦",
                            IsActive = true,
                            IsDeleted = false,
                            Name = "Canadian Dollar",
                            Symbol = "$"
                        },
                        new
                        {
                            Id = 9,
                            Code = "CHF",
                            CreatedAt = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            DisplayOrder = 9,
                            Flag = "🇨🇭",
                            IsActive = true,
                            IsDeleted = false,
                            Name = "Swiss Franc",
                            Symbol = "Fr"
                        },
                        new
                        {
                            Id = 10,
                            Code = "HKD",
                            CreatedAt = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            DisplayOrder = 10,
                            Flag = "🇭🇰",
                            IsActive = true,
                            IsDeleted = false,
                            Name = "Hong Kong Dollar",
                            Symbol = "$"
                        },
                        new
                        {
                            Id = 11,
                            Code = "NZD",
                            CreatedAt = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            DisplayOrder = 11,
                            Flag = "🇳🇿",
                            IsActive = true,
                            IsDeleted = false,
                            Name = "New Zealand Dollar",
                            Symbol = "$"
                        },
                        new
                        {
                            Id = 12,
                            Code = "SEK",
                            CreatedAt = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            DisplayOrder = 12,
                            Flag = "🇸🇪",
                            IsActive = true,
                            IsDeleted = false,
                            Name = "Swedish Krona",
                            Symbol = "kr"
                        },
                        new
                        {
                            Id = 13,
                            Code = "KRW",
                            CreatedAt = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            DisplayOrder = 13,
                            Flag = "🇰🇷",
                            IsActive = true,
                            IsDeleted = false,
                            Name = "South Korean Won",
                            Symbol = "₩"
                        },
                        new
                        {
                            Id = 14,
                            Code = "SGD",
                            CreatedAt = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            DisplayOrder = 14,
                            Flag = "🇸🇬",
                            IsActive = true,
                            IsDeleted = false,
                            Name = "Singapore Dollar",
                            Symbol = "$"
                        },
                        new
                        {
                            Id = 15,
                            Code = "NOK",
                            CreatedAt = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            DisplayOrder = 15,
                            Flag = "🇳🇴",
                            IsActive = true,
                            IsDeleted = false,
                            Name = "Norwegian Krone",
                            Symbol = "kr"
                        },
                        new
                        {
                            Id = 16,
                            Code = "MXN",
                            CreatedAt = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            DisplayOrder = 16,
                            Flag = "🇲🇽",
                            IsActive = true,
                            IsDeleted = false,
                            Name = "Mexican Peso",
                            Symbol = "$"
                        },
                        new
                        {
                            Id = 17,
                            Code = "INR",
                            CreatedAt = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            DisplayOrder = 17,
                            Flag = "🇮🇳",
                            IsActive = true,
                            IsDeleted = false,
                            Name = "Indian Rupee",
                            Symbol = "₹"
                        },
                        new
                        {
                            Id = 18,
                            Code = "RUB",
                            CreatedAt = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            DisplayOrder = 18,
                            Flag = "🇷🇺",
                            IsActive = true,
                            IsDeleted = false,
                            Name = "Russian Ruble",
                            Symbol = "₽"
                        },
                        new
                        {
                            Id = 19,
                            Code = "ZAR",
                            CreatedAt = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            DisplayOrder = 19,
                            Flag = "🇿🇦",
                            IsActive = true,
                            IsDeleted = false,
                            Name = "South African Rand",
                            Symbol = "R"
                        },
                        new
                        {
                            Id = 20,
                            Code = "BRL",
                            CreatedAt = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            DisplayOrder = 20,
                            Flag = "🇧🇷",
                            IsActive = true,
                            IsDeleted = false,
                            Name = "Brazilian Real",
                            Symbol = "R$"
                        });
                });

            modelBuilder.Entity("BudgetApp.API.Models.Device", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("DeviceToken")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("DeviceType")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<DateTime>("LastUsedAt")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("DeviceToken")
                        .IsUnique();

                    b.HasIndex("UserId");

                    b.ToTable("Devices");
                });

            modelBuilder.Entity("BudgetApp.API.Models.Goal", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int?>("CategoryId")
                        .HasColumnType("int");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<decimal>("CurrentAmount")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.Property<decimal>("TargetAmount")
                        .HasColumnType("decimal(18,2)");

                    b.Property<DateTime>("TargetDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("CategoryId");

                    b.HasIndex("UserId");

                    b.ToTable("Goals");
                });

            modelBuilder.Entity("BudgetApp.API.Models.Transaction", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int?>("CategoryId")
                        .HasColumnType("int");

                    b.Property<decimal>("ConvertedAmount")
                        .HasColumnType("decimal(18,2)");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("CurrencyCode")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("ExchangeRate")
                        .HasColumnType("decimal(18,6)");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<decimal>("OriginalAmount")
                        .HasColumnType("decimal(18,2)");

                    b.Property<DateTime>("TransactionDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("Type")
                        .HasColumnType("int");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("CategoryId");

                    b.HasIndex("CurrencyCode");

                    b.HasIndex("UserId");

                    b.ToTable("Transactions");
                });

            modelBuilder.Entity("BudgetApp.API.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<DateTime?>("LastLoginDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PasswordHash")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PasswordSalt")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("Email")
                        .IsUnique();

                    b.ToTable("Users");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            CreatedAt = new DateTime(2025, 1, 28, 23, 39, 5, 914, DateTimeKind.Local).AddTicks(3152),
                            Email = "admin@admin.com",
                            EmailConfirmed = true,
                            FirstName = "Admin",
                            IsDeleted = false,
                            LastName = "Admin",
                            PasswordHash = "0RliDGV36n4zhibHsZF/wcGBPTAdGpjYhHVMZ3uM4MR4o0UKkhbtY8KrdIw8PeQiRpbo9VOMvvSkKyX1u3n/cA==",
                            PasswordSalt = "grAyrNbHlCLK7ioqCs20kbCrDuqzh6u8FnNPZ4Ulcz3u2ynYNJN5FGerGDlxYVKE8DduF0KGa5k6GKDxVl94RT/0+UhKxHksl/wbfnJqU2zHU/clKI7iLTJAW1K6HQwATBRk240aK1RbGhByR5v62jsCELJOz46bNtrhuxmGi50="
                        });
                });

            modelBuilder.Entity("BudgetApp.API.Models.UserSettings", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("BudgetPreferencesJson")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("CurrencyCode")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<string>("Language")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NotificationPreferencesJson")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TimeZone")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("CurrencyCode");

                    b.HasIndex("UserId")
                        .IsUnique();

                    b.ToTable("UserSettings");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            BudgetPreferencesJson = "{\"AutomaticallyCategorizeTransactions\":true,\"WarnWhenBudgetExceeds\":true,\"WarnAtPercentage\":80,\"RolloverUnusedBudget\":false}",
                            CreatedAt = new DateTime(2025, 1, 28, 23, 39, 5, 916, DateTimeKind.Local).AddTicks(3376),
                            CurrencyCode = "TRY",
                            IsDeleted = false,
                            Language = "tr",
                            NotificationPreferencesJson = "{\"EnablePushNotifications\":true,\"EnableEmailNotifications\":true,\"NotifyOnBudgetExceeded\":true,\"NotifyOnGoalProgress\":true,\"NotifyOnLargeTransactions\":true,\"LargeTransactionThreshold\":1000}",
                            TimeZone = "Europe/Istanbul",
                            UserId = 1
                        });
                });

            modelBuilder.Entity("BudgetApp.API.Models.Budget", b =>
                {
                    b.HasOne("BudgetApp.API.Models.Category", "Category")
                        .WithMany()
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("BudgetApp.API.Models.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Category");

                    b.Navigation("User");
                });

            modelBuilder.Entity("BudgetApp.API.Models.Category", b =>
                {
                    b.HasOne("BudgetApp.API.Models.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("BudgetApp.API.Models.Device", b =>
                {
                    b.HasOne("BudgetApp.API.Models.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("BudgetApp.API.Models.Goal", b =>
                {
                    b.HasOne("BudgetApp.API.Models.Category", "Category")
                        .WithMany()
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("BudgetApp.API.Models.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Category");

                    b.Navigation("User");
                });

            modelBuilder.Entity("BudgetApp.API.Models.Transaction", b =>
                {
                    b.HasOne("BudgetApp.API.Models.Category", "Category")
                        .WithMany("Transactions")
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("BudgetApp.API.Models.Currency", "Currency")
                        .WithMany()
                        .HasForeignKey("CurrencyCode")
                        .HasPrincipalKey("Code")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("BudgetApp.API.Models.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Category");

                    b.Navigation("Currency");

                    b.Navigation("User");
                });

            modelBuilder.Entity("BudgetApp.API.Models.UserSettings", b =>
                {
                    b.HasOne("BudgetApp.API.Models.Currency", "CurrencyInfo")
                        .WithMany()
                        .HasForeignKey("CurrencyCode")
                        .HasPrincipalKey("Code")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("BudgetApp.API.Models.User", "User")
                        .WithOne()
                        .HasForeignKey("BudgetApp.API.Models.UserSettings", "UserId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("CurrencyInfo");

                    b.Navigation("User");
                });

            modelBuilder.Entity("BudgetApp.API.Models.Category", b =>
                {
                    b.Navigation("Transactions");
                });
#pragma warning restore 612, 618
        }
    }
}
