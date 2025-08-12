using System.Text.Json.Serialization;

namespace TB.WEBAPP.SUBMITMOTOR.APPLICATION.DTOs.Responses.CoreSystems.Masters
{
    public class PreNameListResponse
    {
        [JsonPropertyName("PRENAME_CODE")]
        public string? PrenameCode { get; set; }

        [JsonPropertyName("PRENAME_T")]
        public string? PrenameTitle { get; set; }

        [JsonPropertyName("FLAG_TYPE")]
        public string? FlagType { get; set; }

        [JsonPropertyName("FLAG_SEX")]
        public string? FlagSex { get; set; }

        [JsonPropertyName("PRENAME_CODE_TNI")]
        public string? PrenameCodeTNI { get; set; }
    }
}
