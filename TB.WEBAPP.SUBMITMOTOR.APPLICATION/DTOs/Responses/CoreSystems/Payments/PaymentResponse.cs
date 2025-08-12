using System.Text.Json.Serialization;

namespace TB.WEBAPP.SUBMITMOTOR.APPLICATION.DTOs.Responses.CoreSystems.Payments
{
    public class PaymentResponse
    {
        [JsonPropertyName("OrderId")]
        public string? OrderId { get; set; }
    }
}
