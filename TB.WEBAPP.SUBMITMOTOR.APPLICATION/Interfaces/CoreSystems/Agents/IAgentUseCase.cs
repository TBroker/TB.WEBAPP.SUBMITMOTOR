using TB.WEBAPP.SUBMITMOTOR.APPLICATION.DTOs;
using TB.WEBAPP.SUBMITMOTOR.APPLICATION.DTOs.Requests.CoreSystems.Agents;
using TB.WEBAPP.SUBMITMOTOR.APPLICATION.DTOs.Responses.CoreSystems.Agents;

namespace TB.WEBAPP.SUBMITMOTOR.APPLICATION.Interfaces.CoreSystem.Agents
{
    public interface IAgentUseCase
    {
        Task<ApiResponseDto<List<AgentDetailResponse>>> FetchAgentDetail(AgentDetailRequest request);
    }
}
