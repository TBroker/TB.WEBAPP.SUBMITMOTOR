using TB.WEBAPP.SUBMITMOTOR.APPLICATION.DTOs;
using TB.WEBAPP.SUBMITMOTOR.APPLICATION.DTOs.Requests.CoreSystems.Compulsories;
using TB.WEBAPP.SUBMITMOTOR.APPLICATION.DTOs.Responses.CoreSystems.Compulsories;

namespace TB.WEBAPP.SUBMITMOTOR.APPLICATION.Interfaces.CoreSystems.Compulsories
{
    public interface ICompulsoryUseCase
    {
        Task<ApiResponseDto<IEnumerable<CompulsoryPremiumResponse>>> FetchCompulsoryCommission(CompulsoryCommissionRequest request);
    }
}