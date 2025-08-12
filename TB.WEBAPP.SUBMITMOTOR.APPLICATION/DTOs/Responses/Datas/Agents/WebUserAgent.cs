using System.Text.Json.Serialization;

namespace TB.WEBAPP.SUBMITMOTOR.APPLICATION.DTOs.Responses.Datas.Agents
{
    public class WebUserAgent
    {
        [JsonPropertyName("id")]
        public decimal Id { get; set; }

        [JsonPropertyName("date_create")]
        public DateTime? DateCreate { get; set; }

        [JsonPropertyName("date_update")]
        public DateTime? DateUpdate { get; set; }

        [JsonPropertyName("create_by")]
        public string? CreateBy { get; set; }

        [JsonPropertyName("agent_type")]
        public string? AgentType { get; set; }

        [JsonPropertyName("user_id")]
        public string? UserID { get; set; }

        [JsonPropertyName("password")]
        public string? Password { get; set; }

        [JsonPropertyName("title_code")]
        public string? TitleCode { get; set; }

        [JsonPropertyName("title_txt")]
        public string? TitleTxt { get; set; }

        [JsonPropertyName("name")]
        public string? Name { get; set; }

        [JsonPropertyName("last_name")]
        public string? LastName { get; set; }

        [JsonPropertyName("id_card")]
        public string? IdCard { get; set; }

        [JsonPropertyName("mobile_phone")]
        public string? MobilePhone { get; set; }

        [JsonPropertyName("email")]
        public string? Email { get; set; }

        [JsonPropertyName("otp_to")]
        public string? OTPTo { get; set; }

        [JsonPropertyName("business_type")]
        public string? BusinessType { get; set; }

        [JsonPropertyName("general_business_type")]
        public string? GeneralBusinessType { get; set; }

        [JsonPropertyName("general_business_type_txt")]
        public string? GeneralBusinessTypeTxt { get; set; }

        [JsonPropertyName("general_business_type_other_txt")]
        public string? GeneralBusinessTypeOtherTxt { get; set; }

        [JsonPropertyName("garage_name")]
        public string? GarageName { get; set; }

        [JsonPropertyName("garage_phone")]
        public string? GaragePhone { get; set; }

        [JsonPropertyName("garage_province")]
        public string? GarageProvince { get; set; }

        [JsonPropertyName("garage_province_txt")]
        public string? GarageProvinceTxt { get; set; }

        [JsonPropertyName("bu_info")]
        public string? BUInfo { get; set; }

        [JsonPropertyName("bu_info_txt")]
        public string? BUInfoTxt { get; set; }

        [JsonPropertyName("ref_agent_code")]
        public string? RefAgentCode { get; set; }

        [JsonPropertyName("ref_agent_name")]
        public string? RefAgentName { get; set; }

        [JsonPropertyName("newsletter_accept")]
        public string? NewsletterAccept { get; set; }

        [JsonPropertyName("img_profile")]
        public string? ImgProfile { get; set; }

        [JsonPropertyName("title_code_en")]
        public string? TitleCodeEn { get; set; }

        [JsonPropertyName("title_txt_en")]
        public string? TitleTxtEn { get; set; }

        [JsonPropertyName("name_en")]
        public string? NameEn { get; set; }

        [JsonPropertyName("last_name_en")]
        public string? LastNameEn { get; set; }

        [JsonPropertyName("birthdate")]
        public string? Birthdate { get; set; }

        [JsonPropertyName("gender")]
        public string? Gender { get; set; }

        [JsonPropertyName("nationality")]
        public string? Nationality { get; set; }

        [JsonPropertyName("education_level")]
        public string? EducationLevel { get; set; }

        [JsonPropertyName("education_level_txt")]
        public string? EducationLevelTxt { get; set; }

        [JsonPropertyName("academy")]
        public string? Academy { get; set; }

        [JsonPropertyName("salary")]
        public string? Salary { get; set; }

        [JsonPropertyName("marital_status")]
        public string? MaritalStatus { get; set; }

        [JsonPropertyName("address_name")]
        public string? AddressName { get; set; }

        [JsonPropertyName("address_room")]
        public string? AddressRoom { get; set; }

        [JsonPropertyName("address_no")]
        public string? AddressNo { get; set; }

        [JsonPropertyName("address_alley")]
        public string? AddressAlley { get; set; }

        [JsonPropertyName("address_street")]
        public string? AddressStreet { get; set; }

        [JsonPropertyName("address_province")]
        public string? AddressProvince { get; set; }

        [JsonPropertyName("address_province_txt")]
        public string? AddressProvinceTxt { get; set; }

        [JsonPropertyName("address_district")]
        public string? AddressDistrict { get; set; }

        [JsonPropertyName("address_district_txt")]
        public string? AddressDistrictTxt { get; set; }

        [JsonPropertyName("address_sub_district")]
        public string? AddressSubDistrict { get; set; }

        [JsonPropertyName("address_sub_district_txt")]
        public string? AddressSubDistrictTxt { get; set; }

        [JsonPropertyName("address_zipcode")]
        public string? AddressZipCode { get; set; }

        [JsonPropertyName("bank_account")]
        public string? BankAccount { get; set; }

        [JsonPropertyName("bank_account_txt")]
        public string? BankAccountTxt { get; set; }

        [JsonPropertyName("bank_branch")]
        public string? BankBranch { get; set; }

        [JsonPropertyName("bank_account_name")]
        public string? BankAccountName { get; set; }

        [JsonPropertyName("bank_account_no")]
        public string? BankAccountNo { get; set; }

        [JsonPropertyName("career_code")]
        public string? CareerCode { get; set; }

        [JsonPropertyName("career_txt")]
        public string? CareerTxt { get; set; }

        [JsonPropertyName("career_other_txt")]
        public string? CareerOtherTxt { get; set; }

        [JsonPropertyName("line_id")]
        public string? LineID { get; set; }

        [JsonPropertyName("facebook")]
        public string? Facebook { get; set; }

        [JsonPropertyName("share_qr_code")]
        public string? ShareQRCode { get; set; }

        [JsonPropertyName("share_qr_code_position")]
        public string? ShareQRCodePosition { get; set; }

        [JsonPropertyName("share_qr_code_text_info")]
        public string? ShareQRCodeTextInfo { get; set; }

        [JsonPropertyName("share_qr_code_position_en")]
        public string? ShareQRCodePositionEn { get; set; }

        [JsonPropertyName("share_qr_code_text_info_en")]
        public string? ShareQRCodeTextInfoEn { get; set; }

        [JsonPropertyName("mlm_agent_code")]
        public string? MLMAgentCode { get; set; }

        [JsonPropertyName("mlm_agent_name")]
        public string? MLMAgentName { get; set; }

        [JsonPropertyName("password_from_import")]
        public string? PasswordFromImport { get; set; }

        [JsonPropertyName("garage_office_phone")]
        public string? GarageOfficePhone { get; set; }

        [JsonPropertyName("id_card_sha256")]
        public string? IdCardSHA256 { get; set; }

        [JsonPropertyName("level_x")]
        public string? LevelX { get; set; }

        [JsonPropertyName("account_type")]
        public string? AccountType { get; set; }

        [JsonPropertyName("credit_limit")]
        public decimal? CreditLimit { get; set; }

        [JsonPropertyName("top_agent_code")]
        public string? TopAgentCode { get; set; }

        [JsonPropertyName("credit_used")]
        public decimal? CreditUsed { get; set; }

        [JsonPropertyName("credit_used_update")]
        public DateTime? CreditUsedUpdate { get; set; }

        [JsonPropertyName("active")]
        public string? Active { get; set; }

        [JsonPropertyName("family_type")]
        public string? FamilyType { get; set; }

        [JsonPropertyName("branch_affiliated_code")]
        public string? BranchAffiliatedCode { get; set; }

        [JsonPropertyName("province_affiliated_code")]
        public string? ProvinceAffiliatedCode { get; set; }

        [JsonPropertyName("license")]
        public string? License { get; set; }

        [JsonPropertyName("date_of_no")]
        public string? DateOfNo { get; set; }

        [JsonPropertyName("date_of_issue")]
        public string? DateOfIssue { get; set; }

        [JsonPropertyName("date_of_expiry")]
        public string? DateOfExpiry { get; set; }

        [JsonPropertyName("license2")]
        public string? License2 { get; set; }

        [JsonPropertyName("date2_of_no")]
        public string? Date2OfNo { get; set; }

        [JsonPropertyName("date2_of_issue")]
        public string? Date2OfIssue { get; set; }

        [JsonPropertyName("date2_of_expiry")]
        public string? Date2OfExpiry { get; set; }
    }
}