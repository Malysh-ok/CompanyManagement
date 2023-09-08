using System.Diagnostics.CodeAnalysis;
using DataAccess.Entities;
using Infrastructure.BaseComponents.Components.Exceptions;

namespace Infrastructure.AppComponents.AppExceptions.CommunicationExceptions;

/// <summary>
/// Исключение при удалении, если есть связь с сущностью-владельцем <see cref="Contact"/>.
/// </summary>
[SuppressMessage("ReSharper", "ClassNeverInstantiated.Global")]
public class CommunicationDeletingByContactIdException : AppException
{
    /// <summary>
    /// Конструктор, запрещающий создание экземпляра без параметров. 
    /// </summary>
    private CommunicationDeletingByContactIdException(string? message = null, Exception? innerException = null) 
        : base(message, innerException)
    {
    }

    /// <summary>
    /// Создать экземпляр исключения.
    /// </summary>
    public static CommunicationDeletingByContactIdException Create(Exception? innerException = null)
    {
        return BaseException.CreateException<CommunicationDeletingByContactIdException>(
            $"Deleting by '{nameof(Communication.ContactId)}' impossible:" +
            $" there is a connection with the owner entity '{nameof(Contact)}'.", 
            innerException, "ru", 
            $"Удаление по '{nameof(Communication.ContactId)}' невозможно:" +
            $" присутствует связь c сущностью-владельцем '{nameof(Contact)}'.");
    }
}