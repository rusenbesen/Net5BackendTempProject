using AutoMapper;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TempProject.Core.DTOs;
using TempProject.Core.Entity;
using TempProject.Core.Repositories;
using TempProject.Core.Services;
using TempProject.Core.UnitOfWorks;

namespace TempProject.Service.Services
{
    public class ProductServiceWithNoCaching : Service<Product>, IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;
        public ProductServiceWithNoCaching(IGenericRepository<Product> repository, IUnitOfWork unitOfWork, IProductRepository productRepository, IMapper mapper) : base(repository, unitOfWork)
        {
            _productRepository = productRepository;
            _mapper = mapper;
        }

        public async Task<CustomResponseDto<List<ProductWithCategoryDto>>> GetProductsWithCategory()
        {
            var products = await _productRepository.GetProductsWithCategory();
            var productDtos = _mapper.Map<List<ProductWithCategoryDto>>(products);
            return CustomResponseDto<List<ProductWithCategoryDto>>.Success(200, productDtos);
        }
    }
}
