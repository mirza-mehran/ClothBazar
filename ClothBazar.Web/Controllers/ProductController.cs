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
        ProductsService productsService = new ProductsService();
        CategoriesService categoryservice = new CategoriesService();

        // GET: Product
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ProductList(string search)
        {
            ProductSearchViewModels model = new ProductSearchViewModels();

            model.Products = productsService.GetAllProduct();
            if (!string.IsNullOrEmpty(search))
            {
                model.SearchTerm = search;

                model.Products = model.Products.Where(x => x.Name !=null && x.Name.ToLower().Contains(search.ToLower())).ToList();
            }
            return PartialView(model);
        }
        public ActionResult Create()
        {
            ProductViewModels model = new ProductViewModels();
            CategoriesService categoryservice = new CategoriesService();
            model.AvailableCategories = categoryservice.GetAllCategory();
            
            return PartialView(model);
        }
        [HttpPost]
        public ActionResult Create(ProductViewModels model)
        {
            if (ModelState.IsValid)
            {
                CategoriesService categoryservice = new CategoriesService();

                var newProduct = new Product();
                newProduct.Name = model.Name;
                newProduct.Description = model.Description;
                newProduct.Price = model.Price;
                newProduct.Category = categoryservice.GetCategoryID(model.CategoryID);
                productsService.SaveProduct(newProduct);
            }
            return RedirectToAction("ProductList");
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            EditProductViewModel model = new EditProductViewModel();
            
            var product = productsService.GetProductID(id);
            model.ID = product.ID;
            model.Name = product.Name;
            model.Description = product.Description;
            model.Price = product.Price;
            model.CategoryID = product.Category != null ? product.Category.ID :0;
            model.AvailableCategories = categoryservice.GetAllCategory();

            return PartialView(model); 
        }
        [HttpPost]
        public ActionResult Edit(EditProductViewModel model)
        {
            if (ModelState.IsValid)
            {
                CategoriesService categoryservice = new CategoriesService();

                var existingProduct = productsService.GetProductID(model.ID);
                existingProduct.Name = model.Name;
                existingProduct.Description = model.Description;
                existingProduct.Price = model.Price;
                existingProduct.Category = categoryservice.GetCategoryID(model.CategoryID);
                
                productsService.GetProductUpdate(existingProduct);
            }
            return RedirectToAction("ProductList");
        }

        [HttpPost]
        public ActionResult Delete(int id)
        {
            productsService.GetProductDelete(id);
            return RedirectToAction("ProductList");
        }
    }
}