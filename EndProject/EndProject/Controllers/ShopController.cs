﻿using EndProject.Helpers;
using EndProject.Models;
using EndProject.Services.Interfaces;
using EndProject.ViewModels.Product;
using EndProject.ViewModels.Shop;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.AspNetCore.Mvc;

namespace EndProject.Controllers
{
    public class ShopController : Controller
    {
        private readonly IProductService _productService;
        private readonly ICategoryService _categoryService;
        private readonly ICrudService<ProductComment> _crudService;
        private readonly ILayoutService _layoutService;


        public ShopController(IProductService productService,
                      ICategoryService categoryService,
                      ILayoutService layoutService,
                      ICrudService<ProductComment> crudService)
        {
            _productService = productService;
            _categoryService = categoryService;
            _layoutService = layoutService;
            _crudService = crudService;
  
        }
        public async Task<IActionResult> Index(int page = 1, int take = 6, int? categoryId = null)
        {
            List<Product> datas = await _productService.GetPaginatedDatasAsync(page, take, categoryId);
            List<ProductVM> mappedDatas = GetDatas(datas);
            int pageCount = 0;
            ViewBag.catId = categoryId;
         

            if (categoryId != null)
            {
                pageCount = await GetPageCountAsync(take, categoryId);
            }
        

         

            Paginate<ProductVM> paginatedDatas = new(mappedDatas, page, pageCount);

            ShopVM model = new()
            {
                Products = await _productService.GetFullDataAsync(),
                Categories = await _categoryService.GetAllAsync(),
                PaginateDatas = paginatedDatas
            };
            return View(model);
        }


        private async Task<int> GetPageCountAsync(int take, int? catId)
        {
            int prodCount = 0;
            if (catId is not null)
            {
                prodCount = await _productService.GetProductsCountByCategoryAsync(catId);
            }
        
     
            if (catId == null )
            {
                prodCount = await _productService.GetCountAsync();
            }

            return (int)Math.Ceiling((decimal)prodCount / take);
        }

        private List<ProductVM> GetDatas(List<Product> products)
        {
            List<ProductVM> mappedDatas = new();
            foreach (var product in products)
            {
                ProductVM productList = new()
                {
                    Id = product.Id,
                    Name = product.Name,
                    Price = product.Price,
                    Image = product.Image
                
                };
                mappedDatas.Add(productList);
            }
            return mappedDatas;
        }

        [HttpGet]
        public async Task<IActionResult> GetProductsByCategory(int? id, int page = 1, int take = 6)
        {
            if (id is null) return BadRequest();
            ViewBag.catId = id;

            var products = await _productService.GetProductsByCategoryIdAsync(id, page, take);

            int pageCount = await GetPageCountAsync(take, (int)id);

            Paginate<ProductVM> model = new(products, page, pageCount);

            return PartialView("_ProductListPartial", model);
        }

        [HttpGet]
        public async Task<IActionResult> GetProducts()
        {
            var products = await _productService.GetDatasAsync();
            return PartialView("_ProductListPartial", products);
        }

        [HttpGet]
        public async Task<IActionResult> ProductDetail(int? id)
        {
            try
            {
                if (id is null) return BadRequest();
                var dbProduct = await _productService.GetFullDataByIdAsync((int)id);
                if (dbProduct is null) return NotFound();

                ProductDetailVM model = new()
                {
                    Id = dbProduct.Id,
                    ProductName = dbProduct.Name,
                    Price = dbProduct.Price,
                    Image = dbProduct.Image,
                    ProductCategories = dbProduct.ProductCategories,
                    Description = dbProduct.Description,
                    SectionBgs = _layoutService.GetSectionBackgroundImages(),
                    RelatedProducts = await _productService.GetRelatedProducts(),
                    ProductCommentVM = new(),
                    ProductComments = dbProduct.ProductComments
                };

                return View(model);
            }
            catch (Exception ex)
            {
                ViewBag.error = ex.Message;
                return View();
            }
        }


    }
}
