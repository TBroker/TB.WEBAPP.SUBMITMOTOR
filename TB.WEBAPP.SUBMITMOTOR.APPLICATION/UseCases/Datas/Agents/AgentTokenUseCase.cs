using TB.WEBAPP.SUBMITMOTOR.APPLICATION.DTOs;
using TB.WEBAPP.SUBMITMOTOR.APPLICATION.DTOs.Responses.Data.Agents;
using TB.WEBAPP.SUBMITMOTOR.APPLICATION.Interfaces;
using TB.WEBAPP.SUBMITMOTOR.APPLICATION.Interfaces.Data.Agents;

namespace TB.WEBAPP.SUBMITMOTOR.APPLICATION.UseCases.Data.Agents
{
    public class AgentTokenUseCase(IApiClientService apiClientService) : IAgentTokenUseCase
    {
        private readonly IApiClientService _apiClientService = apiClientService;
        private readonly string _serviceName = "DataService";

        public async Task<ApiResponseDto<IEnumerable<WebUserAgent>>> FetchUserAgentByUserId(string userId)
        {
            var result = await _apiClientService.GetAsync<IEnumerable<WebUserAgent>>(_serviceName, $"/api/agent/fetch/user/{userId}");
            return result;
        }

        public async Task<ApiResponseDto<IEnumerable<WebUserAgentToken>>> FetchUserAgentTokenByUserId(string userId)
        {
            var result = await _apiClientService.GetAsync<IEnumerable<WebUserAgentToken>>(_serviceName, $"/api/agent/fetch/token/{userId}");
            return result;
        }
    }
}