using System.Text.Json.Serialization;

namespace TB.WEBAPP.SUBMITMOTOR.APPLICATION.DTOs.Entities
{
    public class WebNotifyCoreSystemDraft
    {
        [JsonPropertyName("transaction_id")]
        public string? TransactionId { get; set; }

        [JsonPropertyName("date_create")]
        public DateTime? DateCreate { get; set; }

        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("quotation_no")]
        public string? QuotationNumber { get; set; }

        [JsonPropertyName("appno_vol")]
        public string? ApplicationNoVoluntary { get; set; }

        [JsonPropertyName("appno_com")]
        public string? ApplicationNoCompulsory { get; set; }

        [JsonPropertyName("agent_code")]
        public string? AgentCode { get; set; }

        [JsonPropertyName("policy_type")]
        public string? PolicyType { get; set; }

        [JsonPropertyName("ins_company_code")]
        public string? InsureCompanyCode { get; set; }

        [JsonPropertyName("prename_code")]
        public string? PrenameCode { get; set; }

        [JsonPropertyName("ins_name")]
        public string? InsureName { get; set; }

        [JsonPropertyName("ins_surname")]
        public string? InsureSurname { get; set; }

        [JsonPropertyName("ins_idcard")]
        public string? InsureIdCard { get; set; }

        [JsonPropertyName("ins_birthdate")]
        public DateTime? InsureBirthdate { get; set; }

        [JsonPropertyName("ins_addr")]
        public string? InsureAddress { get; set; }

        [JsonPropertyName("ins_province_code")]
        public string? InsureProvinceCode { get; set; }

        [JsonPropertyName("ins_district_code")]
        public string? InsureDistrictCode { get; set; }

        [JsonPropertyName("ins_subdistrict_code")]
        public string? InsureSubDistrictCode { get; set; }

        [JsonPropertyName("ins_zip_code")]
        public string? InsureZipCode { get; set; }

        [JsonPropertyName("ins_email_address")]
        public string? InsureEmailAddress { get; set; }

        [JsonPropertyName("gender")]
        public string? Gender { get; set; }

        [JsonPropertyName("phone_no")]
        public string? PhoneNumber { get; set; }

        [JsonPropertyName("occup_code")]
        public string? OccupationCode { get; set; }

        [JsonPropertyName("flg_person")]
        public string? FlagPerson { get; set; }

        [JsonPropertyName("senddoc_addr")]
        public string? SendDocumentAddressNo { get; set; }

        [JsonPropertyName("senddoc_province_code")]
        public string? SendDocumentProvinceCode { get; set; }

        [JsonPropertyName("senddoc_district_code")]
        public string? SendDocumentDistrictCode { get; set; }

        [JsonPropertyName("senddoc_subdistrict_code")]
        public string? SendDocumentSubdistrictCode { get; set; }

        [JsonPropertyName("senddoc_zip_code")]
        public string? SendDocumentZipCode { get; set; }

        [JsonPropertyName("email_address")]
        public string? EmailAddress { get; set; }

        [JsonPropertyName("benefic_name")]
        public string? BeneficName { get; set; }

        [JsonPropertyName("flg_send_document")]
        public string? FlagSendDocument { get; set; }

        [JsonPropertyName("with_compulsory")]
        public string? WithCompulsory { get; set; }

        [JsonPropertyName("vol_start_date")]
        public DateTime? VoluntaryStartDate { get; set; }

        [JsonPropertyName("vol_end_date")]
        public DateTime? VoluntaryEndDate { get; set; }

        [JsonPropertyName("cop_start_date")]
        public DateTime? CompulsoryStartDate { get; set; }

        [JsonPropertyName("cop_end_date")]
        public DateTime? CompulsoryEndDate { get; set; }

        [JsonPropertyName("car_regno_pre")]
        public string? CarRegnoPre { get; set; }

        [JsonPropertyName("car_regno")]
        public string? CarRegno { get; set; }

        [JsonPropertyName("car_province_code")]
        public string? CarProvinceCode { get; set; }

        [JsonPropertyName("car_code")]
        public string? CarCode { get; set; }

        [JsonPropertyName("car_brand_code")]
        public string? CarBrandCode { get; set; }

        [JsonPropertyName("car_model_code")]
        public string? CarModelCode { get; set; }

        [JsonPropertyName("car_year")]
        public string? CarYear { get; set; }

        [JsonPropertyName("car_cc")]
        public string? CarCc { get; set; }

        [JsonPropertyName("car_weight")]
        public int? CarWeight { get; set; }

        [JsonPropertyName("car_seat")]
        public int? CarSeat { get; set; }

        [JsonPropertyName("car_chassisnbr")]
        public string? CarChassis { get; set; }

        [JsonPropertyName("car_engineno")]
        public string? CarEngineNo { get; set; }

        [JsonPropertyName("car_color_code")]
        public string? CarColorCode { get; set; }

        [JsonPropertyName("car_registration")]
        public string? CarRegistration { get; set; }

        [JsonPropertyName("car_body_type")]
        public string? CarBodyType { get; set; }

        [JsonPropertyName("flg_repair")]
        public string? FlagRepair { get; set; }

        [JsonPropertyName("cov_moneycost")]
        public int? CoverageMoneyCost { get; set; }

        [JsonPropertyName("cov_cardamage")]
        public int? CoverageCarDamage { get; set; }

        [JsonPropertyName("cov_carloss")]
        public int? CoverageCarLoss { get; set; }

        [JsonPropertyName("cov_floodamt")]
        public int? CoverageFloodAmount { get; set; }

        [JsonPropertyName("vol_net_premium_amt")]
        public int? VoluntaryNetPremiumAmount { get; set; }

        [JsonPropertyName("vol_total_premium_amt")]
        public decimal? VoluntaryTotalPremiumAmount { get; set; }

        [JsonPropertyName("cop_net_premium_amt")]
        public int? CompulsoryNetPremiumAmount { get; set; }

        [JsonPropertyName("cop_total_premium_amt")]
        public decimal? CompulsoryTotalPremiumAmount { get; set; }

        [JsonPropertyName("flg_red_plate")]
        public string? FlagRedPlate { get; set; }

        [JsonPropertyName("flg_print_policy")]
        public string? FlagPrintPolicy { get; set; }

        [JsonPropertyName("flg_renewal_vmi")]
        public string? FlagRenewalVoluntary { get; set; }

        [JsonPropertyName("old_policy_vmi")]
        public string? OldPolicyVoluntary { get; set; }

        [JsonPropertyName("flg_renewal_cmi")]
        public string? FlagRenewalCompulsory { get; set; }

        [JsonPropertyName("old_policy_cmi")]
        public string? OldPolicyCompulsory { get; set; }

        [JsonPropertyName("flg_powered_car")]
        public string? FlagPoweredCar { get; set; }

        [JsonPropertyName("ev_kw_amt")]
        public decimal? EvKwAmount { get; set; }

        [JsonPropertyName("ev_batt_serial")]
        public string? EvBattSerial { get; set; }

        [JsonPropertyName("ev_wallchr_serial")]
        public string? EvWallChargerSerial { get; set; }

        [JsonPropertyName("ev_batt_date")]
        public DateTime? EvBattDate { get; set; }

        [JsonPropertyName("status_code")]
        public string? StatusCode { get; set; }

        [JsonPropertyName("subins_type_vmi")]
        public string? SubInsureTypeVoluntary { get; set; }

        [JsonPropertyName("subins_type_cmi")]
        public string? SubInsureTypeCompulsory { get; set; }

        [JsonPropertyName("flg_commission")]
        public string? FlagCommission { get; set; }

        [JsonPropertyName("channel_pay")]
        public string? ChannelPayment { get; set; }

        [JsonPropertyName("channel_ref1")]
        public string? ChannelReference1 { get; set; }

        [JsonPropertyName("channel_ref2")]
        public string? ChannelReference2 { get; set; }

        [JsonPropertyName("contract_no")]
        public string? ContractNumber { get; set; }

        [JsonPropertyName("remarks")]
        public string? Remarks { get; set; }
    }
}
