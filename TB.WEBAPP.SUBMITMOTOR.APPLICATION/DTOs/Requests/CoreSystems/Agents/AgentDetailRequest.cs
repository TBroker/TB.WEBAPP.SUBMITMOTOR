using System.ComponentModel;
using System.Text.Json.Serialization;

namespace TB.WEBAPP.SUBMITMOTOR.APPLICATION.DTOs.Requests.CoreSystems.Agents
{
    public class AgentDetailRequest
    {
        [JsonPropertyName("AGENT_CODE")]
        [Description("รหัสตัวแทน")]
        public string? AgentCode { get; set; }
    }
}
