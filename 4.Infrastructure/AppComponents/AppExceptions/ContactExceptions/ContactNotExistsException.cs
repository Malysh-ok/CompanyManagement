using System.Diagnostics.CodeAnalysis;
using Infrastructure.BaseComponents.Components.Exceptions;

namespace Infrastructure.AppComponents.AppExceptions.ContactExceptions;

/// <summary>
/// Исключение, если сотрудник не существует.
/// </summary>
[SuppressMessage("ReSharper", "ClassNeverInstantiated.Global")]
public class ContactNotExistsException : AppException
{
    /// <summary>
    /// Конструктор, запрещающий создание экземпляра без параметров. 
    /// </summary>
    private ContactNotExistsException(string? message = null, Exception? innerException = null)
        : base(message, innerException)
    {
    }

    /// <summary>
    /// Создать экземпляр исключения.
    /// </summary>
    public static ContactNotExistsException Create(Exception? innerException = null)
    {
        return BaseException.CreateException<ContactNotExistsException>("This employee does not exist.",
            innerException, "ru", "Такого сотрудника не существует.");
    }
}