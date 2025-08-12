using System.ComponentModel;
using System.Text.Json.Serialization;

namespace TB.WEBAPP.SUBMITMOTOR.APPLICATION.DTOs.Requests.CoreSystems.Masters
{
    public class CarModelRequest
    {
        [JsonPropertyName("CARBRAND_CODE")]
        [Description("รหัสยี่ห้อรถยนต์")]
        public string? CarBrandCode { get; set; }

        [JsonPropertyName("INS_COMPANY_CODE")]
        [Description("รหัสบริษัทประกันภัย")]
        public string? CompanyCode { get; set; }
    }
}
