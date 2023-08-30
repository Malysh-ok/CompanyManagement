using System.ComponentModel.DataAnnotations;

namespace DataAccess.Entities;

/// <summary>
/// Сотрудник компании.
/// </summary>
// TODO: Правильнее было бы назвать "Employee".
public class Contact
{
    /// <summary>
    /// Идентификатор.
    /// </summary>
    public Guid Id  { get; set; }

    /// <summary>
    /// Фамилия.
    /// </summary>
    [Required]
    public string Surname { get; set; } = null!;

    /// <summary>
    /// Имя.
    /// </summary>
    [Required]
    public string Name { get; set; } = null!;

    /// <summary>
    /// Отчество.
    /// </summary>
    public string? MiddleName { get; set; }
        
    /// <summary>
    /// Полное имя (ФИО).
    /// </summary>
    /// <remarks>
    /// Вычисляемое поле.
    /// </remarks>
    public string FullName { get; set; } = null!;

    /// <summary>
    /// Признак того, что сотрудник является ЛПР.
    /// </summary>
    public bool IsDecisionMaker { get; set; }
    
    /// <summary>
    /// Должность.
    /// </summary>
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
    /// Список средств коммуникации.
    /// </summary>
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
        bool isDecisionMaker = false, string? jobTitle = null, Guid? companyId = null) : this()
    {
        Id = id;
        Surname = surname;
        Name = name;
        MiddleName = middleName;
        middleName = middleName != null
            ? $" {middleName}"
            : string.Empty;
        FullName = $"{Surname} {Name}{middleName}";
        IsDecisionMaker = isDecisionMaker;
        JobTitle = jobTitle;
        CompanyId = companyId;
    }

    /// <summary>
    /// Устанавливаем время модификации.
    /// </summary>
    /// <param name="dateTime">Время модификации.</param>
    public void SetModificationTime(DateTime dateTime) => ModificationTime = dateTime;
}