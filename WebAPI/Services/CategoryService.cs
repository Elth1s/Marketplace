using AutoMapper;
using DAL;
using DAL.Entities;
using System.Drawing.Imaging;
using WebAPI.Constants;
using WebAPI.Extensions;
using WebAPI.Helpers;
using WebAPI.Interfaces;
using WebAPI.Specifications;
using WebAPI.ViewModels.Request;
using WebAPI.ViewModels.Response;

namespace WebAPI.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly IRepository<Category> _categorRepository;
        private readonly IRepository<CharacteristicValue> _characteristicRepository;
        private readonly IMapper _mapper;

        public CategoryService(IRepository<Category> categorRepository, IRepository<CharacteristicValue> characteristicRepository, IMapper mapper)
        {
            _categorRepository = categorRepository;
            _characteristicRepository = characteristicRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<CategoryResponse>> GetAsync()
        {
            var spec = new CategoryIncludeFullInfoSpecification();
            var categories = await _categorRepository.ListAsync(spec);
            return _mapper.Map<IEnumerable<CategoryResponse>>(categories);
        }

        public async Task<IEnumerable<CategoryForSelectResponse>> GetForSelectAsync()
        {
            var categories = await _categorRepository.ListAsync();
            return _mapper.Map<IEnumerable<CategoryForSelectResponse>>(categories);
        }

        public async Task<CategoryResponse> GetByIdAsync(int id)
        {
            var spec = new CategoryIncludeFullInfoSpecification(id);
            var category = await _categorRepository.GetBySpecAsync(spec);
            category.CategoryNullChecking();

            return _mapper.Map<CategoryResponse>(category);
        }

        public async Task CreateAsync(CategoryRequest request)
        {
            if (request.ParentId != null)
            {
                var parentCategory = await _categorRepository.GetByIdAsync(request.ParentId);
                parentCategory.CategoryNullChecking();
            }

            var category = _mapper.Map<Category>(request);

            if (!string.IsNullOrEmpty(request.Image))
            {
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), request.Image.Replace(ImagePath.RequestCategoriesImagePath, ImagePath.CategoriesImagePath));

                if (!File.Exists(filePath))
                {
                    var img = ImageWorker.FromBase64StringToImage(request.Image);
                    string randomFilename = Path.GetRandomFileName() + ".jpg";
                    var dir = Path.Combine(Directory.GetCurrentDirectory(), ImagePath.CategoriesImagePath, randomFilename);
                    img.Save(dir, ImageFormat.Jpeg);

                    category.Image = randomFilename;
                }
            }

            await _categorRepository.AddAsync(category);
            await _categorRepository.SaveChangesAsync();
        }

        public async Task UpdateAsync(int id, CategoryRequest request)
        {
            if (request.ParentId != null)
            {
                var parentCategory = await _categorRepository.GetByIdAsync(request.ParentId);
                parentCategory.CategoryNullChecking();
            }

            var category = await _categorRepository.GetByIdAsync(id);
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
                    string randomFilename = Path.GetRandomFileName() + ".jpg";
                    var dir = Path.Combine(Directory.GetCurrentDirectory(), ImagePath.CategoriesImagePath, randomFilename);
                    img.Save(dir, ImageFormat.Jpeg);

                    category.Image = randomFilename;
                }
            }

            _mapper.Map(request, category);

            await _categorRepository.UpdateAsync(category);
            await _categorRepository.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var category = await _categorRepository.GetByIdAsync(id);
            category.CategoryNullChecking();

            if (!string.IsNullOrEmpty(category.Image))
            {
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), ImagePath.CategoriesImagePath, category.Image);

                if (File.Exists(filePath))
                {
                    if (File.Exists(filePath))
                    {
                        File.Delete(filePath);
                    }
                }
            }

            await _categorRepository.DeleteAsync(category);
            await _categorRepository.SaveChangesAsync();
        }
    }
}
