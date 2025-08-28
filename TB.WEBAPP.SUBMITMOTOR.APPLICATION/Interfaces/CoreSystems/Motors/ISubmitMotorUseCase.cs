using TB.WEBAPP.SUBMITMOTOR.APPLICATION.DTOs;
using TB.WEBAPP.SUBMITMOTOR.APPLICATION.DTOs.Requests.CoreSystems.Motors;
using TB.WEBAPP.SUBMITMOTOR.APPLICATION.DTOs.Responses.CoreSystems.Motors;

namespace TB.WEBAPP.SUBMITMOTOR.APPLICATION.Interfaces.CoreSystems.Motors
{
    public interface ISubmitMotorUseCase
    {
        Task<ApiResponseDto<MotorSubmitResponse>> CreateSubmitMotor(MotorSubmitRequest request);

        Task<ApiResponseDto<MotorSubmitResponse>> CreateSubmitMotorUploadFile(MotorUploadFileRequest request);
    }
}