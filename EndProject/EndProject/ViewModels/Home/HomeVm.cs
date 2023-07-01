﻿using EndProject.Models;

namespace EndProject.ViewModels.Home
{
    public class HomeVM
    {
        public IEnumerable<Slider> Sliders { get; set; }
        public SpecialCollection SpecialCollection { get; set; }
        public IEnumerable<HomeAddvertising> HomeAddvertisings { get; set; }
        public string Image { get; set; }
        public string Name { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int AuthorId { get; set; }
        public Author Author { get; set; }
        public List<BlogInfo> BlogInfos { get; set; }
        public List<BlogElement> BlogElements { get; set; }

    }
}
