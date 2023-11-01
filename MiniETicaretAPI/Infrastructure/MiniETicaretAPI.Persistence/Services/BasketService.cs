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
        private readonly IBasketItemWriteRepository _basketItemWriteRepository;
        private readonly IBasketItemReadRepository _basketItemReadRepository;
        private readonly UserManager<AppUser> _userManager;

        public BasketService(IHttpContextAccessor httpContextAccessor,
                             UserManager<AppUser> userManager,
                             IOrderReadRepository orderReadRepository,
                             IBasketWriteRepository basketWriteRepository,
                             IBasketItemWriteRepository basketItemWriteRepository,
                             IBasketItemReadRepository basketItemReadRepository)
        {
            _httpContextAccessor = httpContextAccessor;
            _userManager = userManager;
            _orderReadRepository = orderReadRepository;
            _basketWriteRepository = basketWriteRepository;
            _basketItemWriteRepository = basketItemWriteRepository;
            _basketItemReadRepository = basketItemReadRepository;
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
            if(basket != null)
            {

            }
        }

        public Task<List<BasketItem>> GetBasketItemAsync()
        {
            throw new NotImplementedException();
        }

        public Task RemoveBasketItemAsync(string basketItemId)
        {
            throw new NotImplementedException();
        }

        public Task UpdateQuantityAsync(UpdateBasketItemVM updateBasketItemVM)
        {
            throw new NotImplementedException();
        }
    }
}
