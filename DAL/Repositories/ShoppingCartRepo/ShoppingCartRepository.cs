using DAL.Models.Entities;
using DAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DAL.Repositories.ShoppingCartRepo
{
    public class ShoppingCartRepository : IShoppingCartRepository
    {
        private readonly ApplicationDbContext _context;

        public ShoppingCartRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        // Add an item to the shopping cart
        public async Task AddItemToCartAsync(int userId, int productId, int quantity)
        {
            var existingItem = await _context.ShoppingCarts
                                             .FirstOrDefaultAsync(sc => sc.UserId == userId && sc.ProductId == productId);

            if (existingItem != null)
            {
                // Update the quantity if the item already exists
                existingItem.Quantity += quantity;
                existingItem.CreatedAt = DateTime.Now;  // Update timestamp
            }
            else
            {
                var cartItem = new ShoppingCart
                {
                    UserId = userId,
                    ProductId = productId,
                    Quantity = quantity,
                    CreatedAt = DateTime.Now
                };
                await _context.ShoppingCarts.AddAsync(cartItem);
            }

            await _context.SaveChangesAsync();
        }

        // Get all items in the user's cart
        public async Task<List<ShoppingCart>> GetCartItemsAsync(int userId)
        {
            return await _context.ShoppingCarts
                                 .Where(sc => sc.UserId == userId)
                                 .Include(sc => sc.User) // Include user details
                                 .Include(sc => sc.Product) // Include product details
                                 .ThenInclude(p => p.Category) // Include category through product
                                 .ToListAsync();
        }


        // Remove an item from the cart
        public async Task RemoveItemFromCartAsync(int cartItemId)
        {
            var cartItem = await _context.ShoppingCarts.FindAsync(cartItemId);
            if (cartItem != null)
            {
                _context.ShoppingCarts.Remove(cartItem);
                await _context.SaveChangesAsync();
            }
        }

        // Update the quantity of a cart item
        public async Task UpdateCartItemQuantityAsync(int cartItemId, int newQuantity)
        {
            var cartItem = await _context.ShoppingCarts.FindAsync(cartItemId);
            if (cartItem != null)
            {
                cartItem.Quantity = newQuantity;
                await _context.SaveChangesAsync();
            }
        }
    }
}
