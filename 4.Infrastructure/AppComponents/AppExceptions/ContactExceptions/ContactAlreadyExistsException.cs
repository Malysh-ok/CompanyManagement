using System.Diagnostics.CodeAnalysis;
using Infrastructure.BaseComponents.Components.Exceptions;

namespace Infrastructure.AppComponents.AppExceptions.ContactExceptions;

/// <summary>
/// Исключение, если сотрудник с таким идентификатором уже существует.
/// </summary>
[SuppressMessage("ReSharper", "ClassNeverInstantiated.Global")]
public class ContactAlreadyExistsException : AppException
{
    /// <summary>
    /// Конструктор, запрещающий создание экземпляра без параметров. 
    /// </summary>
    private ContactAlreadyExistsException(string? message = null, Exception? innerException = null) 
        : base(message, innerException)
    {
    }

    /// <summary>
    /// Создать экземпляр исключения.
    /// </summary>
    public static ContactAlreadyExistsException Create(Exception? innerException = null)
    {
        return BaseException.CreateException<ContactAlreadyExistsException>("A employee with this Id already exists.",
            innerException, "ru", "Сотрудник с таким Id уже существует.");
    }
}