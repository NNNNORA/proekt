using proekt.Models;

namespace proekt.Data.DataAccess.Interfaces
{
    public interface IProductRepository : IRepository<Product>
    {
        Task AddAsync(Product product);
        Task UpdateAsync(Product product);
        Task DeleteAsync(object product);
        Task<string?> GetAllAsync();
        Task<Product> GetByIdAsync(int id);
       
        
        IEnumerable<Product> GetProductsByCategory(int categoryId);
    }
}
