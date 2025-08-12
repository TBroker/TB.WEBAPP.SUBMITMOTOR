using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace TB.WEBAPP.SUBMITMOTOR.APPLICATION.DTOs.Responses.Datas.Installments
{
    public class WebPaymentInstallment
    {
        [JsonPropertyName("transaction_id")]
        public string? TransactionId { get; set; }

        [JsonPropertyName("date_create")]
        public DateTime? DateCreate { get; set; }

        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("contract_no")]
        public string? ContractNumber { get; set; }

        [JsonPropertyName("email")]
        public string? Email { get; set; }

        [JsonPropertyName("mobile")]
        public string? Mobile { get; set; }

        [JsonPropertyName("period_total")]
        public int? PeriodTotal { get; set; }

        [JsonPropertyName("first_period_amount")]
        public decimal? FirstPeriodAmount { get; set; }

        [JsonPropertyName("next_period_amount")]
        public decimal? NextPeriodAmount { get; set; }

        [Column("date_expire")]
        [JsonPropertyName("date_expire")]
        public DateTime? DateExpire { get; set; }
    }
}