using TB.WEBAPP.SUBMITMOTOR.APPLICATION.DTOs;
using TB.WEBAPP.SUBMITMOTOR.APPLICATION.DTOs.Requests.Datas.Installments;
using TB.WEBAPP.SUBMITMOTOR.APPLICATION.DTOs.Responses.Datas.Installments;

namespace TB.WEBAPP.SUBMITMOTOR.APPLICATION.Interfaces.Datas.Installments
{
    public interface IPaymentInstallmentUseCase
    {
        Task<ApiResponseDto<IEnumerable<WebPaymentInstallment>>> FetchPaymentInstallment(PaymentInstallmentRequest request);

        Task<ApiResponseDto<IEnumerable<bool>>> CreatePaymentInstallment(WebPaymentInstallment request);

        Task<ApiResponseDto<IEnumerable<bool>>> RemovePaymentInstallment(WebPaymentInstallment request);
    }
}