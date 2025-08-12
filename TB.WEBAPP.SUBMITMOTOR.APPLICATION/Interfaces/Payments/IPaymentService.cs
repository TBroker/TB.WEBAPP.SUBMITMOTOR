using TB.WEBAPP.SUBMITMOTOR.APPLICATION.DTOs;
using TB.WEBAPP.SUBMITMOTOR.APPLICATION.DTOs.Requests.Payments.Kbanks;
using TB.WEBAPP.SUBMITMOTOR.APPLICATION.DTOs.Responses.Payments.Kbanks;

namespace TB.WEBAPP.SUBMITMOTOR.APPLICATION.Interfaces.Payments
{
    public interface IPaymentService
    {
        Task<ApiResponseDto<CreateOrderResponse>> CreatePayment(CreateOrderRequest request);
    }
}
