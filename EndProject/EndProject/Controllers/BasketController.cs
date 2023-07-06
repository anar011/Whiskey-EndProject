using EndProject.Data;
using EndProject.Helpers.Enums;
using EndProject.Models;
using EndProject.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EndProject.Controllers
{
    public class BasketController : Controller
    {
        private readonly IProductService _productService;
        private readonly AppDbContext _context;
        private readonly UserManager<AppUser> _userManager;

        public BasketController(IProductService productService,AppDbContext context,UserManager<AppUser> userManager)
        {
            _productService = productService;
            _context = context;
            _userManager = userManager;
        }
        public async Task<IActionResult> Addbaskets(int productId, int capacityId,int quantity)
        {
            ProductCapacity? productCapacity = _context.ProductCapacities
                .Include(p => p.Product)
                .FirstOrDefault(p => p.ProductId == productId && p.CapacityId == capacityId);

            if (productCapacity is null) return NotFound();

            AppUser? user = new();

            if (User.Identity.IsAuthenticated)
            {
                user = await _userManager.FindByNameAsync(User.Identity.Name);
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
            Basket userActiveBasket = _context.Baskets
                .Include(b => b.AppUser)
                .Include(b => b.BasketItems)
                .ThenInclude(i => i.ProductCapacity)
                .FirstOrDefault(b => b.AppUserID == user.Id && b.IsOrdered != Status.Default);

            if (userActiveBasket is null)
            {
                userActiveBasket = new Basket()
                {
                    AppUser = user,
                    BasketItems = new List<BasketItem>(),
                    IsOrdered = Status.Default
                };
                _context.Baskets.Add(userActiveBasket);
            }

            BasketItem items = userActiveBasket.BasketItems.FirstOrDefault(i => i.ProductCapacity == productCapacity);

            if (items is not null)
            {
                items.Quantity += quantity;
            }
            else
            {
                items = new BasketItem
                {
                    ProductCapacity = productCapacity,
                    Quantity = quantity,
                    Price = productCapacity.Price,
                    Basket = userActiveBasket
                };
                userActiveBasket.BasketItems.Add(items);
            }
            userActiveBasket.TotalPrice = userActiveBasket.BasketItems.Sum(p => p.Quantity * p.Price);
            await _context.SaveChangesAsync();
            return RedirectToAction("index", "home");
        }

        public async Task<IActionResult> RemoveBasketItem(int basketItemId)
        {
            AppUser? user = null; if (User.Identity.IsAuthenticated)
            {
                user = await _userManager.FindByNameAsync(User.Identity.Name);
            }
            BasketItem item = _context.BasketItems.FirstOrDefault(i => i.Id == basketItemId);

            if (item is not null)
            {
                Basket userActiveBasket = _context.Baskets
                    .Include(b => b.AppUser)
                    .Include(b => b.BasketItems)
                    .ThenInclude(i => i.ProductCapacity)
                    .FirstOrDefault(b => b.AppUserID == user.Id && b.IsOrdered != 0);
                if (userActiveBasket is not null)
                {
                    userActiveBasket.BasketItems.Remove(item);
                    userActiveBasket.TotalPrice = userActiveBasket.BasketItems.Sum(p => p.Quantity * p.Price);

                    await _context.SaveChangesAsync();
                }
            }
            return RedirectToAction("index", "home");
        }
    }
}
