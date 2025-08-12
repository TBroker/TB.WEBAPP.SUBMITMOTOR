using TB.WEBAPP.SUBMITMOTOR.APPLICATION.DTOs;
using TB.WEBAPP.SUBMITMOTOR.APPLICATION.DTOs.Requests.Datas.Motors;

namespace TB.WEBAPP.SUBMITMOTOR.APPLICATION.Interfaces.Datas.Motors
{
    public interface IMotorUseCase
    {
        Task<ApiResponseDto<IEnumerable<bool>>> CreateNotifyCoreSystemAsync(WebNotifyCoreSystem request);

        Task<ApiResponseDto<IEnumerable<bool>>> CreateNotifyCoreSystemDraftAsync(WebNotifyCoreSystemDraft request);

        Task<ApiResponseDto<IEnumerable<WebNotifyCoreSystemDraft>>> FetchNotifyCoreSystemDraftsAsync(NotifyCoreSystemDraftRequest request);
    }
}