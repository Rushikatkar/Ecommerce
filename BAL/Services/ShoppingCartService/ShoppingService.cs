using DAL.Models.Entities;
using DAL.Repositories.ShoppingCartRepo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL.Services.ShoppingCartService
{
    public class ShoppingService : IShoppingService
    {
        private readonly IShoppingCartRepository _shoppingCartRepository;

        public ShoppingService(IShoppingCartRepository shoppingCartRepository)
        {
            _shoppingCartRepository = shoppingCartRepository;
        }

        public async Task AddItemToCartAsync(int userId, int productId, int quantity)
        {
            await _shoppingCartRepository.AddItemToCartAsync(userId, productId, quantity);
        }

        public async Task<List<ShoppingCart>> GetCartItemsAsync(int userId)
        {
            return await _shoppingCartRepository.GetCartItemsAsync(userId);
        }

        public async Task RemoveItemFromCartAsync(int cartItemId)
        {
            await _shoppingCartRepository.RemoveItemFromCartAsync(cartItemId);
        }

        public async Task UpdateCartItemQuantityAsync(int cartItemId, int newQuantity)
        {
            await _shoppingCartRepository.UpdateCartItemQuantityAsync(cartItemId, newQuantity);
        }
    }
}
