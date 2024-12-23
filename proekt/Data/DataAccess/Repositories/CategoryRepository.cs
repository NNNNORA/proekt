using Microsoft.EntityFrameworkCore;
using proekt.Data.DataAccess.Repositories;
using proekt.Data.DataAccess.Interfaces;
using System.Collections.Generic;
using System.Linq;
using proekt.Models;

namespace proekt.Data.Repositories
{
    public class CategoryRepository : Repository<Category>, ICategoryRepository
    {
        private readonly ApplicationDbContext _context;

        public CategoryRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        // Пример метода для получения категории с продуктами
        public Category GetCategoryWithProducts(int categoryId)
        {
            return _context.Categories
                .Include(c => c.Products)
                .FirstOrDefault(c => c.Id == categoryId);
        }
    }
}
