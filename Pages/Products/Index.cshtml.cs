using E_Commerce_Razor_Page_Project.Models;
using E_Commerce_Razor_Page_Project.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace E_Commerce_Razor_Page_Project.Pages.Products;

public class IndexModel(ProductService productService, CartService cartService) : PageModel
{
    public IEnumerable<Product> Products { get; set; } = new List<Product>();
    [BindProperty(SupportsGet = true)]
    public string? SearchTerm { get; set; }

    public void OnGet()
    {
        Products = productService.SearchProducts(SearchTerm ?? string.Empty);
    }

    public IActionResult OnPostAddToCart(int productId)
    {
        cartService.AddToCart(productId);
        return RedirectToPage();
    }
}