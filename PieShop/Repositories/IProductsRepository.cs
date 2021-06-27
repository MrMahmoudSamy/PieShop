using PieShop.Model;
using System.Collections.Generic;

namespace PieShop.Repositories
{
    public  interface IProductsRepository
    {
        IEnumerable<Product> AllProducts { get; }
        IEnumerable<Product> ProductOfTheWeek { get; }
        Product GetPieById(int productId);
        Product GetHtmlTagsById(int productId);
    }
}
