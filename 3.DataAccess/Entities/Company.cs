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
    public enum MainPropEnum
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
    public Guid Id  { get; set; }
    
    /// <summary>
    /// Название.
    /// </summary>
    [Required]
    [DefaultValue("Noname")]
    public string Name { get; set; } = null!;

    /// <summary>
    /// Уровень доверия.
    /// </summary>
    [Required]
    public CompanyLevelEnm Level  { get; set; }
    
    /// <summary>
    /// Идентификатор ЛПР.
    /// </summary>
    /// <remarks>
    /// Связь с объектом-владельцем Contact.
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
    public void SetModificationTime(DateTime dateTime) => ModificationTime = dateTime;

    /// <summary>
    /// Копируем данные из экземпляра класса <paramref name="newCompany"/> в текущий.
    /// </summary>
    /// <param name="newCompany">Экземпляр класса, откуда копируем денные.</param>
    /// <param name="modificationTime">Дата/время модификации.</param>
    /// <param name="isUpdateModificationTime">Признак того, что обновляем дату/время модификации.</param>
    /// <returns>Текущий измененный экземпляр класса.</returns>
    public Company Copy(Company newCompany, DateTime? modificationTime = null, bool isUpdateModificationTime = false)
    {
        Id = newCompany.Id;
        Name = newCompany.Name;
        Level = newCompany.Level;
        Comment = newCompany.Comment;
        if (isUpdateModificationTime)
        {
            SetModificationTime(modificationTime ?? DateTime.Now);
        }

        DecisionMakerId = newCompany.DecisionMakerId;

        return this;
    }
}