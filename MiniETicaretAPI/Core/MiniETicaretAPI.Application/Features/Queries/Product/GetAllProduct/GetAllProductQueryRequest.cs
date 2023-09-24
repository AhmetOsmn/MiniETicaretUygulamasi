using MediatR;
using MiniETicaretAPI.Application.RequestParameters;

namespace MiniETicaretAPI.Application.Features.Queries.Product.GetAllProduct
{
    public class GetAllProductQueryRequest : IRequest<GetAllProductQueryResponse>
    {
        public int Page { get; set; } = 0;
        public int Size { get; set; } = 5;
    }
}
