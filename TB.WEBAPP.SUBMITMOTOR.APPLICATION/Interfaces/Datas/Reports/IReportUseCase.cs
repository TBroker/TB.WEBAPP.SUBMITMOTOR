using TB.WEBAPP.SUBMITMOTOR.APPLICATION.DTOs;
using TB.WEBAPP.SUBMITMOTOR.APPLICATION.DTOs.Requests.Datas.Reports;
using TB.WEBAPP.SUBMITMOTOR.APPLICATION.DTOs.Responses.Datas.Reports;

namespace TB.WEBAPP.SUBMITMOTOR.APPLICATION.Interfaces.Datas.Reports
{
    public interface IReportUseCase
    {
        Task<ApiResponseDto<List<QuotationListResponse>>> FetchQuotationReportList(QuotationListRequest request);

        Task<ApiResponseDto<List<QuotationDetailResponse>>> FetchQuotationReportDetail(QuotationDetailRequest request);
    }
}
