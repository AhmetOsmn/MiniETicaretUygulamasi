﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MiniETicaretAPI.Application.Repositories;

namespace MiniETicaretAPI.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductWriteRepository _productWriteRepository;
        private readonly IProductReadRepository _productReadRepository;

        public ProductsController(IProductWriteRepository productWriteRepository, IProductReadRepository productReadRepository)
        {
            _productWriteRepository = productWriteRepository;
            _productReadRepository = productReadRepository;
        }

        [HttpGet]
        public async Task Get()
        {
            //await _productWriteRepository.AddRangeAsync(new()
            //{
            //    new() { Id = Guid.NewGuid(), Name = "Product 1", CreatedDate = DateTime.UtcNow, Stock = 10, Price = 100 },
            //    new() { Id = Guid.NewGuid(), Name = "Product 2", CreatedDate = DateTime.UtcNow, Stock = 20, Price = 200 },
            //    new() { Id = Guid.NewGuid(), Name = "Product 3", CreatedDate = DateTime.UtcNow, Stock = 30, Price = 300 },
            //    new() { Id = Guid.NewGuid(), Name = "Product 4", CreatedDate = DateTime.UtcNow, Stock = 40, Price = 400 }
            //});
            //var count = await _productWriteRepository.SaveAsync();

            var product = await _productReadRepository.GetByIdAsync("f1224beb-df33-46c7-b2a6-24ccf9f71092", false);
            product.Name = "Osman";
            await _productWriteRepository.SaveAsync();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(string id)
        {
            var product = await _productReadRepository.GetByIdAsync(id);
            return Ok(product);
        }


    }
}
