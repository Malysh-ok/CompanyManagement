using System.Diagnostics.CodeAnalysis;
using Infrastructure.BaseComponents.Components.Exceptions;

namespace Infrastructure.AppComponents.AppExceptions.CommunicationExceptions;

/// <summary>
/// Исключение, если средство коммуникации с таким идентификатором уже существует.
/// </summary>
[SuppressMessage("ReSharper", "ClassNeverInstantiated.Global")]
public class CommunicationAlreadyExistsException : AppException
{
    /// <summary>
    /// Конструктор, запрещающий создание экземпляра без параметров. 
    /// </summary>
    private CommunicationAlreadyExistsException(string? message = null, Exception? innerException = null) 
        : base(message, innerException)
    {
    }

    /// <summary>
    /// Создать экземпляр исключения.
    /// </summary>
    public static CommunicationAlreadyExistsException Create(Exception? innerException = null)
    {
        return BaseException.CreateException<CommunicationAlreadyExistsException>("A communication with this Id already exists.",
            innerException, "ru", "Средство коммуникации с таким Id уже существует.");
    }
}