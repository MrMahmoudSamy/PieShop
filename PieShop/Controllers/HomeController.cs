using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PieShop.Repositories;
using PieShop.ViewModels;

namespace PieShop.Controllers
{
    public class HomeController : Controller
    {
        private IProductsRepository _productsRepository;

        public HomeController(IProductsRepository productsRepository)
        {
            _productsRepository = productsRepository;
        }
        public IActionResult Index()
        {
            var homeViewmodel = new HomeViewModel
            {
                PiesOfTheWeek = _productsRepository.ProductOfTheWeek
            };
            return View(homeViewmodel);
        }
    }
}