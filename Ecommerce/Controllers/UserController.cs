using BAL.Services.UserService;
using DAL.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Presentation_Layer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        // Register a new user
        [HttpPost("register")]
        public async Task<IActionResult> RegisterUser(RegisterUserDTO registerUserDTO)
        {
            try
            {
                var user = await _userService.RegisterUserAsync(registerUserDTO);
                return Ok(new { message = "User registered successfully.", user.UserId, user.Email, user.Role });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        // Login a user
        [HttpPost("login")]
        public async Task<IActionResult> LoginUser(LoginUserDTO loginUserDTO)
        {
            try
            {
                var token = await _userService.LoginUserAsync(loginUserDTO);
                return Ok(new { message = "Login successful.", token });
            }
            catch (Exception ex)
            {
                return Unauthorized(new { message = ex.Message });
            }
        }

        // Update an existing user
        [Authorize]
        [HttpPut("update")]
        public async Task<IActionResult> UpdateUser(UpdateUserDTO updateUserDTO)
        {
            try
            {
                var updatedUser = await _userService.UpdateUserAsync(updateUserDTO);
                return Ok(new { message = "User updated successfully." });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        // Delete a user by ID
        [Authorize]
        [HttpDelete("delete/{userId}")]
        public async Task<IActionResult> DeleteUser(int userId)
        {
            try
            {
                var isDeleted = await _userService.DeleteUserAsync(userId);
                if (!isDeleted)
                    return NotFound(new { message = "User not found." });

                return Ok(new { message = "User deleted successfully." });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        // Get a user by ID
        [Authorize]
        [HttpGet("get/{userId}")]
        public async Task<IActionResult> GetUserById(int userId)
        {
            try
            {
                var user = await _userService.GetUserByIdAsync(userId);
                return Ok(new { message = "User fetched successfully.", user.Username, user.UserId, user.Email, user.Role });
            }
            catch (Exception ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }
    }
}
