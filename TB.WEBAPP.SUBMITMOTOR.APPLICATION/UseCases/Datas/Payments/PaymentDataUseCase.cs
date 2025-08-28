using TB.WEBAPP.SUBMITMOTOR.APPLICATION.DTOs;
using TB.WEBAPP.SUBMITMOTOR.APPLICATION.DTOs.Entities;
using TB.WEBAPP.SUBMITMOTOR.APPLICATION.DTOs.Requests.Data.Payments;
using TB.WEBAPP.SUBMITMOTOR.APPLICATION.Interfaces;
using TB.WEBAPP.SUBMITMOTOR.APPLICATION.Interfaces.Data.Payments;

namespace TB.WEBAPP.SUBMITMOTOR.APPLICATION.UseCases.Data.Payments
{
    public class PaymentDataUseCase(IApiClientService apiClientService) : IPaymentDataUseCase
    {
        private readonly IApiClientService _apiClientService = apiClientService;
        private readonly string _serviceName = "DataService";

        public async Task<ApiResponseDto<bool>> CreatePaymentConfirm(WebPaymentConfirm request)
        {
            var result = await _apiClientService.PostAsync<WebPaymentConfirm, bool>(_serviceName, "/api/payment/create/paymentconfirm", request);
            return result;
        }

        public async Task<ApiResponseDto<IEnumerable<WebPaymentConfirm>>> FetchPaymentConfirm(PaymentConfirmRequest request)
        {
            var result = await _apiClientService.PostAsync<PaymentConfirmRequest, IEnumerable<WebPaymentConfirm>>(_serviceName, "/api/payment/fetch/paymentconfirm", request);
            return result;
        }

        public async Task<ApiResponseDto<bool>> ModifyPaymentConfirm(WebPaymentConfirm request)
        {
            var result = await _apiClientService.PostAsync<WebPaymentConfirm, bool>(_serviceName, "/api/payment/modify/paymentconfirm", request);
            return result;
        }

        public async Task<ApiResponseDto<bool>> RemovePaymentConfirmation(WebPaymentConfirm request)
        {
            var result = await _apiClientService.PostAsync<WebPaymentConfirm, bool>(_serviceName, "/api/payment/remove/paymentconfirm", request);
            return result;
        }
    }
}
