using TB.WEBAPP.SUBMITMOTOR.APPLICATION.DTOs;
using TB.WEBAPP.SUBMITMOTOR.APPLICATION.DTOs.Requests.Data.Premiums;
using TB.WEBAPP.SUBMITMOTOR.APPLICATION.DTOs.Responses.Data.Premiums;
using TB.WEBAPP.SUBMITMOTOR.APPLICATION.Interfaces;
using TB.WEBAPP.SUBMITMOTOR.APPLICATION.Interfaces.Data.Premiums;

namespace TB.WEBAPP.SUBMITMOTOR.APPLICATION.UseCases.Data.Premiums
{
    public class PremiumUseCase(IApiClientService apiClientService) :IPremiumUseCase
    {
        private readonly IApiClientService _apiClientService = apiClientService;
        private readonly string _serviceName = "DataService";
        public async Task<ApiResponseDto<List<MasterPlanResponse>>> FetchMasterPlanDetail(MasterPlanRequest request)
        {
            var result = await _apiClientService.PostAsync<MasterPlanRequest, List<MasterPlanResponse>>(_serviceName, "/api/premium/fetch/masterplan/detail", request);
            return result;
        }
    }
}
