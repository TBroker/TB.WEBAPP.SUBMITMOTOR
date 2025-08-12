namespace TB.WEBAPP.SUBMITMOTOR.APPLICATION.DTOs.Requests.CreateFiles
{
    public class CreateFileContractInstallmentRequest
    {
        public string TransactionId { get; set; }
        public bool IsPassword { get; set; } = false;
    }
}
