using Microsoft.EntityFrameworkCore;
using MiniETicaretAPI.Application.Abstactions.Services;
using MiniETicaretAPI.Application.Dtos.Order;
using MiniETicaretAPI.Application.Repositories;

namespace MiniETicaretAPI.Persistence.Services
{
    public class OrderService : IOrderService
    {
        private readonly IOrderWriteRepository _orderWriteRepository;
        private readonly IOrderReadRepository _orderReadRepository;

        public OrderService(IOrderWriteRepository orderWriteRepository, IOrderReadRepository orderReadRepository)
        {
            _orderWriteRepository = orderWriteRepository;
            _orderReadRepository = orderReadRepository;
        }

        public async Task CreateOrderAsync(CreateOrderDto createOrderDto)
        {
            await _orderWriteRepository.AddAsync(new()
            {
                Address = createOrderDto.Address,
                Id = Guid.Parse(createOrderDto.BasketId),
                Description = createOrderDto.Description,
                OrderCode = (new Random()).Next(100000, 999999).ToString(),
            });


            await _orderWriteRepository.SaveAsync();
        }

        public async Task<ListOrder> GetAllOrdersAsync(int page, int size)
        {
            var query = _orderReadRepository
                                .Table
                                .Include(o => o.Basket)
                                .ThenInclude(b => b.User)
                                .Include(o => o.Basket)
                                .ThenInclude(b => b.BasketItems)
                                .ThenInclude(bi => bi.Product);

            var data = query.Skip(page * size).Take(size);

            return new()
            {
                TotalOrderCount = await query.CountAsync(),
                Orders = await data.Select(o => new
                {
                    o.CreatedDate,
                    o.OrderCode,
                    TotalPrice = o.Basket.BasketItems.Sum(bi => bi.Quantity * bi.Product.Price),
                    o.Basket.User.UserName,
                }).ToListAsync()
            };
        }
    }
}
