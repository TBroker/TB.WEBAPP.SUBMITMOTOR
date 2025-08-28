using System.Text.Json.Serialization;

namespace TB.WEBAPP.SUBMITMOTOR.APPLICATION.DTOs.Responses.Data.Rewards
{
    public class WebCmsRewardPromotion
    {
        [JsonPropertyName("transaction_id")]
        public string? TransactionId { get; set; }

        [JsonPropertyName("date_create")]
        public DateTime? DateCreate { get; set; }

        [JsonPropertyName("date_update")]
        public DateTime? DateUpdate { get; set; }

        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("promotion_name")]
        public string? PromotionName { get; set; }

        [JsonPropertyName("flag_new")]
        public string? FlagNew { get; set; }

        [JsonPropertyName("file_path_image")]
        public string? FilePathImage { get; set; }

        [JsonPropertyName("date_start")]
        public DateTime? DateStart { get; set; }

        [JsonPropertyName("date_end")]
        public DateTime? DateEnd { get; set; }

        [JsonPropertyName("status")]
        public string? Status { get; set; }
    }
}
