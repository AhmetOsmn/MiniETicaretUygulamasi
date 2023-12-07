using MiniETicaretAPI.Application.Dtos.Order;

namespace MiniETicaretAPI.Application.Abstactions.Services
{
    public interface IOrderService
    {
        Task CreateOrderAsync(CreateOrderDto createOrderDto);
    }
}
