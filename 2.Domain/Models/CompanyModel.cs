using DataAccess.DbContext;
using DataAccess.Entities;
using DataAccess.Entities.Enums;
using Infrastructure.AppComponents.AppExceptions;
using Infrastructure.AppComponents.AppExceptions.CompanyExceptions;
using Infrastructure.BaseComponents.Components;
using Microsoft.EntityFrameworkCore;

namespace Domain.Models;

/// <summary>
/// Модель, для работы с Компаниями.
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
    public async Task<IEnumerable<Company>> GetAllCompaniesAsync(bool isIncludeContact = false,
        string? filterByName = null, CompanyLevelEnm? filterByLevel = null, Company.MainPropEnum? sortBy = null)
    {
        IQueryable<Company> companies = _dbContext.Companies;
        if (isIncludeContact)
            // Включаем навигационное свойство
            companies = companies.Include("DecisionMaker");

        if (filterByName != null)
            // Фильтруем по Имени
            companies = companies.Where(c => c.Name == filterByName);
         
        if (filterByLevel != null)
            // Фильтруем по Уровню
            companies = companies.Where(c => c.Level == filterByLevel);
            
        // Сортируем
        companies = sortBy switch
        {
            Company.MainPropEnum.Id => companies.OrderBy(c => c.Id),
            Company.MainPropEnum.Name => companies.OrderBy(c => c.Name),
            Company.MainPropEnum.Level => companies.OrderBy(c => c.Level),
            Company.MainPropEnum.CreationTime => companies.OrderBy(c => c.CreationTime),
            Company.MainPropEnum.ModificationTime => companies.OrderBy(c => c.ModificationTime),
            _ => companies
        };

        return await companies.ToListAsync();
    }
    
    /// <summary>
    /// Получить компанию по id.
    /// </summary>
    public async Task<Company?> GetCompanyAsync(Guid id, bool isIncludeContact = false)
    {
        IQueryable<Company> companies = _dbContext.Companies;
        if (isIncludeContact)
            // Включаем навигационное свойство
            companies = companies.Include("DecisionMaker");

        return await companies.FirstOrDefaultAsync(c => c.Id == id);
    }

    /// <summary>
    /// Добавляем новую компанию в Модель.
    /// </summary>
    public async Task<Result<bool>> AddCompanyAsync(Company company)
    {
        try
        {
            var existingCompany = _dbContext.Companies
                .FirstOrDefault(c => c.Id.Equals(company.Id));
            if (existingCompany is not null)
                // Компания с таким Id уже существует
                return Result<bool>.Fail(CompanyAlreadyExistsException.Create());

            company.Id = Guid.NewGuid();
            await _dbContext.AddAsync(company);
            await _dbContext.SaveChangesAsync();
            
            return Result<bool>.Done(true);
        }
        catch (Exception ex)
        {
            return Result<bool>.Fail(ModelException.Create(innerException: ex));        
        }
    }
    
    /// <summary>
    /// Обновляем компанию в Модели.
    /// </summary>
    public async Task<Result<Company>> UpdateCompanyAsync(Company company)
    {
        try
        {
            var existingCompany = await _dbContext.Companies.FindAsync(company.Id);
            if (existingCompany is null)
                // Компании с таким id не существует
                return Result<Company>.Fail(CompanyNotExistsException.Create());

            existingCompany.Copy(company, null, true);
            _dbContext.Update(existingCompany);
            await _dbContext.SaveChangesAsync();
            
            return Result<Company>.Done(existingCompany);
        }
        catch (Exception ex)
        {
            return Result<Company>.Fail(ModelException.Create(innerException: ex));        
        }
    }
    
    /// <summary>
    /// Удаление компании из Модели.
    /// </summary>
    public async Task<Result<bool>> DeleteCompanyAsync(Guid id)
    {
        try
        {
            var existingCompany = await _dbContext.Companies.FindAsync(id);
            if (existingCompany is null)
                // Компании с таким id не существует
                return Result<bool>.Fail(CompanyNotExistsException.Create());

            _dbContext.Remove(existingCompany);
            await _dbContext.SaveChangesAsync();
            
            return Result<bool>.Done(true);
        }
        catch (Exception ex)
        {
            return Result<bool>.Fail(ModelException.Create(innerException: ex));        
        }
    }
}