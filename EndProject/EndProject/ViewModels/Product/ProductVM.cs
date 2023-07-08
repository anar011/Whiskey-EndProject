﻿

using EndProject.Models;

namespace EndProject.ViewModels.Product
{
    public class ProductVM
    {
        public int Id { get; set; }
        public string Image { get; set; }
        public string Name { get; set; }

        public List<ProductCapacity> ProductCapacities { get; set; }

    }
}
