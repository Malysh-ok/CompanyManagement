using App.Main.Controllers.Dto;
using DataAccess.Entities;
using Domain.Models;
using Infrastructure.AppComponents.AppExceptions.ContactExceptions;
using Infrastructure.AspComponents.Extensions;
using Infrastructure.BaseExtensions;
using Microsoft.AspNetCore.Mvc;

namespace App.Main.Controllers;

/// <summary>
/// Контроллер, для работы с Сотрудниками компании.
/// </summary>
[ApiController]
[Route("/api/[controller]")]
public class ContactController : Controller
{
    /// <inheritdoc cref="ContactModel"/>
    private readonly ContactModel _contactModel;
    
    /// <summary>
    /// Конструктор.
    /// </summary>
    public ContactController(ContactModel contactModel)
    {
        _contactModel = contactModel;
    }
    
    /// <summary>
    /// Получить последовательность всех сотрудников.
    /// </summary>
    /// <param name="filterByFullName">Фильтр по полному имени.</param>
    /// <param name="filterByCompanyName">Фильтр по названии компании.</param>
    /// <param name="filterByJobTitle">Фильтр по должности.</param>
    /// <param name="filterByDecisionMaker">Фильтр по признаку ЛПР.</param>
    /// <param name="sortBy">Сортировка с использованием перечисления.</param>

    [HttpGet("GetAll")]
    [Produces("application/json")]
    public async Task<IEnumerable<ContactToViewDto>> GetAll(
        [FromQuery] string? filterByFullName = null, 
        [FromQuery] string? filterByCompanyName = null, 
        [FromQuery] string? filterByJobTitle = null,
        [FromQuery] bool? filterByDecisionMaker = null,
        [FromQuery] Contact.ContactMainPropEnum? sortBy = null)
    {
        var contacts = await _contactModel.GetAllContactsAsync(true,
            filterByFullName, filterByCompanyName, filterByJobTitle, filterByDecisionMaker, sortBy);

        return ContactToViewDto.GetCollection(contacts);
    }
    
    /// <summary>
    /// Получить сотрудника по id.
    /// </summary>
    /// <param name="id">Идентификатор сотрудника.</param>
    [HttpGet("Get/{id:guid}")]
    [Produces("application/json")]
    public async Task<IActionResult> Get(Guid id)
    {
        var contact = await _contactModel.GetContactAsync(id, true);

        if (contact is null)
            return NotFound();
        return Ok(new ContactToViewDto(contact));
    }
    
    /// <summary>
    /// Добавить сотрудника.
    /// </summary>
    /// <inheritdoc cref="ContactModel.AddContactAsync"/>
    [HttpPost("Add")]
    [Produces("application/json")]
    public async Task<IActionResult> Post([FromBody] Contact contact)
    {
        if (!ModelState.IsValid)
        {
            // Ошибка валидации модели
            return BadRequest(this.GetModelStateErrors());
        }
        
        // Добавляем нового сотрудника в Модель
        var result = await _contactModel.AddContactAsync(contact);
        
        if (result.Excptn is ContactAlreadyExistsException)
        {
            // Сотрудник с таким идентификатором уже существует
            return BadRequest(result.Excptn);
        }

        if (!result)
        {
            // Фатальная ошибка
            return StatusCode(StatusCodes.Status500InternalServerError, 
                result.Excptn.Flatten());
        }    
        
        // Все ок!
        // return Ok(new ContactToViewDto(result.Value));
        return CreatedAtAction(nameof(Get), new { id = contact.Id }, new ContactToViewDto(result.Value));
    }
    
    /// <summary>
    /// Изменить данные сотрудника.
    /// </summary>
    /// <inheritdoc cref="ContactModel.UpdateContactAsync"/>
    [HttpPut("Put")]
    [Produces("application/json")]
    public async Task<IActionResult> Put([FromBody] Contact contact)
    {
        if (!ModelState.IsValid)
        {
            // Ошибка валидации модели
            return BadRequest(this.GetModelStateErrors());
        }
        
        // Обновляем данные в главной Модели
        var result = await _contactModel.UpdateContactAsync(contact);
        
        if (result.Excptn is ContactNotExistsException)
        {
            // Сотрудника не существует
            return BadRequest(result.Excptn);
        }

        if (!result)
        {
            // Фатальная ошибка
            return StatusCode(StatusCodes.Status500InternalServerError, 
                result.Excptn.Flatten());
        }    
        
        // Все ок!
        // return Ok(new ContactToViewDto(result.Value));
        return CreatedAtAction(nameof(Get), new { id = contact.Id }, new ContactToViewDto(result.Value));
    }   
    
    /// <summary>
    /// Удалить сотрудника по его Id.
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
        
        // Удаление сотрудника из Модели
        var result = await _contactModel.DeleteContactAsync(id);
        
        if (result.Excptn is ContactNotExistsException)
        {
            // Сотрудника не существует
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