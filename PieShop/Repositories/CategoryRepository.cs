using PieShop.Data;
using PieShop.Model;
using System.Collections.Generic;

namespace PieShop.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private AppDbContext _context;

        public CategoryRepository(AppDbContext appDbContext)
        {
            _context = appDbContext;
        }
        public IEnumerable<Category> AllCategories => _context.Categories;
    }
}
