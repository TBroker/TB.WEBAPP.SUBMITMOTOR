using System.Text.Json.Serialization;

namespace TB.WEBAPP.SUBMITMOTOR.APPLICATION.DTOs.Responses.CoreSystems.Installments
{
    public class InstallmentCalculatePeriodResponse
    {
        [JsonPropertyName("INSTALLM_NO")]
        public int InstallmentNo { get; set; }

        [JsonPropertyName("PREMIUM_AMT")]
        public double PremiumAmount { get; set; }

        [JsonPropertyName("WTAX_AMT")]
        public double TaxAmount { get; set; }
    }
}
