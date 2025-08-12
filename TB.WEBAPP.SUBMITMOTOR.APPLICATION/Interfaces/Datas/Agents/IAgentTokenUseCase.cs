using TB.WEBAPP.SUBMITMOTOR.APPLICATION.DTOs;
using TB.WEBAPP.SUBMITMOTOR.APPLICATION.DTOs.Responses.Datas.Agents;

namespace TB.WEBAPP.SUBMITMOTOR.APPLICATION.Interfaces.Datas.Agents
{
    public interface IAgentTokenUseCase
    {
        Task<ApiResponseDto<IEnumerable<WebUserAgent>>> FetchUserAgentByUserId(string userId);

        Task<ApiResponseDto<IEnumerable<WebUserAgentToken>>> FetchUserAgentTokenByUserId(string userId);
    }
}