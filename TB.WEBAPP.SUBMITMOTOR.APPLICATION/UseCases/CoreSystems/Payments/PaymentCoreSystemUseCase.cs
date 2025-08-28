using TB.WEBAPP.SUBMITMOTOR.APPLICATION.DTOs;
using TB.WEBAPP.SUBMITMOTOR.APPLICATION.DTOs.Requests.CoreSystems.Payments;
using TB.WEBAPP.SUBMITMOTOR.APPLICATION.DTOs.Responses.CoreSystems.Motors;
using TB.WEBAPP.SUBMITMOTOR.APPLICATION.Interfaces;
using TB.WEBAPP.SUBMITMOTOR.APPLICATION.Interfaces.CoreSystems.Payments;

namespace TB.WEBAPP.SUBMITMOTOR.APPLICATION.UseCases.CoreSystems.Payments
{
    public class PaymentCoreSystemUseCase(IApiClientService apiClientService) : IPaymentCoreSystemUseCase
    {
        private readonly IApiClientService _apiClientService = apiClientService;
        private readonly string _coreSystemService = "CoreSystemService";

        public async Task<ApiResponseDto<MotorSubmitResponse>> CreateTransactionPayment(CreateTransactionPaymentRequest request)
        {
            var result = await _apiClientService.PostAsync<CreateTransactionPaymentRequest, MotorSubmitResponse>(_coreSystemService, "/api/payment/create/transaction", request);
            return result;
        }

        public async Task<ApiResponseDto<MotorSubmitResponse>> ModifyTransactionPayment(UpdateTransactionPaymentRequest request)
        {
            var result = await _apiClientService.PostAsync<UpdateTransactionPaymentRequest, MotorSubmitResponse>(_coreSystemService, "/api/payment/modify/transaction", request);
            return result;
        }
    }
}
