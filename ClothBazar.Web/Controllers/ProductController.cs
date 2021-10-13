using ClothBazar.Entities;
using ClothBazar.Services;
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
            return PartialView();
        }
        [HttpPost]
        public ActionResult Create(Product product)
        {
            if (ModelState.IsValid)
            {
                productsService.SaveProduct(product);
            }
            return RedirectToAction("Index");
        }
    }
}