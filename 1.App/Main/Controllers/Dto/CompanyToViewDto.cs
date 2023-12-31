﻿using DataAccess.Entities;
using DataAccess.Entities.Enums;

namespace App.Main.Controllers.Dto;

/// <summary>
/// Класс, предназначенный для передачи данных от Контроллера <see cref="CompanyController"/>
/// в Представление.
/// </summary>
public class CompanyToViewDto
{
    /// <summary>
    /// Идентификатор.
    /// </summary>
    // [SwaggerIgnore]
    public Guid Id  { get; protected set; }
    
    /// <summary>
    /// Название.
    /// </summary>
    public string Name { get; protected set; }

    /// <summary>
    /// Уровень доверия.
    /// </summary>
    public CompanyLevelEnm Level  { get; protected set; }
    
    /// <summary>
    /// Идентификатор ЛПР.
    /// </summary>
    /// <remarks>
    /// Связь с сущностью-владельцем Contact.
    /// </remarks>
    public Guid? DecisionMakerId { get; protected set; }

    /// <summary>
    /// ФИО ЛПР.
    /// </summary>
    /// <remarks>
    /// Связь с сущностью-владельцем Contact.
    /// </remarks>
    public string? DecisionMakerFullName { get; protected set; }
    
    /// <summary>
    /// Комментарий.
    /// </summary>
    public string? Comment { get; protected set; }
    
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
    /// <param name="company">Экземпляр класса <see cref="Company"/>, откуда копируем денные.</param>
    public CompanyToViewDto(Company company)
    {
        Id = company.Id;
        Name = company.Name;
        Level = company.Level;
        
        DecisionMakerId = company.DecisionMakerId;
        DecisionMakerFullName = company.DecisionMaker?.FullName;

        Comment = company.Comment;
        CreationTime = company.CreationTime;
        ModificationTime = company.ModificationTime;
    }

    /// <summary>
    /// Получить последовательность экземпляров <see cref="CompanyToViewDto"/>
    /// из последовательности экземпляров <see cref="Company"/>.
    /// </summary>
    public static IEnumerable<CompanyToViewDto> GetCollection(IEnumerable<Company> companies)
    {
        return companies.Select(company => new CompanyToViewDto(company)).ToList();
    }
}