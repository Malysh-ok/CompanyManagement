﻿using Infrastructure.BaseComponents.Components;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace DataAccess.DbConfigureManagement.Interfaces;

/// <summary>
/// Опции (настройки) провайдера БД.
/// </summary>
public interface IDbProviderOptions
{
    /// <summary>
    /// Тип столбца БД, соответствующий сущности DateTime.
    /// </summary>
    /// <remarks>
    /// Актуально только для определенных провайдеров БД.
    /// </remarks>
    string? DateTimeColumnType { get; }

    /// <summary>
    /// Тип провайдера.
    /// </summary>
    DbProviderEnm DbProviderType { get; }
    
    /// <summary>
    /// Признак того, что база данных является встраиваемой.
    /// </summary>
    bool IsEmbeddedDb { get; }

    /// <summary>
    /// Наименование сборки с миграциями.
    /// </summary>
    string? MigrationsAssemblyName { get; }

    /// <summary>
    /// Создать экземпляр <see cref="IDbProviderOptions"/>.
    /// </summary>
    /// <param name="migrationsAssemblyName">Наименование сборки с миграциями.</param>
#pragma warning disable CA2252
    static abstract IDbProviderOptions Create(string? migrationsAssemblyName);
#pragma warning restore CA2252

    /// <summary>
    /// Сброс начальных значений автоинкремента для столбца <paramref name="autoincrementColumnName"/>
    /// всех таблиц массива <paramref name="tableNames"/>.
    /// </summary>
    /// <param name="dbContext">Контекст БД.</param>
    /// <param name="autoincrementColumnName">столбец, у которого сбрасывается автоинкремент
    /// (необходимость данного параметра определяется провайдером БД).</param>
    /// <param name="tableNames">массив таблиц, в которых сбрасываем автоинкремент.</param>
    void ClearAutoincrementSequence(DbContext dbContext, 
        string autoincrementColumnName, params string[] tableNames);

    /// <summary>
    /// Создание каталога БД.
    /// </summary>
    /// <remarks>
    /// Актуально, по всей видимости, только для встраиваемых БД.
    /// </remarks>
    public Result<bool> CreateDatabaseDir(string connectionString);

    /// <summary>
    /// Коррекция строки подключения.
    /// </summary>
    /// <remarks>
    /// Актуально, по всей видимости, только для встраиваемых БД,
    /// где в строке подключения указывается путь к файлу БД, этот путь и подлежит коррекции.
    /// </remarks>
    /// <param name="connectionString">Исходная строка подключения.</param>
    /// <param name="databaseRootPath">Путь к корневой папке БД.</param>
    Result<string> FixConnectionString(string connectionString, string? databaseRootPath = null);

    /// <summary>
    /// Получить строку подключения из конфигурации.
    /// </summary>
    /// <param name="configuration">Конфигурация.</param>
    string GetConnectionString (IConfiguration configuration);
        
    /// <summary>
    /// Получить параметры контекста БД.
    /// </summary>
    /// <remarks>
    /// Применяется при создании экземпляра <typeparamref name="TDbContext"/>.
    /// </remarks>
    /// <param name="optionsBuilder">Экземпляр <see cref="DbContextOptionsBuilder"/>,
    /// с помощью которого получаем опции контекста БД.</param>
    /// <param name="connectionString">Строка подключения.</param>
    DbContextOptions<TDbContext> GetDbContextsOptions<TDbContext>(
        DbContextOptionsBuilder<TDbContext> optionsBuilder,
        string connectionString) where TDbContext : DbContext;

    /// <summary>
    /// Различные настройки, применяемые при создании БД.
    /// </summary>
    /// <param name="modelBuilder">Экземпляр <see cref="ModelBuilder"/>.</param>
    void ModelBuilderInit(ModelBuilder modelBuilder);

    /// <summary>
    /// Настроить контекст БД для подключения к базе данных текущего поставщика.
    /// </summary>
    public DbContextOptionsBuilder UseDatabaseProvider(DbContextOptionsBuilder optionsBuilder, string connectionString);

}