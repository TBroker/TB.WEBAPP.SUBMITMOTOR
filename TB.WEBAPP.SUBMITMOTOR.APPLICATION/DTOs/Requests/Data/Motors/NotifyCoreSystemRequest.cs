using System.Text.Json.Serialization;

namespace TB.WEBAPP.SUBMITMOTOR.APPLICATION.DTOs.Requests.Data.Motors
{
    public class NotifyCoreSystemRequest
    {
        [JsonPropertyName("appno_vol")]
        public string? ApplicationNoVoluntary { get; set; }

        [JsonPropertyName("appno_com")]
        public string? ApplicationNoCompulsory { get; set; }

        [JsonPropertyName("agent_code")]
        public string? AgentCode { get; set; }

        [JsonPropertyName("transaction_id")]
        public string? TransactionID { get; set; }
    }
}
