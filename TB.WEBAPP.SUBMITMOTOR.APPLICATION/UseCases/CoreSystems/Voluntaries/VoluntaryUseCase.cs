using TB.WEBAPP.SUBMITMOTOR.APPLICATION.DTOs;
using TB.WEBAPP.SUBMITMOTOR.APPLICATION.DTOs.Requests.CoreSystems.Voluntaries;
using TB.WEBAPP.SUBMITMOTOR.APPLICATION.DTOs.Responses.CoreSystems.Voluntaries;
using TB.WEBAPP.SUBMITMOTOR.APPLICATION.Interfaces;
using TB.WEBAPP.SUBMITMOTOR.APPLICATION.Interfaces.CoreSystems.Voluntaries;

namespace TB.WEBAPP.SUBMITMOTOR.APPLICATION.UseCases.CoreSystems.Voluntaries
{
    public class VoluntaryUseCase(IApiClientService apiClientService) : IVoluntaryUseCase
    {
        private readonly IApiClientService _apiClientService = apiClientService;
        private readonly string _coreSystemService = "CoreSystemService";

        public async Task<ApiResponseDto<IEnumerable<VoluntaryCommissionResponse>>> FetchVoluntaryCommission(VoluntaryCommissionRequest request)
        {
            var result = await _apiClientService.PostAsync<VoluntaryCommissionRequest, IEnumerable<VoluntaryCommissionResponse>>(_coreSystemService, "/api/voluntary/fetch/commission", request);
            return result;
        }
    }
}
