using TB.WEBAPP.SUBMITMOTOR.APPLICATION.DTOs;
using TB.WEBAPP.SUBMITMOTOR.APPLICATION.DTOs.Requests.Datas.Installments;
using TB.WEBAPP.SUBMITMOTOR.APPLICATION.DTOs.Responses.Datas.Installments;
using TB.WEBAPP.SUBMITMOTOR.APPLICATION.Interfaces;
using TB.WEBAPP.SUBMITMOTOR.APPLICATION.Interfaces.Datas.Installments;

namespace TB.WEBAPP.SUBMITMOTOR.APPLICATION.UseCases.Datas.Installments
{
    public class PaymentInstallmentUseCase(IApiClientService apiClientService) : IPaymentInstallmentUseCase
    {
        private readonly IApiClientService _apiClientService = apiClientService;
        private readonly string _serviceName = "DataService";

        public async Task<ApiResponseDto<IEnumerable<WebPaymentInstallment>>> FetchPaymentInstallment(PaymentInstallmentRequest request)
        {
            var result = await _apiClientService.PostAsync<PaymentInstallmentRequest, IEnumerable<WebPaymentInstallment>>(_serviceName, "/api/installment/fetch/payment/installment", request);
            return result;
        }        

        public async Task<ApiResponseDto<IEnumerable<bool>>> CreatePaymentInstallment(WebPaymentInstallment request)
        {
            var result = await _apiClientService.PostAsync<WebPaymentInstallment, IEnumerable<bool>>(_serviceName, "/api/installment/create/payment/installment", request);
            return result;
        }

        public async Task<ApiResponseDto<IEnumerable<bool>>> RemovePaymentInstallment(WebPaymentInstallment request)
        {
            var result = await _apiClientService.PostAsync<WebPaymentInstallment, IEnumerable<bool>>(_serviceName, "/api/installment/remove/payment/installment", request);
            return result;
        }
    }
}
