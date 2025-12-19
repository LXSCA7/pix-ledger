using Microsoft.EntityFrameworkCore;
using PixLedger;
using PixLedger.Application.Services;
using PixLedger.Domain.Entities;
using PixLedger.Domain.Interfaces;
using PixLedger.Infrastructure.Data;
using PixLedger.Infrastructure.Gateways;
using PixLedger.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<AppDbContext>(options => 
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<IAccountRepository, AccountRepository>();
builder.Services.AddScoped<ITransactionRepository, TransactionRepository>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IPixKeyGrpcAdapter, PixKeyKeyGrpcAdapter>();

builder.Services.AddScoped<AccountService>();
builder.Services.AddScoped<AuditService>();
builder.Services.AddScoped<TransactionService>();

builder.Services.AddGrpcClient<PixService.PixServiceClient>(options =>
{
    options.Address = new Uri("http://localhost:50051"); 
});

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
