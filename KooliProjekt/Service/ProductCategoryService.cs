using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using KooliProjekt.Data;

namespace KooliProjekt.Service
{
    public class ProductCategoryService : IProductCategoryService
    {
        private readonly ApplicationDbContext _context;

        public ProductCategoryService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<ProductCategory>> GetAllProductCategoriesAsync()
        {
            return await _context.ProductCategories
                .Include(pc => pc.Products)
                .ToListAsync();
        }

        public async Task<ProductCategory> GetProductCategoryByIdAsync(int id)
        {
            return await _context.ProductCategories
                .Include(pc => pc.Products)
                .FirstOrDefaultAsync(pc => pc.Id == id);
        }

        public async Task CreateProductCategoryAsync(ProductCategory productCategory)
        {
            _context.Add(productCategory);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateProductCategoryAsync(ProductCategory productCategory)
        {
            _context.Update(productCategory);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteProductCategoryAsync(int id)
        {
            var productCategory = await _context.ProductCategories.FindAsync(id);
            if (productCategory != null)
            {
                _context.ProductCategories.Remove(productCategory);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<bool> ProductCategoryExistsAsync(int id)
        {
            return await _context.ProductCategories.AnyAsync(e => e.Id == id);
        }
    }
} 