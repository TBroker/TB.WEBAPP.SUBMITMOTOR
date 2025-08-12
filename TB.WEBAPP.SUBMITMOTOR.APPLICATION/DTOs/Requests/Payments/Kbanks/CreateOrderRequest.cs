using System.Text.Json.Serialization;

namespace TB.WEBAPP.SUBMITMOTOR.APPLICATION.DTOs.Requests.Payments.Kbanks
{
    public class CreateOrderRequest
    {
        [JsonPropertyName("amount")]
        public decimal Amount { get; set; }

        [JsonPropertyName("currency")]
        public string? Currency { get; set; }

        [JsonPropertyName("description")]
        public string? Description { get; set; }

        [JsonPropertyName("source_type")]
        public string? SourceType { get; set; }

        [JsonPropertyName("reference_order")]
        public string? ReferenceOrder { get; set; }

        [JsonPropertyName("ref_1")]
        public string? Refer1 { get; set; }

        [JsonPropertyName("ref_2")]
        public string? Refer2 { get; set; }

        [JsonPropertyName("ref_3")]
        public string? Refer3 { get; set; }
    }
}
