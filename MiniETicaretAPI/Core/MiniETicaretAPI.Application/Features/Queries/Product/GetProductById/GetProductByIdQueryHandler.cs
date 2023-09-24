using MediatR;
using MiniETicaretAPI.Application.Repositories;

namespace MiniETicaretAPI.Application.Features.Queries.Product.GetProductById
{
    public class GetProductByIdQueryHandler : IRequestHandler<GetProductByIdQueryRequest, GetProductByIdQueryResponse>
    {
        private readonly IProductReadRepository _productReadRepository;

        public GetProductByIdQueryHandler(IProductReadRepository productReadRepository)
        {
            _productReadRepository = productReadRepository;
        }

        public async Task<GetProductByIdQueryResponse> Handle(GetProductByIdQueryRequest request, CancellationToken cancellationToken)
        {
            Domain.Entities.Product product = await _productReadRepository.GetByIdAsync(request.Id, false);

            return new()
            {
                Name = product.Name,
                Price = product.Price,
                Stock = product.Stock
            };
        }
    }
}
