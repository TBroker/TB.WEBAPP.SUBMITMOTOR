using TB.WEBAPP.SUBMITMOTOR.APPLICATION.DTOs;
using TB.WEBAPP.SUBMITMOTOR.APPLICATION.DTOs.Requests.Data.Installments;
using TB.WEBAPP.SUBMITMOTOR.APPLICATION.DTOs.Responses.Data.Installments;

namespace TB.WEBAPP.SUBMITMOTOR.APPLICATION.Interfaces.Data.Installments
{
    public interface IPaymentInstallmentUseCase
    {
        Task<ApiResponseDto<bool>> CreateNotifyInstallmentContract(WebNotifyInstallmentContract request);

        Task<ApiResponseDto<bool>> CreatePaymentInstallment(WebPaymentInstallment request);

        Task<ApiResponseDto<IEnumerable<WebNotifyInstallmentContract>>> FetchNotifyInstallmentContract(NotifyInstallmentContractRequest request);

        Task<ApiResponseDto<IEnumerable<WebPaymentInstallment>>> FetchPaymentInstallment(PaymentInstallmentRequest request);

        Task<ApiResponseDto<bool>> ModifyNotifyInstallmentContract(WebNotifyInstallmentContract request);

        Task<ApiResponseDto<bool>> RemoveNotifyInstallmentContract(WebNotifyInstallmentContract request);

        Task<ApiResponseDto<bool>> RemovePaymentInstallment(WebPaymentInstallment request);
    }
}