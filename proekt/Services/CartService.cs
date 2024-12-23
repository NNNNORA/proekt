using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using proekt.Models; // Убедитесь, что здесь указано правильное пространство имен

namespace proekt.Services
{
    public class CartService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private const string CartSessionKey = "Cart";

        public CartService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor ?? throw new ArgumentNullException(nameof(httpContextAccessor));
        }

        // Метод получения корзины из сессии
        public Cart GetCart()
        {
            var session = _httpContextAccessor.HttpContext.Session;
            var cartJson = session.GetString(CartSessionKey);
            return string.IsNullOrEmpty(cartJson) ? new Cart() : JsonConvert.DeserializeObject<Cart>(cartJson);
        }

        // Метод сохранения корзины в сессию
        public void SaveCart(Cart cart)
        {
            var session = _httpContextAccessor.HttpContext.Session;
            var cartJson = JsonConvert.SerializeObject(cart);
            session.SetString(CartSessionKey, cartJson);
        }

        // Метод добавления товара в корзину
        public void AddToCart(Product product, int quantity)
        {
            if (product == null)
            {
                throw new ArgumentNullException(nameof(product));
            }

            var cart = GetCart();

            // Найти продукт в корзине
            var cartItem = cart.Items.FirstOrDefault(ci => ci.ProductId == product.Id);
            if (cartItem != null)
            {
                // Если продукт уже есть, увеличить количество
                cartItem.Quantity += quantity;
            }
            else
            {
                // Если продукта нет, добавить новый
                cart.Items.Add(new CartItem
                {
                    ProductId = product.Id,
                    Name = product.Name,
                    Price = product.Price,
                    Quantity = quantity
                });
            }

            SaveCart(cart); // Сохраняем изменения в корзине
        }

        // Метод удаления товара из корзины
        public void RemoveFromCart(int productId)
        {
            var cart = GetCart();
            cart.Items.RemoveAll(i => i.ProductId == productId);
            SaveCart(cart);
        }
    }
}
