using EndProject.Models;
using EndProject.Services;
using EndProject.Services.Interfaces;
using EndProject.ViewModels.Contact;
using EndProject.ViewModels.Home;
using Microsoft.AspNetCore.Mvc;

namespace EndProject.Controllers
{
    public class ContactController : Controller
    {
        private readonly ICrudService<Contact> _crudService;
        private readonly IContactService _contactService;
        private readonly IContactInfoService _contacInfotService;


        public ContactController(ICrudService<Contact> crudService,
                                 IContactService contactService,
                                 IContactInfoService contactInfoService
                                 )
        {
            _crudService = crudService;
            _contactService = contactService;
            _contacInfotService = contactInfoService;
        }

        public async Task<IActionResult> Index()
        {

            ContactVM model = new()
            {

                 ContactInfos = await _contacInfotService.GetAllAsync(),
      



            };
            return View(model);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> PostComment(ContactVM model)
        {
            //if (!ModelState.IsValid) return RedirectToAction("Index", model);
            Contact contact = new()
            {
                Name = model.Name,
                Email = model.Email,
                Subject = model.Subject,
                Message = model.Message,
              
            };
        


            await _crudService.CreateAsync(contact);
            return RedirectToAction(nameof(Index));
        }
    }
}
