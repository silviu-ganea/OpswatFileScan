using System.Text.Json;
using Microsoft.Extensions.Configuration;

public class OpswatFileScannerApiService : IFileScannerApiService
{
    private readonly HttpClient _httpClient;

    private readonly string? _baseUri;

    public OpswatFileScannerApiService(HttpClient httpClient, IConfiguration configuration)
    {
        _httpClient = httpClient;
        _baseUri = configuration["OpswatApi:BaseUri"];
    }

    public async Task<HttpResponseMessage> GetScanReportByHashAsync(string hash)
    {
        var requestUri = $"{_baseUri}/hash/{hash}";
        return await _httpClient.GetAsync(requestUri);
    }

    public async Task<HttpResponseMessage> PostFileForScanAsync(string filePath)
    {
        var requestUri = $"{_baseUri}/file";
        var content = new MultipartFormDataContent();

        using var fileStream = File.OpenRead(filePath);
        content.Add(new StreamContent(fileStream), "file", Path.GetFileName(filePath));

        
        return await _httpClient.PostAsync(requestUri, content);
    }

    public async Task<HttpResponseMessage> PollScanResultsAsync(string dataId, CancellationToken cancellationToken)
    {
        var requestUri = $"{_baseUri}/file/{dataId}";
        _httpClient.DefaultRequestHeaders.Add("x-file-metadata", "1");

        while (!cancellationToken.IsCancellationRequested)
        {
            var response = await _httpClient.GetAsync(requestUri, cancellationToken);
            if (!response.IsSuccessStatusCode)
            {
                return response;
            }

            var content = await response.Content.ReadAsStringAsync(cancellationToken);
            var scanResult = JsonSerializer.Deserialize<ScanResultDto>(content);

            if (scanResult != null && scanResult.ScanResults.ProgressPercentage == FileScannerConstants.ProgressComplete)
            {
                return response;
            }

            await Task.Delay(FileScannerConstants.PollingIntervalMilliseconds, cancellationToken);
        }

        throw new OperationCanceledException("Polling cancelled.");
    }
}
