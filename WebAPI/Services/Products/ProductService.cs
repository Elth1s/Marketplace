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
using WebAPI.Specifications.Filters;
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
            IMapper mapper)
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

        public async Task<SearchResponse<ProductCatalogResponse>> SearchProductsAsync(SearchProductRequest request, string userId)
        {
            if (request.ShopId != null)
            {
                var shop = await _shopRepository.GetByIdAsync(request.ShopId);
                shop.ShopNullChecking();
            }

            var response = new SearchResponse<ProductCatalogResponse>();

            var products = new List<Product>();
            var categories = new List<Category>();
            var filters = new List<FilterValue>();
            if (request.Categories != null)
            {
                var categoryPredicate = PredicateBuilder.False<Category>();
                foreach (var item in request.Categories)
                {
                    categoryPredicate = categoryPredicate
                        .Or(p => p.Id == item);
                }
                var categoriesSpec = new CategoryGetByIdsSpecification(categoryPredicate);
                categories = await _categoryRepository.ListAsync(categoriesSpec);
                if (request.Categories.Count() == 1)
                    if (request.Filters != null)
                    {
                        var filterPredicate = PredicateBuilder.False<FilterValue>();
                        foreach (var item in request.Filters)
                        {
                            filterPredicate = filterPredicate
                                .Or(p => p.Id == item);
                        }
                        var filterValuesSpec = new FilterValueGetByIdsSpecification(filterPredicate);
                        filters = await _filterValueRepository.ListAsync(filterValuesSpec);
                    }
            }

            var productSearchSpec = new ProductSearchSpecification(request.ShopId, request.ProductName, request.Categories == null ? null : categories, request.Filters == null ? null : filters, null, null);
            response.Count = await _productRepository.CountAsync(productSearchSpec);

            productSearchSpec = new ProductSearchSpecification(request.ShopId, request.ProductName, request.Categories == null ? null : categories, request.Filters == null ? null : filters, request.Page, request.RowsPerPage);
            products = await _productRepository.ListAsync(productSearchSpec);

            response.Values = _mapper.Map<IEnumerable<ProductCatalogResponse>>(products);

            var user = await _userManager.GetUserWithSelectedProductsAsync(userId);
            if (user != null)
            {
                foreach (var item in response.Values)
                {
                    item.IsSelected = await _userManager.IsProductSelectedByUserAsync(user.Id, item.Id);
                }
            }

            return response;
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

            var user = await _userManager.GetUserWithReviewedProductsAsync(userId);
            if (user != null)
            {
                var basketSpec = new BasketItemIncludeFullInfoSpecification(user.Id, product.Id);
                var basket = await _basketRepository.GetBySpecAsync(basketSpec);
                if (basket != null)
                    response.Product.IsInBasket = true;

                response.Product.IsSelected = await _userManager.IsProductSelectedByUserAsync(user.Id, product.Id);

                if (user.ReviewedProducts.FirstOrDefault(p => p.Id == product.Id) == null)
                {
                    user.ReviewedProducts.Add(product);
                    await _userManager.UpdateAsync(user);
                }

            }

            return response;
        }

        public async Task<IEnumerable<ProductCatalogResponse>> GetSimilarProductsAsync(string urlSlug, string userId)
        {
            var specGetByUrlSlug = new ProductIncludeFullInfoSpecification(urlSlug);
            var product = await _productRepository.GetBySpecAsync(specGetByUrlSlug);
            product.ProductNullChecking();

            var spec = new ProductGetByCategoryIdSpecification(product.CategoryId, null, 1, 20, product.Id);
            var products = await _productRepository.ListAsync(spec);

            var response = _mapper.Map<IEnumerable<ProductCatalogResponse>>(products);

            var user = await _userManager.GetUserWithSelectedProductsAsync(userId);
            if (user != null)
            {
                foreach (var item in response)
                {
                    item.IsSelected = await _userManager.IsProductSelectedByUserAsync(user.Id, item.Id);
                }
            }

            return response;
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

        public async Task<SearchResponse<ProductResponse>> AdminSellerSearchProductsAsync(SellerSearchRequest request, string userId)
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
            var response = new SearchResponse<ProductResponse>() { Count = count, Values = mappedProducts };

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


        #region Selected And Reviewed Products

        public async Task ChangeSelectProductAsync(string productSlug, string userId)
        {
            var productSpec = new ProductGetByUrlSlugSpecification(productSlug);
            var product = await _productRepository.GetBySpecAsync(productSpec);
            product.ProductNullChecking();

            var user = await _userManager.GetUserWithSelectedProductsAsync(userId);
            user.UserNullChecking();

            var isExist = user.SelectedProducts.FirstOrDefault(p => p.Id == product.Id);
            if (isExist != null)
                user.SelectedProducts.Remove(product);
            else
                user.SelectedProducts.Add(product);

            await _userManager.UpdateAsync(user);
        }

        public async Task<IEnumerable<ProductWithCartResponse>> GetSelectedProductsAsync(string userId)
        {
            var user = await _userManager.GetUserWithSelectedProductsAsync(userId);
            user.UserNullChecking();

            var response = _mapper.Map<IEnumerable<ProductWithCartResponse>>(user.SelectedProducts);

            foreach (var item in response)
            {
                var basketSpec = new BasketItemIncludeFullInfoSpecification(user.Id, item.Id);
                var basket = await _basketRepository.GetBySpecAsync(basketSpec);
                if (basket != null)
                    item.IsInBasket = true;

                item.IsSelected = true;
            }

            return response;
        }

        public async Task<IEnumerable<ProductWithCartResponse>> GetReviewedProductsAsync(string userId)
        {
            var user = await _userManager.GetUserWithReviewedProductsAsync(userId);
            user.UserNullChecking();

            var response = _mapper.Map<IEnumerable<ProductWithCartResponse>>(user.ReviewedProducts);

            foreach (var item in response)
            {
                var basketSpec = new BasketItemIncludeFullInfoSpecification(user.Id, item.Id);
                var basket = await _basketRepository.GetBySpecAsync(basketSpec);
                if (basket != null)
                    item.IsInBasket = true;

                item.IsSelected = await _userManager.IsProductSelectedByUserAsync(user.Id, item.Id);
            }

            return response;
        }

        #endregion
    }
}
