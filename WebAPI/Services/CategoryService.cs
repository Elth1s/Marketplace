using AutoMapper;
using DAL;
using DAL.Constants;
using DAL.Data;
using DAL.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Drawing.Imaging;
using WebAPI.Constants;
using WebAPI.Extensions;
using WebAPI.Helpers;
using WebAPI.Interfaces;
using WebAPI.Specifications.Categories;
using WebAPI.Specifications.Filters;
using WebAPI.Specifications.Products;
using WebAPI.ViewModels.Request;
using WebAPI.ViewModels.Request.Categories;
using WebAPI.ViewModels.Request.Products;
using WebAPI.ViewModels.Response;
using WebAPI.ViewModels.Response.Categories;
using WebAPI.ViewModels.Response.Filters;
using WebAPI.ViewModels.Response.Products;

namespace WebAPI.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IRepository<Category> _categoryRepository;
        private readonly IRepository<Product> _productRepository;
        private readonly IRepository<FilterValue> _filterValueRepository;
        private readonly IRepository<Shop> _shopRepository;
        private readonly IMapper _mapper;

        private readonly MarketplaceDbContext _context;

        public CategoryService(
            IRepository<Category> categorRepository,
            IRepository<Product> productRepository,
            IRepository<FilterValue> filterValueRepository,
            IRepository<Shop> shopRepository,
            IMapper mapper,
            MarketplaceDbContext context,
            UserManager<AppUser> userManager)
        {
            _categoryRepository = categorRepository;
            _productRepository = productRepository;
            _filterValueRepository = filterValueRepository;
            _shopRepository = shopRepository;
            _mapper = mapper;
            _userManager = userManager;
            _context = context;
        }

        public async Task<IEnumerable<CategoryResponse>> GetAsync()
        {
            var spec = new CategoryIncludeFullInfoSpecification();
            var categories = await _categoryRepository.ListAsync(spec);
            return _mapper.Map<IEnumerable<CategoryResponse>>(categories);
        }

        public async Task<SearchResponse<CategoryResponse>> SearchCategoriesAsync(AdminSearchRequest request)
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

            var response = new SearchResponse<CategoryResponse>() { Count = count, Values = mappedCategories };

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

        public async Task<CatalogWithProductsResponse> GetCatalogWithProductsAsync(CatalogWithProductsRequest request, string userId)
        {

            var urlSlugSpec = new CategoryGetByUrlSlugSpecification(request.UrlSlug);
            var category = await _categoryRepository.GetBySpecAsync(urlSlugSpec);
            category.CategoryNullChecking();

            var response = new CatalogWithProductsResponse
            {
                Name = category.CategoryTranslations.FirstOrDefault(c => c.LanguageId == CurrentLanguage.Id).Name
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
                    var filterValuesSpec = new FilterValueGetByIdsSpecification(filterPredicate);
                    filters = await _filterValueRepository.ListAsync(filterValuesSpec);
                }
                var productCategoryIdSpec = new ProductGetByCategoryIdSpecification(category.Id, request.Filters == null ? null : filters, null, null);
                response.CountProducts = await _productRepository.CountAsync(productCategoryIdSpec);

                productCategoryIdSpec = new ProductGetByCategoryIdSpecification(category.Id, request.Filters == null ? null : filters, request.Page, request.RowsPerPage);
                products = await _productRepository.ListAsync(productCategoryIdSpec);

                response.Products = _mapper.Map<IEnumerable<ProductCatalogResponse>>(products);
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

            var user = await _userManager.GetUserWithSelectedProductsAsync(userId);
            if (user != null)
            {
                foreach (var item in response.Products)
                {
                    item.IsSelected = await _userManager.IsProductSelectedByUserAsync(user.Id, item.Id);
                }
            }

            return response;
        }

        public async Task<IEnumerable<FullCatalogItemResponse>> GetCategoriesByProductsAsync(SearchProductRequest request)
        {
            if (request.ShopId != null)
            {
                var shop = await _shopRepository.GetByIdAsync(request.ShopId);
                shop.ShopNullChecking();
            }

            var query = _context.Products
                .Include(o => o.Category).ThenInclude(c => c.CategoryTranslations)
                 .Include(o => o.Status).ThenInclude(s => s.ProductStatusTranslations)
                 .Include(pi => pi.Images)
                 .AsQueryable();
            if (!string.IsNullOrEmpty(request.ProductName))
                query = query.Where(item => item.Name.Contains(request.ProductName));
            if (request.ShopId != null)
                query = query.Where(item => item.ShopId == request.ShopId);

            var categoryQuery = query.GroupBy(p => p.CategoryId).Select(p => p.Key);
            var categoriesId = await categoryQuery.ToListAsync();

            var response = new List<FullCatalogItemResponse>();

            foreach (var item in categoriesId)
            {
                var categories = new List<Category>();
                var spec = new CategoryIncludeFullInfoSpecification(item);
                var category = await _categoryRepository.GetBySpecAsync(spec);
                while (category.Parent != null)
                {
                    categories.Add(category);
                    spec = new CategoryIncludeFullInfoSpecification(category.Parent.Id);
                    category = await _categoryRepository.GetBySpecAsync(spec);
                }
                categories.Add(category);

                var isExist = response.Where(c => c.Id == category.Id).FirstOrDefault();
                if (isExist == null)
                {
                    response.Add(_mapper.Map<FullCatalogItemResponse>(category));
                    response[response.Count - 1].Children = new List<FullCatalogItemResponse>() { _mapper.Map<FullCatalogItemResponse>(categories.First()) };
                }
                else
                {
                    var index = response.IndexOf(isExist);
                    response[index].Children = response[index].Children.Append(_mapper.Map<FullCatalogItemResponse>(categories.First()));
                }
            }

            return response;
        }

        public async Task<IEnumerable<ProductCatalogResponse>> GetMoreProductsAsync(CatalogWithProductsRequest request, string userId)
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
                Name = g.Key.FilterNameTranslations.FirstOrDefault(f => f.LanguageId == CurrentLanguage.Id).Name,
                UnitMeasure = g.Key.Unit?.UnitTranslations.FirstOrDefault(f => f.LanguageId == CurrentLanguage.Id).Measure,
                FilterValues = _mapper.Map<IEnumerable<FilterValueCatalogResponse>>(g.Key.FilterValues)
            });

            return response;
        }

        public async Task<IEnumerable<FilterGroupSellerResponse>> GetFiltersByCategoryIdAsync(int id)
        {
            var spec = new CategoryGetWithFilterValues(id);
            var category = await _categoryRepository.GetBySpecAsync(spec);
            category.CategoryNullChecking();

            var filters = category.FilterValues;
            var gropedFilterNames = filters.GroupBy(f => f.FilterName);
            var gropedFilterGroups = gropedFilterNames.GroupBy(f => f.Key.FilterGroup);
            var response = gropedFilterGroups.Select(g => new FilterGroupSellerResponse()
            {
                Id = g.Key.Id,
                Name = g.Key.FilterGroupTranslations.FirstOrDefault(f => f.LanguageId == CurrentLanguage.Id).Name,
                FilterNames = g.Select(n => new FilterNameSellerResponse()
                {
                    Id = n.Key.Id,
                    Name = n.Key.FilterNameTranslations.FirstOrDefault(f => f.LanguageId == CurrentLanguage.Id).Name,
                    UnitMeasure = n.Key.Unit?.UnitTranslations.FirstOrDefault(f => f.LanguageId == CurrentLanguage.Id).Measure,
                    FilterValues = _mapper.Map<IEnumerable<FilterValueSellerResponse>>(n.Key.FilterValues)
                })
            });


            return response;
        }

        public async Task<IEnumerable<CatalogItemResponse>> GetParentsAsync(string urlSlug)
        {
            var spec = new CategoryIncludeFullInfoSpecification(urlSlug);
            var category = await _categoryRepository.GetBySpecAsync(spec);
            category.CategoryNullChecking();

            var categories = new List<Category>();

            while (category.Parent != null)
            {
                categories.Add(category);
                spec = new CategoryIncludeFullInfoSpecification(category.Parent.Id);
                category = await _categoryRepository.GetBySpecAsync(spec);
            }
            categories.Add(category);

            categories.Reverse();

            return _mapper.Map<IEnumerable<CatalogItemResponse>>(categories);
        }

        public async Task<IEnumerable<CategoryForSelectResponse>> GetForSelectAsync()
        {
            var spec = new CategoryIncludeFullInfoSpecification();
            var categories = await _categoryRepository.ListAsync(spec);
            return _mapper.Map<IEnumerable<CategoryForSelectResponse>>(categories);
        }

        public async Task<IEnumerable<CategoryForSelectResponse>> GetCategoriesWithoutChildrenAsync()
        {
            var spec = new CategoryGetWithoutChildrenSpecification();
            var categories = await _categoryRepository.ListAsync(spec);
            return _mapper.Map<IEnumerable<CategoryForSelectResponse>>(categories);
        }

        public async Task<CategoryFullInfoResponse> GetByIdAsync(int id)
        {
            var spec = new CategoryIncludeFullInfoSpecification(id);
            var category = await _categoryRepository.GetBySpecAsync(spec);
            category.CategoryNullChecking();

            return _mapper.Map<CategoryFullInfoResponse>(category);
        }

        public async Task CreateAsync(CategoryRequest request)
        {
            if (request.ParentId != null)
            {
                var parentCategory = await _categoryRepository.GetByIdAsync(request.ParentId);
                parentCategory.CategoryNullChecking();
            }
            var specName = new CategoryGetByNameSpecification(request.EnglishName, LanguageId.English);
            var categoryEnNameExist = await _categoryRepository.GetBySpecAsync(specName);
            if (categoryEnNameExist != null)
                categoryEnNameExist.CategoryWithEnglishNameChecking(nameof(CountryRequest.EnglishName));

            specName = new CategoryGetByNameSpecification(request.UkrainianName, LanguageId.Ukrainian);
            var categoryUkNameExist = await _categoryRepository.GetBySpecAsync(specName);
            if (categoryUkNameExist != null)
                categoryUkNameExist.CategoryWithUkrainianNameChecking(nameof(CountryRequest.UkrainianName));

            var urlSlugSpec = new CategoryGetByUrlSlugSpecification(request.UrlSlug);
            var categoryUrlSlugExist = await _categoryRepository.GetBySpecAsync(urlSlugSpec);
            if (categoryUrlSlugExist != null)
                categoryUrlSlugExist.CategoryUrlSlugChecking(nameof(CategoryRequest.UrlSlug));

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

            if (!string.IsNullOrEmpty(request.LightIcon))
            {
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), request.LightIcon.Replace(ImagePath.RequestCategoriesImagePath, ImagePath.CategoriesImagePath));

                if (!File.Exists(filePath))
                {
                    var img = ImageWorker.FromBase64StringToImage(request.LightIcon);
                    string randomFilename = Guid.NewGuid() + ".png";
                    var dir = Path.Combine(Directory.GetCurrentDirectory(), ImagePath.CategoriesImagePath, randomFilename);
                    img.Save(dir, ImageFormat.Png);

                    category.LightIcon = randomFilename;
                }
            }

            if (!string.IsNullOrEmpty(request.DarkIcon))
            {
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), request.DarkIcon.Replace(ImagePath.RequestCategoriesImagePath, ImagePath.CategoriesImagePath));

                if (!File.Exists(filePath))
                {
                    var img = ImageWorker.FromBase64StringToImage(request.DarkIcon);
                    string randomFilename = Guid.NewGuid() + ".png";
                    var dir = Path.Combine(Directory.GetCurrentDirectory(), ImagePath.CategoriesImagePath, randomFilename);
                    img.Save(dir, ImageFormat.Png);

                    category.DarkIcon = randomFilename;
                }
            }

            if (!string.IsNullOrEmpty(request.ActiveIcon))
            {
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), request.ActiveIcon.Replace(ImagePath.RequestCategoriesImagePath, ImagePath.CategoriesImagePath));

                if (!File.Exists(filePath))
                {
                    var img = ImageWorker.FromBase64StringToImage(request.ActiveIcon);
                    string randomFilename = Guid.NewGuid() + ".png";
                    var dir = Path.Combine(Directory.GetCurrentDirectory(), ImagePath.CategoriesImagePath, randomFilename);
                    img.Save(dir, ImageFormat.Png);

                    category.ActiveIcon = randomFilename;
                }
            }

            await _categoryRepository.AddAsync(category);
            await _categoryRepository.SaveChangesAsync();
        }

        public async Task UpdateAsync(int id, CategoryRequest request)
        {
            var spec = new CategoryIncludeFullInfoSpecification(id);
            var category = await _categoryRepository.GetBySpecAsync(spec);
            category.CategoryNullChecking();

            var specName = new CategoryGetByNameSpecification(request.EnglishName, LanguageId.English);
            var categoryEnNameExist = await _categoryRepository.GetBySpecAsync(specName);
            if (categoryEnNameExist != null && categoryEnNameExist.Id != category.Id)
                categoryEnNameExist.CategoryWithEnglishNameChecking(nameof(CountryRequest.EnglishName));

            specName = new CategoryGetByNameSpecification(request.UkrainianName, LanguageId.Ukrainian);
            var categoryUkNameExist = await _categoryRepository.GetBySpecAsync(specName);
            if (categoryUkNameExist != null && categoryUkNameExist.Id != category.Id)
                categoryUkNameExist.CategoryWithUkrainianNameChecking(nameof(CountryRequest.UkrainianName));

            var urlSlugSpec = new CategoryGetByUrlSlugSpecification(request.UrlSlug);
            var categoryUrlSlugExist = await _categoryRepository.GetBySpecAsync(urlSlugSpec);
            if (categoryUrlSlugExist != null && categoryUrlSlugExist.Id != id)
                categoryUrlSlugExist.CategoryUrlSlugChecking(nameof(CategoryRequest.UrlSlug));

            if (request.ParentId != null)
            {
                var parentCategory = await _categoryRepository.GetByIdAsync(request.ParentId);
                parentCategory.CategoryNullChecking();
            }

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
            if (!string.IsNullOrEmpty(request.LightIcon))
            {
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), request.LightIcon.Replace(ImagePath.RequestCategoriesImagePath, ImagePath.CategoriesImagePath));
                if (!File.Exists(filePath))
                {
                    if (category.LightIcon != null)
                    {
                        filePath = Path.Combine(Directory.GetCurrentDirectory(), ImagePath.CategoriesImagePath, category.LightIcon);

                        if (File.Exists(filePath))
                        {
                            File.Delete(filePath);
                        }
                    }
                    var img = ImageWorker.FromBase64StringToImage(request.LightIcon);
                    string randomFilename = Guid.NewGuid() + ".png";
                    var dir = Path.Combine(Directory.GetCurrentDirectory(), ImagePath.CategoriesImagePath, randomFilename);
                    img.Save(dir, ImageFormat.Png);

                    category.LightIcon = randomFilename;
                }
            }

            if (!string.IsNullOrEmpty(request.DarkIcon))
            {
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), request.DarkIcon.Replace(ImagePath.RequestCategoriesImagePath, ImagePath.CategoriesImagePath));
                if (!File.Exists(filePath))
                {
                    if (category.DarkIcon != null)
                    {
                        filePath = Path.Combine(Directory.GetCurrentDirectory(), ImagePath.CategoriesImagePath, category.DarkIcon);

                        if (File.Exists(filePath))
                        {
                            File.Delete(filePath);
                        }
                    }
                    var img = ImageWorker.FromBase64StringToImage(request.DarkIcon);
                    string randomFilename = Guid.NewGuid() + ".png";
                    var dir = Path.Combine(Directory.GetCurrentDirectory(), ImagePath.CategoriesImagePath, randomFilename);
                    img.Save(dir, ImageFormat.Png);

                    category.DarkIcon = randomFilename;
                }
            }
            if (!string.IsNullOrEmpty(request.ActiveIcon))
            {
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), request.ActiveIcon.Replace(ImagePath.RequestCategoriesImagePath, ImagePath.CategoriesImagePath));
                if (!File.Exists(filePath))
                {
                    if (category.ActiveIcon != null)
                    {
                        filePath = Path.Combine(Directory.GetCurrentDirectory(), ImagePath.CategoriesImagePath, category.ActiveIcon);

                        if (File.Exists(filePath))
                        {
                            File.Delete(filePath);
                        }
                    }
                    var img = ImageWorker.FromBase64StringToImage(request.ActiveIcon);
                    string randomFilename = Guid.NewGuid() + ".png";
                    var dir = Path.Combine(Directory.GetCurrentDirectory(), ImagePath.CategoriesImagePath, randomFilename);
                    img.Save(dir, ImageFormat.Png);

                    category.ActiveIcon = randomFilename;
                }
            }

            category.CategoryTranslations.Clear();

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
            if (!string.IsNullOrEmpty(category.LightIcon))
            {
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), ImagePath.CategoriesImagePath, category.LightIcon);

                if (File.Exists(filePath))
                {
                    File.Delete(filePath);
                }
            }
            if (!string.IsNullOrEmpty(category.DarkIcon))
            {
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), ImagePath.CategoriesImagePath, category.DarkIcon);

                if (File.Exists(filePath))
                {
                    File.Delete(filePath);
                }
            }
            if (!string.IsNullOrEmpty(category.ActiveIcon))
            {
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), ImagePath.CategoriesImagePath, category.ActiveIcon);

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
                if (!string.IsNullOrEmpty(category.LightIcon))
                {
                    var filePath = Path.Combine(Directory.GetCurrentDirectory(), ImagePath.CategoriesImagePath, category.LightIcon);

                    if (File.Exists(filePath))
                    {
                        File.Delete(filePath);
                    }
                }
                if (!string.IsNullOrEmpty(category.DarkIcon))
                {
                    var filePath = Path.Combine(Directory.GetCurrentDirectory(), ImagePath.CategoriesImagePath, category.DarkIcon);

                    if (File.Exists(filePath))
                    {
                        File.Delete(filePath);
                    }
                }
                if (!string.IsNullOrEmpty(category.ActiveIcon))
                {
                    var filePath = Path.Combine(Directory.GetCurrentDirectory(), ImagePath.CategoriesImagePath, category.ActiveIcon);

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
