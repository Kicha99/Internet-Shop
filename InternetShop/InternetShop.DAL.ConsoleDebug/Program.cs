using InternetShop.DAL.Interfaces;
using InternetShop.DAL.Interfaces.DTO;
using System.Linq;
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
                Description = "CheckDelete",
                Price = 2123.3M,
                Title = "Arduino"
            };
            //ds.AddProduct(product);
            //2. Remove Product
            ProductDTO delProduct = new ProductDTO()
            {
                Description = "CheckDelete",
                Price = 2123.4M,
                Title = "Arduino",
                Id = 1
            };
            //ds.AddProduct(delProduct);
            //ds.RemoveProductById(13);
            /*ds.RemoveProduct(delProduct);*/ //Корректировка Id?
            //3.Edit Product
            product.Price = 22.7M;
            product.Description = "Edit";
            product.Id = 12;
            //ds.EditProduct(product);//???

            //1.Add Category
            CategoryDTO category = new CategoryDTO()
            {
                Title = "Cars",

            };
            //ds.AddCategory(category);
            //2.Remove Category
            //ds.AddCategory(category);
            //category.Id = 6;
            //ds.RemoveCategoryById(13);
            //ds.GetProductsByCategoryId();
            //3.Edit Category
            //...
            //1. Add Order
            //OrderDTO order = new OrderDTO()
            //{
            //    ClientId = 1,
            //};
            //ds.AddOrder(order);
            //1. Remove Order and Edit Order?

            //Get top categories
            var root = ds.GetTopCategory();

            //var order = ds.GetOrders();
            //var child = ds.GetChildCategoriesById(16);
            //category.Title = "as123 asdasfa";
            //category.Id = 13;
            //ds.EditCategory(category);

            //OrderDTO order = ds.GetOrderById(1);
            //ProductDTO product1 = ds.GetProductById(10);
            //ds.AddProductToOrder(order, product1);
            //OrderDTO actualOrder = ds.GetOrderById(1);
            //var myP = ds.GetProductById(21);
            //var myC = ds.GetCategoryById(19);
            //ds.AddProductToCategory(myC, myP);
            //ds.RemoveCategoryById(19);

            //var newOr = ds.GetOrderById(1);
            //var oldPr = ds.GetProductById(4);
            //var newPr = ds.GetProductById(25);
            //newOr.Products.Remove(oldPr);
            //newOr.Products.Add(newPr);
            

            //ds.EditOrder(newOr);
            

        }
    }
}
