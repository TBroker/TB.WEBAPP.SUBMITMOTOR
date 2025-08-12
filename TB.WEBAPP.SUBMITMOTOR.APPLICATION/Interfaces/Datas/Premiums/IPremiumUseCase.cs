using TB.WEBAPP.SUBMITMOTOR.APPLICATION.DTOs;
using TB.WEBAPP.SUBMITMOTOR.APPLICATION.DTOs.Requests.Datas.Premiums;
using TB.WEBAPP.SUBMITMOTOR.APPLICATION.DTOs.Responses.Datas.Premiums;

namespace TB.WEBAPP.SUBMITMOTOR.APPLICATION.Interfaces.Datas.Premiums
{
    public interface IPremiumUseCase
    {
        Task<ApiResponseDto<List<MasterPlanResponse>>> FetchMasterPlanDetail(MasterPlanRequest request);
    }
}
