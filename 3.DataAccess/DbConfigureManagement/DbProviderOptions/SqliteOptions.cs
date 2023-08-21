using System;
using DataAccess.DbConfigureManagement.Interfaces;
using Infrastructure.BaseComponents.Components;
using Infrastructure.BaseComponents.Components.IO;
using Infrastructure.BaseExtensions;
using Infrastructure.Phrases;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace DataAccess.DbConfigureManagement.DbProviderOptions;

/// <summary>
/// Опции (настройки) провайдера БД - SQLite.
/// </summary>
public class SqliteOptions : IDbProviderOptions
{
    /// <inheritdoc />
    public DbProviderEnm DbProviderType => DbProviderEnm.Sqlite;

    /// <inheritdoc />
    public bool IsEmbeddedDb => true;

    /// <inheritdoc />
    public string? DateTimeColumnType => null;

    /// <inheritdoc />
    public string? MigrationsAssemblyName { get; private init; }

    /// <summary>
    /// Конструктор, запрещающий создание экземпляра без параметров.
    /// </summary>
    private SqliteOptions()
    {
    }

    /// <inheritdoc />
    public static IDbProviderOptions Create(string? migrationsAssemblyName = null)
    {
        var sqliteOptions = new SqliteOptions
        {
            MigrationsAssemblyName = migrationsAssemblyName
        };
        return sqliteOptions;
    }

    /// <inheritdoc />
    public Result<bool> CreateDatabaseDir(string connectionString)
    {
        var builder = new SqliteConnectionStringBuilder(connectionString);
        var fi = new FileInfo(builder.DataSource);
        try
        {
            Directory.CreateDirectory(fi.DirectoryName!);
            return Result<bool>.Done(true);
        }
        catch (Exception ex)
        {
            return Result<bool>.Fail(ex);
        }
    }

    /// <inheritdoc />
    public void ClearAutoincrementSequence(DbContext dbContext, 
        string autoincrementColumnName, params string[] tableNames)
    {
        // Формируем из последовательности названий строку для SQL-выражения
        var str = $"'{string.Join("', '", tableNames)}'";
            
        // Удаляем названия таблиц из специальной скрытой таблицы sqlite_sequence,
        // предназначенной для автоинкремента столбцов
        dbContext.Database.ExecuteSqlRaw
            ($"DELETE FROM `sqlite_sequence` WHERE `name` IN ({str});");
    }

    /// <inheritdoc />
    /// <remarks>
    /// Получаем абсолютный путь к БД из относительного.
    /// </remarks>
    public Result<string> FixConnectionString(string connectionString, string? databaseRootPath)
    {
        if (connectionString.IsNull())
            Result<string>.Fail(new ArgumentException(DbPhrases.ConnectionStringError));
                
        var builder = new SqliteConnectionStringBuilder(connectionString);
        builder.DataSource = Path.GetFullPath(
            Path.Combine(databaseRootPath ?? string.Empty, builder.DataSource));
        
        return Result<string>.Done(builder.ToString());
    }

    /// <inheritdoc />
    public string GetConnectionString(IConfiguration configuration)
    {
        return configuration.GetConnectionString("SqliteConnection") ?? string.Empty;
    }
    
    /// <inheritdoc />
    public DbContextOptions<TDbContext> GetDbContextsOptions<TDbContext>(
        DbContextOptionsBuilder<TDbContext> optionsBuilder, string connectionString) where TDbContext : DbContext
    {
        if (MigrationsAssemblyName.IsNullOrEmpty())
            optionsBuilder.UseSqlite(connectionString);
        else
            optionsBuilder.UseSqlite(connectionString, 
                b => b.MigrationsAssembly(MigrationsAssemblyName));

#if DEBUG
        Console.WriteLine(@"MigrationsAssembly ===== " + MigrationsAssemblyName);
#endif
        
        return optionsBuilder.Options;
    }

    /// <inheritdoc />
    public void ModelBuilderInit(ModelBuilder modelBuilder)
    {
        // Ничего не делаем
    }

}