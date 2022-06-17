using System.Threading.Tasks;
using TempProject.Core.DTOs;
using TempProject.Core.Entity;

namespace TempProject.Core.Services
{
    public interface ICategoryService : IService<Category>
    {
        Task<CustomResponseDto<CategoryWithProductsDto>> GetCategoryByIdWithProductsAsync(int categoryId);
    }
}
