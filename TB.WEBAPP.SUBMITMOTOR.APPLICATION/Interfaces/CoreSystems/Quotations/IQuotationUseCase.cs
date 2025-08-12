using TB.WEBAPP.SUBMITMOTOR.APPLICATION.DTOs;
using TB.WEBAPP.SUBMITMOTOR.APPLICATION.DTOs.Requests.CoreSystems.Quotations;
using TB.WEBAPP.SUBMITMOTOR.APPLICATION.DTOs.Responses.CoreSystems.Quotations;

namespace TB.WEBAPP.SUBMITMOTOR.APPLICATION.Interfaces.CoreSystems.Quotations
{
    public interface IQuotationUseCase
    {
        Task<ApiResponseDto<List<QuotationDetailResponse>>> FetchQuotationDetail(QuotationDetailRequest request);
    }
}
