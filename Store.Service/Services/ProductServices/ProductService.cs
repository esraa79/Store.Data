using Store.Data.Entities;
using Store.Repository.Interfaces;
using Store.Service.Services.ProductServices.Dtos;
using ProductEntity = Store.Data.Entities.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Store.Repository.Specification.ProductSpec;
using Store.Service.Helper;

namespace Store.Service.Services.ProductServices
{
    public class ProductService : IProductService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ProductService(IUnitOfWork unitOfWork,IMapper mapper) 
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<IReadOnlyList<BrandTypeDetailsDto>> GetAllBrandsAsync()
        {
            var brands = await _unitOfWork.Repository<ProductBrand,int>().GetAllAsNoTrackingAsync();
           var MappedBrands = _mapper.Map<IReadOnlyList<BrandTypeDetailsDto>>(brands);
            return MappedBrands;
        }

        public async Task<PaginatedResultDto<ProductDetailsDto>> GetAllProductsAsync(ProductSpecification input)
        {
            var specs = new ProductWithSpecifications(input);
            var products = await _unitOfWork.Repository<ProductEntity, int>().GetAllWithSpecAsync(specs);
            var countSpec = new ProductWithCountSpecification(input);
            var count = await _unitOfWork.Repository<ProductEntity, int>().GetCountSpecAsync(countSpec);
             var mappedProducts =_mapper.Map<IReadOnlyList<ProductDetailsDto>>(products);

            return new PaginatedResultDto<ProductDetailsDto>(input.PageSize,count, input.PageIndex,mappedProducts);
        }

        public async Task<IReadOnlyList<BrandTypeDetailsDto>> GetAllTypesAsync()
        {
            var types = await _unitOfWork.Repository<ProductType, int>().GetAllAsNoTrackingAsync();
            var MappedTypes = _mapper.Map<IReadOnlyList<BrandTypeDetailsDto>>(types);

            return MappedTypes;
        }

        public async Task<ProductDetailsDto> GetProductByIDAsync(int? productId)
        {

            if (productId is null)
                throw new Exception("Id is null");
            var specs = new ProductWithSpecifications(productId);
            var product = await _unitOfWork.Repository<ProductEntity, int>().GetWithSpecByIdAsync(specs);
            if (product is null)
                throw new Exception("Product Not found");
            var mappedProduct = _mapper.Map<ProductDetailsDto>(product);

            return mappedProduct;
        }
    }
}
