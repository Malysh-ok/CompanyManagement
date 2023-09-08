using DataAccess.DbContext;
using DataAccess.Entities;
using DataAccess.Entities.Enums;
using Infrastructure.AppComponents.AppExceptions;
using Infrastructure.AppComponents.AppExceptions.CompanyExceptions;
using Infrastructure.BaseComponents.Components;
using Microsoft.EntityFrameworkCore;

namespace Domain.Models;

/// <summary>
/// Модель, для работы с компаниями.
/// </summary>
public class CompanyModel
{
    /// <summary>
    /// Контекст БД.
    /// </summary>
    private readonly AppDbContext _dbContext;

    /// <summary>
    /// Конструктор.
    /// </summary>
    public CompanyModel(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    /// <summary>
    /// Получить последовательность всех компаний.
    /// </summary>
    /// <param name="isIncludeDecisionMaker">Признак включения в каждую компанию связанного объекта
    /// <see cref="Company.DecisionMaker"/>.</param>
    /// <param name="filterByName">Фильтр по имени.</param>
    /// <param name="filterByLevel">Фильтр по уровню доверия.</param>
    /// <param name="sortBy">Сортировка с использованием перечисления.</param>
    public async Task<IEnumerable<Company>> GetAllCompaniesAsync(bool isIncludeDecisionMaker = false,
        string? filterByName = null, CompanyLevelEnm? filterByLevel = null, Company.CompanyMainPropEnum? sortBy = null)
    {
        IQueryable<Company> companies = _dbContext.Companies;
        
        if (isIncludeDecisionMaker)
            // Включаем навигационное свойство
            companies = companies.Include(nameof(Company.DecisionMaker));

        if (filterByName != null)
            // Фильтруем по имени
            companies = companies.Where(c => c.Name == filterByName);
         
        if (filterByLevel != null)
            // Фильтруем по уровню доверия
            companies = companies.Where(c => c.Level == filterByLevel);
            
        // Сортируем
        companies = sortBy switch
        {
            Company.CompanyMainPropEnum.Id => companies.OrderBy(c => c.Id),
            Company.CompanyMainPropEnum.Name => companies.OrderBy(c => c.Name),
            Company.CompanyMainPropEnum.Level => companies.OrderBy(c => c.Level),
            Company.CompanyMainPropEnum.CreationTime => companies.OrderBy(c => c.CreationTime),
            Company.CompanyMainPropEnum.ModificationTime => companies.OrderBy(c => c.ModificationTime),
            _ => companies
        };

        return await companies.ToListAsync();
    }

    /// <summary>
    /// Получить компанию по id.
    /// </summary>
    /// <param name="id">Идентификатор компании.</param>
    /// <param name="isIncludeDecisionMaker">Признак включения в каждую компанию связанного объекта
    /// <see cref="Company.DecisionMaker"/>.</param>
    public async Task<Company?> GetCompanyAsync(Guid id, bool isIncludeDecisionMaker = false)
    {
        IQueryable<Company> companies = _dbContext.Companies;
        
        if (isIncludeDecisionMaker)
            // Включаем навигационное свойство
            companies = companies.Include(nameof(Company.DecisionMaker));

        return await companies.FirstOrDefaultAsync(c => c.Id == id);
    }

    /// <summary>
    /// Добавляем новую компанию в Модель.
    /// </summary>
    /// <param name="company">Добавляемая компания.</param>
    public async Task<Result<Company>> AddCompanyAsync(Company company)
    {
        try
        {
            var existingCompany = await _dbContext.Companies
                .FirstOrDefaultAsync(c => c.Id == company.Id);
            
            if (existingCompany is not null)
                // Компания с таким Id уже существует
                return Result<Company>.Fail(CompanyAlreadyExistsException.Create());

            // TODO Удалить?
            // if (company.Id == Guid.Empty)
            //     // Присваиваем новый GUID только если он был пустым
            //     company.Id = Guid.NewGuid();
            
            // Добавляем компанию
            await _dbContext.AddAsync(company);
            
            // Сохраняем изменения в БД
            await _dbContext.SaveChangesAsync();
            
            return Result<Company>.Done(company);
        }
        catch (Exception ex)
        {
            return Result<Company>.Fail(ModelException.Create(nameof(CompanyModel), innerException: ex));        
        }
    }
    
    /// <summary>
    /// Обновляем данные компании в Модели.
    /// </summary>
    /// <param name="company">Компания, данными которой заменяются данные исходной компании
    /// (той, у которой Id совпадает с <paramref name="company"/>).</param>
    public async Task<Result<Company>> UpdateCompanyAsync(Company company)
    {
        try
        {
            // Ищем компанию по Id
            var existingCompany = await _dbContext.Companies.FindAsync(company.Id);
            
            if (existingCompany is null)
                // Компании с таким id не существует
                return Result<Company>.Fail(CompanyNotExistsException.Create());

            // Копируем данные в найденную компанию
            company.Copy(ref existingCompany, false, false);
            existingCompany.SetModificationTime();
            
            // Обновляем компанию
            _dbContext.Update(existingCompany);
            
            // Сохраняем изменения в БД
            await _dbContext.SaveChangesAsync();
            
            return Result<Company>.Done(existingCompany);
        }
        catch (Exception ex)
        {
            return Result<Company>.Fail(ModelException.Create(nameof(CompanyModel), innerException: ex));        
        }
    }
    
    /// <summary>
    /// Удаление компании из Модели.
    /// </summary>
    /// <param name="id">Идентификатор удаляемой компании.</param>
    public async Task<Result<bool>> DeleteCompanyAsync(Guid id)
    {
        try
        {
            // Ищем компанию по Id
            var existingCompany = await _dbContext.Companies.FindAsync(id);
            
            if (existingCompany is null)
                // Компании с таким id не существует
                return Result<bool>.Fail(CompanyNotExistsException.Create());

            // Удаляем компанию
            _dbContext.Remove(existingCompany);
            
            // Сохраняем изменения в БД
            await _dbContext.SaveChangesAsync();
            
            return Result<bool>.Done(true);
        }
        catch (Exception ex)
        {
            return Result<bool>.Fail(ModelException.Create(nameof(CompanyModel), innerException: ex));        
        }
    }
}