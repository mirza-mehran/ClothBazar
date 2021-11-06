﻿using ClothBazar.Entities;
using ClothBazar.Services;
using ClothBazar.Web.HomeModelViews;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ClothBazar.Web.Controllers
{
    public class CategoryController : Controller
    {
        // GET: Category
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult CategoryList(string search)
        {
            CategorySearchViewModels model = new CategorySearchViewModels();
            model.Categories = CategoriesService.Instance.GetAllCategory();


            if (!string.IsNullOrEmpty(search))
            {
                model.SearchTerm = search;
                model.Categories = model.Categories.Where(x => x.Name !=null && x.Name.ToLower().Contains(search.ToLower())).ToList();
            }
            return PartialView("_CategoryList", model);
        }

        [HttpGet]
        public ActionResult Create()
        {
            CategoryViewModels model = new CategoryViewModels();
        //    CategoriesService categoryservice = new CategoriesService();
          
            return PartialView(model);
        }
        [HttpPost]
        public ActionResult Create(CategoryViewModels model)
        {
            if (ModelState.IsValid)
            {

                var newcategory = new Category();
                newcategory.Name = model.Name;
                newcategory.Description = model.Description;
                newcategory.ImageURL = model.ImageURL;
                newcategory.IsFeatured = model.isFeatured;

                CategoriesService.Instance.SaveCategory(newcategory);
            }
            return RedirectToAction("CategoryList");
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            EditCategoryViewModels model = new EditCategoryViewModels();

            var category = CategoriesService.Instance.GetCategoryID(id);

            model.ID = category.ID;
            model.Name = category.Name;
            model.Description = category.Description;
            model.ImageURL = category.ImageURL;
            model.isFeatured = category.IsFeatured;

            return PartialView(model);
        }
        [HttpPost]
        public ActionResult Edit(EditCategoryViewModels model)
        {
           
            if (ModelState.IsValid)
            {

                var existingCategory = CategoriesService.Instance.GetCategoryID(model.ID);
                existingCategory.Name = model.Name;
                existingCategory.Description = model.Description;
                existingCategory.ImageURL = model.ImageURL;
                existingCategory.IsFeatured = model.isFeatured;

                CategoriesService.Instance.GetCategoryUpdate(existingCategory);

            }
            return RedirectToAction("CategoryList");
        }

        [HttpPost]
        public ActionResult Delete(int id)
        {
            CategoriesService.Instance.GetCategoryDelete(id);
            return RedirectToAction("CategoryList");
        }
    }
}