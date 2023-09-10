using DataAccess.DbContext;
using Domain.Models;
using Infrastructure.BaseComponents.Components;
using Microsoft.EntityFrameworkCore;
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
    public static async Task Init(IServiceProvider serviceProvider)
    {
        // Инициализация БД
        {
            // Получаем контекст
            var dbContext = serviceProvider.GetRequiredService<AppDbContext>();
            
            // Применяем последнюю миграцию.
            await dbContext.Database.MigrateAsync(); 

            // Из-за циклической связи между Компаниями и Сотрудниками
            // перезаписываем данные сотрудников ЛПР
            // (таким образом обновляются связи Company.DecisionMakerId и Communication.ContactId).
            var contactModel = serviceProvider.GetRequiredService<ContactModel>();
            var contacts = await contactModel.GetAllContactsAsync(filterByDecisionMaker: true);
            await contactModel.UpdateContactRangeAsync(contacts.ToList());
        }

        // Дальнейшая инициализация
        // ...
    }
}