using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Infrastructure.AppComponents.SwaggerComponents;

namespace DataAccess.Entities;

/// <summary>
/// Сотрудник компании.
/// </summary>
// TODO: Правильнее было бы назвать "Employee".
public class Contact
{
    /// <summary>
    /// Основные свойства сущности.
    /// </summary>
    public enum ContactMainPropEnum
    {
        // None = 0,
        Id = 1,
        Surname,
        Name,
        CompanyId,
        CreationTime,
        ModificationTime
    }

    
    /// <summary>
    /// Идентификатор.
    /// </summary>
    [SwaggerGenGuid]
    public Guid Id  { get; set; }

    /// <summary>
    /// Фамилия.
    /// </summary>
    [Required]
    [DefaultValue("Фамилия")]
    public string Surname { get; set; } = null!;

    /// <summary>
    /// Имя.
    /// </summary>
    [Required]
    [DefaultValue("Имя")]
    public string Name { get; set; } = null!;

    /// <summary>
    /// Отчество.
    /// </summary>
    [DefaultValue("Отчество")]
    public string? MiddleName { get; set; }
        
    /// <summary>
    /// Полное имя (ФИО).
    /// </summary>
    /// <remarks>
    /// Вычисляемое поле.
    /// </remarks>
    [DefaultValue(null)]
    public string? FullName { get; set; } = null!;
    
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
    [SwaggerIgnore]
    [DefaultValue(null)]
    public Company? Company { get; set; }

    /// <summary>
    /// Признак того, что сотрудник является ЛПР.
    /// </summary>
    [DefaultValue(false)]
    public bool IsDecisionMaker { get; set; } = false;
    
    /// <summary>
    /// Должность.
    /// </summary>
    [DefaultValue(null)]
    public string? JobTitle { get; set; }
    
    /// <summary>
    /// Дата создания.
    /// </summary>
    [Required]
    public DateTime CreationTime { get; protected set; }
    
    /// <summary>
    /// Дата изменения.
    /// </summary>
    [Required]
    public DateTime ModificationTime { get; protected set; }
    
    /// <summary>
    /// Список средств коммуникации.
    /// </summary>
    [SwaggerIgnore]
    public ICollection<Communication> Communications { get; set; } = new HashSet<Communication>();


    /// <summary>
    /// Конструктор для MVC.
    /// </summary>
    protected Contact()
    {
        CreationTime = DateTime.Now;
        ModificationTime = DateTime.Now;
    }

    /// <summary>
    /// Конструктор.
    /// </summary>
    /// <param name="id">Идентификатор.</param>
    /// <param name="surname">Фамилия.</param>
    /// <param name="name">Имя.</param>
    /// <param name="middleName">Отчество.</param>
    /// <param name="isDecisionMaker">Признак того, что сотрудник является ЛПР.</param>
    /// <param name="jobTitle">Должность.</param>
    /// <param name="companyId">Идентификатор компании (связь с таблицей Company).</param>
    public Contact(Guid id, string surname, string name, string? middleName = null,
        Guid? companyId = null, bool isDecisionMaker = false, string? jobTitle = null) 
        : this()
    {
        Id = id;
        Surname = surname;
        Name = name;
        MiddleName = middleName;
        middleName = middleName != null
            ? $" {middleName}"
            : string.Empty;
        FullName = $"{Surname} {Name}{middleName}";
        CompanyId = companyId;
        IsDecisionMaker = isDecisionMaker;
        JobTitle = jobTitle;
    }

    /// <summary>
    /// Устанавливаем время модификации.
    /// </summary>
    /// <param name="dateTime">Время модификации (если null - устанавливаем текущее).</param>
    public void SetModificationTime(DateTime? dateTime = null) => 
        ModificationTime = dateTime ?? DateTime.Now;
    
    /// <summary>
    /// Копируем данные в <paramref name="contactTo"/>.
    /// </summary>
    /// <param name="contactTo">Экземпляр класса, куда копируем денные.</param>
    /// <param name="isCopyCreationTime">Признак копирования даты/времени создания.</param>
    /// <param name="isCopyModificationTime">Признак копирования даты/времени модификации.</param>
    public void Copy(ref Contact contactTo, bool isCopyCreationTime = true, bool isCopyModificationTime = true)
    {
        contactTo.Id = Id;
        contactTo.Surname = Surname;
        contactTo.Name = Name;
        contactTo.MiddleName = MiddleName;
        contactTo.FullName = FullName;
        contactTo.CompanyId = CompanyId;
        contactTo.IsDecisionMaker = IsDecisionMaker;
        contactTo.JobTitle = JobTitle;
        
        if (isCopyCreationTime)
            contactTo.CreationTime = CreationTime;

        if (isCopyModificationTime)
            contactTo.ModificationTime = ModificationTime;
    }
}