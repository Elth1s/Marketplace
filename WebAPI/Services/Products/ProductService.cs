using AutoMapper;
using DAL;
using DAL.Constants;
using DAL.Entities;
using Microsoft.AspNetCore.Identity;
using WebAPI.Extensions;
using WebAPI.Helpers;
using WebAPI.Interfaces.Products;
using WebAPI.Specifications;
using WebAPI.Specifications.Categories;
using WebAPI.Specifications.Products;
using WebAPI.ViewModels.Request;
using WebAPI.ViewModels.Request.Products;
using WebAPI.ViewModels.Response;
using WebAPI.ViewModels.Response.Categories;
using WebAPI.ViewModels.Response.Products;
namespace WebAPI.Services.Products
{
    public class ProductService : IProductService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IRepository<Product> _productRepository;
        private readonly IRepository<ProductImage> _productImageRepository;
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
            IRepository<ProductImage> productImageRepository,
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
            _productImageRepository = productImageRepository;
            _shopRepository = shopRepository;
            _shopRepository = shopRepository;
            _productStatusRepository = productStatusRepository;
            _productImageRepository = productImageRepository;
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

            var spec = new ProductIncludeFullInfoSpecification(urlSlug);
            var product = await _productRepository.GetBySpecAsync(spec);
            product.ProductNullChecking();

            var response = new ProductWithCategoryParentsResponse
            {
                Product = _mapper.Map<ProductPageResponse>(product)
            };

            response.Product.Filters = product.FilterValueProducts.Select(f => new ProductFilterValue()
            {
                Value = f.CustomValue == null ? f.FilterValue.FilterValueTranslations.FirstOrDefault(
                                                f => f.LanguageId == CurrentLanguage.Id)?.Value : f.CustomValue.ToString(),
                FilterName = f.FilterValue.FilterName.FilterNameTranslations.FirstOrDefault(
                                                f => f.LanguageId == CurrentLanguage.Id)?.Name,
                UnitMeasure = f.FilterValue.FilterName.Unit?.UnitTranslations.FirstOrDefault(
                                                f => f.LanguageId == CurrentLanguage.Id)?.Measure
            });

            var categories = new List<Category>();
            var category = product.Category;
            do
            {
                categories.Add(category);
                var specCategory = new CategoryIncludeFullInfoSpecification(category.ParentId);
                category = await _categoryRepository.GetBySpecAsync(specCategory);
            } while (category != null);

            categories.Reverse();
            var temp = _mapper.Map<List<CatalogItemResponse>>(categories);
            temp.Add(new CatalogItemResponse() { Name = product.Name });
            response.Parents = temp;

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

        public async Task<IEnumerable<ProductCatalogResponse>> GetSimilarProductsAsync(string urlSlug)
        {
            var specGetByUrlSlug = new ProductIncludeFullInfoSpecification(urlSlug);
            var product = await _productRepository.GetBySpecAsync(specGetByUrlSlug);
            product.ProductNullChecking();

            var spec = new ProductGetByCategoryIdSpecification(product.CategoryId, null, 1, 20, product.Id);
            var products = await _productRepository.ListAsync(spec);

            return _mapper.Map<IEnumerable<ProductCatalogResponse>>(products);
        }

        public async Task CreateAsync(ProductCreateRequest request, string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            user.UserNullChecking();

            var shop = await _shopRepository.GetByIdAsync(user.ShopId);
            shop.ShopNullChecking();

            var productStatus = await _productStatusRepository.GetByIdAsync(request.StatusId);
            productStatus.ProductStatusNullChecking();

            var category = await _categoryRepository.GetByIdAsync(request.CategoryId);
            category.CategoryNullChecking();

            var product = _mapper.Map<Product>(request);
            product.ShopId = shop.Id;
            product.UrlSlug = Guid.NewGuid();

            await _productRepository.AddAsync(product);
            await _productRepository.SaveChangesAsync();

            foreach (var filterValue in request.FiltersValue)
            {
                await _filterValueProductRepository.AddAsync(
                    new FilterValueProduct()
                    {
                        FilterValueId = filterValue.ValueId,
                        ProductId = product.Id,
                        CustomValue = filterValue.CustomValue != null ? filterValue.CustomValue : null
                    });
            }
            await _filterValueProductRepository.SaveChangesAsync();

            foreach (var image in request.Images)
            {
                var productImage = await _productImageRepository.GetByIdAsync(image.Id);
                productImage.ProductId = product.Id;
                await _productImageRepository.UpdateAsync(productImage);
            }
            await _productImageRepository.SaveChangesAsync();
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

            product.IsDeleted = true;
            product.StatusId = ProductStatusId.NotAvailable;

            await _productRepository.UpdateAsync(product);
            await _productRepository.SaveChangesAsync();
        }

        public async Task<AdminSearchResponse<ProductResponse>> SearchProductsAsync(SellerSearchRequest request, string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            user.UserNullChecking();

            var shop = await _shopRepository.GetByIdAsync(user.ShopId);
            shop.ShopNullChecking();

            var spec = new ProductSearchSpecification(request.Name, request.IsAscOrder, request.OrderBy, request.IsSeller, shop.Id);
            var count = await _productRepository.CountAsync(spec);
            spec = new ProductSearchSpecification(
                request.Name,
                request.IsAscOrder,
                request.OrderBy,
                request.IsSeller,
                user.ShopId,
                (request.Page - 1) * request.RowsPerPage,
                request.RowsPerPage);

            var products = await _productRepository.ListAsync(spec);
            var mappedProducts = _mapper.Map<IEnumerable<ProductResponse>>(products);
            var response = new AdminSearchResponse<ProductResponse>() { Count = count, Values = mappedProducts };

            return response;
        }

        public async Task DeleteProductsAsync(IEnumerable<int> ids)
        {
            foreach (var item in ids)
            {
                var product = await _productRepository.GetByIdAsync(item);

                product.IsDeleted = true;
                product.StatusId = ProductStatusId.NotAvailable;

                await _productRepository.UpdateAsync(product);
            }
            await _productRepository.SaveChangesAsync();
        }
    }
}
