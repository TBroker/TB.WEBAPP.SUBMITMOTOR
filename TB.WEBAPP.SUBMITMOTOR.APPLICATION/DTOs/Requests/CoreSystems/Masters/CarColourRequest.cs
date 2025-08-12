using System.ComponentModel;
using System.Text.Json.Serialization;

namespace TB.WEBAPP.SUBMITMOTOR.APPLICATION.DTOs.Requests.CoreSystems.Masters
{
    public class CarColorRequest
    {
        [JsonPropertyName("INS_COMPANY_CODE")]
        [Description("รหัสบริษัทประกันภัย")]
        public string? InsureCompanyCode { get; set; }
    }
}
