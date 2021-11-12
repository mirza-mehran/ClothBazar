using ClothBazar.Services;
using ClothBazar.Web.Code;
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
       public ActionResult Index(string searchTerm,int? minimumPrice,int? maximumPrice,int? categoryID,int? sortBy, int? pageNo)
        {
            var pageSize = ConfigurationService.Instance.ShopPageSize();
            ShopViewModels model = new ShopViewModels();
            model.SearchTerm = searchTerm;
            model.FeaturedCategory = CategoriesService.Instance.GetFeaturedCategories();
            model.MaximumPrice = ProductsService.Instance.GetMaximumPrice();

            pageNo = pageNo.HasValue ? pageNo.Value > 0 ? pageNo.Value : 1 : 1;

            model.SortBy = sortBy;
            model.CategoryID = categoryID;
    

            int totalCount = ProductsService.Instance.SearchProductsCount(searchTerm, minimumPrice, maximumPrice, categoryID, sortBy);
            model.Products = ProductsService.Instance.SearchProducts(searchTerm, minimumPrice, maximumPrice, categoryID, sortBy, pageNo.Value, pageSize);
            model.Pager = new Pager(totalCount, pageNo, pageSize);
            //always ENUM property declare in solution not dec in web 
            //var sort = (SortByEnums)sortBy.Value;
            //switch (sort)
            //{
            //    case SortByEnums.Default:
            //        break;
            //    case SortByEnums.Popularity:
            //        break;
            //    case SortByEnums.PriceLowToHigh:
            //        break;
            //    case SortByEnums.PriceHighToLow:
            //        break;
            //    default:
            //        break;
            //}
            return View(model);
        }
        public ActionResult FilterProducts(string searchTerm, int? minimumPrice, int? maximumPrice, int? categoryID, int? sortBy, int? pageNo)
        {
            var pageSize = ConfigurationService.Instance.ShopPageSize();

            FilterProductsViewModels model = new FilterProductsViewModels();

            model.SearchTerm = searchTerm;
            pageNo = pageNo.HasValue ? pageNo.Value > 0 ? pageNo.Value : 1 : 1;

            model.SortBy = sortBy;
            model.CategoryID = categoryID;
           
            int totalCount = ProductsService.Instance.SearchProductsCount(searchTerm, minimumPrice, maximumPrice, categoryID, sortBy);

            model.Products = ProductsService.Instance.SearchProducts(searchTerm, minimumPrice, maximumPrice, categoryID, sortBy, pageNo.Value, pageSize);

            model.Pager = new Pager(totalCount, pageNo, pageSize);

            return PartialView(model);
        }

        
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
                model.CartProducts = ProductsService.Instance.GetProducts(model.CartProductIDs);
            }
            return View(model);
        }
    }
}