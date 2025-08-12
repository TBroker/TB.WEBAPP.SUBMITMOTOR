using System.Text.Json.Serialization;

namespace TB.WEBAPP.SUBMITMOTOR.APPLICATION.DTOs.Responses.Services
{
    public class SendSmsTniResponse
    {
        [JsonPropertyName("Data")]
        public DataSend? Data { get; set; }

        [JsonPropertyName("Message")]
        public string? Message { get; set; }

        [JsonPropertyName("Success")]
        public bool Success { get; set; }
    }

    public class DataSend
    {
        [JsonPropertyName("MessageId")]
        public string? MessageId { get; set; }

        [JsonPropertyName("To")]
        public string? To { get; set; }
    }
}
