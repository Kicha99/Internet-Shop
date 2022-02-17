using InternetShop.BL.Interfaces;
using InternetShop.DAL.Interfaces;
using InternetShop.DAL.Interfaces.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InternetShop.BL
{
    public class BusinessService : IBusinessService
    {
        private readonly IDataSource _ds;
        public BusinessService(IDataSource ds)
        {
            _ds = ds;
        }

        public void AddProduct(ModelProduct pr)
        {
            var res = new ProductDTO()
            {
                Id = pr.Id,
                Description = pr.Description,
                Title = pr.Title,
                Price = pr.Price
            };
            _ds.AddProduct(res);
        }

        public void AddProductInOrder(ModelProduct product, ModelOrder order)
        {
            ProductDTO productDTO = new ProductDTO()
            {
                Id = product.Id,
                Description = product.Description,
                Price = product.Price,
                Title = product.Title
            };
            OrderDTO orderDTO = new OrderDTO()
            {
                UserId = order.UserId,
                Id = order.Id,
                Products = (from p in order.Products
                            select new ProductDTO()
                            {
                                Id = p.Id,
                                Description = p.Description,
                                Price = p.Price,
                                Title = p.Title
                            }).ToList()
            };

            _ds.AddProductToOrder(orderDTO, productDTO);
        }

        public void EditProduct(ModelProduct pr)
        {
            var newPr = new ProductDTO()
            {
                Id = pr.Id,
                Description = pr.Description,
                Price = pr.Price,
                Title = pr.Title,
                CategoryId = pr.CategoryId
            };
            _ds.EditProduct(newPr);
        }

        public IEnumerable<ModelCategory> GetCategories()
        {
            var categories = _ds.GetTopCategories();
            foreach (var item in categories)
            {
                yield return new ModelCategory()
                {
                    Id = item.Id,
                    Title = item.Title,
                    Products = (from p in item.Products
                                where p != null
                                select new ModelProduct()
                                {
                                    Id = p.Id,
                                    Description = p.Description,
                                    Price = p.Price,
                                    Title = p.Title
                                }).ToList(),
                    Child = (from p in item.Child 
                             select new ModelCategory()
                             {
                                 Title = p.Title,
                                 Id = p.Id,
                                 ChildId = p.ChildId
                                 
                             })
                };
            }
            //map to Model Category
        }

        public ModelCategory GetCategoryById(int id)
        {
            var children = _ds.GetChildCategoriesById(id);
            var category = _ds.GetCategoryById(id);

                 return new ModelCategory()
                {
                    Id = category.Id,
                    Title = category.Title,
                    Products = (from p in category.Products
                                select new ModelProduct() 
                                { 
                                    Id = p.Id,
                                    CategoryId = p.CategoryId,
                                    Description = p.Description,
                                    Price = p.Price,
                                    Title = p.Title
                                }).ToList(),
                     Child = (from p in category.Child
                              where category.ChildId == p.CategoryId
                              select new ModelCategory()
                              {
                                  ChildId = p.ChildId,
                                  Id = p.Id,
                                  Title = p.Title
                              })
                 };
        }

        public ModelOrder GetOrderById(int orderId)
        {
            var order = _ds.GetOrderById(orderId);

            ModelOrder newOrder = new ModelOrder()
            {
                UserId = order.UserId,
                Id = order.Id,
                Products = (from p in order.Products
                            select new ModelProduct()
                            {
                                Id = p.Id,
                                Description = p.Description,
                                Price = p.Price,
                                Title = p.Title
                            }).ToList()
            };

            return newOrder;
        }

        public ModelOrder GetOrderByUserId(Guid userId)
        {
            var order = _ds.GetOrderByClient(userId);

            return new ModelOrder()
            {
                Id = order.Id,
                UserId = order.UserId,
                Products = (from p in order.Products
                            select new ModelProduct()
                            {
                                Id = p.Id,
                                CategoryId = p.CategoryId,
                                Description = p.Description,
                                Price = p.Price,
                                Title = p.Title
                            }).ToList()
            };
        }

        public IEnumerable<ModelOrder> GetOrders()
        {
            var orders = _ds.GetOrders();

            foreach (var item in orders)
            {
                yield return new ModelOrder()
                {
                    Id = item.Id,
                    UserId = item.UserId,
                    Products = (from p in item.Products
                                where p != null
                                select new ModelProduct()
                                {
                                    Id = p.Id,
                                    Description = p.Description,
                                    Price = p.Price,
                                    Title = p.Title
                                }).ToList()
                };
            }
        }

        public ModelProduct GetProductById(int id)
        {
            var p = _ds.GetProductById(id);

            return new ModelProduct()
            {
                Id = p.Id,
                CategoryId = p.CategoryId,
                Price = p.Price,
                Description = p.Description,
                Title = p.Title
            };
        }

        public IEnumerable<ModelProduct> GetProductsByCategoryId(int id)
        {
            var products = _ds.GetProductsByCategoryId(id);
            foreach (var item in products)
            {
                yield return new ModelProduct()
                {
                    Id = item.Id,
                    Description = item.Description,
                    Price = item.Price,
                    Title = item.Title
                };
            }
        }

        public void RemoveFromOrder(ModelProduct product, ModelOrder order)
        {
            var deleteProduct = new ProductDTO()
            {
                CategoryId = product.CategoryId,
                Description = product.Description,
                Id = product.Id,
                Price = product.Price,
                Title = product.Title
            };

            var o = new OrderDTO()
            {
                Id = order.Id
            };

            _ds.RemoveProductFromOrder(deleteProduct, o);

            
        }

        public IEnumerable<ModelProduct> SortProductsByPriceAscending(IEnumerable<ModelProduct> mp)
        {
            var sorted = from p in mp
                         orderby p.Price
                         select p;
            return sorted;
        }
    }
}
