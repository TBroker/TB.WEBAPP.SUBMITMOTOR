using Microsoft.AspNetCore.Mvc;

namespace TB.WEBAPP.SUBMITMOTOR.Controllers
{
    public class PaymentInstallmentController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult PaymentInstallment()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ConFirmPaymentInstallment() 
        {
            return Json(new
            {
                success = true,
                message = "บันทึกข้อมูลทั้งหมดสำเร็จ",
                data = new
                {
                    
                }
            });
        }
    }
}
