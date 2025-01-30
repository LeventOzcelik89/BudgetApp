using Microsoft.EntityFrameworkCore;
using BudgetApp.API.Data;
using BudgetApp.API.Data.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using BudgetApp.API.Services;
using FluentValidation.AspNetCore;
using BudgetApp.API.DTOs.Auth.Validators;
using FluentValidation;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using AutoMapper;

var builder = WebApplication.CreateBuilder(args);

// DbContext'i ekle
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddControllers();

// Repository'leri kaydet
builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<ITransactionRepository, TransactionRepository>();

// JWT Authentication
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["JwtSettings:Issuer"],
            ValidAudience = builder.Configuration["JwtSettings:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(builder.Configuration["JwtSettings:SecretKey"]))
        };
    });

// Service registrations
builder.Services.AddScoped<IAuthService, AuthService>();

// Add FluentValidation
builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddValidatorsFromAssemblyContaining<LoginDtoValidator>();

// OpenAPI/Swagger ayarlarını güncelle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "BudgetApp API",
        Version = "v1"
    });

    // JWT authentication için Swagger ayarı
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] {}
        }
    });
});

// AutoMapper
builder.Services.AddAutoMapper(typeof(Program).Assembly);

// Repository registrations
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<IBudgetRepository, BudgetRepository>();
builder.Services.AddScoped<INotificationRepository, NotificationRepository>();

// Service registrations
builder.Services.AddScoped<IBudgetService, BudgetService>();

// Service registrations
builder.Services.AddScoped<IReportService, ReportService>();

// Repository registrations
builder.Services.AddScoped<IGoalRepository, GoalRepository>();

// Service registrations
builder.Services.AddScoped<IGoalService, GoalService>();

// Repository registrations
builder.Services.AddScoped<IDeviceRepository, DeviceRepository>();

// Service registrations
builder.Services.AddScoped<IDeviceService, DeviceService>();

// Push notification service
builder.Services.AddScoped<IPushNotificationService, FirebasePushNotificationService>();

// Repository registrations
builder.Services.AddScoped<IUserSettingsRepository, UserSettingsRepository>();

// Repository registrations
builder.Services.AddScoped<ICurrencyRepository, CurrencyRepository>();

// Service registrations
builder.Services.AddScoped<ICurrencyService, CurrencyService>();

// Service registrations
builder.Services.AddScoped<IUserSettingsService, UserSettingsService>();

// Service registrations

//  NotificationService registrations
builder.Services.AddScoped<INotificationService, NotificationService>();
builder.Services.AddScoped<ITransactionService, TransactionService>();

// Add CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAllOrigins",
        builder =>
        {
            builder.AllowAnyOrigin()
                   .AllowAnyMethod()
                   .AllowAnyHeader();
        });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseDeveloperExceptionPage();
}

// Logging middleware'i ekleyin
app.UseMiddleware<RequestResponseLoggingMiddleware>();


app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.UseCors("AllowAllOrigins");

app.MapControllers();

app.Run();
