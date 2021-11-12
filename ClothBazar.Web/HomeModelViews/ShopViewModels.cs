using ClothBazar.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ClothBazar.Web.HomeModelViews
{
    public class CheckoutViewModels
    {
        public List<Product> CartProducts { get; set; }
        public List<int> CartProductIDs { get; set; }
    }
    public class ShopViewModels
    {
        public int MaximumPrice { get; set; }
        public List<Category> FeaturedCategory { get; set; }
        public List<Product> Products { get; set; }
        public int? SortBy { get;  set; }
        public int? CategoryID { get;  set; }
        public Pager Pager { get; set; }
        public string SearchTerm { get;  set; }
    }
    public class FilterProductsViewModels
    {
        public List<Product> Products { get; set; }
        public Pager Pager { get; set; }
        public int? SortBy { get;  set; }
        public int? CategoryID { get;  set; }
        public string SearchTerm { get; set; }
    }
}