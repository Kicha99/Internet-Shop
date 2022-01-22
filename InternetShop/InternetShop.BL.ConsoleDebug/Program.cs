using InternetShop.BL.Interfaces;
using InternetShop.DAL;
using System;
using System.Collections.Generic;

namespace InternetShop.BL.ConsoleDebug
{
    class Program
    {
        static void Main(string[] args)
        {
            var dal = new DataSource();
            IBusinessService bl = new BusinessService(dal);

            IEnumerable<ModelCategory> mc = bl.GetCategories();
        }
    }
}
