using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace FlaUIScreenReaderMCP.Services;

public static class JsonResponseHelper
{
    public static string ToJsonResponse<T>(T result)
    {
        return JsonConvert.SerializeObject(result, Formatting.Indented, new JsonSerializerSettings
        {
            Converters = new List<JsonConverter>
            {
                new StringEnumConverter()
            }
        });
    }
}