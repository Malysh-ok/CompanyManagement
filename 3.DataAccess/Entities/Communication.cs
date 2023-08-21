using DataAccess.Entities.Enums;

namespace DataAccess.Entities;

/// <summary>
/// Средство коммуникации.
/// </summary>
public class Communication
{
    /// <summary>
    /// Идентификатор.
    /// </summary>
    public Guid Id  { get; set; }
    
    /// <summary>
    /// Тип связи.
    /// </summary>
    public CommunicationTypeEnm Type { get; set; }
    
    /// <summary>
    /// Телефон.
    /// </summary>
    public string? PhoneNumber { get; set; } = null!;

    /// <summary>
    /// Email.
    /// </summary>
    public string? Email { get; set; } = null!;
    
    /// <summary>
    /// Идентификатор компании.
    /// </summary>
    /// <remarks>
    /// Связь с объектом-владельцем Company.
    /// </remarks>
    public Guid? CompanyId { get; set; }
        
    /// <summary>
    /// Компания.
    /// </summary>
    /// <inheritdoc cref="CompanyId"/>
    public Company Company { get; set; } = null!;

    /// <summary>
    /// Идентификатор контакта.
    /// </summary>
    /// <remarks>
    /// Связь с объектом-владельцем Contact.
    /// </remarks>
    public Guid? ContactId { get; set; }

    /// <summary>
    /// Контакт.
    /// </summary>
    /// <inheritdoc cref="ContactId"/>
    public Contact Contact { get; set; } = null!;

    
    /// <summary>
    /// Конструктор для MVC.
    /// </summary>
    protected Communication()
    {
    }

    /// <summary>
    /// Конструктор.
    /// </summary>
    /// <param name="id">Идентификатор.</param>
    /// <param name="type">Тип связи.</param>
    /// <param name="phoneNumber">Телефон</param>
    /// <param name="email">Email.</param>
    /// <param name="companyId">Идентификатор компании (связь с таблицей Company).</param>
    /// <param name="contactId">Идентификатор контакта (связь с таблицей Contact).</param>
    public Communication(Guid id, CommunicationTypeEnm type, 
        string? phoneNumber = null, string? email = null,
        Guid? companyId = null, Guid? contactId = null)
    {
        Id = id;
        Type = type;
        PhoneNumber = phoneNumber;
        Email = email;
        CompanyId = companyId;
        ContactId = contactId;
    }
}