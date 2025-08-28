using System.ComponentModel;
using System.Text.Json.Serialization;

namespace TB.WEBAPP.SUBMITMOTOR.APPLICATION.DTOs.Requests.CoreSystems.Policies
{
    public class PolicyReportRequest
    {
        [JsonPropertyName("AGENT_CODE")]
        [Description("รหัสตัวแทน")]
        public string? AgentCode { get; set; }

        [JsonPropertyName("DATE_START")]
        [Description("วันที่เริ่มความคุ้มครอง")]
        public string? DateStart { get; set; }

        [JsonPropertyName("DATE_END")]
        [Description("วันที่สิ้นสุดความคุ้มครอง")]
        public string? DateEnd { get; set; }

        [JsonPropertyName("VEHICLE_NO")]
        [Description("เลขทะเบียนรถยนต์")]
        public string? VehicleNo { get; set; }

        [JsonPropertyName("POLICY_TYPE")]
        [Description("ประเภทกรมธรรม์")]
        public string? PolicyType { get; set; }

        [JsonPropertyName("NAME_LASTNAME")]
        [Description("ชื่อ-นามสกุลลูกค้า")]
        public string? FullName { get; set; }

        [JsonPropertyName("APPNO_VOL")]
        [Description("เลขที่ใบคำขอภาคสมัครใจ")]
        public string? ApplicationNoVoluntary { get; set; }

        [JsonPropertyName("APPNO_COM")]
        [Description("เลขที่ใบคำขอภาคบังคับ")]
        public string? ApplicationNoCompulsory { get; set; }

        [JsonPropertyName("FLG_NEW_RENEW_VOL")]
        [Description("ประเภทกรมธรรม์ งานใหม่ หรืองานเก่า")]
        public string? FlagDocType { get; set; }
    }
}
