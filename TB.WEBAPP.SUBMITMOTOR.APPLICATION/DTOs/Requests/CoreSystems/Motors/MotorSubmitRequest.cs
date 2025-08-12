using System.ComponentModel;
using System.Text.Json.Serialization;

namespace TB.WEBAPP.SUBMITMOTOR.APPLICATION.DTOs.Requests.CoreSystems.Motors
{
    public class MotorSubmitRequest
    {
        [JsonPropertyName("INS_COMPANY_CODE")]
        [Description("รหัสบริษัทประกันภัย")]
        public string? InsureCompanyCode { get; set; }

        [JsonPropertyName("AGENT_CODE")]
        [Description("รหัสนายหน้า")]
        public string? AgentCode { get; set; }

        [JsonPropertyName("POLICY_TYPE")]
        [Description("ประเภทกรมธรรม์")]
        public string? PolicyType { get; set; }

        [JsonPropertyName("QUOTATION_NO")]
        [Description("เลขที่ใบเสนอราคา")]
        public string? QuotationNumber { get; set; }

        [JsonPropertyName("PRENAME_CODE")]
        [Description("คำนำหน้าผู้เอาประกัน")]
        public string? PrenameCode { get; set; }

        [JsonPropertyName("INS_NAME")]
        [Description("ชื่อผู้เอาประกัน")]
        public string? InsureName { get; set; }

        [JsonPropertyName("INS_SURNAME")]
        [Description("นามสกุลลผู้เอาประกัน")]
        public string? InsureSurName { get; set; }

        [JsonPropertyName("INS_IDCARD")]
        [Description("เลขบัตรประจำตัวประชาชน,ผู้เสียภาษี,Passport ผู้เอาประกัน")]
        public string? InsureIDCard { get; set; }

        [JsonPropertyName("INS_BIRTHDATE")]
        [Description("วันเกิดผู้เอาประกัน")]
        public string? InsureBirthDate { get; set; }

        [JsonPropertyName("INS_ADDR")]
        [Description("บ้านเลขที่ผู้เอาประกัน")]
        public string? InsureAddress { get; set; }

        [JsonPropertyName("INS_SUBDISTRICT_CODE")]
        [Description("ตำบลผู้เอาประกัน")]
        public string? InsureSubDistrictCode { get; set; }

        [JsonPropertyName("INS_DISTRICT_CODE")]
        [Description("อำเภทผู้เอาประกัน")]
        public string? InsureDistrictCode { get; set; }

        [JsonPropertyName("INS_PROVINCE_CODE")]
        [Description("จังหวัดผู้เอาประกัน")]
        public string? InsureProvinceCode { get; set; }

        [JsonPropertyName("INS_ZIP_CODE")]
        [Description("รหัสไปรษณีย์ผู้เอาประกัน")]
        public string? InsureZipCode { get; set; }

        [JsonPropertyName("OCCUP_CODE")]
        [Description("อาชีพผู้เอาประกัน")]
        public string? OccupationCode { get; set; }

        [JsonPropertyName("SENDDOC_ADDR")]
        [Description("ที่อยู่จัดส่งเอกสาร")]
        public string? SendDocumentAddressNo { get; set; }

        [JsonPropertyName("SENDDOC_SUBDISTRICT_CODE")]
        [Description("ตำบลจัดส่งเอกสาร")]
        public string? SendDocumentSubDistrictCode { get; set; }

        [JsonPropertyName("SENDDOC_DISTRICT_CODE")]
        [Description("อำเภอจัดส่งเอกสาร")]
        public string? SendDocumentDistrictCode { get; set; }

        [JsonPropertyName("SENDDOC_PROVINCE_CODE")]
        [Description("จังหวัดจักส่งเอกสาร")]
        public string? SendDocumentProvinceCode { get; set; }

        [JsonPropertyName("SENDDOC_ZIP_CODE")]
        [Description("รหัสไปรษณีย์จัดส่งเอกสาร")]
        public string? SendDocumentZipCode { get; set; }

        [JsonPropertyName("PHONE_NO")]
        [Description("เบอร์โทรศัพท์ผู้เอาประกัน")]
        public string? PhoneNumber { get; set; }

        [JsonPropertyName("EMAIL_ADDRESS")]
        [Description("อีเมลนายหน้า")]
        public string? EmailAddress { get; set; }

        [JsonPropertyName("BENEFIC_NAME")]
        [Description("ผู้รับผลประโยชน์")]
        public string? BeneficName { get; set; }

        [JsonPropertyName("WITH_COMPLULSORY")]
        [Description("ซื้องานควบ พรบ.")]
        public string? WithCompulsory { get; set; }

        [JsonPropertyName("VOL_START_DATE")]
        [Description("วันเริ่มคุ้มครอง สมัครใจ")]
        public string? VoluntaryStartDate { get; set; }

        [JsonPropertyName("VOL_END_DATE")]
        [Description("วันสิ้นสุดคุ้มครอง สมัครใจ")]
        public string? VoluntaryEndDate { get; set; }

        [JsonPropertyName("COP_START_DATE")]
        [Description("วันเริ่มคุ้มครอง พรบ.")]
        public string? CompulsoryStartDate { get; set; }

        [JsonPropertyName("COP_END_DATE")]
        [Description("วันสิ้นสุดคุ้มครอง พรบ.")]
        public string? CompulsoryEndDate { get; set; }

        [JsonPropertyName("FLAG_REPAIR")]
        [Description("เกรดการซ่อม")]
        public string? FlagRepair { get; set; }

        [JsonPropertyName("CAR_REGNO_PRE")]
        [Description("เลขทะเบียนรถยนต์ ส่วนตัวอักษร")]
        public string? CarRegisNoPre { get; set; }

        [JsonPropertyName("CAR_REGNO")]
        [Description("เลขทะเบียนรถยนต์ ส่วนตัวเลข")]
        public string? CarRegisNo { get; set; }

        [JsonPropertyName("CAR_PROVINCE_CODE")]
        [Description("จังหวัดทะเบียนรถยนต์")]
        public string? CarProvinceCode { get; set; }

        [JsonPropertyName("CAR_CODE")]
        public string? CarCode { get; set; }

        [JsonPropertyName("CAR_BRAND_CODE")]
        public string? CarBrandCode { get; set; }

        [JsonPropertyName("CAR_MODEL_CODE")]
        public string? CarModelCode { get; set; }

        [JsonPropertyName("CAR_YEAR")]
        public string? CarYear { get; set; }

        [JsonPropertyName("CAR_CC")]
        public string? CarCc { get; set; }

        [JsonPropertyName("CAR_CHASSISNBR")]
        public string? CarChassis { get; set; }

        [JsonPropertyName("CAR_ENGINENO")]
        public string? CarEngineNo { get; set; }

        [JsonPropertyName("CAR_COLOR_CODE")]
        public string? CarColorCode { get; set; }

        [JsonPropertyName("COV_MONEYCOST")]
        public int CoverageMoneyCost { get; set; }

        [JsonPropertyName("COV_CARDAMAGE")]
        public int CoverageCarDamage { get; set; }

        [JsonPropertyName("COV_CARLOSS")]
        public int CoverageCarLoss { get; set; }

        [JsonPropertyName("COV_FLOODAMT")]
        public int CoverageFloodAmount { get; set; }

        [JsonPropertyName("VOL_NET_PREMIUM_AMT")]
        public int VoluntaryNetPremiumAmount { get; set; }

        [JsonPropertyName("VOL_TOTAL_PREMIUM_AMT")]
        public double VoluntaryTotalPremiumAmount { get; set; }

        [JsonPropertyName("COP_NET_PREMIUM_AMT")]
        public double CompulsoryNetPremiumAmount { get; set; }

        [JsonPropertyName("COP_TOTAL_PREMIUM_AMT")]
        public double CompulsoryTotalPremiumAmount { get; set; }

        [JsonPropertyName("REMARKS")]
        public string? Remarks { get; set; }

        [JsonPropertyName("FLG_RED_PLATE")]
        public string? FlagRedPlate { get; set; }

        [JsonPropertyName("CAR_REGISTRATION")]
        public string? CarRegistration { get; set; }

        [JsonPropertyName("CAR_SEAT")]
        public string? CarSeat { get; set; }

        [JsonPropertyName("FLG_PERSON")]
        public string? FlagPerson { get; set; }

        [JsonPropertyName("STATUS_CODE")]
        public string? StatusCode { get; set; }

        [JsonPropertyName("FLG_SEND_DOCUMENT")]
        public string? FlagSendDocument { get; set; }

        [JsonPropertyName("SUBINS_TYPE")]
        public string? SubInsureTypeVoluntary { get; set; }

        [JsonPropertyName("SUBINS_TYPE_CMI")]
        public string? SubInsureTypeCompulsory { get; set; }

        [JsonPropertyName("INS_EMAIL_ADDRESS")]
        public string? InsureEmailAddress { get; set; }

        [JsonPropertyName("GENDER")]
        public string? Gender { get; set; }

        [JsonPropertyName("CAR_BODY_TYPE")]
        public string? CarBodyType { get; set; }

        [JsonPropertyName("CHANNEL_PAY")]
        [Description("QR=QRCode , SM=TTB Smart Shop , OT=อื่นๆ , PO=Payment Online , CR=Credit Card , IN=Installment")]
        public string? ChannelPayment { get; set; }

        [JsonPropertyName("NUM_INSTALL")]
        [Description("จำนวนงวด ผ่อนชำระ")]
        public int NumberInstall { get; set; }

        [JsonPropertyName("CHANNEL_REF1")]
        public string? ChannelReference1 { get; set; }

        [JsonPropertyName("CHANNEL_REF2")]
        public string? ChannelReference2 { get; set; }

        [JsonPropertyName("FLAG_PRINT_POLICY")]
        public string? FlagPrintPolicy { get; set; }

        [JsonPropertyName("CUST_TYPECARD_CODE")]
        public string? CustomerTypeCardCode { get; set; }

        [JsonPropertyName("FLG_RENEWAL_VMI")]
        public string? FlagRenewalVoluntary { get; set; }

        [JsonPropertyName("OLD_POLICY_VMI")]
        public string? OldPolicyVoluntary { get; set; }

        [JsonPropertyName("FLG_RENEWAL_CMI")]
        public string? FlagRenewalCompulsory { get; set; }

        [JsonPropertyName("OLD_POLICY_CMI")]
        public string? OldPolicyCompulsory { get; set; }

        [JsonPropertyName("FLG_POWERED_CAR")]
        public string? FlagPoweredCar { get; set; }

        [JsonPropertyName("EV_KW_AMT")]
        public double EvKiloWattAmount { get; set; }

        [JsonPropertyName("EV_BATT_SERIAL")]
        public string? EvBatterySerial { get; set; }

        [JsonPropertyName("EV_WALLCHR_SERIAL")]
        public string? EvWallChargerSerial { get; set; }

        [JsonPropertyName("EV_BATT_DATE")]
        public string? EvBatteryDate { get; set; }

        [JsonPropertyName("DRV1_PRENAME_CODE")]
        public string? Driver1PrenameCode { get; set; }

        [JsonPropertyName("DRV1_NAME")]
        public string? Driver1Name { get; set; }

        [JsonPropertyName("DRV1_SURNAME")]
        public string? Driver1Surname { get; set; }

        [JsonPropertyName("DRV1_BIRTHDATE")]
        public string? Driver1BirthDate { get; set; }

        [JsonPropertyName("DRV1_SEX")]
        public string? Driver1Gender { get; set; }

        [JsonPropertyName("DRV1_OCCP")]
        public string? Driver1Occupation { get; set; }

        [JsonPropertyName("DRV1_LICENSE")]
        public string? Driver1License { get; set; }

        [JsonPropertyName("DRV1_IDCARD")]
        public string? Driver1IDCard { get; set; }

        [JsonPropertyName("DRV1_BEH_LEVEL")]
        public string? Driver1BehaviorLevel { get; set; }

        [JsonPropertyName("DRV1_CONSENT")]
        public string? Driver1Consent { get; set; }

        [JsonPropertyName("DRV2_PRENAME_CODE")]
        public string? Driver2PrenameCode { get; set; }

        [JsonPropertyName("DRV2_NAME")]
        public string? Driver2Name { get; set; }

        [JsonPropertyName("DRV2_SURNAME")]
        public string? Driver2Surname { get; set; }

        [JsonPropertyName("DRV2_BIRTHDATE")]
        public string? Driver2BirthDate { get; set; }

        [JsonPropertyName("DRV2_SEX")]
        public string? Driver2Gender { get; set; }

        [JsonPropertyName("DRV2_OCCP")]
        public string? Driver2Occupation { get; set; }

        [JsonPropertyName("DRV2_LICENSE")]
        public string? Driver2License { get; set; }

        [JsonPropertyName("DRV2_IDCARD")]
        public string? Driver2IDCard { get; set; }

        [JsonPropertyName("DRV2_BEH_LEVEL")]
        public string? Driver2BehaviorLevel { get; set; }

        [JsonPropertyName("DRV2_CONSENT")]
        public string? Driver2Consent { get; set; }

        [JsonPropertyName("DRV3_PRENAME_CODE")]
        public string? Driver3PrenameCode { get; set; }

        [JsonPropertyName("DRV3_NAME")]
        public string? Driver3Name { get; set; }

        [JsonPropertyName("DRV3_SURNAME")]
        public string? Driver3Surname { get; set; }

        [JsonPropertyName("DRV3_BIRTHDATE")]
        public string? Driver3BirthDate { get; set; }

        [JsonPropertyName("DRV3_SEX")]
        public string? Driver3Gender { get; set; }

        [JsonPropertyName("DRV3_OCCP")]
        public string? Driver3Occupation { get; set; }

        [JsonPropertyName("DRV3_LICENSE")]
        public string? Driver3License { get; set; }

        [JsonPropertyName("DRV3_IDCARD")]
        public string? Driver3IDCard { get; set; }

        [JsonPropertyName("DRV3_BEH_LEVEL")]
        public string? Driver3BehaviorLevel { get; set; }

        [JsonPropertyName("DRV3_CONSENT")]
        public string? Driver3Consent { get; set; }

        [JsonPropertyName("DRV4_PRENAME_CODE")]
        public string? Driver4PrenameCode { get; set; }

        [JsonPropertyName("DRV4_NAME")]
        public string? Driver4Name { get; set; }

        [JsonPropertyName("DRV4_SURNAME")]
        public string? Driver4Surname { get; set; }

        [JsonPropertyName("DRV4_BIRTHDATE")]
        public string? Driver4BirthDate { get; set; }

        [JsonPropertyName("DRV4_SEX")]
        public string? Driver4Gender { get; set; }

        [JsonPropertyName("DRV4_OCCP")]
        public string? Driver4Occupation { get; set; }

        [JsonPropertyName("DRV4_LICENSE")]
        public string? Driver4License { get; set; }

        [JsonPropertyName("DRV4_IDCARD")]
        public string? Driver4IDCard { get; set; }

        [JsonPropertyName("DRV4_BEH_LEVEL")]
        public string? Driver4BehaviorLevel { get; set; }

        [JsonPropertyName("DRV4_CONSENT")]
        public string? Driver4Consent { get; set; }

        [JsonPropertyName("DRV5_PRENAME_CODE")]
        public string? Driver5PrenameCode { get; set; }

        [JsonPropertyName("DRV5_NAME")]
        public string? Driver5Name { get; set; }

        [JsonPropertyName("DRV5_SURNAME")]
        public string? Driver5Surname { get; set; }

        [JsonPropertyName("DRV5_BIRTHDATE")]
        public string? Driver5BirthDate { get; set; }

        [JsonPropertyName("DRV5_SEX")]
        public string? Driver5Gender { get; set; }

        [JsonPropertyName("DRV5_OCCP")]
        public string? Driver5Occupation { get; set; }

        [JsonPropertyName("DRV5_LICENSE")]
        public string? Driver5License { get; set; }

        [JsonPropertyName("DRV5_IDCARD")]
        public string? Driver5IDCard { get; set; }

        [JsonPropertyName("DRV5_BEH_LEVEL")]
        public string? Driver5BehaviorLevel { get; set; }

        [JsonPropertyName("DRV5_CONSENT")]
        public string? Driver5Consent { get; set; }
    }
}
