using Microsoft.AspNetCore.Mvc;
using PieShop.Repositories;

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
        public ViewResult List()
        {
            return View(_productsRepository.AllProducts);
        }
    }
}