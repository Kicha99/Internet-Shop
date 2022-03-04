using InternetShop.BL.Interfaces;
using InternetShop.UI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace InternetShop.UI.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IBusinessService _businessService;

        public HomeController(ILogger<HomeController> logger, IBusinessService businessService)
        {
            _logger = logger;
            _businessService = businessService;
        }

        public IActionResult Index()
        {
            var category = _businessService.GetRootCategory();
            var bestProducts = _businessService.GetBestFiveProducts();

            IndexModel indexView = new IndexModel()
            {
                RootCategory = category,
                BestProducts = bestProducts
            };

            return View(indexView);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        public IActionResult Subcategory(int id)
        {
            var category = _businessService.GetCategoryById(id);
            return View(category); //TODO
        }

        public IActionResult Product(int id)
        {
            var product = _businessService.GetProductById(id);
            return View(product);
        }

        
    }
}
