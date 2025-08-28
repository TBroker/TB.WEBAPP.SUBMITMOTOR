using TB.WEBAPP.SUBMITMOTOR.APPLICATION.DTOs;
using TB.WEBAPP.SUBMITMOTOR.APPLICATION.DTOs.Requests.CoreSystems.Agents;
using TB.WEBAPP.SUBMITMOTOR.APPLICATION.DTOs.Responses.CoreSystems.Agents;
using TB.WEBAPP.SUBMITMOTOR.APPLICATION.Interfaces;
using TB.WEBAPP.SUBMITMOTOR.APPLICATION.Interfaces.CoreSystem.Agents;

namespace TB.WEBAPP.SUBMITMOTOR.APPLICATION.UseCases.CoreSystems.Agents
{
    public class AgentUseCase(IApiClientService apiClientService) : IAgentUseCase
    {
        private readonly IApiClientService _apiClientService = apiClientService;
        private readonly string _coreSystemService = "CoreSystemService";

        public async Task<ApiResponseDto<IEnumerable<AgentDetailResponse>>> FetchAgentDetail(AgentDetailRequest request)
        {
            var result = await _apiClientService.PostAsync<AgentDetailRequest, IEnumerable<AgentDetailResponse>>(_coreSystemService, "/api/agent/fetch/detail", request);
            // เข้าถึง data
            return result;
        }
    }
}
