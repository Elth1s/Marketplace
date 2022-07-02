using AutoMapper;
using DAL;
using DAL.Constants;
using DAL.Entities;
using DAL.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using System.Drawing.Imaging;
using WebAPI.Constants;
using WebAPI.Exceptions;
using WebAPI.Extensions;
using WebAPI.Helpers;
using WebAPI.Interfaces;
using WebAPI.Resources;
using WebAPI.Specifications.Shops;
using WebAPI.ViewModels.Request;
using WebAPI.ViewModels.Response;

namespace WebAPI.Services
{
    public class ShopService : IShopService
    {
        private readonly IRepository<Shop> _shopRepository;
        private readonly UserManager<AppUser> _userManager;
        private readonly IMapper _mapper;

        public ShopService(
            IMapper mapper,
            IRepository<Shop> shopRepository,
            UserManager<AppUser> userManager
            )
        {
            _shopRepository = shopRepository;
            _userManager = userManager;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ShopResponse>> GetShopsAsync()
        {
            var spec = new ShopIncludeCityWithCountrySpecification();
            var shops = await _shopRepository.ListAsync(spec);

            var response = _mapper.Map<IEnumerable<ShopResponse>>(shops);
            return response;
        }
        public async Task<ShopResponse> GetShopByIdAsync(int shopId)
        {
            var spec = new ShopIncludeCityWithCountrySpecification(shopId);
            var shop = await _shopRepository.GetBySpecAsync(spec);
            shop.ShopNullChecking();

            var response = _mapper.Map<ShopResponse>(shop);
            return response;
        }

        public async Task CreateShopAsync(ShopRequest request, string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            user.UserNullChecking();

            if (_userManager.GetRolesAsync(user) == null)
            {
                var resultRole = await _userManager.AddToRoleAsync(user, Roles.Seller);
                if (!resultRole.Succeeded)
                {
                    throw new AppException(ErrorMessages.UserAddRoleFail);
                }
            }

            var shop = _mapper.Map<Shop>(request);
            shop.UserId = userId;

            if (!string.IsNullOrEmpty(request.Photo))
            {
                var img = ImageWorker.FromBase64StringToImage(request.Photo);
                string randomFilename = Path.GetRandomFileName() + ".jpg";
                var dir = Path.Combine(Directory.GetCurrentDirectory(), ImagePath.ShopsImagePath, randomFilename);
                img.Save(dir, ImageFormat.Jpeg);

                shop.Photo = randomFilename;
            }

            await _shopRepository.AddAsync(shop);
            await _shopRepository.SaveChangesAsync();


        }

        public async Task UpdateShopAsync(int shopId, ShopRequest request, string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            user.UserNullChecking();

            var shop = await _shopRepository.GetByIdAsync(shopId);
            shop.ShopNullChecking();

            if (!string.IsNullOrEmpty(request.Photo))
            {
                var filePath = Path.Combine(
                    Directory.GetCurrentDirectory(),
                    request.Photo.Replace(ImagePath.RequestShopsImagePath,
                    ImagePath.ShopsImagePath));
                if (!File.Exists(filePath))
                {
                    if (!string.IsNullOrEmpty(shop.Photo))
                    {
                        filePath = Path.Combine(
                           Directory.GetCurrentDirectory(),
                           ImagePath.ShopsImagePath,
                           shop.Photo);

                        if (File.Exists(filePath))
                            File.Delete(filePath);
                    }
                    var img = ImageWorker.FromBase64StringToImage(request.Photo);
                    string randomFilename = Path.GetRandomFileName() + ".jpg";
                    var dir = Path.Combine(Directory.GetCurrentDirectory(), ImagePath.ShopsImagePath, randomFilename);
                    img.Save(dir, ImageFormat.Jpeg);

                    shop.Photo = randomFilename;
                }
            }

            _mapper.Map(request, shop);

            await _shopRepository.UpdateAsync(shop);
            await _shopRepository.SaveChangesAsync();
        }

        public async Task DeleteShopAsync(int shopId)
        {
            var shop = await _shopRepository.GetByIdAsync(shopId);
            shop.ShopNullChecking();

            if (!string.IsNullOrEmpty(shop.Photo))
            {
                var filePath = Path.Combine(
                           Directory.GetCurrentDirectory(),
                           ImagePath.ShopsImagePath,
                           shop.Photo);

                if (File.Exists(filePath))
                    File.Delete(filePath);
            }

            await _shopRepository.DeleteAsync(shop);
            await _shopRepository.SaveChangesAsync();
        }
    }
}
