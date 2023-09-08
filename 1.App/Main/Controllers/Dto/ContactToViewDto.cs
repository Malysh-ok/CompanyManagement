using DataAccess.Entities;
using DataAccess.Entities.Enums;

namespace App.Main.Controllers.Dto;

/// <summary>
/// Класс, предназначенный для передачи данных от Контроллера <see cref="ContactController"/>
/// в Представление.
/// </summary>
public class ContactToViewDto
{
    /// <summary>
    /// Идентификатор.
    /// </summary>
    // [SwaggerIgnore]
    public Guid Id  { get; protected set; }
    
    /// <summary>
    /// Фамилия.
    /// </summary>
    public string Surname { get; protected set; }

    /// <summary>
    /// Имя.
    /// </summary>
    public string Name { get; protected set; }

    /// <summary>
    /// Отчество.
    /// </summary>
    public string? MiddleName { get; protected set; }
        
    /// <summary>
    /// Полное имя (ФИО).
    /// </summary>
    /// <remarks>
    /// Вычисляемое поле.
    /// </remarks>
    public string FullName { get; protected set; }

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
    /// <remarks>
    /// Связь с сущностью-владельцем Company.
    /// </remarks>
    public string? CompanyName { get; protected set; }
    
    /// <summary>
    /// Признак того, что сотрудник является ЛПР.
    /// </summary>
    public bool IsDecisionMaker { get; protected set; }
    
    /// <summary>
    /// Должность.
    /// </summary>
    public string? JobTitle { get; protected set; }
    
    /// <summary>
    /// Дата создания.
    /// </summary>
    public DateTime CreationTime { get; protected set; }
    
    /// <summary>
    /// Дата изменения.
    /// </summary>
    public DateTime ModificationTime { get; protected set; }
    
    /// <summary>
    /// Конструктор.
    /// </summary>
    /// <param name="contact">Экземпляр класса <see cref="Contact"/>, откуда копируем денные.</param>
    public ContactToViewDto(Contact contact)
    {
        Id = contact.Id;
        
        Surname = contact.Surname;
        Name = contact.Name;
        MiddleName = contact.MiddleName;
        FullName = contact.FullName!;

        CompanyId = contact.CompanyId;
        CompanyName = contact.Company?.Name;
        
        IsDecisionMaker = contact.IsDecisionMaker;
        JobTitle = contact.JobTitle;
        
        CreationTime = contact.CreationTime;
        ModificationTime = contact.ModificationTime;
    }

    /// <summary>
    /// Получить последовательность экземпляров <see cref="ContactToViewDto"/>
    /// из последовательности экземпляров <see cref="Contact"/>.
    /// </summary>
    public static IEnumerable<ContactToViewDto> GetCollection(IEnumerable<Contact> contacts)
    {
        return contacts.Select(contact => new ContactToViewDto(contact)).ToList();
    }
}