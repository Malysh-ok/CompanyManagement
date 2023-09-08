using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using Infrastructure.BaseExtensions;
using Infrastructure.BaseExtensions.Collections;
using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Infrastructure.AppComponents.SwaggerComponents;

/// <summary>
/// Фильтр, реализующий атрибут <see cref="SwaggerGenGuidAttribute">SwaggerGenGuid</see>.
/// </summary>
[SuppressMessage("ReSharper", "ClassNeverInstantiated.Global")]
public class SwaggerGenGuidFilter : ISchemaFilter
{
    public void Apply(OpenApiSchema schema, SchemaFilterContext schemaFilterContext)
    {
        if (schema.Properties is null)
            return;

        // Получаем список атрибутов для текущей схемы
        var attributes = schemaFilterContext?.MemberInfo?
            .GetCustomAttributes(true).OfType<SwaggerGenGuidAttribute>().ToList();

        if (attributes?.Any() == true)
        {
            if (schema.Type == "string" && schema.Format == "uuid")
                // Заменяем значение по умолчанию
                schema.Default = attributes.First().Value;
        }
    }
}