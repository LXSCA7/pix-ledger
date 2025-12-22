using Microsoft.EntityFrameworkCore;
using PixLedger;
using PixLedger.Application.Services;
using PixLedger.Domain.Interfaces;
using PixLedger.Infrastructure.Adapters;
using PixLedger.Infrastructure.Data;
using PixLedger.Infrastructure.Repositories;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

var seqHost = Environment.GetEnvironmentVariable("SeqHost") ?? "http://localhost:5341";
Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Information()
    .MinimumLevel.Override("Microsoft", Serilog.Events.LogEventLevel.Warning)
    .MinimumLevel.Override("Microsoft.EntityFrameworkCore", Serilog.Events.LogEventLevel.Warning)
    .Enrich.FromLogContext()
    // .WriteTo.Console()
    .WriteTo.Seq(seqHost)
    .CreateLogger();

builder.Host.UseSerilog();

// Add services to the container.
builder.Services.AddDbContext<AppDbContext>(options => 
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<IAccountRepository, AccountRepository>();
builder.Services.AddScoped<ITransactionRepository, TransactionRepository>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IPixKeyGateway, PixKeyGrpcAdapter>();

builder.Services.AddScoped<AccountService>();
builder.Services.AddScoped<AuditService>();
builder.Services.AddScoped<TransactionService>();
builder.Services.AddScoped<PixKeyAppService>();

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

var port = Environment.GetEnvironmentVariable("ASPNETCORE_HTTP_PORTS") 
           ?? Environment.GetEnvironmentVariable("PORT") 
           ?? "5014"; 

Log.Information("🤓 pix ledger runnint at :{Port}  [{Env}]", 
    port, app.Environment.EnvironmentName);

Console.BackgroundColor = ConsoleColor.DarkMagenta;
Console.ForegroundColor = ConsoleColor.White;
Console.WriteLine($"\n 🤓 pix ledger running at :{port}\n");
Console.ResetColor();

app.Run();
