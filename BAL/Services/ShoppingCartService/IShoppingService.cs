using DAL.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL.Services.ShoppingCartService
{
    public interface IShoppingService
    {
        Task AddItemToCartAsync(int userId, int productId, int quantity);

        Task<List<ShoppingCart>> GetCartItemsAsync(int userId);

        Task RemoveItemFromCartAsync(int cartItemId);

        Task UpdateCartItemQuantityAsync(int cartItemId, int newQuantity);
    }
}
