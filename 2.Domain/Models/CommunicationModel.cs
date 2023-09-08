using DataAccess.DbContext;
using DataAccess.Entities;
using DataAccess.Entities.Enums;
using Infrastructure.AppComponents.AppExceptions;
using Infrastructure.AppComponents.AppExceptions.CommunicationExceptions;
using Infrastructure.BaseComponents.Components;
using Microsoft.EntityFrameworkCore;

namespace Domain.Models;

/// <summary>
/// Модель, для работы с средствами коммуникации.
/// </summary>
public class CommunicationModel
{
    /// <summary>
    /// Контекст БД.
    /// </summary>
    private readonly AppDbContext _dbContext;

    
    /// <summary>
    /// Конструктор.
    /// </summary>
    public CommunicationModel(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    #region [----- Вспомогательные методы -----]

    /// <summary>
    /// Валидация типа связи.
    /// </summary>
    private Result<Communication> ValidateCommunicationType(Communication communication)
    {
        var isOk = communication.Type switch
        {
            CommunicationTypeEnm.Phone => communication.PhoneNumber is not null,
            CommunicationTypeEnm.Email => communication.Email is not null,
            _ => (communication.PhoneNumber is not null) & (communication.Email is not null)
        };

        return isOk
            ? Result<Communication>.Done(communication)
            : Result<Communication>.Fail(CommunicationTypeException.Create(communication.Type));
    }
    
    /// <summary>
    /// Валидация связей с сущностями-владельцами.
    /// </summary>
    private Result<Communication> ValidateOwnerEntity(Communication communication)
    {
        if ((communication.CompanyId is null) & (communication.ContactId is null))
            // Исключение: одно из двух значений является обязательным
            return Result<Communication>.Fail(CommunicationOwnerEntityException.Create());

        return Result<Communication>.Done(communication);
    }

    #endregion
    
    /// <summary>
    /// Получить последовательность всех средств коммуникации.
    /// </summary>
    /// <param name="isIncludeCompany">Признак включения в каждое средство коммуникации
    /// навигационного свойства <see cref="Communication.Company"/>.</param>
    /// <param name="isIncludeContact">Признак включения в каждое средство коммуникации
    /// навигационного свойства <see cref="Communication.Contact"/>.</param>
    public async Task<IEnumerable<Communication>> GetAllCommunicationsAsync(bool isIncludeCompany = false, bool isIncludeContact = false)
    {
        IQueryable<Communication> communications = _dbContext.Communications;
        
        if (isIncludeCompany)
            // Включаем навигационное свойство
            communications = communications.Include(nameof(Communication.Company));
        if (isIncludeContact)
            // Включаем навигационное свойство
            communications = communications.Include(nameof(Communication.Contact));

        return await communications.ToListAsync();
    }
    
    /// <summary>
    /// Получить средство коммуникации по id.
    /// </summary>
    /// <param name="id">Идентификатор.</param>
    /// <param name="isIncludeCompany">Признак включения в каждое средство коммуникации
    /// навигационного свойства <see cref="Communication.Company"/>.</param>
    /// <param name="isIncludeContact">Признак включения в каждое средство коммуникации
    /// навигационного свойства <see cref="Communication.Contact"/>.</param>
    public async Task<Communication?> GetCommunicationAsync(Guid id, bool isIncludeCompany = false, bool isIncludeContact = false)
    {
        IQueryable<Communication> communications = _dbContext.Communications;
        
        if (isIncludeCompany)
            // Включаем навигационное свойство
            communications = communications.Include(nameof(Communication.Company));
        if (isIncludeContact)
            // Включаем навигационное свойство
            communications = communications.Include(nameof(Communication.Contact));

        return await communications.FirstOrDefaultAsync(c => c.Id == id);
    }
    
    /// <summary>
    /// Добавляем новое средство коммуникации в Модель.
    /// </summary>
    /// <param name="communication">Добавляемый сотрудник.</param>
    public async Task<Result<Communication>> AddCommunicationAsync(Communication communication)
    {
        try
        {
            var existingCommunication = _dbContext.Communications
                .FirstOrDefault(c => c.Id == communication.Id);
            
            if (existingCommunication is not null)
                // Средство коммуникации с таким Id уже существует
                return Result<Communication>.Fail(CommunicationAlreadyExistsException.Create());
            
            // Валидация типа связи
            var communicationTypeResult = ValidateCommunicationType(communication);
            if (!communicationTypeResult)
                return communicationTypeResult;
            
            // Валидация типа связи
            var ownerEntityResult = ValidateOwnerEntity(communication);
            if (!ownerEntityResult)
                return ownerEntityResult;
            
            // Добавляем средство коммуникации
            await _dbContext.AddAsync(communication);
            
            // Сохраняем изменения в БД
            await _dbContext.SaveChangesAsync();

            return Result<Communication>.Done(communication);
        }
        catch (Exception ex)
        {
            return Result<Communication>.Fail(ModelException.Create(nameof(CommunicationModel), innerException: ex));        
        }
    }
    
    /// <summary>
    /// Обновляем данные средства коммуникации в Модели.
    /// </summary>
    /// <param name="communication">Средство коммуникации, данными которой заменяются данные исходного средства коммуникации
    /// (того, у которого Id совпадает с <paramref name="communication"/>).</param>
    public async Task<Result<Communication>> UpdateCommunicationAsync(Communication communication)
    {
        try
        {
            // Ищем средство коммуникации по Id
            var existingCommunication = await _dbContext.Communications.FindAsync(communication.Id);
            
            if (existingCommunication is null)
                // Средство коммуникации с таким id не существует
                return Result<Communication>.Fail(CommunicationNotExistsException.Create());
            
            // Валидация типа связи
            var communicationTypeResult = ValidateCommunicationType(communication);
            if (!communicationTypeResult)
                return communicationTypeResult;
            
            // Валидация типа связи
            var ownerEntityResult = ValidateOwnerEntity(communication);
            if (!ownerEntityResult)
                return ownerEntityResult;

            // Копируем данные в найденное средство коммуникации
            communication.Copy(ref existingCommunication);
            
            // Обновляем средство коммуникации
            _dbContext.Update(existingCommunication);
            
            // Сохраняем изменения в БД
            await _dbContext.SaveChangesAsync();
            
            return Result<Communication>.Done(existingCommunication);
        }
        catch (Exception ex)
        {
            return Result<Communication>.Fail(ModelException.Create(nameof(CommunicationModel), innerException: ex));        
        }
    }
    
    /// <summary>
    /// Удаление средства коммуникации из Модели.
    /// </summary>
    /// <param name="id">Идентификатор средства коммуникации.</param>
    public async Task<Result<bool>> DeleteCommunicationAsync(Guid id)
    {
        try
        {
            // Ищем средство коммуникации по Id
            var existingCommunication = await _dbContext.Communications.FindAsync(id);
            
            if (existingCommunication is null)
                // Средство коммуникации с таким id не существует
                return Result<bool>.Fail(CommunicationNotExistsException.Create());

            // Удаляем средство коммуникации
            _dbContext.Remove(existingCommunication);
            
            // Сохраняем изменения в БД
            await _dbContext.SaveChangesAsync();
            
            return Result<bool>.Done(true);
        }
        catch (Exception ex)
        {
            return Result<bool>.Fail(ModelException.Create(nameof(CommunicationModel), innerException: ex));        
        }
    }
    
    /// <summary>
    /// Удаление средства коммуникации из Модели по Id компании.
    /// </summary>
    /// <param name="companyId">Идентификатор компании.</param>
    public async Task<Result<bool>> DeleteCommunicationByCompanyIdAsync(Guid companyId)
    {
        try
        {
            // Получаем средства коммуникации
            var communications = _dbContext.Communications
                .Where(c => c.CompanyId == companyId);

            // Удаляем средства коммуникации
            _dbContext.RemoveRange(communications);
            
            // Сохраняем изменения в БД
            await _dbContext.SaveChangesAsync();
            
            return Result<bool>.Done(true);
        }
        catch (Exception ex)
        {
            return Result<bool>.Fail(ModelException.Create(nameof(CommunicationModel), innerException: ex));        
        }
    }
    /// <summary>
    /// Удаление средства коммуникации из Модели по Id сотрудника.
    /// </summary>
    /// <param name="contactId">Идентификатор сотрудника.</param>
    public async Task<Result<bool>> DeleteCommunicationByContactIdAsync(Guid contactId)
    {
        try
        {
            // Получаем средства коммуникации
            var communications = _dbContext.Communications
                .Where(c => c.ContactId == contactId);

            if (await _dbContext.Contacts.FindAsync(contactId) is not null &
                (await communications.ToListAsync()).Any())
                // Удаление по невозможно: присутствует связь c сущностью-владельцем
                return Result<bool>.Fail(CommunicationDeletingByContactIdException.Create());
            
            // Удаляем средства коммуникации
            _dbContext.RemoveRange(communications);
            
            // Сохраняем изменения в БД
            await _dbContext.SaveChangesAsync();
            
            return Result<bool>.Done(true);
        }
        catch (Exception ex)
        {
            return Result<bool>.Fail(ModelException.Create(nameof(CommunicationModel), innerException: ex));        
        }
    } 
}