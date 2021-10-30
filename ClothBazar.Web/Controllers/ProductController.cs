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
        // GET: Product
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ProductList(string search)
        {
            var data = productsService.GetAllProduct();
            if (!string.IsNullOrEmpty(search))
            {
                data = data.Where(x => x.Name.ToLower().Contains(search.ToLower())).ToList();
            }
            return PartialView(data);
        }
        public ActionResult Create()
        {
            CategoriesService categoryservice = new CategoriesService();
            var categories = categoryservice.GetAllCategory();
            return PartialView(categories);
        }
        [HttpPost]
        public ActionResult Create(CategoryViewModels model)
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
            var data = productsService.GetProductID(id);
            return PartialView(data); 
        }
        [HttpPost]
        public ActionResult Edit(Product product)
        {
            if (ModelState.IsValid)
            {
                productsService.GetProductUpdate(product);
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