using Newtonsoft.Json;

namespace MaxiCrush.Contracts;

public class ErrorMessage
{
    [JsonProperty("title")]
    public string Message { get; set; }

    [JsonProperty("details")]
    public string Details { get; set; }
}
