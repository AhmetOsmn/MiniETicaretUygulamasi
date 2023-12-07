using MiniETicaretAPI.Application.Abstactions.Services;
using MiniETicaretAPI.Application.Dtos.Order;
using MiniETicaretAPI.Application.Repositories;

namespace MiniETicaretAPI.Persistence.Services
{
    public class OrderService : IOrderService
    {
        private readonly IOrderWriteRepository _orderWriteRepository;

        public OrderService(IOrderWriteRepository orderWriteRepository)
        {
            _orderWriteRepository = orderWriteRepository;
        }

        public async Task CreateOrderAsync(CreateOrderDto createOrderDto)
        {
            await _orderWriteRepository.AddAsync(new()
            {
                Address = createOrderDto.Address,
                Id = Guid.Parse(createOrderDto.BasketId),
                Description = createOrderDto.Description
            });

            await _orderWriteRepository.SaveAsync();
        }
    }
}
