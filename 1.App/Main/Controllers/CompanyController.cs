using App.Main.Controllers.Dto;
using DataAccess.Entities;
using DataAccess.Entities.Enums;
using Domain.Models;
using Infrastructure.AppComponents.AppExceptions.CompanyExceptions;
using Infrastructure.AspComponents.Extensions;
using Infrastructure.BaseExtensions;
using Microsoft.AspNetCore.Mvc;

namespace App.Main.Controllers;

/// <summary>
/// Контроллер, для работы с Компаниями.
/// </summary>
[ApiController]
[Route("/api/[controller]")]
public class CompanyController : Controller
{
    /// <inheritdoc cref="CompanyModel"/>
    private readonly CompanyModel _companyModel;
    
    /// <summary>
    /// Конструктор.
    /// </summary>
    public CompanyController(CompanyModel companyModel)
    {
        _companyModel = companyModel;
    }

    /// <summary>
    /// Получить последовательность всех компаний.
    /// </summary>
    /// <param name="filterByName">Фильтр по имени.</param>
    /// <param name="filterByLevel">Фильтр по уровню доверия.</param>
    /// <param name="sortBy">Сортировка с использованием перечисления.</param>
    [HttpGet("GetAll")]
    [Produces("application/json")]
    public async Task<IEnumerable<CompanyToViewDto>> GetAll(
        [FromQuery] string? filterByName = null, [FromQuery] CompanyLevelEnm? filterByLevel = null,
        [FromQuery] Company.CompanyMainPropEnum? sortBy = null)
    {
        var companies = await _companyModel.GetAllCompaniesAsync(true,
            filterByName, filterByLevel, sortBy);

        return CompanyToViewDto.GetCollection(companies);
    }

    /// <summary>
    /// Получить компанию по id.
    /// </summary>
    /// <param name="id">Идентификатор компании.</param>
    [HttpGet("Get/{id:guid}")]
    [Produces("application/json")]
    public async Task<IActionResult> Get(Guid id)
    {
        var company = await _companyModel.GetCompanyAsync(id, true);

        if (company is null)
            return NotFound();
        return Ok(new CompanyToViewDto(company));
    }

    /// <summary>
    /// Добавить компанию.
    /// </summary>
    /// <inheritdoc cref="CompanyModel.AddCompanyAsync"/>
    [HttpPost("Add")]
    [Produces("application/json")]
    public async Task<IActionResult> Post([FromBody] Company company)
    {
        if (!ModelState.IsValid)
        {
            // Ошибка валидации модели
            return BadRequest(this.GetModelStateErrors());
        }
        
        // Добавляем новую компанию в Модель
        var result = await _companyModel.AddCompanyAsync(company);
        
        if (result.Excptn is CompanyAlreadyExistsException)
        {
            // Компания с таким идентификатором уже существует
            return BadRequest(result.Excptn);
        }

        if (!result)
        {
            // Фатальная ошибка
            return StatusCode(StatusCodes.Status500InternalServerError, 
                result.Excptn.Flatten());
        }    
        
        // Все ок!
        // return Ok(new CompanyToViewDto(result.Value));
        return CreatedAtAction(nameof(Get), new { id = company.Id }, new CompanyToViewDto(result.Value));
    }
    
    /// <summary>
    /// Изменить данные компании.
    /// </summary>
    /// <inheritdoc cref="CompanyModel.UpdateCompanyAsync"/>
    [HttpPut("Put")]
    [Produces("application/json")]
    public async Task<IActionResult> Put([FromBody] Company company)
    {
        if (!ModelState.IsValid)
        {
            // Ошибка валидации модели
            return BadRequest(this.GetModelStateErrors());
        }
        
        // Обновляем данные компании в Модели
        var result = await _companyModel.UpdateCompanyAsync(company);
        
        if (result.Excptn is CompanyNotExistsException)
        {
            // Компании не существует
            return BadRequest(result.Excptn);
        }

        if (!result)
        {
            // Фатальная ошибка
            return StatusCode(StatusCodes.Status500InternalServerError, 
                result.Excptn.Flatten());
        }    
        
        // Все ок!
        // return Ok(new CompanyToViewDto(result.Value));
        return CreatedAtAction(nameof(Get), new { id = company.Id }, new CompanyToViewDto(result.Value));
    }   
    
    /// <summary>
    /// Удалить компанию по ее Id.
    /// </summary>
    [HttpDelete("Delete/{id:guid}")]
    [Produces("application/json")]
    public async Task<IActionResult> Delete(Guid id)
    {
        if (!ModelState.IsValid)
        {
            // Ошибка валидации модели
            return BadRequest(this.GetModelStateErrors());
        }
        
        // Удаление компании из Модели
        var result = await _companyModel.DeleteCompanyAsync(id);
        
        if (result.Excptn is CompanyNotExistsException)
        {
            // Компании не существует
            return BadRequest(result.Excptn);
        }

        if (!result)
        {
            // Фатальная ошибка
            return StatusCode(StatusCodes.Status500InternalServerError, 
                result.Excptn.Flatten());
        }    
        
        // Все ок!
        return Ok();
    }
}