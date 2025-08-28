using System.Text.Json.Serialization;

namespace TB.WEBAPP.SUBMITMOTOR.APPLICATION.DTOs.Responses.CoreSystems.Motors
{
    public class MotorSubmitResponse
    {
        [JsonPropertyName("Result")]
        public string? Result { get; set; }

        [JsonPropertyName("ErrorMsg")]
        public string? ErrorMessage { get; set; }

        [JsonPropertyName("TransDateTime")]
        public string? TransDateTime { get; set; }

        [JsonPropertyName("OrderId")]
        public string? OrderId { get; set; }

        [JsonPropertyName("VolAppNo")]
        public string? AppNoVMI { get; set; }

        [JsonPropertyName("CopAppNo")]
        public string? AppNoCMI { get; set; }
    }
}
