using TB.WEBAPP.SUBMITMOTOR.APPLICATION.DTOs;
using TB.WEBAPP.SUBMITMOTOR.APPLICATION.DTOs.Requests.Payments.KBanks;
using TB.WEBAPP.SUBMITMOTOR.APPLICATION.DTOs.Responses.Payments.KBanks;

namespace TB.WEBAPP.SUBMITMOTOR.APPLICATION.Interfaces.Payments
{
    public interface IPaymentKBankService
    {
        Task<ApiResponseDto<CreateOrderResponse>> CreateOrderPayment(CreateOrderRequest request);

        Task<ApiResponseDto<InquiryOrderResponse>> InquiryOrder(string orderId);

        Task<ApiResponseDto<InquiryOrderResponse>> InquiryQrTransaction(string id);
    }
}