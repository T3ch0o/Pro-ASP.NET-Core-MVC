namespace SportsStore.Services.Interfaces
{
    using System.Linq;

    using SportsStore.Models;
    using SportsStore.Models.ViewModels;

    public interface IOrderService
    {
        IQueryable<Order> GetAll();

        void SaveOrder(Order order);
    }
}
