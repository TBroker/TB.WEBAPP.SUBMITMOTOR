using TB.WEBAPP.SUBMITMOTOR.APPLICATION.DTOs;
using TB.WEBAPP.SUBMITMOTOR.APPLICATION.DTOs.Responses.Data.Rewards;

namespace TB.WEBAPP.SUBMITMOTOR.APPLICATION.Interfaces.Data.Rewards
{
    public interface IRewardUesCase
    {
        Task<ApiResponseDto<List<WebDataRewardPoint>>> FetchRewardPointAsync(string userId);

        Task<ApiResponseDto<List<WebCmsRewardPoint>>> FetchListRewardPointAsync();

        Task<ApiResponseDto<List<WebCmsRewardPromotion>>> FetchListRewardPromotionAsync();
    }
}