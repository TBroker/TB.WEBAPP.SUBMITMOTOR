using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace TB.WEBAPP.SUBMITMOTOR.APPLICATION.DTOs.Responses.Datas.Verifies
{
    public class WebVerifyOtp
    {
        [JsonPropertyName("transaction_id")]
        public string? TransactionId { get; set; }

        [JsonPropertyName("date_create")]
        public DateTime? DateCreate { get; set; }

        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("sent_to")]
        public string? SentTo { get; set; }

        [JsonPropertyName("ref_code")]
        public string? RefCode { get; set; }

        [JsonPropertyName("otp_code")]
        public string? OtpCode { get; set; }

        [JsonPropertyName("error_count")]
        public int? ErrorCount { get; set; }

        [JsonPropertyName("status")]
        public string? Status { get; set; }

        [JsonPropertyName("date_expire")]
        public DateTime? DateExpire { get; set; }
    }
}