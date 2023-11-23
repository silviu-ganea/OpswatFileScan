public class FileScanResult
{
    public bool IsSuccess { get; set; }
    public string ErrorMessage { get; set; } = string.Empty;
    public ScanResultDto? ScanResult { get; set; }

    public static FileScanResult Success(ScanResultDto scanResult)
    {
        return new FileScanResult { IsSuccess = true, ScanResult = scanResult };
    }

    public static FileScanResult Failure(string message)
    {
        return new FileScanResult { IsSuccess = false, ErrorMessage = message };
    }
}
