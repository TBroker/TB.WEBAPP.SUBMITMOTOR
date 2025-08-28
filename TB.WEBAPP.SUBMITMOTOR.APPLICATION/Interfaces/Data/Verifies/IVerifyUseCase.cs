using TB.WEBAPP.SUBMITMOTOR.APPLICATION.DTOs;
using TB.WEBAPP.SUBMITMOTOR.APPLICATION.DTOs.Responses.Data.Verifies;

namespace TB.WEBAPP.SUBMITMOTOR.APPLICATION.Interfaces.Data.Verifies
{
    public interface IVerifyUseCase
    {
        Task<ApiResponseDto<bool>> CreateVerifyOtp(WebVerifyOtp request);

        Task<ApiResponseDto<IEnumerable<WebVerifyOtp>>> FetchVerifyOtpByOtpRefNo(string otpRefNo);

        Task<ApiResponseDto<IEnumerable<WebVerifyOtp>>> FetchVerifyOtpByTransactionId(string transactionId);

        Task<ApiResponseDto<bool>> ModifyVerifyOtp(WebVerifyOtp request);

        Task<ApiResponseDto<bool>> RemoveVerifyOtp(WebVerifyOtp request);
    }
}