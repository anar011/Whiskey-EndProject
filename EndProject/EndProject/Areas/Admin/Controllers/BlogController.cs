﻿using EndProject.Areas.Admin.ViewModels.Blog;
using EndProject.Helpers;
using EndProject.Models;
using EndProject.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace EndProject.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class BlogController : Controller
    {
        private readonly IWebHostEnvironment _env;
        private readonly IBlogService _blogService;
        private readonly ICrudService<Blog> _crudService;


        public BlogController(IBlogService blogService,
                            ICrudService<Blog> crudService,
                            IWebHostEnvironment env)
        {
            _blogService = blogService;
            _crudService = crudService;
            _env = env;
        }
       
       public async Task<IActionResult> Index()
        {
            return View(await _blogService.GetAllAsync());
        }

        [HttpGet]
        public IActionResult Create() { return View(); }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(BlogCreateVM model)
        {
            try
            {
                if (!ModelState.IsValid) return View();

                if (!model.Photo.CheckFileType("image/"))
                {
                    ModelState.AddModelError("Photo", "File type must be image");
                    return View();
                }
                if (!model.Photo.CheckFileSize(200))
                {
                    ModelState.AddModelError("Photo", "Image size must be max 200kb");
                    return View();
                }




                Blog newBlog = new()
                {
                    Image = model.Photo.CreateFile(_env, "assets/img"),
                    Title = model.Title,
                    Description = model.Description,
                    

                   
                    
                };

             
                await _crudService.CreateAsync(newBlog);
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