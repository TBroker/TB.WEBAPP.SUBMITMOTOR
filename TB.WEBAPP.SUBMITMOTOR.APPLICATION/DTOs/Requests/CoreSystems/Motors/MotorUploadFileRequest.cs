using System.Text.Json.Serialization;

namespace TB.WEBAPP.SUBMITMOTOR.APPLICATION.DTOs.Requests.CoreSystems.Motors
{
    public class MotorUploadFileRequest
    {
        [JsonPropertyName("APP_VOLNO")]
        public string? AppNoVMI { get; set; }

        [JsonPropertyName("APP_COMNO")]
        public string? AppNoCMI { get; set; }

        [JsonPropertyName("REGIS_EXTENSION")]
        public string? RegisExtension { get; set; }

        [JsonPropertyName("REGIS_BASE64")]
        public string? RegisBase64 { get; set; }

        [JsonPropertyName("INSPECTFORM_EXTENSION")]
        public string? InspectFormExtension { get; set; }

        [JsonPropertyName("INSPECTFORM_BASE64")]
        public string? InspectFormBase64 { get; set; }

        [JsonPropertyName("FRONT_EXTENSION")]
        public string? FrontExtension { get; set; }

        [JsonPropertyName("FRONT_BASE64")]
        public string? FrontBase64 { get; set; }

        [JsonPropertyName("BACK_EXTENSION")]
        public string? BackExtension { get; set; }

        [JsonPropertyName("BACK_BASE64")]
        public string? BackBase64 { get; set; }

        [JsonPropertyName("FRONT_LEFT_EXTENSION")]
        public string? FrontLeftExtension { get; set; }

        [JsonPropertyName("FRONT_LEFT_BASE64")]
        public string? FrontLeftBase64 { get; set; }

        [JsonPropertyName("FRONT_RIGHT_EXTENSION")]
        public string? FrontRightExtension { get; set; }

        [JsonPropertyName("FRONT_RIGHT_BASE64")]
        public string? FrontRightBase64 { get; set; }

        [JsonPropertyName("BACK_LEFT_EXTENSION")]
        public string? BackLeftExtension { get; set; }

        [JsonPropertyName("BACK_LEFT_BASE64")]
        public string? BackLeftBase64 { get; set; }

        [JsonPropertyName("BACK_RIGHT_EXTENSION")]
        public string? BackRightExtension { get; set; }

        [JsonPropertyName("BACK_RIGHT_BASE64")]
        public string? BackRightBase64 { get; set; }

        [JsonPropertyName("ENGINE_EXTENSION")]
        public string? EngineExtension { get; set; }

        [JsonPropertyName("ENGINE_BASE64")]
        public string? EngineBase64 { get; set; }

        [JsonPropertyName("TRUNK_EXTENSION")]
        public string? TrunkExtension { get; set; }

        [JsonPropertyName("TRUNK_BASE64")]
        public string? TrunkBase64 { get; set; }

        [JsonPropertyName("CONSOLE_EXTENSION")]
        public string? ConsoleExtension { get; set; }

        [JsonPropertyName("CONSOLE_BASE64")]
        public string? ConsoleBase64 { get; set; }

        [JsonPropertyName("PANEL_EXTENSION")]
        public string? PanelExtension { get; set; }

        [JsonPropertyName("PANEL_BASE64")]
        public string? PanelBase64 { get; set; }

        [JsonPropertyName("FRONT_DOOR_EXTENSION")]
        public string? FrontDoorExtension { get; set; }

        [JsonPropertyName("FRONT_DOOR_BASE64")]
        public string? FrontDoorBase64 { get; set; }

        [JsonPropertyName("BACK_DOOR_EXTENSION")]
        public string? BackDoorExtension { get; set; }

        [JsonPropertyName("BACK_DOOR_BASE64")]
        public string? BackDoorBase64 { get; set; }

        [JsonPropertyName("IDCARD_EXTENSION")]
        public string? IdCardExtension { get; set; }

        [JsonPropertyName("IDCARD_BASE64")]
        public string? IdCardBase64 { get; set; }

        [JsonPropertyName("OTHER_DOC_EXTENSION")]
        public string? OtherDocExtension { get; set; }

        [JsonPropertyName("OTHER_DOC_BASE64")]
        public string? OtherDocBase64 { get; set; }
    }
}
