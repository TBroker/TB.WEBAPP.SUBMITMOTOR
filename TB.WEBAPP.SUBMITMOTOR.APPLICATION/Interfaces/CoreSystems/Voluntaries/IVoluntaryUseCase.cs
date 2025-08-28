using TB.WEBAPP.SUBMITMOTOR.APPLICATION.DTOs;
using TB.WEBAPP.SUBMITMOTOR.APPLICATION.DTOs.Requests.CoreSystems.Voluntaries;
using TB.WEBAPP.SUBMITMOTOR.APPLICATION.DTOs.Responses.CoreSystems.Voluntaries;

namespace TB.WEBAPP.SUBMITMOTOR.APPLICATION.Interfaces.CoreSystems.Voluntaries
{
    public interface IVoluntaryUseCase
    {
        Task<ApiResponseDto<IEnumerable<VoluntaryCommissionResponse>>> FetchVoluntaryCommission(VoluntaryCommissionRequest request);
    }
}
