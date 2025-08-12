using TB.WEBAPP.SUBMITMOTOR.APPLICATION.DTOs;
using TB.WEBAPP.SUBMITMOTOR.APPLICATION.DTOs.Requests.Services;
using TB.WEBAPP.SUBMITMOTOR.APPLICATION.DTOs.Responses.Services;
using TB.WEBAPP.SUBMITMOTOR.APPLICATION.Interfaces;
using TB.WEBAPP.SUBMITMOTOR.APPLICATION.Interfaces.Notifications;

namespace TB.WEBAPP.SUBMITMOTOR.APPLICATION.Services.Notifications
{
    public class NotificationService(IApiClientService apiClientService) : INotificationService
    {
        private readonly IApiClientService _apiClientService = apiClientService;
        private readonly string _notificationService = "NotificationService";

        public async Task<ApiResponseDto<SendMailTBResponse>> SendMailTBroker(SendMailTBRequest request)
        {
            var result = await _apiClientService.PostAsync<SendMailTBRequest, SendMailTBResponse>(_notificationService, "/api/services/tbroker/send/email", request);
            return result;
        }

        public async Task<ApiResponseDto<SendSmsTBResponse>> SendSmsTBroker(SendSmsTBRequest request)
        {
            var result = await _apiClientService.PostAsync<SendSmsTBRequest, SendSmsTBResponse>(_notificationService, "/api/services/tbroker/send/sms", request);
            return result;
        }

        public async Task<ApiResponseDto<SendSmsTniResponse>> SendSmsTni(SendSmsTniRequest request)
        {
            var result = await _apiClientService.PostAsync<SendSmsTniRequest, SendSmsTniResponse>(_notificationService, "/api/services/tni/send/sms", request);
            return result;
        }
    }
}