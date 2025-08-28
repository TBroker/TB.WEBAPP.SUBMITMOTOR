using TB.WEBAPP.SUBMITMOTOR.APPLICATION.DTOs;
using TB.WEBAPP.SUBMITMOTOR.APPLICATION.DTOs.Requests.Payments.KBanks;
using TB.WEBAPP.SUBMITMOTOR.APPLICATION.DTOs.Responses.Payments.KBanks;
using TB.WEBAPP.SUBMITMOTOR.APPLICATION.Interfaces;
using TB.WEBAPP.SUBMITMOTOR.APPLICATION.Interfaces.Payments;

namespace TB.WEBAPP.SUBMITMOTOR.APPLICATION.Services.Payments
{
    public class PaymentKBankService(IApiClientService apiClientService) : IPaymentKBankService
    {
        private readonly IApiClientService _apiClientService = apiClientService;
        private readonly string _paymentService = "PaymentService";

        public async Task<ApiResponseDto<CreateOrderResponse>> CreateOrderPayment(CreateOrderRequest request)
        {
            var result = await _apiClientService.PostAsync<CreateOrderRequest, CreateOrderResponse>(_paymentService, "/api/payment/kbank/qr/create/order", request);
            return result;
        }

        public async Task<ApiResponseDto<InquiryOrderResponse>> InquiryOrder(string orderId)
        {
            var result = await _apiClientService.GetAsync<InquiryOrderResponse>(_paymentService, $"/api/payment/kbank/qr/inquiry/order/{orderId}");
            return result;
        }

        public async Task<ApiResponseDto<InquiryOrderResponse>> InquiryQrTransaction(string id)
        {
            var result = await _apiClientService.GetAsync<InquiryOrderResponse>(_paymentService, $"/api/payment/kbank/qr/inquiry/transaction/{id}");
            return result;
        }
    }
}
