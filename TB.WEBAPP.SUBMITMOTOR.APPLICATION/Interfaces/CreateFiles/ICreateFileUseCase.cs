using TB.WEBAPP.SUBMITMOTOR.APPLICATION.DTOs.Requests.CreateFiles;

namespace TB.WEBAPP.SUBMITMOTOR.APPLICATION.Interfaces.CreateFiles
{
    public interface ICreateFileUseCase
    {
        Task<byte[]> CreateFileContactInstallment(CreateFileContractInstallmentRequest request);

        Task<byte[]> CreateFileCoverNote(CreateFileCoverNoteRequest request);
    }
}