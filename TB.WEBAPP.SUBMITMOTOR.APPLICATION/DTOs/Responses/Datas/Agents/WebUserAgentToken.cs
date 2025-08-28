using System.Text.Json.Serialization;

namespace TB.WEBAPP.SUBMITMOTOR.APPLICATION.DTOs.Responses.Data.Agents
{
    public class WebUserAgentToken
    {
        [JsonPropertyName("id")]
        public decimal Id { get; set; }

        [JsonPropertyName("date_create")]
        public DateTime? DateCreate { get; set; }

        [JsonPropertyName("date_update")]
        public DateTime? DateUpdate { get; set; }

        [JsonPropertyName("user_id")]
        public string? UserID { get; set; }

        [JsonPropertyName("token")]
        public string? Token { get; set; }

        [JsonPropertyName("expire_date")]
        public DateTime? ExpireDate { get; set; }
    }
}
