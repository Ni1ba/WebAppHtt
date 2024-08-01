using HTTWebApplication.Models;
using Microsoft.EntityFrameworkCore;

public class ApplicationDbContext : DbContext
{
    public DbSet<Product> Products { get; set; }
    public DbSet<Category> Categories { get; set; }

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Category>()
            .Property(c => c.Name)
            .HasColumnType("text"); 

        modelBuilder.Entity<Product>()
            .Property(p => p.Name)
            .HasColumnType("text"); 

        modelBuilder.Entity<Product>()
            .HasOne(p => p.Category)
            .WithMany(c => c.Products)
            .HasForeignKey(p => p.CategoryId);
    }
    public List<ProductCategoryViewModel> GetJoinedData()
    {
        var query = from product in Products
                    join category in Categories
                    on product.CategoryId equals category.CategoryId
                    select new ProductCategoryViewModel
                    {
                        ProductId = product.ProductId,
                        ProductName = product.Name,
                        ProductPrice = product.Price,
                        CategoryId = category.CategoryId,
                        CategoryName = category.Name
                    };

        return query.ToList();
    }
}