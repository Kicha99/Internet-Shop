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
            IEnumerable<ModelProduct> mp = bl.GetProductsByCategoryId(20);

            foreach (var item in mp)
            {
                Console.WriteLine(item.Title);
            }

            //foreach (ModelCategory item in mc)
            //{
            //    Console.WriteLine(item.Id);
            //    Console.WriteLine(item.Title);
            //}

            Console.ReadLine();
        }
    }
}
