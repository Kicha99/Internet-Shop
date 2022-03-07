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
                Description = product.Description,
                NumberOfPurchase = product.NumberOfPurchase
                
            };
            _dBContext.Products.Add(newProduct);
            _dBContext.SaveChanges();

        }
        public void EditProduct(ProductDTO product)
        {
            //Ставит CategoryId в null +
            var editp = (from p in _dBContext.Products where p.Id == product.Id select p).FirstOrDefault();

            editp.Description = product.Description;
            editp.Price = product.Price;
            editp.Title = product.Title;
            editp.Id = product.Id;
            editp.CategoryId = product.CategoryId;
            editp.NumberOfPurchase = product.NumberOfPurchase;
            
            
            _dBContext.Products.Update(editp);
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
                UserId = order.UserId,
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
                                                    Title = p.Title,
                                                    NumberOfPurchase = p.NumberOfPurchase
                                               };
            return products.ToList();
        }

        public CategoryDTO GetTopCategory()
        {
            int rootID = (from p in _dBContext.Categories
                          where p.Id == p.CategoryId
                          select p.Id).FirstOrDefault();
            return new CategoryDTO()
            {
                Child = GetChildCategoriesById(rootID).ToList(),
                ChildId = rootID,
                Products = GetProductsByCategoryId(rootID).ToList(),
                Title = ">",
                Id = rootID

            };

            //IEnumerable < CategoryDTO > categories = from p in _dBContext.Categories
            //                                         where p.CategoryId == rootID
            //                                      select new CategoryDTO()
            //                                      {
            //                                          Id = p.Id,
            //                                          Title = p.Title,
            //                                          Products = (from x in p.Products
            //                                                      select new ProductDTO()
            //                                                      {
            //                                                          Id = x.Id,
            //                                                          Description = x.Description,
            //                                                          Price = x.Price,
            //                                                          Title = x.Title
            //                                                      }).ToList(),
            //                                         Child = (from c in _dBContext.Categories where p.ChildId == c.CategoryId
            //                                                  select new CategoryDTO()
            //                                                  {
            //                                                      Title = c.Title,
            //                                                      Id = c.Id,
            //                                                      ChildId = c.ChildId,
            //                                                      CategoryId = c.CategoryId
            //                                                  }).ToList(),                                                 

            //                                      };
            
            //return categories.ToList();
        }

        public IEnumerable<CategoryDTO> GetChildCategoriesById(int id)
        {

            var categories = from p in _dBContext.Categories
                             where p.CategoryId == id
                             select new CategoryDTO()
                             {
                                 CategoryId = p.CategoryId,
                                 Id = p.Id,
                                 Products = (from x in p.Products
                                             select new ProductDTO()
                                             {
                                                 Id = x.Id,
                                                 CategoryId = x.CategoryId,
                                                 Description = x.Description,
                                                 NumberOfPurchase = x.NumberOfPurchase,
                                                 Price = x.Price,
                                                 Title = x.Title
                                             }).ToList(),
                                 Title = p.Title,
                                 Child = (from y in _dBContext.Categories
                                         where y.CategoryId == p.Id
                                         select new CategoryDTO()
                                         {
                                             Id = y.Id,
                                             Title = y.Title
                                         }).ToList()
                                 
                             };

            return categories.ToList();
        }

        public IEnumerable<OrderDTO> GetOrders()
        {
            IEnumerable<OrderDTO> orders = from p in _dBContext.Orders
                                           select new OrderDTO()
                                           {
                                               UserId = p.UserId,
                                               Products = (from x in p.Products
                                                          select new ProductDTO()
                                                          {
                                                              Description = x.Description,
                                                              Id = x.Id,
                                                              Price = x.Price,
                                                              Title = x.Title,
                                                              NumberOfPurchase = x.NumberOfPurchase
                                                          }).ToList(),
                                                Id = p.Id
                                           };
            return orders.ToList();
        }

        public OrderDTO GetOrderByClient(Guid id)
        {
            var order = (from p in _dBContext.Orders
                         where p.UserId == id
                         select p).FirstOrDefault();
            if (order != null)
            {
                return new OrderDTO()
                {
                    Id = order.Id,
                    Products = (from p in order.Products
                                select new ProductDTO()
                                {
                                    Id = p.Id,
                                    CategoryId = p.CategoryId,
                                    Description = p.Description,
                                    Price = p.Price,
                                    Title = p.Title,
                                    NumberOfPurchase = p.NumberOfPurchase
                                }).ToList(),
                    UserId = order.UserId
                };
            }
            else
            {
                var newOrder = new Order() { UserId = id, Products = new List<Product>() };
                _dBContext.Orders.Add(newOrder);
                _dBContext.SaveChanges();
                return new OrderDTO() { UserId = newOrder.UserId };
            }
            
            
        }

        public void EditCategory(CategoryDTO category)
        {
            Category changedCategory = new Category()
            {
                CategoryId = category.Id,
                Id = category.Id,
                Title = category.Title,
                ChildId = category.ChildId
            };
            _dBContext.Categories.Update(changedCategory);
            _dBContext.SaveChanges();
        }

        public void EditOrder(OrderDTO order)
        {
            var entityOrder= (from p in _dBContext.Orders
                               where p.Id == order.Id
                               select p).FirstOrDefault();
            var deletedEntities = new List<Product>();
 
            
            var DTOproducts = order.Products;

            foreach (var p in entityOrder.Products)
            {
                var IsExists = (from x in DTOproducts
                               where x.Id == p.Id
                               select x).Count();
                if(IsExists == 0)
                {   //delete from db
                    deletedEntities.Add(p);
                }              
            }
            foreach (var item in DTOproducts)
            {
                var IsExists = (from p in entityOrder.Products
                                where p.Id == item.Id
                                select p).Count();
                if (IsExists == 0)
                {
                    Product product = new Product()
                    {
                        Id = item.Id,
                        Description = item.Description,
                        Price = item.Price,
                        Title = item.Title,
                        NumberOfPurchase = item.NumberOfPurchase
                    };

                    entityOrder.Products.Add(product);
                }
            }
            foreach (var item in deletedEntities)
            {
                entityOrder.Products.Remove(item);
            }
            _dBContext.Orders.Update(entityOrder);
            _dBContext.SaveChanges();
             
            
            //throw new NotImplementedException("TODO: change product list with compare old and new list");
            
        }

        public OrderDTO GetOrderById(int id)
        {
            OrderDTO order = (from p in _dBContext.Orders
                              where p.Id == id
                              select new OrderDTO()
                              {
                                  Id = p.Id,
                                  UserId = p.UserId,
                                  Products = (from x in p.Products
                                             select new ProductDTO()
                                             {
                                                 Description = x.Description,
                                                 Id = x.Id,
                                                 Price = x.Price,
                                                 Title = x.Title,
                                                 NumberOfPurchase = x.NumberOfPurchase
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
                                      Title = p.Title,
                                      CategoryId = p.CategoryId,
                                      NumberOfPurchase = p.NumberOfPurchase
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
                                                    where p.Id == x.CategoryId
                                                    select new ProductDTO()
                                                    {
                                                        Description = x.Description,
                                                        Id = x.Id,
                                                        Price = x.Price,
                                                        Title = x.Title,
                                                        NumberOfPurchase = x.NumberOfPurchase
                                                    }).ToList(),
                                        Child = (from c in _dBContext.Categories
                                                 where p.ChildId == c.CategoryId
                                                 select new CategoryDTO()
                                                 {
                                                     Title = c.Title,
                                                     Id = c.Id,
                                                     ChildId = c.ChildId,
                                                     CategoryId = c.CategoryId
                                                 }).ToList()

                                    }).FirstOrDefault();
            return category;
        }

        public void RemoveProductFromOrder(ProductDTO product, OrderDTO order)
        {
            var deleteProduct = (from p in _dBContext.Products
                                where p.Id == product.Id
                                select p).FirstOrDefault();

            var o = (from p in _dBContext.Orders
                    where p.UserId == order.UserId
                    select p).FirstOrDefault();
            o.Products.Remove(deleteProduct);

            _dBContext.Orders.Update(o);
            _dBContext.SaveChanges();
        }

        public IEnumerable<ProductDTO> GetAllProducts()
        {
            IEnumerable<ProductDTO> products = from p in _dBContext.Products
                                               select new ProductDTO()
                                               {
                                                   CategoryId = p.CategoryId,
                                                   Description = p.Description,
                                                   Id = p.Id,
                                                   NumberOfPurchase = p.NumberOfPurchase,
                                                   Price = p.Price,
                                                   Title = p.Title
                                               };

            return products;
        }
    }
}
