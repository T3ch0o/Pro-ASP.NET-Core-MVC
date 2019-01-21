namespace SportsStore.Services.Interfaces
{
    using System.Linq;

    using SportsStore.Models;

    public interface IOrderService
    {
        IQueryable<Order> GetAll();

        void Save(Order order);
    }
}
