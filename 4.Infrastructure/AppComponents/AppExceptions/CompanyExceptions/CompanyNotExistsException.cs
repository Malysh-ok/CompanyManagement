using System.Diagnostics.CodeAnalysis;
using Infrastructure.BaseComponents.Components.Exceptions;

namespace Infrastructure.AppComponents.AppExceptions.CompanyExceptions;

/// <summary>
/// Исключение, если компания не существует.
/// </summary>
[SuppressMessage("ReSharper", "ClassNeverInstantiated.Global")]
public class CompanyNotExistsException : AppException
{
    /// <summary>
    /// Конструктор, запрещающий создание экземпляра без параметров. 
    /// </summary>
    private CompanyNotExistsException(string? message = null, Exception? innerException = null)
        : base(message, innerException)
    {
    }

    /// <summary>
    /// Создать экземпляр исключения.
    /// </summary>
    public static CompanyNotExistsException Create(Exception? innerException = null)
    {
        return BaseException.CreateException<CompanyNotExistsException>("This company does not exist.",
            innerException, "ru", "Такой компании не существует.");
    }
}