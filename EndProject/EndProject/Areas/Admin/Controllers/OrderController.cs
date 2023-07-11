using EndProject.Data;
using EndProject.Helpers.Enums;
using EndProject.Models;
using EndProject.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EndProject.Areas.Admin.Controllers
{
    [Area("Admin")]

    public class OrderController : Controller
    {
        private readonly AppDbContext _context;
        private readonly EmailService _emailService;

        public OrderController(AppDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            List<Order> orders = _context.Orders.Include(x=>x.AppUser).ToList();
            return View(orders);
        }
        public IActionResult Edit(int id)
        {
            Order order = _context.Orders.Include(o => o.OrderItems).Include(o => o.AppUser).FirstOrDefault(o => o.Id == id);
            ViewBag.Products = _context.Products.ToList();
            if (order == null) return Redirect("~/Error/Error");

            return View(order);
        }
        public IActionResult Accept(int id)
        {


            Order order = _context.Orders.Include(x => x.AppUser).FirstOrDefault(o => o.Id == id);
            if (order == null) return Redirect("~/Error/Error");

            order.Status = Status.Accepted;

            _context.SaveChanges();
            //string recipientEmail = order.AppUser.Email;
            //string subject = "Your order has been accepted";
            //string body = "Your order has been accepted. Thank you! The total amount to be paid is " + order.TotalPrice + "₼";


            //_emailService.SendEmail(recipientEmail, subject, body);
            return RedirectToAction("Index", "Order");

        }
        public IActionResult Reject(int id)
        {
            Order order = _context.Orders.Include(x => x.AppUser).FirstOrDefault(o => o.Id == id);
            if (order == null) return Redirect("~/Error/Error");

            order.Status = Status.Rejected;

            _context.SaveChanges();
            //string recipientEmail = order.AppUser.Email;
            //string subject = "Your order has been rejected";
            //string body = "Your order has been rejected. Unfortunately, the products you ordered are currently out of stock.Thank you for your understanding.";


            //_emailService.SendEmail(recipientEmail, subject, body);
            return RedirectToAction("Index", "Order");

        }
    }

}
