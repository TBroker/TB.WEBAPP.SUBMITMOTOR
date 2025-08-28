using TB.WEBAPP.SUBMITMOTOR.APPLICATION.DTOs;
using TB.WEBAPP.SUBMITMOTOR.APPLICATION.DTOs.Responses.Data.Rewards;
using TB.WEBAPP.SUBMITMOTOR.APPLICATION.Interfaces;
using TB.WEBAPP.SUBMITMOTOR.APPLICATION.Interfaces.Data.Rewards;

namespace TB.WEBAPP.SUBMITMOTOR.APPLICATION.UseCases.Data.Rewards
{
    public class RewardUseCase(IApiClientService apiClientService) : IRewardUesCase
    {
        private readonly IApiClientService _apiClientService = apiClientService;
        private readonly string _serviceName = "DataService";

        public async Task<ApiResponseDto<List<WebDataRewardPoint>>> FetchRewardPointAsync(string userId)
        {
            var result = await _apiClientService.GetAsync< List<WebDataRewardPoint>>(_serviceName, $"/api/reward/fetch/data/{userId}");
            return result;
        }

        public async Task<ApiResponseDto<List<WebCmsRewardPoint>>> FetchListRewardPointAsync()
        {
            var result = await _apiClientService.GetAsync<List<WebCmsRewardPoint>>(_serviceName, $"/api/reward/fetch/list/data");
            return result;
        }

        public async Task<ApiResponseDto<List<WebCmsRewardPromotion>>> FetchListRewardPromotionAsync()
        {
            var result = await _apiClientService.GetAsync<List<WebCmsRewardPromotion>>(_serviceName, $"/api/reward/fetch/list/data/promotion");
            return result;
        }
    }
}
