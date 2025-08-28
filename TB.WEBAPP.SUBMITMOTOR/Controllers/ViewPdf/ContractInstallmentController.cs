using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Web;
using TB.WEBAPP.SUBMITMOTOR.Adapters;
using TB.WEBAPP.SUBMITMOTOR.APPLICATION.DTOs.Requests.CreateFiles;
using TB.WEBAPP.SUBMITMOTOR.APPLICATION.DTOs.Requests.Data.Installments;
using TB.WEBAPP.SUBMITMOTOR.APPLICATION.Features.UploadFile;
using TB.WEBAPP.SUBMITMOTOR.APPLICATION.Interfaces.CreateFiles;
using TB.WEBAPP.SUBMITMOTOR.APPLICATION.Interfaces.Data.Installments;
using TB.WEBAPP.SUBMITMOTOR.APPLICATION.Interfaces.Data.Verifies;
using TB.WEBAPP.SUBMITMOTOR.APPLICATION.Interfaces.Utilities;

namespace TB.WEBAPP.SUBMITMOTOR.Controllers.ViewPdf
{
    public class ContractInstallmentController(
         ILogger<ContractInstallmentController> logger
        , IMediator mediator
        , IPaymentInstallmentUseCase paymentInstallmentUseCase
        , IUtilityHelper utilityHelper
        , IVerifyUseCase verifyUseCase
        , ICreateFileUseCase createFileUseCase
        ) : Controller
    {
        private readonly ICreateFileUseCase _createFileUseCase = createFileUseCase;
        private readonly ILogger<ContractInstallmentController> _logger = logger;
        private readonly IMediator _mediator = mediator;
        private readonly IPaymentInstallmentUseCase _paymentInstallmentUseCase = paymentInstallmentUseCase;
        private readonly IUtilityHelper _utilityHelper = utilityHelper;
        private readonly IVerifyUseCase _verifyUseCase = verifyUseCase;

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ConfirmContactInstallment(IFormCollection collection)
        {
            try
            {
                if (!ModelState.IsValid) return Json(new { success = false, message = "ข้อมูลไม่ถูกต้อง" });

                var confirmConsentHidden = HttpUtility.UrlDecode(collection["confirmConsentHidden"].ToString()).Replace(" ", "+");
                var decryptedToken = _utilityHelper.Decrypt(confirmConsentHidden, "T");
                var transactionId = decryptedToken;
                var resultOtp = await _verifyUseCase.FetchVerifyOtpByTransactionId(transactionId ?? "");
                var resultOtpData = resultOtp.Data != null && resultOtp.Data.Any() ? resultOtp.Data.FirstOrDefault() : null;

                if (resultOtpData == null) return Json(new { success = false, message = "ข้อมูลไม่ถูกต้อง" });

                if (resultOtpData.Status != "Y") return Json(new { success = false, message = "กรุณายืนยัน OTP ก่อน" });

                if (collection.Files["documentFileInput"] == null || collection.Files.Count == 0) return Json(new { success = false, message = "กรุณาอัปโหลดไฟล์" });

                var isSuccessFile = await IsCheckValidInputFileAsync(collection);
                if (!isSuccessFile) return Json(new { success = false, message = "ไฟล์ที่อัปโหลดไม่ถูกต้อง กรุณาอัปโหลดไฟล์ใหม่" });

                return Json(new { success = true, message = "ยืนยันสำเร็จ", data = new { formData = HttpUtility.UrlEncode(collection["confirmConsentHidden"].ToString()) } });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in ConfirmContactInstallment");
                return Json(new { success = false, message = "เกิดข้อผิดพลาดในการบันทึกข้อมูล กรุณาลองใหม่อีกครั้ง" });
            }
        }

        [Route("/ContractInstallment/ContactInstallment/{token}")]
        public async Task<IActionResult> ContactInstallment(string token)
        {
            if (token == null) return View("TokenNotFound");

            var decryptedToken = HttpUtility.UrlDecode(token).Replace(" ", "+");
            var transactionId = _utilityHelper.Decrypt(decryptedToken, "T");
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

            var pdfBytes = await _createFileUseCase.CreateFileContactInstallment(
                new CreateFileContractInstallmentRequest
                {
                    IsPassword = false,
                    TransactionId = transactionId ?? "",
                });

            var base64Pdf = Convert.ToBase64String(pdfBytes);
            ViewBag.PdfData = base64Pdf;
            return View();
        }

        private async Task<bool> IsCheckValidInputFileAsync(IFormCollection collection)
        {
            // ตรวจสอบว่ามีข้อมูลใน form หรือไม่
            if (!ModelState.IsValid) return false;

            var adaptedFile = new FormFileAdapter(collection.Files["documentFileInput"]!);
            var result = await _mediator.Send(new UploadFileCommand(adaptedFile));
            if (!result)
            {
                return false; // ถ้าไฟล์ไม่ถูกต้อง ให้คืนค่า false ทันที
            }

            return true;
        }
    }
}