using TB.WEBAPP.SUBMITMOTOR.APPLICATION.DTOs;
using TB.WEBAPP.SUBMITMOTOR.APPLICATION.DTOs.Requests.CoreSystems.Motors;
using TB.WEBAPP.SUBMITMOTOR.APPLICATION.DTOs.Responses.CoreSystems.Motors;
using TB.WEBAPP.SUBMITMOTOR.APPLICATION.Interfaces;
using TB.WEBAPP.SUBMITMOTOR.APPLICATION.Interfaces.CoreSystems.Motors;

namespace TB.WEBAPP.SUBMITMOTOR.APPLICATION.UseCases.CoreSystems.Motors
{
    public class SubmitMotorUseCase(IApiClientService apiClientService) : ISubmitMotorUseCase
    {
        private readonly IApiClientService _apiClientService = apiClientService;
        private readonly string _coreSystemService = "CoreSystemService";

        public async Task<ApiResponseDto<MotorSubmitResponse>> CreateSubmitMotor(MotorSubmitRequest request)
        {
            var result = await _apiClientService.PostAsync<MotorSubmitRequest, MotorSubmitResponse>(_coreSystemService, "/api/motor/submit/motor", request);
            return result;
        }

        public async Task<ApiResponseDto<MotorSubmitResponse>> CreateSubmitMotorUploadFile(MotorUploadFileRequest request)
        {
            var result = await _apiClientService.PostAsync<MotorUploadFileRequest, MotorSubmitResponse>(_coreSystemService, "/api/motor/submit/motor/upload", request);
            return result;
        }
    }
}
