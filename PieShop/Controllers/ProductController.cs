﻿using Microsoft.AspNetCore.Mvc;
using PieShop.Model;
using PieShop.Repositories;
using PieShop.ViewModels;
using System.Collections.Generic;
using System.Linq;

namespace PieShop.Controllers
{
    public class ProductController : Controller
    {
        private IProductsRepository _productsRepository;
        private ICategoryRepository _categoryRepository;

        public ProductController(IProductsRepository productsRepository,ICategoryRepository categoryRepository)
        {
            _productsRepository = productsRepository;
            _categoryRepository = categoryRepository;
        }
        //public ViewResult List()
        //{
        //    ProductlistViewModel productlistViewModel = new ProductlistViewModel();
        //    productlistViewModel.AllProduct = _productsRepository.AllProducts;
        //    productlistViewModel.CurrentCategory = "Chees Cake";
        //    return View(productlistViewModel);
        //}
        public ViewResult List(string category)
        {
            IEnumerable<Product> products;
            string currentCategory;
            if(string.IsNullOrEmpty(category))
            {
                products = _productsRepository.AllProducts.OrderBy(p => p.ProductId);
                currentCategory = "All Pies";
            }
            else
            {
                products=_productsRepository.AllProducts.Where(p=>p.Category.CategoryName==category)
                    .OrderBy(p => p.ProductId);
                var cat= _categoryRepository.AllCategories.FirstOrDefault(c => c.CategoryName == category);
                currentCategory = cat.CategoryName;
            }
            return View(new ProductlistViewModel
            {
                AllProduct = products,
                CurrentCategory = currentCategory
            });
        }
        public IActionResult Details(int id,string slug)
        {
            var product = _productsRepository.GetPieById(id);
            if (product == null)
                return NotFound();
           
            return View(product);
        }
        public IActionResult Index(int id,string slug)
        {
            var product = _productsRepository.GetPieById(id);
            if (product == null)
                return NotFound();
         
            return View(product);
        }
    }
}