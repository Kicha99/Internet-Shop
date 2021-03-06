using InternetShop.BL.Interfaces;
using InternetShop.UI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace InternetShop.UI.Controllers
{
    [Authorize]
    public class BasketController : Controller
    {
        private readonly IBusinessService _businessService;
        private readonly ILogger<BasketController> _logger;

        public BasketController(ILogger<BasketController> logger, IBusinessService businessService)
        {
            _businessService = businessService;
            _logger = logger;
        }
        public IActionResult Index()
        {
            var id = GetCurrentUserId();
            var order = _businessService.GetOrderByUserId(id);

            return View(order);
        }
        public IActionResult AddProduct(int id)
        {
            var userId = GetCurrentUserId();
            var order = _businessService.GetOrderByUserId(userId);
            var product = _businessService.GetProductById(id);
            _businessService.AddProductInOrder(product, order);
            return RedirectToAction("Index");
        }
        public IActionResult RemoveFromBasket(int id)
        {
            var userId = GetCurrentUserId();
            var order = _businessService.GetOrderByUserId(userId);
            var product = _businessService.GetProductById(id);
            _businessService.RemoveFromOrder(product, order);

            return RedirectToAction("Index");
        }
        public IActionResult BuyProducts()
        {
            var id = GetCurrentUserId();
            var order = _businessService.GetOrderByUserId(id);

            return View(order);
        }
        [HttpPost]
        public IActionResult Processing([FromForm] Payment payment)
        {
            _businessService.Payment(GetCurrentUserId());
            return View(payment);
        }
        private Guid GetCurrentUserId()
        {
            var claimsIdentity = (ClaimsIdentity)this.User.Identity;
            var claim = claimsIdentity.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier);
            return new System.Guid(claim.Value);

        }
    }
}
