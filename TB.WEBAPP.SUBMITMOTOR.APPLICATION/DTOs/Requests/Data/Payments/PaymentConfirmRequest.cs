using System.Text.Json.Serialization;

namespace TB.WEBAPP.SUBMITMOTOR.APPLICATION.DTOs.Requests.Data.Payments
{
    public class PaymentConfirmRequest
    {
        [JsonPropertyName("payment_ref1")]
        public string? PaymentReference1 { get; set; }

        [JsonPropertyName("transaction_id")]
        public string? TransactionId { get; set; }
    }
}
