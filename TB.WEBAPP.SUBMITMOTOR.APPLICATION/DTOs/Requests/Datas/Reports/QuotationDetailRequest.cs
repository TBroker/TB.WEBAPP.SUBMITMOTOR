using System.Text.Json.Serialization;

namespace TB.WEBAPP.SUBMITMOTOR.APPLICATION.DTOs.Requests.Datas.Reports
{
    public class QuotationDetailRequest
    {
        [JsonPropertyName("id")]
        public int? Id { get; set; }

        [JsonPropertyName("quo_number")]
        public string? QuotationNo { get; set; }

        [JsonPropertyName("premiums_id")]
        public int? PremiumsID { get; set; }

        [JsonPropertyName("company_code")]
        public string? CompanyCode { get; set; }

        [JsonPropertyName("tm_product_code")]
        public string? ProductCode { get; set; }
    }
}
