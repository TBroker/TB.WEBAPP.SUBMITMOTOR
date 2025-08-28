using AspNetCoreGeneratedDocument;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Globalization;
using System.Net;
using System.Web;
using TB.WEBAPP.SUBMITMOTOR.Adapters;
using TB.WEBAPP.SUBMITMOTOR.APPLICATION.DTOs.Entities;
using TB.WEBAPP.SUBMITMOTOR.APPLICATION.DTOs.Requests.CoreSystems.Agents;
using TB.WEBAPP.SUBMITMOTOR.APPLICATION.DTOs.Requests.CoreSystems.Installments;
using TB.WEBAPP.SUBMITMOTOR.APPLICATION.DTOs.Requests.CoreSystems.Motors;
using TB.WEBAPP.SUBMITMOTOR.APPLICATION.DTOs.Requests.Data.Motors;
using TB.WEBAPP.SUBMITMOTOR.APPLICATION.DTOs.Requests.Data.Reports;
using TB.WEBAPP.SUBMITMOTOR.APPLICATION.DTOs.Responses.CoreSystems.Agents;
using TB.WEBAPP.SUBMITMOTOR.APPLICATION.DTOs.Responses.CoreSystems.Installments;
using TB.WEBAPP.SUBMITMOTOR.APPLICATION.DTOs.Responses.CoreSystems.Motors;
using TB.WEBAPP.SUBMITMOTOR.APPLICATION.DTOs.Responses.Data.Installments;
using TB.WEBAPP.SUBMITMOTOR.APPLICATION.DTOs.Responses.Data.Reports;
using TB.WEBAPP.SUBMITMOTOR.APPLICATION.Features.UploadFile;
using TB.WEBAPP.SUBMITMOTOR.APPLICATION.Interfaces;
using TB.WEBAPP.SUBMITMOTOR.APPLICATION.Interfaces.CoreSystem.Agents;
using TB.WEBAPP.SUBMITMOTOR.APPLICATION.Interfaces.CoreSystems.Installments;
using TB.WEBAPP.SUBMITMOTOR.APPLICATION.Interfaces.CoreSystems.Motors;
using TB.WEBAPP.SUBMITMOTOR.APPLICATION.Interfaces.Data.Installments;
using TB.WEBAPP.SUBMITMOTOR.APPLICATION.Interfaces.Data.Motors;
using TB.WEBAPP.SUBMITMOTOR.APPLICATION.Interfaces.Data.Reports;
using TB.WEBAPP.SUBMITMOTOR.APPLICATION.Interfaces.Utilities;
using TB.WEBAPP.SUBMITMOTOR.APPLICATION.Interfaces.Verifies;
using TB.WEBAPP.SUBMITMOTOR.DOMIAN.Enums;
using TB.WEBAPP.SUBMITMOTOR.INFRASTRUCTURE.Configurations;

namespace TB.WEBAPP.SUBMITMOTOR.Controllers.SubmitMotor
{
    public class SubmitMotorNewController(
         IHostEnvironment hostEnvironment
        , IAgentUseCase agentUseCase
        , IInstallmentUseCase installmentUseCase
        , IJwtReaderService jwtReaderService
        , ILogger<SubmitMotorNewController> logger
        , IMediator mediator
        , IMotorUseCase motorUseCase
        , IOptions<AppSetting> appSetting
        , IPaymentInstallmentUseCase paymentInstallmentUseCase
        , IReportUseCase reportUseCase
        , IServiceVerifyUseCase verifyUseCase
        , ISubmitMotorUseCase submitMotorUseCase
        , IUtilityHelper utilityHelper
        ) : Controller
    {
        private static readonly CultureInfo culture = CultureInfo.GetCultureInfo("en-GB");
        private static readonly string transactionId = Guid.NewGuid().ToString().ToUpper();
        private readonly IAgentUseCase _agentUseCase = agentUseCase;
        private readonly AppSetting _appSetting = appSetting.Value;
        private readonly IHostEnvironment _hostEnvironment = hostEnvironment;
        private readonly IInstallmentUseCase _installmentUseCase = installmentUseCase;
        private readonly IJwtReaderService _jwtReaderService = jwtReaderService;
        private readonly ILogger<SubmitMotorNewController> _logger = logger;
        private readonly IMediator _mediator = mediator;
        private readonly IMotorUseCase _motorUseCase = motorUseCase;
        private readonly IPaymentInstallmentUseCase _paymentInstallmentUseCase = paymentInstallmentUseCase;
        private readonly IReportUseCase _reportUseCase = reportUseCase;
        private readonly ISubmitMotorUseCase _submitMotorUseCase = submitMotorUseCase;
        private readonly IUtilityHelper _utilityHelper = utilityHelper;
        private readonly IServiceVerifyUseCase _verifyUseCase = verifyUseCase;

        public async Task<IActionResult> Index()
        {
            var agentCookies = Request.Cookies["AgentAuth"];
            if (string.IsNullOrEmpty(agentCookies))
            {
                return RedirectToAction("Login", "Auth");
            }

            var agentInfo = _jwtReaderService.ReadAgentInfo(agentCookies);
            if (agentInfo == null)
            {
                return RedirectToAction("Login", "Auth");
            }

            var agentDetail = await FetchAgentDetail(agentInfo.Value.AgentCode);
            if (agentDetail.AgentCode == null)
            {
                return RedirectToAction("Login", "Auth");
            }

            ViewBag.AgentCode = agentDetail!.AgentCode;
            ViewBag.Name = agentDetail!.Name;

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SubmitMotor(IFormCollection collection)
        {
            try
            {
                // ตรวจสอบว่ามีข้อมูลใน form หรือไม่
                if (!ModelState.IsValid) return Json(new { success = false, message = "กรุณากรอกข้อมูลให้ครบถ้วน" });

                // ตรวจสอบว่ามีไฟล์ที่ต้องการอัปโหลดหรือไม่
                var isSuccessFile = await IsCheckValidInputFileAsync(collection);
                if (!isSuccessFile) return Json(new { success = false, message = "ข้อมูลไฟล์ไม่ถูกต้อง" });

                // ตรวจสอบว่ามีข้อมูลใบเสนอราคาหรือไม่
                var quotationDetail = await FetchQuotationReportDetail(collection);
                if (string.IsNullOrEmpty(quotationDetail.QuotationNo)) return Json(new { success = false, message = "ไม่พบข้อมูลใบเสนอราคา" });

                // ตรวจสอบว่ามีข้อมูลตัวแทนหรือไม่
                var agentDetail = await FetchAgentDetail(quotationDetail.UserId ?? "");
                if (string.IsNullOrEmpty(agentDetail.AgentCode)) return Json(new { success = false, message = "ไม่พบข้อมูลตัวแทน" });

                var coverType = _utilityHelper.ConvertPolicyType(quotationDetail.CoverageCode ?? ""); // แปลงประเภทกรมธรรม์
                var policyType = _utilityHelper.GetDescription(PolicyType.type1); // รับคำอธิบายของประเภทกรมธรรม์
                var isCustomerType = collection["customerTypeRadio"].ToString() == "N"; // ตรวจสอบประเภทลูกค้า (บุคคลธรรมดาหรือไม่)
                var insureBirthDate = isCustomerType ? collection["birthDateCustomerInput"].ToString() : collection["registerDateCustomerInput"]!.ToString(); // กำหนดวันเกิดหรือวันที่จดทะเบียนตามประเภทลูกค้า
                var isCompulsory = collection["isCompulsoryRadio"].ToString() == "Y"; // ตรวจสอบว่ามีประกันภัยภาคบังคับหรือไม่
                var voluntaryTotalPremiums = double.TryParse(quotationDetail.TotalPremiums, out var vmiTotalPremium) ? vmiTotalPremium : 0; // จำนวนเบี้ยประกันภัยภาคสมัครใจ
                var compulsoryNetPremiums = double.TryParse(quotationDetail.CMITotalPremiums, out var cmiNetPremium) ? cmiNetPremium : 0; // จำนวนเบี้ยประกันภัยภาคบังคับ (Net Premiums)
                var compulsoryTotalPremiums = double.TryParse(quotationDetail.TotalPremiumsWithCMI, out var cmiTotalPremium) ? cmiTotalPremium : 0; // จำนวนเบี้ยประกันภัยภาคบังคับ (Total Premiums)

                // สร้างคำขอสำหรับการส่งข้อมูล Motor
                var submitMotorRequest = new MotorSubmitRequest
                {
                    AgentCode = agentDetail.AgentCode, // รหัสตัวแทน
                    BeneficName = WebUtility.HtmlEncode(collection["beneficiaryNameInput"].ToString()), // ชื่อผู้รับผลประโยชน์
                    CarBodyType = quotationDetail.AutoMobileType, // ประเภทรถยนต์ (เก๋ง , กระบะ 2 ประตู , กระบะ 4 ประตู , ตู้)
                    CarBrandCode = collection["carBrandSelect"].ToString(), // รหัสยี่ห้อรถยนต์
                    CarCc = collection["vehicleSizeInput"].ToString(), // ปริมาตรกระบอกสูบ
                    CarChassis = collection["chassisNumberInput"].ToString(), // หมายเลขตัวถังรถยนต์
                    CarCode = collection["vehicleCodeSelect"].ToString(), // รหัสรถยนต์
                    CarColorCode = collection["carColorSelect"].ToString(), // รหัสสีรถยนต์
                    CarEngineNo = collection["machineNumberInput"].ToString(), // หมายเลขเครื่องยนต์
                    CarModelCode = collection["carModelSelect"].ToString(), // รหัสรุ่นรถยนต์
                    CarProvinceCode = collection["registrationCarProvinceSelect"].ToString(), // รหัสจังหวัด
                    CarRegisNo = collection["registrationCarInput"].ToString(), // หมายเลขทะเบียนรถ
                    CarRegisNoPre = collection["registrationPreCarInput"].ToString(), // หมายเลขทะเบียนรถ (ก่อนหน้า)
                    CarRegistration = collection["registrationCarYearInput"].ToString(), // วันที่จดทะเบียน
                    CarSeat = collection["carSeat"].ToString(), // จำนวนที่นั่ง
                    CarYear = collection["registrationCarYearInput"].ToString(), // ปีที่ผลิต
                    CompulsoryEndDate = DateTime.TryParse(collection["compulsoryStartDateInput"], culture, out var cmiEndDate) ? cmiEndDate.AddYears(1).ToString("dd/MM/yyyy") : null, // วันที่สิ้นสุดประกันภัยภาคบังคับ
                    CompulsoryStartDate = DateTime.TryParse(collection["compulsoryStartDateInput"], culture, out var cmiStartDate) ? cmiStartDate.ToString("dd/MM/yyyy") : null, // วันที่เริ่มต้นประกันภัยภาคบังคับ
                    CompulsoryNetPremiumAmount = isCompulsory ? compulsoryNetPremiums : 0, // จำนวนเบี้ยประกันภัยภาคบังคับ
                    CompulsoryTotalPremiumAmount = isCompulsory ? compulsoryTotalPremiums : voluntaryTotalPremiums, // จำนวนเบี้ยประกันภัยภาคบังคับ
                    CoverageCarDamage = int.TryParse("0", out var coverCarDamage) ? coverCarDamage : 0, // จำนวนความคุ้มครองความเสียหายรถยนต์
                    CoverageCarLoss = int.TryParse(quotationDetail.FT, out var coverCarLoss) ? coverCarLoss : 0, // จำนวนความคุ้มครองการสูญเสียรถยนต์
                    CoverageFloodAmount = int.TryParse("0", out var coverFlood) ? coverFlood : 0, // จำนวนความคุ้มครองน้ำท่วม
                    CoverageMoneyCost = int.TryParse(quotationDetail.SumInsure, out var coverMoneyCost) ? coverMoneyCost : 0, // จำนวนความคุ้มครองเงินสด
                    CustomerTypeCardCode = collection["customerTypeCardRadio"].ToString(), // รหัสประเภทบัตรลูกค้า (01 = บัตรประจำตัวประชาชน , 02 = บัตรข้าราชการ , 03 = Passport)
                    EmailAddress = agentDetail.EmailAddress ?? _appSetting.EmailBase, // ที่อยู่อีเมล
                    EvKiloWattAmount = double.TryParse("0", out var evKiloWatt) ? evKiloWatt : 0, // จำนวนกิโลวัตต์สำหรับรถยนต์ไฟฟ้า
                    FlagPerson = collection["customerTypeRadio"].ToString(), // ประเภทลูกค้า (N = บุคคล , Y = นิติบุคคล)
                    FlagPoweredCar = "PT", // รถยนต์ที่ใช้พลังงาน (PT = น้ำมัน , EV = ไฟฟ้า)
                    FlagPrintPolicy = collection["sendPolicyRadio"].ToString(), // พิมพ์กรมธรรม์ (E = E-Policy , P = Paper)
                    FlagRedPlate = "", // แผ่นป้ายทะเบียนสีแดง (Y = ป้ายแดง , "" = ไม่ใช่ป้ายแดง)
                    FlagRenewalCompulsory = isCompulsory ? "N" : "", // การต่ออายุประกันภัยภาคบังคับ (N = งานใหม่ , R = งานต่ออายุ)
                    FlagRenewalVoluntary = "N", // การต่ออายุประกันภัยภาคสมัครใจ (N = งานใหม่ , R = งานต่ออายุ)
                    FlagRepair = collection["carRepairTypeRadio"].ToString(), // การซ่อมแซม (S = ซ่อมห้าง , G = ซ่อมอู่)
                    FlagSendDocument = collection["sendByAddressRadio"].ToString(), // การส่งเอกสาร (A = ตามที่อยู่นายหน้า , I = ตามที่อยู่ผู้เอาประกันภัย)
                    Gender = collection["customerGender"].ToString(), // เพศ
                    InsureAddress = WebUtility.HtmlEncode(collection["customerAddressNoInput"].ToString()), // ที่อยู่ของผู้เอาประกันภัย
                    InsureBirthDate = DateTime.TryParse(insureBirthDate, culture, out var birthDate) ? birthDate.ToString("dd/MM/yyyy") : null, // วันเกิดของผู้เอาประกันภัย
                    InsureCompanyCode = quotationDetail.CompanyCode, // รหัสบริษัทผู้เอาประกันภัย
                    InsureDistrictCode = collection["customerAddressDistrictSelect"].ToString(), // รหัสเขตของผู้เอาประกันภัย
                    InsureEmailAddress = agentDetail.EmailAddress ?? _appSetting.EmailBase, // ที่อยู่อีเมลของผู้เอาประกันภัย
                    InsureIDCard = collection["citizenIdInput"].ToString(), // หมายเลขบัตรประชาชนของผู้เอาประกันภัย
                    InsureName = isCustomerType ? WebUtility.HtmlEncode(collection["firstNameInput"].ToString()) : WebUtility.HtmlEncode(collection["corporationNameInput"].ToString()), // ชื่อผู้เอาประกันภัย
                    InsureProvinceCode = collection["customerAddressProvinceSelect"].ToString(), // รหัสจังหวัดของผู้เอาประกันภัย
                    InsureRelationPhoneNumber = "", // หมายเลขโทรศัพท์ของผู้เอาประกันภัย (ไม่ใช้ในกรณีนี้)
                    InsureRelationShips = "", // ความสัมพันธ์ของผู้เอาประกันภัย (ไม่ใช้ในกรณีนี้)
                    InsureSubDistrictCode = collection["customerAddressSubDistrictSelect"].ToString(), // รหัสตำบลของผู้เอาประกันภัย
                    InsureSurName = isCustomerType ? WebUtility.HtmlEncode(collection["lastNameInput"].ToString()) : null, // นามสกุลผู้เอาประกันภัย
                    InsureZipCode = collection["customerAddressZipCodeInput"].ToString(), // รหัสไปรษณีย์ของผู้เอาประกันภัย
                    NumberInstall = int.TryParse(collection["installmentPeriodSelect"].ToString(), out var numberInstall) ? numberInstall : 0, // จำนวนงวดผ่อนชำระ
                    OccupationCode = collection["customerOccupationSelect"].ToString(), // รหัสอาชีพ
                    OldPolicyCompulsory = "", // ประกันภัยภาคบังคับเก่า
                    OldPolicyVoluntary = "", // ประกันภัยภาคสมัครใจเก่า
                    PhoneNumber = collection["mobileCustomerInput"].ToString(), // หมายเลขโทรศัพท์
                    PolicyType = coverType, // ประเภทกรมธรรม์
                    PrenameCode = collection["preNameTitleSelect"].ToString(), // รหัสคำนำหน้าชื่อ
                    QuotationNumber = quotationDetail.QuotationNo, // หมายเลขใบเสนอราคา
                    Remarks = WebUtility.HtmlEncode(collection["inspectionRemarkHidden"].ToString()), // หมายเหตุ
                    SendDocumentAddressNo = WebUtility.HtmlEncode(collection["sendAddressNoInput"].ToString()), // ที่อยู่สำหรับส่งเอกสาร
                    SendDocumentDistrictCode = collection["sendAddressDistrictSelect"].ToString(), // รหัสเขตสำหรับส่งเอกสาร
                    SendDocumentProvinceCode = collection["sendAddressProvinceSelect"].ToString(), // รหัสจังหวัดสำหรับส่งเอกสาร
                    SendDocumentSubDistrictCode = collection["sendAddressSubDistrictSelect"].ToString(), // รหัสตำบลสำหรับส่งเอกสาร
                    SendDocumentZipCode = collection["sendAddressZipCodeInput"].ToString(), // รหัสไปรษณีย์สำหรับส่งเอกสาร
                    StatusCode = "Q", // รหัสสถานะ (Q = ส่งงาน , H = Hold , W = ติดเครดิต)
                    SubInsureTypeCompulsory = collection["subInsureTypeCompulsory"].ToString(), // ประเภทประกันภัยภาคบังคับ
                    SubInsureTypeVoluntary = quotationDetail.OriginalProductCode, // ประเภทประกันภัยภาคสมัครใจ
                    VoluntaryEndDate = DateTime.TryParse(collection["voluntaryStartDateInput"], culture, out var vmiEndDate) ? vmiEndDate.AddYears(1).ToString("dd/MM/yyyy") : null, // วันที่สิ้นสุดประกันภัยภาคสมัครใจ
                    VoluntaryStartDate = DateTime.TryParse(collection["voluntaryStartDateInput"], culture, out var vmiStartDate) ? vmiStartDate.ToString("dd/MM/yyyy") : null, // วันที่เริ่มต้นประกันภัยภาคสมัครใจ
                    VoluntaryNetPremiumAmount = int.TryParse(quotationDetail.NetPremiums, out var vmiNetPremium) ? vmiNetPremium : 0, // จำนวนเบี้ยประกันภัยภาคสมัครใจ
                    VoluntaryTotalPremiumAmount = voluntaryTotalPremiums, // จำนวนเบี้ยประกันภัยภาคสมัครใจ
                    WithCompulsory = coverType == policyType ? collection["isCompulsoryRadio"].ToString() : "", // ประกันภัยภาคบังคับ (N = ไม่มี พรบ. , Y = มีพรบ.)
                };

                // กำหนดช่องทางการชำระเงินและอ้างอิงตามเงื่อนไขของตัวแทน
                if (agentDetail.FCredit == "Y")
                {
                    submitMotorRequest.ChannelPayment = "OT"; // ถ้าตัวแทนมีเครดิต ให้ใช้ช่องทางการชำระเงินเป็น OT (Other)
                    submitMotorRequest.ChannelReference1 = "-"; // ช่องทางการชำระเงิน (อ้างอิง 1) สำหรับตัวแทนที่มีเครดิต
                    submitMotorRequest.ChannelReference2 = "-"; // ช่องทางการชำระเงิน (อ้างอิง 2) สำหรับตัวแทนที่มีเครดิต
                }
                else
                {
                    var ref1 = submitMotorRequest.InsureIDCard;
                    var ref1_2 = ref1!.Substring(ref1.Length - 4, 4); // รับ 4 หลักสุดท้ายของหมายเลขบัตรประชาชน
                    var ref2 = submitMotorRequest.CarRegisNo!.PadLeft(4, '0');
                    var ref2_2 = ref2.Substring(ref2.Length - 4, 4); // รับ 4 หลักสุดท้ายของหมายเลขทะเบียนรถยนต์

                    submitMotorRequest.ChannelPayment = collection["channelPaymentRadio"].ToString() == "PO" ? collection["channelPaymentOnlineRadio"].ToString() : collection["channelInstallmentRadio"].ToString(); // ใช้ช่องทางการชำระเงินที่เลือกในฟอร์ม
                    submitMotorRequest.ChannelReference1 = $"{DateTime.Now.ToString("yyyyMMddHHmm", culture)}{ref1_2}{ref2_2}"; // ช่องทางการชำระเงิน (อ้างอิง 1)
                    submitMotorRequest.ChannelReference2 = collection["mobileCustomerInput"].ToString(); // ช่องทางการชำระเงิน (อ้างอิง 2)
                }

                if (collection["channelPaymentRadio"].ToString() == "IN")
                {
                    // ถ้าเลือกช่องทางการชำระเงินเป็นผ่อนชำระ
                    var installmentCalculatePeriod = await FetchInstallmentCalculatePeriod(submitMotorRequest);
                    if (installmentCalculatePeriod == null || !installmentCalculatePeriod.Any()) return Json(new { success = false, message = "ไม่พบข้อมูลการคำนวณงวดผ่อนชำระ" });

                    // สร้างคำขอสำหรับการส่งข้อมูล Motor
                    var mappingFieldSubmitMotorDraft = MappingFieldSubmitMotorDraft(submitMotorRequest); // แปลงข้อมูลคำขอเป็นรูปแบบที่ต้องการสำหรับแจ้งเตือนระบบหลัก
                    var resultPaymentInstallment = await CreatePaymentInstallment(installmentCalculatePeriod, collection, mappingFieldSubmitMotorDraft.ContractNumber!); // สร้างข้อมูลการผ่อนชำระ
                    if (string.IsNullOrEmpty(resultPaymentInstallment.TransactionId)) return Json(new { success = false, message = "ไม่สามารถสร้างข้อมูลการผ่อนชำระได้" });

                    var isSuccessNotifyCoreSystemDraft = await _motorUseCase.CreateNotifyCoreSystemDraftAsync(mappingFieldSubmitMotorDraft); // บันทึกข้อมูลแจ้งเตือนระบบหลัก
                    if (!isSuccessNotifyCoreSystemDraft.Data) return Json(new { success = false, message = "ไม่สามารถบันทึกข้อมูลแจ้งเตือนระบบหลักได้" });

                    var resultInstallContract = await CreateDataInstallmentContract(collection, submitMotorRequest, mappingFieldSubmitMotorDraft.ContractNumber!, quotationDetail, installmentCalculatePeriod); // สร้างข้อมูลสัญญาผ่อนชำระ
                    if (string.IsNullOrEmpty(resultInstallContract.ContractNo)) return Json(new { success = false, message = "ไม่สามารถสร้างข้อมูลสัญญาผ่อนชำระได้" });

                    // ส่งอีเมลยืนยันบัตรประชาชน
                    var isSendEmail = await SendVerifyCardIdEmail(resultInstallContract.Email!, mappingFieldSubmitMotorDraft.TransactionId!);
                    if (!isSendEmail) return Json(new { success = false, message = "ไม่สามารถส่งอีเมลยืนยันบัตรประชาชนได้" });

                    return Json(new { success = true, message = "บันทึกข้อมูลสำเร็จ กรุณาตรวจสอบอีเมลเพื่อยืนยัน", channel = Url.Action("Index", "SubmitMotorNew") });
                }
                else
                {
                    var submitMotorResponse = await CreateSubmitMotor(submitMotorRequest);

                    if (string.IsNullOrEmpty(submitMotorResponse.AppNoCMI) && string.IsNullOrEmpty(submitMotorResponse.AppNoVMI))
                        return Json(new { success = false, message = $"พบปัญหา{submitMotorResponse.ErrorMessage} ไม่สามารถบันทึกข้อมูลข้อมูล Motor ได้" });

                    var mappingFieldSubmitMotor = MappingFieldSubmitMotor(submitMotorRequest, submitMotorResponse, collection); // แปลงข้อมูลคำขอเป็นรูปแบบที่ต้องการสำหรับแจ้งเตือนระบบหลัก
                    var isSuccessNotifyCoreSystem = await _motorUseCase.CreateNotifyCoreSystemAsync(mappingFieldSubmitMotor); // บันทึกข้อมูลแจ้งเตือนระบบหลัก
                    if (!isSuccessNotifyCoreSystem.Data) return Json(new { success = false, message = "ไม่สามารถบันทึกข้อมูลแจ้งเตือนระบบหลักได้" });

                    var mappingFieldCoverNote = MappingFieldCoverNote(collection, submitMotorRequest, submitMotorResponse, quotationDetail); // แปลงข้อมูลสำหรับพิมพ์ใบเสร็จรับเงิน/ใบกำ
                    var isSuccessCoverNote = await _motorUseCase.CreateNotifyCoverNoteAsync(mappingFieldCoverNote); // บันทึกข้อมูลสำหรับพิมพ์ใบเสร็จรับเงิน/ใบกำกับภาษี
                    if (!isSuccessCoverNote.Data) return Json(new { success = false, message = "ไม่สามารถออกเอกสารหลักฐานการแจ้งงาน" });

                    TempData["transaction_id"] = mappingFieldSubmitMotor.TransactionId;

                    if (collection["channelPaymentOnlineRadio"].Equals("QR"))
                        return Json(new { success = true, message = "ยืนยันการแจ้งงาน", channel = Url.Action("PaymentQRCode", "PaymentKBank") });

                    if (collection["channelPaymentOnlineRadio"].Equals("PO"))
                        return Json(new { success = true, message = $"ระบบจะส่ง SMS ไปที่เบอร์ {collection["mobileCustomerInput"]} เพื่อชำระเงิน", channel = Url.Action("PaymentCreditCard", "PaymentT2P") });

                    return Json(new { success = true, message = "บันทึกข้อมูลสำเร็จ" });
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred");
                return Json(new { success = false, message = "เกิดข้อผิดพลาดในการบันทึกข้อมูล" });
            }
        }

        private WebNotifyCoreSystem MappingFieldSubmitMotor(MotorSubmitRequest request, MotorSubmitResponse motorSubmitResponse, IFormCollection collection)
        {
            try
            {
                return new WebNotifyCoreSystem()
                {
                    AgentCode = request.AgentCode,
                    ApplicationNoCompulsory = motorSubmitResponse.AppNoCMI,
                    ApplicationNoVoluntary = motorSubmitResponse.AppNoVMI,
                    BeneficName = request.BeneficName,
                    CarBodyType = request.CarBodyType,
                    CarBrandCode = request.CarBrandCode,
                    CarCc = request.CarCc,
                    CarChassis = request.CarChassis,
                    CarCode = request.CarCode,
                    CarColorCode = request.CarColorCode,
                    CarEngineNo = request.CarEngineNo,
                    CarModelCode = request.CarModelCode,
                    CarProvinceCode = request.CarProvinceCode,
                    CarRegistration = request.CarRegistration,
                    CarRegno = request.CarRegisNo,
                    CarRegnoPre = request.CarRegisNoPre,
                    CarSeat = int.TryParse(request.CarSeat, out var seat) ? seat : 0,
                    CarWeight = 0,
                    CarYear = request.CarYear,
                    ChannelPayment = request.ChannelPayment,
                    ChannelReference1 = request.ChannelReference1,
                    ChannelReference2 = request.ChannelReference2,
                    CompulsoryEndDate = Convert.ToDateTime(request.CompulsoryEndDate, culture),
                    CompulsoryNetPremiumAmount = (int)request.CompulsoryNetPremiumAmount,
                    CompulsoryStartDate = Convert.ToDateTime(request.CompulsoryStartDate, culture),
                    CompulsoryTotalPremiumAmount = (decimal)request.CompulsoryTotalPremiumAmount,
                    ContractNumber = null,
                    CoverageCarDamage = request.CoverageCarDamage,
                    CoverageCarLoss = request.CoverageCarLoss,
                    CoverageFloodAmount = request.CoverageFloodAmount,
                    CoverageMoneyCost = request.CoverageMoneyCost,
                    DateCreate = DateTime.Now,
                    EmailAddress = request.EmailAddress,
                    EvBattDate = null,
                    EvBattSerial = null,
                    EvKwAmount = null,
                    EvWallChargerSerial = null,
                    FlagCommission = (request.ChannelPayment == "OT" || request.ChannelPayment == "IN") ? collection["patternPaymentRadio"] : "N",
                    FlagPerson = request.FlagPerson,
                    FlagPrintPolicy = request.FlagPrintPolicy,
                    FlagRedPlate = request.FlagRedPlate,
                    FlagRenewalVoluntary = request.FlagRenewalVoluntary,
                    FlagRepair = request.FlagRepair,
                    FlagPoweredCar = request.FlagRenewalVoluntary,
                    FlagRenewalCompulsory = request.FlagRenewalCompulsory,
                    FlagSendDocument = request.FlagSendDocument,
                    Gender = request.Gender,
                    InsureAddress = request.InsureAddress,
                    InsureBirthdate = Convert.ToDateTime(request.InsureBirthDate, culture),
                    InsureCompanyCode = request.InsureCompanyCode,
                    InsureDistrictCode = request.InsureDistrictCode,
                    InsureEmailAddress = request.InsureEmailAddress,
                    InsureIdCard = request.InsureIDCard,
                    InsureName = request.InsureName,
                    InsureProvinceCode = request.InsureProvinceCode,
                    InsureSubDistrictCode = request.InsureSubDistrictCode,
                    InsureSurname = request.InsureSurName,
                    InsureZipCode = request.InsureZipCode,
                    OccupationCode = request.OccupationCode,
                    OldPolicyCompulsory = request.OldPolicyCompulsory,
                    OldPolicyVoluntary = request.OldPolicyVoluntary,
                    PhoneNumber = request.PhoneNumber,
                    PolicyType = request.PolicyType,
                    PrenameCode = request.PrenameCode,
                    QuotationNumber = request.QuotationNumber,
                    Remarks = request.Remarks,
                    SendDocumentAddressNo = request.SendDocumentAddressNo,
                    SendDocumentDistrictCode = request.SendDocumentDistrictCode,
                    SendDocumentProvinceCode = request.SendDocumentProvinceCode,
                    SendDocumentSubdistrictCode = request.SendDocumentSubDistrictCode,
                    SendDocumentZipCode = request.SendDocumentZipCode,
                    StatusCode = request.StatusCode,
                    SubInsureTypeCMI = request.SubInsureTypeCompulsory,
                    SubInsureTypeVMI = request.SubInsureTypeVoluntary,
                    TransactionId = transactionId,
                    VoluntaryEndDate = Convert.ToDateTime(request.VoluntaryEndDate, culture),
                    VoluntaryNetPremiumAmount = request.VoluntaryNetPremiumAmount,
                    VoluntaryStartDate = Convert.ToDateTime(request.VoluntaryStartDate, culture),
                    VoluntaryTotalPremiumAmount = (decimal)request.VoluntaryTotalPremiumAmount,
                    WithCompulsory = request.WithCompulsory,
                };
            }
            catch (Exception ex)
            {
                // Log the exception if necessary
                _logger.LogError(ex, "Error mapping MotorSubmitRequest to WebNotifyCoreSystem");
                return new WebNotifyCoreSystem();
            }
        }

        private WebNotifyCoreSystemDraft MappingFieldSubmitMotorDraft(MotorSubmitRequest request)
        {
            try
            {
                var dateNow = DateTime.Now; // กำหนดวันที่และเวลาปัจจุบัน
                var contactNumber = $"889{dateNow:yyMMddHHmmss}"; // สร้างหมายเลขติดต่อที่ไม่ซ้ำกัน

                var response = new WebNotifyCoreSystemDraft()
                {
                    AgentCode = request.AgentCode,
                    ApplicationNoCompulsory = null,
                    ApplicationNoVoluntary = null,
                    BeneficName = request.BeneficName,
                    CarBodyType = request.CarBodyType,
                    CarBrandCode = request.CarBrandCode,
                    CarCc = request.CarCc,
                    CarChassis = request.CarChassis,
                    CarCode = request.CarCode,
                    CarColorCode = request.CarColorCode,
                    CarEngineNo = request.CarEngineNo,
                    CarModelCode = request.CarModelCode,
                    CarProvinceCode = request.CarProvinceCode,
                    CarRegistration = request.CarRegistration,
                    CarRegno = request.CarRegisNo,
                    CarRegnoPre = request.CarRegisNoPre,
                    CarSeat = int.TryParse(request.CarSeat, out var seat) ? seat : 0,
                    CarWeight = 0,
                    CarYear = request.CarYear,
                    ChannelPayment = request.ChannelPayment,
                    ChannelReference1 = request.ChannelReference1,
                    ChannelReference2 = request.ChannelReference2,
                    CompulsoryEndDate = Convert.ToDateTime(request.CompulsoryEndDate, culture),
                    CompulsoryNetPremiumAmount = (int)request.CompulsoryNetPremiumAmount,
                    CompulsoryStartDate = Convert.ToDateTime(request.CompulsoryStartDate, culture),
                    CompulsoryTotalPremiumAmount = (decimal)request.CompulsoryTotalPremiumAmount,
                    ContractNumber = contactNumber,
                    CoverageCarDamage = request.CoverageCarDamage,
                    CoverageCarLoss = request.CoverageCarLoss,
                    CoverageFloodAmount = request.CoverageFloodAmount,
                    CoverageMoneyCost = request.CoverageMoneyCost,
                    DateCreate = DateTime.Now,
                    EmailAddress = request.EmailAddress,
                    EvBattDate = null,
                    EvBattSerial = null,
                    EvKwAmount = null,
                    EvWallChargerSerial = null,
                    FlagCommission = "N",
                    FlagPerson = request.FlagPerson,
                    FlagPrintPolicy = request.FlagPrintPolicy,
                    FlagRedPlate = request.FlagRedPlate,
                    FlagRenewalVoluntary = request.FlagRenewalVoluntary,
                    FlagRepair = request.FlagRepair,
                    FlagPoweredCar = request.FlagRenewalVoluntary,
                    FlagRenewalCompulsory = request.FlagRenewalCompulsory,
                    FlagSendDocument = request.FlagSendDocument,
                    Gender = request.Gender,
                    InsureAddress = request.InsureAddress,
                    InsureBirthdate = Convert.ToDateTime(request.InsureBirthDate, culture),
                    InsureCompanyCode = request.InsureCompanyCode,
                    InsureDistrictCode = request.InsureDistrictCode,
                    InsureEmailAddress = request.InsureEmailAddress,
                    InsureIdCard = request.InsureIDCard,
                    InsureName = request.InsureName,
                    InsureProvinceCode = request.InsureProvinceCode,
                    InsureSubDistrictCode = request.InsureSubDistrictCode,
                    InsureSurname = request.InsureSurName,
                    InsureZipCode = request.InsureZipCode,
                    OccupationCode = request.OccupationCode,
                    OldPolicyCompulsory = request.OldPolicyCompulsory,
                    OldPolicyVoluntary = request.OldPolicyVoluntary,
                    PhoneNumber = request.PhoneNumber,
                    PolicyType = request.PolicyType,
                    PrenameCode = request.PrenameCode,
                    QuotationNumber = request.QuotationNumber,
                    Remarks = request.Remarks,
                    SendDocumentAddressNo = request.SendDocumentAddressNo,
                    SendDocumentDistrictCode = request.SendDocumentDistrictCode,
                    SendDocumentProvinceCode = request.SendDocumentProvinceCode,
                    SendDocumentSubdistrictCode = request.SendDocumentSubDistrictCode,
                    SendDocumentZipCode = request.SendDocumentZipCode,
                    StatusCode = request.StatusCode,
                    SubInsureTypeCompulsory = request.SubInsureTypeCompulsory,
                    SubInsureTypeVoluntary = request.SubInsureTypeVoluntary,
                    TransactionId = transactionId,
                    VoluntaryEndDate = Convert.ToDateTime(request.VoluntaryEndDate, culture),
                    VoluntaryNetPremiumAmount = request.VoluntaryNetPremiumAmount,
                    VoluntaryStartDate = Convert.ToDateTime(request.VoluntaryStartDate, culture),
                    VoluntaryTotalPremiumAmount = (decimal)request.VoluntaryTotalPremiumAmount,
                    WithCompulsory = request.WithCompulsory,
                };

                return response;
            }
            catch (Exception ex)
            {
                // Log the exception if necessary
                _logger.LogError(ex, "Error mapping MotorSubmitRequest to WebNotifyCoreSystemDraft");
                return new WebNotifyCoreSystemDraft();
            }
        }

        private WebNotifyCoverNote MappingFieldCoverNote(IFormCollection collection, MotorSubmitRequest request, MotorSubmitResponse response, QuotationDetailResponse quotationDetailResponse)
        {
            try
            {
                return new WebNotifyCoverNote()
                {
                    AgentName = "",
                    ApplicationNoCompulsory = response.AppNoCMI,
                    ApplicationNoVoluntary = response.AppNoVMI,
                    EmailAddress = request.InsureEmailAddress,
                    CarBrand = collection["CarBrand"],
                    CarModel = collection["CarModel"],
                    CarRepair = request.FlagRepair == "S" ? "ซ่อมห้าง" : "ซ่อมอู่",
                    CompanyName = request.InsureCompanyCode,
                    CompulsoryCoverageTotal = 0,
                    CompulsoryEndDate = Convert.ToDateTime(request.CompulsoryEndDate, culture),
                    CompulsoryPremiumsTotal = (decimal)request.CompulsoryNetPremiumAmount,
                    CompulsoryStartDate = Convert.ToDateTime(request.CompulsoryStartDate, culture),
                    DateCreate = DateTime.Now,
                    FilePath = null,
                    ImagePath = null,
                    InsureName = $"{collection["preNameTitle"]} {request.InsureName} {request.InsureSurName}",
                    Password = request.InsureIDCard!.Substring((request.InsureIDCard.Length - 4), 4).PadLeft(4, '0'),
                    PolicyType = quotationDetailResponse.CoverageName,
                    QuotationNo = request.QuotationNumber,
                    TransactionId = transactionId,
                    Vehicle = $"{request.CarRegisNoPre} {request.CarRegisNo} {collection["CarProvince"]}",
                    VoluntaryCoverageTotal = request.CoverageMoneyCost,
                    VoluntaryEndDate = Convert.ToDateTime(request.VoluntaryEndDate, culture),
                    VoluntaryPremiumsTotal = request.VoluntaryNetPremiumAmount,
                    VoluntaryStartDate = Convert.ToDateTime(request.VoluntaryStartDate, culture),
                };
            }
            catch (Exception ex)
            {
                // Log the exception if necessary
                _logger.LogError(ex, "Error mapping MotorSubmitRequest to WebNotifyCoverNote");
                return new WebNotifyCoverNote();
            }
        }

        private async Task<WebNotifyInstallmentContract> CreateDataInstallmentContract(IFormCollection collection, MotorSubmitRequest request, string ContractNumber, QuotationDetailResponse quotationDetailResponse, IEnumerable<InstallmentCalculatePeriodResponse> calculatePeriodResponses)
        {
            try
            {
                var NextPeriodAmount = calculatePeriodResponses.Where(x => x.InstallmentNo == 2).Select(x => x.PremiumAmount + x.TaxAmount).FirstOrDefault();

                var result = new WebNotifyInstallmentContract
                {
                    Address = collection["Address"].ToString(),
                    InstallmentAmount = (decimal)NextPeriodAmount,
                    PremiumAmount = (decimal)request.VoluntaryTotalPremiumAmount,
                    CarRegistration = $"{request.CarRegisNoPre} {request.CarRegisNo} {collection["CarProvince"]}",
                    ContractNo = ContractNumber,
                    CoverageEndDate = DateTime.TryParse(request.VoluntaryEndDate, culture, out var vmiEndDate) ? vmiEndDate : null,
                    CoverageStartDate = DateTime.TryParse(request.VoluntaryStartDate, culture, out var vmiStartDate) ? vmiStartDate : null,
                    DateCreate = DateTime.Now,
                    Email = request.InsureEmailAddress,
                    FullName = $"{collection["preNameTitle"]} {request.InsureName} {request.InsureSurName}",
                    IdCardNumber = request.InsureIDCard,
                    TransactionId = transactionId,
                    InstallmentCount = request.NumberInstall,
                    InsuranceCompany = quotationDetailResponse.CompanyName,
                    InsuranceType = quotationDetailResponse.CoverageName,
                    InsuredPhone = request.PhoneNumber,
                    ProductName = quotationDetailResponse.TitleMasterPlan,
                    ReferencePhone = null,
                    Relationship = null,
                };

                var isSuccess = await _paymentInstallmentUseCase.CreateNotifyInstallmentContract(result);
                return isSuccess.Data ? result : new WebNotifyInstallmentContract();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while creating installment contract data");
                return new WebNotifyInstallmentContract();
            }
        }

        private async Task<WebPaymentInstallment> CreatePaymentInstallment(IEnumerable<InstallmentCalculatePeriodResponse> response, IFormCollection collection, string contract)
        {
            try
            {
                var FirstPeriodAmount = response.Where(x => x.InstallmentNo == 1).Select(x => x.PremiumAmount + x.TaxAmount).FirstOrDefault();
                var NextPeriodAmount = response.Where(x => x.InstallmentNo == 2).Select(x => x.PremiumAmount + x.TaxAmount).FirstOrDefault();

                var installmentResult = new WebPaymentInstallment()
                {
                    ContractNumber = contract,
                    DateCreate = DateTime.Now,
                    Email = collection["emailVerifyInput"].ToString(),
                    FirstPeriodAmount = (decimal)FirstPeriodAmount,
                    Mobile = collection["mobileCustomerInput"].ToString(),
                    NextPeriodAmount = (decimal)NextPeriodAmount,
                    PeriodTotal = int.TryParse(collection["installmentPeriodSelect"].ToString(), out var numberInstall) ? numberInstall : 0,
                    TransactionId = transactionId,
                    DateExpire = DateTime.Now.Date.AddDays(1).AddSeconds(-1),
                };

                var isSuccess = await _paymentInstallmentUseCase.CreatePaymentInstallment(installmentResult);
                return isSuccess.Data ? installmentResult : new WebPaymentInstallment();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while creating payment installment");
                return new WebPaymentInstallment();
            }
        }

        private async Task<MotorSubmitResponse> CreateSubmitMotor(MotorSubmitRequest request)
        {
            try
            {
                var submitMotor = await _submitMotorUseCase.CreateSubmitMotor(request); // บันทึกข้อมูลการส่งข้อมูล Motor
                if (submitMotor.Data == null)
                {
                    _logger.LogError("Motor submit response data is null");
                    return new MotorSubmitResponse(); // คืนค่าเริ่มต้นหากไม่มีข้อมูล
                }
                return submitMotor.Data; // คืนค่าข้อมูลการส่งข้อมูล Motor
            }
            catch (Exception ex)
            {
                // Log the exception if necessary
                _logger.LogError(ex, "An error occurred while creating submit motor");
                return new MotorSubmitResponse();
            }
        }

        private async Task<AgentDetailResponse> FetchAgentDetail(string agentCode)
        {
            var agentDetailsResponse = await _agentUseCase.FetchAgentDetail(new AgentDetailRequest { AgentCode = agentCode });
            agentDetailsResponse.Data = agentDetailsResponse.Data ?? []; // ตรวจสอบว่า Data ไม่เป็น null
            return agentDetailsResponse.Data.FirstOrDefault() ?? new AgentDetailResponse(); // กำหนดค่าเริ่มต้นหากไม่มีข้อมูล
        }

        private async Task<IEnumerable<InstallmentCalculatePeriodResponse>> FetchInstallmentCalculatePeriod(MotorSubmitRequest request)
        {
            try
            {
                var installmentCalculatePeriod = await _installmentUseCase.FetchCalculatePeriod(new InstallmentCalculatePeriodRequest()
                {
                    CompulsoryTotalPremium = request.CompulsoryNetPremiumAmount,
                    FlagPerson = request.FlagPerson!,
                    NumberOfInstallments = request.NumberInstall,
                    VoluntaryNetPremium = request.VoluntaryNetPremiumAmount,
                    VoluntaryTotalPremium = request.VoluntaryTotalPremiumAmount
                });
                installmentCalculatePeriod.Data = installmentCalculatePeriod.Data ?? []; // ตรวจสอบว่า Data ไม่เป็น null
                return installmentCalculatePeriod.Data; // คืนค่าข้อมูลการคำนวณงวดผ่อนชำระ
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while fetching installment calculate period");
                return [];
            }
        }

        private async Task<QuotationDetailResponse> FetchQuotationReportDetail(IFormCollection collection)
        {
            try
            {
                var quotationResponse = await _reportUseCase.FetchQuotationReportDetail(new QuotationDetailRequest()
                {
                    CompanyCode = collection["companyCodeHidden"].ToString(),
                    QuotationNo = collection["quotationNoHidden"].ToString(),
                    Id = int.TryParse(collection["quotationIdHidden"], out var id) ? id : (int?)null,
                    PremiumsID = int.TryParse(collection["premiumIdHidden"], out var premiumsId) ? premiumsId : (int?)null,
                    ProductCode = collection["productCodeHidden"].ToString()
                });
                quotationResponse.Data = quotationResponse.Data ?? []; // ตรวจสอบว่า Data ไม่เป็น null
                return quotationResponse.Data.FirstOrDefault() ?? new QuotationDetailResponse(); // กำหนดค่าเริ่มต้นหากไม่มีข้อมูล
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while fetching quotation report detail");
                return new QuotationDetailResponse();
            }
        }

        private async Task<bool> IsCheckValidInputFileAsync(IFormCollection collection)
        {
            // ตรวจสอบว่ามีข้อมูลใน form หรือไม่
            if (!ModelState.IsValid) return false;

            // ตรวจสอบว่ามีไฟล์ที่ต้องการอัปโหลดหรือไม่
            var files = new List<IFormFile>
            {
               collection.Files["copyCardIDInput"]!, // สำเนาบัตรประชาชน
               collection.Files["copyRegistrationCarFile"]! // สำเนาการจดทะเบียนรถยนต์
            };

            if (collection["resultsInspectionRadio"].ToString() == "Y")
            {
                files.Add(collection.Files["inspectionCarFormFile"]!); // แบบฟอร์มการตรวจสภาพรถยนต์
                files.Add(collection.Files["inspectionCar1File"]!);
                files.Add(collection.Files["inspectionCar2File"]!);
                files.Add(collection.Files["inspectionCar3File"]!);
                files.Add(collection.Files["inspectionCar4File"]!);
                files.Add(collection.Files["inspectionCar5File"]!);
                files.Add(collection.Files["inspectionCar6File"]!);
                files.Add(collection.Files["inspectionCar7File"]!);
                files.Add(collection.Files["inspectionCar8File"]!);
                files.Add(collection.Files["inspectionCar9File"]!);
                files.Add(collection.Files["inspectionCar10File"]!);
                files.Add(collection.Files["inspectionCar11File"]!);
                files.Add(collection.Files["inspectionCar12File"]!);
            }

            foreach (var file in files)
            {
                if (file == null) continue;

                var adaptedFile = new FormFileAdapter(file);
                var result = await _mediator.Send(new UploadFileCommand(adaptedFile));
                if (!result)
                {
                    return false; // ถ้าไฟล์ไม่ถูกต้อง ให้คืนค่า false ทันที
                }
            }

            return true;
        }

        private async Task<bool> SendVerifyCardIdEmail(string email, string transactionId)
        {
            try
            {
                var baseUrl = $"{_appSetting.WebBaseSubmitMotorUrl}/VerifyIdentity/VerifyCardID"; // กำหนด URL ที่ต้องการส่งไปยังผู้ใช้
                var logoUrl = $"{_appSetting.WebBaseSubmitMotorUrl}/image/logo-company/tbroker_header.png"; // กำหนด URL ของโลโก้บริษัท
                var token = _utilityHelper.Encrypt(transactionId, "T");
                var hrefLink = $"{baseUrl}/{HttpUtility.UrlEncode(token)}";
                var templateEmail = Path.Combine(_hostEnvironment.ContentRootPath, "EmailTemplate", "VerifyCardId.html"); // กำหนด path ของไฟล์ HTML template

                return await _verifyUseCase.SendVerificationEmailInstallmentAsync(email, templateEmail, logoUrl, hrefLink);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while sending verification email");
                return false; // คืนค่า false หากเกิดข้อผิดพลาด
            }
        }
    }
}