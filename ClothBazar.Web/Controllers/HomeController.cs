using ClothBazar.Entities;
using ClothBazar.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ClothBazar.Web.HomeModelViews;

namespace ClothBazar.Web.Controllers
{
    public class HomeController : Controller
    {
        CategoriesService categoryService = new CategoriesService();

        public ActionResult Index()
        {
            HomeModelView model = new HomeModelView();
            model.Categories = categoryService.GetAllCategory();

            return View(model);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}