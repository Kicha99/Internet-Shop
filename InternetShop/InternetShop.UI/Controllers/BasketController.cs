using InternetShop.BL.Interfaces;
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
            throw new NotFiniteNumberException();
        }
        public IActionResult RemoveFromProduct(int id)
        {
            throw new NotFiniteNumberException();
        }
        public IActionResult BuyProducts()
        {
            throw new NotFiniteNumberException();
        }
        private Guid GetCurrentUserId()
        {
            var claimsIdentity = (ClaimsIdentity)this.User.Identity;
            var claim = claimsIdentity.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier);
            return new System.Guid(claim.Value);

        }
    }
}
