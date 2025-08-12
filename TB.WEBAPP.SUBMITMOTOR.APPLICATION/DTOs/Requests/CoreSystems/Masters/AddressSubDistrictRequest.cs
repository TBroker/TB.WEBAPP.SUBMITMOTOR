using System.ComponentModel;
using System.Text.Json.Serialization;

namespace TB.WEBAPP.SUBMITMOTOR.APPLICATION.DTOs.Requests.CoreSystems.Masters
{
    public class AddressSubDistrictRequest
    {
        [JsonPropertyName("PROVINCE_CODE")]
        [Description("รหัสจังหวัด")]
        public string? ProvinceCode { get; set; }

        [JsonPropertyName("DISTRICT_CODE")]
        [Description("รหัสอำเภอ")]
        public string? DistrictCode { get; set; }
    }
}
