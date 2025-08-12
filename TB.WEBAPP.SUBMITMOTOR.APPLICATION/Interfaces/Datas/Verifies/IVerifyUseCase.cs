using TB.WEBAPP.SUBMITMOTOR.APPLICATION.DTOs;
using TB.WEBAPP.SUBMITMOTOR.APPLICATION.DTOs.Responses.Datas.Verifies;

namespace TB.WEBAPP.SUBMITMOTOR.APPLICATION.Interfaces.Datas.Verifies
{
    public interface IVerifyUseCase
    {
        Task<ApiResponseDto<IEnumerable<bool>>> CreateVerifyOtp(WebVerifyOtp request);

        Task<ApiResponseDto<IEnumerable<WebVerifyOtp>>> FetchVerifyOtpByOtpRefNo(string otpRefNo);

        Task<ApiResponseDto<IEnumerable<WebVerifyOtp>>> FetchVerifyOtpByTransactionId(string transactionId);

        Task<ApiResponseDto<IEnumerable<bool>>> ModifyVerifyOtp(WebVerifyOtp request);

        Task<ApiResponseDto<IEnumerable<bool>>> RemoveVerifyOtp(WebVerifyOtp request);
    }
}