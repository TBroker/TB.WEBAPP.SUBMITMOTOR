using System.Text.Json.Serialization;

namespace TB.WEBAPP.SUBMITMOTOR.APPLICATION.DTOs.Requests.Data.Premiums
{
    public class MasterPlanRequest
    {
        [JsonPropertyName("tm_product_code")]
        public string? ProductCode { get; set; }

        [JsonPropertyName("company_code")]
        public string? CompanyCode { get; set; }

        [JsonPropertyName("coverage_code")]
        public string? CoverageCode { get; set; }
    }
}
