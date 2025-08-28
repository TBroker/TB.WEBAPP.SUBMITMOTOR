using TB.WEBAPP.SUBMITMOTOR.APPLICATION.DTOs;
using TB.WEBAPP.SUBMITMOTOR.APPLICATION.DTOs.Requests.CoreSystems.Policies;
using TB.WEBAPP.SUBMITMOTOR.APPLICATION.DTOs.Responses.CoreSystems.Policies;
using TB.WEBAPP.SUBMITMOTOR.APPLICATION.Interfaces;
using TB.WEBAPP.SUBMITMOTOR.APPLICATION.Interfaces.CoreSystems.Policies;

namespace TB.WEBAPP.SUBMITMOTOR.APPLICATION.UseCases.CoreSystems.Policies
{
    public class PolicyUseCase(IApiClientService apiClientService) : IPolicyUseCase
    {
        private readonly IApiClientService _apiClientService = apiClientService;
        private readonly string _coreSystemService = "CoreSystemService";

        public async Task<ApiResponseDto<IssuePolicyCompulsoryResponse>> CreatePolicyCompulsory(IssuePolicyCompulsoryRequest request)
        {
            var result = await _apiClientService.PostAsync<IssuePolicyCompulsoryRequest, IssuePolicyCompulsoryResponse>(_coreSystemService, "/api/policy/create/voluntary", request);
            return result;
        }

        public async Task<ApiResponseDto<IssuePolicyVoluntaryResponse>> CreatePolicyVoluntary(IssuePolicyVoluntaryRequest request)
        {
            var result = await _apiClientService.PostAsync<IssuePolicyVoluntaryRequest, IssuePolicyVoluntaryResponse>(_coreSystemService, "/api/policy/create/voluntary", request);
            return result;
        }

        public async Task<ApiResponseDto<PolicyReportResponse>> FetchPolicyReport(PolicyReportRequest request)
        {
            var result = await _apiClientService.PostAsync<PolicyReportRequest, PolicyReportResponse>(_coreSystemService, "/api/policy/fetch/report", request);
            return result;
        }
    }
}