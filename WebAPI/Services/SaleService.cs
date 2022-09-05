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
    public class SaleService : ISaleService
    {
        private readonly IRepository<Sale> _saleRepository;
        private readonly IRepository<Category> _categoryRepository;
        private readonly IMapper _mapper;

        public SaleService(
            IRepository<Sale> saleRepository,
            IMapper mapper,
            IRepository<Category> categoryRepository)
        {
            _saleRepository = saleRepository;
            _mapper = mapper;
            _categoryRepository = categoryRepository;
        }

        public async Task<IEnumerable<SaleResponse>> GetSalesAsync()
        {
            var spec = new SaleIncludeFullInfoSpecification();
            var sales = await _saleRepository.ListAsync(spec);

            return _mapper.Map<IEnumerable<SaleResponse>>(sales);

        }
        public async Task<SaleResponse> GetSaleByIdAsync(int saleId)
        {
            var spec = new SaleIncludeFullInfoSpecification(saleId);
            var sale = await _saleRepository.GetBySpecAsync(spec);
            sale.SaleNullChecking();

            return _mapper.Map<SaleResponse>(sale);
        }

        public async Task CreateAsync(SaleRequest request)
        {
            var sale = _mapper.Map<Sale>(request);

            if (!string.IsNullOrEmpty(request.HorizontalImage))
            {
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), request.HorizontalImage.Replace(ImagePath.RequestSalesImagePath, ImagePath.SalesImagePath));

                if (!File.Exists(filePath))
                {
                    var img = ImageWorker.FromBase64StringToImage(request.HorizontalImage);
                    string randomFilename = Guid.NewGuid() + ".png";
                    var dir = Path.Combine(Directory.GetCurrentDirectory(), ImagePath.SalesImagePath, randomFilename);
                    img.Save(dir, ImageFormat.Png);

                    sale.HorizontalImage = randomFilename;
                }
            }

            if (!string.IsNullOrEmpty(request.VerticalImage))
            {
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), request.VerticalImage.Replace(ImagePath.RequestSalesImagePath, ImagePath.SalesImagePath));

                if (!File.Exists(filePath))
                {
                    var img = ImageWorker.FromBase64StringToImage(request.VerticalImage);
                    string randomFilename = Guid.NewGuid() + ".png";
                    var dir = Path.Combine(Directory.GetCurrentDirectory(), ImagePath.SalesImagePath, randomFilename);
                    img.Save(dir, ImageFormat.Png);

                    sale.VerticalImage = randomFilename;
                }
            }

            var categories = new List<Category>();
            foreach (var item in request.Categories)
            {
                var category = await _categoryRepository.GetByIdAsync(item);
                if (category != null)
                    categories.Add(category);
            }
            sale.Categories = categories;
            await _saleRepository.AddAsync(sale);
            await _saleRepository.SaveChangesAsync();

        }

        public async Task UpdateAsync(int id, SaleRequest request)
        {
            var spec = new SaleIncludeFullInfoSpecification(id);
            var sale = await _saleRepository.GetBySpecAsync(spec);
            sale.SaleNullChecking();

            _mapper.Map(request, sale);

            if (!string.IsNullOrEmpty(request.HorizontalImage))
            {
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), request.HorizontalImage.Replace(ImagePath.RequestSalesImagePath, ImagePath.SalesImagePath));

                if (!File.Exists(filePath))
                {
                    var img = ImageWorker.FromBase64StringToImage(request.HorizontalImage);
                    string randomFilename = Guid.NewGuid() + ".png";
                    var dir = Path.Combine(Directory.GetCurrentDirectory(), ImagePath.SalesImagePath, randomFilename);
                    img.Save(dir, ImageFormat.Png);

                    sale.HorizontalImage = randomFilename;
                }
            }

            if (!string.IsNullOrEmpty(request.VerticalImage))
            {
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), request.VerticalImage.Replace(ImagePath.RequestSalesImagePath, ImagePath.SalesImagePath));

                if (!File.Exists(filePath))
                {
                    var img = ImageWorker.FromBase64StringToImage(request.VerticalImage);
                    string randomFilename = Guid.NewGuid() + ".png";
                    var dir = Path.Combine(Directory.GetCurrentDirectory(), ImagePath.SalesImagePath, randomFilename);
                    img.Save(dir, ImageFormat.Png);

                    sale.VerticalImage = randomFilename;
                }
            }

            sale.Categories.Clear();
            foreach (var item in request.Categories)
            {
                var category = await _categoryRepository.GetByIdAsync(item);
                if (category != null)
                    sale.Categories.Add(category);
            }

            await _saleRepository.UpdateAsync(sale);

            await _saleRepository.SaveChangesAsync();

        }
        public async Task DeleteSaleAsync(int saleId)
        {
            var sale = await _saleRepository.GetByIdAsync(saleId);
            sale.SaleNullChecking();

            if (!string.IsNullOrEmpty(sale.VerticalImage))
            {
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), ImagePath.SalesImagePath, sale.VerticalImage);

                if (File.Exists(filePath))
                {
                    File.Delete(filePath);
                }
            }

            if (!string.IsNullOrEmpty(sale.HorizontalImage))
            {
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), ImagePath.SalesImagePath, sale.HorizontalImage);

                if (File.Exists(filePath))
                {
                    File.Delete(filePath);
                }
            }

            await _saleRepository.DeleteAsync(sale);
            await _saleRepository.SaveChangesAsync();

        }

        public async Task DeleteSalesAsync(IEnumerable<int> ids)
        {
            foreach (var item in ids)
            {
                var sale = await _saleRepository.GetByIdAsync(item);

                if (!string.IsNullOrEmpty(sale.HorizontalImage))
                {
                    var filePath = Path.Combine(Directory.GetCurrentDirectory(), ImagePath.SalesImagePath, sale.HorizontalImage);

                    if (File.Exists(filePath))
                    {
                        File.Delete(filePath);
                    }
                }
                if (!string.IsNullOrEmpty(sale.VerticalImage))
                {
                    var filePath = Path.Combine(Directory.GetCurrentDirectory(), ImagePath.SalesImagePath, sale.VerticalImage);

                    if (File.Exists(filePath))
                    {
                        File.Delete(filePath);
                    }
                }

                await _saleRepository.DeleteAsync(sale);
            }
            await _saleRepository.SaveChangesAsync();
        }
    }
}

