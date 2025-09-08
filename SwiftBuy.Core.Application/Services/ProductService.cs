using AutoMapper;
using SwiftBuy.Core.Application.Abstraction.Common;
using SwiftBuy.Core.Application.Abstraction.Models;
using SwiftBuy.Core.Application.Abstraction.Services;
using SwiftBuy.Core.Domain.Common.Entities;
using SwiftBuy.Core.Domain.Contracts;
using SwiftBuy.Core.Domain.Entities.Product;
using SwiftBuy.Core.Domain.Specifications.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwiftBuy.Core.Application.Services
{
    internal class ProductService : IProductService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ProductService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<Pagination<ProductToReturnDto>> GetPrductsAsync(ProductSpecParams specParams)
        {
            var spec = new ProductWithBrandAndCategorySpecifications(specParams.Sort, specParams.BrandId, specParams.CategoryId, specParams.PageIndex, specParams.PageSize, specParams.Search);
            var products = await _unitOfWork.GetRepository<Product, int>().GetAllWithSpecAsync(spec);
            var productsToReturnDto = _mapper.Map<IEnumerable<ProductToReturnDto>>(products);

            var countSpec = new ProductWithFilterationForCountSpecs(specParams.BrandId, specParams.CategoryId, specParams.Search);
            var count = await _unitOfWork.GetRepository<Product, int>().GetCountAsync(countSpec);
            return new Pagination<ProductToReturnDto>(specParams.PageIndex, specParams.PageSize, count, productsToReturnDto);
        }

        public async Task<ProductToReturnDto> GetPrductByIdAsync(int id)
        {
            var spec = new ProductWithBrandAndCategorySpecifications(id);
            var product = await _unitOfWork.GetRepository<Product, int>().GetByIdWithSpecAsync(spec);
            return _mapper.Map<ProductToReturnDto>(product);
        }

        public async Task<IEnumerable<BrandDto>> GetBrandsAsync()
        {
            var brands = await _unitOfWork.GetRepository<ProductBrand, int>().GetAllAsync();
            return _mapper.Map<IEnumerable<BrandDto>>(brands);
        }

        public async Task<IEnumerable<CategoryDto>> GetCategoriesAsync()
        {
            var categories = await _unitOfWork.GetRepository<ProductCategory, int>().GetAllAsync();
            return _mapper.Map<IEnumerable<CategoryDto>>(categories);
        }
    }
}
