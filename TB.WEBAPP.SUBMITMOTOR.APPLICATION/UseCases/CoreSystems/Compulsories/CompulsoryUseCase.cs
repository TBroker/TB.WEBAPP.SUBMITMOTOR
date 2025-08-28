using TB.WEBAPP.SUBMITMOTOR.APPLICATION.DTOs;
using TB.WEBAPP.SUBMITMOTOR.APPLICATION.DTOs.Requests.CoreSystems.Compulsories;
using TB.WEBAPP.SUBMITMOTOR.APPLICATION.DTOs.Responses.CoreSystems.Compulsories;
using TB.WEBAPP.SUBMITMOTOR.APPLICATION.Interfaces;
using TB.WEBAPP.SUBMITMOTOR.APPLICATION.Interfaces.CoreSystems.Compulsories;

namespace TB.WEBAPP.SUBMITMOTOR.APPLICATION.UseCases.CoreSystems.Compulsories
{
    public class CompulsoryUseCase(IApiClientService apiClientService) : ICompulsoryUseCase
    {
        private readonly IApiClientService _apiClientService = apiClientService;
        private readonly string _coreSystemService = "CoreSystemService";

        public async Task<ApiResponseDto<IEnumerable<CompulsoryPremiumResponse>>> FetchCompulsoryCommission(CompulsoryCommissionRequest request)
        {
            var result = await _apiClientService.PostAsync<CompulsoryCommissionRequest, IEnumerable<CompulsoryPremiumResponse>>(_coreSystemService, "/api/compulsory/fetch/commission", request);
            return result;
        }
    }
}
