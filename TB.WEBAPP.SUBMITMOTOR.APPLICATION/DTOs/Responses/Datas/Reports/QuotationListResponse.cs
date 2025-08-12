using System.Text.Json.Serialization;

namespace TB.WEBAPP.SUBMITMOTOR.APPLICATION.DTOs.Responses.Datas.Reports
{
    public class QuotationListResponse
    {
        [JsonPropertyName("quo_number")]
        public string? QuotationNumber { get; set; }

        [JsonPropertyName("name")]
        public string? FirstName { get; set; }

        [JsonPropertyName("last_name")]
        public string? LastName { get; set; }

        [JsonPropertyName("phone_number")]
        public string? PhoneNumber { get; set; }

        [JsonPropertyName("car_year")]
        public string? CarYear { get; set; }

        [JsonPropertyName("car_brand")]
        public string? CarBrand { get; set; }

        [JsonPropertyName("car_model")]
        public string? CarModel { get; set; }

        [JsonPropertyName("car_sub_model")]
        public string? CarSubModel { get; set; }

        [JsonPropertyName("date_create")]
        public DateTime DateCreated { get; set; }

        [JsonPropertyName("vehicle_license")]
        public string? VehicleLicense { get; set; }

        [JsonPropertyName("buy_cmi")]
        public string? BuyCMI { get; set; }

        [JsonPropertyName("id")]
        public string? Id { get; set; }

        [JsonPropertyName("premiums_id")]
        public string? PremiumsId { get; set; }

        [JsonPropertyName("company_code")]
        public string? CompanyCode { get; set; }

        [JsonPropertyName("tm_product_code")]
        public string? ProductCode { get; set; }

        [JsonPropertyName("title_masterplan")]
        public string? TitleMasterPlan { get; set; }

        [JsonPropertyName("total_premiums")]
        public decimal TotalPremiums { get; set; }

        [JsonPropertyName("cmi_total_premiums")]
        public decimal CMITotalPremiums { get; set; }

        [JsonPropertyName("coverage_code")]
        public string? CoverageCode { get; set; }

        [JsonPropertyName("is_notified")]
        public string? IsNotified { get; set; }
    }
}
