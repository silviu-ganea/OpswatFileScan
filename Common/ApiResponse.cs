public class ApiResponse<TSuccess, TError>
{
    public TSuccess? SuccessResult { get; private set; }
    public TError? ErrorResult { get; private set; }
    public bool IsSuccess { get; private set; }
    public int ErrorCode { get; private set; }

    public static ApiResponse<TSuccess, TError> Success(TSuccess? result)
    {
        return new ApiResponse<TSuccess, TError> { SuccessResult = result, IsSuccess = true };
    } 

    public static ApiResponse<TSuccess, TError> Error(TError? error, int errorCode = FileScannerConstants.DefaultErrorCode)
    {
        return new ApiResponse<TSuccess, TError> { ErrorResult = error, IsSuccess = false, ErrorCode = errorCode  };
    }
}
