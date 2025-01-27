using BAL.Services.ShoppingCartService;
using DAL.Models.Entities;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ECommerce.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ShoppingCartController : ControllerBase
    {
        private readonly IShoppingService _shoppingService;

        public ShoppingCartController(IShoppingService shoppingService)
        {
            _shoppingService = shoppingService;
        }

        [HttpPost("AddToCart")]
        public async Task<IActionResult> AddToCart(int userId, int productId, int quantity)
        {
            try
            {
                await _shoppingService.AddItemToCartAsync(userId, productId, quantity);
                return Ok(new { Message = "Item added to the cart successfully." });
            }
            catch (Exception ex)
            {
                return BadRequest(new { Error = ex.Message });
            }
        }

       
        [HttpGet("GetCartItems/{userId}")]
        public async Task<IActionResult> GetCartItems(int userId)
        {
            try
            {
                var cartItems = await _shoppingService.GetCartItemsAsync(userId);
                return Ok(cartItems);
            }
            catch (Exception ex)
            {
                return BadRequest(new { Error = ex.Message });
            }
        }

       
        [HttpDelete("RemoveFromCart/{cartItemId}")]
        public async Task<IActionResult> RemoveFromCart(int cartItemId)
        {
            try
            {
                await _shoppingService.RemoveItemFromCartAsync(cartItemId);
                return Ok(new { Message = "Item removed from the cart successfully." });
            }
            catch (Exception ex)
            {
                return BadRequest(new { Error = ex.Message });
            }
        }

       
        [HttpPut("UpdateCartItemQuantity")]
        public async Task<IActionResult> UpdateCartItemQuantity(int cartItemId, int newQuantity)
        {
            try
            {
                await _shoppingService.UpdateCartItemQuantityAsync(cartItemId, newQuantity);
                return Ok(new { Message = "Cart item quantity updated successfully." });
            }
            catch (Exception ex)
            {
                return BadRequest(new { Error = ex.Message });
            }
        }
    }
}
