using System.Diagnostics.CodeAnalysis;
using DataAccess.Entities;
using DataAccess.Entities.Enums;
using Infrastructure.BaseComponents.Components.Exceptions;
using Infrastructure.BaseExtensions;

namespace Infrastructure.AppComponents.AppExceptions.CommunicationExceptions;

/// <summary>
/// Исключение, если отсутствуют необходимые данные для выбранного типа связи.
/// </summary>
[SuppressMessage("ReSharper", "ClassNeverInstantiated.Global")]
public class CommunicationTypeException : AppException
{
    /// <summary>
    /// Конструктор, запрещающий создание экземпляра без параметров. 
    /// </summary>
    private CommunicationTypeException(string? message = null, Exception? innerException = null) 
        : base(message, innerException)
    {
    }

    /// <summary>
    /// Создать экземпляр исключения.
    /// </summary>
    public static CommunicationTypeException Create(CommunicationTypeEnm communicationType, Exception? innerException = null)
    {
        var messageEn = "For this type of connection, the field {0} cannot be null.";
        var messageRu = "Для данного типа связи поле {0} не может быть равно null.";

        switch (communicationType)
        {
            case CommunicationTypeEnm.Phone:
                messageEn = messageEn.Format($"'{nameof(Communication.PhoneNumber)}'");
                messageRu = messageRu.Format($"'{nameof(Communication.PhoneNumber)}'");
                break;

            case CommunicationTypeEnm.Email:
                messageEn = messageEn.Format($"'{nameof(Communication.Email)}'");
                messageRu = messageRu.Format($"'{nameof(Communication.Email)}'");
                break;

            case CommunicationTypeEnm.All:
            default:
                messageEn = $"For this type of connection, the fields '{nameof(Communication.PhoneNumber)}'" +
                            $" and '{nameof(Communication.Email)}' cannot be null.";
                messageRu = $"Для данного типа связи поля '{nameof(Communication.PhoneNumber)}'" +
                            $" и '{nameof(Communication.Email)}' не могут быть равны null.";
                break;
        }
        
        return BaseException.CreateException<CommunicationTypeException>(
            messageEn, innerException, "ru", messageRu);
    }
}