using System.ComponentModel.DataAnnotations;
using DataAccess.Entities.Enums;

namespace DataAccess.Entities;

/// <summary>
/// Компания.
/// </summary>
public class Company
{
    /// <summary>
    /// Идентификатор.
    /// </summary>
    public Guid Id  { get; set; }
    
    /// <summary>
    /// Уровень доверия.
    /// </summary>
    public CompanyLevelEnm Level  { get; set; }
    
    /// <summary>
    /// Комментарий.
    /// </summary>
    // TODO: [MaxLength(200, ErrorMessage = "{0} может содержать не более {1} символов.")]
    public string? Comment { get; set; }
    
    /// <summary>
    /// Дата создания.
    /// </summary>
    public DateTime CreationTime { get; protected set; }
    
    /// <summary>
    /// Дата изменения.
    /// </summary>
    public DateTime ModificationTime { get; protected set; }
        
    /// <summary>
    /// Идентификатор ЛПР.
    /// </summary>
    /// <remarks>
    /// Связь с объектом-владельцем Contact.
    /// </remarks>
    public Guid DecisionMakerId  { get; set; }
    
    /// <summary>
    /// ЛПР.
    /// </summary>
    /// <inheritdoc cref="DecisionMakerId"/>
    public Contact DecisionMaker { get; set; } = null!;

    /// <summary>
    /// Список сотрудников.
    /// </summary>
    public ICollection<Contact> Contacts { get; set; } = new HashSet<Contact>();
    
    /// <summary>
    /// Список средств коммуникации.
    /// </summary>
    public ICollection<Communication> Communications { get; set; } = new HashSet<Communication>();

    
    /// <summary>
    /// Конструктор для MVC.
    /// </summary>
    protected Company()
    {
        CreationTime = DateTime.Now;
        ModificationTime = DateTime.Now;
    }

    /// <summary>
    /// Конструктор.
    /// </summary>
    /// <param name="id">Идентификатор.</param>
    /// <param name="level">Уровень доверия.</param>
    /// <param name="comment">Комментарий.</param>
    /// <param name="decisionMakerId">Идентификатор ЛПР (связь с таблицей Contact).</param>
    public Company(Guid id, CompanyLevelEnm level, string? comment = null, Guid? decisionMakerId = null) : this()
    {
        Id = id;
        Level = level;
        Comment = comment;
        DecisionMakerId = decisionMakerId ?? Guid.Empty;
    }

    /// <summary>
    /// Устанавливаем время модификации.
    /// </summary>
    /// <param name="dateTime">Время модификации.</param>
    public void SetModificationTime(DateTime dateTime) => ModificationTime = dateTime;
}