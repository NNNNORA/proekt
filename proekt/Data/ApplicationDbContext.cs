using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using proekt.Models;

public class ApplicationDbContext : IdentityDbContext<User>
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }

    public DbSet<User> Users { get; set; }
    public DbSet<Product> Products { get; set; }
    public DbSet<Recipe> Recipes { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<Cart> Carts { get; set; }
    public DbSet<OrderItem> OrderItems { get; set; }
    public DbSet<Review> Reviews { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<Ingredient> Ingredients { get; set; }
    public DbSet<ProductIngredient> ProductIngredients { get; set; }
    public DbSet<CartItem> CartItems { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite("Data Source=proekt.db");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Настройка типов данных для SQLite и сущностей

        modelBuilder.Entity<IdentityUser>(entity =>
        {
            entity.Property(m => m.Id).HasColumnType("TEXT");
            entity.Property(m => m.UserName).HasColumnType("TEXT").HasMaxLength(256);
            entity.Property(m => m.NormalizedUserName).HasColumnType("TEXT").HasMaxLength(256);
            entity.Property(m => m.Email).HasColumnType("TEXT").HasMaxLength(256);
            entity.Property(m => m.NormalizedEmail).HasColumnType("TEXT").HasMaxLength(256);
            entity.Property(m => m.PasswordHash).HasColumnType("TEXT");
            entity.Property(m => m.SecurityStamp).HasColumnType("TEXT");
            entity.Property(m => m.ConcurrencyStamp).HasColumnType("TEXT");
            entity.Property(m => m.PhoneNumber).HasColumnType("TEXT");
        });

        modelBuilder.Entity<IdentityRole>(entity =>
        {
            entity.Property(m => m.Id).HasColumnType("TEXT");
            entity.Property(m => m.Name).HasColumnType("TEXT").HasMaxLength(256);
            entity.Property(m => m.NormalizedName).HasColumnType("TEXT").HasMaxLength(256);
            entity.Property(m => m.ConcurrencyStamp).HasColumnType("TEXT");
        });

        modelBuilder.Entity<IdentityUserToken<string>>(entity =>
        {
            entity.HasKey(m => new { m.UserId, m.LoginProvider, m.Name });
            entity.Property(m => m.UserId).HasColumnType("TEXT");
            entity.Property(m => m.LoginProvider).HasColumnType("TEXT").HasMaxLength(128);
            entity.Property(m => m.Name).HasColumnType("TEXT").HasMaxLength(128);
            entity.Property(m => m.Value).HasColumnType("TEXT");
        });

        modelBuilder.Entity<IdentityUserLogin<string>>(entity =>
        {
            entity.HasKey(m => new { m.LoginProvider, m.ProviderKey });
            entity.Property(m => m.LoginProvider).HasColumnType("TEXT").HasMaxLength(128);
            entity.Property(m => m.ProviderKey).HasColumnType("TEXT").HasMaxLength(128);
            entity.Property(m => m.ProviderDisplayName).HasColumnType("TEXT");
            entity.Property(m => m.UserId).HasColumnType("TEXT");
        });
        /*
        modelBuilder.Entity<IdentityUserRole<string>>(entity =>
        {
            entity.HasKey(m => new { m.UserId, m.RoleId });

            // Определение типов колонок
            entity.Property(m => m.UserId).HasColumnType("TEXT");
            entity.Property(m => m.RoleId).HasColumnType("TEXT");

            // Связь с таблицами IdentityRole и IdentityUser
            entity.HasOne<IdentityRole>()
                .WithMany(r => r.Users)
                .HasForeignKey(ur => ur.RoleId)
                .OnDelete(DeleteBehavior.Cascade);

            entity.HasOne<IdentityUser>()
                .WithMany(u => u.Roles)
                .HasForeignKey(ur => ur.UserId)
                .OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<IdentityUserClaim<string>>(entity =>
        {
            entity.Property(m => m.Id).HasColumnType("INTEGER");
            entity.Property(m => m.UserId).HasColumnType("TEXT");
            entity.Property(m => m.ClaimType).HasColumnType("TEXT");
            entity.Property(m => m.ClaimValue).HasColumnType("TEXT");
        });

        modelBuilder.Entity<IdentityRoleClaim<string>>(entity =>
        {
            entity.Property(m => m.Id).HasColumnType("INTEGER");
            entity.Property(m => m.RoleId).HasColumnType("TEXT");
            entity.Property(m => m.ClaimType).HasColumnType("TEXT");
            entity.Property(m => m.ClaimValue).HasColumnType("TEXT");
        });
        */
        // Настройки связей между сущностями

        modelBuilder.Entity<Product>()
            .HasOne(p => p.Category)
            .WithMany(c => c.Products)
            .HasForeignKey(p => p.CategoryId)
            .OnDelete(DeleteBehavior.SetNull);

        modelBuilder.Entity<ProductIngredient>()
        .HasKey(pi => new { pi.ProductId, pi.IngredientId });

        modelBuilder.Entity<ProductIngredient>()
            .HasOne(pi => pi.Product)
            .WithMany(p => p.ProductIngredients)
            .HasForeignKey(pi => pi.ProductId);

        modelBuilder.Entity<ProductIngredient>()
            .HasOne(pi => pi.Ingredient)
            .WithMany(i => i.ProductIngredients)
            .HasForeignKey(pi => pi.IngredientId);
        modelBuilder.Entity<CartItem>()
            .HasOne(ci => ci.Product)
            .WithMany(p => p.CartItems)
            .HasForeignKey(ci => ci.ProductId);


        modelBuilder.Entity<OrderItem>()
            .HasOne(oi => oi.Product)
            .WithMany(p => p.OrderItems)
            .HasForeignKey(oi => oi.ProductId);

        modelBuilder.Entity<OrderItem>()
            .HasOne(oi => oi.Order)
            .WithMany(o => o.OrderItems)
            .HasForeignKey(oi => oi.OrderId);

        modelBuilder.Entity<Review>()
            .HasOne(r => r.Product)
            .WithMany(p => p.Reviews)
            .HasForeignKey(r => r.ProductId);

        modelBuilder.Entity<Review>()
            .HasOne(r => r.User)
            .WithMany(u => u.Reviews)
            .HasForeignKey(r => r.UserId);

        modelBuilder.Entity<Cart>()
            .HasOne(ci => ci.Product)
            .WithMany(p => p.Carts)
            .HasForeignKey(ci => ci.ProductId);

        modelBuilder.Entity<Cart>()
            .HasOne(ci => ci.User)
            .WithMany(u => u.Carts)
            .HasForeignKey(ci => ci.UserId);
    }
}
