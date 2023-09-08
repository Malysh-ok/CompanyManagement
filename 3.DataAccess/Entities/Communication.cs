using System.ComponentModel;
using DataAccess.Entities.Enums;
using Infrastructure.AppComponents.SwaggerComponents;

namespace DataAccess.Entities;

/// <summary>
/// Средство коммуникации.
/// </summary>
public class Communication
{
    /// <summary>
    /// Идентификатор.
    /// </summary>
    [SwaggerGenGuid]
    public Guid Id  { get; set; }
    
    /// <summary>
    /// Идентификатор компании.
    /// </summary>
    /// <remarks>
    /// Связь с сущностью-владельцем Company.
    /// </remarks>
    [DefaultValue(null)]
    public Guid? CompanyId { get; set; }
        
    /// <summary>
    /// Компания.
    /// </summary>
    /// <inheritdoc cref="CompanyId"/>
    [DefaultValue(null)]
    [SwaggerIgnore]
    public Company? Company { get; set; }

    /// <summary>
    /// Идентификатор сотрудника.
    /// </summary>
    /// <remarks>
    /// Связь с сущностью-владельцем Contact.
    /// </remarks>
    [DefaultValue(null)]
    public Guid? ContactId { get; set; }

    /// <summary>
    /// Сотрудник.
    /// </summary>
    /// <inheritdoc cref="ContactId"/>
    [DefaultValue(null)]
    [SwaggerIgnore]
    public Contact? Contact { get; set; }
    
    /// <summary>
    /// Тип связи.
    /// </summary>
    public CommunicationTypeEnm Type { get; set; }

    /// <summary>
    /// Телефон.
    /// </summary>
    [DefaultValue(null)]
    public string? PhoneNumber { get; set; }

    /// <summary>
    /// Email.
    /// </summary>
    [DefaultValue(null)]
    public string? Email { get; set; }

    
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
    public Communication(Guid id, Guid? companyId = null, Guid? contactId = null,
        CommunicationTypeEnm type = CommunicationTypeEnm.All, 
        string? phoneNumber = null, string? email = null)
        : this()
    {
        Id = id;
        CompanyId = companyId;
        ContactId = contactId;
        Type = type;
        PhoneNumber = phoneNumber;
        Email = email;
    }
    
    /// <summary>
    /// Копируем данные в <paramref name="communicationTo"/>.
    /// </summary>
    /// <param name="communicationTo">Экземпляр класса, куда копируем денные.</param>
    public void Copy(ref Communication communicationTo)
    {
        communicationTo.Id = Id;
        communicationTo.CompanyId = CompanyId;
        communicationTo.ContactId = ContactId;
        communicationTo.Type = Type;
        communicationTo.PhoneNumber = PhoneNumber;
        communicationTo.Email = Email;
    }
}