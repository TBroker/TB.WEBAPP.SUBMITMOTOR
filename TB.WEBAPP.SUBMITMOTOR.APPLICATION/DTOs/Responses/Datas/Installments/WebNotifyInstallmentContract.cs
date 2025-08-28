using System.Text.Json.Serialization;

namespace TB.WEBAPP.SUBMITMOTOR.APPLICATION.DTOs.Responses.Data.Installments
{
    public class WebNotifyInstallmentContract
    {
        [JsonPropertyName("transaction_id")]
        public string? TransactionId { get; set; }

        [JsonPropertyName("date_create")]
        public DateTime? DateCreate { get; set; }

        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("full_name")]
        public string? FullName { get; set; }

        [JsonPropertyName("product_name")]
        public string? ProductName { get; set; }

        [JsonPropertyName("address")]
        public string? Address { get; set; }

        [JsonPropertyName("id_card_number")]
        public string? IdCardNumber { get; set; }

        [JsonPropertyName("email")]
        public string? Email { get; set; }

        [JsonPropertyName("relationship")]
        public string? Relationship { get; set; }

        [JsonPropertyName("insured_phone")]
        public string? InsuredPhone { get; set; }

        [JsonPropertyName("reference_phone")]
        public string? ReferencePhone { get; set; }

        [JsonPropertyName("premium_amount")]
        public decimal? PremiumAmount { get; set; }

        [JsonPropertyName("installment_count")]
        public int? InstallmentCount { get; set; }

        [JsonPropertyName("insurance_company")]
        public string? InsuranceCompany { get; set; }

        [JsonPropertyName("installment_amount")]
        public decimal? InstallmentAmount { get; set; }

        [JsonPropertyName("car_registration")]
        public string? CarRegistration { get; set; }

        [JsonPropertyName("insurance_type")]
        public string? InsuranceType { get; set; }

        [JsonPropertyName("coverage_start_date")]
        public DateTime? CoverageStartDate { get; set; }

        [JsonPropertyName("coverage_end_date")]
        public DateTime? CoverageEndDate { get; set; }

        [JsonPropertyName("contract_no")]
        public string? ContractNo { get; set; }
    }
}
