using System.Text.Json.Serialization;

namespace TB.WEBAPP.SUBMITMOTOR.APPLICATION.DTOs.Responses.CoreSystems.Masters
{
    public class AddressProvinceListResponse
    {
        [JsonPropertyName("PROVINCE_CODE")]
        public string? ProvinceCode { get; set; }

        [JsonPropertyName("PROV_NAME_T")]
        public string? ProvinceName { get; set; }
    }
}
