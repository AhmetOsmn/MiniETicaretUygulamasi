using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MiniETicaretAPI.Application.Repositories;
using MiniETicaretAPI.Domain.Entities;

namespace MiniETicaretAPI.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductWriteRepository _productWriteRepository;
        private readonly IProductReadRepository _productReadRepository;

        private readonly IOrderWriteRepository _orderWriteRepository;
        private readonly IOrderReadRepository _orderReadRepository;

        private readonly ICustomerWriteRepository _customerWriteRepository;



        public ProductsController(
                                    IProductWriteRepository productWriteRepository,
                                    IProductReadRepository productReadRepository,
                                    IOrderWriteRepository orderWriteRepository,
                                    IOrderReadRepository orderReadRepository,
                                    ICustomerWriteRepository customerWriteRepository
                                 )
        {
            _productWriteRepository = productWriteRepository;
            _productReadRepository = productReadRepository;
            _orderWriteRepository = orderWriteRepository;
            _orderReadRepository = orderReadRepository;
            _customerWriteRepository = customerWriteRepository;
        }

        [HttpGet]
        public async Task<IActionResult>Get()
        {
            return Ok("Merhaba");
        }

    }
}
