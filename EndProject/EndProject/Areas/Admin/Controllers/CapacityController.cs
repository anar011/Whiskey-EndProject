using EndProject.Areas.Admin.ViewModels.Capacity;
using EndProject.Models;
using EndProject.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Drawing;

namespace EndProject.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CapacityController : Controller
    {
        private readonly ICapacityService _capacityService;
        private readonly ICrudService<Capacity> _crudService;

        public CapacityController(ICapacityService capacityService, ICrudService<Capacity> crudService)
        {
            _capacityService = capacityService;
            _crudService = crudService;
        }
        public async Task<IActionResult> Index()
        {
            return View(await _capacityService.GetAllAsync());
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CapacityVM model)
        {
            try
            {
                if (!ModelState.IsValid) return View();

                if (_capacityService.CheckByName(model.Name))
                {
                    ModelState.AddModelError("Name", "Name already exist");
                    return View(model);
                }

                Capacity capacity = new()
                {
                    Name = model.Name
                };

                await _crudService.CreateAsync(capacity);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ViewBag.error = ex.Message;
                return View();
            }
        }



        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            try
            {
                if (id is null) return BadRequest();
                Capacity dbCapacity = await _capacityService.GetByIdAsync((int)id);
                if (dbCapacity is null) return NotFound();

                CapacityVM size = new()
                {
                    Id = dbCapacity.Id,
                    Name = dbCapacity.Name,
                };
                return View(size);
            }
            catch (Exception ex)
            {
                ViewBag.error = ex.Message;
                return View();
            }
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int? id, CapacityVM model)
        {
            try
            {
                if (!ModelState.IsValid) return View();

                if (id is null) return BadRequest();

                Capacity dbCapacity = await _capacityService.GetByIdAsync((int)id);

                if (dbCapacity is null) return NotFound();

                if (dbCapacity.Name.Trim().ToLower() == model.Name.Trim().ToLower())
                {
                    return RedirectToAction(nameof(Index));
                }

                Capacity capacity = new()
                {
                    Id = model.Id,
                    Name = model.Name
                };

                _crudService.Edit(capacity);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ViewBag.error = ex.Message;
                return View();
            }
        }



        [HttpPost]
        public async Task<IActionResult> Delete(int? id)
        {
            try
            {
                if (id is null) return BadRequest();

                Capacity dbCapacity = await _capacityService.GetByIdAsync((int)id);

                if (dbCapacity is null) return NotFound();

                _crudService.Delete(dbCapacity);

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
