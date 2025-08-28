using Microsoft.AspNetCore.Mvc;
using System.Web;
using TB.WEBAPP.SUBMITMOTOR.APPLICATION.DTOs.Requests.Data.Installments;
using TB.WEBAPP.SUBMITMOTOR.APPLICATION.Interfaces.Data.Installments;
using TB.WEBAPP.SUBMITMOTOR.APPLICATION.Interfaces.Data.Verifies;
using TB.WEBAPP.SUBMITMOTOR.APPLICATION.Interfaces.Utilities;

namespace TB.WEBAPP.SUBMITMOTOR.Controllers
{
    public class PaymentInstallmentController(
        IUtilityHelper utilityHelper
        , IPaymentInstallmentUseCase paymentInstallmentUseCase
        , IVerifyUseCase verifyUseCase
        ) : Controller
    {
        private readonly IUtilityHelper _utilityHelper = utilityHelper;
        private readonly IPaymentInstallmentUseCase _paymentInstallmentUseCase = paymentInstallmentUseCase;
        private readonly IVerifyUseCase _verifyUseCase = verifyUseCase;

        [Route("/PaymentInstallment/PaymentInstallment/{token}")]
        public async Task<IActionResult> PaymentInstallment(string token)
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

            TempData["transaction_id"] = transactionId;
            ViewBag.Token = token;
            ViewBag.Amount = string.Format("{0:N2}", data.FirstPeriodAmount);

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ConfirmPaymentInstallment()
        {
            return Json(new
            {
                success = true,
                message = "ยืนยันการชำระเงิน",
            });
        }
    }
}
