using proekt.Data.DataAccess.Interfaces;
using proekt.Data.DataAccess.Repositories;
using proekt.Models;

namespace proekt.Data.DataAccess.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;
        private Repository<User> _userRepository;
        private Repository<Product> _productRepository;
        private Repository<Recipe> _recipeRepository;
        private Repository<Order> _orderRepository;
        private Repository<Cart> _cartRepository;
        private Repository<OrderItem> _orderItemRepository;
        private Repository<Review> _reviewRepository;
        private Repository<Category> _categoryRepository;
        private Repository<Ingredient> _ingredientRepository;
        private Repository<ProductIngredient> _productIngredientRepository;

        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
        }

        public Repository<User> Users => _userRepository ??= new Repository<User>(_context);
        public Repository<Product> Products => _productRepository ??= new Repository<Product>(_context);
        public Repository<Recipe> Recipes => _recipeRepository ??= new Repository<Recipe>(_context);
        public Repository<Order> Orders => _orderRepository ??= new Repository<Order>(_context);
        public Repository<Cart> Carts => _cartRepository ??= new Repository<Cart>(_context);
        public Repository<OrderItem> OrderItems => _orderItemRepository ??= new Repository<OrderItem>(_context);
        public Repository<Review> Reviews => _reviewRepository ??= new Repository<Review>(_context);
        public Repository<Category> Categories => _categoryRepository ??= new Repository<Category>(_context);
        public Repository<Ingredient> Ingredients => _ingredientRepository ??= new Repository<Ingredient>(_context);
        public Repository<ProductIngredient> ProductIngredients => _productIngredientRepository ??= new Repository<ProductIngredient>(_context);

        public void Save()
        {
            _context.SaveChanges();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
