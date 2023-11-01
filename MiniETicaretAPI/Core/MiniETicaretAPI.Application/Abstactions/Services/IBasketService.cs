using MiniETicaretAPI.Application.ViewModels.Baskets;
using MiniETicaretAPI.Domain.Entities;

namespace MiniETicaretAPI.Application.Abstactions.Services
{
    public interface IBasketService
    {
        public Task<List<BasketItem>> GetBasketItemAsync();
        public Task AddItemToBasketAsync(CreateBasketItemVM createBasketItemVM);
        public Task UpdateQuantityAsync(UpdateBasketItemVM updateBasketItemVM);
        public Task RemoveBasketItemAsync(string basketItemId);
    }
}
