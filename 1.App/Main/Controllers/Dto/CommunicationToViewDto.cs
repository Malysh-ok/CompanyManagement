using DataAccess.Entities;
using DataAccess.Entities.Enums;

namespace App.Main.Controllers.Dto;

/// <summary>
/// Класс, предназначенный для передачи данных от Контроллера <see cref="CommunicationController"/>
/// в Представление.
/// </summary>
public class CommunicationToViewDto
{
    /// <summary>
    /// Идентификатор.
    /// </summary>
    public Guid Id  { get; protected set; }
    
    /// <summary>
    /// Идентификатор компании.
    /// </summary>
    /// <remarks>
    /// Связь с сущностью-владельцем Company.
    /// </remarks>
    public Guid? CompanyId { get; protected set; }
        
    /// <summary>
    /// Наименование компании.
    /// </summary>
    /// <inheritdoc cref="CompanyId"/>
    public string? CompanyName { get; protected set; }

    /// <summary>
    /// Идентификатор сотрудника.
    /// </summary>
    /// <remarks>
    /// Связь с сущностью-владельцем Contact.
    /// </remarks>
    public Guid? ContactId { get; protected set; }

    /// <summary>
    /// ФИО сотрудника.
    /// </summary>
    /// <inheritdoc cref="ContactId"/>
    public string? ContactFullName { get; protected set; }
    
    /// <summary>
    /// Тип связи.
    /// </summary>
    public CommunicationTypeEnm Type { get; protected set; }

    /// <summary>
    /// Телефон.
    /// </summary>
    public string? PhoneNumber { get; protected set; }

    /// <summary>
    /// Email.
    /// </summary>
    public string? Email { get; protected set; }
    
    /// <summary>
    /// Конструктор.
    /// </summary>
    /// <param name="communication">Экземпляр класса <see cref="Company"/>, откуда копируем денные.</param>
    public CommunicationToViewDto(Communication communication)
    {
        Id = communication.Id;
        
        CompanyId = communication.CompanyId;
        CompanyName = communication.Company?.Name;

        ContactId = communication.ContactId;
        ContactFullName = communication.Contact?.FullName;
        
        Type = communication.Type;
        PhoneNumber = communication.PhoneNumber;
        Email = communication.Email;
    }

    /// <summary>
    /// Получить последовательность экземпляров <see cref="CommunicationToViewDto"/>
    /// из последовательности экземпляров <see cref="Communication"/>.
    /// </summary>
    public static IEnumerable<CommunicationToViewDto> GetCollection(IEnumerable<Communication> communications)
    {
        return communications.Select(communication => new CommunicationToViewDto(communication)).ToList();
    }
}