﻿using System.ComponentModel.DataAnnotations;

namespace EndProject.Areas.Admin.ViewModels.SpecialCollection
{
    public class SpecialCollectionCreateVM
    {

        [Required(ErrorMessage = "Don`t be empty")]
        public IFormFile Photo { get; set; }

        [Required(ErrorMessage = "Don`t be empty")]
        public string Title { get; set; }


        [Required(ErrorMessage = "Don`t be empty")]
        public string Description { get; set; }
    }
}
