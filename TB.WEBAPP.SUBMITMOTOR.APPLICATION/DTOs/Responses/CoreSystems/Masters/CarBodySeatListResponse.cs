using System.Text.Json.Serialization;

namespace TB.WEBAPP.SUBMITMOTOR.APPLICATION.DTOs.Responses.CoreSystems.Masters
{
    public class CarBodySeatListResponse
    {
        [JsonPropertyName("CODECAR")]
        public string? CodeCar { get; set; }

        [JsonPropertyName("CODECAR_USED")]
        public string? CodeCarUse { get; set; }

        [JsonPropertyName("CODE_MODEL")]
        public string? CodeModel { get; set; }

        [JsonPropertyName("CAR_MODEL")]
        public string? CarModel { get; set; }

        [JsonPropertyName("CAR_SEAT")]
        public string? CarSeat { get; set; }

        [JsonPropertyName("CARTYPE_CODE")]
        public string? CarTypeCode { get; set; }

        [JsonPropertyName("CARTYPE")]
        public string? CarType { get; set; }

        [JsonPropertyName("CARUSED_CODE")]
        public string? CarUsedCode { get; set; }

        [JsonPropertyName("CARUSED")]
        public string? CarUsed { get; set; }
    }
}
