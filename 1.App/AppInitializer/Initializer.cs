using DataAccess.DbContext;
using Infrastructure.AppComponents.AppExceptions;
using Infrastructure.BaseComponents.Components;
using Infrastructure.Phrases;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.DependencyInjection;

namespace App.AppInitializer;

/// <summary>
/// Инициализатор приложения.
/// </summary>
public static class Initializer
{
    /// <summary>
    /// Инициализация приложения.
    /// </summary>
    /// <param name="serviceProvider">Экземпляр <see cref="IServiceProvider"/> (сервисы).</param>
    /// <returns>True или false (в случае неудачи), обернутое в <see cref="Result{T}"/>.</returns>
    public static void Init(IServiceProvider serviceProvider)
    {
        // Инициализация БД
        var dbContext = serviceProvider.GetRequiredService<AppDbContext>();
        dbContext!.Database.Migrate();          // применяем последнюю миграцию.

        // Дальнейшая инициализация
        // ...
    }
}