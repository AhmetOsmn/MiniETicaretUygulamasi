using MediatR;
using Microsoft.EntityFrameworkCore;
using MiniETicaretAPI.Application.Repositories;

namespace MiniETicaretAPI.Application.Features.Commands.ProductImageFile.DeleteProductImage
{
    public class DeleteProductImageCommandHandler : IRequestHandler<DeleteProductImageCommandRequest>
    {
        private readonly IProductReadRepository _productReadRepository;
        private readonly IProductWriteRepository _productWriteRepository;

        public DeleteProductImageCommandHandler(IProductReadRepository productReadRepository, IProductWriteRepository productWriteRepository)
        {
            _productReadRepository = productReadRepository;
            _productWriteRepository = productWriteRepository;
        }

        public async Task<Unit> Handle(DeleteProductImageCommandRequest request, CancellationToken cancellationToken)
        {
            Domain.Entities.Product? product = await _productReadRepository.Table
                .Include(p => p.ProductImageFiles)
                .FirstOrDefaultAsync(p => p.Id == Guid.Parse(request.ProductId));

            Domain.Entities.ProductImageFile? productImageFile = product.ProductImageFiles.FirstOrDefault(p => p.Id == Guid.Parse(request.ImageId));
            
            product.ProductImageFiles.Remove(productImageFile);
            
            await _productWriteRepository.SaveAsync();

            return Unit.Value;
        }
    }
}
