using TB.WEBAPP.SUBMITMOTOR.APPLICATION.DTOs;
using TB.WEBAPP.SUBMITMOTOR.APPLICATION.DTOs.Responses.Datas.Rewards;

namespace TB.WEBAPP.SUBMITMOTOR.APPLICATION.Interfaces.Datas.Rewards
{
    public interface IRewardUesCase
    {
        Task<ApiResponseDto<List<WebDataRewardPoint>>> FetchRewardPointAsync(string userId);

        Task<ApiResponseDto<List<WebCmsRewardPoint>>> FetchListRewardPointAsync();

        Task<ApiResponseDto<List<WebCmsRewardPromotion>>> FetchListRewardPromotionAsync();
    }
}