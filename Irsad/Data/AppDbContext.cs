using Irsad.Models.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Irsad.Data
{
    public class AppDbContext : IdentityDbContext<AppUser>
    {
        public AppDbContext(DbContextOptions options) : base(options) { }

        public DbSet<Product> Products { get; set; }        
        public DbSet<Category> Categories { get; set; }
        public DbSet<ProductTag> ProductTags { get; set; }
        public DbSet<UserProduct> UserProducts { get; set; }
        public DbSet<Tag> Tags { get; set; }

        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    //modelBuilder.Entity<ProductTag>()
        //    //     .HasKey(pt => new { pt.ProductId, pt.TagId });

        //    //modelBuilder.Entity<UserProduct>()
        //    //     .HasKey(pt => new { pt.ProductId, pt.UserId });

        //    //modelBuilder.Entity<AppUser>().
        //    //    HasKey(u=>u.Id);
        //    //modelBuilder.Entity<Product>()
        //    //       .HasOne(p => p.Category)
        //    //       .WithMany(p => p.Products)
        //    //       .HasForeignKey(p => p.CategoryId);

        //    //modelBuilder.Entity<UserProduct>()
        //    //    .HasOne(p => p.Product)
        //    //    .WithMany(p => p.UserProducts)
        //    //    .HasForeignKey(p => p.ProductId);

        //    //modelBuilder.Entity<UserProduct>()
        //    //     .HasOne(p => p.User)
        //    //     .WithMany(p => p.Products)
        //    //     .HasForeignKey(p => p.UserId);

        //    //modelBuilder.Entity<ProductTag>()
        //    //    .HasOne(p => p.Product)
        //    //    .WithMany(p => p.ProductTags)
        //    //    .HasForeignKey(p => p.ProductId);

        //    //modelBuilder.Entity<ProductTag>()
        //    //     .HasOne(p => p.Tag)
        //    //     .WithMany(p => p.ProductTags)
        //    //     .HasForeignKey(p => p.TagId);
        //}

    }
}
