using System.Diagnostics.CodeAnalysis;
using DataAccess.DbConfigureManagement;
using Infrastructure.BaseExtensions;
using Microsoft.EntityFrameworkCore.Design;

namespace DataAccess.DataAccessManagement
{
    /// <summary>
    /// Фабрика создания контекста БД (используется при создании миграций).
    /// </summary>
    public class AppDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
    {
        /// <inheritdoc />
        [SuppressMessage("ReSharper", "HeuristicUnreachableCode")]
        public AppDbContext CreateDbContext(string[] args)
        {
            // Получаем конфигуратор БД
            var dbConfigurator = DbConfigurator.CreateDbConfiguratorWithAppData(true);
            
            // Добавляем название проекта с миграциями, которое берем из командной строки
            dbConfigurator.MigrationsAssemblyName = args.FindArg("migrationsAssembly");

            return new AppDbContext(dbConfigurator.GetContextsOptions<AppDbContext>());
        }
    }
}