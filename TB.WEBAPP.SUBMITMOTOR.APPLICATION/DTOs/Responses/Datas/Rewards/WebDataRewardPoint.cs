using System.Text.Json.Serialization;

namespace TB.WEBAPP.SUBMITMOTOR.APPLICATION.DTOs.Responses.Data.Rewards
{
    public class WebDataRewardPoint
    {
        [JsonPropertyName("transaction_id")]
        public string? TransactionId { get; set; }

        [JsonPropertyName("date_create")]
        public DateTime? DateCreate { get; set; }

        [JsonPropertyName("date_update")]
        public DateTime? DateUpdate { get; set; }

        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("agent_code")]
        public string? AgentCode { get; set; }

        [JsonPropertyName("point")]
        public decimal? Point { get; set; }

        [JsonPropertyName("point_focus")]
        public decimal? PointFocus { get; set; }

        [JsonPropertyName("point_plus")]
        public decimal? PointPlus { get; set; }

        [JsonPropertyName("point_monthly")]
        public decimal? PointMonthly { get; set; }

        [JsonPropertyName("date_as_of")]
        public DateTime? DateAsOf { get; set; }
    }
}
