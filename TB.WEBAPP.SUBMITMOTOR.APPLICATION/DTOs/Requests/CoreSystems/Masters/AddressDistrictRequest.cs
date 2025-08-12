using System.ComponentModel;
using System.Text.Json.Serialization;

namespace TB.WEBAPP.SUBMITMOTOR.APPLICATION.DTOs.Requests.CoreSystems.Masters
{
    public class AddressDistrictRequest
    {
        [JsonPropertyName("PROVINCE_CODE")]
        [Description("รหัสจังหวัด")]
        public string? ProvinceCode { get; set; }
    }
}
