using System.Text.Json.Serialization;

namespace TB.WEBAPP.SUBMITMOTOR.APPLICATION.DTOs.Responses.CoreSystems.Quotations
{
    public class QuotationDetailResponse
    {
        [JsonPropertyName("QUOTE_NO")]
        public string? QuotationNo { get; set; }

        [JsonPropertyName("AGENT_CODE")]
        public string? AgentCode { get; set; }

        [JsonPropertyName("DATE_CREATE")]
        public string? DateCreate { get; set; }

        [JsonPropertyName("APPNO_VOL")]
        public string? AppNoVmi { get; set; }

        [JsonPropertyName("TOTAL_VOL")]
        public double TotalVmi { get; set; }

        [JsonPropertyName("APPNO_COM")]
        public string? AppNoCmi { get; set; }

        [JsonPropertyName("TOTAL_COM")]
        public double TotalCmi { get; set; }

        [JsonPropertyName("POLICY_TYPE")]
        public string? PolicyType { get; set; }

        [JsonPropertyName("PRODUCT")]
        public string? Product { get; set; }

        [JsonPropertyName("NAME_LASTNAME")]
        public string? FullName { get; set; }

        [JsonPropertyName("VEHICLE_NO")]
        public string? VehicleNo { get; set; }

        [JsonPropertyName("SUBPOLICY")]
        public string? SubPolicy { get; set; }

        [JsonPropertyName("POLICYNO_VOL")]
        public string? PolicyVmi { get; set; }

        [JsonPropertyName("POLICYNO_COM")]
        public string? PolicyCmi { get; set; }
    }
}