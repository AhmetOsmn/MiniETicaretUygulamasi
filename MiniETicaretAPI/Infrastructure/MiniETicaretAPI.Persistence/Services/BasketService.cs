using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MiniETicaretAPI.Application.Abstactions.Services;
using MiniETicaretAPI.Application.Repositories;
using MiniETicaretAPI.Application.ViewModels.Baskets;
using MiniETicaretAPI.Domain.Entities;
using MiniETicaretAPI.Domain.Entities.Identity;

namespace MiniETicaretAPI.Persistence.Services
{
    public class BasketService : IBasketService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IOrderReadRepository _orderReadRepository;
        private readonly IBasketWriteRepository _basketWriteRepository;
        private readonly IBasketReadRepository _basketReadRepository;
        private readonly IBasketItemWriteRepository _basketItemWriteRepository;
        private readonly IBasketItemReadRepository _basketItemReadRepository;
        private readonly UserManager<AppUser> _userManager;

        public BasketService(IHttpContextAccessor httpContextAccessor,
                             UserManager<AppUser> userManager,
                             IOrderReadRepository orderReadRepository,
                             IBasketWriteRepository basketWriteRepository,
                             IBasketItemWriteRepository basketItemWriteRepository,
                             IBasketItemReadRepository basketItemReadRepository,
                             IBasketReadRepository basketReadRepository)
        {
            _httpContextAccessor = httpContextAccessor;
            _userManager = userManager;
            _orderReadRepository = orderReadRepository;
            _basketWriteRepository = basketWriteRepository;
            _basketItemWriteRepository = basketItemWriteRepository;
            _basketItemReadRepository = basketItemReadRepository;
            _basketReadRepository = basketReadRepository;
        }

        private async Task<Basket?> GetTargetBasket()
        {
            var username = _httpContextAccessor?.HttpContext?.User?.Identity?.Name;
            if (!string.IsNullOrEmpty(username))
            {
                AppUser? user = await _userManager.Users.Include(u => u.Baskets).FirstOrDefaultAsync(u => u.UserName == username);

                var _basket = from basket in user.Baskets
                              join order in _orderReadRepository.Table
                              on basket.Id equals order.Id into BasketOrders
                              from order in BasketOrders.DefaultIfEmpty()
                              select new
                              {
                                  Basket = basket,
                                  Order = order
                              };

                Basket? targetBasket = null;
                if (_basket.Any(x => x.Order is null))
                {
                    targetBasket = _basket.FirstOrDefault(b => b.Order is null)?.Basket;
                }
                else
                {
                    targetBasket = new();
                    user.Baskets.Add(targetBasket);
                }

                await _basketWriteRepository.SaveAsync();
                return targetBasket;
            }
            throw new Exception("Beklenmeyen bir hata ile karşılaşıldı!");
        }

        public async Task AddItemToBasketAsync(CreateBasketItemVM createBasketItemVM)
        {
            Basket? basket = await GetTargetBasket();
            if (basket != null)
            {
                BasketItem _basketItem = await _basketItemReadRepository.GetSingleAsync(x => x.BasketId == basket.Id && x.ProductId == Guid.Parse(createBasketItemVM.ProductId));

                if (_basketItem != null)
                {
                    _basketItem.Quantity++;
                }
                else
                {
                    await _basketItemWriteRepository.AddAsync(new()
                    {
                        BasketId = basket.Id,
                        ProductId = Guid.Parse(createBasketItemVM.ProductId),
                        Quantity = createBasketItemVM.Quantity
                    });
                }

                await _basketItemWriteRepository.SaveAsync();

            }
        }

        public async Task<List<BasketItem>> GetBasketItemsAsync()
        {
            var basket = await GetTargetBasket();

            Basket? result = await _basketReadRepository
                .Table
                .Include(b => b.BasketItems)
                .ThenInclude(x => x.Product)
                .FirstOrDefaultAsync(x => x.Id == basket.Id);

            return result.BasketItems.ToList();
        }

        public async Task RemoveBasketItemAsync(string basketItemId)
        {
            BasketItem basketItem = await _basketItemReadRepository.GetByIdAsync(basketItemId);
            if (basketItem != null)
            {
                _basketItemWriteRepository.Remove(basketItem);
                await _basketItemWriteRepository.SaveAsync();
            }
        }

        public async Task UpdateQuantityAsync(UpdateBasketItemVM updateBasketItemVM)
        {
            var basketItem = await _basketItemReadRepository.GetByIdAsync(updateBasketItemVM.BasketItemId);
            if (basketItem != null)
            {
                basketItem.Quantity = updateBasketItemVM.Quantity;
                await _basketItemWriteRepository.SaveAsync();
            }
        }
    }
}
