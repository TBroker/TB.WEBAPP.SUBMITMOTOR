using TB.WEBAPP.SUBMITMOTOR.APPLICATION.DTOs;
using TB.WEBAPP.SUBMITMOTOR.APPLICATION.DTOs.Requests.CoreSystems.Installments;
using TB.WEBAPP.SUBMITMOTOR.APPLICATION.DTOs.Responses.CoreSystems.Installments;
using TB.WEBAPP.SUBMITMOTOR.APPLICATION.Interfaces;
using TB.WEBAPP.SUBMITMOTOR.APPLICATION.Interfaces.CoreSystems.Installments;

namespace TB.WEBAPP.SUBMITMOTOR.APPLICATION.UseCases.CoreSystems.Installments
{
    public class InstallmentUseCase(IApiClientService apiClientService) : IInstallmentUseCase
    {
        private readonly IApiClientService _apiClientService = apiClientService;
        private readonly string _coreSystemService = "CoreSystemService";

        public async Task<ApiResponseDto<IEnumerable<InstallmentCalculatePeriodResponse>>> FetchCalculatePeriod(InstallmentCalculatePeriodRequest request)
        {
            var result = await _apiClientService.PostAsync<InstallmentCalculatePeriodRequest, IEnumerable<InstallmentCalculatePeriodResponse>>(_coreSystemService, "/api/installment/fetch/calculate/period", request);
            return result;
        }
    }
}
