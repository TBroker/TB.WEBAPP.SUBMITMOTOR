using System.Text.Json.Serialization;

namespace TB.WEBAPP.SUBMITMOTOR.APPLICATION.DTOs.Responses.CoreSystems.Masters
{
    public class CarUsedListResponse
    {
        [JsonPropertyName("CARUSED_CODE")]
        public string? CarUsedCode { get; set; }

        [JsonPropertyName("CARUSED_DESC")]
        public string? CarUsedDescription { get; set; }
    }
}
