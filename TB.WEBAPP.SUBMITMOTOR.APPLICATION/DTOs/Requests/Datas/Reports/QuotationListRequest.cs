using System.Text.Json.Serialization;

namespace TB.WEBAPP.SUBMITMOTOR.APPLICATION.DTOs.Requests.Datas.Reports
{
    public class QuotationListRequest
    {
        [JsonPropertyName("user_id")]
        public string? UserId { get; set; }

        [JsonPropertyName("name")]
        public string? Name { get; set; }

        [JsonPropertyName("vehicle_license")]
        public string? VehicleLicense { get; set; }

        [JsonPropertyName("company_code")]
        public string? CompanyCode { get; set; }

        [JsonPropertyName("coverage_code")]
        public string? CoverageCode { get; set; }

        [JsonPropertyName("date_start")]
        public DateTime? DateStart { get; set; }

        [JsonPropertyName("date_end")]
        public DateTime? DateEnd { get; set; }
    }
}
