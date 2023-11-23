using System.Text.Json.Serialization;

public class PostFileForScanResponseDto
{
    [JsonPropertyName("data_id")]
    public string? DataId { get; set; }

    [JsonPropertyName("status")]
    public string? Status { get; set; }

    [JsonPropertyName("in_queue")]
    public int InQueue { get; set; }

    [JsonPropertyName("queue_priority")]
    public string? QueuePriority { get; set; }

    [JsonPropertyName("sha1")]
    public string? Sha1 { get; set; }

    [JsonPropertyName("sha256")]
    public string? Sha256 { get; set; }
}
