using EndProject.Data;
using EndProject.Models;
using EndProject.Services.Interfaces;
using EndProject.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EndProject.Controllers
{
    public class OrderController : Controller
    {
        private readonly AppDbContext _context;
        private readonly UserManager<AppUser> _userManager;
    

        public OrderController(AppDbContext context, UserManager<AppUser> userManager)
        {
            _context = context;
            _userManager = userManager;
           
        }

        public async Task<IActionResult> Index()
        {
            OrderVm model = new OrderVm();
            if (User.Identity.IsAuthenticated)
            {

                AppUser user = await _userManager.FindByNameAsync(User.Identity.Name);
                 model = new()
                {
                    Fullname = string.Concat(user.FirstName, " ", user.LastName),
                    Email = user.Email,
                    BasketItems = _context.BasketItems.Include(x => x.Basket).Include(p => p.ProductCapacity).Where(c => c.Basket.AppUserID == user.Id).ToList()
                };
            }
            return View(model);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(OrderVm orderVM)
        {
            AppUser user = await _userManager.FindByNameAsync(User.Identity.Name);
            OrderVm model = new()
            {
                Fullname = orderVM.Fullname,
                Email = orderVM.Email,
                Message = orderVM.Message,
                Address = orderVM.Address,
                BasketItems = _context.BasketItems.Include(x => x.Basket).Include(p => p.ProductCapacity).ThenInclude(x=>x.Product).Where(c => c.Basket.AppUserID == user.Id).ToList()


            };
            if (!ModelState.IsValid) return View(model);
            if (model.BasketItems.Count == 0) return RedirectToAction("Index", "Home");


            Order order = new Order()
            {
                Address = orderVM.Address,
                TotalPrice = 0,
                Date = DateTime.Now,
                AppUserId = user.Id,
                Message = orderVM.Message,
                Status = Helpers.Enums.Status.Pending
            };


            foreach (BasketItem item in model.BasketItems)
            {

                OrderItem orderItem = new OrderItem
                {
                    Name = item.ProductCapacity.Product.Name,
                    Price = item.ProductCapacity.Price,
                    ProductId = item.ProductCapacity.ProductId,
                    Quantity = item.Quantity,
                    Order = order
                };
                order.TotalPrice += item.Price * item.Quantity;
                _context.OrderItems.Add(orderItem);
            }
            _context.BasketItems.RemoveRange(model.BasketItems);
            _context.Orders.Add(order);
            _context.SaveChanges();
            return RedirectToAction("Index", "Home");
        }
    }
}
