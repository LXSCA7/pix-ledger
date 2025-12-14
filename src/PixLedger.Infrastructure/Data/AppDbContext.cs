using Microsoft.EntityFrameworkCore;
using PixLedger.Domain.Entities;
using PixLedger.Domain.Interfaces;
using PixLedger.Domain.ValueObjects;

namespace PixLedger.Infrastructure.Data;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<Transaction> Transactions { get; set; }
    public DbSet<Account> Accounts { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        TransactionModelBuilder(modelBuilder);
        AccountModelBuilder(modelBuilder);
    }

    protected void TransactionModelBuilder(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Transaction>(builder =>
        {
            builder.HasKey(t => t.Id);
            builder.HasOne<Account>()
                   .WithMany()
                   .HasForeignKey(t => t.AccountId)
                   .OnDelete(DeleteBehavior.Restrict);
            
            builder.Property(t => t.Amount).HasColumnType("decimal(18,2)");
            builder.Property(t => t.Type).IsRequired();
            builder.Property(t => t.AccountId).IsRequired();
            builder.Property(t => t.CorrelationId).IsRequired();
            
            builder.Property(t => t.PreviousHash)
                .HasConversion(t => t.Value, value => TransactionHash.Load(value))
                .HasMaxLength(64)
                .IsRequired();
            
            builder.Property(t => t.CurrentHash)
                .HasConversion(t => t.Value, value => TransactionHash.Load(value))
                .HasMaxLength(64)
                .IsRequired();
            
            builder.HasIndex(t => new { t.AccountId, t.PreviousHash }).IsUnique();
        });
    }

    protected void AccountModelBuilder(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Account>(builder =>
        {
            builder.HasKey(t => t.Id);
            builder.Property(t => t.FirstName).IsRequired();
            builder.Property(t => t.LastName).IsRequired();
            builder.Property(t => t.Balance).HasColumnType("decimal(18,2)");
            
            builder.Property(t => t.LastTransactionHash)
                .HasConversion(t => t.Value, value => TransactionHash.Load(value))
                .HasMaxLength(64)
                .IsRequired();
            
            builder.Property(t => t.Version)
                   .IsConcurrencyToken()
                   .IsRequired();
        });
    }
}