using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using TempProject.Core.Entity;
using TempProject.Core.Repositories;

namespace TempProject.Repository.Repositories
{
    public class CategoryRepository : GenericRepository<Category>, ICategoryRepository
    {
        public CategoryRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<Category> GetCategoryByIdWithProductsAsync(int categoryId)
        {
            return await _context.Categories.Include(x => x.Products).Where(x => x.Id == categoryId).SingleOrDefaultAsync();
        }
    }
}
