using System.Text.Json.Serialization;

namespace TB.WEBAPP.SUBMITMOTOR.APPLICATION.DTOs.Requests.Data.Motors
{
    public class NotifyCoverNoteRequest
    {
        [JsonPropertyName("appno_vol")]
        public string? AppNoVMI { get; set; }

        [JsonPropertyName("appno_com")]
        public string? AppNoCMI { get; set; }

        [JsonPropertyName("transaction_id")]
        public string? TransactionID { get; set; }
    }
}
