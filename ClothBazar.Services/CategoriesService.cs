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
   public  class CategoriesService
    {
        #region Singleton
        public static CategoriesService Instance
        {
            get
            {
                if (instance == null)
                    instance = new CategoriesService();
                return instance;
            }
        }

        private static CategoriesService instance { get; set; }

        private CategoriesService()
        {

        }
        #endregion
        public void SaveCategory(Category category)
        {
            using (var context=new CBDContext())
            {
                context.Categories.Add(category);
                context.SaveChanges();
            }
        }
        public List<Category> GetAllCategory()
        {
            using (var context = new CBDContext())
            {               
                return  context.Categories.Include(x => x.Products).ToList();
            }
        }
        public List<Category> GetFeaturedCategories()
        {
            using (var context = new CBDContext())
            {
                return context.Categories.Where(x => x.IsFeatured && x.ImageURL !=null).ToList();
            }
        }
        public Category GetCategoryID(int id)
        {
            using (var context = new CBDContext())
            {
               return  context.Categories.Find(id);            
            }
        }

        public void GetCategoryUpdate(Category category)
        {
            using (var context = new CBDContext())
            {
                context.Entry(category).State = System.Data.Entity.EntityState.Modified;
                context.SaveChanges();
            }
        }
        public void GetCategoryDelete(int id)
        {
            using (var context = new CBDContext())
            {
                var category = context.Categories.Find(id);
                context.Categories.Remove(category);
                context.SaveChanges();
            }
        }
    }
}
