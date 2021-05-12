using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PieShop.Model;
using PieShop.Repositories;
using PieShop.ViewModels;

namespace PieShop.Controllers
{
    [Authorize]
    public class OrderController : Controller
    {
        private ShoppingCart _shoppingCart;
        private IOrderRepository _orderRepo;

        public OrderController(ShoppingCart shoppingCart,IOrderRepository orderRepo)
        {
            _shoppingCart = shoppingCart;
            _orderRepo = orderRepo;
        }
        public IActionResult Checkout()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Checkout(Order order)
        {
            var items = _shoppingCart.GetShoppingCartItems();
            _shoppingCart.ShoppingCartItems = items;
            if(_shoppingCart.ShoppingCartItems.Count==0)
            {
                ModelState.AddModelError("", "Your chart is empty,add some pies first");
            }
            if(ModelState.IsValid)
            {
                _orderRepo.CreateOrder(order);
                _shoppingCart.ClearCart();
                return RedirectToAction("CheckoutComplete");
            }
            return View();
        }
        public IActionResult CheckoutComplete()
        {
            var MessageViewModel = new MessageViewModel
            {
                CheckoutComleteMessage = "Thanks for your order. You will soon enjoy our delicious pies!"
            };
            return View(MessageViewModel);
        }
    }
} 