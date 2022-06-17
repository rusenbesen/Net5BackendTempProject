using System.Collections.Generic;
using System.Threading.Tasks;
using TempProject.Core.DTOs;
using TempProject.Core.Entity;

namespace TempProject.Core.Services
{
    public interface IProductService : IService<Product>
    {
        Task<CustomResponseDto<List<ProductWithCategoryDto>>> GetProductsWithCategory();
    }
}
