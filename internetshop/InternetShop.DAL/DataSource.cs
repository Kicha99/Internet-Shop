using InternetShop.DAL.Entities;
using InternetShop.DAL.Interfaces;
using InternetShop.DAL.Interfaces.DTO;
using System.Linq;
using System;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

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
        public void EditProduct(ProductDTO product)
        {
            //?
            Product editP = new Product()
            {
                Description = product.Description,
                Price = product.Price,
                Title = product.Title,
                Id = product.Id
            };
            _dBContext.Products.Update(editP);
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

        public void RemoveCategoryById(int id)
        {
            if (id <= 0)
                throw new ArgumentException();
            _dBContext.Database.ExecuteSqlInterpolated($"DELETE FROM CATEGORIES WHERE ID={id}");
        }

        public IEnumerable<ProductDTO> GetProductsByCategoryId(int id)
        {
            IEnumerable<ProductDTO> products = from p in _dBContext.Products
                                               where p.Category.Id == id
                                               select new ProductDTO()
                                               {
                                                    Id = p.Id,
                                                    Description = p.Description,
                                                    Price = p.Price,
                                                    Title = p.Title
                                               };
            return products.ToList();
        }

        public IEnumerable<CategoryDTO> GetTopCategories()
        {
            IEnumerable<CategoryDTO> categories = from p in _dBContext.Categories
                                                  where p.Child == null
                                                  select new CategoryDTO()
                                                  {
                                                      Id = p.Id,
                                                      Title = p.Title
                                                  };
            return categories.ToList();
        }

        public IEnumerable<CategoryDTO> GetChildCategoriesById(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<OrderDTO> GetOrders()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<OrderDTO> GetOrdersByClient(int id)
        {
            throw new NotImplementedException();
        }
    }
}
