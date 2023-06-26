﻿using System.ComponentModel.DataAnnotations;

namespace EndProject.Areas.Admin.ViewModels.HomeAdvertising
{
    public class HomeAdvertisingUpdateVM
    {
        [Required(ErrorMessage = "Don`t be empty")]
        public IFormFile Photo { get; set; }
        public string Image { get; set; }

        [Required(ErrorMessage = "Don`t be empty")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Don`t be empty")]
        public string Description { get; set; }
    }
}
