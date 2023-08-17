using DataAccess.Entities;
using DataAccess.Entities.Enums;
using Infrastructure.BaseExtensions.ValueTypes;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.DataAccessManagement.DbContext;

public sealed partial class AppDbContext : Microsoft.EntityFrameworkCore.DbContext
{
    /// <summary>
    /// Строка подключения к БД.
    /// </summary>
    private readonly string _connectionString = null!;

    /// <summary>
    /// Компании.
    /// </summary>
    public DbSet<Company> Companies { get; set; } = null!;

    /// <summary>
    /// Сотрудники.
    /// </summary>
    public DbSet<Contact> Contacts { get; set; } = null!;

    /// <summary>
    /// Средства коммуникации.
    /// </summary>
    public DbSet<Communication> Communications { get; set; } = null!;


    /// <summary>
    /// Конструктор, запрещающий создание экземпляра без параметров.
    /// </summary>
    private AppDbContext()
    {
    }

    /// <summary>
    /// Конструктор.
    /// </summary>
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
        _connectionString = Database.GetConnectionString()!;
    }
    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (optionsBuilder.IsConfigured) return;
        
        optionsBuilder.UseSqlite(_connectionString);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Создание модели компаний. 
        modelBuilder.Entity<Company>(entity =>
        {
            entity.ToTable($"Companies",
                t => t.HasComment("Компании"));

            entity.Property(c => c.Id).ValueGeneratedOnAdd();
            
            entity.Property(c => c.Level)
                .HasConversion(
                    enm => enm.ToInt(),
                    i => i.ToEnumWithException<CompanyLevelEnm>()
                );

            entity.Property(c => c.Comment).HasMaxLength(200);
            
            entity.HasKey(c => c.Id)
                .HasName("PK_Companies");
        });
        
        // Создание модели сотрудников. 
        modelBuilder.Entity<Contact>(entity =>
        {
            entity.ToTable($"Contacts",
                t => t.HasComment("Сотрудники"));

            entity.Property(c => c.Id).ValueGeneratedOnAdd();
            
            // Вычисляемое поле FullName
            entity.Property(c => c.FullName)
                .HasComputedColumnSql($"{nameof(Contact.Surname)} || ' ' || " +
                                      $"{nameof(Contact.Name)} || " +
                                      $"IIF({nameof(Contact.MiddleName)} IS NULL, '', ' ' || {nameof(Contact.MiddleName)})"
                    , stored: true);

            entity.HasKey(c => c.Id)
                .HasName("PK_Contacts");
        });
        
        // Создание модели средств коммуникации. 
        modelBuilder.Entity<Communication>(entity =>
        {
            entity.ToTable($"Communications",
                t => t.HasComment("Средства коммуникации"));

            entity.Property(c => c.Id).ValueGeneratedOnAdd();
            
            entity.Property(c => c.Type)
                .HasConversion(
                    enm => enm.ToInt(),
                    i => i.ToEnumWithException<CommunicationTypeEnm>()
                );
            
            entity.HasKey(c => c.Id)
                .HasName("PK_Communications");
        });
    }
}