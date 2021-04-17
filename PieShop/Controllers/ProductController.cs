using Microsoft.AspNetCore.Mvc;
using PieShop.Repositories;
using PieShop.ViewModels;
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
            ProductlistViewModel productlistViewModel = new ProductlistViewModel();
            productlistViewModel.AllProduct = _productsRepository.AllProducts;
            productlistViewModel.CurrentCategory = "Chees Cake";
            return View(productlistViewModel);
        }
    }
}