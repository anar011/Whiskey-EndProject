﻿using EndProject.Helpers.Enums;
using EndProject.Models;
using EndProject.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EndProject.Areas.Admin.Controllers
{
    [Authorize(Roles = "SuperAdmin, Admin")]

    [Area("Admin")]
    public class ProductCommentController : Controller
    {
        private readonly ICrudService<ProductComment> _crudService;
        private readonly IProductService _productService;

        public ProductCommentController(ICrudService<ProductComment> crudService,
                                IProductService productService)
        {
            _crudService = crudService;
            _productService = productService;
        }


        public async Task<IActionResult> Index()
        {
            var comments = await _productService.GetComments();
            return View(comments);
        }


        [HttpGet]
        public async Task<IActionResult> Detail(int? id)
        {
            if (id == null) return BadRequest();
            ProductComment dbcomment = await _productService.GetCommentByIdWithProduct((int)id);
            if (dbcomment is null) return NotFound();
            return View(dbcomment);
        }


        [HttpPost]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return BadRequest();
            ProductComment dbcomment = await _productService.GetCommentById((int)id);
            if (dbcomment is null) return NotFound();

            _crudService.Delete(dbcomment);
            return RedirectToAction(nameof(Index));
        }


    }
}
