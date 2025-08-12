using System.Text.Json.Serialization;

namespace TB.WEBAPP.SUBMITMOTOR.APPLICATION.DTOs.Responses.Services
{
    public class SendSmsTBResponse
    {
        [JsonPropertyName("status")]
        public string Status { get; set; } = string.Empty;

        [JsonPropertyName("message")]
        public string Message { get; set; } = string.Empty;

        [JsonPropertyName("success")]
        public bool IsSuccess { get; set; }

        [JsonPropertyName("track_id")]
        public string TrackId { get; set; } = string.Empty;
    }
}
