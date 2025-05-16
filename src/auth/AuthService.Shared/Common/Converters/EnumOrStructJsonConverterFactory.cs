using System.Text.Json;
using System.Text.Json.Serialization;

namespace AuthService.Shared.Common.Converters;

public class EnumOrStructJsonConverterFactory : JsonConverterFactory
{
    public override bool CanConvert(Type typeToConvert)
    {
        return typeToConvert.IsEnum || (typeToConvert.IsValueType && !typeToConvert.IsPrimitive);
    }

    public override JsonConverter CreateConverter(Type typeToConvert, JsonSerializerOptions options)
    {
        if (typeToConvert.IsEnum)
        {
            var converterType = typeof(EnumOrStructJsonConverter<>).MakeGenericType(typeToConvert);
            return (JsonConverter)Activator.CreateInstance(converterType)!;
        }
        else if (typeToConvert.IsValueType && !typeToConvert.IsPrimitive)
        {
            var converterType = typeof(StructJsonConverter<>).MakeGenericType(typeToConvert);
            return (JsonConverter)Activator.CreateInstance(converterType)!;
        }
        throw new NotSupportedException($"Type {typeToConvert} is not supported by this factory.");
    }
}

public class EnumOrStructJsonConverter<T> : JsonConverter<T> where T : struct, Enum
{
    public override T Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        try
        {
            return (T)Enum.Parse(typeof(T), reader.GetString()!);
        }
        catch
        {
            var requiredValue = string.Join(", ", Enum.GetValues(typeof(T)).Cast<T>());
            throw new JsonException($"Value must in be one of the following values: {requiredValue}");
        }
    }

    public override void Write(Utf8JsonWriter writer, T value, JsonSerializerOptions options)
        => writer.WriteStringValue(value.ToString());
}

public class StructJsonConverter<T> : JsonConverter<T> where T : struct
{
    public override T Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        try
        {
            return JsonSerializer.Deserialize<T>(ref reader, options);
        }
        catch
        {
            var requiredValue = string.Join(", ", Enum.GetValues(typeof(T)).Cast<T>());
            throw new JsonException($"Value must in be one of the following values: {requiredValue}");
        }
    }

    public override void Write(Utf8JsonWriter writer, T value, JsonSerializerOptions options)
        => JsonSerializer.Serialize(writer, value, options);
}
