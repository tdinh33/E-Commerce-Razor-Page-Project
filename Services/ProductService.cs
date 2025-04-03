using E_Commerce_Razor_Page_Project.Models;

namespace E_Commerce_Razor_Page_Project.Services;

public class ProductService
{
    private readonly List<Product> _products = new()
    {
        new Product
        {
            Id = 1,
            Name = "Laptop Pro X",
            Description = "High-performance laptop with latest processor",
            Price = 1299.99M,
            ImageUrl = "/images/laptop.jpg",
            StockQuantity = 10,
            Category = "Electronics"
        },
        new Product
        {
            Id = 2,
            Name = "Wireless Headphones",
            Description = "Premium noise-canceling wireless headphones",
            Price = 199.99M,
            ImageUrl = "/images/headphones.jpg",
            StockQuantity = 20,
            Category = "Electronics"
        },
        new Product
        {
            Id = 3,
            Name = "Smart Watch",
            Description = "Feature-rich smartwatch with health tracking",
            Price = 299.99M,
            ImageUrl = "/images/smartwatch.jpg",
            StockQuantity = 15,
            Category = "Electronics"
        }
    };

    public IEnumerable<Product> GetAllProducts() => _products;

    public Product? GetProductById(int id) => _products.FirstOrDefault(p => p.Id == id);

    public IEnumerable<Product> SearchProducts(string searchTerm)
    {
        return string.IsNullOrWhiteSpace(searchTerm)
            ? _products
            : _products.Where(p =>
                p.Name.Contains(searchTerm, StringComparison.OrdinalIgnoreCase) ||
                p.Category.Contains(searchTerm, StringComparison.OrdinalIgnoreCase));
    }
}