using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Task3
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public int Quantity { get; set; }
    }

    public class ProductsDbContext : DbContext
    {
        public DbSet<Product> Products { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=productsDB.db");
        }
    }

    public class Program
    {
        public static void Main(string[] args)
        {
            using (var context = new ProductsDbContext())
            {
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();
                context.Products.AddRange(
                    new Product { Id = 1, Name = "Яблоко", Price = 49, Quantity = 50 },
                    new Product { Id = 2, Name = "Вода", Price = 69, Quantity = 50 },
                    new Product { Id = 3, Name = "Лопата", Price = 599, Quantity = 10 },
                    new Product { Id = 4, Name = "Кактус", Price = 259, Quantity = 10 }
                );
                context.SaveChanges();


                Console.WriteLine("Метод Select:");
                var allProducts = context.Products.Select(p => p);
                foreach (var product in allProducts)
                {
                    Console.WriteLine($"Id: {product.Id}, Имя: {product.Name}, Цена: {product.Price}, Количество: {product.Quantity}");
                }

                Console.WriteLine("Метод Where:");
                var expensiveProducts = context.Products.Where(p => p.Price > 200);
                foreach (var product in expensiveProducts)
                {
                    Console.WriteLine($"Id: {product.Id}, Имя: {product.Name}, Цена: {product.Price}, Количество: {product.Quantity}");
                }

                Console.WriteLine("Метод OrderBy:");
                var sortedProducts = context.Products.OrderBy(p => p.Price);
                foreach (var product in sortedProducts)
                {
                    Console.WriteLine($"Id: {product.Id}, Имя: {product.Name}, Цена: {product.Price}, Количество: {product.Quantity}");
                }

                var minPrice = context.Products.Min(p => p.Price);
                var maxPrice = context.Products.Max(p => p.Price);
                var averagePrice = context.Products.Average(p => p.Price);
                var sumPrice = context.Products.Sum(p => p.Price);
                Console.WriteLine($"Метод Min: {minPrice}");
                Console.WriteLine($"Метод Max: {maxPrice}");
                Console.WriteLine($"Метод Average: {averagePrice}");
                Console.WriteLine($"Метод Sum: {sumPrice}");

                Console.WriteLine("Метод GroupBy:");
                var products = context.Products.ToList();
                var groupedQuantity = products.GroupBy(p => p.Quantity);
                foreach (var group in groupedQuantity)
                {
                    Console.WriteLine($"Количество: {group.Key}:");
                    foreach (var product in group)
                    {
                        Console.WriteLine($"Id: {product.Id}, Имя: {product.Name}, Цена: {product.Price}, Количество: {product.Quantity}");
                    }
                }
            }
        }
    }
}
