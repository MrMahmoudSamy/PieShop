using Microsoft.AspNetCore.Mvc;
using PieShop.Repositories;
using PieShop.ViewModels;
using System.Linq;

namespace PieShop.Components
{
    public class CategoryMenu:ViewComponent
    {
        private ICategoryRepository _categoryRepository;

        public CategoryMenu(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }
        public IViewComponentResult Invoke()
        {
            var category = _categoryRepository.AllCategories.OrderBy(c => c.CategoryName);
            return View(category);
        }
    }
}
