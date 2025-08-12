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

        /// <summary>
        /// Fetches the installment calculation periods based on the provided request.
        /// </summary>
        /// <remarks>This method sends a POST request to the core system service to retrieve installment
        /// calculation periods. Ensure that the <paramref name="request"/> object is properly populated with valid data
        /// before calling this method.</remarks>
        /// <param name="request">The request object containing the parameters required to fetch the installment calculation periods. This
        /// parameter cannot be null.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains an  <see
        /// cref="ApiResponseDto{T}"/> object with a list of <see cref="InstallmentCalculatePeriodResponse"/> 
        /// representing the installment calculation periods.</returns>
        public async Task<ApiResponseDto<List<InstallmentCalculatePeriodResponse>>> FetchCalculatePeriod(InstallmentCalculatePeriodRequest request)
        {
            var result = await _apiClientService.PostAsync<InstallmentCalculatePeriodRequest, List<InstallmentCalculatePeriodResponse>>(_coreSystemService, "/api/installment/fetch/calculate/period", request);
            return result;
        }
    }
}
