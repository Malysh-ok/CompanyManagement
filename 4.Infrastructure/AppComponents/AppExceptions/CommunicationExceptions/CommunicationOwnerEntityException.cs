using System.Diagnostics.CodeAnalysis;
using DataAccess.Entities;
using Infrastructure.BaseComponents.Components.Exceptions;

namespace Infrastructure.AppComponents.AppExceptions.CommunicationExceptions;

/// <summary>
/// Исключение, если отсутствуют необходимые связи с сущностями-владельцами.
/// </summary>
[SuppressMessage("ReSharper", "ClassNeverInstantiated.Global")]
public class CommunicationOwnerEntityException : AppException
{
    /// <summary>
    /// Конструктор, запрещающий создание экземпляра без параметров. 
    /// </summary>
    private CommunicationOwnerEntityException(string? message = null, Exception? innerException = null) 
        : base(message, innerException)
    {
    }

    /// <summary>
    /// Создать экземпляр исключения.
    /// </summary>
    public static CommunicationOwnerEntityException Create(Exception? innerException = null)
    {
        return BaseException.CreateException<CommunicationOwnerEntityException>(
            $"At least one of the fields '{nameof(Communication.CompanyId)}'," +
            $" '{nameof(Communication.ContactId)}' must not be null.", 
            innerException, "ru", 
            $"Хотя бы одно из полей '{nameof(Communication.CompanyId)}'," +
            $" '{nameof(Communication.ContactId)}' не должно быть равно null.");
    }
}