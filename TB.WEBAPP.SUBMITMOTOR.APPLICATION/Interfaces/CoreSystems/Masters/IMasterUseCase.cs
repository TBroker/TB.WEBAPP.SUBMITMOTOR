using TB.WEBAPP.SUBMITMOTOR.APPLICATION.DTOs;
using TB.WEBAPP.SUBMITMOTOR.APPLICATION.DTOs.Requests.CoreSystems.Masters;
using TB.WEBAPP.SUBMITMOTOR.APPLICATION.DTOs.Responses.CoreSystems.Masters;

namespace TB.WEBAPP.SUBMITMOTOR.APPLICATION.Interfaces.CoreSystems.Masters
{
    public interface IMasterUseCase
    {
        Task<ApiResponseDto<List<AddressDistrictListResponse>>> FetchAddressDistrict(AddressDistrictRequest request);

        Task<ApiResponseDto<List<AddressProvinceListResponse>>> FetchAddressProvince();

        Task<ApiResponseDto<List<AddressSubDistrictListResponse>>> FetchAddressSubDistrict(AddressSubDistrictRequest request);

        Task<ApiResponseDto<List<CarBodySeatListResponse>>> FetchCarBodySeat();

        Task<ApiResponseDto<List<CarBrandListResponse>>> FetchCarBrand(CarBrandRequest request);

        Task<ApiResponseDto<List<CarColorListResponse>>> FetchCarColor(CarColorRequest request);

        Task<ApiResponseDto<List<CarModelListResponse>>> FetchCarModel(CarModelRequest request);

        Task<ApiResponseDto<List<CarUsedListResponse>>> FetchCarUsed();

        Task<ApiResponseDto<List<CarVolCodeListResponse>>> FetchCarVoluntaryCode();

        Task<ApiResponseDto<List<OccupationListResponse>>> FetchOccupation(OccupationRequest request);

        Task<ApiResponseDto<List<PreNameListResponse>>> FetchPrename(PreNameRequest request);

        Task<ApiResponseDto<List<RelationshipResponse>>> FetchRelationship();
    }
}