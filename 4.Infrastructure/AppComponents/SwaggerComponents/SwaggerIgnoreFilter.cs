using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using Infrastructure.BaseExtensions;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Infrastructure.AppComponents.SwaggerComponents;

/// <summary>
/// Фильтр, реализующий атрибут <see cref="SwaggerIgnoreAttribute">SwaggerIgnore</see>.
/// </summary>
[SuppressMessage("ReSharper", "ClassNeverInstantiated.Global")]
public class SwaggerIgnoreFilter : ISchemaFilter
{
    public void Apply(OpenApiSchema schema, SchemaFilterContext schemaFilterContext)
    {
        if (schema.Properties is null)
            return;

        const BindingFlags bindingFlags = BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance;
        
        // Получаем последовательность MemberInfo всех свойств и полей сущности
        var memberList = schemaFilterContext.Type
            .GetFields(bindingFlags).Cast<MemberInfo>()
            .Concat(schemaFilterContext.Type.GetProperties(bindingFlags));

        // Получаем последовательность строк свойств и полей (приведенных к LowerCamelCase),
        // содержат атрибут SwaggerIgnore
        var excludedList = memberList
            .Where(m => m.GetCustomAttribute<SwaggerIgnoreAttribute>() != null)
            .Select(m => m.GetCustomAttribute<JsonPropertyAttribute>()?.PropertyName ?? m.Name.ToLowerCamelCase());

        // Исключаем свойства и поля, содержащие атрибут SwaggerIgnore, из схемы Swagger'а
        foreach (var excludedName in excludedList)
        {
            if (schema.Properties.ContainsKey(excludedName))
                schema.Properties.Remove(excludedName);
        }
    }
}