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

        public async Task<IActionResult> ContactInstallment()
        {
            var pdfBytes = await _createFileUseCase.CreateFileCoverNote(
                new CreateFileCoverNoteRequest
                {
                    IsPassword = false,
                    ReportId = 1,
                });
            var base64Pdf = Convert.ToBase64String(pdfBytes);
            ViewBag.PdfData = base64Pdf;
            return View();
        }
    }
}
