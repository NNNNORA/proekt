using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

using proekt.Data.DataAccess.Interfaces;
using Microsoft.EntityFrameworkCore;
using proekt.Models;
using System.Threading.Tasks;

namespace proekt.Controllers
{
    
    public class AdminController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IProductRepository _productRepository;
        private readonly ICategoryRepository _categoryRepository;

        public AdminController(
            UserManager<IdentityUser> userManager,
            RoleManager<IdentityRole> roleManager,
            IProductRepository productRepository,
            ICategoryRepository categoryRepository)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _productRepository = productRepository;
            _categoryRepository = categoryRepository;
        }

        // Главная страница панели администратора
        public IActionResult Index()
        {
            return View();
        }

        // Управление пользователями
        public async Task<IActionResult> ManageUsers()
        {
            var users = await _userManager.Users.ToListAsync();
            return View(users);
        }

        // Управление ролями
        public async Task<IActionResult> ManageRoles()
        {
            var roles = await _roleManager.Roles.ToListAsync();
            return View(roles);
        }

        // Управление продуктами
        public async Task<IActionResult> ManageProducts()
        {
            var products = await _productRepository.GetAllAsync();
            return View(products);
        }

        // Добавление нового продукта
        [HttpGet]
        public IActionResult AddProduct()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddProduct(Product product)
        {
            if (ModelState.IsValid)
            {
                await _productRepository.AddAsync(product);
                return RedirectToAction(nameof(ManageProducts));
            }
            return View(product);
        }

        // Редактирование продукта
        [HttpGet]
        public async Task<IActionResult> EditProduct(int id)
        {
            var product = await _productRepository.GetByIdAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }

        [HttpPost]
        public async Task<IActionResult> EditProduct(Product product)
        {
            if (ModelState.IsValid)
            {
                await _productRepository.UpdateAsync(product);
                return RedirectToAction(nameof(ManageProducts));
            }
            return View(product);
        }

        // Удаление продукта
        [HttpPost]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var product = await _productRepository.GetByIdAsync(id);
            if (product == null)
            {
                return NotFound();
            }

            await _productRepository.DeleteAsync(product);
            return RedirectToAction(nameof(ManageProducts));
        }

        // Отчеты
        public IActionResult Reports()
        {
            // Здесь можно добавить логику для формирования отчетов
            return View();
        }
    }
}
