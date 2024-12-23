using proekt.Models;
using System.Collections.Generic;
namespace proekt.Data.DataAccess.Interfaces
{
    public interface ICategoryRepository : IRepository<Category>
    {
        Category GetCategoryWithProducts(int categoryId);
    }
}
