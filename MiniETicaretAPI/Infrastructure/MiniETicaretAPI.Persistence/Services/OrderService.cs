using Microsoft.EntityFrameworkCore;
using MiniETicaretAPI.Application.Abstactions.Services;
using MiniETicaretAPI.Application.Dtos.Order;
using MiniETicaretAPI.Application.Repositories;
using MiniETicaretAPI.Domain.Entities;

namespace MiniETicaretAPI.Persistence.Services
{
    public class OrderService : IOrderService
    {
        private readonly IOrderWriteRepository _orderWriteRepository;
        private readonly IOrderReadRepository _orderReadRepository;
        private readonly ICompletedOrderWriteRepository _completedOrderWriteRepository;
        private readonly ICompletedOrderReadRepository _completedOrderReadRepository;

        public OrderService(IOrderWriteRepository orderWriteRepository,
                            IOrderReadRepository orderReadRepository,
                            ICompletedOrderWriteRepository completedOrderWriteRepository,
                            ICompletedOrderReadRepository completedOrderReadRepository)
        {
            _orderWriteRepository = orderWriteRepository;
            _orderReadRepository = orderReadRepository;
            _completedOrderWriteRepository = completedOrderWriteRepository;
            _completedOrderReadRepository = completedOrderReadRepository;
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

            var joinedData = (from order in query
                              join completedOrder in _completedOrderReadRepository.Table
                                 on order.Id equals completedOrder.OrderId into co
                              from _co in co.DefaultIfEmpty()
                              select new
                              {
                                  Id = order.Id,
                                  CreatedDate = order.CreatedDate,
                                  OrderCode = order.OrderCode,
                                  order.Basket,
                                  Completed = _co != null,
                              });



            return new()
            {
                TotalOrderCount = await query.CountAsync(),
                Orders = await joinedData.Select(o => new
                {
                    o.Id,
                    o.CreatedDate,
                    o.OrderCode,
                    TotalPrice = o.Basket.BasketItems.Sum(bi => bi.Quantity * bi.Product.Price),
                    o.Basket.User.UserName,
                    o.Completed
                }).ToListAsync()
            };
        }

        public async Task<SingleOrder> GetOrderByIdAsync(string id)
        {
            var data = _orderReadRepository.Table
                                                .Include(o => o.Basket)
                                                .ThenInclude(b => b.BasketItems)
                                                .ThenInclude(bi => bi.Product);

            var joinedData = await (from order in data
                              join completedOrder in _completedOrderReadRepository.Table
                                on order.Id equals completedOrder.OrderId into co
                              from _co in co.DefaultIfEmpty()                              
                              select new
                              {
                                  order.Id,
                                  order.CreatedDate,
                                  order.OrderCode,
                                  order.Basket,
                                  Completed = _co != null,
                                  order.Address,
                                  order.Description,
                              }).FirstOrDefaultAsync(o => o.Id == Guid.Parse(id)); ;


            return new()
            {
                Id = joinedData.Id.ToString(),
                Address = joinedData.Address,
                CreatedDate = joinedData.CreatedDate,
                Description = joinedData.Description,
                OrderCode = joinedData.OrderCode,
                BasketItems = joinedData.Basket.BasketItems.Select(bi => new
                {
                    ProductName = bi.Product.Name,
                    Quantity = bi.Quantity,
                    UnitPrice = bi.Product.Price,
                }).ToList(),
                Completed = joinedData.Completed,
            };

        }

        public async Task CompleteOrderAsync(string id)
        {
            Order order = await _orderReadRepository.GetByIdAsync(id);
            if (order != null)
            {
                await _completedOrderWriteRepository.AddAsync(new()
                {
                    OrderId = Guid.Parse(id),
                });
                await _completedOrderWriteRepository.SaveAsync();
            }
        }
    }
}
