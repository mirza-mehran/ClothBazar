using ClothBazar.Entities;
using ClothBazar.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ClothBazar.Web.Controllers
{
    public class CategoryController : Controller
    {
        CategoriesService categoryService = new CategoriesService();
        // GET: Category
        [HttpGet]
        public ActionResult Index()
        {
            List<Category>   cate = categoryService.GetAllCategory();
            return View(cate);
        }
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(Category category)
        {
            if (ModelState.IsValid)
            {
                categoryService.SaveCategory(category);
            }
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            var data = categoryService.GetCategoryID(id);
            return View(data);
        }
        [HttpPost]
        public ActionResult Edit(Category category)
        {
            if (ModelState.IsValid)
            {
                categoryService.GetCategoryUpdate(category);
            }
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            var data = categoryService.GetCategoryID(id);
            return View(data);
        }
        [HttpPost]
        public ActionResult Delete(Category category)
        {
            categoryService.GetCategoryDelete(category.ID);           
            return RedirectToAction("Index");
        }
    }
}