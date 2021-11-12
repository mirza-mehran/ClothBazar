using ClothBazar.Entities;
using ClothBazar.Services;
using ClothBazar.Web.HomeModelViews;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ClothBazar.Web.Controllers
{
    public class ProductController : Controller
    {
       // ProductsService productsService = new ProductsService();
      //  CategoriesService categoryservice = new CategoriesService();

        // GET: Product
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ProductList(string search,int? pageNo)
        {
            var pageSize = ConfigurationService.Instance.PageSize();
            ProductSearchViewModels model = new ProductSearchViewModels();
            model.SearchTerm = search;
            //check page value otherwise 1 value assign
            pageNo = pageNo.HasValue ? pageNo.Value > 0 ? pageNo.Value : 1 : 1;

            //Example one line if condition
            //if (pageNo.HasValue)
            //{
            //    if (pageNo.Value > 0)
            //    {
            //        model.pageNo = pageNo.Value;
            //    }
            //    else
            //    {
            //        model.pageNo = 1;
            //    }
            //}
            //else
            //{
            //   model.pageNo = 1;
            //}

    
            var totalRecords = ProductsService.Instance.GetAllProductCount(search);
            model.Products = ProductsService.Instance.GetAllProduct(search, pageNo.Value, pageSize);

            model.Pager = new Pager(totalRecords,pageNo, pageSize );
        
            return PartialView(model);
        }

        public ActionResult Create()
        {
            ProductViewModels model = new ProductViewModels();
         //   CategoriesService categoryservice = new CategoriesService();
            model.AvailableCategories = CategoriesService.Instance.GetAllCategory();
            
            return PartialView(model);
        }
        [HttpPost]
        public ActionResult Create(ProductViewModels model)
        {
            if (ModelState.IsValid)
            {
               // CategoriesService categoryservice = new CategoriesService();

                var newProduct = new Product();
                newProduct.Name = model.Name;
                newProduct.Description = model.Description;
                newProduct.Price = model.Price;
                newProduct.Category = CategoriesService.Instance.GetCategoryID(model.CategoryID);
                newProduct.ImageURL = model.ImageURL;

                ProductsService.Instance.SaveProduct(newProduct);
            }
            return RedirectToAction("ProductList");
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            EditProductViewModel model = new EditProductViewModel();
            
            var product = ProductsService.Instance.GetProductID(id);
            model.ID = product.ID;
            model.Name = product.Name;
            model.Description = product.Description;
            model.Price = product.Price;
            model.CategoryID = product.Category != null ? product.Category.ID :0;
            model.ImageURL = product.ImageURL;

            model.AvailableCategories = CategoriesService.Instance.GetAllCategory();

            return PartialView(model); 
        }
        [HttpPost]
        public ActionResult Edit(EditProductViewModel model)
        {
            if (ModelState.IsValid)
            {
               // CategoriesService categoryservice = new CategoriesService();

                var existingProduct = ProductsService.Instance.GetProductID(model.ID);
                existingProduct.Name = model.Name;
                existingProduct.Description = model.Description;
                existingProduct.Price = model.Price;
                existingProduct.CategoryID = model.CategoryID;
              //  existingProduct.ImageURL = model.ImageURL;

                if (!string.IsNullOrEmpty(model.ImageURL))
                {
                    existingProduct.ImageURL = model.ImageURL;
                }
                ProductsService.Instance.GetProductUpdate(existingProduct);
            }
            return RedirectToAction("ProductList");
        }

        [HttpPost]
        public ActionResult Delete(int id)
        {
            ProductsService.Instance.GetProductDelete(id);
            return RedirectToAction("ProductList");
        }

        [HttpGet]
        public ActionResult Details(int id)
        {
            ProductViewModel model = new ProductViewModel();

            model.Product = ProductsService.Instance.GetProductID(id);
            return View(model);
        }

    }
}