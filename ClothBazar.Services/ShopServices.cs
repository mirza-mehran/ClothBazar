
using ClothBazar.Database;
using ClothBazar.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClothBazar.Services
{
   public  class ShopServices
    {
        #region Singleton
        public static ShopServices Instance
        {
            get
            {
                if (instance == null)
                    instance = new ShopServices();
                return instance;
            }
        }

        private static ShopServices instance { get; set; }



        private ShopServices()
        {

        }


        #endregion

        public int SaveOrder(Order order)
        {
            using (var context=new CBDContext())
            {
                context.Orders.Add(order);
               return context.SaveChanges();
            }
        }
    }
}
