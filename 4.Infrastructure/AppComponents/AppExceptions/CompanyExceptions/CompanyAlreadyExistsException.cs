using System.Diagnostics.CodeAnalysis;
using Infrastructure.BaseComponents.Components.Exceptions;

namespace Infrastructure.AppComponents.AppExceptions.CompanyExceptions;

/// <summary>
/// Исключение, если компания с таким идентификатором уже существует.
/// </summary>
[SuppressMessage("ReSharper", "ClassNeverInstantiated.Global")]
public class CompanyAlreadyExistsException : AppException
{
    /// <summary>
    /// Конструктор, запрещающий создание экземпляра без параметров. 
    /// </summary>
    private CompanyAlreadyExistsException(string? message = null, Exception? innerException = null) 
        : base(message, innerException)
    {
    }

    /// <summary>
    /// Создать экземпляр исключения.
    /// </summary>
    public static CompanyAlreadyExistsException Create(Exception? innerException = null)
    {
        return BaseException.CreateException<CompanyAlreadyExistsException>("A company with this Id already exists.",
            innerException, "ru", "Компания с таким Id уже существует.");
    }
}