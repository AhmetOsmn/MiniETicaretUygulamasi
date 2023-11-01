using MediatR;
using Microsoft.EntityFrameworkCore;
using MiniETicaretAPI.Application.Repositories;

namespace MiniETicaretAPI.Application.Features.Commands.ProductImageFile.ChangeShowcaseProductImage
{
    public class ChangeShowcaseProductImageCommandHandler : IRequestHandler<ChangeShowcaseProductImageCommandRequest>
    {
        private readonly IProductImageFileWriteRepository _productImageFileWriteRepository;

        public ChangeShowcaseProductImageCommandHandler(IProductImageFileWriteRepository productImageFileWriteRepository)
        {
            _productImageFileWriteRepository = productImageFileWriteRepository;
        }

        public async Task<Unit> Handle(ChangeShowcaseProductImageCommandRequest request, CancellationToken cancellationToken)
        {
            var query = _productImageFileWriteRepository.Table.Include(p => p.Products).SelectMany(p => p.Products, (pif, p) => new
            {
                pif,
                p
            });

            var image = await query.FirstOrDefaultAsync(p => p.p.Id == Guid.Parse(request.ProductId) && p.pif.Showcase);

            if(image != null) image.pif.Showcase = false;

            var data = await query.FirstOrDefaultAsync(p => p.pif.Id == Guid.Parse(request.ImageId));

            if (data != null)  data.pif.Showcase = true;

            await _productImageFileWriteRepository.SaveAsync();

            return Unit.Value;
        }
    }
}
