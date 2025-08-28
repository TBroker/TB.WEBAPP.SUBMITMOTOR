using System.Text.Json.Serialization;

namespace TB.WEBAPP.SUBMITMOTOR.APPLICATION.DTOs.Requests.CoreSystems.Compulsories
{
    public class CompulsoryCommissionRequest
    {
        [JsonPropertyName("APPNO_COM")]
        public string? ApplicationNoCompulsory { get; set; }
    }
}
