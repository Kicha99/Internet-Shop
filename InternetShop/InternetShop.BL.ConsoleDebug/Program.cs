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

            //IEnumerable<ModelCategory> mc = bl.GetCategories();
            var p = bl.GetProductById(6);
            p.Price = 12343;
            //bl.EditProduct(p); //Меняет значение CategoryId на null
            IEnumerable<ModelProduct> mp = bl.GetProductsByCategoryId(16);
            //ModelProduct p = bl.GetProductById(26);
            //IEnumerable<ModelOrder> orders = bl.GetOrders();
            //IEnumerable<ModelCategory> child = bl.GetChildCategoriesById(27);
            IEnumerable<ModelProduct> sortProductsByPrice = bl.SortProductsByPriceAscending(mp);
            //ModelOrder order = bl.GetOrderById(1);
            
            //bl.AddProductInOrder(p, order);
            //ModelProduct pr = new ModelProduct();
            //pr.Id = 25;
            //pr.Description = "123213";
            //pr.Price = 120;
            //pr.Title = "SSASSADA";
            //pr.Price = 2500;
            //bl.EditProduct(pr);
            //bl.AddProduct(pr);
            //var res = bl.GetProductById(pr.Id);

            //foreach (var item in child)
            //{
            //    Console.WriteLine(item.Id);
            //}

            //foreach (var item in orders)
            //{
            //    Console.WriteLine(item.Id);
            //}
            //Console.WriteLine(p.Title);

            //foreach (var item in mp)
            //{
            //    Console.WriteLine(item.Title);
            //}

            //foreach (ModelCategory item in mc)
            //{
            //    Console.WriteLine(item.Id);
            //    Console.WriteLine(item.Title);
            //}

            Console.ReadLine();
        }
    }
}
