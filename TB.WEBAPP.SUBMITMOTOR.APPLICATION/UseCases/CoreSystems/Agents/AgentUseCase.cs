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

        /// <summary>
        /// Retrieves detailed information about agents based on the specified request parameters.
        /// </summary>
        /// <remarks>This method sends a POST request to the core system service to fetch agent details.
        /// Ensure that the <paramref name="request"/> object is properly populated with valid criteria  before calling
        /// this method.</remarks>
        /// <param name="request">The request object containing criteria for fetching agent details.  This parameter cannot be null.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains an  <see
        /// cref="ApiResponseDto{T}"/> object with a list of <see cref="AgentDetailResponse"/>  representing the agent
        /// details.</returns>
        public async Task<ApiResponseDto<List<AgentDetailResponse>>> FetchAgentDetail(AgentDetailRequest request)
        {
            var result = await _apiClientService.PostAsync<AgentDetailRequest, List<AgentDetailResponse>>(_coreSystemService, "/api/agent/fetch/detail", request);
            // เข้าถึง data
            return result;
        }
    }
}
