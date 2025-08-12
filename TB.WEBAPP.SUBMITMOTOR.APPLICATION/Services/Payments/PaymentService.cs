using TB.WEBAPP.SUBMITMOTOR.APPLICATION.DTOs;
using TB.WEBAPP.SUBMITMOTOR.APPLICATION.DTOs.Requests.Payments.Kbanks;
using TB.WEBAPP.SUBMITMOTOR.APPLICATION.DTOs.Responses.Payments.Kbanks;
using TB.WEBAPP.SUBMITMOTOR.APPLICATION.Interfaces;
using TB.WEBAPP.SUBMITMOTOR.APPLICATION.Interfaces.Payments;

namespace TB.WEBAPP.SUBMITMOTOR.APPLICATION.Services.Payments
{
    public class PaymentService(IApiClientService apiClientService) : IPaymentService
    {
        private readonly IApiClientService _apiClientService = apiClientService;
        private readonly string _paymentService = "PaymentService";

        public async Task<ApiResponseDto<CreateOrderResponse>> CreatePayment(CreateOrderRequest request)
        {
            var result = await _apiClientService.PostAsync<CreateOrderRequest, CreateOrderResponse>(_paymentService, "/api/payment/kbank/qr/create/order", request);
            return result;
        }
    }
}
