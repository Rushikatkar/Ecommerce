using DAL.DTOs;
using DAL.Models.Entities;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BAL.Services.ProductService
{
    public interface IProductService
    {
        Task<IEnumerable<Product>> GetAllProductsAsync();
        Task<Product> GetProductByIdAsync(int productId);
        Task<Product> AddProductAsync(CreateProductDTO createProductDto);
        Task<Product> UpdateProductAsync(int id, UpdateProductDTO updateProductDto);
        Task DeleteProductAsync(int productId);
    }
}
