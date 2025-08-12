using System.Text.Json.Serialization;

namespace TB.WEBAPP.SUBMITMOTOR.APPLICATION.DTOs.Responses.CoreSystems.Masters
{
    public class CarVolCodeListResponse
    {
        [JsonPropertyName("CODECAR")]
        public string? CodeCar { get; set; }

        [JsonPropertyName("CARUSED_DESC")]
        public string? CarUsedDesc { get; set; }

        [JsonPropertyName("CARTYPE_CODE")]
        public string? CarTypeCode { get; set; }

        [JsonPropertyName("CARTYPE_DESC")]
        public string? CarTypeDescription { get; set; }

        [JsonPropertyName("SUBINS_TYPE_CODE")]
        public string? SubInsureTypeCode { get; set; }

        [JsonPropertyName("SUBINS_TYPE_DESC")]
        public string? SubInsureTypeDescription { get; set; }

        [JsonPropertyName("BODY_TYPE_CODE")]
        public string? BodyTypeCode { get; set; }

        [JsonPropertyName("CAR_MODEL")]
        public string? CarModel { get; set; }

        [JsonPropertyName("CAR_SEAT")]
        public string? CarSeat { get; set; }
    }
}
