using System.Text.Json.Serialization;

namespace TB.WEBAPP.SUBMITMOTOR.APPLICATION.DTOs.Responses.CoreSystems.Masters
{
    public class CarColorListResponse
    {
        [JsonPropertyName("COLOUR_CODE")]
        public string? ColorCode { get; set; }

        [JsonPropertyName("COLOUR_NAME")]
        public string? ColorName { get; set; }
    }
}
