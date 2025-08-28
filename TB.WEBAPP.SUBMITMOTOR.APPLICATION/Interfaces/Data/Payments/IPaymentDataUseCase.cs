using TB.WEBAPP.SUBMITMOTOR.APPLICATION.DTOs;
using TB.WEBAPP.SUBMITMOTOR.APPLICATION.DTOs.Entities;
using TB.WEBAPP.SUBMITMOTOR.APPLICATION.DTOs.Requests.Data.Payments;

namespace TB.WEBAPP.SUBMITMOTOR.APPLICATION.Interfaces.Data.Payments
{
    public interface IPaymentDataUseCase
    {
        Task<ApiResponseDto<bool>> CreatePaymentConfirm(WebPaymentConfirm request);

        Task<ApiResponseDto<IEnumerable<WebPaymentConfirm>>> FetchPaymentConfirm(PaymentConfirmRequest request);

        Task<ApiResponseDto<bool>> ModifyPaymentConfirm(WebPaymentConfirm request);

        Task<ApiResponseDto<bool>> RemovePaymentConfirmation(WebPaymentConfirm request);
    }
}