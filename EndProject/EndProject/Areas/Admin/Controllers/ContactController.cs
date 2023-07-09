using EndProject.Data;
using EndProject.Helpers;
using EndProject.Models;
using EndProject.Services;
using EndProject.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace EndProject.Areas.Admin.Controllers
{
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
