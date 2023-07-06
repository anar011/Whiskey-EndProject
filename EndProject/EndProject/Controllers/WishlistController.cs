using EndProject.Data;
using EndProject.Models;
using EndProject.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EndProject.Controllers
{
    public class WishlistController : Controller
    {
        private readonly IProductService _productService;
        private readonly AppDbContext _context;
        private readonly UserManager<AppUser> _userManager;

        public WishlistController(IProductService productService,AppDbContext context,UserManager<AppUser> userManager)
        {
            _productService = productService;
            _context = context;
            _userManager = userManager;
        }
        public IActionResult Index()
        {
            if (!User.Identity.IsAuthenticated)
            {
                return View(new List<WishlistItem>());
            }

            var userId = _userManager.GetUserId(User);

            var wishListItems = _context.WishlistItems
                .Include(wli => wli.Product).ThenInclude(x=>x.ProductCapacities).ThenInclude(x => x.Capacity)
                .Where(wli => wli.AppUserId == userId)
                .ToList();

            if (wishListItems.Count == 0)
            {
                return View(new List<WishlistItem>());
            }

            return View(wishListItems);
        }

        public async Task<IActionResult> AddWishlist(int id)
        {
            Product dbProduct = await _productService.GetFullDataByIdAsync((int)id);

            if (dbProduct is null) return BadRequest();

            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Login", "Account");
            }

            AppUser user = await _userManager.FindByNameAsync(User.Identity.Name);

            WishlistItem userWishlistItem = await _context.WishlistItems
                .FirstOrDefaultAsync(x => x.AppUserId == user.Id && x.ProductId == id);

            if (userWishlistItem is null)
            {
                userWishlistItem = new()
                {
                    AppUserId = user.Id,
                    ProductId = id
                };
                _context.WishlistItems.Add(userWishlistItem);
            }

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }


        public async Task<IActionResult> RemoveFromWishList(int wishListItemId)
        {
            AppUser user = await _userManager.FindByNameAsync(User.Identity.Name);
            WishlistItem wishListItem = await _context.WishlistItems
                .FirstOrDefaultAsync(x => x.AppUserId == user.Id && x.Id == wishListItemId);
            if (wishListItem is null)
            {
                return NotFound();
            }
            _context.WishlistItems.Remove(wishListItem);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
