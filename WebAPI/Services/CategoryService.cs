using Ardalis.Specification.EntityFrameworkCore;
using AutoMapper;
using DAL.Data;
using DAL.Entities;
using Microsoft.EntityFrameworkCore;
using System.Drawing.Imaging;
using WebAPI.Constants;
using WebAPI.Helpers;
using WebAPI.Interfaces;
using WebAPI.ViewModels.Request;
using WebAPI.ViewModels.Response;

namespace WebAPI.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly IMapper _mapper;
        private readonly MarketplaceDbContext _context;

        public CategoryService(IMapper mapper, MarketplaceDbContext context)
        {
            _mapper = mapper;
            _context = context;
        }

        public async Task<IEnumerable<CategoryResponse>> GetAsync()
        {
            var categories = await _context.Categories.ToListAsync();
            return _mapper.Map<IEnumerable<CategoryResponse>>(categories);
        }

        public async Task<IEnumerable<CategoryForSelectResponse>> GetForSelectAsync()
        {
            var categories = await _context.Categories.ToListAsync();
            return _mapper.Map<IEnumerable<CategoryForSelectResponse>>(categories);
        }

        public async Task<CategoryResponse> GetByIdAsync(int id)
        {
            var category = await _context.Categories.FirstOrDefaultAsync(u => u.Id == id);
            return _mapper.Map<CategoryResponse>(category);
        }

        public async Task CreateAsync(CategoryRequest request)
        {
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

            await _context.AddRangeAsync(category);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(int id, CategoryRequest request)
        {
            var category = await _context.Categories.FirstOrDefaultAsync(u => u.Id == id);

            if (category != null)
            {
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

                await _context.SaveChangesAsync();
            }
        }

        public async Task DeleteAsync(int id)
        {
            var category = await _context.Categories.FirstOrDefaultAsync(u => u.Id == id);

            if (category != null)
            {
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

                _context.Categories.Remove(category);
                await _context.SaveChangesAsync();
            }
        }
    }
}
