using ClothBazar.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
        public Pager Pager { get; set; }
    }
    public class CategoryViewModels
    {
        [Required]
        [MinLength(5), MaxLength(50)]
        public string Name { get; set; }
        [Required]
        [MaxLength(500)]
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