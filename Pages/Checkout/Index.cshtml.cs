using E_Commerce_Razor_Page_Project.Models;
using E_Commerce_Razor_Page_Project.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace E_Commerce_Razor_Page_Project.Pages.Checkout;

public class IndexModel(CartService cartService) : PageModel
{
    [BindProperty]
    public CustomerInfo CustomerInfo { get; set; } = new();
    public List<CartItem> CartItems { get; set; } = new();
    public decimal CartTotal => cartService.GetTotal();

    public IActionResult OnGet()
    {
        CartItems = cartService.GetCart();
        if (!CartItems.Any())
        {
            return RedirectToPage("/Cart/Index");
        }
        return Page();
    }

    public IActionResult OnPost()
    {
        if (!ModelState.IsValid)
        {
            CartItems = cartService.GetCart();
            return Page();
        }

        // Here you would typically:
        // 1. Create an order in the database
        // 2. Process payment (not implemented in this demo)
        // 3. Send confirmation email
        // 4. Clear the cart

        cartService.ClearCart();

        // Redirect to a "thank you" page
        return RedirectToPage("/Checkout/ThankYou");
    }
}