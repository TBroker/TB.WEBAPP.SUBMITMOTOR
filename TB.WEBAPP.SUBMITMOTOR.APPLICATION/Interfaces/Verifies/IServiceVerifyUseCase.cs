namespace TB.WEBAPP.SUBMITMOTOR.APPLICATION.Interfaces.Verifies
{
    public interface IServiceVerifyUseCase
    {
        Task<bool> SendVerificationEmailInstallmentAsync(string email, string templateEmail, string logoUrl, string hrefLink);

        Task<bool> SendNotificationEmailCoverNoteAsync(string email, string templateEmail, string logoUrl, string applicationNo, string fileByte);
    }
}