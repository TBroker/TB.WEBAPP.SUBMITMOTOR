using TB.WEBAPP.SUBMITMOTOR.APPLICATION.DTOs;
using TB.WEBAPP.SUBMITMOTOR.APPLICATION.DTOs.Requests.CoreSystems.Payments;
using TB.WEBAPP.SUBMITMOTOR.APPLICATION.DTOs.Responses.CoreSystems.Motors;

namespace TB.WEBAPP.SUBMITMOTOR.APPLICATION.Interfaces.CoreSystems.Payments
{
    public interface IPaymentCoreSystemUseCase
    {
        Task<ApiResponseDto<MotorSubmitResponse>> CreateTransactionPayment(CreateTransactionPaymentRequest request);

        Task<ApiResponseDto<MotorSubmitResponse>> ModifyTransactionPayment(UpdateTransactionPaymentRequest request);
    }
}
