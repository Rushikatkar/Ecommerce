using DAL.DTOs;
using DAL.Models.Entities;
using DAL.Repositories.ProductRepo;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace BAL.Services.ProductService
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;

        public ProductService(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<IEnumerable<Product>> GetAllProductsAsync()
        {
            return await _productRepository.GetAllProductsAsync();
        }

        public async Task<Product> GetProductByIdAsync(int productId)
        {
            return await _productRepository.GetProductByIdAsync(productId);
        }

        public async Task<Product> AddProductAsync(CreateProductDTO createProductDto)
        {
            var product = new Product
            {
                Name = createProductDto.Name,
                Description = createProductDto.Description,
                Price = createProductDto.Price,
                Stock = createProductDto.Stock,
                CategoryId = createProductDto.CategoryId,
            };

            if (createProductDto.ImageFile != null)
            {
                // Get the path to the wwwroot folder
                var wwwRootPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot");

                // Ensure the Images folder exists
                var imagesFolderPath = Path.Combine(wwwRootPath, "Images");
                if (!Directory.Exists(imagesFolderPath))
                {
                    Directory.CreateDirectory(imagesFolderPath);
                }

                // Construct the full file path
                var filePath = Path.Combine(imagesFolderPath, createProductDto.ImageFile.FileName);

                // Save the file to the Images folder
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await createProductDto.ImageFile.CopyToAsync(stream);
                }

                // Store the relative path in the database
                product.ImageUrl = Path.Combine("Images", createProductDto.ImageFile.FileName).Replace("\\", "/"); // Use forward slashes for URLs
            }

            // Add the product to the database
            await _productRepository.AddProductAsync(product);
            return product;
        }

        public async Task<Product> UpdateProductAsync(int id, UpdateProductDTO updateProductDto)
        {
            var existingProduct = await _productRepository.GetProductByIdAsync(id);
            if (existingProduct == null) return null;

            // Update the product details
            existingProduct.Name = updateProductDto.Name;
            existingProduct.Description = updateProductDto.Description;
            existingProduct.Price = updateProductDto.Price;
            existingProduct.Stock = updateProductDto.Stock;
            existingProduct.CategoryId = updateProductDto.CategoryId;

            if (updateProductDto.ImageFile != null)
            {
                var imagesFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Images");

                if (!Directory.Exists(imagesFolder))
                {
                    Directory.CreateDirectory(imagesFolder);
                }
                var filePath = Path.Combine(imagesFolder, updateProductDto.ImageFile.FileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await updateProductDto.ImageFile.CopyToAsync(stream);
                }
                existingProduct.ImageUrl = Path.Combine("Images", updateProductDto.ImageFile.FileName).Replace("\\", "/");
            }


            // Save the updated product to the database
            await _productRepository.UpdateProductAsync(existingProduct);
            return existingProduct;
        }


        public async Task DeleteProductAsync(int productId)
        {
            await _productRepository.DeleteProductAsync(productId);
        }
    }
}
