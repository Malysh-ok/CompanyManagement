using System;
using Infrastructure.BaseComponents.Components.Exceptions;

namespace Infrastructure.AppComponents.AppExceptions;

/// <summary>
/// Базовый класс исключений в главной Модели.
/// </summary>
public class ModelException : AppException
{
    /// <summary>
    /// Конструктор, запрещающий создание экземпляра без параметров. 
    /// </summary>
    private ModelException(string? message = null, Exception? innerException = null) 
        : base(message, innerException)
    {
    }

    /// <summary>
    /// Создать экземпляр исключения.
    /// </summary>
    public static ModelException Create(Exception? innerException = null)
    {
        return BaseException.CreateException<ModelException>("Ошибка обновления данных Модели.",
            innerException, "ru", "Ошибка обновления данных Модели.");
    }
}