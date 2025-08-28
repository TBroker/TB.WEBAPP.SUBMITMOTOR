using TB.WEBAPP.SUBMITMOTOR.APPLICATION.DTOs;
using TB.WEBAPP.SUBMITMOTOR.APPLICATION.DTOs.Requests.Data.Premiums;
using TB.WEBAPP.SUBMITMOTOR.APPLICATION.DTOs.Responses.Data.Premiums;

namespace TB.WEBAPP.SUBMITMOTOR.APPLICATION.Interfaces.Data.Premiums
{
    public interface IPremiumUseCase
    {
        Task<ApiResponseDto<List<MasterPlanResponse>>> FetchMasterPlanDetail(MasterPlanRequest request);
    }
}
