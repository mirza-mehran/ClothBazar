using ClothBazar.Services;
using ClothBazar.Web.HomeModelViews;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ClothBazar.Web.Controllers
{
    public class ShopController : Controller
    {
        ProductsService productsService = new ProductsService();
        // GET: Shop
        public ActionResult Checkout()
        {
            CheckoutViewModels model = new CheckoutViewModels();

            var CartProductsCookiee = Request.Cookies["CartProducts"];

            if (CartProductsCookiee !=null)
            {
                //var productIDs = CartProductsCookiee.Value;
                //var ids = productIDs.Split('-');
                //List<int> pIDs = ids.Select(x => int.Parse(x)).ToList();

                model.CartProductIDs = CartProductsCookiee.Value.Split('-').Select(x => int.Parse(x)).ToList();
                model.CartProducts = productsService.GetProducts(model.CartProductIDs);
            }
            return View(model);
        }
    }
}