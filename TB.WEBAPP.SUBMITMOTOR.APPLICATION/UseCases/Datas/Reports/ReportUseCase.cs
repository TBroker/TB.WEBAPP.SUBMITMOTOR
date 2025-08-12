using TB.WEBAPP.SUBMITMOTOR.APPLICATION.DTOs;
using TB.WEBAPP.SUBMITMOTOR.APPLICATION.DTOs.Requests.Datas.Reports;
using TB.WEBAPP.SUBMITMOTOR.APPLICATION.DTOs.Responses.Datas.Reports;
using TB.WEBAPP.SUBMITMOTOR.APPLICATION.Interfaces;
using TB.WEBAPP.SUBMITMOTOR.APPLICATION.Interfaces.Datas.Reports;

namespace TB.WEBAPP.SUBMITMOTOR.APPLICATION.UseCases.Datas.Reports
{
    public class ReportUseCase(IApiClientService apiClientService) : IReportUseCase
    {
        private readonly IApiClientService _apiClientService = apiClientService;
        private readonly string _serviceName = "DataService";

        public async Task<ApiResponseDto<List<QuotationListResponse>>> FetchQuotationReportList(QuotationListRequest request)
        {
            var result = await _apiClientService.PostAsync<QuotationListRequest, List<QuotationListResponse>>(_serviceName, "/api/report/quotation/motor", request);
            return result;
        }

        public async Task<ApiResponseDto<List<QuotationDetailResponse>>> FetchQuotationReportDetail(QuotationDetailRequest request)
        {
            var result = await _apiClientService.PostAsync<QuotationDetailRequest, List<QuotationDetailResponse>>(_serviceName, "/api/report/quotation/detail", request);
            return result;
        }
    }
}