using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MiniETicaretAPI.Application.Abstactions.Services;
using MiniETicaretAPI.Application.Consts;
using MiniETicaretAPI.Application.CustomAttributes;
using MiniETicaretAPI.Application.Enums;
using MiniETicaretAPI.Application.Features.Commands.Product.CreateProduct;
using MiniETicaretAPI.Application.Features.Commands.Product.DeleteProduct;
using MiniETicaretAPI.Application.Features.Commands.Product.UpdateProduct;
using MiniETicaretAPI.Application.Features.Commands.ProductImageFile.ChangeShowcaseProductImage;
using MiniETicaretAPI.Application.Features.Commands.ProductImageFile.DeleteProductImage;
using MiniETicaretAPI.Application.Features.Commands.ProductImageFile.UploadProductImage;
using MiniETicaretAPI.Application.Features.Queries.Product.GetAllProduct;
using MiniETicaretAPI.Application.Features.Queries.Product.GetProductById;
using MiniETicaretAPI.Application.Features.Queries.ProductImageFile.GetProductImages;
using System.Net;

namespace MiniETicaretAPI.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class ProductsController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IProductService _productService;

        public ProductsController(
            IMediator mediator, IProductService productService)
        {
            _mediator = mediator;
            _productService = productService;
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] GetAllProductQueryRequest getAllProductQueryRequest)
        {
            return Ok(await _mediator.Send(getAllProductQueryRequest));
        }

        [HttpGet("{Id}")]
        public async Task<IActionResult> Get([FromRoute] GetProductByIdQueryRequest getProductByIdQueryRequest)
        {
            return Ok(await _mediator.Send(getProductByIdQueryRequest));
        }

        [HttpPost]
        [Authorize(AuthenticationSchemes = "Admin")]
        [AuthorizeDefinition(Menu = AuthorizeDefinitionConstants.Product, Definition = "Create Product", Action = ActionType.Writing)]
        public async Task<IActionResult> Post(CreateProductCommandRequest createProductCommandRequest)
        {
            await _mediator.Send(createProductCommandRequest);
            return StatusCode((int)HttpStatusCode.Created);
        }

        [HttpPut]
        [Authorize(AuthenticationSchemes = "Admin")]
        [AuthorizeDefinition(Menu = AuthorizeDefinitionConstants.Product, Definition = "Update Product", Action = ActionType.Updating)]
        public async Task<IActionResult> Put(UpdateProductCommandRequest updateProductCommandRequest)
        {
            await _mediator.Send(updateProductCommandRequest);
            return NoContent();
        }

        [HttpDelete("{Id}")]
        [Authorize(AuthenticationSchemes = "Admin")]
        [AuthorizeDefinition(Menu = AuthorizeDefinitionConstants.Product, Definition = "Delete Product", Action = ActionType.Deleting)]
        public async Task<IActionResult> Delete([FromRoute] DeleteProductCommandRequest deleteProductCommandRequest)
        {
            await _mediator.Send(deleteProductCommandRequest);
            return NoContent();
        }

        [HttpPost("[action]")]
        [Authorize(AuthenticationSchemes = "Admin")]
        [AuthorizeDefinition(Menu = AuthorizeDefinitionConstants.Product, Definition = "Upload Image", Action = ActionType.Writing)]
        public async Task<IActionResult> Upload([FromQuery] UploadProductImageCommandRequest uploadProductImageCommandRequest)
        {
            uploadProductImageCommandRequest.Files = Request.Form.Files;
            await _mediator.Send(uploadProductImageCommandRequest);
            return NoContent();
        }

        [HttpGet("[action]/{Id}")]
        [Authorize(AuthenticationSchemes = "Admin")]
        [AuthorizeDefinition(Menu = AuthorizeDefinitionConstants.Product, Definition = "Get Product Images", Action = ActionType.Reading)]
        public async Task<IActionResult> GetProductImages([FromRoute] GetProductImagesQueryRequest getProductImagesQueryRequest)
        {
            GetProductImagesQueryResponse response = await _mediator.Send(getProductImagesQueryRequest);
            return Ok(response.ProductImageDetails);
        }

        [HttpDelete("[action]/{ProductId}")]
        [Authorize(AuthenticationSchemes = "Admin")]
        [AuthorizeDefinition(Menu = AuthorizeDefinitionConstants.Product, Definition = "Delete Product Image", Action = ActionType.Deleting)]
        public async Task<IActionResult> DeleteProductImage([FromRoute] DeleteProductImageCommandRequest deleteProductImageCommandRequest, [FromQuery] string imageId)
        {
            deleteProductImageCommandRequest.ImageId = imageId;
            await _mediator.Send(deleteProductImageCommandRequest);
            return NoContent();
        }

        [HttpGet("[action]")]
        [Authorize(AuthenticationSchemes = "Admin")]
        [AuthorizeDefinition(Menu = AuthorizeDefinitionConstants.Product, Definition = "Change Showcase", Action = ActionType.Updating)]
        public async Task<IActionResult> ChangeShowcase([FromQuery] ChangeShowcaseProductImageCommandRequest changeShowcaseProductImageCommandRequest)
        {
            await _mediator.Send(changeShowcaseProductImageCommandRequest);
            return NoContent();
        }

        [HttpGet("GenerateQrCode/{productId}")]               
        public async Task<IActionResult> GenerateQrCode([FromRoute] string productId)
        {            
            return File(await _productService.GenerateQRCodeToProductAsync(productId),"image/png");
        }
    }
}
