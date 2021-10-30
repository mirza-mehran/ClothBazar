using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ClothBazar.Web.HomeModelViews
{
    public class CategoryViewModels
    {
        public string Name
        {
            get; set;
        }
        public string Description
        {
            get; set;
        }
        public decimal Price
        {
            get; set;
        }

        public int CategoryID
        {
            get; set;
        }
    }
}