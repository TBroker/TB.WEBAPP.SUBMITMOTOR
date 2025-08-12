using System.Text.Json.Serialization;

namespace TB.WEBAPP.SUBMITMOTOR.APPLICATION.DTOs.Responses.CoreSystems.Agents
{
    public class AgentDetailResponse
    {
        [JsonPropertyName("AGENT_CODE")]
        public string? AgentCode { get; set; }

        [JsonPropertyName("NAME")]
        public string? Name { get; set; }

        [JsonPropertyName("EXPIRE_DATE")]
        public string? ExpireDate { get; set; }

        [JsonPropertyName("NUMEXPIRE")]
        public string? NumExpire { get; set; }

        [JsonPropertyName("FLAG_NONLIFE_DOC")]
        public string? FlagNonlifeDoc { get; set; }

        [JsonPropertyName("ACTIVE")]
        public string? Active { get; set; }

        [JsonPropertyName("BELONG_TO")]
        public string? BelongTo { get; set; }

        [JsonPropertyName("AGENT_TYPE")]
        public string? AgentType { get; set; }

        [JsonPropertyName("AGENT_TYPE_DESC")]
        public string? AgentTypeDesc { get; set; }

        [JsonPropertyName("AC_TYPE_CODE")]
        public string? AcTypeCode { get; set; }

        [JsonPropertyName("AC_TYPE_DESC")]
        public string? AcTypeDesc { get; set; }

        [JsonPropertyName("CHANNEL_CODE")]
        public string? ChannelCode { get; set; }

        [JsonPropertyName("CHANNEL_DESC")]
        public string? ChannelDesc { get; set; }

        [JsonPropertyName("CHANNEL_RUNNING")]
        public string? ChannelRunning { get; set; }

        [JsonPropertyName("F_ADDRESS_NO")]
        public string? FAddressNo { get; set; }

        [JsonPropertyName("F_SOI")]
        public string? FSoi { get; set; }

        [JsonPropertyName("F_STREET")]
        public string? FStreet { get; set; }

        [JsonPropertyName("F_DISTRICT_CODE_SUB")]
        public string? FDistrictCodeSub { get; set; }

        [JsonPropertyName("F_SUBDISTRICT")]
        public string? FSubdistrict { get; set; }

        [JsonPropertyName("F_DISTRICT_CODE")]
        public string? FDistrictCode { get; set; }

        [JsonPropertyName("DISTRICT_NAME_T")]
        public string? DistrictNameT { get; set; }

        [JsonPropertyName("F_PROVINCE_CODE")]
        public string? FProvinceCode { get; set; }

        [JsonPropertyName("PROV_NAME_T")]
        public string? ProvNameT { get; set; }

        [JsonPropertyName("F_ZIPCODE")]
        public string? FZipCode { get; set; }

        [JsonPropertyName("F_PHONE")]
        public string? FPhone { get; set; }

        [JsonPropertyName("F_FAXNO")]
        public string? FFaxNo { get; set; }

        [JsonPropertyName("E_MAIL_ADDR")]
        public string? EmailAddress { get; set; }

        [JsonPropertyName("F_CREDIT")]
        public string? FCredit { get; set; }

        [JsonPropertyName("W_TAX_RATE")]
        public int WTaxRate { get; set; }

        [JsonPropertyName("VAT_RATE")]
        public int VatRate { get; set; }
    }
}
