using Microsoft.EntityFrameworkCore;
using WebApiSample.Entities;

namespace WebApiSample.DbAccess;

public class AppDbContext : DbContext
{
    public DbSet<Product> Products { get; set; }
    //public DbSet<Article> Articles { get; set; }

    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Product>(builder =>
        {
            builder.HasKey(p => p.Id);

            builder.Property(p => p.Id).ValueGeneratedOnAdd();

            builder.Property(p => p.Price).HasPrecision(18, 2);

            builder.ToTable("Products", tableBuilder =>
            {
                tableBuilder.HasCheckConstraint(
                    "CK_Price_NotNegative",
                    sql: $"{nameof(Product.Price)} > 0");
            });

        });

        //modelBuilder.Entity<Article>(builder =>
        //{
        //    builder.OwnsOne(a => a.Tags, tagsBuilder => tagsBuilder.ToJson());
        //});
        
    }
}
