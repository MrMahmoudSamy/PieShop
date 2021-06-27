using Microsoft.EntityFrameworkCore;
using PieShop.Data;
using PieShop.Model;
using System.Collections.Generic;
using System.Linq;

namespace PieShop.Repositories
{
    public class ProductRepository : IProductsRepository
    {
        private AppDbContext _context;

        public ProductRepository(AppDbContext appDbContext)
        {
            _context = appDbContext;
        }
        public IEnumerable<Product> AllProducts
        {
            get 
            {
              return  _context.Products.Include(c => c.Category);
            }
        }

        public IEnumerable<Product> ProductOfTheWeek
        {
            get 
            {
                return _context.Products.Include(c => c.Category).Where(p=>p.IsProductOfTheWeek);
            }
        }

        public Product GetPieById(int productId)
        {
            return AllProducts.FirstOrDefault(p => p.ProductId == productId);
        }
        public Product GetHtmlTagsById(int productId)
        {
            return AllProducts.FirstOrDefault(p => p.ProductId == productId);
        }
    }
}
