using System.Text.Json.Serialization;

namespace TB.WEBAPP.SUBMITMOTOR.APPLICATION.DTOs.Responses.CoreSystems.Masters
{
    public class AddressDistrictListResponse
    {
        [JsonPropertyName("PROVINCE_CODE")]
        public string? ProvinceCode { get; set; }

        [JsonPropertyName("PROV_NAME_T")]
        public string? ProvinceName { get; set; }

        [JsonPropertyName("DISTRICT_CODE")]
        public string? DistrictCode { get; set; }

        [JsonPropertyName("DISTRICT_NAME_T")]
        public string? DistrictName { get; set; }
    }
}
