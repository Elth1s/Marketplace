using AutoMapper;
using DAL;
using DAL.Entities;
using Microsoft.Extensions.Localization;
using System.Drawing.Imaging;
using WebAPI.Constants;
using WebAPI.Exceptions;
using WebAPI.Extensions;
using WebAPI.Helpers;
using WebAPI.Interfaces;
using WebAPI.Specifications.Categories;
using WebAPI.Specifications.Filters;
using WebAPI.Specifications.Products;
using WebAPI.ViewModels.Request;
using WebAPI.ViewModels.Request.Categories;
using WebAPI.ViewModels.Response;
using WebAPI.ViewModels.Response.Categories;
using WebAPI.ViewModels.Response.Filters;
using WebAPI.ViewModels.Response.Products;

namespace WebAPI.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly IStringLocalizer<ErrorMessages> _errorMessagesLocalizer;
        private readonly IRepository<Category> _categoryRepository;
        private readonly IRepository<Product> _productRepository;
        private readonly IRepository<FilterValue> _filterValueRepository;
        private readonly IMapper _mapper;

        public CategoryService(IStringLocalizer<ErrorMessages> errorMessagesLocalizer,
            IRepository<Category> categorRepository,
            IRepository<Product> productRepository,
            IRepository<FilterValue> filterValueRepository,
            IMapper mapper)
        {
            _errorMessagesLocalizer = errorMessagesLocalizer;
            _categoryRepository = categorRepository;
            _productRepository = productRepository;
            _filterValueRepository = filterValueRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<CategoryResponse>> GetAsync()
        {
            var spec = new CategoryIncludeFullInfoSpecification();
            var categories = await _categoryRepository.ListAsync(spec);
            return _mapper.Map<IEnumerable<CategoryResponse>>(categories);
        }

        public async Task<AdminSearchResponse<CategoryResponse>> SearchCategoriesAsync(AdminSearchRequest request)
        {
            var spec = new CategorySearchSpecification(request.Name, request.IsAscOrder, request.OrderBy);
            var count = await _categoryRepository.CountAsync(spec);
            spec = new CategorySearchSpecification(
                request.Name,
                request.IsAscOrder,
                request.OrderBy,
                (request.Page - 1) * request.RowsPerPage,
                request.RowsPerPage);

            var categories = await _categoryRepository.ListAsync(spec);
            var mappedCategories = _mapper.Map<IEnumerable<CategoryResponse>>(categories);

            var response = new AdminSearchResponse<CategoryResponse>() { Count = count, Values = mappedCategories };

            return response;
        }
        public async Task<IEnumerable<CatalogItemResponse>> GetCatalogAsync()
        {
            var spec = new CatalogSpecification();
            var categories = await _categoryRepository.ListAsync(spec);
            return _mapper.Map<IEnumerable<CatalogItemResponse>>(categories);
        }

        public async Task<IEnumerable<FullCatalogItemResponse>> GetFullCatalogAsync()
        {
            var spec = new CatalogSpecification(null);
            var categories = await _categoryRepository.ListAsync(spec);
            return _mapper.Map<IEnumerable<FullCatalogItemResponse>>(categories);
        }

        public async Task<CatalogWithProductsResponse> GetCatalogWithProductsAsync(CatalogWithProductsRequest request)
        {

            var urlSlugSpec = new CategoryGetByUrlSlugSpecification(request.UrlSlug);
            var category = await _categoryRepository.GetBySpecAsync(urlSlugSpec);
            category.CategoryNullChecking();

            var response = new CatalogWithProductsResponse
            {
                Name = category.Name
            };

            var parentIdSpec = new CategoryGetByParentIdSpecification(category.Id);
            var childs = await _categoryRepository.ListAsync(parentIdSpec);
            response.CatalogItems = _mapper.Map<IEnumerable<CatalogItemResponse>>(childs);

            var products = new List<Product>();
            var filters = new List<FilterValue>();
            if (childs.Count == 0)
            {
                if (request.Filters != null)
                {
                    var filterPredicate = PredicateBuilder.False<FilterValue>();
                    foreach (var item in request.Filters)
                    {
                        filterPredicate = filterPredicate
                            .Or(p => p.Id == item);
                    }
                    var filterValuesSpec = new FilterValuesGetByIdsSpecification(filterPredicate);
                    filters = await _filterValueRepository.ListAsync(filterValuesSpec);
                }
                var productCategoryIdSpec = new ProductGetByCategoryIdSpecification(category.Id, request.Filters == null ? null : filters, null, null);
                response.CountProducts = await _productRepository.CountAsync(productCategoryIdSpec);

                productCategoryIdSpec = new ProductGetByCategoryIdSpecification(category.Id, request.Filters == null ? null : filters, request.Page, request.RowsPerPage);
                products = await _productRepository.ListAsync(productCategoryIdSpec);

                response.Products = _mapper.Map<IEnumerable<ProductCatalogResponse>>(products); ;
            }
            else
            {
                foreach (var item in childs)
                {
                    var productCategoryIdSpec = new ProductGetByCategoryIdSpecification(item.Id, null, null, null);
                    response.CountProducts += await _productRepository.CountAsync(productCategoryIdSpec);

                    productCategoryIdSpec = new ProductGetByCategoryIdSpecification(item.Id, null, request.Page, 5);
                    products.AddRange(await _productRepository.ListAsync(productCategoryIdSpec));
                }
                response.Products = _mapper.Map<IEnumerable<ProductCatalogResponse>>(products);
            }

            return response;
        }

        public async Task<IEnumerable<ProductCatalogResponse>> GetMoreProductsAsync(CatalogWithProductsRequest request)
        {
            var urlSlugSpec = new CategoryGetByUrlSlugSpecification(request.UrlSlug);
            var category = await _categoryRepository.GetBySpecAsync(urlSlugSpec);

            var parentIdSpec = new CategoryGetByParentIdSpecification(category.Id);
            var childs = await _categoryRepository.ListAsync(parentIdSpec);

            var products = new List<Product>();
            foreach (var item in childs)
            {
                var productCategoryIdSpec = new ProductGetByCategoryIdSpecification(item.Id, null, request.Page, 5);
                products.AddRange(await _productRepository.ListAsync(productCategoryIdSpec));
            }
            return _mapper.Map<IEnumerable<ProductCatalogResponse>>(products);
        }

        public async Task<IEnumerable<FilterNameValuesResponse>> GetFiltersByCategoryAsync(string urlSlug)
        {
            var spec = new CategoryGetWithFilterValues(urlSlug);
            var category = await _categoryRepository.GetBySpecAsync(spec);
            category.CategoryNullChecking();

            var filters = category.FilterValues;
            var gropedFilters = filters.GroupBy(f => f.FilterName);
            var response = gropedFilters.Select(g => new FilterNameValuesResponse()
            {
                Id = g.Key.Id,
                Name = g.Key.Name,
                UnitMeasure = g.Key.Unit?.Measure,
                FilterValues = _mapper.Map<IEnumerable<FilterValueCatalogResponse>>(g.Key.FilterValues)
            });

            return response;
        }

        public async Task<IEnumerable<CatalogItemResponse>> GetParentsAsync(string urlSlug)
        {
            var spec = new CategoryIncludeFullInfoSpecification(urlSlug);
            var category = await _categoryRepository.GetBySpecAsync(spec);
            category.CategoryNullChecking();

            var categories = new List<Category>();
            do
            {
                categories.Add(category);
                category = await _categoryRepository.GetByIdAsync(category.ParentId);
            } while (category != null);

            categories.Reverse();

            return _mapper.Map<IEnumerable<CatalogItemResponse>>(categories);
        }

        public async Task<IEnumerable<CategoryForSelectResponse>> GetForSelectAsync()
        {
            var categories = await _categoryRepository.ListAsync();
            return _mapper.Map<IEnumerable<CategoryForSelectResponse>>(categories);
        }

        public async Task<CategoryResponse> GetByIdAsync(int id)
        {
            var spec = new CategoryIncludeFullInfoSpecification(id);
            var category = await _categoryRepository.GetBySpecAsync(spec);
            category.CategoryNullChecking();

            return _mapper.Map<CategoryResponse>(category);
        }

        public async Task CreateAsync(CategoryRequest request)
        {
            if (request.ParentId != null)
            {
                var parentCategory = await _categoryRepository.GetByIdAsync(request.ParentId);
                parentCategory.CategoryNullChecking();
            }

            var nameSpec = new CategoryGetByNameSpecification(request.Name);
            if (await _categoryRepository.GetBySpecAsync(nameSpec) != null)
                throw new AppException(_errorMessagesLocalizer["CategoryNameNotUnique"]);
            var urlSlugSpec = new CategoryGetByUrlSlugSpecification(request.Name);
            if (await _categoryRepository.GetBySpecAsync(urlSlugSpec) != null)
                throw new AppException(_errorMessagesLocalizer["CategoryUrlSlugNotUnique"]);

            var category = _mapper.Map<Category>(request);

            if (!string.IsNullOrEmpty(request.Image))
            {
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), request.Image.Replace(ImagePath.RequestCategoriesImagePath, ImagePath.CategoriesImagePath));

                if (!File.Exists(filePath))
                {
                    var img = ImageWorker.FromBase64StringToImage(request.Image);
                    string randomFilename = Guid.NewGuid() + ".png";
                    var dir = Path.Combine(Directory.GetCurrentDirectory(), ImagePath.CategoriesImagePath, randomFilename);
                    img.Save(dir, ImageFormat.Png);

                    category.Image = randomFilename;
                }
            }

            if (!string.IsNullOrEmpty(request.Icon))
            {
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), request.Icon.Replace(ImagePath.RequestCategoriesImagePath, ImagePath.CategoriesImagePath));

                if (!File.Exists(filePath))
                {
                    var img = ImageWorker.FromBase64StringToImage(request.Icon);
                    string randomFilename = Guid.NewGuid() + ".png";
                    var dir = Path.Combine(Directory.GetCurrentDirectory(), ImagePath.CategoriesImagePath, randomFilename);
                    img.Save(dir, ImageFormat.Png);

                    category.Icon = randomFilename;
                }
            }

            await _categoryRepository.AddAsync(category);
            await _categoryRepository.SaveChangesAsync();
        }

        public async Task UpdateAsync(int id, CategoryRequest request)
        {
            if (request.ParentId != null)
            {
                var parentCategory = await _categoryRepository.GetByIdAsync(request.ParentId);
                parentCategory.CategoryNullChecking();
            }

            var nameSpec = new CategoryGetByNameSpecification(request.Name);
            var categoryNameExist = await _categoryRepository.GetBySpecAsync(nameSpec);
            if (categoryNameExist != null && categoryNameExist.Id != id)
                categoryNameExist.CategoryNameChecking();
            var urlSlugSpec = new CategoryGetByUrlSlugSpecification(request.Name);
            var categoryUrlSlugExist = await _categoryRepository.GetBySpecAsync(urlSlugSpec);
            if (categoryUrlSlugExist != null && categoryUrlSlugExist.Id != id)
                categoryUrlSlugExist.CategoryUrlSlugChecking();

            var category = await _categoryRepository.GetByIdAsync(id);
            category.CategoryNullChecking();

            if (!string.IsNullOrEmpty(request.Image))
            {
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), request.Image.Replace(ImagePath.RequestCategoriesImagePath, ImagePath.CategoriesImagePath));
                if (!File.Exists(filePath))
                {
                    if (category.Image != null)
                    {
                        filePath = Path.Combine(Directory.GetCurrentDirectory(), ImagePath.CategoriesImagePath, category.Image);

                        if (File.Exists(filePath))
                        {
                            File.Delete(filePath);
                        }
                    }
                    var img = ImageWorker.FromBase64StringToImage(request.Image);
                    string randomFilename = Guid.NewGuid() + ".png";
                    var dir = Path.Combine(Directory.GetCurrentDirectory(), ImagePath.CategoriesImagePath, randomFilename);
                    img.Save(dir, ImageFormat.Png);

                    category.Image = randomFilename;
                }
            }
            if (!string.IsNullOrEmpty(request.Icon))
            {
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), request.Icon.Replace(ImagePath.RequestCategoriesImagePath, ImagePath.CategoriesImagePath));
                if (!File.Exists(filePath))
                {
                    if (category.Icon != null)
                    {
                        filePath = Path.Combine(Directory.GetCurrentDirectory(), ImagePath.CategoriesImagePath, category.Icon);

                        if (File.Exists(filePath))
                        {
                            File.Delete(filePath);
                        }
                    }
                    var img = ImageWorker.FromBase64StringToImage(request.Icon);
                    string randomFilename = Guid.NewGuid() + ".png";
                    var dir = Path.Combine(Directory.GetCurrentDirectory(), ImagePath.CategoriesImagePath, randomFilename);
                    img.Save(dir, ImageFormat.Png);

                    category.Icon = randomFilename;
                }
            }

            _mapper.Map(request, category);

            await _categoryRepository.UpdateAsync(category);
            await _categoryRepository.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var category = await _categoryRepository.GetByIdAsync(id);
            category.CategoryNullChecking();

            if (!string.IsNullOrEmpty(category.Image))
            {
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), ImagePath.CategoriesImagePath, category.Image);

                if (File.Exists(filePath))
                {
                    File.Delete(filePath);
                }
            }
            if (!string.IsNullOrEmpty(category.Icon))
            {
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), ImagePath.CategoriesImagePath, category.Icon);

                if (File.Exists(filePath))
                {
                    File.Delete(filePath);
                }
            }

            await _categoryRepository.DeleteAsync(category);
            await _categoryRepository.SaveChangesAsync();
        }

        public async Task DeleteCategoriesAsync(IEnumerable<int> ids)
        {
            foreach (var item in ids)
            {
                var category = await _categoryRepository.GetByIdAsync(item);
                //country.CountryNullChecking();
                if (!string.IsNullOrEmpty(category.Image))
                {
                    var filePath = Path.Combine(Directory.GetCurrentDirectory(), ImagePath.CategoriesImagePath, category.Image);

                    if (File.Exists(filePath))
                    {
                        File.Delete(filePath);
                    }
                }
                if (!string.IsNullOrEmpty(category.Icon))
                {
                    var filePath = Path.Combine(Directory.GetCurrentDirectory(), ImagePath.CategoriesImagePath, category.Icon);

                    if (File.Exists(filePath))
                    {
                        File.Delete(filePath);
                    }
                }
                await _categoryRepository.DeleteAsync(category);
            }
            await _categoryRepository.SaveChangesAsync();
        }
    }
}
