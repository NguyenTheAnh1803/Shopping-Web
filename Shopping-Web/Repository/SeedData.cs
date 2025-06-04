using Microsoft.EntityFrameworkCore;
using Shopping_Web.Models;

namespace Shopping_Web.Repository
{
    public class SeedData
    {
        public static void SeedingData(DataContext _context)
        {
            _context.Database.Migrate();
            if (!_context.Products.Any())
            {
               CategoryModel macbook = new CategoryModel { Name = "Macbook", Slug = "macbook", Description = "Macbook is large product in the world", Status=1 };
               CategoryModel pc = new CategoryModel { Name = "PC", Slug = "pc", Description = "Pc is large product in the world" ,Status = 1 };

                BrandModel apple = new BrandModel { Name = "Apple", Slug = "apple", Description = "Apple is a large brand in the world", Status = 1 };
                BrandModel samsung = new BrandModel { Name = "Samsung", Slug = "samsung", Description = "Samsung is a large brand in the world", Status = 1 };

                _context.Products.AddRange(
                    new ProductModel { Name = "Macbook", Slug = "macbook", Description = "Macbook is Best", Price = 99999, Brand = apple, Category =macbook , Image = "macbook.jpg" },
                    new ProductModel { Name = "PC", Slug = "pc", Description = "PC is best ", Price = 119999, Brand = samsung, Category = pc, Image = "pc.jpg" }
                );
                _context.SaveChanges();
            }
        }
    }
}
