using E_Commerce_Razor_Page_Project.Models;
using E_Commerce_Razor_Page_Project.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace E_Commerce_Razor_Page_Project.Pages.Products;

public class DetailsModel(ProductService productService, CartService cartService) : PageModel
{
    public Product? Product { get; set; }

    public IActionResult OnGet(int id)
    {
        Product = productService.GetProductById(id);
        if (Product == null)
        {
            return NotFound();
        }
        return Page();
    }

    public IActionResult OnPost(int id, int quantity)
    {
        if (quantity <= 0)
        {
            return BadRequest();
        }

        var product = productService.GetProductById(id);
        if (product == null)
        {
            return NotFound();
        }

        if (quantity > product.StockQuantity)
        {
            ModelState.AddModelError("", "Requested quantity exceeds available stock.");
            Product = product;
            return Page();
        }

        for (int i = 0; i < quantity; i++)
        {
            cartService.AddToCart(id);
        }

        return RedirectToPage("/Cart/Index");
    }
}