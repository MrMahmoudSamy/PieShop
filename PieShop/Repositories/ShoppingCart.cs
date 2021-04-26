using PieShop.Data;
using PieShop.Model;
using System.Collections.Generic;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using System;
using Microsoft.AspNetCore.Http;
using System.Linq;

namespace PieShop.Repositories
{
    public class ShoppingCart
    {
        private AppDbContext _context;

        public ShoppingCart(AppDbContext appDbContext)
        {
            _context = appDbContext;
        }
        public static ShoppingCart GetCart(IServiceProvider service)
        {
            //Access to a session
            ISession session = service.GetRequiredService<IHttpContextAccessor>()?
                .HttpContext.Session;
            //ask the service collection for Adddbcontext
            var context = service.GetService<AppDbContext>();
            //check the session to see if already have string id if not we create a new one
            string cartId = session.GetString("CartId") ?? Guid.NewGuid().ToString();
            //set cartid into session 
            session.SetString("CartId", cartId);
            //create a new shoppingcart passing in that Context
            return new ShoppingCart(context) { ShoppingCartId = cartId };
        }
        public void AddToCart(Product product,int amount)
        {
            var shoppingCartItems = _context.ShoppingCartItems.SingleOrDefault
                (s => s.Product.ProductId == product.ProductId && s.ShoppingCartId == ShoppingCartId);
            if(shoppingCartItems==null)
            {
                shoppingCartItems = new ShoppingCartItem
                {
                    ShoppingCartId = ShoppingCartId,
                    Product = product,
                    Amount = 1
                };
                _context.ShoppingCartItems.Add(shoppingCartItems);
            }
            else
            {
                shoppingCartItems.Amount++;
            }
            _context.SaveChanges();
        }
        public int RemoveFromCart(Product product)
        {
            var shoppingCartItem = _context.ShoppingCartItems.SingleOrDefault
               (s => s.Product.ProductId == product.ProductId && s.ShoppingCartId == ShoppingCartId);
            var localAmount = 0;
            if(shoppingCartItem!=null)
            {
                if(shoppingCartItem.Amount>1)
                {
                    shoppingCartItem.Amount--;
                    localAmount = shoppingCartItem.Amount;
                }
                else
                {
                    _context.ShoppingCartItems.Remove(shoppingCartItem);
                }
            }
            _context.SaveChanges();
            return localAmount;
        }
        public List<ShoppingCartItem> GetShoppingCartItems()
        {
            return ShoppingCartItems ??
                (ShoppingCartItems = _context.ShoppingCartItems.Where(c => c.ShoppingCartId == ShoppingCartId)
                .Include(s => s.Product).ToList());
        }
        public void ClearCart()
        {
            var cartItems = _context.ShoppingCartItems.Where(cart => cart.ShoppingCartId == ShoppingCartId);
            _context.ShoppingCartItems.RemoveRange(cartItems);
            _context.SaveChanges();
        }
        public decimal GetShoppingCartTotal()
        {
            var total = _context.ShoppingCartItems.Where(s => s.ShoppingCartId == ShoppingCartId)
                .Select(c => c.Product.Price * c.Amount).Sum();
            return total;
        }
        public string ShoppingCartId { get; set; }
        public List<ShoppingCartItem> ShoppingCartItems { get; set; }
    }
}
