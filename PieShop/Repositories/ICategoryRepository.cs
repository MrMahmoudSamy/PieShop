using PieShop.Model;
using System.Collections.Generic;

namespace PieShop.Repositories
{
    public  interface ICategoryRepository
    {
        IEnumerable<Category> AllCategories { get; }
    }
}
