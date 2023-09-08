using Microsoft.OpenApi.Any;

namespace Infrastructure.AppComponents.SwaggerComponents;

/// <summary>
/// Атрибут, осуществляющий автоматическую генерацию GUID
/// для помеченного им свойства или поля.
/// </summary>
[AttributeUsage(AttributeTargets.Field | AttributeTargets.Property)]
public class SwaggerGenGuidAttribute : Attribute
{
    public IOpenApiPrimitive Value { get; set; }

    public SwaggerGenGuidAttribute()
    {
        Value = new OpenApiString(Guid.NewGuid().ToString());
    }
}