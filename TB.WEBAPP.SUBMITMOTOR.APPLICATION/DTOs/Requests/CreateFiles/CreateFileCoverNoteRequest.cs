namespace TB.WEBAPP.SUBMITMOTOR.APPLICATION.DTOs.Requests.CreateFiles
{
    public class CreateFileCoverNoteRequest
    {
        public string TransactionId { get; set; } = string.Empty;
        public bool IsPassword { get; set; } = false;
    }
}