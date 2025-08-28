using System.Text.Json.Serialization;

namespace TB.WEBAPP.SUBMITMOTOR.APPLICATION.DTOs.Requests.CoreSystems.Payments
{
    public class CreateTransactionPaymentRequest
    {
        [JsonPropertyName("APPNO_VOL")]
        public string? ApplicationNoVoluntary { get; set; }

        [JsonPropertyName("APPNO_COM")]
        public string? ApplicationNoCompulsory { get; set; }

        [JsonPropertyName("STATUS_PAID")]
        public string? StatusPaid { get; set; }

        [JsonPropertyName("DATE_CREATE")]
        public string? DateCreate { get; set; }

        [JsonPropertyName("DATE_UPDATE")]
        public string? DateUpdate { get; set; }

        [JsonPropertyName("ORDER_ID")]
        public string? OrderID { get; set; }

        [JsonPropertyName("TOTAL_PAYAMOUNT")]
        public decimal TotalPayAmount { get; set; }

        [JsonPropertyName("STATUS_COMMISSION")]
        public string? StatusCommission { get; set; }
    }
}
