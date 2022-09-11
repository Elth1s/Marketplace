using AutoMapper;
using DAL;
using DAL.Constants;
using DAL.Entities;
using System.Drawing.Imaging;
using WebAPI.Constants;
using WebAPI.Extensions;
using WebAPI.Helpers;
using WebAPI.Interfaces;
using WebAPI.Specifications.Sales;
using WebAPI.ViewModels.Request;
using WebAPI.ViewModels.Response;
using WebAPI.ViewModels.Response.Sales;

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
        public async Task<SaleFullInfoResponse> GetSaleByIdAsync(int saleId)
        {
            var spec = new SaleIncludeFullInfoSpecification(saleId);
            var sale = await _saleRepository.GetBySpecAsync(spec);
            sale.SaleNullChecking();

            return _mapper.Map<SaleFullInfoResponse>(sale);
        }

        public async Task<SearchResponse<SaleResponse>> SearchSalesAsync(AdminSearchRequest request)
        {
            var spec = new SaleSearchSpecification(request.Name, request.IsAscOrder, request.OrderBy);
            var count = await _saleRepository.CountAsync(spec);
            spec = new SaleSearchSpecification(
                request.Name,
                request.IsAscOrder,
                request.OrderBy,
                (request.Page - 1) * request.RowsPerPage,
                request.RowsPerPage);
            var sales = await _saleRepository.ListAsync(spec);
            var mappedSales = _mapper.Map<IEnumerable<SaleResponse>>(sales);
            var response = new SearchResponse<SaleResponse>() { Count = count, Values = mappedSales };

            return response;
        }

        public async Task CreateAsync(SaleRequest request)
        {
            var sale = _mapper.Map<Sale>(request);
            var engTranslation = new SaleTranslation() { LanguageId = LanguageId.English };
            if (!string.IsNullOrEmpty(request.EnglishHorizontalImage))
            {
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), request.EnglishHorizontalImage.Replace(ImagePath.RequestSalesImagePath, ImagePath.SalesImagePath));

                if (!File.Exists(filePath))
                {
                    var img = ImageWorker.FromBase64StringToImage(request.EnglishHorizontalImage);
                    string randomFilename = Guid.NewGuid() + ".png";
                    var dir = Path.Combine(Directory.GetCurrentDirectory(), ImagePath.SalesImagePath, randomFilename);
                    img.Save(dir, ImageFormat.Png);

                    engTranslation.HorizontalImage = randomFilename;
                }
            }

            if (!string.IsNullOrEmpty(request.EnglishVerticalImage))
            {
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), request.EnglishVerticalImage.Replace(ImagePath.RequestSalesImagePath, ImagePath.SalesImagePath));

                if (!File.Exists(filePath))
                {
                    var img = ImageWorker.FromBase64StringToImage(request.EnglishVerticalImage);
                    string randomFilename = Guid.NewGuid() + ".png";
                    var dir = Path.Combine(Directory.GetCurrentDirectory(), ImagePath.SalesImagePath, randomFilename);
                    img.Save(dir, ImageFormat.Png);

                    engTranslation.VerticalImage = randomFilename;
                }
            }
            var ukTranslation = new SaleTranslation() { LanguageId = LanguageId.Ukrainian };
            if (!string.IsNullOrEmpty(request.UkrainianHorizontalImage))
            {
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), request.UkrainianHorizontalImage.Replace(ImagePath.RequestSalesImagePath, ImagePath.SalesImagePath));

                if (!File.Exists(filePath))
                {
                    var img = ImageWorker.FromBase64StringToImage(request.UkrainianHorizontalImage);
                    string randomFilename = Guid.NewGuid() + ".png";
                    var dir = Path.Combine(Directory.GetCurrentDirectory(), ImagePath.SalesImagePath, randomFilename);
                    img.Save(dir, ImageFormat.Png);

                    ukTranslation.HorizontalImage = randomFilename;
                }
            }

            if (!string.IsNullOrEmpty(request.UkrainianVerticalImage))
            {
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), request.UkrainianVerticalImage.Replace(ImagePath.RequestSalesImagePath, ImagePath.SalesImagePath));

                if (!File.Exists(filePath))
                {
                    var img = ImageWorker.FromBase64StringToImage(request.UkrainianVerticalImage);
                    string randomFilename = Guid.NewGuid() + ".png";
                    var dir = Path.Combine(Directory.GetCurrentDirectory(), ImagePath.SalesImagePath, randomFilename);
                    img.Save(dir, ImageFormat.Png);

                    ukTranslation.VerticalImage = randomFilename;
                }
            }
            sale.SaleTranslations = new List<SaleTranslation>() { engTranslation, ukTranslation };

            var categories = new List<Category>();
            if (request.Categories != null)
            {
                foreach (var item in request.Categories)
                {
                    var category = await _categoryRepository.GetByIdAsync(item);
                    if (category != null)
                        categories.Add(category);
                }
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

            var engTranslation = sale.SaleTranslations.FirstOrDefault(s => s.LanguageId == LanguageId.English);
            if (!string.IsNullOrEmpty(request.EnglishHorizontalImage))
            {
                var filePath = Path.Combine(Directory.GetCurrentDirectory(),
                    request.EnglishHorizontalImage.Replace(ImagePath.RequestSalesImagePath,
                    ImagePath.SalesImagePath));
                if (!File.Exists(filePath))
                {
                    if (engTranslation.HorizontalImage != null)
                    {
                        filePath = Path.Combine(Directory.GetCurrentDirectory(), ImagePath.SalesImagePath, engTranslation.HorizontalImage);

                        if (File.Exists(filePath))
                        {
                            File.Delete(filePath);
                        }
                    }
                    var img = ImageWorker.FromBase64StringToImage(request.EnglishHorizontalImage);
                    string randomFilename = Guid.NewGuid() + ".png";
                    var dir = Path.Combine(Directory.GetCurrentDirectory(), ImagePath.SalesImagePath, randomFilename);
                    img.Save(dir, ImageFormat.Png);

                    engTranslation.HorizontalImage = randomFilename;
                }
            }

            if (!string.IsNullOrEmpty(request.EnglishVerticalImage))
            {
                var filePath = Path.Combine(Directory.GetCurrentDirectory(),
                    request.EnglishVerticalImage.Replace(ImagePath.RequestSalesImagePath,
                    ImagePath.SalesImagePath));
                if (!File.Exists(filePath))
                {
                    if (engTranslation.VerticalImage != null)
                    {
                        filePath = Path.Combine(Directory.GetCurrentDirectory(), ImagePath.SalesImagePath, engTranslation.VerticalImage);

                        if (File.Exists(filePath))
                        {
                            File.Delete(filePath);
                        }
                    }
                    var img = ImageWorker.FromBase64StringToImage(request.EnglishVerticalImage);
                    string randomFilename = Guid.NewGuid() + ".png";
                    var dir = Path.Combine(Directory.GetCurrentDirectory(), ImagePath.SalesImagePath, randomFilename);
                    img.Save(dir, ImageFormat.Png);

                    engTranslation.VerticalImage = randomFilename;
                }
            }

            var ukTranslation = sale.SaleTranslations.FirstOrDefault(s => s.LanguageId == LanguageId.Ukrainian);
            if (!string.IsNullOrEmpty(request.UkrainianHorizontalImage))
            {
                var filePath = Path.Combine(Directory.GetCurrentDirectory(),
                    request.UkrainianHorizontalImage.Replace(ImagePath.RequestSalesImagePath,
                    ImagePath.SalesImagePath));
                if (!File.Exists(filePath))
                {
                    if (ukTranslation.HorizontalImage != null)
                    {
                        filePath = Path.Combine(Directory.GetCurrentDirectory(), ImagePath.SalesImagePath, ukTranslation.HorizontalImage);

                        if (File.Exists(filePath))
                        {
                            File.Delete(filePath);
                        }
                    }
                    var img = ImageWorker.FromBase64StringToImage(request.UkrainianHorizontalImage);
                    string randomFilename = Guid.NewGuid() + ".png";
                    var dir = Path.Combine(Directory.GetCurrentDirectory(), ImagePath.SalesImagePath, randomFilename);
                    img.Save(dir, ImageFormat.Png);

                    ukTranslation.HorizontalImage = randomFilename;
                }
            }

            if (!string.IsNullOrEmpty(request.UkrainianVerticalImage))
            {
                var filePath = Path.Combine(Directory.GetCurrentDirectory(),
                    request.UkrainianVerticalImage.Replace(ImagePath.RequestSalesImagePath,
                    ImagePath.SalesImagePath));
                if (!File.Exists(filePath))
                {
                    if (ukTranslation.VerticalImage != null)
                    {
                        filePath = Path.Combine(Directory.GetCurrentDirectory(), ImagePath.SalesImagePath, ukTranslation.VerticalImage);

                        if (File.Exists(filePath))
                        {
                            File.Delete(filePath);
                        }
                    }
                    var img = ImageWorker.FromBase64StringToImage(request.UkrainianVerticalImage);
                    string randomFilename = Guid.NewGuid() + ".png";
                    var dir = Path.Combine(Directory.GetCurrentDirectory(), ImagePath.SalesImagePath, randomFilename);
                    img.Save(dir, ImageFormat.Png);

                    ukTranslation.VerticalImage = randomFilename;
                }
            }

            sale.SaleTranslations.Clear();
            sale.SaleTranslations = new List<SaleTranslation>() { engTranslation, ukTranslation };

            sale.Categories.Clear();
            if (request.Categories != null)
            {
                foreach (var item in request.Categories)
                {
                    var category = await _categoryRepository.GetByIdAsync(item);
                    if (category != null)
                        sale.Categories.Add(category);
                }
            }
            await _saleRepository.UpdateAsync(sale);

            await _saleRepository.SaveChangesAsync();

        }
        public async Task DeleteSaleAsync(int saleId)
        {
            var spec = new SaleIncludeFullInfoSpecification(saleId);
            var sale = await _saleRepository.GetBySpecAsync(spec);
            sale.SaleNullChecking();

            var engTranslation = sale.SaleTranslations.FirstOrDefault(s => s.LanguageId == LanguageId.English);
            var ukTranslation = sale.SaleTranslations.FirstOrDefault(s => s.LanguageId == LanguageId.Ukrainian);
            if (!string.IsNullOrEmpty(engTranslation.HorizontalImage))
            {
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), ImagePath.SalesImagePath, engTranslation.HorizontalImage);

                if (File.Exists(filePath))
                {
                    File.Delete(filePath);
                }
            }
            if (!string.IsNullOrEmpty(engTranslation.VerticalImage))
            {
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), ImagePath.SalesImagePath, engTranslation.VerticalImage);

                if (File.Exists(filePath))
                {
                    File.Delete(filePath);
                }
            }
            if (!string.IsNullOrEmpty(ukTranslation.HorizontalImage))
            {
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), ImagePath.SalesImagePath, ukTranslation.HorizontalImage);

                if (File.Exists(filePath))
                {
                    File.Delete(filePath);
                }
            }
            if (!string.IsNullOrEmpty(ukTranslation.VerticalImage))
            {
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), ImagePath.SalesImagePath, ukTranslation.VerticalImage);

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
                var spec = new SaleIncludeFullInfoSpecification(item);
                var sale = await _saleRepository.GetBySpecAsync(spec);
                var engTranslation = sale.SaleTranslations.FirstOrDefault(s => s.LanguageId == LanguageId.English);
                var ukTranslation = sale.SaleTranslations.FirstOrDefault(s => s.LanguageId == LanguageId.Ukrainian);
                if (!string.IsNullOrEmpty(engTranslation.HorizontalImage))
                {
                    var filePath = Path.Combine(Directory.GetCurrentDirectory(), ImagePath.SalesImagePath, engTranslation.HorizontalImage);

                    if (File.Exists(filePath))
                    {
                        File.Delete(filePath);
                    }
                }
                if (!string.IsNullOrEmpty(engTranslation.VerticalImage))
                {
                    var filePath = Path.Combine(Directory.GetCurrentDirectory(), ImagePath.SalesImagePath, engTranslation.VerticalImage);

                    if (File.Exists(filePath))
                    {
                        File.Delete(filePath);
                    }
                }
                if (!string.IsNullOrEmpty(ukTranslation.HorizontalImage))
                {
                    var filePath = Path.Combine(Directory.GetCurrentDirectory(), ImagePath.SalesImagePath, ukTranslation.HorizontalImage);

                    if (File.Exists(filePath))
                    {
                        File.Delete(filePath);
                    }
                }
                if (!string.IsNullOrEmpty(ukTranslation.VerticalImage))
                {
                    var filePath = Path.Combine(Directory.GetCurrentDirectory(), ImagePath.SalesImagePath, ukTranslation.VerticalImage);

                    if (File.Exists(filePath))
                    {
                        File.Delete(filePath);
                    }
                }

                await _saleRepository.DeleteAsync(sale);
            }
            await _saleRepository.SaveChangesAsync();
        }

        public async Task<SaleResponse> GetSaleAsync(int saleId)
        {
            var spec = new SaleIncludeFullInfoSpecification(saleId);
            var sale = await _saleRepository.GetBySpecAsync(spec);
            sale.SaleNullChecking();

            return _mapper.Map<SaleResponse>(sale);
        }
    }
}

