using System.Text.Json.Serialization;

namespace TB.WEBAPP.SUBMITMOTOR.APPLICATION.DTOs.Responses.CoreSystems.Masters
{
    public class OccupationListResponse
    {
        [JsonPropertyName("OCC_CODE")]
        public string? OccupationCode { get; set; }

        [JsonPropertyName("OCC_NAME")]
        public string? OccupationName { get; set; }

        [JsonPropertyName("OCC_CODE_VIB")]
        public string? OccupationCodeVIB { get; set; }

        [JsonPropertyName("OCC_CODE_TNI")]
        public string? OccupationCodeTNI { get; set; }
    }
}
