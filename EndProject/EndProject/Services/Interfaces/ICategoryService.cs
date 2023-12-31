﻿using EndProject.Models;

namespace EndProject.Services.Interfaces
{
    public interface ICategoryService
    {
        Task<IEnumerable<Category>> GetAllAsync();
        Task<Category> GetByIdAsync(int? id);
        bool CheckByName(string name);
    }
}
