using TB.WEBAPP.SUBMITMOTOR.APPLICATION.DTOs.Requests.CreateFiles;
using TB.WEBAPP.SUBMITMOTOR.APPLICATION.Interfaces;
using TB.WEBAPP.SUBMITMOTOR.APPLICATION.Interfaces.CreateFiles;

namespace TB.WEBAPP.SUBMITMOTOR.APPLICATION.UseCases.CreateFiles
{
    public class CreateFileUseCase(IApiClientService apiClientService) : ICreateFileUseCase
    {
        private readonly IApiClientService _apiClientService = apiClientService;
        private readonly string _createFileService = "CreateFileService";

        public async Task<byte[]> CreateFileContactInstallment(CreateFileContractInstallmentRequest request) 
        {
            var result = await _apiClientService.PostFileAsync(_createFileService, $"api/create/file/contract/installment", request);
            return result;
        }

        public async Task<byte[]> CreateFileCoverNote(CreateFileCoverNoteRequest request)
        {
            var result = await _apiClientService.PostFileAsync(_createFileService, $"api/create/file/notification/covernote", request);
            return result;
        }
    }
}
