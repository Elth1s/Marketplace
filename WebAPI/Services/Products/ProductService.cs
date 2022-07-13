using AutoMapper;
using DAL;
using DAL.Entities;
using DAL.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using WebAPI.Extensions;
using WebAPI.Interfaces.Products;
using WebAPI.Specifications;
using WebAPI.Specifications.Products;
using WebAPI.ViewModels.Request.Products;
using WebAPI.ViewModels.Response.Categories;
using WebAPI.ViewModels.Response.Products;

namespace WebAPI.Services.Products
{
    public class ProductService : IProductService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IRepository<Product> _productRepository;
        private readonly IRepository<Shop> _shopRepository;
        private readonly IRepository<ProductStatus> _productStatusRepository;
        private readonly IRepository<Category> _categoryRepository;
        private readonly IRepository<FilterValue> _filterValueRepository;
        private readonly IRepository<FilterValueProduct> _filterValueProductRepository;
        private readonly IRepository<BasketItem> _basketRepository;
        private readonly IMapper _mapper;

        public ProductService(
            IRepository<Product> productRepository,
            IRepository<Shop> shopRepository,
            IRepository<ProductStatus> productStatusRepository,
            IRepository<Category> categoryRepository,
            IRepository<FilterValue> filterValueRepository,
            IRepository<FilterValueProduct> filterValueProductRepository,
            IRepository<BasketItem> basketRepository,
            UserManager<AppUser> userManager,
            IMapper mapper
            )
        {
            _userManager = userManager;
            _productRepository = productRepository;
            _shopRepository = shopRepository;
            _shopRepository = shopRepository;
            _productStatusRepository = productStatusRepository;
            _categoryRepository = categoryRepository;
            _filterValueRepository = filterValueRepository;
            _filterValueProductRepository = filterValueProductRepository;
            _basketRepository = basketRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ProductResponse>> GetAsync()
        {
            var spec = new ProductIncludeFullInfoSpecification();
            var products = await _productRepository.ListAsync(spec);

            return _mapper.Map<IEnumerable<ProductResponse>>(products);
        }

        public async Task<ProductResponse> GetByIdAsync(int id)
        {
            var spec = new ProductIncludeFullInfoSpecification(id);
            var product = await _productRepository.GetBySpecAsync(spec);
            product.ProductNullChecking();

            return _mapper.Map<ProductResponse>(product);
        }

        public async Task<ProductWithCategoryParentsResponse> GetByUrlSlugAsync(string urlSlug, string userId)
        {
            var response = new ProductWithCategoryParentsResponse();

            var spec = new ProductIncludeFullInfoSpecification(urlSlug);
            var product = await _productRepository.GetBySpecAsync(spec);
            product.ProductNullChecking();

            response.Product = _mapper.Map<ProductPageResponse>(product);

            response.Product.Filters = product.FilterValueProducts.Select(f => new ProductFilterValue()
            {
                Value = f.CustomValue == null ? f.FilterValue.Value : f.CustomValue.ToString(),
                FilterName = f.FilterValue.FilterName.Name,
                UnitMeasure = f.FilterValue.FilterName.Unit?.Measure
            });

            var categories = new List<Category>();
            categories.Add(new Category() { Name = product.Name });
            var category = product.Category;
            do
            {
                categories.Add(category);
                category = await _categoryRepository.GetByIdAsync(category.ParentId);
            } while (category != null);

            categories.Reverse();
            response.Parents = _mapper.Map<IEnumerable<CatalogItemResponse>>(categories);

            var user = await _userManager.FindByIdAsync(userId);
            if (user != null)
            {
                var basketSpec = new BasketItemIncludeFullInfoSpecification(user.Id, product.Id);
                var basket = await _basketRepository.GetBySpecAsync(basketSpec);
                if (basket != null)
                    response.Product.IsInBasket = true;
            }

            return response;
        }

        public async Task CreateAsync(ProductCreateRequest request)
        {
            var shop = await _shopRepository.GetByIdAsync(request.ShopId);
            shop.ShopNullChecking();

            var productStatus = await _productStatusRepository.GetByIdAsync(request.StatusId);
            productStatus.ProductStatusNullChecking();

            var category = await _categoryRepository.GetByIdAsync(request.CategoryId);
            category.CategoryNullChecking();

            var product = _mapper.Map<Product>(request);

            await _productRepository.AddAsync(product);
            await _productRepository.SaveChangesAsync();

            foreach (var filterValue in request.FiltersValue)
            {
                await _filterValueProductRepository.AddAsync(
                    new FilterValueProduct()
                    {
                        
                        FilterValueId = filterValue.Id,
                        ProductId = product.Id,
                        CustomValue = filterValue.CustomValue != null ? filterValue.CustomValue : null
                    });
            }
            await _filterValueProductRepository.SaveChangesAsync();
        }

        //public async Task UpdateAsync(int id, ProductUpdateRequest request)
        //{
        //    var product = await _productRepository.GetByIdAsync(id);
        //    product.ProductNullChecking();

        //    var shop = await _shopRepository.GetByIdAsync(request.ShopId);
        //    shop.ShopNullChecking();

        //    var productStatus = await _productStatusRepository.GetByIdAsync(request.StatusId);
        //    productStatus.ProductStatusNullChecking();

        //    if (request.CategoryId != null)
        //    {
        //        var category = await _categoryRepository.GetByIdAsync(request.CategoryId);
        //        category.CategotyNullChecking();
        //    }

        //    _mapper.Map(request, product);

        //    await _productRepository.UpdateAsync(product);
        //    await _productRepository.SaveChangesAsync();

        //    //foreach (ProductImageForProductUpdateRequest imageRequest in request.ProductImages)
        //    //{
        //    //    var productImage = _mapper.Map<ProductImageRequest>(imageRequest);
        //    //    await _productImageService.UpdateAsync(imageRequest.Id, productImage);
        //    //}
        //}

        public async Task DeleteAsync(int id)
        {
            var product = await _productRepository.GetByIdAsync(id);
            product.ProductNullChecking();

            //var specProductImage = new ProductImageGetByProductSpecification(product.Id);
            //var productImages = await _productImageRepository.ListAsync(specProductImage);

            //if (productImages != null)
            //{
            //    foreach (ProductImage productImage in productImages)
            //    {
            //        await _productImageService.DeleteAsync(productImage.Id);
            //    }
            //}

            await _productRepository.DeleteAsync(product);
            await _productRepository.SaveChangesAsync();
        }
    }
}
