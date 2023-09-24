using MediatR;
using Microsoft.AspNetCore.Http;

namespace MiniETicaretAPI.Application.Features.Commands.ProductImageFile.UploadProductImage
{
    public class UploadProductImageCommandRequest : IRequest
    {
        public string Id { get; set; }
        public IFormFileCollection? Files { get; set; }
    }
}
