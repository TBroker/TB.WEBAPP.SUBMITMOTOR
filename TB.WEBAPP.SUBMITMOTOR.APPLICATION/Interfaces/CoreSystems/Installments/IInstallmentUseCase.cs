using TB.WEBAPP.SUBMITMOTOR.APPLICATION.DTOs;
using TB.WEBAPP.SUBMITMOTOR.APPLICATION.DTOs.Requests.CoreSystems.Installments;
using TB.WEBAPP.SUBMITMOTOR.APPLICATION.DTOs.Responses.CoreSystems.Installments;

namespace TB.WEBAPP.SUBMITMOTOR.APPLICATION.Interfaces.CoreSystems.Installments
{
    public interface IInstallmentUseCase
    {  
        Task<ApiResponseDto<IEnumerable<InstallmentCalculatePeriodResponse>>> FetchCalculatePeriod(InstallmentCalculatePeriodRequest request);
    }
}
