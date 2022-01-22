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
            // TODO: What should we do with child categories?
            // Remove or Assign as root
            // TODO: Thinking about other cases while remove categories
            //1) If we remove category, we need to remove all its child
            //2) If we remove category, we need to change categories for products that contains old category(categories) 
            if (id <= 0)
                throw new ArgumentException();
            var category = (from p in _dBContext.Categories
                           where p.Id == id
                           select p).FirstOrDefault();
            _dBContext.Categories.Remove(category);
            

            var products = from p in _dBContext.Products
                           where p.CategoryId.Value == id
                           select p;//?
            foreach (var item in products)
            {
                item.CategoryId = null;
                _dBContext.Products.Update(item);
            }
            _dBContext.SaveChanges();


            //_dBContext.Categories.Remove();
            //_dBContext.Database.ExecuteSqlInterpolated($"DELETE Products FROM CATEGORIES WHERE ID={id}");
            //_dBContext.Database.ExecuteSqlInterpolated($"DELETE FROM CATEGORIES WHERE CategoryId={id}");
            //_dBContext.Database.ExecuteSqlInterpolated($"DELETE FROM CATEGORIES WHERE ID={id}");

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
                                                  where !p.CategoryId.HasValue 
                                                  select new CategoryDTO()
                                                  {
                                                      Id = p.Id,
                                                      Title = p.Title
                                                  };
            return categories.ToList();
        }

        public IEnumerable<CategoryDTO> GetChildCategoriesById(int id)
        {
            IEnumerable<CategoryDTO> child = from p in _dBContext.Categories
                                             where p.CategoryId == id
                                             select new CategoryDTO()
                                             {
                                                 Id = p.Id,
                                                 Title = p.Title
                                             };
            return child.ToList();
        }

        public IEnumerable<OrderDTO> GetOrders()
        {
            IEnumerable<OrderDTO> orders = from p in _dBContext.Orders
                                           select new OrderDTO()
                                           {
                                               ClientId = p.ClientId,
                                               Products = (from x in p.Products
                                                          select new ProductDTO()
                                                          {
                                                              Description = x.Description,
                                                              Id = x.Id,
                                                              Price = x.Price,
                                                              Title = x.Title
                                                          }).ToList(),
                                                Id = p.Id
                                           };
            return orders.ToList();
        }

        public IEnumerable<OrderDTO> GetOrdersByClient(int id)
        {
            throw new NotImplementedException();
        }

        public void EditCategory(CategoryDTO category)
        {
            Category changedCategory = new Category()
            {
                CategoryId = category.Id,
                Id = category.Id,
                Title = category.Title
            };
            _dBContext.Categories.Update(changedCategory);
            _dBContext.SaveChanges();
        }

        public void EditOrder(OrderDTO order)
        {
            //Order changedOrder = new Order()
            //{
            //    Id = order.Id,
            //    ClientId = order.ClientId,
            //    Products = from p in order.Products
            //               select new Product()
            //               {
            //                    Id = p.Id,
            //                    Description = p.Description,
            //                    Price = p.Price,
            //                    Title = p.Title
            //               }
            //}
            //_dBContext.Orders.Update(changedOrder);
            //_dBContext.SaveChanges();
        }

        public OrderDTO GetOrderById(int id)
        {
            OrderDTO order = (from p in _dBContext.Orders
                              where p.Id == id
                              select new OrderDTO()
                              {
                                  Id = p.Id,
                                  ClientId = p.ClientId,
                                  Products = (from x in p.Products
                                             select new ProductDTO()
                                             {
                                                 Description = x.Description,
                                                 Id = x.Id,
                                                 Price = x.Price,
                                                 Title = x.Title
                                             }).ToList()
                              }).FirstOrDefault();

            return order;
        }

        public ProductDTO GetProductById(int id)
        {
            ProductDTO product = (from p in _dBContext.Products
                                  where p.Id == id
                                  select new ProductDTO()
                                  {
                                      Description = p.Description,
                                      Id = p.Id,
                                      Price = p.Price,
                                      Title = p.Title
                                  }).FirstOrDefault();
            return product;
        }

        public void AddProductToOrder(OrderDTO order, ProductDTO product)
        { 
            var prod = (from p in _dBContext.Products where p.Id == product.Id select p).FirstOrDefault();
            var myOrder = (from p in _dBContext.Orders where p.Id == order.Id select p).FirstOrDefault();
            myOrder.Products.Add(prod);
            _dBContext.SaveChanges();
        }

        public void AddProductToCategory(CategoryDTO category, ProductDTO product)
        {
            var tmpCategory = (from c in _dBContext.Categories where c.Id == category.Id select c).FirstOrDefault();
            var tmpProduct = (from p in _dBContext.Products where p.Id == product.Id select p).FirstOrDefault();
            tmpCategory.Products.Add(tmpProduct);//Null Reference Exception
            _dBContext.SaveChanges();
        }

        public CategoryDTO GetCategoryById(int id)
        {
            CategoryDTO category = (from p in _dBContext.Categories
                                    where p.Id == id
                                    select new CategoryDTO()
                                    {
                                        Id = p.Id,
                                        Title = p.Title,
                                        Products = (from x in _dBContext.Products
                                                    where p.CategoryId == x.Id
                                                    select new ProductDTO()
                                                    {
                                                        Description = x.Description,
                                                        Id = x.Id,
                                                        Price = x.Price,
                                                        Title = x.Title
                                                    }).ToList()
                                        
                                    }).FirstOrDefault();
            return category;
        }
    }
}
