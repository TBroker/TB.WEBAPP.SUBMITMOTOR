using TB.WEBAPP.SUBMITMOTOR.APPLICATION.DTOs;
using TB.WEBAPP.SUBMITMOTOR.APPLICATION.DTOs.Entities;
using TB.WEBAPP.SUBMITMOTOR.APPLICATION.DTOs.Requests.Data.Motors;
using TB.WEBAPP.SUBMITMOTOR.APPLICATION.Interfaces;
using TB.WEBAPP.SUBMITMOTOR.APPLICATION.Interfaces.Data.Motors;

namespace TB.WEBAPP.SUBMITMOTOR.APPLICATION.UseCases.Data.Motors
{
    public class MotorUseCase(IApiClientService apiClientService) : IMotorUseCase
    {
        private readonly IApiClientService _apiClientService = apiClientService;
        private readonly string _serviceName = "DataService";

        public async Task<ApiResponseDto<bool>> CreateNotifyCoreSystemAsync(WebNotifyCoreSystem request)
        {
            var result = await _apiClientService.PostAsync<WebNotifyCoreSystem, bool>(_serviceName, "/api/motor/create/notify/coresystem", request);
            return result;
        }

        public async Task<ApiResponseDto<bool>> CreateNotifyCoreSystemDraftAsync(WebNotifyCoreSystemDraft request)
        {
            var result = await _apiClientService.PostAsync<WebNotifyCoreSystemDraft, bool>(_serviceName, "/api/motor/create/notify/coresystem/draft", request);
            return result;
        }

        public async Task<ApiResponseDto<bool>> CreateNotifyCoverNoteAsync(WebNotifyCoverNote request)
        {
            var result = await _apiClientService.PostAsync<WebNotifyCoverNote, bool>(_serviceName, "/api/motor/create/notify/covernote", request);
            return result;
        }

        public async Task<ApiResponseDto<IEnumerable<WebNotifyCoreSystem>>> FetchNotifyCoreSystemAsync(NotifyCoreSystemRequest request)
        {
            var result = await _apiClientService.PostAsync<NotifyCoreSystemRequest, IEnumerable<WebNotifyCoreSystem>>(_serviceName, $"/api/motor/fetch/notify/coresystem", request);
            return result;
        }

        public async Task<ApiResponseDto<IEnumerable<WebNotifyCoreSystemDraft>>> FetchNotifyCoreSystemDraftsAsync(NotifyCoreSystemDraftRequest request)
        {
            var result = await _apiClientService.PostAsync<NotifyCoreSystemDraftRequest, IEnumerable<WebNotifyCoreSystemDraft>>(_serviceName, $"/api/motor/fetch/notify/draft", request);
            return result;
        }

        public async Task<ApiResponseDto<IEnumerable<WebNotifyCoverNote>>> FetchNotifyCoverNoteAsync(NotifyCoverNoteRequest request)
        {
            var result = await _apiClientService.PostAsync<NotifyCoverNoteRequest, IEnumerable<WebNotifyCoverNote>>(_serviceName, $"/api/motor/fetch/notify/covernote", request);
            return result;
        }
    }
}