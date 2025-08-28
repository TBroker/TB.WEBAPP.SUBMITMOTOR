using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.Extensions.Options;
using System.Web;
using TB.WEBAPP.SUBMITMOTOR.APPLICATION.DTOs.Requests.Data.Installments;
using TB.WEBAPP.SUBMITMOTOR.APPLICATION.DTOs.Requests.Data.Motors;
using TB.WEBAPP.SUBMITMOTOR.APPLICATION.DTOs.Requests.Services;
using TB.WEBAPP.SUBMITMOTOR.APPLICATION.DTOs.Responses.Data.Verifies;
using TB.WEBAPP.SUBMITMOTOR.APPLICATION.Interfaces.Data.Installments;
using TB.WEBAPP.SUBMITMOTOR.APPLICATION.Interfaces.Data.Motors;
using TB.WEBAPP.SUBMITMOTOR.APPLICATION.Interfaces.Data.Verifies;
using TB.WEBAPP.SUBMITMOTOR.APPLICATION.Interfaces.Notifications;
using TB.WEBAPP.SUBMITMOTOR.APPLICATION.Interfaces.Utilities;
using TB.WEBAPP.SUBMITMOTOR.INFRASTRUCTURE.Configurations;

namespace TB.WEBAPP.SUBMITMOTOR.Controllers.VerifyIdentity
{
    public class VerifyIdentityController(
        IOptions<AppSetting> appSetting
        , INotificationService notificationService
        , IPaymentInstallmentUseCase paymentInstallmentUseCase
        , IUtilityHelper utilityHelper
        , IMotorUseCase motorUseCase
        , IVerifyUseCase verifyUseCase
        , ILogger<VerifyIdentityController> logger
        ) : Controller
    {
        private readonly AppSetting _appSetting = appSetting.Value;
        private readonly IMotorUseCase _motorUseCase = motorUseCase;
        private readonly INotificationService _notificationService = notificationService;
        private readonly IPaymentInstallmentUseCase _paymentInstallmentUseCase = paymentInstallmentUseCase;
        private readonly IUtilityHelper _utilityHelper = utilityHelper;
        private readonly IVerifyUseCase _verifyUseCase = verifyUseCase;
        private readonly ILogger<VerifyIdentityController> _logger = logger;

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ConfirmMobile(IFormCollection collection)
        {
            try
            {
                if (!ModelState.IsValid) return Json(new { success = false, message = "ข้อมูลไม่ถูกต้อง" });

                var verifyMobileHidden = HttpUtility.UrlDecode(collection["verifyMobileHidden"].ToString()).Replace(" ", "+");
                var decryptedToken = _utilityHelper.Decrypt(verifyMobileHidden, "T");
                var transactionId = decryptedToken;
                var resultOtp = await _verifyUseCase.FetchVerifyOtpByTransactionId(transactionId ?? "");
                var resultOtpData = resultOtp.Data != null && resultOtp.Data.Any() ? resultOtp.Data.FirstOrDefault() : null;

                if (resultOtpData == null) return Json(new { success = false, message = "ข้อมูลไม่ถูกต้อง" });

                if (resultOtpData.Status != "Y") return Json(new { success = false, message = "กรุณายืนยัน OTP ก่อน" });

                if (string.IsNullOrEmpty(collection["relationshipSelect"].ToString()) || string.IsNullOrEmpty(collection["mobileInput"].ToString())) return Json(new { success = false, message = "กรุณากรอกข้อมูล" });

                var result = await _paymentInstallmentUseCase.FetchNotifyInstallmentContract(new NotifyInstallmentContractRequest() { TransactionId = transactionId ?? "", });
                var notifyInstallmentContract = result.Data != null && result.Data.Any() ? result.Data.FirstOrDefault() : null;
                if (notifyInstallmentContract == null) return Json(new { success = false, message = "ข้อมูลไม่ถูกต้อง" });

                notifyInstallmentContract.Relationship = collection["relationshipSelect"].ToString();
                notifyInstallmentContract.ReferencePhone = collection["mobileInput"].ToString();

                var isSuccess = await _paymentInstallmentUseCase.ModifyNotifyInstallmentContract(notifyInstallmentContract);
                if (!isSuccess.Data) return Json(new { success = false, message = "เกิดข้อผิดพลาด" });

                return Json(new { success = true, message = "บันทึกข้อมูลทั้งหมดสำเร็จ", data = new { formData = HttpUtility.UrlEncode(collection["verifyMobileHidden"].ToString()) } });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "ConfirmMobile Error: {Message}", ex.Message);
                return Json(new { success = false, message = "เกิดข้อผิดพลาด" });
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ConfirmVerifyCardID(IFormCollection collection)
        {
            try
            {
                if (!ModelState.IsValid) return Json(new { success = false, message = "ข้อมูลไม่ถูกต้อง" });

                var token = collection["verifyCardIdHidden"].ToString() ?? "";
                if (token == null || token == "") return Json(new { success = false, message = "ข้อมูลไม่ถูกต้อง" });

                var transactionNumber = _utilityHelper.Decrypt(HttpUtility.UrlDecode(token).Replace(" ", "+"), "T");
                var notifyCoreSystemDrafts = await _motorUseCase.FetchNotifyCoreSystemDraftsAsync(new NotifyCoreSystemDraftRequest() { TransactionId = transactionNumber ?? "" });
                var notifyCoreSystemDraft = notifyCoreSystemDrafts.Data != null && notifyCoreSystemDrafts.Data.Any() ? notifyCoreSystemDrafts.Data.FirstOrDefault() : null;
                if (notifyCoreSystemDraft == null) return Json(new { success = false, message = "ข้อมูลไม่ถูกต้อง" });

                if (string.IsNullOrEmpty(collection["verifyCardIdInput"].ToString())) return Json(new { success = false, message = "กรุณากรอกรหัสบัตรประชาชน" });

                var isMatchCardId = notifyCoreSystemDraft.InsureIdCard == collection["verifyCardIdInput"].ToString();
                if (!isMatchCardId) return Json(new { success = false, message = "ข้อมูลไม่ถูกต้อง" });

                var otpCode = _utilityHelper.GenerateOtpCode();
                var otpRef = _utilityHelper.GenerateOtpRef();
                var otpExpire = DateTime.Now.AddMinutes(5);
                var otpToken = notifyCoreSystemDraft.TransactionId ?? "";
                var otpEncrypt = _utilityHelper.Encrypt(otpToken, "T");
                var otpLink = $"{_appSetting.WebBaseSubmitMotorUrl}/VerifyIdentity/VerifyOtp/{HttpUtility.UrlEncode(otpEncrypt)}";
                var otpMessage = $"รหัส OTP ของคุณ คือ {otpCode} โดยมี Ref : {otpRef} \r\nกดลิงก์เพื่อยืนยัน: {otpLink}\r\nรหัสนี้จะหมดอายุใน 5 นาที กรุณาอย่าเปิดเผยรหัสนี้กับผู้อื่น";

                var resultOtp = await _verifyUseCase.FetchVerifyOtpByTransactionId(notifyCoreSystemDraft.TransactionId ?? "");
                var resultOtpData = resultOtp.Data != null && resultOtp.Data.Any() ? resultOtp.Data.FirstOrDefault() : null;

                if (resultOtpData != null)
                {
                    await _verifyUseCase.ModifyVerifyOtp(new WebVerifyOtp()
                    {
                        Id = resultOtpData.Id,
                        DateCreate = DateTime.Now,
                        DateExpire = otpExpire,
                        ErrorCount = 0,
                        OtpCode = otpCode,
                        RefCode = otpRef,
                        SentTo = notifyCoreSystemDraft.PhoneNumber,
                        Status = "N",
                        TransactionId = notifyCoreSystemDraft.TransactionId,
                    });

                    var isSendSmsModify = await _notificationService.SendSmsTBroker(new SendSmsTBRequest() { PhoneNumber = notifyCoreSystemDraft.PhoneNumber ?? "", Message = otpMessage });
                    if (isSendSmsModify == null) return Json(new { success = false, message = "เกิดข้อผิดพลาด: ไม่สามารถส่ง OTP ได้" });

                    return Json(new { success = true, message = $"ระบบได้ส่งรหัสยืนยัน OTP ไปยังเบอร์ {notifyCoreSystemDraft.PhoneNumber}", });
                }

                await _verifyUseCase.CreateVerifyOtp(new WebVerifyOtp()
                {
                    DateCreate = DateTime.Now,
                    DateExpire = otpExpire,
                    ErrorCount = 0,
                    OtpCode = otpCode,
                    RefCode = otpRef,
                    SentTo = notifyCoreSystemDraft.PhoneNumber,
                    Status = "N",
                    TransactionId = notifyCoreSystemDraft.TransactionId,
                });

                var isSendSms = await _notificationService.SendSmsTBroker(new SendSmsTBRequest() { PhoneNumber = notifyCoreSystemDraft.PhoneNumber ?? "", Message = otpMessage });
                if (isSendSms == null) return Json(new { success = false, message = "เกิดข้อผิดพลาด: ไม่สามารถส่ง OTP ได้" });

                return Json(new { success = true, message = $"ระบบได้ส่งรหัสยืนยัน OTP ไปยังเบอร์ {notifyCoreSystemDraft.PhoneNumber}", });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "ConfirmVerifyCardID Error: {Message}", ex.Message);
                return Json(new { success = false, message = "เกิดข้อผิดพลาด" });
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ConfirmVerifyOtp(IFormCollection collection)
        {
            try
            {
                if (!ModelState.IsValid) return Json(new { success = false, message = "ข้อมูลไม่ถูกต้อง" });

                if (string.IsNullOrEmpty(collection["verifyOtpInput"].ToString())) return Json(new { success = false, message = "กรุณากรอกรหัส OTP" });

                var verifyOtpInput = collection["verifyOtpInput"].ToString();

                var verifyOtpCodeHidden = HttpUtility.UrlDecode(collection["verifyOtpCodeHidden"].ToString()).Replace(" ", "+");
                var decryptedToken = _utilityHelper.Decrypt(verifyOtpCodeHidden, "T");
                var transactionId = decryptedToken;
                var resultOtp = await _verifyUseCase.FetchVerifyOtpByTransactionId(transactionId ?? "");
                var resultOtpData = resultOtp.Data != null && resultOtp.Data.Any() ? resultOtp.Data.FirstOrDefault() : null;

                if (resultOtpData == null) return Json(new { success = false, message = "ข้อมูลไม่ถูกต้อง" });

                if (resultOtpData.ErrorCount >= 3) return Json(new { success = false, message = "ระบุรหัส OTP เกินจำนวน 3 ครั้ง" });

                if (resultOtpData.OtpCode != verifyOtpInput)
                {
                    resultOtpData.ErrorCount = resultOtpData.ErrorCount + 1;
                    var isModifyVerifyOtp = await _verifyUseCase.ModifyVerifyOtp(resultOtpData);
                    if (!isModifyVerifyOtp.Data) return Json(new { success = false, message = "เกิดข้อผิดพลาด: ไม่สามารถยืนยัน OTP ได้" });

                    return Json(new { success = false, message = "ข้อมูลไม่ถูกต้อง" });
                }
                resultOtpData.Status = "Y";
                var isConfirmVerifyOtp = await _verifyUseCase.ModifyVerifyOtp(resultOtpData);
                if (!isConfirmVerifyOtp.Data) return Json(new { success = false, message = "เกิดข้อผิดพลาด: ไม่สามารถยืนยัน OTP ได้" });

                return Json(new { success = true, message = "ยืนยันรหัส OTP สําเร็จ", data = new { formData = HttpUtility.UrlEncode(collection["verifyOtpCodeHidden"].ToString()), } });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "ConfirmVerifyOtp Error: {Message}", ex.Message);
                return Json(new { success = false, message = "เกิดข้อผิดพลาด" });
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ResendVerifyOtp(IFormCollection collection)
        {
            if (!ModelState.IsValid)
            {
                return Json(new
                {
                    success = false,
                    message = "ข้อมูลไม่ถูกต้อง"
                });
            }

            var verifyOtpCodeHidden = HttpUtility.UrlDecode(collection["verifyOtpCodeHidden"].ToString()).Replace(" ", "+");
            var decryptedToken = _utilityHelper.Decrypt(verifyOtpCodeHidden, "T");
            var transactionId = decryptedToken;
            var resultOtp = await _verifyUseCase.FetchVerifyOtpByTransactionId(transactionId ?? "");
            var resultOtpData = resultOtp.Data != null && resultOtp.Data.Any() ? resultOtp.Data.FirstOrDefault() : null;
            var otpCode = _utilityHelper.GenerateOtpCode();
            var otpRef = _utilityHelper.GenerateOtpRef();
            var otpExpire = DateTime.Now.AddMinutes(5);

            if (resultOtpData == null)
                return Json(new { success = false, message = "ข้อมูลไม่ถูกต้อง" });

            if (resultOtpData != null)
            {
                resultOtpData.OtpCode = otpCode;
                resultOtpData.RefCode = otpRef;
                resultOtpData.ErrorCount = 0;
                resultOtpData.DateExpire = otpExpire;
                await _verifyUseCase.ModifyVerifyOtp(resultOtpData);
            }

            var otpMessage = $"รหัส OTP ของคุณ คือ {otpCode} โดยมี Ref : {otpRef} \r\nรหัสนี้จะหมดอายุใน 5 นาที กรุณาอย่าเปิดเผยรหัสนี้กับผู้อื่น";

            await _notificationService.SendSmsTBroker(new SendSmsTBRequest() { PhoneNumber = resultOtpData!.SentTo!, Message = otpMessage });

            ViewBag.OtpCode = resultOtpData.OtpCode;
            ViewBag.OtpRef = resultOtpData.RefCode;

            return Json(new
            {
                success = true,
                message = "รหัส OTP ถูกส่งไปยังเบอร์โทรศัพท์ของคุณแล้ว",
                data = new
                {
                    otpRef
                }
            });
        }

        [Route("/VerifyIdentity/VerifyCardID/{token}")]
        public async Task<IActionResult> VerifyCardID(string token)
        {
            if (token == null) return View("TokenNotFound");
            var urlDecode = HttpUtility.UrlDecode(token).Replace(" ", "+");
            var transactionId = _utilityHelper.Decrypt(urlDecode, "T");
            var result = await _paymentInstallmentUseCase.FetchPaymentInstallment(new PaymentInstallmentRequest() { TransactionId = transactionId });
            if (result.Data == null || !result.Data.Any()) return View("resultNotFound");

            var data = result.Data.FirstOrDefault();
            if (data == null) return View("ResultNotFound");

            var isExpired = data.DateExpire <= DateTime.Now;
            if (isExpired) return RedirectToAction("VerifyExpired", "PageError");

            ViewBag.Token = token;
            return View();
        }

        [Route("/VerifyIdentity/VerifyMobile/{token}")]
        public async Task<IActionResult> VerifyMobile(string token)
        {
            if (token == null) return View("TokenNotFound");
            var urlDecode = HttpUtility.UrlDecode(token).Replace(" ", "+");
            var decryptedToken = _utilityHelper.Decrypt(urlDecode, "T");
            var transactionId = decryptedToken;
            var resultOtp = await _verifyUseCase.FetchVerifyOtpByTransactionId(transactionId ?? "");
            var resultOtpData = resultOtp.Data != null && resultOtp.Data.Any() ? resultOtp.Data.FirstOrDefault() : null;

            if (resultOtpData == null) return View("resultNotFound");

            var result = await _paymentInstallmentUseCase.FetchPaymentInstallment(new PaymentInstallmentRequest() { TransactionId = transactionId });
            if (result.Data == null || !result.Data.Any()) return View("resultNotFound");

            var data = result.Data.FirstOrDefault();
            if (data == null) return View("ResultNotFound");

            var isExpired = data.DateExpire <= DateTime.Now;
            if (isExpired) return RedirectToAction("VerifyExpired", "PageError");

            if (resultOtpData.Status != "Y") return RedirectToAction("VerifyExpired", "PageError");

            ViewBag.Token = token;
            return View();
        }

        [Route("/VerifyIdentity/VerifyOtp/{token}")]
        public async Task<IActionResult> VerifyOtp(string token)
        {
            if (token == null) return View("TokenNotFound");
            var urlDecode = HttpUtility.UrlDecode(token).Replace(" ", "+");
            var decryptedToken = _utilityHelper.Decrypt(urlDecode, "T");
            var transactionId = decryptedToken;
            var resultOtp = await _verifyUseCase.FetchVerifyOtpByTransactionId(transactionId ?? "");
            var resultOtpData = resultOtp.Data != null && resultOtp.Data.Any() ? resultOtp.Data.FirstOrDefault() : null;

            if (resultOtpData == null) return View("resultNotFound");

            var isExpired = resultOtpData.DateExpire <= DateTime.Now;
            if (isExpired) return RedirectToAction("VerifyExpired", "PageError");

            ViewBag.OtpCode = resultOtpData.OtpCode;
            ViewBag.OtpRef = resultOtpData.RefCode;
            ViewBag.Token = token;
            return View();
        }
    }
}