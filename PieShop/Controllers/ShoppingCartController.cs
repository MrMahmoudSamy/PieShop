using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PieShop.Repositories;
using PieShop.ViewModels;

namespace PieShop.Controllers
{
    public class ShoppingCartController : Controller
    {
        private IProductsRepository _productsRepository;
        private ShoppingCart _shoppingCart;

        public ShoppingCartController(IProductsRepository productsRepository,ShoppingCart shoppingCart)
        {
            _productsRepository = productsRepository;
            _shoppingCart = shoppingCart;
        }
        public ViewResult Index()
        {
            var items = _shoppingCart.GetShoppingCartItems();
            _shoppingCart.ShoppingCartItems = items;
            var shoppingcartViewModel = new ShoppingCartViewModel
            {
                ShoppingCart = _shoppingCart,
                ShoppingCartTotal = _shoppingCart.GetShoppingCartTotal()
            };
            return View(shoppingcartViewModel);
        }
        public RedirectToActionResult AddToShoppingCart(int productId)
        {
            var selectedproduct = _productsRepository.AllProducts.FirstOrDefault(p => p.ProductId == productId);
            if(selectedproduct!=null)
            {
                _shoppingCart.AddToCart(selectedproduct, 1);
            }
            return RedirectToAction("Index");
        }
        public RedirectToActionResult RemoveFromShoppingCart(int productId)
        {
            var selectedproduct = _productsRepository.AllProducts.FirstOrDefault(p => p.ProductId == productId);
            if (selectedproduct != null)
            {
                _shoppingCart.RemoveFromCart(selectedproduct);
            }
            return RedirectToAction("Index");
        }
    }
}