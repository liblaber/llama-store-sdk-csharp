using System.Diagnostics;
using System.Text.Json;
using System.Text.Json.Serialization;
using LlamaStore.Models;

namespace LlamaStore.Json;

internal class ValueEnumJsonConverterFactory : JsonConverterFactory
{
    public override bool CanConvert(Type typeToConvert)
    {
        if (typeToConvert.BaseType is null || !typeToConvert.BaseType.IsGenericType)
        {
            return false;
        }

        var baseType = typeToConvert.BaseType.GetGenericTypeDefinition();
        return baseType == typeof(ValueEnum<>);
    }

    public override JsonConverter? CreateConverter(
        Type typeToConvert,
        JsonSerializerOptions options
    )
    {
        var genericArguments =
            typeToConvert.BaseType?.GetGenericArguments() ?? throw new UnreachableException();
        var jsonConverterType = typeof(ValueEnumJsonConverter<,>).MakeGenericType(
            typeToConvert,
            genericArguments[0]
        );
        return (JsonConverter)Activator.CreateInstance(jsonConverterType);
    }
}
