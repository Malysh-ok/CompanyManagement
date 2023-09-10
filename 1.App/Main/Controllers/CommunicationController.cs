using App.Main.Controllers.Dto;
using DataAccess.Entities;
using Domain.Models;
using Infrastructure.AppComponents.AppExceptions.CommunicationExceptions;
using Infrastructure.AspComponents.Extensions;
using Infrastructure.BaseComponents.Components;
using Infrastructure.BaseExtensions;
using Microsoft.AspNetCore.Mvc;

namespace App.Main.Controllers;

/// <summary>
/// Контроллер, для работы с Компаниями.
/// </summary>
[ApiController]
[Route("/api/[controller]")]
public class CommunicationController : Controller
{
    /// <inheritdoc cref="CommunicationModel"/>
    private readonly CommunicationModel _communicationModel;
    
    /// <summary>
    /// Конструктор.
    /// </summary>
    public CommunicationController(CommunicationModel communicationModel)
    {
        _communicationModel = communicationModel;
    }
    
    /// <summary>
    /// Получить последовательность всех средств коммуникации.
    /// </summary>
    [HttpGet("GetAll")]
    [Produces("application/json")]
    public async Task<IEnumerable<CommunicationToViewDto>> GetAll()
    {
        var communications = 
            await _communicationModel.GetAllCommunicationsAsync(true, true);

        return CommunicationToViewDto.GetCollection(communications);
    }
    
    /// <summary>
    /// Получить средство коммуникации по id.
    /// </summary>
    /// <param name="id">Идентификатор.</param>
    [HttpGet("Get/{id:guid}")]
    [Produces("application/json")]
    public async Task<IActionResult> Get(Guid id)
    {
        var communication = await _communicationModel.GetCommunicationAsync(id, true, true);

        if (communication is null)
            return NotFound();
        return Ok(new CommunicationToViewDto(communication));
    }
    
    /// <summary>
    /// Добавить средство коммуникации.
    /// </summary>
    /// <inheritdoc cref="CommunicationModel.AddCommunicationAsync"/>
    [HttpPost("Add")]
    [Produces("application/json")]
    public async Task<IActionResult> Post([FromBody] Communication communication)
    {
        if (!ModelState.IsValid)
        {
            // Ошибка валидации модели
            return BadRequest(this.GetModelStateErrors());
        }
        
        // Добавляем новое средство коммуникации в Модель
        var result = await _communicationModel.AddCommunicationAsync(communication);
        
        switch (result.Excptn)
        {
            case CommunicationAlreadyExistsException:
                // Средство коммуникации с таким идентификатором уже существует
                return BadRequest(result.Excptn);
            case CommunicationTypeException:
                // Отсутствуют необходимые данные для выбранного типа связи
                return BadRequest(result.Excptn);
            case CommunicationOwnerEntityException:
                // Отсутствуют необходимые связи с сущностями-владельцами
                return BadRequest(result.Excptn);
        }

        if (!result)
        {
            // Фатальная ошибка
            return StatusCode(StatusCodes.Status500InternalServerError, 
                result.Excptn.Flatten());
        }    
        
        // Все ок!
        // return Ok(new CommunicationToViewDto(result.Value));
        return CreatedAtAction(nameof(Get), new { id = communication.Id }, new CommunicationToViewDto(result.Value));
    }
    
    /// <summary>
    /// Изменить данные средства коммуникации.
    /// </summary>
    /// <inheritdoc cref="CommunicationModel.UpdateCommunicationAsync"/>
    [HttpPut("Put")]
    [Produces("application/json")]
    public async Task<IActionResult> Put([FromBody] Communication communication)
    {
        if (!ModelState.IsValid)
        {
            // Ошибка валидации модели
            return BadRequest(this.GetModelStateErrors());
        }
        
        // Обновляем данные средства коммуникации в Модели
        var result = await _communicationModel.UpdateCommunicationAsync(communication);
        
        if (result.Excptn is CommunicationNotExistsException)
        {
            // Средство коммуникации не существует
            return BadRequest(result.Excptn);
        }

        if (!result)
        {
            // Фатальная ошибка
            return StatusCode(StatusCodes.Status500InternalServerError, 
                result.Excptn.Flatten());
        }    
        
        // Все ок!
        // return Ok(new CommunicationToViewDto(result.Value));
        return CreatedAtAction(nameof(Get), new { id = communication.Id }, new CommunicationToViewDto(result.Value));
    }  
    
    /// <summary>
    /// Удалить средство коммуникации по ее Id.
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
        
        // Удаление средства коммуникации из Модели
        var result = await _communicationModel.DeleteCommunicationAsync(id);
        
        if (result.Excptn is CommunicationNotExistsException)
        {
            // Средства коммуникации не существует
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

    /// <summary>
    /// Удалить средство коммуникации по Id компании.
    /// </summary>
    [HttpDelete("DeleteByCompanyId/{companyId:guid}")]
    [Produces("application/json")]
    public async Task<IActionResult> DeleteByCompanyId(Guid companyId)
    {
        if (!ModelState.IsValid)
        {
            // Ошибка валидации модели
            return BadRequest(this.GetModelStateErrors());
        }
        
        // Удаление средств коммуникации из Модели
        var result = await _communicationModel.DeleteCommunicationByCompanyIdAsync(companyId);
        
        if (!result)
        {
            // Фатальная ошибка
            return StatusCode(StatusCodes.Status500InternalServerError, 
                result.Excptn.Flatten());
        }    
        
        // Все ок!
        return Ok();    
    }
    
    /// <summary>
    /// Удалить средство коммуникации по Id сотрудника.
    /// </summary>
    [HttpDelete("DeleteByContactId/{contactId:guid}")]
    [Produces("application/json")]
    public async Task<IActionResult> DeleteByContactId(Guid contactId)
    {
        if (!ModelState.IsValid)
        {
            // Ошибка валидации модели
            return BadRequest(this.GetModelStateErrors());
        }
        
        // Удаление средств коммуникации из Модели
        var result = await _communicationModel.DeleteCommunicationByContactIdAsync(contactId);

        if (result.Excptn is CommunicationDeletingByContactIdException)
        {
            // Удаление по невозможно: присутствует связь c сущностью-владельцем
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