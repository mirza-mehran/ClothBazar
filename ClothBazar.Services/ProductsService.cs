using ClothBazar.Database;
using ClothBazar.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClothBazar.Services
{
   public  class ProductsService
    {
        public void SaveProduct(Product product)
        {
            using (var context=new CBDContext())
            {
                context.Products.Add(product);
                context.SaveChanges();
            }
        }
        public List<Product> GetAllProduct()
        {
            using (var context = new CBDContext())
            {               
                return  context.Products.ToList();
            }
        }

        public Product GetProductID(int id)
        {
            using (var context = new CBDContext())
            {
               return  context.Products.Find(id);            
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
