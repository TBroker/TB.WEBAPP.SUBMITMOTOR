using TB.WEBAPP.SUBMITMOTOR.APPLICATION.DTOs;
using TB.WEBAPP.SUBMITMOTOR.APPLICATION.DTOs.Requests.Datas.Motors;
using TB.WEBAPP.SUBMITMOTOR.APPLICATION.Interfaces;
using TB.WEBAPP.SUBMITMOTOR.APPLICATION.Interfaces.Datas.Motors;

namespace TB.WEBAPP.SUBMITMOTOR.APPLICATION.UseCases.Datas.Motors
{
    public class MotorUseCase(IApiClientService apiClientService) : IMotorUseCase
    {
        private readonly IApiClientService _apiClientService = apiClientService;
        private readonly string _serviceName = "DataService";

        public async Task<ApiResponseDto<IEnumerable<bool>>> CreateNotifyCoreSystemAsync(WebNotifyCoreSystem request)
        {
            var result = await _apiClientService.PostAsync<WebNotifyCoreSystem, IEnumerable<bool>>(_serviceName, "/api/motor/create/notify/coresystem", request);
            return result;
        }

        public async Task<ApiResponseDto<IEnumerable<bool>>> CreateNotifyCoreSystemDraftAsync(WebNotifyCoreSystemDraft request)
        {
            var result = await _apiClientService.PostAsync<WebNotifyCoreSystemDraft, IEnumerable<bool>>(_serviceName, "/api/motor/create/notify/coresystem/draft", request);
            return result;
        }

        public async Task<ApiResponseDto<IEnumerable<WebNotifyCoreSystemDraft>>> FetchNotifyCoreSystemDraftsAsync(NotifyCoreSystemDraftRequest request)
        {
            var result = await _apiClientService.PostAsync<NotifyCoreSystemDraftRequest, IEnumerable<WebNotifyCoreSystemDraft>>(_serviceName, $"/api/motor/fetch/notify/draft", request);
            return result;
        }
    }
}