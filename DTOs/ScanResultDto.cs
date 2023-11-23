using System.Text.Json.Serialization;

public class ScanResultDto
{
    [JsonPropertyName("process_info")]
    public ProcessInfo ProcessInfo { get; set; } = new ProcessInfo();

    [JsonPropertyName("scan_results")]
    public ScanResults ScanResults { get; set; } = new ScanResults();

    [JsonPropertyName("file_info")]
    public FileInfo FileInfo { get; set; } = new FileInfo();
}

public class ProcessInfo
{
    [JsonPropertyName("progress_percentage")]
    public int ProgressPercentage { get; set; }

    [JsonPropertyName("result")]
    public string Result { get; set; } = string.Empty;
}

public class ScanResults
{
    [JsonPropertyName("scan_details")]
    public Dictionary<string, ScanDetail> ScanDetails { get; set; } = new Dictionary<string, ScanDetail>();

    [JsonPropertyName("progress_percentage")]
    public int ProgressPercentage { get; set; }
}

public class ScanDetail
{
    [JsonPropertyName("scan_result_i")]
    public int ScanResultI { get; set; }

    [JsonPropertyName("def_time")]
    public string DefTime { get; set; } = string.Empty;
}

public class FileInfo
{
    [JsonPropertyName("display_name")]
    public string DisplayName { get; set; } = string.Empty;
}
