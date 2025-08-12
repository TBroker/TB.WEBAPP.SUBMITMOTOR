using System.Text.Json.Serialization;

namespace TB.WEBAPP.SUBMITMOTOR.APPLICATION.DTOs.Responses.CoreSystems.Motors
{
    public class MotorSubmitResponse
    {
        [JsonPropertyName("VolAppNo")]
        public string? AppNoVMI { get; set; }

        [JsonPropertyName("CopAppNo")]
        public string? AppNoCMI { get; set; }
    }
}
