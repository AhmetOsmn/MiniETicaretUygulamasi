using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MiniETicaretAPI.Application.Abstactions.Storage;
using MiniETicaretAPI.Application.Features.Commands.Product.CreateProduct;
using MiniETicaretAPI.Application.Features.Commands.Product.DeleteProduct;
using MiniETicaretAPI.Application.Features.Commands.ProductImageFile.DeleteProductImage;
using MiniETicaretAPI.Application.Features.Commands.ProductImageFile.UploadProductImage;
using MiniETicaretAPI.Application.Features.Queries.Product.GetAllProduct;
using MiniETicaretAPI.Application.Features.Queries.Product.GetProductById;
using MiniETicaretAPI.Application.Features.Queries.ProductImageFile.GetProductImages;
using MiniETicaretAPI.Application.Repositories;
using MiniETicaretAPI.Application.RequestParameters;
using MiniETicaretAPI.Application.ViewModels.Products;
using MiniETicaretAPI.Domain.Entities;
using System.Net;

namespace MiniETicaretAPI.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = "Admin")]
    public class ProductsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ProductsController(
            IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] GetAllProductQueryRequest getAllProductQueryRequest)
        {
            return Ok(await _mediator.Send(getAllProductQueryRequest));
        }

        [HttpGet("{Id}")]
        public async Task<IActionResult> GetAsync([FromRoute]GetProductByIdQueryRequest getProductByIdQueryRequest)
        {
            return Ok(await _mediator.Send(getProductByIdQueryRequest));
        }

        [HttpPost]
        public async Task<IActionResult> PostAsync(CreateProductCommandRequest createProductCommandRequest)
        {
            await _mediator.Send(createProductCommandRequest);
            return StatusCode((int)HttpStatusCode.Created);
        }

        [HttpPut]
        public async Task<IActionResult> Put(UpdateProductCommandRequest updateProductCommandRequest)
        {
            await _mediator.Send(updateProductCommandRequest);
            return NoContent();
        }

        [HttpDelete("{Id}")]
        public async Task<IActionResult> Delete([FromRoute] DeleteProductCommandRequest deleteProductCommandRequest)
        {
            await _mediator.Send(deleteProductCommandRequest);
            return NoContent();
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> Upload([FromQuery] UploadProductImageCommandRequest uploadProductImageCommandRequest)
        {
            uploadProductImageCommandRequest.Files = Request.Form.Files;
            await _mediator.Send(uploadProductImageCommandRequest);
            return NoContent();
        }

        [HttpGet("[action]/{Id}")]
        public async Task<IActionResult> GetProductImages([FromRoute] GetProductImagesQueryRequest getProductImagesQueryRequest)
        {
            GetProductImagesQueryResponse response = await _mediator.Send(getProductImagesQueryRequest);       
            return Ok(response.ProductImageDetails);
        }

        [HttpDelete("[action]/{ProductId}")]
        public async Task<IActionResult> DeleteProductImage([FromRoute] DeleteProductImageCommandRequest deleteProductImageCommandRequest, [FromQuery] string imageId)
        {
            deleteProductImageCommandRequest.ImageId = imageId;
            await _mediator.Send(deleteProductImageCommandRequest);
            return NoContent();
        }
    }
}
