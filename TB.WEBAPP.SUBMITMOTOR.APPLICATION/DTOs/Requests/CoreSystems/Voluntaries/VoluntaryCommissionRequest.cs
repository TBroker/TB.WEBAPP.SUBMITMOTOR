using System.ComponentModel;
using System.Text.Json.Serialization;

namespace TB.WEBAPP.SUBMITMOTOR.APPLICATION.DTOs.Requests.CoreSystems.Voluntaries
{
    public class VoluntaryCommissionRequest
    {
        [JsonPropertyName("APPNO_VOL")]
        [Description("เลขใบคำขอสมัครใจ")]
        public string? ApplicationNoVoluntary { get; set; }
    }
}
