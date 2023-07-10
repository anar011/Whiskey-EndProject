using EndProject.Data;
using EndProject.Models;
using EndProject.Services.Interfaces;
using EndProject.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace EndProject.Services
{
    public class LayoutService : ILayoutService
    {
        private readonly AppDbContext _context;
        private readonly IHttpContextAccessor _accessor;
        private readonly UserManager<AppUser> _userManager;

        public LayoutService(AppDbContext context,IHttpContextAccessor accessor,UserManager<AppUser> userManager)
        {
            _context = context;
            _accessor = accessor;
            _userManager = userManager;
        }
        public Setting GetById(int? id)
        {
            return _context.Settings.Where(s => s.Id == id).FirstOrDefault();
        }

        public async Task<SectionHeader> GetSectionAsync(int? id)
        {
            return await _context.SectionHeaders.FirstOrDefaultAsync(s => s.Id == id);
        }

        public async Task<SectionBackgroundImage> GetSectionBackgroundImageByIdAsync(int? id)
        {
            return await _context.SectionBackgroundImages.FirstOrDefaultAsync(sb => sb.Id == id);
        }

        public async Task<IEnumerable<SectionBackgroundImage>> GetSectionBackgroundImageDatasAsync()
        {
            return await _context.SectionBackgroundImages.ToListAsync();
        }

        public Dictionary<string, string> GetSectionBackgroundImages()
        {
            return _context.SectionBackgroundImages.AsEnumerable().ToDictionary(s => s.Key, s => s.Value);
        }

        public async Task<IEnumerable<SectionHeader>> GetSectionsDatasAsync()
        {
            return await _context.SectionHeaders.ToListAsync();
        }

        public Dictionary<string, string> GetSectionsHeaders()
        {
            return _context.SectionHeaders.AsEnumerable().ToDictionary(s => s.Key, s => s.Value);
        }

        public async Task<List<Setting>> GetSettingDatas()
        {
            List<Setting> settings = await _context.Settings.ToListAsync();

            return settings;
        }

        public Dictionary<string, string> GetSettings()
        {
            return _context.Settings.AsEnumerable().ToDictionary(s => s.Key, s => s.Value);
        }

        public List<Product> GetProducts()
        {
            List<Product> products = _context.Products.ToList();
            return products;
        }
        public List<BasketItem>? GetBasketItems()
        {
            AppUser user = new();

            if (_accessor.HttpContext.User.Identity.IsAuthenticated)
            {
                user = _userManager.Users.FirstOrDefault(x => x.UserName == _accessor.HttpContext.User.Identity.Name);
            }

            List<BasketItem> basket = _context.BasketItems.Include(x => x.ProductCapacity.Product).Include(p => p.Basket).Where(x => x.Basket.AppUserID == user.Id && x.Basket.IsOrdered == Helpers.Enums.Status.Default).ToList();

            return basket;
        }


        public List<BasketItemVM> GetBasketItem()
        {
            List<BasketItemVM> items = new();

            AppUser user = new();

            if (_accessor.HttpContext.User.Identity.IsAuthenticated)
            {
                user = _userManager.Users.FirstOrDefault(x => x.UserName == _accessor.HttpContext.User.Identity.Name);
            }

            List<BasketItem> basketItems = _context.BasketItems.Include(x => x.ProductCapacity.Product).Where(x => x.Basket.AppUserID == user.Id && x.Basket.IsOrdered == Helpers.Enums.Status.Default).ToList();
            items = basketItems.Select(x => new BasketItemVM
            {
                ProductId = x.ProductCapacity.ProductId,
                Quantity = x.Quantity,
                ProductCapacityId = x.ProductCapacityId,
                Price = x.ProductCapacity.Price,
            }).ToList();


            return items;
        }
    }
}
