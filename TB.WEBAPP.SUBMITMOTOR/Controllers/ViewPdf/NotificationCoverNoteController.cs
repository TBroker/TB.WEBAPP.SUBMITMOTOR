using Microsoft.AspNetCore.Mvc;
using TB.WEBAPP.SUBMITMOTOR.APPLICATION.DTOs.Requests.CreateFiles;
using TB.WEBAPP.SUBMITMOTOR.APPLICATION.Interfaces.CreateFiles;

namespace TB.WEBAPP.SUBMITMOTOR.Controllers.ViewPdf
{
    public class NotificationCoverNoteController(ICreateFileUseCase createFileUseCase) : Controller
    {
        private readonly ICreateFileUseCase _createFileUseCase = createFileUseCase;

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> Voluntary()
        {
            var transactionId = "C1584793-FD70-4501-A8C4-CB12AFDD62EF";//TempData["transaction_id"]!.ToString();

            var pdfBytes = await _createFileUseCase.CreateFileCoverNoteVoluntary(
               new CreateFileCoverNoteRequest
               {
                   IsPassword = true,
                   TransactionId = transactionId ?? "",
               });

            var base64Pdf = Convert.ToBase64String(pdfBytes);
            ViewBag.PdfData = base64Pdf;
            return View();
        }

        public async Task<IActionResult> Compulsory()
        {
            var transactionId = "C1584793-FD70-4501-A8C4-CB12AFDD62EF";//TempData["transaction_id"]!.ToString();

            var pdfBytes = await _createFileUseCase.CreateFileCoverNoteCompulsory(
               new CreateFileCoverNoteRequest
               {
                   IsPassword = true,
                   TransactionId = transactionId ?? "",
               });

            var base64Pdf = Convert.ToBase64String(pdfBytes);
            ViewBag.PdfData = base64Pdf;
            return View();
        }
    }
}
