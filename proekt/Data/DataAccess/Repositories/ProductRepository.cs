using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using proekt.Data.DataAccess.Repositories;
using proekt.Data.DataAccess.Interfaces;
using proekt.Models;

namespace proekt.Data.Repositories
{
    public class ProductRepository : Repository<Product>, IProductRepository
    {
        private readonly ApplicationDbContext _context;

        public ProductRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        // Пример метода для получения всех продуктов в наличии
        public IEnumerable<Product> GetAvailableProducts()
        {
            return _context.Products.Where(p => p.InStock).ToList();
        }

        // Пример метода для поиска продукта по категории
        public IEnumerable<Product> GetProductsByCategory(int categoryId)
        {
            return _context.Products
                .Include(p => p.Category)
                .Where(p => p.CategoryId == categoryId)
                .ToList();
        }

        public async Task AddAsync(Product product)
        {
            await _context.Products.AddAsync(product);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Product product)
        {
            _context.Products.Update(product);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(object product)
        {
            if (product is Product productToRemove)
            {
                _context.Products.Remove(productToRemove);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<string?> GetAllAsync()
        {
            var products = await _context.Products.ToListAsync();
            return products != null ? string.Join(", ", products.Select(p => p.Name)) : null;
        }

        public async Task<Product> GetByIdAsync(int id)
        {
            return await _context.Products.FindAsync(id);
        }
    }
}
