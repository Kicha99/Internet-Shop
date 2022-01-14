using InternetShop.DAL.Entities;
using InternetShop.DAL.Interfaces;
using InternetShop.DAL.Interfaces.DTO;
using System.Linq;
using System;
using Microsoft.EntityFrameworkCore;

namespace InternetShop.DAL
{
    public class DataSource : IDataSource
    {
        private readonly InternetShopDBContext _dBContext;
        public DataSource()
        {
            _dBContext = new InternetShopDBContext();
        }

        public void AddProduct(ProductDTO product)
        {
            Product newProduct = new Product()
            {
                Title = product.Title,
                Price = product.Price,
                Description = product.Description
            };
            _dBContext.Products.Add(newProduct);
            _dBContext.SaveChanges();

        }

        private int GetProductId(ProductDTO p)
        {
            // не устойчиво к изменениям?
            return _dBContext.Products.FirstOrDefault(t => t.Title == p.Title 
                                            && t.Description == p.Description 
                                            && t.Price == p.Price).Id;
        }
        public void RemoveProduct(ProductDTO delProduct)
        {
            Product product = _dBContext.Products.Find(GetProductId(delProduct));
            _dBContext.Products.Remove(product);
            _dBContext.SaveChanges();
        }

        public void EditProduct(ProductDTO product)
        {
            //?
            Product editP = new Product()
            {
                Description = product.Description,
                Price = product.Price,
                Title = product.Title
            };
            //_dBContext.Products.Update(editP);
            _dBContext.SaveChanges();
        }

        public void AddCategory(CategoryDTO category)
        {
            Category newCategory = new Category()
            {
                Title = category.Title,
            };
            _dBContext.Categories.Add(newCategory);
            _dBContext.SaveChanges();
        }

        private int GetCategoryId(CategoryDTO c)
        {
            // не устойчиво к изменениям?
            return _dBContext.Categories.FirstOrDefault(t => t.Title == c.Title).Id;
        }

        public void RemoveCategory(CategoryDTO category)
        {
            Category deleteCategory = _dBContext.Categories.Find(GetCategoryId(category));
            _dBContext.Categories.Remove(deleteCategory);
            _dBContext.SaveChanges();
        }

        public void AddOrder(OrderDTO order)
        {
            Order newOrder = new Order()
            {
                ClientId = order.ClientId,
            };
            _dBContext.Orders.Add(newOrder);
            _dBContext.SaveChanges();

        }

        public void RemoveProductById(int id)
        {
            if (id <= 0)
                throw new ArgumentException();
            _dBContext.Database.ExecuteSqlInterpolated($"DELETE FROM PRODUCTS  WHERE ID={id}");
        }
    }
}
