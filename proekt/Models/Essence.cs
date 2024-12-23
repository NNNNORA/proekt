using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.ComponentModel.DataAnnotations.Schema;
using proekt.Models;
using System.ComponentModel.DataAnnotations;

namespace proekt.Models
{
    public class Essence
    {
    }

    public class User:IdentityUser
    {
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        [Required]
        public string Password { get; set; }
        public DateTime DateRegistered { get; set; }
        public string Address { get; set; }
        public ICollection<Cart> Carts { get; set; }
        public ICollection<Review> Reviews { get; set; }
        public string Role { get; set; } // Добавлено поле Role
    }

    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }

        [Required]
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int CategoryId { get; set; }
        public string Image { get; set; }
        public double Weight { get; set; } // Добавлено свойство Weight
        public bool InStock { get; set; }
        public string Ingredients { get; set; }
        public Category Category { get; set; }
        public ICollection<OrderItem> OrderItems { get; set; }
        public ICollection<CartItem> CartItems { get; set; } // Связь с корзиной
        public ICollection<Review> Reviews { get; set; }
        public ICollection<Cart> Carts { get; set; }

        [BindNever]
        public virtual ICollection<ProductIngredient> ProductIngredients { get; set; }
    }


    public class Recipe
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public string Instructions { get; set; }
        public TimeSpan CookingTime { get; set; }
    }

    public class Order
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public DateTime DateTime { get; set; }
        public decimal TotalAmount { get; set; }
        public string Status { get; set; }
        public string DeliveryAddress { get; set; }
        public ICollection<OrderItem> OrderItems { get; set; }
    }
    public class CartItem
    {
        public int ProductId { get; set; }
        public string Name { get; set; }
        public string UserId { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public int Id { get; set; }
        public Product Product { get; set; }

    }
    public class Cart
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public Product Product { get; set; }  // Навигационное свойство
        public User User { get; set; }
        public List<CartItem> Items { get; set; } = new List<CartItem>();
        public decimal TotalPrice => Items.Sum(i => i.Price * i.Quantity);
    }

    public class OrderItem
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public Product Product { get; set; } // Навигационное свойство
        public Order Order { get; set; }

    }


    public class Review
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public string UserId { get; set; }
        public int Rating { get; set; }
        public string Text { get; set; }
        public DateTime DateWritten { get; set; }
        public Product Product { get; set; }  // Навигационное свойство
        public User User { get; set; }
    }

    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public ICollection<Product> Products { get; set; }
    }

    public class Ingredient
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public ICollection<ProductIngredient> ProductIngredients { get; set; }
    }

    public class ProductIngredient
    {
        public int ProductId { get; set; }
        public int IngredientId { get; set; }
        public decimal Quantity { get; set; }
        public Product Product { get; set; }    // Навигационное свойство для Product
        public Ingredient Ingredient { get; set; } // Навигационное свойство для Ingredien
    }
   
}
