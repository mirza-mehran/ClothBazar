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
    public class ConfigurationService
    {
        //public static ConfigurationService PublicConfigInstance {
        //    get {
        //        if (PrivateConfigInstance == null)
        //            PrivateConfigInstance = new ConfigurationService();
        //        return PrivateConfigInstance;
        //    }
        //}
        //private static ConfigurationService PrivateConfigInstance { get; set; }

        //private ConfigurationService()
        //{

        //}
        public Config GetConfig(string key)
        {
            using (var context = new CBDContext())
            {
                return context.Configurations.Find(key);
            }
        }
    }
}
