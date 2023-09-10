using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using DataAccess.Entities.Enums;
using Infrastructure.AppComponents.SwaggerComponents;
using Microsoft.Extensions.Options;

namespace DataAccess.Entities;

/// <summary>
/// Компания.
/// </summary>
public class Company
{
    /// <summary>
    /// Основные свойства сущности.
    /// </summary>
    public enum CompanyMainPropEnum
    {
        // None = 0,
        Id = 1,
        Name,
        Level,
        CreationTime,
        ModificationTime
    }
    
   
    /// <summary>
    /// Идентификатор.
    /// </summary>
    [SwaggerGenGuid]
    public Guid Id  { get; set; }
    
    /// <summary>
    /// Название.
    /// </summary>
    [Required]
    [DefaultValue("CompanyName")]
    public string Name { get; set; } = null!;

    /// <summary>
    /// Уровень доверия.
    /// </summary>
    [Required]
    [DefaultValue(CompanyLevelEnm.Third)]
    public CompanyLevelEnm Level  { get; set; }
    
    /// <summary>
    /// Идентификатор ЛПР.
    /// </summary>
    /// <remarks>
    /// Связь с сущностью-владельцем Contact.
    /// </remarks>
    [DefaultValue(null)]
    public Guid? DecisionMakerId { get; set; }
    
    /// <summary>
    /// ЛПР.
    /// </summary>
    /// <inheritdoc cref="DecisionMakerId"/>
    [SwaggerIgnore]
    [DefaultValue(null)]
    public Contact? DecisionMaker { get; set; }

    /// <summary>
    /// Комментарий.
    /// </summary>
    [DefaultValue(null)]
    [MaxLength(200, ErrorMessage = "Свойство {0} может содержать не более {1} символов.")]
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
    /// Список сотрудников.
    /// </summary>
    [SwaggerIgnore]
    public ICollection<Contact> Contacts { get; set; } = new HashSet<Contact>();
    
    /// <summary>
    /// Список средств коммуникации.
    /// </summary>
    [SwaggerIgnore]
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
    /// <param name="name">Название компании.</param>
    /// <param name="level">Уровень доверия.</param>
    /// <param name="comment">Комментарий.</param>
    /// <param name="decisionMakerId">Идентификатор ЛПР (связь с таблицей Contact).</param>
    public Company(Guid id, string name, CompanyLevelEnm level, string? comment = null, Guid? decisionMakerId = null) 
        : this()
    {
        Id = id;
        Name = name;
        Level = level;
        Comment = comment;
        DecisionMakerId = decisionMakerId;
    }

    /// <summary>
    /// Устанавливаем время модификации.
    /// </summary>
    /// <param name="dateTime">Время модификации.</param>
    public void SetModificationTime(DateTime? dateTime = null) => 
        ModificationTime = dateTime ?? DateTime.Now;

    /// <summary>
    /// Копируем данные в <paramref name="companyTo"/>.
    /// </summary>
    /// <param name="companyTo">Экземпляр класса, куда копируем денные.</param>
    /// <param name="isCopyCreationTime">Признак копирования даты/времени создания.</param>
    /// <param name="isCopyModificationTime">Признак копирования даты/времени модификации.</param>
    public void Copy(ref Company companyTo, bool isCopyCreationTime = true, bool isCopyModificationTime = true)
    {
        companyTo.Id = Id;
        companyTo.Name = Name;
        companyTo.Level = Level;
        companyTo.DecisionMakerId = DecisionMakerId;
        companyTo.Comment = Comment;

        
        if (isCopyCreationTime)
            companyTo.CreationTime = CreationTime;

        if (isCopyModificationTime)
            companyTo.ModificationTime = ModificationTime;
    }
}