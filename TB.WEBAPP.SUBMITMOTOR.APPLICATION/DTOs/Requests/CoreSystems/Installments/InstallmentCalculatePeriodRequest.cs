using System.ComponentModel;
using System.Text.Json.Serialization;

namespace TB.WEBAPP.SUBMITMOTOR.APPLICATION.DTOs.Requests.CoreSystems.Installments
{
    public class InstallmentCalculatePeriodRequest
    {
        [JsonPropertyName("VOL_NET_PREMIUM_AMT")]
        [Description("เบี้ยสุทธิ ภาคสมัครใจ")]
        public double VoluntaryNetPremium { get; set; }

        [JsonPropertyName("VOL_TOTAL_PREMIUM_AMT")]
        [Description("เบี้ยรวม ภาคสมัครใจ")]
        public double VoluntaryTotalPremium { get; set; }

        [JsonPropertyName("COP_TOTAL_PREMIUM_AMT")]
        [Description("เบี้ยรวม พ.ร.บ.")]
        public double CompulsoryTotalPremium { get; set; }

        [JsonPropertyName("NUM_INSTALL")]
        [Description("จำนวนงวด")]
        public int NumberOfInstallments { get; set; }

        [JsonPropertyName("FLG_PERSON")]
        [Description("N=บุคคลธรรมดา , Y=นิติบุคคล")]
        public string FlagPerson { get; set; } = string.Empty;
    }
}
