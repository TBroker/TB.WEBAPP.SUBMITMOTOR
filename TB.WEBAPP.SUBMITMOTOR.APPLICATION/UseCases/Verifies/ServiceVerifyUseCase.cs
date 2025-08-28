using Microsoft.Extensions.Logging;
using TB.WEBAPP.SUBMITMOTOR.APPLICATION.DTOs.Requests.Services;
using TB.WEBAPP.SUBMITMOTOR.APPLICATION.Interfaces.Notifications;
using TB.WEBAPP.SUBMITMOTOR.APPLICATION.Interfaces.Verifies;

namespace TB.WEBAPP.SUBMITMOTOR.APPLICATION.UseCases.Verifies
{
    public class ServiceVerifyUseCase(ILogger<ServiceVerifyUseCase> logger, INotificationService notificationService) : IServiceVerifyUseCase
    {
        private readonly ILogger<ServiceVerifyUseCase> _logger = logger;
        private readonly INotificationService _notificationService = notificationService;
        public async Task<bool> SendVerificationEmailInstallmentAsync(string email, string templateEmail, string logoUrl, string hrefLink)
        {
            try
            {
                string html = await File.ReadAllTextAsync(templateEmail); // อ่านไฟล์ HTML template

                // แทนที่ตัวแปรใน HTML ด้วยค่าจริง
                html = html.Replace("{{url_logo}}", logoUrl);
                html = html.Replace("{{href_link}}", hrefLink);

                // ส่งอีเมล
                var response = await _notificationService.SendMailTBroker(new SendMailTBRequest
                {
                    ToRecipients =
                    [
                        new ToRecipient
                    {
                        EmailAddress = new EmailAddress
                        {
                            Address = email
                        }
                    }
                    ],
                    Subject = "กรุณายืนยันตัวตนเพื่อดำเนินการผ่อนชำระ",
                    Body = new Body
                    {
                        ContentType = "html",
                        Content = html
                    },
                    Attachments = [],
                    SaveToSentItems = true
                });

                return response.Code == 200;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error sending verification email for installment.");
                return false;
            }
        }

        public async Task<bool> SendNotificationEmailCoverNoteAsync(string email, string templateEmail, string logoUrl, string applicationNo, string fileByte)
        {
            try
            {
                string html = await File.ReadAllTextAsync(templateEmail); // อ่านไฟล์ HTML template

                // แทนที่ตัวแปรใน HTML ด้วยค่าจริง
                html = html.Replace("{{url_logo}}", logoUrl);

                // ส่งอีเมล
                var response = await _notificationService.SendMailTBroker(new SendMailTBRequest
                {
                    ToRecipients =
                    [
                        new ToRecipient
                        {
                            EmailAddress = new EmailAddress
                            {
                                Address = email
                            }
                        }
                    ],
                    Subject = "กรุณายืนยันตัวตนเพื่อดำเนินการผ่อนชำระ",
                    Body = new Body
                    {
                        ContentType = "html",
                        Content = html
                    },
                    Attachments =
                    [
                        new Attachments
                        {
                            Name = $"{applicationNo}.pdf",
                            ContentType = "application/pdf",
                            ContentBytes = fileByte
                        }
                    ],
                    SaveToSentItems = true
                });

                return response.Code == 200;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error sending verification email for covernote.");
                return false;
            }
        }
    }
}
