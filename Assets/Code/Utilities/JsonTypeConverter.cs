using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

public class JsonTypeConverter<T> : JsonConverter<T>
{
    private string selector;
    private readonly Dictionary<string, Type> typeDictionary;


    private string GetTypeName(Type t)
    {
        return t.GetCustomAttributes(typeof(JsonTypeAttribute), false).FirstOrDefault() is JsonTypeAttribute a // holy fuck
            ? a.Alias 
            : t.FullName;
    }
    public JsonTypeConverter(string typeField = "$type")
    {
        this.selector = typeField;

        var baseType = typeof(T);
        typeDictionary =
            typeof(T)
                .Assembly
                .GetTypes()
                .Where(t => !t.IsInterface && !t.IsAbstract && baseType.IsAssignableFrom(t))
                .ToDictionary(
                    t => GetTypeName(t), 
                    t => t);
    }


    public override T ReadJson(JsonReader reader, Type objectType, T existingValue, bool hasExistingValue, JsonSerializer serializer)
    {
        if (reader.TokenType == JsonToken.Null) return default;

        JObject jObject = JObject.Load(reader);

        string key = jObject[selector]?.Value<string>() ?? string.Empty;
        if (string.IsNullOrEmpty(key))
            return default;

        if (typeDictionary.TryGetValue(key, out Type target))
            return (T)JsonConvert.DeserializeObject(jObject.ToString(), target);
            
        return default;
    }

    public override void WriteJson(JsonWriter writer, T value, JsonSerializer serializer)
    {
        JObject jo = JObject.FromObject(value);
        jo.AddFirst(new JProperty(selector, GetTypeName(value.GetType())));

        serializer.Serialize(writer, jo);
    }
}
