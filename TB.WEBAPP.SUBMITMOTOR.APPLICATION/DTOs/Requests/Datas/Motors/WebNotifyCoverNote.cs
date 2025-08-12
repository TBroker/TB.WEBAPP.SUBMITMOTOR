using System.Text.Json.Serialization;

namespace TB.WEBAPP.SUBMITMOTOR.APPLICATION.DTOs.Requests.Datas.Motors
{
    public class WebNotifyCoverNote
    {
        [JsonPropertyName("transaction_id")]
        public string? TransactionId { get; set; }

        [JsonPropertyName("date_create")]
        public DateTime DateCreate { get; set; }

        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("quotation_no")]
        public string? QuotationNo { get; set; }

        [JsonPropertyName("appno_vol")]
        public string? ApplicationNoVoluntary { get; set; }

        [JsonPropertyName("appno_com")]
        public string? ApplicationNoCompulsory { get; set; }

        [JsonPropertyName("company_name")]
        public string? CompanyName { get; set; }

        [JsonPropertyName("policy_type")]
        public string? PolicyType { get; set; }

        [JsonPropertyName("ins_name")]
        public string? InsureName { get; set; }

        [JsonPropertyName("car_brand")]
        public string? CarBrand { get; set; }

        [JsonPropertyName("car_model")]
        public string? CarModel { get; set; }

        [JsonPropertyName("vehicle")]
        public string? Vehicle { get; set; }

        [JsonPropertyName("car_repair")]
        public string? CarRepair { get; set; }

        [JsonPropertyName("vmi_coverage_total")]
        public int VoluntaryCoverageTotal { get; set; }

        [JsonPropertyName("vmi_premiums_total")]
        public decimal VoluntaryPremiumsTotal { get; set; }

        [JsonPropertyName("cmi_coverage_total")]
        public int CompulsoryCoverageTotal { get; set; }

        [JsonPropertyName("cmi_premiums_total")]
        public decimal CompulsoryPremiumsTotal { get; set; }

        [JsonPropertyName("vmi_start_date")]
        public DateTime VoluntaryStartDate { get; set; }

        [JsonPropertyName("vmi_end_date")]
        public DateTime VoluntaryEndDate { get; set; }

        [JsonPropertyName("cmi_start_date")]
        public DateTime CompulsoryStartDate { get; set; }

        [JsonPropertyName("cmi_end_date")]
        public DateTime CompulsoryEndDate { get; set; }

        [JsonPropertyName("agent_name")]
        public string? AgentName { get; set; }

        [JsonPropertyName("image_path")]
        public string? ImagePath { get; set; }

        [JsonPropertyName("file_path")]
        public string? FilePath { get; set; }

        [JsonPropertyName("password")]
        public string? Password { get; set; }

        [JsonPropertyName("email_address")]
        public string? EmailAddress { get; set; }
    }
}
