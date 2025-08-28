using TB.WEBAPP.SUBMITMOTOR.APPLICATION.DTOs;
using TB.WEBAPP.SUBMITMOTOR.APPLICATION.DTOs.Entities;
using TB.WEBAPP.SUBMITMOTOR.APPLICATION.DTOs.Requests.Data.Motors;

namespace TB.WEBAPP.SUBMITMOTOR.APPLICATION.Interfaces.Data.Motors
{
    public interface IMotorUseCase
    {
        Task<ApiResponseDto<bool>> CreateNotifyCoreSystemAsync(WebNotifyCoreSystem request);

        Task<ApiResponseDto<bool>> CreateNotifyCoreSystemDraftAsync(WebNotifyCoreSystemDraft request);

        Task<ApiResponseDto<bool>> CreateNotifyCoverNoteAsync(WebNotifyCoverNote request);

        Task<ApiResponseDto<IEnumerable<WebNotifyCoreSystem>>> FetchNotifyCoreSystemAsync(NotifyCoreSystemRequest request);

        Task<ApiResponseDto<IEnumerable<WebNotifyCoreSystemDraft>>> FetchNotifyCoreSystemDraftsAsync(NotifyCoreSystemDraftRequest request);

        Task<ApiResponseDto<IEnumerable<WebNotifyCoverNote>>> FetchNotifyCoverNoteAsync(NotifyCoverNoteRequest request);
    }
}