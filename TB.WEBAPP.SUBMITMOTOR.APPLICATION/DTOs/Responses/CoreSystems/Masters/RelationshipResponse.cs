using System.Text.Json.Serialization;

namespace TB.WEBAPP.SUBMITMOTOR.APPLICATION.DTOs.Responses.CoreSystems.Masters
{
    public class RelationshipResponse
    {
        [JsonPropertyName("INS_RELATION_CODE")]
        public string RelationshipCode { get; set; } = string.Empty;

        [JsonPropertyName("INS_RELATIONSHIPS")]
        public string RelationshipName { get; set; } = string.Empty;
    }
}
