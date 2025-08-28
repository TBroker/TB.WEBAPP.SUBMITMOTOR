using TB.WEBAPP.SUBMITMOTOR.APPLICATION.DTOs;
using TB.WEBAPP.SUBMITMOTOR.APPLICATION.DTOs.Requests.Data.Reports;
using TB.WEBAPP.SUBMITMOTOR.APPLICATION.DTOs.Responses.Data.Reports;

namespace TB.WEBAPP.SUBMITMOTOR.APPLICATION.Interfaces.Data.Reports
{
    public interface IReportUseCase
    {
        Task<ApiResponseDto<IEnumerable<QuotationListResponse>>> FetchQuotationReportList(QuotationListRequest request);

        Task<ApiResponseDto<IEnumerable<QuotationDetailResponse>>> FetchQuotationReportDetail(QuotationDetailRequest request);
    }
}
