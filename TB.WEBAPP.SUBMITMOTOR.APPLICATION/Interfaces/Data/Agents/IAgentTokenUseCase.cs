using TB.WEBAPP.SUBMITMOTOR.APPLICATION.DTOs;
using TB.WEBAPP.SUBMITMOTOR.APPLICATION.DTOs.Responses.Data.Agents;

namespace TB.WEBAPP.SUBMITMOTOR.APPLICATION.Interfaces.Data.Agents
{
    public interface IAgentTokenUseCase
    {
        Task<ApiResponseDto<IEnumerable<WebUserAgent>>> FetchUserAgentByUserId(string userId);

        Task<ApiResponseDto<IEnumerable<WebUserAgentToken>>> FetchUserAgentTokenByUserId(string userId);
    }
}