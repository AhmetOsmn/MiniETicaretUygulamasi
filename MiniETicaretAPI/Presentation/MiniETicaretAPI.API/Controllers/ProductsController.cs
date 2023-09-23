using Microsoft.AspNetCore.Mvc;
using MiniETicaretAPI.Application.Abstactions.Storage;
using MiniETicaretAPI.Application.Abstactions.Storage.Local;
using MiniETicaretAPI.Application.Repositories;
using MiniETicaretAPI.Application.RequestParameters;
using MiniETicaretAPI.Application.ViewModels.Products;
using MiniETicaretAPI.Domain.Entities;
using System.Net;

namespace MiniETicaretAPI.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductWriteRepository _productWriteRepository;
        private readonly IProductReadRepository _productReadRepository;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IFileWriteRepository _fileWriteRepository;
        private readonly IFileReadRepository _fileReadRepository;
        private readonly IProductImageFileReadRepository _productImageFileReadRepository;
        private readonly IProductImageFileWriteRepository _productImageFileWriteRepository;
        private readonly IInvoiceFileReadRepository _invoiceFileReadRepository;
        private readonly IInvoiceFileWriteRepository _invoiceFileWriteRepository;
        private readonly IStorageService _storageService;

        public ProductsController(
            IProductWriteRepository productWriteRepository,
            IProductReadRepository productReadRepository,
            IWebHostEnvironment webHostEnvironment,
            IFileWriteRepository fileWriteRepository,
            IFileReadRepository fileReadRepository,
            IProductImageFileReadRepository productImageFileReadRepository,
            IProductImageFileWriteRepository productImageFileWriteRepository,
            IInvoiceFileReadRepository invoiceFileReadRepository,
            IInvoiceFileWriteRepository invoiceFileWriteRepository,
            IStorageService storageService
        )
        {
            _productWriteRepository = productWriteRepository;
            _productReadRepository = productReadRepository;
            this._webHostEnvironment = webHostEnvironment;
            _fileWriteRepository = fileWriteRepository;
            _fileReadRepository = fileReadRepository;
            _productImageFileReadRepository = productImageFileReadRepository;
            _productImageFileWriteRepository = productImageFileWriteRepository;
            _invoiceFileReadRepository = invoiceFileReadRepository;
            _invoiceFileWriteRepository = invoiceFileWriteRepository;
            _storageService = storageService;
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] Pagination pagination)
        {
            var totalCount = _productReadRepository.GetAll(false).Count();
            var products = _productReadRepository.GetAll(false).Skip(pagination.Page * pagination.Size).Take(pagination.Size).Select(p => new
            {
                p.Id,
                p.Name,
                p.Stock,
                p.Price,
                p.CreatedDate,
                p.UpdatedDate
            }).ToList();

            return Ok(new
            {
                totalCount,
                products
            });
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(string id)
        {
            return Ok(await _productReadRepository.GetByIdAsync(id, false));
        }

        [HttpPost]
        public async Task<IActionResult> Post(CreateProductVM model)
        {
            await _productWriteRepository.AddAsync(new()
            {
                Name = model.Name,
                Price = model.Price,
                Stock = model.Stock
            });
            await _productWriteRepository.SaveAsync();
            return StatusCode((int)HttpStatusCode.Created);
        }

        [HttpPut]
        public async Task<IActionResult> Put(UpdateProductVM model)
        {
            Product product = await _productReadRepository.GetByIdAsync(model.Id);
            product.Name = model.Name;
            product.Price = model.Price;
            product.Stock = model.Stock;
            await _productWriteRepository.SaveAsync();
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            await _productWriteRepository.RemoveAsync(id);
            await _productWriteRepository.SaveAsync();
            return Ok();
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> Upload()
        {
            var datas = await _storageService.UploadAsync("resource/files", Request.Form.Files);

            // product image file test
            await _productImageFileWriteRepository.AddRangeAsync(datas.Select(data => new ProductImageFile()
            {
                FileName = data.fileName,
                Path = data.pathOrContainerName,
                Storage = _storageService.StorageName
            }).ToList());

            await _productImageFileWriteRepository.SaveAsync();

            // invoice file test
            //await _invoiceFileWriteRepository.AddRangeAsync(datas.Select(data => new InvoiceFile()
            //{
            //    FileName = data.fileName,
            //    Path = data.path,
            //    Price = new Random().Next()
            //}).ToList());

            // file test
            //await _fileWriteRepository.AddRangeAsync(datas.Select(data => new Domain.Entities.File()
            //{
            //    FileName = data.fileName,
            //    Path = data.path
            //}).ToList());

            //await _fileWriteRepository.SaveAsync();

            // read testleri
            //var data1 = _fileReadRepository.GetAll(false); // bütün file'lar gelir
            //var data2 = _productImageFileReadRepository.GetAll(false); // sadece product image file'lar gelir
            //var data3 = _invoiceFileReadRepository.GetAll(false); // sadece invoice file'lar gelir

            return Ok();
        }
    }
}
