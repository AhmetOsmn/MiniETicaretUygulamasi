using MediatR;

namespace MiniETicaretAPI.Application.Features.Queries.ProductImageFile.GetProductImages
{
    public class GetProductImagesQueryRequest : IRequest<GetProductImagesQueryResponse>
    {
        public string Id { get; set; }
    }
}
