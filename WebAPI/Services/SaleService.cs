using AutoMapper;
using DAL;
using DAL.Entities;
using Microsoft.AspNetCore.Identity;
using WebAPI.Extensions;
using WebAPI.Interfaces;
using WebAPI.Specifications;
using WebAPI.ViewModels.Request;
using WebAPI.ViewModels.Response;

namespace WebAPI.Services
{
    public class SaleService : ISaleService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IRepository<Sale> _saleRepository;
        private readonly IRepository<Category> _categoryRepository;
        private readonly IMapper _mapper;

        public SaleService(
            UserManager<AppUser> userManager,
            IRepository<Sale> saleRepository,
            IMapper mapper
            )
        {
            _userManager = userManager;
            _saleRepository = saleRepository; 
            _mapper = mapper;
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

        public async Task CreateAsync(SaleRequest request, string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            user.UserNullChecking();

            var sale = _mapper.Map<Sale>(request);
 
            var categories = new List<Category>();
            foreach (var item in request.Categories)
            {
               var category = await _categoryRepository.GetByIdAsync(item);
                
               categories.Add(category);
            }
            sale.Categories = categories;
            await _saleRepository.AddAsync(sale);
            await _saleRepository.SaveChangesAsync();
            
        }

        public async Task DeleteSaleAsync(int saleId)
        {
            var sale = await _saleRepository.GetByIdAsync(saleId);
            sale.SaleNullChecking();

            await _saleRepository.DeleteAsync(sale);
            await _saleRepository.SaveChangesAsync();
            
        }

       
    }
}
