using WebAPI.ViewModels.Request.Users;

namespace WebAPI.Interfaces.Users
{
    public interface IGenderService
    {
        Task<IEnumerable<GenderResponse>> GetGendersAsync();
    }
}
