﻿using Microsoft.AspNetCore.Mvc;

namespace EndProject.Controllers
{
    public class BlogController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
