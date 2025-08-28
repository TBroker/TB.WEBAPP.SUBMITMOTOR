using TB.WEBAPP.SUBMITMOTOR.APPLICATION.DTOs;
using TB.WEBAPP.SUBMITMOTOR.APPLICATION.DTOs.Requests.Data.Reports;
using TB.WEBAPP.SUBMITMOTOR.APPLICATION.DTOs.Responses.Data.Reports;
using TB.WEBAPP.SUBMITMOTOR.APPLICATION.Interfaces;
using TB.WEBAPP.SUBMITMOTOR.APPLICATION.Interfaces.Data.Reports;

namespace TB.WEBAPP.SUBMITMOTOR.APPLICATION.UseCases.Data.Reports
{
    public class ReportUseCase(IApiClientService apiClientService) : IReportUseCase
    {
        private readonly IApiClientService _apiClientService = apiClientService;
        private readonly string _serviceName = "DataService";

        public async Task<ApiResponseDto<IEnumerable<QuotationListResponse>>> FetchQuotationReportList(QuotationListRequest request)
        {
            var result = await _apiClientService.PostAsync<QuotationListRequest, IEnumerable<QuotationListResponse>>(_serviceName, "/api/report/quotation/motor", request);
            return result;
        }

        public async Task<ApiResponseDto<IEnumerable<QuotationDetailResponse>>> FetchQuotationReportDetail(QuotationDetailRequest request)
        {
            var result = await _apiClientService.PostAsync<QuotationDetailRequest, IEnumerable<QuotationDetailResponse>>(_serviceName, "/api/report/quotation/detail", request);
            return result;
        }
    }
}