using MediatR;
using TB.WEBAPP.SUBMITMOTOR.APPLICATION.Validators;

namespace TB.WEBAPP.SUBMITMOTOR.APPLICATION.Features.UploadFile
{
    public class UploadFileCommandHandler : IRequestHandler<UploadFileCommand, bool>
    {
        public async Task<bool> Handle(UploadFileCommand request, CancellationToken cancellationToken)
        {
            return await FileValidator.IsValidFileAsync(request.File);
        }
    }
}
