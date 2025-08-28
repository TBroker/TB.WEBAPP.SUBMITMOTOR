using System.Text.Json.Serialization;

namespace TB.WEBAPP.SUBMITMOTOR.APPLICATION.DTOs.Responses.Data.Reports
{
    public class QuotationDetailResponse
    {
        [JsonPropertyName("tm_product_dst_group")]
        public string? ProductGroup { get; set; }

        [JsonPropertyName("user_id")]
        public string? UserId { get; set; }

        [JsonPropertyName("car_brand")]
        public string? CarBrand { get; set; }

        [JsonPropertyName("car_model")]
        public string? CarModel { get; set; }

        [JsonPropertyName("car_year")]
        public string? CarYear { get; set; }

        [JsonPropertyName("car_engine_size")]
        public string? CarEngineSize { get; set; }

        [JsonPropertyName("quo_number")]
        public string? QuotationNo { get; set; }

        [JsonPropertyName("company_code")]
        public string? CompanyCode { get; set; }

        [JsonPropertyName("company_name")]
        public string? CompanyName { get; set; }

        [JsonPropertyName("coverage_code")]
        public string? CoverageCode { get; set; }

        [JsonPropertyName("coverage_name")]
        public string? CoverageName { get; set; }

        [JsonPropertyName("repair_type")]
        public string? RepairType { get; set; }

        [JsonPropertyName("sum_insure")]
        public string? SumInsure { get; set; }

        [JsonPropertyName("f_t")]
        public string? FT { get; set; }

        [JsonPropertyName("net_premiums")]
        public string? NetPremiums { get; set; }

        [JsonPropertyName("total_premiums")]
        public string? TotalPremiums { get; set; }

        [JsonPropertyName("cmi_total_premiums")]
        public string? CMITotalPremiums { get; set; }

        [JsonPropertyName("total_premiums_with_cmi")]
        public string? TotalPremiumsWithCMI { get; set; }

        [JsonPropertyName("origianl_product_code")]
        public string? OriginalProductCode { get; set; }

        [JsonPropertyName("title_masterplan")]
        public string? TitleMasterPlan { get; set; }

        [JsonPropertyName("cover_val19")]
        public string? CoverageVal19 { get; set; }

        [JsonPropertyName("cover_val20")]
        public string? CoverageVal20 { get; set; }

        [JsonPropertyName("atm_type")]
        public string? AutoMobileType { get; set; }
    }
}
