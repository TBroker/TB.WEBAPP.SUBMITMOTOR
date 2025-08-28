using System.Text.Json.Serialization;

namespace TB.WEBAPP.SUBMITMOTOR.APPLICATION.DTOs.Responses.Data.Premiums
{
    public class MasterPlanResponse
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }
        [JsonPropertyName("tm_product_code")]
        public string? ProductCode { get; set; }
        [JsonPropertyName("title")]
        public string? Title { get; set; }
        [JsonPropertyName("en_title")]
        public string? EnTitle { get; set; }
        [JsonPropertyName("tm_product_code_oic")]
        public string? ProductCodeOic { get; set; }
        [JsonPropertyName("tm_product_dst_group")]
        public string? ProductDescriptionGroup { get; set; }
        [JsonPropertyName("coverage_code")]
        public string? CoverageCode { get; set; }
        [JsonPropertyName("company_code")]
        public string? CompanyCode { get; set; }
        [JsonPropertyName("origianl_product_code")]
        public string? OriginalProductCode { get; set; }
        [JsonPropertyName("repair_type")]
        public string? RepairType { get; set; }
        [JsonPropertyName("group_agent")]
        public string? GroupAgent { get; set; }
        [JsonPropertyName("effective_date")]
        public string? EffectiveDate { get; set; }
        [JsonPropertyName("expire_date")]
        public string? ExpireDate { get; set; }
    }
}