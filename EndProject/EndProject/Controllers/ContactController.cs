using EndProject.Models;
using EndProject.Services.Interfaces;
using EndProject.ViewModels.Contact;
using Microsoft.AspNetCore.Mvc;

namespace EndProject.Controllers
{
    public class ContactController : Controller
    {
        private readonly ICrudService<Contact> _crudService;

        public ContactController(ICrudService<Contact> crudService)
        {
            _crudService = crudService;
        }

        public IActionResult Index()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> PostComment(ContactVM model)
        {
            if (!ModelState.IsValid) return RedirectToAction("Index", model);
            Contact contact = new()
            {
                Name = model.Name,
                Email = model.Email,
                Subject = model.Subject,
                Message = model.Message,
            };

            await _crudService.CreateAsync(contact);
            return RedirectToAction("Index");
        }
    }
}
