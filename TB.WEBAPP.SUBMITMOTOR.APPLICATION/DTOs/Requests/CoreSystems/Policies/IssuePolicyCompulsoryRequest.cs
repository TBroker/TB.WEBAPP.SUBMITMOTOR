using System.Text.Json.Serialization;

namespace TB.WEBAPP.SUBMITMOTOR.APPLICATION.DTOs.Requests.CoreSystems.Policies
{
    public class IssuePolicyCompulsoryRequest
    {
        [JsonPropertyName("AGENT_CODE")]
        public string? AgentCode { get; set; }

        [JsonPropertyName("INS_COMPANY_CODE")]
        public string? InsureCompanyCode { get; set; }

        [JsonPropertyName("APP_COMPULNO")]
        public string? ApplicationNoCompulsory { get; set; }
    }
}
