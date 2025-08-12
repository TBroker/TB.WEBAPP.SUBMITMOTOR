using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Globalization;
using TB.WEBAPP.SUBMITMOTOR.APPLICATION.DTOs.Requests.Payments.Kbanks;
using TB.WEBAPP.SUBMITMOTOR.APPLICATION.Interfaces.Payments;
using TB.WEBAPP.SUBMITMOTOR.INFRASTRUCTURE.Configurations;

namespace TB.WEBAPP.SUBMITMOTOR.Controllers.Payment
{
    public class PaymentKBankController(IOptions<PaymentKBankSetting> paymentKBankSettings,IPaymentService paymentService) : Controller
    {
        private readonly PaymentKBankSetting _paymentKBankSettings = paymentKBankSettings.Value;
        private readonly IPaymentService _paymentService = paymentService;

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> PaymentQRCode()
        {
            ViewBag.KBankPublicKey = _paymentKBankSettings.KBankPublicKey;
            ViewBag.KBankJS = _paymentKBankSettings.KBankJS;

            decimal amount = 1000;
             var createOrderRequest = new CreateOrderRequest
            {
                Amount = decimal.Parse(amount.ToString("0.##")), // Example amount, you can set this dynamically based on your requirements
                Currency = "THB", // Thai Baht
                Description = "T Broker Online - QR Code Payment",
                Refer1 = DateTime.Now.ToString("yyyyMMddHHmmss", CultureInfo.GetCultureInfo("en-GB")), // Optional reference field
                Refer2 = "0882829551", // Optional reference field
                Refer3 = DateTime.Now.ToString("yyyyMMddHHmmss", CultureInfo.GetCultureInfo("en-GB")), // Optional reference field
                ReferenceOrder = DateTime.Now.ToString("yyyyMMddHHmmss", CultureInfo.GetCultureInfo("en-GB")), // Unique order reference
                SourceType = "qr" // Specify the source type as QR code
             };

            var paymentResult = await _paymentService.CreatePayment(createOrderRequest);
            // Check if the payment creation was successful
            if (paymentResult == null || paymentResult.Code != 200)
            {
                // Handle error case, e.g., log the error or return an error view
                ViewBag.ErrorMessage = "Failed to create payment. Please try again.";
                return View("Error");
            }

            if (paymentResult.Code == 200)
            {
                if (paymentResult == null || paymentResult.Code != 200 || paymentResult.Data == null)
                {
                    // Handle error case, e.g., log the error or return an error view
                    ViewBag.ErrorMessage = "Failed to create payment. Please try again.";
                    return View("Error");
                }

                if (paymentResult.Code == 200)
                {
                    ViewBag.Amount = decimal.Parse(paymentResult.Data.Amount.ToString("0.##")); // Example amount, you can set this dynamically based on your requirements
                    ViewBag.Id = paymentResult.Data.Id; // Unique order ID returned from the payment service
                }
                return View();
            }
            ViewBag.Amount = 0;
            ViewBag.Id = string.Empty; // Initialize with an empty string or a default value
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SubmitPaymentQRCode(IFormCollection form)
        {
            return Json(new
            {
                success = true,
                message = "บันทึกข้อมูลทั้งหมดสำเร็จ",
                data = new
                {
                    // You can add any additional data you want to return here
                }
            });
        }

        public IActionResult ConfirmPaymentQRCode()
        {
            return View();
        }
    }
}
