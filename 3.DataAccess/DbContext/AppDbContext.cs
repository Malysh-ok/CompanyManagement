using DataAccess.Entities;
using DataAccess.Entities.Enums;
using Infrastructure.BaseExtensions.ValueTypes;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.DbContext;

public sealed class AppDbContext : Microsoft.EntityFrameworkCore.DbContext
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

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Данные для занесения в БД
        var companies = new List<Company>
        {
            new(Guid.NewGuid(), "СССР", CompanyLevelEnm.First, "Добавлено с помощью миграции"),
            new(Guid.NewGuid(), "Китай", CompanyLevelEnm.Second, "Добавлено с помощью миграции"),
            new(Guid.NewGuid(), "Noname", CompanyLevelEnm.Third, "Добавлено с помощью миграции"),
        };
        var contacts = new List<Contact>
        {
            new(Guid.NewGuid(), "Иванов", "Иван", null,
                companies[0].Id, true, "Менеджер"),
            new(Guid.NewGuid(), "Петров", "Петр", null,
                companies[0].Id, false, "Водитель"),
            new(Guid.NewGuid(), "Сидоров", "Сидор", null,
                companies[1].Id, true, "Менеджер"),
            new(Guid.NewGuid(), "Кузьмин", "Кузьма", null,
                companies[2].Id, false, "Механик"),
        };
        var communications = new List<Communication>
        {
            new(Guid.NewGuid(), companies[0].Id, null,  // ЛПР
                CommunicationTypeEnm.Phone, "+70000000000"),
            new(Guid.NewGuid(), companies[0].Id, contacts[1].Id, 
                CommunicationTypeEnm.Phone, "+70000000001"),
            new(Guid.NewGuid(), companies[1].Id, null,    // ЛПР
                CommunicationTypeEnm.Phone, "+71000000000"),
            new(Guid.NewGuid(), companies[2].Id, contacts[3].Id, 
                CommunicationTypeEnm.Phone, "+72000000000"),
            new(Guid.NewGuid(), null, contacts[3].Id, 
                CommunicationTypeEnm.Phone, "+72000000001"),
        };
        
        // Создание модели компаний. 
        modelBuilder.Entity<Company>(entity =>
        {
            entity.ToTable($"Companies",
                t => t.HasComment("Компании"));

            entity.Property(c => c.Id).ValueGeneratedNever();
            
            entity.Property(c => c.Level)
                .HasConversion(
                    enm => enm.ToInt(),
                    i => i.ToEnumWithException<CompanyLevelEnm>()
                );
            
            entity.HasKey(c => c.Id)
                .HasName("PK_Companies");
                        
            // Вторичный ключ (один-к-одному) - ЛПР
            entity.HasOne(c => c.DecisionMaker)
                .WithOne()
                .HasForeignKey<Company>(c => c.DecisionMakerId)
                .HasConstraintName("FK_Companies_DecisionMakerId")
                .OnDelete(DeleteBehavior.SetNull);
                        
            // Заполнение данными
            entity.HasData(companies);
        });
        
        // Создание модели сотрудников. 
        modelBuilder.Entity<Contact>(entity =>
        {
            entity.ToTable($"Contacts",
                t => t.HasComment("Сотрудники"));

            entity.Property(c => c.Id).ValueGeneratedNever();
            
            // Вычисляемое поле FullName
            entity.Property(c => c.FullName)
                .HasComputedColumnSql($"{nameof(Contact.Surname)} || ' ' || " +
                                      $"{nameof(Contact.Name)} || " +
                                      $"IIF({nameof(Contact.MiddleName)} IS NULL, '', ' ' || {nameof(Contact.MiddleName)})", 
                    stored: true);

            entity.HasKey(c => c.Id)
                .HasName("PK_Contacts");
            
            // Вторичный ключ - Компания
            entity.HasOne(с => с.Company)
                .WithMany(cmp => cmp.Contacts)
                .HasForeignKey(c => c.CompanyId)
                .HasConstraintName("FK_Contacts_CompanyId")
                .OnDelete(DeleteBehavior.Cascade);
            
            // Заполнение данными
            entity.HasData(contacts);
        });
        
        // Создание модели средств коммуникации. 
        modelBuilder.Entity<Communication>(entity =>
        {
            entity.ToTable($"Communications",
                t => t.HasComment("Средства коммуникации"));

            entity.Property(c => c.Id).ValueGeneratedNever();
            
            entity.Property(c => c.Type)
                .HasConversion(
                    enm => enm.ToInt(),
                    i => i.ToEnumWithException<CommunicationTypeEnm>()
                );
            
            entity.HasKey(c => c.Id)
                .HasName("PK_Communications");
            
            // Вторичный ключ - Компания
            entity.HasOne(с => с.Company)
                .WithMany(cmp => cmp.Communications)
                .HasForeignKey(c => c.CompanyId)
                .HasConstraintName("FK_Communications_CompanyId")
                .OnDelete(DeleteBehavior.Cascade);

            // Вторичный ключ - Контакт
            entity.HasOne(с => с.Contact)
                .WithMany(cnt => cnt.Communications)
                .HasForeignKey(c => c.ContactId)
                .HasConstraintName("FK_Communications_ContactId")
                .OnDelete(DeleteBehavior.SetNull);
            
            // Заполнение данными
            entity.HasData(communications);
        });
    }
}