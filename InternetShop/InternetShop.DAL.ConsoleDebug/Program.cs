using InternetShop.DAL.Interfaces;
using InternetShop.DAL.Interfaces.DTO;
using System;

namespace InternetShop.DAL.ConsoleDebug
{
    class Program
    {
        static void Main(string[] args)
        {
            IDataSource ds = new DataSource();
            //1. Add Product
            ProductDTO product = new ProductDTO()
            {
                 Description = "22222",
                 Price = 2.3M,
                 Title = "Arduino"
            };
            ds.AddProduct(product);
        }
    }
}
