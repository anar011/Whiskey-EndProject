using EndProject.Areas.Admin.ViewModels.Product;
using EndProject.Areas.Admin.ViewModels.Slider;
using EndProject.Data;
using EndProject.Helpers;
using EndProject.Models;
using EndProject.Services.Interfaces;
using EndProject.ViewModels.Product;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Drawing;


namespace EndProject.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductController : Controller
    {
        private readonly IWebHostEnvironment _env;
        private readonly AppDbContext _context;
        private readonly IProductService _productService;
        private readonly ICapacityService _capacityService;
        private readonly ICategoryService _categoryService;
        private readonly ICrudService<Product> _crudService;

        public ProductController(IProductService productService,
                               ICrudService<Product> crudService,
                               IWebHostEnvironment env,
                               ICapacityService capacityService,
                               ICategoryService categoryService, AppDbContext context)

        {

            _productService = productService;
            _crudService = crudService;
            _env = env;
            _capacityService = capacityService;
            _categoryService = categoryService;
            _context = context;

        }

        public async Task<IActionResult> Index(int page = 1, int take = 5)
        {
            List<Product> datas = await _productService.GetPaginatedDatasAsync(page, take, null);
          
            List<ProductListVM> mappedDatas = GetDatas(datas);
            
            List<Product> productCapacities = await _context.Products.Include(m => m.ProductCapacities).ToListAsync();
            int pageCount = await GetPageCountAsync(take);

            Paginate<ProductListVM> paginatedDatas = new(mappedDatas, page, pageCount);

            return View(paginatedDatas);
        }

        private async Task<int> GetPageCountAsync(int take)
        {
            var productCount = await _productService.GetCountAsync();
            return (int)Math.Ceiling((decimal)productCount / take);
        }

        private List<ProductListVM> GetDatas(List<Product> products)
        {
            List<ProductListVM> mappedDatas = new();
            foreach (var product in products)
            {
                ProductListVM productList = new()
                {
                    Id = product.Id,
                    Name = product.Name,                
                    Image = product.Image,
                    ProductCapacities = product.ProductCapacities.ToList(),
                };
                mappedDatas.Add(productList);
            }
            return mappedDatas;
        }



        [HttpGet]
        public async Task<IActionResult> Create()
        {
           
            ViewBag.capacities = await GetCapacitiesAsync();
            ViewBag.categories = await GetCategoriesAsync();
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ProductCreateVM model)
        {
            try
            {

                 ViewBag.capacities = await GetCapacitiesAsync();
                ViewBag.categories = await GetCategoriesAsync();

                if (!ModelState.IsValid) return View(model);

               

              

                Product newProduct = new();
    
                List<ProductCapacity> productCapacities = new();
                List<ProductCategory> productCategories = new();


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





                if (model.CapacityIds.Count > 0)
                {
                    foreach (var item in model.CapacityIds)
                    {
                        var convertedPrice = decimal.Parse(model.Price);
                        ProductCapacity productCapacity = new()
                        {
                            CapacityId = item,
                            Price = convertedPrice
                            
                        };
                        productCapacities.Add(productCapacity);
                    }
                    newProduct.ProductCapacities = productCapacities;
                }
                else
                {
                    ModelState.AddModelError("CapacityIds", "Don`t be empty");
                    return View();
                }

                if (model.CategoryIds.Count > 0)
                {
                    foreach (var item in model.CategoryIds)
                    {
                        ProductCategory productCategory = new()
                        {
                            CategoryId = item
                        };
                        productCategories.Add(productCategory);
                    }
                    newProduct.ProductCategories = productCategories;
                }
                else
                {
                    ModelState.AddModelError("CategoryIds", "Don`t be empty");
                    return View();
                }

                Random random = new();

                newProduct.Name = model.Name;
                newProduct.Image = model.Photo.CreateFile(_env, "assets/img");
                newProduct.Description = model.Description;
                newProduct.StockCount = model.StockCount;
                newProduct.SaleCount = model.SaleCount;

                await _crudService.CreateAsync(newProduct);
                await _crudService.SaveAsync();

                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ViewBag.error = ex.Message;
                return View();
            }
        }



        [HttpGet]
        public async Task<IActionResult> Detail(int? id, int page)
        {
            try
            {
                if (id is null) return BadRequest();
                Product dbProduct = await _productService.GetFullDataByIdAsync((int)id);
                if (dbProduct is null) return NotFound();
                ViewBag.page = page;


           
                ViewModels.Product.ProductDetailVM model = new()
                {
                    Id = dbProduct.Id,
                    Name = dbProduct.Name,
                    Description = dbProduct.Description,
                    StockCount = dbProduct.StockCount,
                    SaleCount = dbProduct.SaleCount,
                    Image = dbProduct.Image,
                    CategoryNames = dbProduct.ProductCategories,
                    CapacityNames = dbProduct.ProductCapacities,
                    

                };
                return View(model);
            }
            catch (Exception ex)
            {
                ViewBag.error = ex.Message;
                return View();
            }
        }




        [HttpGet]
        public async Task<IActionResult> Edit(int? id, int page)
        {
            try
            {
                if (id is null) return BadRequest();
                Product dbProduct = await _productService.GetFullDataByIdAsync((int)id);
                if (dbProduct is null) return NotFound();

              
           
                ViewBag.capacities = await GetCapacitiesAsync();
                ViewBag.categories = await GetCategoriesAsync();
                ViewBag.page = page;


                ProductUpdateVM model = new()
                {
                    Id = dbProduct.Id,
                    Name = dbProduct.Name,
                    Description = dbProduct.Description,
                    StockCount = dbProduct.StockCount,
                    Image = dbProduct.Image,
                    CategoryIds = dbProduct.ProductCategories.Select(c => c.Category.Id).ToList(),
                    CapacityIds = dbProduct.ProductCapacities.Select(s => s.Capacity.Id).ToList(),
                   
                };
                return View(model);
            }
            catch (Exception ex)
            {
                ViewBag.error = ex.Message;
                return View();
            }
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int? id, ProductUpdateVM model)
        {
            try
            {

                ViewBag.capacities = await GetCapacitiesAsync();
                ViewBag.categories = await GetCategoriesAsync();

                if (id is null) return BadRequest();
                Product dbProduct = await _productService.GetFullDataByIdAsync((int)id);
                if (dbProduct is null) return NotFound();



                ProductUpdateVM productUpdateVM = new()
                {
                    Image = dbProduct.Image



                };


                if (model.Photo is not null)
                {
                    if (!model.Photo.CheckFileType("image/"))
                    {
                        ModelState.AddModelError("Photo", "File type must be image");
                        return View(productUpdateVM);
                    }
                    if (!model.Photo.CheckFileSize(200))
                    {
                        ModelState.AddModelError("Photo", "Image size must be max 200kb");
                        return View(productUpdateVM);
                    }
                    string path = FileHelper.GetFilePath(_env.WebRootPath, "assets/img", productUpdateVM.Image);
                    FileHelper.DeleteFile(path);

                    dbProduct.Image = model.Photo.CreateFile(_env, "assets/img");
                }
                else
                {
                    Product newProduct = new()
                    {
                        Image = dbProduct.Image
                    };
                }




                if (model.CapacityIds.Count > 0)
                {
                    List<ProductCapacity> productCapacities = new();

                    foreach (var item in model.CapacityIds)
                    {
                        var convertedPrice = decimal.Parse(model.Price);
                        ProductCapacity productCapacity = new()
                        {
                            CapacityId = item,
                            Price= convertedPrice
                        };
                        productCapacities.Add(productCapacity);
                    }
                    dbProduct.ProductCapacities = productCapacities;
                }

                if (model.CategoryIds.Count > 0)
                {
                    List<ProductCategory> productCategories = new();

                    foreach (var item in model.CategoryIds)
                    {
                        ProductCategory productCategory = new()
                        {
                            CategoryId = item
                        };
                        productCategories.Add(productCategory);
                    }
                    dbProduct.ProductCategories = productCategories;
                }

                dbProduct.Name = model.Name;
                dbProduct.Description = model.Description;
               
                dbProduct.StockCount = model.StockCount;
       

                await _crudService.SaveAsync();

                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ViewBag.error = ex.Message;
                return View();
            }
        }




        [HttpPost]
        public async Task<IActionResult> Delete(int? id)
        {
            try
            {
                if (id is null) return BadRequest();
                Product dbProduct = await _productService.GetByIdAsync((int)id);
                if (dbProduct is null) return NotFound();

                string path = FileHelper.GetFilePath(_env.WebRootPath, "assets/img", dbProduct.Image);

                _crudService.Delete(dbProduct);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ViewBag.error = ex.Message;
                return View();
            }
        }




        private async Task<SelectList> GetCapacitiesAsync()
        {
            IEnumerable<Capacity> capacities = await _capacityService.GetAllAsync();
            return new SelectList(capacities, "Id", "Name");
        }
        private async Task<SelectList> GetCategoriesAsync()
        {
            IEnumerable<Category> categories = await _categoryService.GetAllAsync();
            return new SelectList(categories, "Id", "Name");
        }



        public async Task<IActionResult> GetAllProduct(int? id)
        {
            List<Product> products = await _productService.GetFullDataAsync();

            return PartialView("_ProductsPartial", products);
        }


        //public async Task<IActionResult> GetProductByAuthor(int? id)
        //{
        //    List<Product> products = await _context.ProductCategories.Include(m => m.Category).Include(m => m.Product).Where(m => m.CategoryId == id).Select(m => m.Product).ToListAsync();

        //    return PartialView("_ProductsPartial", products);
        //}


    }
}
