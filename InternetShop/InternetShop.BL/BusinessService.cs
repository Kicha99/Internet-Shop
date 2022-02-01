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

        public void EditProduct(ModelProduct pr)
        {
            var newPr = new ProductDTO()
            {
                Id = pr.Id,
                Description = pr.Description,
                Price = pr.Price,
                Title = pr.Title
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
                                }).ToList() //Null Reference Exception
                };
            }
            //map to Model Category
        }

        public IEnumerable<ModelCategory> GetChildCategoriesById(int id)
        {
            var children = _ds.GetChildCategoriesById(id);

            foreach (var item in children)
            {
                yield return new ModelCategory()
                {
                    Id = item.Id,
                    Title = item.Title
                };
            }

        }

        public ModelOrder GetOrderById(int orderId)
        {
            var order = _ds.GetOrderById(orderId);

            ModelOrder newOrder = new ModelOrder()
            {
                ClientId = order.ClientId,
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

        public IEnumerable<ModelOrder> GetOrders()
        {
            var orders = _ds.GetOrders();

            foreach (var item in orders)
            {
                yield return new ModelOrder()
                {
                    Id = item.Id,
                    ClientId = item.ClientId,
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
    }
}
