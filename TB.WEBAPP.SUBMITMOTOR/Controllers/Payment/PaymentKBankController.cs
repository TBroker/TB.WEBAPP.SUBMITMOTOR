using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using System.Globalization;
using System.Web;
using TB.WEBAPP.SUBMITMOTOR.APPLICATION.DTOs.Entities;
using TB.WEBAPP.SUBMITMOTOR.APPLICATION.DTOs.Requests.CoreSystems.Compulsories;
using TB.WEBAPP.SUBMITMOTOR.APPLICATION.DTOs.Requests.CoreSystems.Payments;
using TB.WEBAPP.SUBMITMOTOR.APPLICATION.DTOs.Requests.CoreSystems.Policies;
using TB.WEBAPP.SUBMITMOTOR.APPLICATION.DTOs.Requests.CoreSystems.Voluntaries;
using TB.WEBAPP.SUBMITMOTOR.APPLICATION.DTOs.Requests.CreateFiles;
using TB.WEBAPP.SUBMITMOTOR.APPLICATION.DTOs.Requests.Data.Installments;
using TB.WEBAPP.SUBMITMOTOR.APPLICATION.DTOs.Requests.Data.Motors;
using TB.WEBAPP.SUBMITMOTOR.APPLICATION.DTOs.Requests.Data.Payments;
using TB.WEBAPP.SUBMITMOTOR.APPLICATION.DTOs.Requests.Payments.KBanks;
using TB.WEBAPP.SUBMITMOTOR.APPLICATION.DTOs.Responses.CoreSystems.Compulsories;
using TB.WEBAPP.SUBMITMOTOR.APPLICATION.DTOs.Responses.CoreSystems.Voluntaries;
using TB.WEBAPP.SUBMITMOTOR.APPLICATION.Interfaces.CoreSystems.Compulsories;
using TB.WEBAPP.SUBMITMOTOR.APPLICATION.Interfaces.CoreSystems.Payments;
using TB.WEBAPP.SUBMITMOTOR.APPLICATION.Interfaces.CoreSystems.Policies;
using TB.WEBAPP.SUBMITMOTOR.APPLICATION.Interfaces.CoreSystems.Voluntaries;
using TB.WEBAPP.SUBMITMOTOR.APPLICATION.Interfaces.CreateFiles;
using TB.WEBAPP.SUBMITMOTOR.APPLICATION.Interfaces.Data.Installments;
using TB.WEBAPP.SUBMITMOTOR.APPLICATION.Interfaces.Data.Motors;
using TB.WEBAPP.SUBMITMOTOR.APPLICATION.Interfaces.Data.Payments;
using TB.WEBAPP.SUBMITMOTOR.APPLICATION.Interfaces.Payments;
using TB.WEBAPP.SUBMITMOTOR.APPLICATION.Interfaces.Utilities;
using TB.WEBAPP.SUBMITMOTOR.APPLICATION.Interfaces.Verifies;
using TB.WEBAPP.SUBMITMOTOR.DOMIAN.Enums;
using TB.WEBAPP.SUBMITMOTOR.INFRASTRUCTURE.Configurations;

namespace TB.WEBAPP.SUBMITMOTOR.Controllers.Payment
{
    public class PaymentKBankController(
         IMotorUseCase motorUseCase
        , IHostEnvironment hostEnvironment
        , ICompulsoryUseCase compulsoryUseCase
        , ILogger<PaymentKBankController> logger
        , IOptions<PaymentKBankSetting> paymentKBankSettings
        , IOptions<AppSetting> appSetting
        , IPaymentCoreSystemUseCase paymentUseCase
        , IPaymentDataUseCase paymentDataUseCase
        , IPaymentInstallmentUseCase paymentInstallmentUseCase
        , IPaymentKBankService paymentKBankService
        , IVoluntaryUseCase voluntaryUseCase
        , IPolicyUseCase policyUseCase
        , IServiceVerifyUseCase verifyUseCase
        , ICreateFileUseCase _createFileUseCase
        ) : Controller
    {
        private readonly CultureInfo _cultureInfo = CultureInfo.GetCultureInfo("en-GB");
        private readonly ICompulsoryUseCase _compulsoryUseCase = compulsoryUseCase;
        private readonly ILogger<PaymentKBankController> _logger = logger;
        private readonly AppSetting _appSetting = appSetting.Value;
        private readonly IMotorUseCase _motorUseCase = motorUseCase;
        private readonly IPaymentCoreSystemUseCase _paymentUseCase = paymentUseCase;
        private readonly IPaymentDataUseCase _paymentDataUseCase = paymentDataUseCase;
        private readonly IPaymentInstallmentUseCase _paymentInstallmentUseCase = paymentInstallmentUseCase;
        private readonly IPaymentKBankService _paymentKBankService = paymentKBankService;
        private readonly IVoluntaryUseCase _voluntaryUseCase = voluntaryUseCase;
        private readonly IPolicyUseCase _policyUseCase = policyUseCase;
        private readonly IServiceVerifyUseCase _verifyUseCase = verifyUseCase;
        private readonly IHostEnvironment _hostEnvironment = hostEnvironment;
        private readonly ICreateFileUseCase _createFileUseCase = _createFileUseCase;
        private readonly PaymentKBankSetting _paymentKBankSettings = paymentKBankSettings.Value;

        public async Task<IActionResult> ConfirmPaymentQRCode()
        {
            var transactionId = "C1584793-FD70-4501-A8C4-CB12AFDD62EF"; //TempData["transaction_id"]!.ToString();

            if (string.IsNullOrEmpty(transactionId))
                return RedirectToPaymentError();

            // Fetch the notify core system data using the transaction ID
            var resultNotifyCoreSystem = await _motorUseCase.FetchNotifyCoreSystemAsync(new NotifyCoreSystemRequest { TransactionID = transactionId.ToUpper() });
            if (resultNotifyCoreSystem.Data == null || !resultNotifyCoreSystem.Data.Any())
                return RedirectToPaymentError();

            // Get the first data item from the result
            var dataNotifyCoreSystem = resultNotifyCoreSystem.Data.FirstOrDefault();
            if (dataNotifyCoreSystem == null)
                return RedirectToPaymentError();

            // Fetch the notify core system data using the transaction ID
            var resultNotifyCoverNote = await _motorUseCase.FetchNotifyCoverNoteAsync(new NotifyCoverNoteRequest { TransactionID = transactionId.ToUpper() });
            if (resultNotifyCoverNote.Data == null || !resultNotifyCoverNote.Data.Any())
                return RedirectToPaymentError();

            // Get the first data item from the result
            var dataNotifyCoverNote = resultNotifyCoverNote.Data.FirstOrDefault();
            if (dataNotifyCoverNote == null)
                return RedirectToPaymentError();

            var resultCreatePolicyVoluntary = await _policyUseCase.CreatePolicyVoluntary(new IssuePolicyVoluntaryRequest
            {
                ApplicationNoVoluntary = dataNotifyCoverNote.ApplicationNoVoluntary,
                AgentCode = dataNotifyCoreSystem.AgentCode,
                InsureCompanyCode = dataNotifyCoreSystem.InsureCompanyCode
            });

            if (resultCreatePolicyVoluntary.Status!.Equals(MessageStatus.Success.ToString(), StringComparison.CurrentCultureIgnoreCase) && !string.IsNullOrEmpty(dataNotifyCoreSystem.ApplicationNoCompulsory))
            {
                // If the application number for compulsory insurance is provided, create the compulsory policy
                await _policyUseCase.CreatePolicyCompulsory(new IssuePolicyCompulsoryRequest
                {
                    ApplicationNoCompulsory = dataNotifyCoverNote.ApplicationNoCompulsory,
                    AgentCode = dataNotifyCoreSystem.AgentCode,
                    InsureCompanyCode = dataNotifyCoreSystem.InsureCompanyCode
                });
            }

           var resultVoluntaryEmail = await SendNotificationVoluntaryCoverNoteEmail(dataNotifyCoverNote.EmailAddress ?? "", dataNotifyCoverNote.ApplicationNoVoluntary ?? "", dataNotifyCoverNote.TransactionId ?? "");

            if (resultVoluntaryEmail && !string.IsNullOrEmpty(dataNotifyCoverNote.ApplicationNoCompulsory))
            {
                await SendNotificationCompulsoryCoverNoteEmail(dataNotifyCoverNote.EmailAddress ?? "", dataNotifyCoverNote.ApplicationNoCompulsory ?? "", dataNotifyCoverNote.TransactionId ?? "");
            }            

            TempData["transaction_id"] = dataNotifyCoverNote.TransactionId; // Store the transaction ID for later use

            ViewBag.TransactionId = dataNotifyCoverNote.TransactionId;
            ViewBag.VehicleRegNo = dataNotifyCoverNote.Vehicle;
            ViewBag.ApplicationNoVoluntary = dataNotifyCoverNote.ApplicationNoVoluntary;
            ViewBag.ApplicationNoCompulsory = dataNotifyCoverNote.ApplicationNoCompulsory;
            ViewBag.InsurceEmail = dataNotifyCoverNote.EmailAddress;
            return View();
        }

        public async Task<IActionResult> PaymentQRCode()
        {
            var transactionId = TempData["transaction_id"]!.ToString();
            if (string.IsNullOrEmpty(transactionId))
                return RedirectToPaymentError();

            // Fetch the notify core system data using the transaction ID
            var resultNotifyCoreSystem = await _motorUseCase.FetchNotifyCoreSystemAsync(new NotifyCoreSystemRequest { TransactionID = transactionId.ToUpper() });
            if (resultNotifyCoreSystem.Data == null || !resultNotifyCoreSystem.Data.Any())
                return RedirectToPaymentError();

            // Get the first data item from the result
            var dataNotifyCoreSystem = resultNotifyCoreSystem.Data.FirstOrDefault();
            if (dataNotifyCoreSystem == null)
                return RedirectToPaymentError();

            TempData["transaction_id"] = dataNotifyCoreSystem.TransactionId; // Store the transaction ID for later use

            // Check if the application number for voluntary insurance is provided
            var commissionVoluntary = await _voluntaryUseCase.FetchVoluntaryCommission(new VoluntaryCommissionRequest { ApplicationNoVoluntary = dataNotifyCoreSystem.ApplicationNoVoluntary });
            if (commissionVoluntary.Data == null || !commissionVoluntary.Data.Any())
                return RedirectToPaymentError();

            var commissionVoluntaryData = commissionVoluntary.Data.FirstOrDefault() ?? new VoluntaryCommissionResponse();

            // Calculate the commission amount based on whether it is voluntary or compulsory insurance
            var commissionAmount = dataNotifyCoreSystem.FlagCommission == "Y" ? commissionVoluntaryData.TotalPayNetCommission : commissionVoluntaryData.TotalPayFull;

            // If the application number for compulsory insurance is provided, fetch the commission for compulsory insurance
            if (dataNotifyCoreSystem.WithCompulsory == "Y" || !string.IsNullOrEmpty(dataNotifyCoreSystem.ApplicationNoCompulsory))
            {
                // If compulsory insurance is included, fetch the commission for compulsory insurance
                var commissionCompulsory = await _compulsoryUseCase.FetchCompulsoryCommission(new CompulsoryCommissionRequest { ApplicationNoCompulsory = dataNotifyCoreSystem.ApplicationNoCompulsory });
                if (commissionCompulsory.Data == null || !commissionCompulsory.Data.Any())
                    return RedirectToPaymentError();

                var commissionCompulsoryData = commissionCompulsory.Data.FirstOrDefault() ?? new CompulsoryPremiumResponse();
                commissionAmount += dataNotifyCoreSystem.FlagCommission == "Y" ? commissionCompulsoryData.TotalPayNetCommission : commissionCompulsoryData.TotalPayFull;
            }

            ViewBag.KBankPublicKey = _paymentKBankSettings.KBankPublicKey;
            ViewBag.KBankJS = _paymentKBankSettings.KBankJS;

            var createOrderRequest = new CreateOrderRequest
            {
                Amount = decimal.Parse(string.Format("{0:0.##}", commissionAmount)), // Example amount, you can set this dynamically based on your requirements
                Currency = "THB", // Thai Baht
                Description = "T Broker Online - QR Code Payment", // Description for the payment
                Refer1 = dataNotifyCoreSystem.ApplicationNoVoluntary, // Optional reference field
                Refer2 = dataNotifyCoreSystem.PhoneNumber, // Optional reference field
                Refer3 = DateTime.Now.ToString("yyyyMMddHHmmss", _cultureInfo), // Optional reference field
                ReferenceOrder = DateTime.Now.ToString("yyyyMMddHHmmss", _cultureInfo), // Unique order reference
                SourceType = "qr" // Specify the source type as QR code
            };

            var paymentResult = await _paymentKBankService.CreateOrderPayment(createOrderRequest);
            // Check if the payment creation was successful
            if (paymentResult == null || paymentResult.Code != 200 || paymentResult.Data == null)
            {
                // Handle error case, e.g., log the error or return an error view
                ViewBag.Amount = 0; // Set to zero or a default value
                ViewBag.Id = string.Empty; // Initialize with an empty string or a default value
                return RedirectToPaymentError();
            }

            var resultCreateTransactionPayment = await _paymentUseCase.CreateTransactionPayment(new CreateTransactionPaymentRequest
            {
                ApplicationNoCompulsory = dataNotifyCoreSystem.ApplicationNoCompulsory,
                ApplicationNoVoluntary = dataNotifyCoreSystem.ApplicationNoVoluntary,
                StatusPaid = "N",
                DateCreate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss", _cultureInfo),
                DateUpdate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss", _cultureInfo),
                OrderID = paymentResult.Data.Id,
                TotalPayAmount = decimal.Parse(string.Format("{0:0.##}", commissionAmount)),
                StatusCommission = dataNotifyCoreSystem.FlagCommission,
            });

            if (resultCreateTransactionPayment.Code != 200)
            {
                // Handle error case, e.g., log the error or return an error view
                ViewBag.Amount = 0; // Set to zero or a default value
                ViewBag.Id = string.Empty; // Initialize with an empty string or a default value
                return RedirectToPaymentError();
            }

            var isCreatePaymentConfirmSuccess = await _paymentDataUseCase.CreatePaymentConfirm(new WebPaymentConfirm
            {
                ApplicationNoCompulsory = dataNotifyCoreSystem.ApplicationNoCompulsory, // Application number for compulsory insurance
                ApplicationNoVoluntary = dataNotifyCoreSystem.ApplicationNoVoluntary, // Application number for voluntary insurance
                CompanyCode = dataNotifyCoreSystem.InsureCompanyCode, // Insurance company code
                DateCreate = DateTime.Now, // Set the creation date to now
                DateUpdate = DateTime.Now, // Set the update date to now
                OrderId = paymentResult.Data.Id, // Use the order ID returned from the payment service
                PaymentChannel = dataNotifyCoreSystem.ChannelPayment, // Payment channel used
                PaymentReference1 = dataNotifyCoreSystem.ChannelReference1, // Reference 1 for payment
                PaymentReference2 = dataNotifyCoreSystem.ChannelReference2, // Reference 2 for payment
                PaymentStatus = "N", // Initial status, will be updated after payment confirmation
                PaymentSystem = "Payment Online (KBank)", // Payment system used
                QuotationNumber = dataNotifyCoreSystem.QuotationNumber, // Quotation number
                TransactionId = dataNotifyCoreSystem.TransactionId, // Transaction ID
                UserId = dataNotifyCoreSystem.AgentCode, // User ID or agent code
            });

            if (!isCreatePaymentConfirmSuccess.Data)
            {
                // Handle error case, e.g., log the error or return an error view
                ViewBag.Amount = 0;
                ViewBag.Id = string.Empty; // Initialize with an empty string or a default value
                return RedirectToPaymentError();
            }

            ViewBag.Amount = decimal.Parse(string.Format("{0:0.##}", paymentResult.Data.Amount)); // Example amount, you can set this dynamically based on your requirements
            ViewBag.Id = paymentResult.Data.Id; // Unique order ID returned from the payment service
            return View();
        }

        public async Task<IActionResult> PaymentQRCodeInstallment()
        {
            // Retrieve the transaction ID from TempData
            var transactionId = TempData["transaction_id"]?.ToString();
            if (string.IsNullOrEmpty(transactionId)) return RedirectToPaymentError();

            // Fetch the payment installment data using the transaction ID
            var result = await _paymentInstallmentUseCase.FetchPaymentInstallment(new PaymentInstallmentRequest() { TransactionId = transactionId.ToUpper() });
            if (result.Data == null || !result.Data.Any()) return RedirectToPaymentError();

            // Get the first data item from the result
            var data = result.Data.FirstOrDefault();
            if (data == null) return RedirectToPaymentError();

            // Set the KBank public key and JS script in ViewBag for use in the view
            ViewBag.KBankPublicKey = _paymentKBankSettings.KBankPublicKey;
            ViewBag.KBankJS = _paymentKBankSettings.KBankJS;

            var createOrderRequest = new CreateOrderRequest
            {
                Amount = decimal.Parse(string.Format("{0:0.##}", data.FirstPeriodAmount)), // Example amount, you can set this dynamically based on your requirements
                Currency = "THB", // Thai Baht
                Description = "T Broker Online - QR Code Payment",
                Refer1 = data.ContractNumber, // Optional reference field
                Refer2 = data.Mobile, // Optional reference field
                Refer3 = DateTime.Now.ToString("yyyyMMddHHmmss", _cultureInfo), // Optional reference field
                ReferenceOrder = DateTime.Now.ToString("yyyyMMddHHmmss", _cultureInfo), // Unique order reference
                SourceType = "qr" // Specify the source type as QR code
            };

            var paymentResult = await _paymentKBankService.CreateOrderPayment(createOrderRequest);
            // Check if the payment creation was successful
            if (paymentResult == null || paymentResult.Code != 200 || paymentResult.Data == null)
            {
                // Handle error case, e.g., log the error or return an error view
                ViewBag.Amount = 0;
                ViewBag.Id = string.Empty; // Initialize with an empty string or a default value
                return RedirectToPaymentError();
            }

            ViewBag.Amount = decimal.Parse(string.Format("{0:0.##}", paymentResult.Data.Amount)); // Example amount, you can set this dynamically based on your requirements
            ViewBag.Id = paymentResult.Data.Id; // Unique order ID returned from the payment service
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SubmitPaymentQRCode(IFormCollection collection)
        {
            if (!ModelState.IsValid) return RedirectToPaymentError();

            // 1. Early validation
            var validationResult = ValidateRequest(collection);
            if (!validationResult.IsValid)
                return RedirectToPaymentError();

            try
            {
                // 2. Process payment inquiry
                var inquiryResult = await ProcessPaymentInquiry(validationResult.ChargeId);
                if (!inquiryResult.IsSuccess)
                    return RedirectToPaymentError();

                // 3. Update payment status
                var updateResult = await UpdatePaymentStatus(validationResult.TransactionId, inquiryResult.Data);
                if (!updateResult.IsSuccess)
                    return RedirectToPaymentError();

                TempData["transaction_id"] = validationResult.TransactionId;
                return RedirectToAction("ConfirmPaymentQRCode");
            }
            catch (Exception ex)
            {
                // Log exception
                _logger.LogError(ex, "Error processing payment QR code submission");
                return RedirectToPaymentError();
            }
        }

        private async Task<bool> SendNotificationVoluntaryCoverNoteEmail(string email, string applicationNo, string transactionId)
        {
            try
            {
                var pdfBytes = await _createFileUseCase.CreateFileCoverNoteVoluntary(
                   new CreateFileCoverNoteRequest
                   {
                       IsPassword = true,
                       TransactionId = transactionId ?? "",
                   });

                var base64Pdf = Convert.ToBase64String(pdfBytes);

                var logoUrl = $"{_appSetting.WebBaseSubmitMotorUrl}/image/logo-company/tbroker_header.png"; // กำหนด URL ของโลโก้บริษัท
                var templateEmail = Path.Combine(_hostEnvironment.ContentRootPath, "EmailTemplate", "CoverNote.html"); // กำหนด path ของไฟล์ HTML template

                return await _verifyUseCase.SendNotificationEmailCoverNoteAsync(email, templateEmail, logoUrl, applicationNo, base64Pdf);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while sending verification email");
                return false; // คืนค่า false หากเกิดข้อผิดพลาด
            }
        }

        private async Task<bool> SendNotificationCompulsoryCoverNoteEmail(string email, string applicationNo, string transactionId)
        {
            try
            {
                var pdfBytes = await _createFileUseCase.CreateFileCoverNoteCompulsory(
                   new CreateFileCoverNoteRequest
                   {
                       IsPassword = true,
                       TransactionId = transactionId ?? "",
                   });

                var base64Pdf = Convert.ToBase64String(pdfBytes);

                var logoUrl = $"{_appSetting.WebBaseSubmitMotorUrl}/image/logo-company/tbroker_header.png"; // กำหนด URL ของโลโก้บริษัท
                var templateEmail = Path.Combine(_hostEnvironment.ContentRootPath, "EmailTemplate", "CoverNote.html"); // กำหนด path ของไฟล์ HTML template

                return await _verifyUseCase.SendNotificationEmailCoverNoteAsync(email, templateEmail, logoUrl, applicationNo, base64Pdf);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while sending verification email");
                return false; // คืนค่า false หากเกิดข้อผิดพลาด
            }
        }

        private static bool IsPaymentSuccessful(string status)
        {
            return status.Equals(MessageStatus.Success.ToString(), StringComparison.CurrentCultureIgnoreCase);
        }

        private async Task<WebPaymentConfirm> GetPaymentConfirmation(string transactionId)
        {
            var result = await _paymentDataUseCase.FetchPaymentConfirm(
                new PaymentConfirmRequest { TransactionId = transactionId.ToUpper() });

            return result.Data?.FirstOrDefault() ?? new WebPaymentConfirm();
        }

        // Payment inquiry processing
        private async Task<InquiryResult> ProcessPaymentInquiry(string chargeId)
        {
            var result = await _paymentKBankService.InquiryQrTransaction(chargeId);

            if (result?.Code != 200 || result.Data == null)
                return InquiryResult.Failed();

            var data = result.Data;
            if (string.IsNullOrEmpty(data.Id) || string.IsNullOrEmpty(data.Status))
                return InquiryResult.Failed();

            if (!IsPaymentSuccessful(data.Status))
                return InquiryResult.Failed();

            return InquiryResult.Success(data);
        }

        // Helper method
        private IActionResult RedirectToPaymentError()
        {
            return RedirectToAction("PagePayment", "PageError");
        }

        private async Task<bool> UpdatePaymentConfirmation(WebPaymentConfirm paymentConfirm)
        {
            paymentConfirm.DateUpdate = DateTime.Now;
            paymentConfirm.PaymentStatus = "Y";

            var result = await _paymentDataUseCase.ModifyPaymentConfirm(paymentConfirm);
            return result.Data;
        }

        // Payment status update
        private async Task<UpdateResult> UpdatePaymentStatus(string transactionId, dynamic inquiryData)
        {
            // Fetch payment confirmation
            var paymentConfirm = await GetPaymentConfirmation(transactionId);
            if (paymentConfirm == null)
                return UpdateResult.Failed();

            // Update transaction
            var transactionUpdated = await UpdateTransaction(paymentConfirm);
            if (!transactionUpdated)
                return UpdateResult.Failed();

            // Update payment confirmation
            var confirmationUpdated = await UpdatePaymentConfirmation(paymentConfirm);
            if (!confirmationUpdated)
                return UpdateResult.Failed();

            return UpdateResult.Success();
        }

        private async Task<bool> UpdateTransaction(WebPaymentConfirm paymentConfirm)
        {
            var request = new UpdateTransactionPaymentRequest
            {
                DateUpdate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss", _cultureInfo),
                OrderId = paymentConfirm.OrderId,
                StatusPaid = "Y"
            };

            var result = await _paymentUseCase.ModifyTransactionPayment(request);
            return result.Data != null;
        }

        // Validation methods
        private ValidationResult ValidateRequest(IFormCollection collection)
        {
            var transactionId = TempData["transaction_id"]?.ToString();
            if (string.IsNullOrEmpty(transactionId))
                return ValidationResult.Invalid();

            var chargeId = collection["chargeId"].ToString();
            if (string.IsNullOrEmpty(chargeId))
                return ValidationResult.Invalid();

            return ValidationResult.Valid(transactionId, chargeId);
        }

        public class InquiryResult
        {
            private InquiryResult(bool isSuccess, dynamic data = null)
            {
                IsSuccess = isSuccess;
                Data = data;
            }

            public dynamic Data { get; private set; }
            public bool IsSuccess { get; private set; }

            public static InquiryResult Failed() =>
                new(false);

            public static InquiryResult Success(dynamic data) =>
                            new(true, data);
        }

        public class UpdateResult
        {
            private UpdateResult(bool isSuccess) => IsSuccess = isSuccess;

            public bool IsSuccess { get; private set; }

            public static UpdateResult Failed() => new(false);

            public static UpdateResult Success() => new(true);
        }

        public class ValidationResult
        {
            private ValidationResult(bool isValid, string transactionId = null, string chargeId = null)
            {
                IsValid = isValid;
                TransactionId = transactionId;
                ChargeId = chargeId;
            }

            public string ChargeId { get; private set; }
            public bool IsValid { get; private set; }
            public string TransactionId { get; private set; }

            public static ValidationResult Invalid() =>
                new(false);

            public static ValidationResult Valid(string transactionId, string chargeId) =>
                            new(true, transactionId, chargeId);
        }
    }
}