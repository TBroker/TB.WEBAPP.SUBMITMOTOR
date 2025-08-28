using TB.WEBAPP.SUBMITMOTOR.APPLICATION.DTOs;
using TB.WEBAPP.SUBMITMOTOR.APPLICATION.DTOs.Requests.CoreSystems.Policies;
using TB.WEBAPP.SUBMITMOTOR.APPLICATION.DTOs.Responses.CoreSystems.Policies;

namespace TB.WEBAPP.SUBMITMOTOR.APPLICATION.Interfaces.CoreSystems.Policies
{
    public interface IPolicyUseCase
    {
        Task<ApiResponseDto<IssuePolicyCompulsoryResponse>> CreatePolicyCompulsory(IssuePolicyCompulsoryRequest request);

        Task<ApiResponseDto<IssuePolicyVoluntaryResponse>> CreatePolicyVoluntary(IssuePolicyVoluntaryRequest request);

        Task<ApiResponseDto<PolicyReportResponse>> FetchPolicyReport(PolicyReportRequest request);
    }
}