using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using proekt.Models;
using proekt.Services;

namespace proekt.Controllers
{
    public class CartController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly CartService _cartService;

        public CartController(ApplicationDbContext context, CartService cartService)
        {
            _context = context;
            _cartService = cartService;
        }

        public IActionResult Index()
        {
            var cart = _cartService.GetCart();
            // Передаем только элементы корзины, а не весь объект
            return View(cart.Items);
        }

        public IActionResult AddToCart(int id, int quantity = 1)
        {
            // Получаем продукт по ID
            var product = GetProductById(id);

            if (product != null)
            {
                // Добавляем продукт в корзину через CartService
                _cartService.AddToCart(product, quantity);

                Console.WriteLine($"Product {product.Name} added to cart.");
            }
            else
            {
                Console.WriteLine($"Product with ID {id} not found.");
            }

            // Возвращаем пользователя к странице корзины
            return RedirectToAction("Index", "Cart");
        }
        [HttpPost]
        public IActionResult RemoveFromCart(int id)
        {
            _cartService.RemoveFromCart(id);
            return RedirectToAction("Index");
        }

        // Метод для получения продукта из базы данных
        private Product GetProductById(int id)
        {
            // Извлекаем продукт из базы данных по его ID
            return _context.Products.FirstOrDefault(p => p.Id == id);
        }
    }
}