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
            // �������� ������ �������� �������, � �� ���� ������
            return View(cart.Items);
        }

        public IActionResult AddToCart(int id, int quantity = 1)
        {
            // �������� ������� �� ID
            var product = GetProductById(id);

            if (product != null)
            {
                // ��������� ������� � ������� ����� CartService
                _cartService.AddToCart(product, quantity);

                Console.WriteLine($"Product {product.Name} added to cart.");
            }
            else
            {
                Console.WriteLine($"Product with ID {id} not found.");
            }

            // ���������� ������������ � �������� �������
            return RedirectToAction("Index", "Cart");
        }
        [HttpPost]
        public IActionResult RemoveFromCart(int id)
        {
            _cartService.RemoveFromCart(id);
            return RedirectToAction("Index");
        }

        // ����� ��� ��������� �������� �� ���� ������
        private Product GetProductById(int id)
        {
            // ��������� ������� �� ���� ������ �� ��� ID
            return _context.Products.FirstOrDefault(p => p.Id == id);
        }
    }
}