using TB.WEBAPP.SUBMITMOTOR.APPLICATION.DTOs;
using TB.WEBAPP.SUBMITMOTOR.APPLICATION.DTOs.Requests.Services;
using TB.WEBAPP.SUBMITMOTOR.APPLICATION.DTOs.Responses.Services;

namespace TB.WEBAPP.SUBMITMOTOR.APPLICATION.Interfaces.Notifications
{
    public interface INotificationService
    {
        Task<ApiResponseDto<SendMailTBResponse>> SendMailTBroker(SendMailTBRequest request);

        Task<ApiResponseDto<SendSmsTBResponse>> SendSmsTBroker(SendSmsTBRequest request);

        Task<ApiResponseDto<SendSmsTniResponse>> SendSmsTni(SendSmsTniRequest request);
    }
}