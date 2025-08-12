using TB.WEBAPP.SUBMITMOTOR.APPLICATION.DTOs;

namespace TB.WEBAPP.SUBMITMOTOR.APPLICATION.Interfaces
{
    public interface IApiClientService
    {
        Task<ApiResponseDto<TResponse>> GetAsync<TResponse>(string clientName, string endpoint);

        Task<ApiResponseDto<TResponse>> PostAsync<TRequest, TResponse>(string clientName, string endpoint, TRequest request);

        Task<ApiResponseDto<TResponse>> PostAsync<TResponse>(string clientName, string endpoint);

        Task<byte[]> GetFileAsync(string clientName, string endpoint);

        Task<byte[]> PostFileAsync<TRequest>(string clientName, string endpoint, TRequest request);
    }
}
