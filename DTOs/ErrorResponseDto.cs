using System.Text.Json.Serialization;

public class ErrorResponseDto
{
    [JsonPropertyName("error")]
    public ErrorDto? Error { get; set; }
}

public class ErrorDto
{
    [JsonPropertyName("code")]
    public int Code { get; set; }

    [JsonPropertyName("messages")]
    public List<string>? Messages { get; set; }
}