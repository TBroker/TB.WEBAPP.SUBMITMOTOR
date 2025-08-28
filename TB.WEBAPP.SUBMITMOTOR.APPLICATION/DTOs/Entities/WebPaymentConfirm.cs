using System.Text.Json.Serialization;

namespace TB.WEBAPP.SUBMITMOTOR.APPLICATION.DTOs.Entities
{
    public class WebPaymentConfirm
    {
        [JsonPropertyName("transaction_id")]
        public string? TransactionId { get; set; }

        [JsonPropertyName("date_create")]
        public DateTime DateCreate { get; set; }

        [JsonPropertyName("date_update")]
        public DateTime DateUpdate { get; set; }

        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("order_id")]
        public string? OrderId { get; set; }

        [JsonPropertyName("quotation_no")]
        public string? QuotationNumber { get; set; }

        [JsonPropertyName("appno_vol")]
        public string? ApplicationNoVoluntary { get; set; }

        [JsonPropertyName("appno_com")]
        public string? ApplicationNoCompulsory { get; set; }

        [JsonPropertyName("user_id")]
        public string? UserId { get; set; }

        [JsonPropertyName("company_code")]
        public string? CompanyCode { get; set; }

        [JsonPropertyName("payment_ref1")]
        public string? PaymentReference1 { get; set; }

        [JsonPropertyName("payment_ref2")]
        public string? PaymentReference2 { get; set; }

        [JsonPropertyName("payment_system")]
        public string? PaymentSystem { get; set; }

        [JsonPropertyName("payment_channel")]
        public string? PaymentChannel { get; set; }

        [JsonPropertyName("payment_status")]
        public string? PaymentStatus { get; set; }
    }
}