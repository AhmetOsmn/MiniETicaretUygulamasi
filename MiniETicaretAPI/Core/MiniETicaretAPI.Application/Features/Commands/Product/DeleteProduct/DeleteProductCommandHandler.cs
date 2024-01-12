using MediatR;
using MiniETicaretAPI.Application.Repositories;

namespace MiniETicaretAPI.Application.Features.Commands.Product.DeleteProduct
{
    public class DeleteProductCommandHandler : IRequestHandler<DeleteProductCommandRequest>
    {
        private readonly IProductWriteRepository _productWriteRepository;

        public DeleteProductCommandHandler(IProductWriteRepository productWriteRepository)
        {
            _productWriteRepository = productWriteRepository;
        }

        public async Task<Unit> Handle(DeleteProductCommandRequest request, CancellationToken cancellationToken)
        {
            await _productWriteRepository.RemoveAsync(request.Id);
            await _productWriteRepository.SaveAsync();

            return Unit.Value;
        }
    }
}
