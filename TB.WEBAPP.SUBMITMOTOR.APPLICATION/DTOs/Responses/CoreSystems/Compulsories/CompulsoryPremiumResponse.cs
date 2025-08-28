using System.Text.Json.Serialization;

namespace TB.WEBAPP.SUBMITMOTOR.APPLICATION.DTOs.Responses.CoreSystems.Compulsories
{
    public class CompulsoryPremiumResponse
    {
        [JsonPropertyName("APP_NO")]
        public string? ApplicationNo { get; set; }

        [JsonPropertyName("QUOTE_NO")]
        public string? QuotationNo { get; set; }

        [JsonPropertyName("CREATE_DATE")]
        public DateTime CreateDate { get; set; }

        [JsonPropertyName("NET_PREM_AMT")]
        public double NetPremiumsAmount { get; set; }

        [JsonPropertyName("STAMP_DUTY_AMT")]
        public double StampDutyAmount { get; set; }

        [JsonPropertyName("VAT_AMT")]
        public double VatAmount { get; set; }

        [JsonPropertyName("TOTAL_PREM_AMT")]
        public double TotalPremiumsAmount { get; set; }

        [JsonPropertyName("COMM_RATE_AGENT")]
        public double CommissionRateAgent { get; set; }

        [JsonPropertyName("COMM_AMTE_AGENT")]
        public double CommissionAmountAgent { get; set; }

        [JsonPropertyName("W_TAX_RATE_AGENT")]
        public double TaxRateAgent { get; set; }

        [JsonPropertyName("W_TAX_AMT_AGENT")]
        public double TaxAmountAgent { get; set; }

        [JsonPropertyName("VAT_RATE_AGENT")]
        public double VatRateAgent { get; set; }

        [JsonPropertyName("VAT_AMT_AGENT")]
        public double VatAmountAgent { get; set; }

        [JsonPropertyName("W_TAX_AMT_INS_1")]
        public double TaxAmountInsure { get; set; }

        [JsonPropertyName("TOTAL_PAY_FULL")]
        public double TotalPayFull { get; set; }

        [JsonPropertyName("TOTAL_PAY_NETCOMM")]
        public double TotalPayNetCommission { get; set; }
    }
}
