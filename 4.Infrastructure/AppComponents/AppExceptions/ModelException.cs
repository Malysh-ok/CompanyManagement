namespace Infrastructure.AppComponents.AppExceptions;

/// <summary>
/// Базовый класс исключений в главной Модели.
/// </summary>
public class ModelException : AppException
{
    /// <summary>
    /// Конструктор.
    /// </summary>
    public ModelException(string? message = null, Exception? innerException = null) 
        : base(message, innerException)
    {
    }
}