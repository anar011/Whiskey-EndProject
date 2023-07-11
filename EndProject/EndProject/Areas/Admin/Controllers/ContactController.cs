using EndProject.Data;
using EndProject.Helpers;
using EndProject.Helpers.Enums;
using EndProject.Models;
using EndProject.Services;
using EndProject.Services.Interfaces;
using EndProject.ViewModels.Contact;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mail;
using System.Net;

namespace EndProject.Areas.Admin.Controllers
{
    [Authorize(Roles = "SuperAdmin, Admin")]

    [Area("Admin")]
    public class ContactController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;
        private readonly ICrudService<Contact> _crudService;
        private readonly IContactService _contactService;

        public ContactController(AppDbContext context,
                                IWebHostEnvironment env,
                                IContactService contactService,
                                ICrudService<Contact> crudService)
        {
            _contactService = contactService;
            _context = context;
            _env = env;
            _crudService = crudService;
        }
        public async Task<IActionResult> Index()
        {
            IEnumerable<Contact> dbContact = await _contactService.GetAllAsync();
            return View(dbContact);
        }


        [HttpGet]
        public async Task<IActionResult> Detail(int? id)
        {

            if (id == null) return BadRequest();
            Contact dbContact = await _contactService.GetByIdAsync((int)id);

            if (dbContact is null) return NotFound();

            return View(dbContact);
        }

        [HttpPost]
        public async Task<IActionResult> Detail(int id, string replace)
        {
            if (id == 0) return NotFound();
            Contact contact = _context.Contacts.FirstOrDefault(x => x.Id == id);
            if (contact is null) return NotFound();
            MailMessage message = new MailMessage();
            message.From = new MailAddress("whiskeys1234@gmail.com", "Whiskey");
            message.To.Add(new MailAddress(contact.Email));
            message.Subject = "Whiskey Support";
            message.Body = string.Empty;
            message.Body = $"{replace}";

            SmtpClient smtpClient = new SmtpClient();
            smtpClient.Host = "smtp.gmail.com";
            smtpClient.Port = 587;
            smtpClient.EnableSsl = true;
            smtpClient.UseDefaultCredentials = false;
            smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
            smtpClient.Credentials = new NetworkCredential("whiskeys1234@gmail.com", "ypcewhlqtxhfgiwj");
            smtpClient.Send(message);
            contact.IsReply = true;
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }


        [HttpPost]
        public async Task<IActionResult> Delete(int? id)
        {
            try
            {
                if (id is null) return BadRequest();
                Contact dbContact = await _contactService.GetByIdAsync((int)id);
                if (dbContact is null) return NotFound();

                
                

                _crudService.Delete(dbContact);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ViewBag.error = ex.Message;
                return View();
            }
        }



    }
}
