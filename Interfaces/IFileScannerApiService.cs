public interface IFileScannerApiService
{
    Task<HttpResponseMessage> GetScanReportByHashAsync(string hash);
    Task<HttpResponseMessage> PostFileForScanAsync(string filePath);
    Task<HttpResponseMessage> PollScanResultsAsync(string dataId, CancellationToken cancellationToken);
}
