using System.Text.Json.Serialization;

namespace TB.WEBAPP.SUBMITMOTOR.APPLICATION.DTOs.Responses.Data.Rewards
{
    public class WebCmsRewardPoint
    {
        [JsonPropertyName("transaction_id")]
        public string? TransactionId { get; set; }

        [JsonPropertyName("date_create")]
        public DateTime? DateCreate { get; set; }

        [JsonPropertyName("date_update")]
        public DateTime? DateUpdate { get; set; }

        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("header")]
        public string? Header { get; set; }

        [JsonPropertyName("description")]
        public string? Description { get; set; }

        [JsonPropertyName("file_path_image")]
        public string? FilePathImage { get; set; }

        [JsonPropertyName("point")]
        public decimal? Point { get; set; }

        [JsonPropertyName("unit_pointed")]
        public string? UnitPointed { get; set; }

        [JsonPropertyName("point_focus")]
        public decimal? PointFocus { get; set; }

        [JsonPropertyName("date_start")]
        public DateTime? DateStart { get; set; }

        [JsonPropertyName("date_end")]
        public DateTime? DateEnd { get; set; }

        [JsonPropertyName("status")]
        public string? Status { get; set; }
    }
}
