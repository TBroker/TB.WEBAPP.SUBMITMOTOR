using System.Text.Json.Serialization;

namespace TB.WEBAPP.SUBMITMOTOR.APPLICATION.DTOs.Responses.CoreSystems.Policies
{
    public class PolicyReportResponse
    {
        [JsonPropertyName("AGENT_CODE")]
        public string? AgentCode { get; set; }

        [JsonPropertyName("QUOTE_NO")]
        public string? QuotationNo { get; set; }

        [JsonPropertyName("NAME_LASTNAME")]
        public string? FullName { get; set; }

        [JsonPropertyName("VEHICLE_NO")]
        public string? VehicleNo { get; set; }

        [JsonPropertyName("APPNO_VOL")]
        public string? ApplicationNoVoluntary { get; set; }

        [JsonPropertyName("POLICYNO_VOL")]
        public string? PolicyNoVoluntary { get; set; }

        [JsonPropertyName("DATE_CREATE_VOL")]
        public string? DateCreateVoluntary { get; set; }

        [JsonPropertyName("POLICY_TYPE_VOL")]
        public string? PolicyTypeVoluntary { get; set; }

        [JsonPropertyName("PRODUCT_VOL")]
        public string? ProductVMI { get; set; }

        [JsonPropertyName("NET_PREM_AMT_VOL")]
        public int NetPremiumAmountVoluntary { get; set; }

        [JsonPropertyName("STAMP_DUTY_AMT_VOL")]
        public int StampDutyAmountVoluntary { get; set; }

        [JsonPropertyName("VAT_AMT_VOL")]
        public double VatAmountVoluntary { get; set; }

        [JsonPropertyName("TOTAL_PREM_AMT_VOL")]
        public double TotalPremiumAmountVoluntary { get; set; }

        [JsonPropertyName("FLG_NEW_RENEW_VOL")]
        public string? FlagNewRenewVoluntary { get; set; }

        [JsonPropertyName("RENEWAL_FROM_VOL")]
        public string? RenewalFromVoluntary { get; set; }

        [JsonPropertyName("APPNO_COM")]
        public string? ApplicationNoCompulsory { get; set; }

        [JsonPropertyName("POLICY_NO_COM")]
        public string? PolicyNoCompulsory { get; set; }

        [JsonPropertyName("DATE_CREATE_COM")]
        public string? DateCreateCompulsory { get; set; }

        [JsonPropertyName("POLICY_TYPE_COM")]
        public string? PolicyTypeCompulsory { get; set; }

        [JsonPropertyName("PRODUCT_COM")]
        public string? ProductCompulsory { get; set; }

        [JsonPropertyName("NET_PREM_AMT_COM")]
        public object? NetPremiumAmountCompulsory { get; set; }

        [JsonPropertyName("STAMP_DUTY_AMT_COM")]
        public object? StampDutyAmountCompulsory { get; set; }

        [JsonPropertyName("VAT_AMT_COM")]
        public object? VatAmountCompulsory { get; set; }

        [JsonPropertyName("TOTAL_PREM_AMT_COM")]
        public object? TotalPremiumAmountCompulsory { get; set; }

        [JsonPropertyName("FLG_NEW_RENEW_COM")]
        public string? FlagNewRenewCompulsory { get; set; }

        [JsonPropertyName("RENEWAL_FROM_COM")]
        public string? RenewalFromCompulsory { get; set; }
    }
}
