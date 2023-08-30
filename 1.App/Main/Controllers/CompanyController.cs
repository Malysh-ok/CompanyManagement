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
    private readonly CompanyModel _companyModel;
    
    /// <summary>
    /// Конструктор.
    /// </summary>
    public CompanyController(CompanyModel companyModel)
    {
        _companyModel = companyModel;
    }

    /// <summary>
    /// Получить коллекцию всех компаний.
    /// </summary>
    [HttpGet("GetAll")]
    [Produces("application/json")]
    public async Task<IEnumerable<CompanyToViewDto>> GetAll(
        [FromQuery] string? filterByName = null, [FromQuery] CompanyLevelEnm? filterByLevel = null,
            [FromQuery] Company.MainPropEnum? sortBy = null)
    {
        var companies = await _companyModel.GetAllCompaniesAsync(true,
            filterByName, filterByLevel, sortBy);

        return CompanyToViewDto.GetCollection(companies);
    }

    /// <summary>
    /// Получить компанию по id.
    /// </summary>
    [HttpGet("Get/{id:guid}")]
    [Produces("application/json")]
    public async Task<IActionResult> Get(Guid id)
    {
        var company = await _companyModel.GetCompanyAsync(id);

        if (company is null)
            return NotFound();
        return Ok(company);
    }

    /// <summary>
    /// Добавить компанию.
    /// </summary>
    [HttpPost("Add")]
    [Produces("application/json")]
    public async Task<IActionResult> PostAsync([FromBody] Company company)
    {
        if (!ModelState.IsValid)
        {
            // Ошибка валидации модели
            return BadRequest(this.GetModelStateErrors());
        }
        
        // Обновляем данные в главной Модели
        var result = await _companyModel.AddCompanyAsync(company);
        
        if (result.Excptn is CompanyAlreadyExistsException)
        {
            // Компания с таким названием уже существует
            return BadRequest(result.Excptn);
        }

        if (!result)
        {
            // Фатальная ошибка
            return StatusCode(StatusCodes.Status500InternalServerError, 
                result.Excptn.Flatten());
        }    
        
        // Все ок!
        return CreatedAtAction(nameof(Get), new { id = company.Id }, company);
    }
    
    /// <summary>
    /// Изменить компанию.
    /// </summary>
    [HttpPut("Put")]
    [Produces("application/json")]
    public async Task<IActionResult> PutAsync([FromBody] Company company)
    {
        if (!ModelState.IsValid)
        {
            // Ошибка валидации модели
            return BadRequest(this.GetModelStateErrors());
        }
        
        // Обновляем данные в главной Модели
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
        return Ok(result.Value);
    }    
    /// <summary>
    /// Удалить компанию по ее Id.
    /// </summary>
    [HttpDelete("Delete/{id:guid}")]
    [Produces("application/json")]
    public async Task<IActionResult> DeleteAsync(Guid id)
    {
        if (!ModelState.IsValid)
        {
            // Ошибка валидации модели
            return BadRequest(this.GetModelStateErrors());
        }
        
        // Обновляем данные в главной Модели
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