namespace Infrastructure.AppComponents.SwaggerComponents;

/// <summary>
/// Атрибут, свойство или поле, помеченное которым, не включается в модель Swagger'а.
/// </summary>
[AttributeUsage(AttributeTargets.Field | AttributeTargets.Property)]
public class SwaggerIgnoreAttribute : Attribute
{
}