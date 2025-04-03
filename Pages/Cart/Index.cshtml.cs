using E_Commerce_Razor_Page_Project.Models;
using E_Commerce_Razor_Page_Project.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace E_Commerce_Razor_Page_Project.Pages.Cart;

public class IndexModel(CartService cartService) : PageModel
{
    public List<CartItem> CartItems { get; set; } = new();
    public decimal CartTotal => cartService.GetTotal();

    public void OnGet()
    {
        CartItems = cartService.GetCart();
    }

    public IActionResult OnPostUpdateQuantity(int productId, int quantity)
    {
        if (quantity <= 0)
        {
            return BadRequest();
        }

        cartService.UpdateQuantity(productId, quantity);
        return RedirectToPage();
    }

    public IActionResult OnPostRemoveItem(int productId)
    {
        cartService.RemoveFromCart(productId);
        return RedirectToPage();
    }
}