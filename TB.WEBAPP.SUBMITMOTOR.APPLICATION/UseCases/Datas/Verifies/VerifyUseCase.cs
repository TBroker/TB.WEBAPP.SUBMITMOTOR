using TB.WEBAPP.SUBMITMOTOR.APPLICATION.DTOs;
using TB.WEBAPP.SUBMITMOTOR.APPLICATION.DTOs.Responses.Datas.Verifies;
using TB.WEBAPP.SUBMITMOTOR.APPLICATION.Interfaces;
using TB.WEBAPP.SUBMITMOTOR.APPLICATION.Interfaces.Datas.Verifies;

namespace TB.WEBAPP.SUBMITMOTOR.APPLICATION.UseCases.Datas.Verifies
{
    public class VerifyUseCase(IApiClientService apiClientService) : IVerifyUseCase
    {
        private readonly IApiClientService _apiClientService = apiClientService;
        private readonly string _serviceName = "DataService";

        public async Task<ApiResponseDto<IEnumerable<bool>>> CreateVerifyOtp(WebVerifyOtp request)
        {
            var result = await _apiClientService.PostAsync<WebVerifyOtp, IEnumerable<bool>>(_serviceName, "/api/verify/create/otp", request);
            return result;
        }

        public async Task<ApiResponseDto<IEnumerable<WebVerifyOtp>>> FetchVerifyOtpByOtpRefNo(string otpRefNo)
        {
            var result = await _apiClientService.GetAsync<IEnumerable<WebVerifyOtp>>(_serviceName, $"/api/verify/fetch/otp/ref/{otpRefNo}");
            return result;
        }

        public async Task<ApiResponseDto<IEnumerable<WebVerifyOtp>>> FetchVerifyOtpByTransactionId(string transactionId)
        {
            var result = await _apiClientService.GetAsync<IEnumerable<WebVerifyOtp>>(_serviceName, $"/api/verify/fetch/otp/txn/{transactionId}");
            return result;
        }

        public async Task<ApiResponseDto<IEnumerable<bool>>> ModifyVerifyOtp(WebVerifyOtp request)
        {
            var result = await _apiClientService.PostAsync<WebVerifyOtp, IEnumerable<bool>>(_serviceName, "/api/verify/modify/otp", request);
            return result;
        }

        public async Task<ApiResponseDto<IEnumerable<bool>>> RemoveVerifyOtp(WebVerifyOtp request)
        {
            var result = await _apiClientService.PostAsync<WebVerifyOtp, IEnumerable<bool>>(_serviceName, "/api/verify/remove/otp", request);
            return result;
        }
    }
}
