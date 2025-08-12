using TB.WEBAPP.SUBMITMOTOR.APPLICATION.DTOs;
using TB.WEBAPP.SUBMITMOTOR.APPLICATION.DTOs.Requests.CoreSystems.Quotations;
using TB.WEBAPP.SUBMITMOTOR.APPLICATION.DTOs.Responses.CoreSystems.Quotations;
using TB.WEBAPP.SUBMITMOTOR.APPLICATION.Interfaces;
using TB.WEBAPP.SUBMITMOTOR.APPLICATION.Interfaces.CoreSystems.Quotations;

namespace TB.WEBAPP.SUBMITMOTOR.APPLICATION.UseCases.CoreSystems.Quotations
{
    public class QuotationUseCase(IApiClientService apiClientService) : IQuotationUseCase
    {
        private readonly IApiClientService _apiClientService = apiClientService;
        private readonly string _coreSystemService = "CoreSystemService";

        public async Task<ApiResponseDto<List<QuotationDetailResponse>>> FetchQuotationDetail(QuotationDetailRequest request)
        {
            var result = await _apiClientService.PostAsync<QuotationDetailRequest, List<QuotationDetailResponse>>(_coreSystemService, "/api/quotation/fetch/detail", request);
            return result;
        }
    }
}
