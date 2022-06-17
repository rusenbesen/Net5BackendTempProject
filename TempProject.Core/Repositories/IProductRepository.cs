using System.Collections.Generic;
using System.Threading.Tasks;
using TempProject.Core.Entity;

namespace TempProject.Core.Repositories
{
    public interface IProductRepository : IGenericRepository<Product>
    {
        Task<List<Product>> GetProductsWithCategory();
    }
}
