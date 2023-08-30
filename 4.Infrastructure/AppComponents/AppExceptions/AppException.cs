using System;

namespace Infrastructure.AppComponents.AppExceptions;

public class AppException : Exception
{
    /// <summary>
    /// Конструктор.
    /// </summary>
    public AppException(string? message = null, Exception? innerException = null) 
        : base(message, innerException)
    {
    }
}