namespace SportsStore.Services.Interfaces
{
    using System.Linq;

    using SportsStore.Models;

    public interface IProductService
    {
        IQueryable<Product> GetAll { get; }
    }
}
