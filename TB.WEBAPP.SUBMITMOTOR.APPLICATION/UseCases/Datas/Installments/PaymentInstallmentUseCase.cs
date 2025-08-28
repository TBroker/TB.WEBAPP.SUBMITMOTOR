using TB.WEBAPP.SUBMITMOTOR.APPLICATION.DTOs;
using TB.WEBAPP.SUBMITMOTOR.APPLICATION.DTOs.Requests.Data.Installments;
using TB.WEBAPP.SUBMITMOTOR.APPLICATION.DTOs.Responses.Data.Installments;
using TB.WEBAPP.SUBMITMOTOR.APPLICATION.Interfaces;
using TB.WEBAPP.SUBMITMOTOR.APPLICATION.Interfaces.Data.Installments;

namespace TB.WEBAPP.SUBMITMOTOR.APPLICATION.UseCases.Data.Installments
{
    public class PaymentInstallmentUseCase(IApiClientService apiClientService) : IPaymentInstallmentUseCase
    {
        private readonly IApiClientService _apiClientService = apiClientService;
        private readonly string _serviceName = "DataService";

        public async Task<ApiResponseDto<bool>> CreateNotifyInstallmentContract(WebNotifyInstallmentContract request)
        {
            var result = await _apiClientService.PostAsync<WebNotifyInstallmentContract, bool>(_serviceName, "/api/installment/create/notify/installment/contract", request);
            return result;
        }

        public async Task<ApiResponseDto<bool>> CreatePaymentInstallment(WebPaymentInstallment request)
        {
            var result = await _apiClientService.PostAsync<WebPaymentInstallment, bool>(_serviceName, "/api/installment/create/payment/installment", request);
            return result;
        }

        public async Task<ApiResponseDto<IEnumerable<WebNotifyInstallmentContract>>> FetchNotifyInstallmentContract(NotifyInstallmentContractRequest request)
        {
            var result = await _apiClientService.PostAsync<NotifyInstallmentContractRequest, IEnumerable<WebNotifyInstallmentContract>>(_serviceName, "/api/installment/fetch/notify/installment/contract", request);
            return result;
        }

        public async Task<ApiResponseDto<IEnumerable<WebPaymentInstallment>>> FetchPaymentInstallment(PaymentInstallmentRequest request)
        {
            var result = await _apiClientService.PostAsync<PaymentInstallmentRequest, IEnumerable<WebPaymentInstallment>>(_serviceName, "/api/installment/fetch/payment/installment", request);
            return result;
        }

        public async Task<ApiResponseDto<bool>> ModifyNotifyInstallmentContract(WebNotifyInstallmentContract request)
        {
            var result = await _apiClientService.PostAsync<WebNotifyInstallmentContract, bool>(_serviceName, "/api/installment/modify/notify/installment/contract", request);
            return result;
        }

        public async Task<ApiResponseDto<bool>> RemoveNotifyInstallmentContract(WebNotifyInstallmentContract request)
        {
            var result = await _apiClientService.PostAsync<WebNotifyInstallmentContract, bool>(_serviceName, "/api/installment/remove/notify/installment/contract", request);
            return result;
        }

        public async Task<ApiResponseDto<bool>> RemovePaymentInstallment(WebPaymentInstallment request)
        {
            var result = await _apiClientService.PostAsync<WebPaymentInstallment, bool>(_serviceName, "/api/installment/remove/payment/installment", request);
            return result;
        }
    }
}