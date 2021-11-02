using ClothBazar.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ClothBazar.Web.HomeModelViews
{
    public class CategorySearchViewModels
    {
        public List<Category> Categories
        {
            get;set;
        }
        public string SearchTerm
        {
            get;set;
        }
    }
    public class CategoryViewModels
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string ImageURL { get; set;}
        public bool isFeatured { get; set; }

    }

    public class EditCategoryViewModels
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string ImageURL
        {
            get; set;
        }
        public bool isFeatured { get; set; }
    }

}