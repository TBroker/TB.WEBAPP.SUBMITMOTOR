using MediatR;
using TB.WEBAPP.SUBMITMOTOR.APPLICATION.Interfaces;

namespace TB.WEBAPP.SUBMITMOTOR.APPLICATION.Features.UploadFile
{
    public class UploadFileCommand(IFileContent file) : IRequest<bool>
    {
        public IFileContent File { get; } = file;
    }
}