using System.Text.Json.Serialization;

namespace TB.WEBAPP.SUBMITMOTOR.APPLICATION.DTOs.Responses.CoreSystems.Masters
{
    public class AddressSubDistrictListResponse
    {
        [JsonPropertyName("SUBDISTRICT")]
        public string? SubDistrict { get; set; }

        [JsonPropertyName("DISTRICT_NAME_T")]
        public string? DistrictName { get; set; }

        [JsonPropertyName("PROV_NAME_T")]
        public string? ProvinceName { get; set; }

        [JsonPropertyName("I_ZIPCODE")]
        public string? ZipCode { get; set; }

        [JsonPropertyName("PROVINCE_CODE")]
        public string? ProvinceCode { get; set; }

        [JsonPropertyName("DISTRICT_CODE")]
        public string? DistrictCode { get; set; }

        [JsonPropertyName("I_SUBDISTRICT")]
        public string? ISubDistrict { get; set; }
    }
}
