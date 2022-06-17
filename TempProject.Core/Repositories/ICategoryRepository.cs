using System.Threading.Tasks;
using TempProject.Core.Entity;

namespace TempProject.Core.Repositories
{
    public interface ICategoryRepository : IGenericRepository<Category>
    {
        Task<Category> GetCategoryByIdWithProductsAsync(int categoryId);
    }
}
