using System.Diagnostics.CodeAnalysis;
using Infrastructure.BaseComponents.Components.Exceptions;

namespace Infrastructure.AppComponents.AppExceptions.CommunicationExceptions;

/// <summary>
/// Исключение, если средство коммуникации не существует.
/// </summary>
[SuppressMessage("ReSharper", "ClassNeverInstantiated.Global")]
public class CommunicationNotExistsException : AppException
{
    /// <summary>
    /// Конструктор, запрещающий создание экземпляра без параметров. 
    /// </summary>
    private CommunicationNotExistsException(string? message = null, Exception? innerException = null)
        : base(message, innerException)
    {
    }

    /// <summary>
    /// Создать экземпляр исключения.
    /// </summary>
    public static CommunicationNotExistsException Create(Exception? innerException = null)
    {
        return BaseException.CreateException<CommunicationNotExistsException>("This Communication does not exist.",
            innerException, "ru", "Такого средства коммуникации не существует.");
    }
}