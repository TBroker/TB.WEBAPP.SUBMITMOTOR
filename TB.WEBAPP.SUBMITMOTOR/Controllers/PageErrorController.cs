using Microsoft.AspNetCore.Mvc;

namespace TB.WEBAPP.SUBMITMOTOR.Controllers
{
    public class PageErrorController : Controller
    {
        public IActionResult VerifyExpired()
        {
            return View();
        }

        public IActionResult VerifyNotFound()
        {
            return View();
        }

        public IActionResult PagePayment()
        {
            return View();
        }

        public IActionResult AgentNotFound()
        {
            return View();
        }

        public IActionResult AgentExpire()
        {
            return View();
        }
    }
}
