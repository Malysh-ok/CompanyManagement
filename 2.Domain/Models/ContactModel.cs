using DataAccess.DbContext;
using DataAccess.Entities;
using Infrastructure.AppComponents.AppExceptions;
using Infrastructure.AppComponents.AppExceptions.ContactExceptions;
using Infrastructure.BaseComponents.Components;
using Microsoft.EntityFrameworkCore;

namespace Domain.Models;

/// <summary>
/// Модель, для работы с сотрудниками компании.
/// </summary>
public class ContactModel
{
    /// <summary>
    /// Контекст БД.
    /// </summary>
    private readonly AppDbContext _dbContext;

    
    /// <summary>
    /// Конструктор.
    /// </summary>
    public ContactModel(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    #region [----- Вспомогательные методы -----]

    /// <summary>
    /// Обновляем ЛПР, в том числе данные у компаний (без сохранения изменений в БД).
    /// </summary>
    private async Task<Result<bool>> UpdateDecisionMaker(Contact contact)
    {
        try
        {
            // Удаляем у всех ЛПР (за исключением contact) данный признак.
            var existingDecisionMakers = _dbContext.Contacts.Where(c => 
                c.IsDecisionMaker && c.Id != contact.Id);
            await existingDecisionMakers.ForEachAsync(c => c.IsDecisionMaker = false);
        
            // Заполняем ЛПР в сущности-владельце Company
            // (делаем с помощью IQueryable, чтобы осуществлялся только один запрос к БД)
            if (contact.CompanyId != null)
            {
                var companies = _dbContext.Companies.Where(c =>
                    c.Id == (Guid)contact.CompanyId);
                await companies.ForEachAsync(c => c.DecisionMakerId = contact.Id);
            }
            
            // !!!!!!!!!!!!!!!
            
        }
        catch (Exception ex)
        {
            Result<bool>.Fail(ex);
        }

        return Result<bool>.Done(true);
    }
    
    #endregion

    /// <summary>
    /// Получить последовательность всех сотрудников.
    /// </summary>
    /// <param name="isIncludeCompany">Признак включения к каждому сотруднику связанного объекта
    /// <see cref="Contact.Company"/>.</param>
    /// <param name="filterByFullName">Фильтр по полному имени.</param>
    /// <param name="filterByCompanyName">Фильтр по названии компании.</param>
    /// <param name="filterByJobTitle">Фильтр по должности.</param>
    /// <param name="sortBy">Сортировка с использованием перечисления.</param>
    // TODO: Сделать сортировку по названию компании?
    public async Task<IEnumerable<Contact>> GetAllContactsAsync(bool isIncludeCompany = false,
        string? filterByFullName = null, string? filterByCompanyName = null, string? filterByJobTitle = null,
        Contact.ContactMainPropEnum? sortBy = null)
    {
        IQueryable<Contact> contacts = _dbContext.Contacts;
        
        if (isIncludeCompany)
            // Включаем навигационное свойство
            contacts = contacts.Include(nameof(Contact.Company));

        if (filterByFullName != null)
            // Фильтруем по полному имени
            contacts = contacts.Where(c => c.FullName == filterByFullName);
         
        if (filterByCompanyName != null)
            // Фильтруем по названию компании TODO: проверить!
            contacts = contacts.Where(c => c.Company != null && c.Company.Name == filterByCompanyName);
            
        if (filterByJobTitle != null)
            // Фильтруем по должности
            contacts = contacts.Where(c => c.JobTitle == filterByJobTitle);

        // Сортируем
        contacts = sortBy switch
        {
            Contact.ContactMainPropEnum.Id => contacts.OrderBy(c => c.Id),
            Contact.ContactMainPropEnum.Surname => contacts.OrderBy(c => c.Surname),
            Contact.ContactMainPropEnum.Name => contacts.OrderBy(c => c.Name),
            Contact.ContactMainPropEnum.CompanyId => contacts.OrderBy(c => c.CompanyId),
            Contact.ContactMainPropEnum.CreationTime => contacts.OrderBy(c => c.CreationTime),
            Contact.ContactMainPropEnum.ModificationTime => contacts.OrderBy(c => c.ModificationTime),
            _ => contacts
        };

        return await contacts.ToListAsync();
    }
    
    /// <summary>
    /// Получить сотрудника по id.
    /// </summary>
    /// <param name="id">Идентификатор сотрудника.</param>
    /// <param name="isIncludeCompany">Признак включения к каждому сотруднику связанного объекта
    /// <see cref="Contact.Company"/>.</param>
    public async Task<Contact?> GetContactAsync(Guid id, bool isIncludeCompany = false)
    {
        IQueryable<Contact> contacts = _dbContext.Contacts;
        if (isIncludeCompany)
            // Включаем навигационное свойство
            contacts = contacts.Include(nameof(Contact.Company));

        return await contacts.FirstOrDefaultAsync(c => c.Id == id);
    }

    /// <summary>
    /// Добавляем нового сотрудника в Модель.
    /// </summary>
    /// <param name="contact">Добавляемый сотрудник.</param>
    public async Task<Result<Contact>> AddContactAsync(Contact contact)
    {
        try
        {
            var existingContact = _dbContext.Contacts
                .FirstOrDefault(c => c.Id == contact.Id);
            
            if (existingContact is not null)
                // Сотрудник с таким Id уже существует
                return Result<Contact>.Fail(ContactAlreadyExistsException.Create());
            
            // Добавляем сотрудника
            await _dbContext.AddAsync(contact);
            
            // Если contact - ЛПР
            if (contact.IsDecisionMaker)
            {
                var result = await UpdateDecisionMaker(contact);
                if (!result)
                    return Result<Contact>.Fail(result.Excptn);
            }
            
            // Сохраняем изменения в БД
            await _dbContext.SaveChangesAsync();

            return Result<Contact>.Done(contact);
        }
        catch (Exception ex)
        {
            return Result<Contact>.Fail(ModelException.Create(nameof(ContactModel), innerException: ex));        
        }
    }
    
    /// <summary>
    /// Обновляем сотрудника в Модели.
    /// </summary>
    /// <param name="contact">Сотрудник, данными которого заменяются данные исходного сотрудника
    /// (того, у которого Id совпадает с <paramref name="contact"/>).</param>
    public async Task<Result<Contact>> UpdateContactAsync(Contact contact)
    {
        try
        {
            // Ищем сотрудника по Id
            var existingContact = await _dbContext.Contacts.FindAsync(contact.Id);
            
            if (existingContact is null)
                // Сотрудника с таким id не существует
                return Result<Contact>.Fail(ContactNotExistsException.Create());

            // Копируем данные в данные найденного сотрудника
            contact.Copy(ref existingContact, false, false);
            existingContact.SetModificationTime();
            
            // Обновляем сотрудника
            _dbContext.Update(existingContact);
            
            // Если contact - ЛПР
            if (contact.IsDecisionMaker)
            {
                var result = await UpdateDecisionMaker(contact);
                if (!result)
                    return Result<Contact>.Fail(result.Excptn);
            }
            
            // Сохраняем изменения в БД
            await _dbContext.SaveChangesAsync();
            
            return Result<Contact>.Done(existingContact);
        }
        catch (Exception ex)
        {
            return Result<Contact>.Fail(ModelException.Create(nameof(ContactModel), innerException: ex));        
        }
    }
    
    /// <summary>
    /// Удаление сотрудника из Модели.
    /// </summary>
    /// <param name="id">Идентификатор удаляемого сотрудника.</param>
    public async Task<Result<bool>> DeleteContactAsync(Guid id)
    {
        try
        {
            // Ищем сотрудника по Id
            var existingContact = await _dbContext.Contacts.FindAsync(id);
            
            if (existingContact is null)
                // Сотрудника с таким id не существует
                return Result<bool>.Fail(ContactNotExistsException.Create());

            // Удаляем сотрудника
            _dbContext.Remove(existingContact);
            
            // Сохраняем изменения в БД
            await _dbContext.SaveChangesAsync();
            
            return Result<bool>.Done(true);
        }
        catch (Exception ex)
        {
            return Result<bool>.Fail(ModelException.Create(nameof(ContactModel), innerException: ex));        
        }
    }
}
