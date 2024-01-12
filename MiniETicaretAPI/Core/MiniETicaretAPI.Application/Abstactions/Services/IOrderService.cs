using MiniETicaretAPI.Application.Dtos.Order;

namespace MiniETicaretAPI.Application.Abstactions.Services
{
    public interface IOrderService
    {
        Task CreateOrderAsync(CreateOrderDto createOrderDto);
        Task<ListOrder> GetAllOrdersAsync(int page, int size);
        Task<SingleOrder> GetOrderByIdAsync(string id);
        Task<Dtos.Order.CompletedOrder?> CompleteOrderAsync(string id);
    }
}
