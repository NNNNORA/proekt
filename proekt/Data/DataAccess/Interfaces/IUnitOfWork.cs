using proekt.Data.DataAccess.Repositories;
using proekt.Models;

namespace proekt.Data.DataAccess.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        Repository<User> Users { get; }
        Repository<Product> Products { get; }
        Repository<Recipe> Recipes { get; }
        Repository<Order> Orders { get; }
        Repository<Cart> Carts { get; }
        Repository<OrderItem> OrderItems { get; }
        Repository<Review> Reviews { get; }
        Repository<Category> Categories { get; }
        Repository<Ingredient> Ingredients { get; }
        Repository<ProductIngredient> ProductIngredients { get; }
        void Save();
    }
}
