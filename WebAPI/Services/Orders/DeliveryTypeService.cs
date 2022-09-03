using AutoMapper;
using DAL;
using DAL.Constants;
using DAL.Entities;
using System.Drawing.Imaging;
using WebAPI.Constants;
using WebAPI.Extensions;
using WebAPI.Helpers;
using WebAPI.Interfaces.Orders;
using WebAPI.Specifications.Orders;
using WebAPI.ViewModels.Request;
using WebAPI.ViewModels.Request.Orders;
using WebAPI.ViewModels.Response;
using WebAPI.ViewModels.Response.Orders;

namespace WebAPI.Services.Orders
{
    public class DeliveryTypeService : IDeliveryTypeService
    {
        private readonly IRepository<DeliveryType> _deliveryTypeRepository;
        private readonly IMapper _mapper;

        public DeliveryTypeService(IRepository<DeliveryType> deliveryTypeRepository, IMapper mapper)
        {
            _deliveryTypeRepository = deliveryTypeRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<DeliveryTypeResponse>> GetAsync()
        {
            var spec = new DeliveryTypeIncludeInfoSpecification();
            var orderStatus = await _deliveryTypeRepository.ListAsync(spec);

            return _mapper.Map<IEnumerable<DeliveryTypeResponse>>(orderStatus);
        }

        public async Task<DeliveryTypeFullInfoResponse> GetByIdAsync(int id)
        {
            var spec = new DeliveryTypeIncludeInfoSpecification(id);
            var type = await _deliveryTypeRepository.GetBySpecAsync(spec);
            type.DeliveryTypeNullChecking();

            return _mapper.Map<DeliveryTypeFullInfoResponse>(type);
        }

        public async Task<SearchResponse<DeliveryTypeResponse>> SearchDeliveryTypesAsync(AdminSearchRequest request)
        {
            var spec = new DeliveryTypeSearchSpecification(request.Name, request.IsAscOrder, request.OrderBy);
            var count = await _deliveryTypeRepository.CountAsync(spec);
            spec = new DeliveryTypeSearchSpecification(
                request.Name,
                request.IsAscOrder,
                request.OrderBy,
                (request.Page - 1) * request.RowsPerPage,
                request.RowsPerPage);
            var types = await _deliveryTypeRepository.ListAsync(spec);
            var mappedTypes = _mapper.Map<IEnumerable<DeliveryTypeResponse>>(types);
            var response = new SearchResponse<DeliveryTypeResponse>() { Count = count, Values = mappedTypes };

            return response;
        }

        public async Task CreateAsync(DeliveryTypeRequest request)
        {
            var specName = new DeliveryTypeGetByNameSpecification(request.EnglishName, LanguageId.English);
            var deliveryTypeEnNameExist = await _deliveryTypeRepository.GetBySpecAsync(specName);
            if (deliveryTypeEnNameExist != null)
                deliveryTypeEnNameExist.DeliveryTypeWithEnglishNameChecking(nameof(DeliveryTypeRequest.EnglishName));

            specName = new DeliveryTypeGetByNameSpecification(request.UkrainianName, LanguageId.Ukrainian);
            var deliveryTypeUkNameExist = await _deliveryTypeRepository.GetBySpecAsync(specName);
            if (deliveryTypeUkNameExist != null)
                deliveryTypeUkNameExist.DeliveryTypeWithUkrainianNameChecking(nameof(DeliveryTypeRequest.UkrainianName));

            var type = _mapper.Map<DeliveryType>(request);

            if (!string.IsNullOrEmpty(request.DarkIcon))
            {
                var filePath = Path.Combine(
                    Directory.GetCurrentDirectory(),
                    request.DarkIcon.Replace(ImagePath.RequestDeliveryTypesImagePath, ImagePath.DeliveryTypesImagePath));

                if (!File.Exists(filePath))
                {
                    var img = ImageWorker.FromBase64StringToImage(request.DarkIcon);
                    string randomFilename = Guid.NewGuid() + ".png";
                    var dir = Path.Combine(Directory.GetCurrentDirectory(), ImagePath.DeliveryTypesImagePath, randomFilename);
                    img.Save(dir, ImageFormat.Png);

                    type.DarkIcon = randomFilename;
                }
            }

            if (!string.IsNullOrEmpty(request.LightIcon))
            {
                var filePath = Path.Combine(
                    Directory.GetCurrentDirectory(),
                    request.LightIcon.Replace(ImagePath.RequestDeliveryTypesImagePath, ImagePath.DeliveryTypesImagePath));

                if (!File.Exists(filePath))
                {
                    var img = ImageWorker.FromBase64StringToImage(request.LightIcon);
                    string randomFilename = Guid.NewGuid() + ".png";
                    var dir = Path.Combine(Directory.GetCurrentDirectory(), ImagePath.DeliveryTypesImagePath, randomFilename);
                    img.Save(dir, ImageFormat.Png);

                    type.LightIcon = randomFilename;
                }
            }


            await _deliveryTypeRepository.AddAsync(type);
            await _deliveryTypeRepository.SaveChangesAsync();
        }

        public async Task UpdateAsync(int id, DeliveryTypeRequest request)
        {
            var spec = new DeliveryTypeIncludeInfoSpecification(id);
            var type = await _deliveryTypeRepository.GetBySpecAsync(spec);
            type.DeliveryTypeNullChecking();

            var specName = new DeliveryTypeGetByNameSpecification(request.EnglishName, LanguageId.English);
            var deliveryTypeEnNameExist = await _deliveryTypeRepository.GetBySpecAsync(specName);
            if (deliveryTypeEnNameExist != null && deliveryTypeEnNameExist.Id != type.Id)
                deliveryTypeEnNameExist.DeliveryTypeWithEnglishNameChecking(nameof(OrderStatusRequest.EnglishName));

            specName = new DeliveryTypeGetByNameSpecification(request.UkrainianName, LanguageId.Ukrainian);
            var deliveryTypeUkNameExist = await _deliveryTypeRepository.GetBySpecAsync(specName);
            if (deliveryTypeUkNameExist != null && deliveryTypeUkNameExist.Id != type.Id)
                deliveryTypeUkNameExist.DeliveryTypeWithUkrainianNameChecking(nameof(OrderStatusRequest.UkrainianName));



            if (!string.IsNullOrEmpty(request.DarkIcon))
            {
                var filePath = Path.Combine(
                    Directory.GetCurrentDirectory(),
                    request.DarkIcon.Replace(ImagePath.RequestDeliveryTypesImagePath, ImagePath.DeliveryTypesImagePath));
                if (!File.Exists(filePath))
                {
                    if (request.DarkIcon != null)
                    {
                        filePath = Path.Combine(Directory.GetCurrentDirectory(), ImagePath.CategoriesImagePath, request.DarkIcon);

                        if (File.Exists(filePath))
                        {
                            File.Delete(filePath);
                        }
                    }
                    var img = ImageWorker.FromBase64StringToImage(request.DarkIcon);
                    string randomFilename = Guid.NewGuid() + ".png";
                    var dir = Path.Combine(Directory.GetCurrentDirectory(), ImagePath.DeliveryTypesImagePath, randomFilename);
                    img.Save(dir, ImageFormat.Png);

                    request.DarkIcon = randomFilename;
                }
            }

            if (!string.IsNullOrEmpty(request.LightIcon))
            {
                var filePath = Path.Combine(
                    Directory.GetCurrentDirectory(),
                    request.LightIcon.Replace(ImagePath.RequestDeliveryTypesImagePath, ImagePath.DeliveryTypesImagePath));
                if (!File.Exists(filePath))
                {
                    if (request.LightIcon != null)
                    {
                        filePath = Path.Combine(Directory.GetCurrentDirectory(), ImagePath.CategoriesImagePath, request.LightIcon);

                        if (File.Exists(filePath))
                        {
                            File.Delete(filePath);
                        }
                    }
                    var img = ImageWorker.FromBase64StringToImage(request.LightIcon);
                    string randomFilename = Guid.NewGuid() + ".png";
                    var dir = Path.Combine(Directory.GetCurrentDirectory(), ImagePath.DeliveryTypesImagePath, randomFilename);
                    img.Save(dir, ImageFormat.Png);

                    request.LightIcon = randomFilename;
                }
            }

            type.DeliveryTypeTranslations.Clear();

            _mapper.Map(request, type);

            await _deliveryTypeRepository.UpdateAsync(type);
            await _deliveryTypeRepository.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var type = await _deliveryTypeRepository.GetByIdAsync(id);
            type.DeliveryTypeNullChecking();

            if (!string.IsNullOrEmpty(type.DarkIcon))
            {
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), ImagePath.DeliveryTypesImagePath, type.DarkIcon);

                if (File.Exists(filePath))
                {
                    File.Delete(filePath);
                }
            }

            if (!string.IsNullOrEmpty(type.LightIcon))
            {
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), ImagePath.DeliveryTypesImagePath, type.LightIcon);

                if (File.Exists(filePath))
                {
                    File.Delete(filePath);
                }
            }

            await _deliveryTypeRepository.DeleteAsync(type);
            await _deliveryTypeRepository.SaveChangesAsync();
        }

        public async Task DeleteDeliveryTypesAsync(IEnumerable<int> ids)
        {
            foreach (var item in ids)
            {
                var type = await _deliveryTypeRepository.GetByIdAsync(item);

                if (!string.IsNullOrEmpty(type.DarkIcon))
                {
                    var filePath = Path.Combine(Directory.GetCurrentDirectory(), ImagePath.DeliveryTypesImagePath, type.DarkIcon);

                    if (File.Exists(filePath))
                    {
                        File.Delete(filePath);
                    }
                }

                if (!string.IsNullOrEmpty(type.LightIcon))
                {
                    var filePath = Path.Combine(Directory.GetCurrentDirectory(), ImagePath.DeliveryTypesImagePath, type.LightIcon);

                    if (File.Exists(filePath))
                    {
                        File.Delete(filePath);
                    }
                }

                await _deliveryTypeRepository.DeleteAsync(type);
            }
            await _deliveryTypeRepository.SaveChangesAsync();
        }
    }
}
