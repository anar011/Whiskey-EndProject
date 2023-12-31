﻿using Microsoft.AspNetCore.Identity;

namespace EndProject.Models
{
    public class AppUser: IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public bool IsRemember { get; set; }
        public List<WishlistItem> WishlistItems { get; set; }
        public AppUser()
        {
            WishlistItems = new();
        }
    }
}
