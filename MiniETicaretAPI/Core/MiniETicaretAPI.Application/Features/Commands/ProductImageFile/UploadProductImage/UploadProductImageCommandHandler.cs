using MediatR;
using MiniETicaretAPI.Application.Abstactions.Storage;
using MiniETicaretAPI.Application.Repositories;

namespace MiniETicaretAPI.Application.Features.Commands.ProductImageFile.UploadProductImage
{
    public class UploadProductImageCommandHandler : IRequestHandler<UploadProductImageCommandRequest>
    {
        private readonly IStorageService _storageService;
        private readonly IProductReadRepository _productReadRepository;
        private readonly IProductImageFileWriteRepository _productImageFileWriteRepository;

        public UploadProductImageCommandHandler(IStorageService storageService,
                                                IProductReadRepository productReadRepository,
                                                IProductImageFileWriteRepository productImageFileWriteRepository)
        {
            _storageService = storageService;
            _productReadRepository = productReadRepository;
            _productImageFileWriteRepository = productImageFileWriteRepository;
        }

        public async Task<Unit> Handle(UploadProductImageCommandRequest request, CancellationToken cancellationToken)
        {
            List<(string fileName, string pathOrContainerName)> datas = await _storageService.UploadAsync("product-images", request.Files);
            Domain.Entities.Product product = await _productReadRepository.GetByIdAsync(request.Id);

            await _productImageFileWriteRepository.AddRangeAsync(datas.Select(data => new Domain.Entities.ProductImageFile()
            {
                FileName = data.fileName,
                Path = data.pathOrContainerName,
                Storage = _storageService.StorageName,
                Products = new List<Domain.Entities.Product>() { product }
            }).ToList());

            await _productImageFileWriteRepository.SaveAsync();

            return Unit.Value;
        }
    }
}
