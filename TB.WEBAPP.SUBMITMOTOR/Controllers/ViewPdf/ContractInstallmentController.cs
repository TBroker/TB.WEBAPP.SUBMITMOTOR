using Microsoft.AspNetCore.Mvc;
using System.Web;
using TB.WEBAPP.SUBMITMOTOR.APPLICATION.DTOs.Requests.CreateFiles;
using TB.WEBAPP.SUBMITMOTOR.APPLICATION.Interfaces.CreateFiles;
using TB.WEBAPP.SUBMITMOTOR.APPLICATION.Interfaces.Utilities;

namespace TB.WEBAPP.SUBMITMOTOR.Controllers.ViewPdf
{
    public class ContractInstallmentController(ICreateFileUseCase createFileUseCase, IUtilityHelper utilityHelper) : Controller
    {
        private readonly ICreateFileUseCase _createFileUseCase = createFileUseCase;
        private readonly IUtilityHelper _utilityHelper = utilityHelper;

        public IActionResult Index()
        {
            return View();
        }

        [Route("/ContractInstallment/ContactInstallment/{token}")]
        public async Task<IActionResult> ContactInstallment(string token)
        {
            if (token == null)
                return View("TokenNotFound");

            var decryptedToken = HttpUtility.UrlDecode(token).Replace(" ", "+");
            var transactionId = _utilityHelper.Decrypt(decryptedToken, "T");

            var pdfBytes = await _createFileUseCase.CreateFileContactInstallment(
                new CreateFileContractInstallmentRequest
                {
                    IsPassword = false,
                    TransactionId = transactionId,
                });

            var base64Pdf = Convert.ToBase64String(pdfBytes);
            ViewBag.PdfData = base64Pdf;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ConFirmContactInstallment(IFormCollection collection)
        {
            return Json(new
            {
                success = true,
                message = "บันทึกข้อมูลทั้งหมดสำเร็จ",
                data = new
                {
                    formData = collection["verifyMobileInput"].ToString(),
                    totalFields = collection.Count
                }
            });
        }
    }
}