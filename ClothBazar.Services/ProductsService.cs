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
        public void SaveProduct(Product product)
        {
            using (var context=new CBDContext())
            {
                context.Entry(product.Category).State = System.Data.Entity.EntityState.Unchanged;

                context.Products.Add(product);
                context.SaveChanges();
            }
        }
        public List<Product> GetAllProduct()
        {
           // var context = new CBDContext();
           //return context.Products.ToList();

            using (var context = new CBDContext())
            {
                return context.Products.Include(x => x.Category).ToList();
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
    }
}
