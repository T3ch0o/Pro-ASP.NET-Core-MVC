namespace SportsStore.Services
{
    using System.Linq;

    using Microsoft.EntityFrameworkCore;

    using SportsStore.Data;
    using SportsStore.Models;
    using SportsStore.Services.Interfaces;

    public class OrderService : IOrderService
    {
        private readonly SportsStoreDbContext _db;

        public OrderService(SportsStoreDbContext db)
        {
            _db = db;
        }

        public IQueryable<Order> GetAll()
        {
            return _db.Orders.Include(o => o.CartLines).ThenInclude(c => c.Product);
        }

        public void Save(Order order)
        {
            _db.AttachRange(order.CartLines.Select(c => c.Product));

            if (order.Id == 0)
            {
                _db.Orders.Add(order);
            }

            _db.SaveChanges();
        }
    }
}
