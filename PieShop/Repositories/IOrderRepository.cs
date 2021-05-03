using PieShop.Model;

namespace PieShop.Repositories
{
    public interface IOrderRepository
    {
        void CreateOrder(Order order);
    }
}
