using EndProject.Data;
using EndProject.Helpers.Enums;
using EndProject.Models;
using EndProject.Services;
using EndProject.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace EndProject.Areas.Admin.Controllers
{
    [Authorize(Roles = "SuperAdmin, Admin")]

    [Area("Admin")]

    public class OrderController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IEmailService _emailService;

        public OrderController(AppDbContext context, IEmailService emailService)
        {
            _context = context;
            _emailService= emailService;
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
            if (order == null) return NotFound(); ;

            return View(order);
        }
        public IActionResult Accept(int id)
        {


            Order order = _context.Orders.Include(x => x.AppUser).FirstOrDefault(o => o.Id == id);
            if (order == null)  return NotFound();

            order.Status = Status.Accepted;

            _context.SaveChanges();
            string recipientEmail = order.AppUser.Email;
            string subject = "Your order has been accepted";
            string body = "Your order has been accepted. Thank you! The total amount to be paid is " + order.TotalPrice + "₼";


            _emailService.Send(recipientEmail, subject, body);

            return RedirectToAction("Index", "Order");

        }
        public IActionResult Reject(int id)
        {
            Order order = _context.Orders.Include(x => x.AppUser).FirstOrDefault(o => o.Id == id);
            if (order == null) return NotFound();

            order.Status = Status.Rejected;

            _context.SaveChanges();
            string recipientEmail = order.AppUser.Email;
            string subject = "Your order has been rejected";
            string body = "The order was canceled because the product you selected is out of stock";


            _emailService.Send(recipientEmail, subject, body);


            return RedirectToAction("Index", "Order");

        }
    }

}
