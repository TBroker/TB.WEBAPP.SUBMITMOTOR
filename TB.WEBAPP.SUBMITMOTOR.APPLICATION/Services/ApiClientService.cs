using Microsoft.Extensions.Logging;
using System.Text;
using System.Text.Json;
using TB.WEBAPP.SUBMITMOTOR.APPLICATION.DTOs;
using TB.WEBAPP.SUBMITMOTOR.APPLICATION.Interfaces;

namespace TB.WEBAPP.SUBMITMOTOR.APPLICATION.Services
{
    public class ApiClientService(IHttpClientFactory httpClientFactory, ILogger<ApiClientService> logger) : IApiClientService
    {
        private readonly IHttpClientFactory _httpClientFactory = httpClientFactory;
        private readonly ILogger<ApiClientService> _logger = logger;

        public async Task<ApiResponseDto<TResponse>> GetAsync<TResponse>(string clientName, string endpoint)
        {
            try
            {
                var client = _httpClientFactory.CreateClient(clientName);
                var response = await client.GetAsync(endpoint);
                var content = await response.Content.ReadAsStringAsync();

                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };

                var apiResponse = JsonSerializer.Deserialize<ApiResponseDto<TResponse>>(content, options);

                return apiResponse!;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "HttpRequestException occurred while calling API: {Message}", ex.Message);
                throw new HttpRequestException($"HttpRequestException occurred: {ex.Message}", ex);
            }
        }

        public async Task<ApiResponseDto<TResponse>> PostAsync<TRequest, TResponse>(string clientName, string endpoint, TRequest request)
        {
            try
            {
                var client = _httpClientFactory.CreateClient(clientName);
                var json = JsonSerializer.Serialize(request);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                var response = await client.PostAsync(endpoint, content);
                var responseContent = await response.Content.ReadAsStringAsync();

                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };

                var apiResponse = JsonSerializer.Deserialize<ApiResponseDto<TResponse>>(responseContent, options);

                return apiResponse!;
            }
            catch (Exception ex)
            {
                // Log หรือจัดการข้อผิดพลาดจากการเรียก API
                _logger.LogError(ex, "HttpRequestException occurred while calling API: {Message}", ex.Message);
                throw new HttpRequestException($"HttpRequestException occurred: {ex.Message}", ex);
            }
        }

        public async Task<ApiResponseDto<TResponse>> PostAsync<TResponse>(string clientName, string endpoint)
        {
            try
            {
                var client = _httpClientFactory.CreateClient(clientName);
                var content = new StringContent("", Encoding.UTF8, "application/json");
                var response = await client.PostAsync(endpoint, content);
                var responseContent = await response.Content.ReadAsStringAsync();

                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };

                var apiResponse = JsonSerializer.Deserialize<ApiResponseDto<TResponse>>(responseContent, options);

                return apiResponse!;
            }
            catch (HttpRequestException ex)
            {
                // Log หรือจัดการข้อผิดพลาดจากการเรียก API
                _logger.LogError(ex, "HttpRequestException occurred while calling API: {Message}", ex.Message);
                throw new HttpRequestException($"HttpRequestException occurred: {ex.Message}", ex);
            }
        }


        public async Task<byte[]> GetFileAsync(string clientName, string endpoint)
        {
            try
            {
                var client = _httpClientFactory.CreateClient(clientName);
                var response = await client.GetAsync(endpoint);

                response.EnsureSuccessStatusCode();

                return await response.Content.ReadAsByteArrayAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while calling file API: {Message}", ex.Message);
                throw new HttpRequestException($"HttpRequestException occurred: {ex.Message}", ex);
            }
        }

        public async Task<byte[]> PostFileAsync<TRequest>(string clientName, string endpoint, TRequest request)
        {
            try
            {
                var client = _httpClientFactory.CreateClient(clientName);
                var json = JsonSerializer.Serialize(request);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                var response = await client.PostAsync(endpoint, content);
                response.EnsureSuccessStatusCode();
                return await response.Content.ReadAsByteArrayAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while calling file API: {Message}", ex.Message);
                throw new HttpRequestException($"HttpRequestException occurred: {ex.Message}", ex);
            }
        }

    }
}