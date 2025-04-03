using System.Text.Json;
using E_Commerce_Razor_Page_Project.Models;

namespace E_Commerce_Razor_Page_Project.Services;

public class CartService(IHttpContextAccessor httpContextAccessor, ProductService productService)
{
    private const string CartSessionKey = "Cart";

    private ISession Session => httpContextAccessor.HttpContext!.Session;

    public List<CartItem> GetCart()
    {
        var cartJson = Session.GetString(CartSessionKey);
        return string.IsNullOrEmpty(cartJson)
            ? new List<CartItem>()
            : JsonSerializer.Deserialize<List<CartItem>>(cartJson) ?? new List<CartItem>();
    }

    public void SaveCart(List<CartItem> cart)
    {
        var cartJson = JsonSerializer.Serialize(cart);
        Session.SetString(CartSessionKey, cartJson);
    }

    public void AddToCart(int productId)
    {
        var cart = GetCart();
        var cartItem = cart.FirstOrDefault(item => item.ProductId == productId);
        var product = productService.GetProductById(productId);

        if (product == null) return;

        if (cartItem == null)
        {
            cart.Add(new CartItem
            {
                ProductId = product.Id,
                ProductName = product.Name,
                Price = product.Price,
                Quantity = 1
            });
        }
        else
        {
            cartItem.Quantity++;
        }

        SaveCart(cart);
    }

    public void RemoveFromCart(int productId)
    {
        var cart = GetCart();
        var cartItem = cart.FirstOrDefault(item => item.ProductId == productId);

        if (cartItem != null)
        {
            cart.Remove(cartItem);
            SaveCart(cart);
        }
    }

    public void UpdateQuantity(int productId, int quantity)
    {
        var cart = GetCart();
        var cartItem = cart.FirstOrDefault(item => item.ProductId == productId);

        if (cartItem != null)
        {
            if (quantity > 0)
            {
                cartItem.Quantity = quantity;
            }
            else
            {
                cart.Remove(cartItem);
            }
            SaveCart(cart);
        }
    }

    public decimal GetTotal()
    {
        return GetCart().Sum(item => item.TotalPrice);
    }

    public void ClearCart()
    {
        Session.Remove(CartSessionKey);
    }
}