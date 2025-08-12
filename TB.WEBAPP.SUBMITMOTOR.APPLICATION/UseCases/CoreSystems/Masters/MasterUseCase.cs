using TB.WEBAPP.SUBMITMOTOR.APPLICATION.DTOs;
using TB.WEBAPP.SUBMITMOTOR.APPLICATION.DTOs.Requests.CoreSystems.Masters;
using TB.WEBAPP.SUBMITMOTOR.APPLICATION.DTOs.Responses.CoreSystems.Masters;
using TB.WEBAPP.SUBMITMOTOR.APPLICATION.Interfaces;
using TB.WEBAPP.SUBMITMOTOR.APPLICATION.Interfaces.CoreSystems.Masters;

namespace TB.WEBAPP.SUBMITMOTOR.APPLICATION.UseCases.CoreSystems.Masters
{
    public class MasterUseCase(IApiClientService apiClientService) : IMasterUseCase
    {
        private readonly IApiClientService _apiClientService = apiClientService;
        private readonly string _coreSystemService = "CoreSystemService";

        public async Task<ApiResponseDto<List<AddressDistrictListResponse>>> FetchAddressDistrict(AddressDistrictRequest request)
        {
            var result = await _apiClientService.PostAsync<AddressDistrictRequest, List<AddressDistrictListResponse>>(_coreSystemService, "/api/master/address/district", request);
            return result;
        }

        public async Task<ApiResponseDto<List<AddressProvinceListResponse>>> FetchAddressProvince()
        {
            var result = await _apiClientService.PostAsync<List<AddressProvinceListResponse>>(_coreSystemService, "/api/master/address/province");
            return result;
        }

        public async Task<ApiResponseDto<List<AddressSubDistrictListResponse>>> FetchAddressSubDistrict(AddressSubDistrictRequest request)
        {
            var result = await _apiClientService.PostAsync<AddressSubDistrictRequest, List<AddressSubDistrictListResponse>>(_coreSystemService, "/api/master/address/subdistrict", request);
            return result;
        }

        public async Task<ApiResponseDto<List<CarBodySeatListResponse>>> FetchCarBodySeat()
        {
            var result = await _apiClientService.PostAsync<List<CarBodySeatListResponse>>(_coreSystemService, "/api/master/car/seat");
            return result;
        }

        public async Task<ApiResponseDto<List<CarBrandListResponse>>> FetchCarBrand(CarBrandRequest request)
        {
            var result = await _apiClientService.PostAsync<CarBrandRequest, List<CarBrandListResponse>>(_coreSystemService, "/api/master/car/brand", request);
            return result;
        }

        public async Task<ApiResponseDto<List<CarColorListResponse>>> FetchCarColor(CarColorRequest request)
        {
            var result = await _apiClientService.PostAsync<CarColorRequest, List<CarColorListResponse>>(_coreSystemService, "/api/master/car/color", request);
            return result;
        }

        public async Task<ApiResponseDto<List<CarModelListResponse>>> FetchCarModel(CarModelRequest request)
        {
            var result = await _apiClientService.PostAsync<CarModelRequest, List<CarModelListResponse>>(_coreSystemService, "/api/master/car/model", request);
            return result;
        }

        public async Task<ApiResponseDto<List<CarUsedListResponse>>> FetchCarUsed()
        {
            var result = await _apiClientService.PostAsync<List<CarUsedListResponse>>(_coreSystemService, "/api/master/car/use");
            return result;
        }

        public async Task<ApiResponseDto<List<CarVolCodeListResponse>>> FetchCarVoluntaryCode()
        {
            var result = await _apiClientService.PostAsync<List<CarVolCodeListResponse>>(_coreSystemService, "/api/master/car/voluntary/code");
            return result;
        }

        public async Task<ApiResponseDto<List<OccupationListResponse>>> FetchOccupation(OccupationRequest request)
        {
            var result = await _apiClientService.PostAsync<OccupationRequest, List<OccupationListResponse>>(_coreSystemService, "/api/master/categorical/occupation", request);
            return result;
        }

        public async Task<ApiResponseDto<List<PreNameListResponse>>> FetchPrename(PreNameRequest request)
        {
            var result = await _apiClientService.PostAsync<PreNameRequest, List<PreNameListResponse>>(_coreSystemService, "/api/master/categorical/prename", request);
            return result;
        }

        public async Task<ApiResponseDto<List<RelationshipResponse>>> FetchRelationship()
        {
            var result = await _apiClientService.PostAsync<List<RelationshipResponse>>(_coreSystemService, "/api/master/categorical/relationship");
            return result;
        }
    }
}