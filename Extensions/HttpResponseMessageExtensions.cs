using System.Text.Json;

public static class HttpResponseMessageExtensions
{
    public static async Task<ApiResponse<TSuccess, TError>> MapToApiResponseAsync<TSuccess, TError>(this HttpResponseMessage response)
    {
        var content = await response.Content.ReadAsStringAsync();

        if (response.IsSuccessStatusCode)
        {
            var result = JsonSerializer.Deserialize<TSuccess>(content);
            return ApiResponse<TSuccess, TError>.Success(result);
        }
        else
        {
            var error = JsonSerializer.Deserialize<TError>(content);
            return ApiResponse<TSuccess, TError>.Error(error);
        }
    }
}