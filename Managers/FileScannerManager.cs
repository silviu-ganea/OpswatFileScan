public class FileScannerManager : IFileScannerManager
{
    private readonly IFileHashingService _fileHashingService;
    private readonly IFileScannerApiService _fileScannerApiService;

    public FileScannerManager(IFileHashingService fileHashingService, IFileScannerApiService fileScannerApiService)
    {
        _fileHashingService = fileHashingService;
        _fileScannerApiService = fileScannerApiService;
    }

    public async Task<FileScanResult> RunScanAsync(string filePath)
    {
        string sha256Hash = _fileHashingService.CalculateSHA256(filePath);
        var cachedResult = await TryGetCachedResultsAsync(sha256Hash);
        
        if (cachedResult.IsSuccess)
        {
            return FileScanResult.Success(cachedResult.SuccessResult!);
        }

        var uploadResult = await UploadFileAsync(filePath);
        if (uploadResult.IsSuccess)
        {
            string dataId = uploadResult.SuccessResult!;
            var pollResult = await PollForScanResultsAsync(dataId);
            if (pollResult.IsSuccess)
            {
                return FileScanResult.Success(pollResult.SuccessResult!);
            }
            else
            {
                return FileScanResult.Failure(pollResult.ErrorResult?.Error?.Messages?.FirstOrDefault() ?? "Error occurred while polling for scan results.");
            }
        }
        else
        {
            return FileScanResult.Failure(uploadResult.ErrorResult?.Error?.Messages?.FirstOrDefault() ?? "Error occurred while uploading file for scanning.");
        }
    }

    private async Task<ApiResponse<ScanResultDto, ErrorResponseDto>> TryGetCachedResultsAsync(string hash)
    {
        var httpResponse = await _fileScannerApiService.GetScanReportByHashAsync(hash);
        var response = await httpResponse.MapToApiResponseAsync<ScanResultDto, ErrorResponseDto>();

        if (response.IsSuccess)
        {
            return ApiResponse<ScanResultDto, ErrorResponseDto>.Success(response.SuccessResult);
        }
        else
        {
            return ApiResponse<ScanResultDto, ErrorResponseDto>.Error(response.ErrorResult, response.ErrorResult?.Error?.Code  ?? FileScannerConstants.DefaultErrorCode);
        }
    }

    private async Task<ApiResponse<string, ErrorResponseDto>> UploadFileAsync(string filePath)
    {
        var httpResponse = await _fileScannerApiService.PostFileForScanAsync(filePath);
        var response = await httpResponse.MapToApiResponseAsync<PostFileForScanResponseDto, ErrorResponseDto>();

        if (response.IsSuccess && response.SuccessResult != null)
        {
            return ApiResponse<string, ErrorResponseDto>.Success(response.SuccessResult.DataId);
        }
        else
        {
            return ApiResponse<string, ErrorResponseDto>.Error(response.ErrorResult, response.ErrorResult?.Error?.Code ?? FileScannerConstants.DefaultErrorCode);
        }
    }

    private async Task<ApiResponse<ScanResultDto, ErrorResponseDto>> PollForScanResultsAsync(string dataId)
    {
        using var cts = new CancellationTokenSource(FileScannerConstants.TotalPollingTimeoutMilliseconds);
        var httpResponse = await _fileScannerApiService.PollScanResultsAsync(dataId, cts.Token);
        var response = await httpResponse.MapToApiResponseAsync<ScanResultDto, ErrorResponseDto>();

        if (response.IsSuccess)
        {
            return ApiResponse<ScanResultDto, ErrorResponseDto>.Success(response.SuccessResult);
        }
        else
        {
            var errorCode = response.ErrorResult?.Error?.Code ?? FileScannerConstants.DefaultErrorCode;
            return ApiResponse<ScanResultDto, ErrorResponseDto>.Error(response.ErrorResult, errorCode);
        }
    }
}
