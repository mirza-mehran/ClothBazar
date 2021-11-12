using ClothBazar.Database;
using ClothBazar.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace ClothBazar.Services
{
   public  class ProductsService
    {
        #region Singleton
        public static ProductsService Instance {
            get {
                if (instance == null)
                    instance = new ProductsService();
                return instance;
            }
        }

        private static ProductsService instance { get; set; }

     

        private ProductsService()
        {

        }


        #endregion

        public List<Product> SearchProducts(string searchTerm, int? minimumPrice, int? maximumPrice, int? categoryID, int? sortBy,int pageNo,int pageSize)
        {
            using (var context = new CBDContext())
            {
                var products = context.Products.ToList();

                if (categoryID.HasValue)
                {
                    products = products.Where(x => x.Category.ID == categoryID.Value).ToList();
                }
                if (!string.IsNullOrEmpty(searchTerm))
                {
                    products = products.Where(x => x.Name.ToLower().Contains(searchTerm.ToLower())).ToList();
                }
                if (minimumPrice.HasValue)
                {
                    products = products.Where(x => x.Price >= minimumPrice.Value).ToList();
                }
                if (maximumPrice.HasValue)
                {
                    products = products.Where(x => x.Price <= maximumPrice.Value).ToList();
                }

                if (sortBy.HasValue)
                {
                    switch (sortBy.Value)
                    {
                        case 2:
                            products = products.OrderByDescending(x => x.Name).ToList();
                            break;
                        case 3:
                            products = products.OrderBy(x => x.Price).ToList();
                            break;
                        default:
                            products = products.OrderByDescending(x => x.Price).ToList();
                            break;
                    }
                }
                return products.Skip((pageNo -1) * pageSize).Take(pageSize).ToList();
            }
        }
        public int SearchProductsCount(string searchTerm, int? minimumPrice, int? maximumPrice, int? categoryID, int? sortBy)
        {
            using (var context = new CBDContext())
            {
                var products = context.Products.ToList();

                if (categoryID.HasValue)
                {
                    products = products.Where(x => x.Category.ID == categoryID.Value).ToList();
                }
                if (!string.IsNullOrEmpty(searchTerm))
                {
                    products = products.Where(x => x.Name.ToLower().Contains(searchTerm.ToLower())).ToList();
                }
                if (minimumPrice.HasValue)
                {
                    products = products.Where(x => x.Price >= minimumPrice.Value).ToList();
                }
                if (maximumPrice.HasValue)
                {
                    products = products.Where(x => x.Price <= maximumPrice.Value).ToList();
                }

                if (sortBy.HasValue)
                {
                    switch (sortBy.Value)
                    {
                        case 2:
                            products = products.OrderByDescending(x => x.Name).ToList();
                            break;
                        case 3:
                            products = products.OrderBy(x => x.Price).ToList();
                            break;
                        default:
                            products = products.OrderByDescending(x => x.Price).ToList();
                            break;
                    }
                }
                return products.Count;
            }
        }

        public void SaveProduct(Product product)
        {
            using (var context=new CBDContext())
            {
                context.Entry(product.Category).State = System.Data.Entity.EntityState.Unchanged;

                context.Products.Add(product);
                context.SaveChanges();
            }
        }
        public int GetMaximumPrice()
        {
            using (var context = new CBDContext())
            {
                return (int)(context.Products.Max(x => x.Price));
            }
        }
        public List<Product> GetAllProduct(int pageNo)
        {
            using (var context = new CBDContext())
            {
                int pageSize = 5; //int.Parse(ConfigurationService.Instance.GetConfig("ListingPageNumber").Value);
                return context.Products.OrderBy(x => x.Name).Skip( (pageNo-1) * pageSize).Take(pageSize).Include(x => x.Category).ToList();
               // return context.Products.Include(x => x.Category).ToList();

            }
        }
        public List<Product> GetAllProduct(string search, int pageNo,int pageSize)
        {
            
            using (var context = new CBDContext())
            {
                if (!string.IsNullOrEmpty(search))
                {
                    return context.Products.Where(x => x.Name != null && x.Name.ToLower()
                    .Contains(search.ToLower()))
                    .OrderBy(x => x.Name)
                    .Skip((pageNo - 1) * pageSize)
                    .Take(pageSize)
                    .Include(x => x.Category)
                    .ToList();
                }
                else
                {
                    return context.Products
                        .OrderBy(x => x.Name)
                        .Skip((pageNo - 1) * pageSize)
                        .Take(pageSize)
                        .Include(x => x.Category)
                        .ToList();
                }
            }
        }
        public int GetAllProductCount(string search)
        {

            using (var context = new CBDContext())
            {
                if (!string.IsNullOrEmpty(search))
                {
                    return context.Products.Where(x => x.Name != null && x.Name.ToLower()
                    .Contains(search.ToLower()))
                    .Count();
                }
                else
                {
                    return context.Products.Count();
                }
            }
        }
        //Use in Widgets Service Method overloading
        public List<Product> GetAllProduct(int pageNo,int pageSize)
        {
            using (var context = new CBDContext())
            {
                return context.Products.OrderByDescending(x => x.Name).Skip((pageNo - 1) * pageSize).Take(pageSize).Include(x => x.Category).ToList();
            }
        }
        //Use in Widgets
        public List<Product> GetLatestProduct(int numberOfProducts)
        {
            using (var context=new CBDContext())
            {
                return context.Products.OrderByDescending(x => x.ID).Take(numberOfProducts).Include(x => x.Category).ToList();
            }
        }
        //Use in Widgets Service 
        public List<Product> GetProductByCategory(int categoryID, int pageSize)
        {
            using (var context = new CBDContext())
            {
                return context.Products.Where(x => x.Category.ID == categoryID).OrderByDescending(x => x.Name).Take(pageSize).Include(x => x.Category).ToList();
            }
        }

        public Product GetProductID(int id)
        {
            using (var context = new CBDContext())
            {
               return  context.Products.Where(x => x.ID ==id).Include(x => x.Category).FirstOrDefault();            
            }
        }

        public void GetProductUpdate(Product product)
        {
            using (var context = new CBDContext())
            {
                context.Entry(product).State = System.Data.Entity.EntityState.Modified;
                context.SaveChanges();
            }
        }
        public void GetProductDelete(int id)
        {
            using (var context = new CBDContext())
            {
                var product = context.Products.Find(id);
                context.Products.Remove(product);
                context.SaveChanges();
            }
        }
        public List<Product> GetProducts(List<int> IDs)
        {
            using (var context = new CBDContext())
            {
                return context.Products.Where(x => IDs.Contains(x.ID)).ToList();
            }
        }
    }
}
