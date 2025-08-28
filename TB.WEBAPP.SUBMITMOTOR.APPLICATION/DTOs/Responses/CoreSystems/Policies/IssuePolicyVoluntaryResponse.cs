using System.Text.Json.Serialization;

namespace TB.WEBAPP.SUBMITMOTOR.APPLICATION.DTOs.Responses.CoreSystems.Policies
{
    public class IssuePolicyVoluntaryResponse
    {
        [JsonPropertyName("status")]
        public string? Status { get; set; }

        [JsonPropertyName("objresponse")]
        public ObjResponseVoluntary? ObjectResponse { get; set; }

        [JsonPropertyName("volpolicyno")]
        public string? VoluntaryPolicyNo { get; set; }

        [JsonPropertyName("appno")]
        public string? ApplicationNo { get; set; }

        [JsonPropertyName("transdatetime")]
        public string? TransDateTime { get; set; }

        [JsonPropertyName("payload")]
        public PayloadVoluntary? Payload { get; set; }
    }

    public class MotorDataVoluntary
    {
        [JsonPropertyName("app_no")]
        public string? APP_NO { get; set; }

        [JsonPropertyName("app_date")]
        public string? APP_DATE { get; set; }

        [JsonPropertyName("app_inform_name")]
        public string? APP_INFORM_NAME { get; set; }

        [JsonPropertyName("cus_type")]
        public string? CUS_TYPE { get; set; }

        [JsonPropertyName("cus_foreigner")]
        public string? CUS_FOREIGNER { get; set; }

        [JsonPropertyName("cus_prefix")]
        public string? CUS_PREFIX { get; set; }

        [JsonPropertyName("cus_name")]
        public string? CUS_NAME { get; set; }

        [JsonPropertyName("cus_surname")]
        public string? CUS_SURNAME { get; set; }

        [JsonPropertyName("cus_idtype")]
        public string? CUS_IDTYPE { get; set; }

        [JsonPropertyName("cus_idcard")]
        public string? CUS_IDCARD { get; set; }

        [JsonPropertyName("cus_sex")]
        public string? CUS_SEX { get; set; }

        [JsonPropertyName("cus_birthdate")]
        public string? CUS_BIRTHDATE { get; set; }

        [JsonPropertyName("cus_occupation")]
        public string? CUS_OCCUPATION { get; set; }

        [JsonPropertyName("cus_nation")]
        public string? CUS_NATION { get; set; }

        [JsonPropertyName("cus_email")]
        public string? CUS_EMAIL { get; set; }

        [JsonPropertyName("cus_tax_location")]
        public string? CUS_TAX_LOCATION { get; set; }

        [JsonPropertyName("cus_tax_brn")]
        public string? CUS_TAX_BRN { get; set; }

        [JsonPropertyName("cus_tax_brn_nm")]
        public string? CUS_TAX_BRN_NM { get; set; }

        [JsonPropertyName("cus_gain_name")]
        public string? CUS_GAIN_NAME { get; set; }

        [JsonPropertyName("atm_brand")]
        public string? ATM_BRAND { get; set; }

        [JsonPropertyName("atm_model")]
        public string? ATM_MODEL { get; set; }

        [JsonPropertyName("atm_year")]
        public string? ATM_YEAR { get; set; }

        [JsonPropertyName("atm_color")]
        public string? ATM_COLOR { get; set; }

        [JsonPropertyName("atm_reg")]
        public string? ATM_REG { get; set; }

        [JsonPropertyName("atm_regprv")]
        public string? ATM_REGPRV { get; set; }

        [JsonPropertyName("atm_regyear")]
        public string? ATM_REGYEAR { get; set; }

        [JsonPropertyName("atm_type")]
        public string? ATM_TYPE { get; set; }

        [JsonPropertyName("atm_used")]
        public string? ATM_USED { get; set; }

        [JsonPropertyName("atm_repair")]
        public object? ATM_REPAIR { get; set; }

        [JsonPropertyName("atm_cc")]
        public int? ATM_CC { get; set; }

        [JsonPropertyName("atm_engine")]
        public object? ATM_ENGINE { get; set; }

        [JsonPropertyName("atm_seat")]
        public string? ATM_SEAT { get; set; }

        [JsonPropertyName("atm_weight")]
        public int? ATM_WEIGHT { get; set; }

        [JsonPropertyName("atm_sch")]
        public string? ATM_SCH { get; set; }

        [JsonPropertyName("atm_chassis")]
        public string? ATM_CHASSIS { get; set; }

        [JsonPropertyName("atm_price")]
        public int? ATM_PRICE { get; set; }

        [JsonPropertyName("addr_addr1")]
        public string? ADDR_ADDR1 { get; set; }

        [JsonPropertyName("addr_addr2")]
        public string? ADDR_ADDR2 { get; set; }

        [JsonPropertyName("addr_district")]
        public string? ADDR_DISTRICT { get; set; }

        [JsonPropertyName("addr_amphur")]
        public string? ADDR_AMPHUR { get; set; }

        [JsonPropertyName("addr_province")]
        public string? ADDR_PROVINCE { get; set; }

        [JsonPropertyName("addr_postcode")]
        public string? ADDR_POSTCODE { get; set; }

        [JsonPropertyName("addr_tel")]
        public string? ADDR_TEL { get; set; }

        [JsonPropertyName("addr_fax")]
        public string? ADDR_FAX { get; set; }

        [JsonPropertyName("send_prefix")]
        public string? SEND_PREFIX { get; set; }

        [JsonPropertyName("send_name")]
        public string? SEND_NAME { get; set; }

        [JsonPropertyName("send_surname")]
        public string? SEND_SURNAME { get; set; }

        [JsonPropertyName("send_addr1")]
        public string? SEND_ADDR1 { get; set; }

        [JsonPropertyName("send_addr2")]
        public string? SEND_ADDR2 { get; set; }

        [JsonPropertyName("send_district")]
        public string? SEND_DISTRICT { get; set; }

        [JsonPropertyName("send_amphur")]
        public string? SEND_AMPHUR { get; set; }

        [JsonPropertyName("send_province")]
        public string? SEND_PROVINCE { get; set; }

        [JsonPropertyName("send_postcode")]
        public string? SEND_POSTCODE { get; set; }

        [JsonPropertyName("send_tel")]
        public string? SEND_TEL { get; set; }

        [JsonPropertyName("send_fax")]
        public string? SEND_FAX { get; set; }

        [JsonPropertyName("effect_date")]
        public string? EFFECT_DATE { get; set; }

        [JsonPropertyName("expire_date")]
        public string? EXPIRE_DATE { get; set; }

        [JsonPropertyName("print_epolicy")]
        public string? PRINT_EPOLICY { get; set; }

        [JsonPropertyName("inf_note")]
        public string? INF_NOTE { get; set; }

        [JsonPropertyName("paid_date")]
        public string? PAID_DATE { get; set; }

        [JsonPropertyName("paid_type")]
        public string? PAID_TYPE { get; set; }

        [JsonPropertyName("paid_doc_desc")]
        public string? PAID_DOC_DESC { get; set; }

        [JsonPropertyName("paid_crcard")]
        public string? PAID_CRCARD { get; set; }

        [JsonPropertyName("memo_option_01")]
        public string? MEMO_OPTION_01 { get; set; }

        [JsonPropertyName("plc_fsticker")]
        public string? PLC_FSTICKER { get; set; }

        [JsonPropertyName("plc_fpolicy")]
        public string? PLC_FPOLICY { get; set; }

        [JsonPropertyName("plc_psdate")]
        public string? PLC_PSDATE { get; set; }

        [JsonPropertyName("plc_pedate")]
        public string? PLC_PEDATE { get; set; }

        [JsonPropertyName("inf_pol_type")]
        public string? INF_POL_TYPE { get; set; }

        [JsonPropertyName("inf_old_policy")]
        public object? INF_OLD_POLICY { get; set; }

        [JsonPropertyName("inf_prmm_date")]
        public string? INF_PRMM_DATE { get; set; }

        [JsonPropertyName("product_code")]
        public string? PRODUCT_CODE { get; set; }

        [JsonPropertyName("plc_pfund")]
        public int? PLC_PFUND { get; set; }

        [JsonPropertyName("camera_disc")]
        public int? CAMERA_DISC { get; set; }

        [JsonPropertyName("prem_total")]
        public double? PREM_TOTAL { get; set; }

        [JsonPropertyName("plc_pprm")]
        public double? PLC_PPRM { get; set; }

        [JsonPropertyName("plc_fnetprm")]
        public int? PLC_FNETPRM { get; set; }

        [JsonPropertyName("plc_fprm")]
        public int? PLC_FPRM { get; set; }

        [JsonPropertyName("plc_ftotalprm")]
        public double? PLC_FTOTALPRM { get; set; }

        [JsonPropertyName("topup_net")]
        public int? TOPUP_NET { get; set; }

        [JsonPropertyName("topup_code")]
        public string? TOPUP_CODE { get; set; }

        [JsonPropertyName("plc_ctype")]
        public string? PLC_CTYPE { get; set; }

        [JsonPropertyName("reinsurance_code")]
        public string? REINSURANCE_CODE { get; set; }

        [JsonPropertyName("drv_name1")]
        public object? DRV_NAME1 { get; set; }

        [JsonPropertyName("drv_bdate1")]
        public object? DRV_BDATE1 { get; set; }

        [JsonPropertyName("drv_idcard1")]
        public object? DRV_IDCARD1 { get; set; }

        [JsonPropertyName("drv_license1")]
        public object? DRV_LICENSE1 { get; set; }

        [JsonPropertyName("drv_name2")]
        public object? DRV_NAME2 { get; set; }

        [JsonPropertyName("drv_bdate2")]
        public object? DRV_BDATE2 { get; set; }

        [JsonPropertyName("drv_idcard2")]
        public object? DRV_IDCARD2 { get; set; }

        [JsonPropertyName("drv_license2")]
        public object? DRV_LICENSE2 { get; set; }

        [JsonPropertyName("inf_rcp_name")]
        public string? INF_RCP_NAME { get; set; }

        [JsonPropertyName("inf_rcp_date")]
        public string? INF_RCP_DATE { get; set; }

        [JsonPropertyName("inf_rcp_nametype")]
        public string? INF_RCP_NAMETYPE { get; set; }

        [JsonPropertyName("inf_rcp_custype")]
        public string? INF_RCP_CUSTYPE { get; set; }

        [JsonPropertyName("inf_rcp_idcard")]
        public string? INF_RCP_IDCARD { get; set; }

        [JsonPropertyName("inf_rcp_taxid")]
        public string? INF_RCP_TAXID { get; set; }

        [JsonPropertyName("inf_rcp_tax_location")]
        public string? INF_RCP_TAX_LOCATION { get; set; }

        [JsonPropertyName("inf_rcp_tax_brn")]
        public string? INF_RCP_TAX_BRN { get; set; }

        [JsonPropertyName("inf_rcp_tax_brn_nm")]
        public string? INF_RCP_TAX_BRN_NM { get; set; }

        [JsonPropertyName("ini_company_code")]
        public string? INI_COMPANY_CODE { get; set; }

        [JsonPropertyName("servicetype")]
        public string? SERVICETYPE { get; set; }
    }

    public class ObjResponseVoluntary
    {
        [JsonPropertyName("message")]
        public string? MESSAGE { get; set; }

        [JsonPropertyName("message_code")]
        public string? MESSAGE_CODE { get; set; }

        [JsonPropertyName("message_desc")]
        public string? MESSAGE_DESC { get; set; }

        [JsonPropertyName("volpolicyno")]
        public object? VOLPOLICYNO { get; set; }

        [JsonPropertyName("compolicyno")]
        public object? COMPOLICYNO { get; set; }

        [JsonPropertyName("stickerno")]
        public object? STICKERNO { get; set; }

        [JsonPropertyName("appno")]
        public string? APPNO { get; set; }

        [JsonPropertyName("returndate")]
        public string? RETURNDATE { get; set; }

        [JsonPropertyName("returntime")]
        public string? RETURNTIME { get; set; }
    }

    public class PayloadVoluntary
    {
        [JsonPropertyName("motordata")]
        public MotorDataVoluntary? MotorData { get; set; }
    }
}
