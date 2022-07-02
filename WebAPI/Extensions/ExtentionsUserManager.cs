using DAL.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace WebAPI.Extensions
{
    public static class ExtentionsUserManager
    {
        public static async Task<AppUser> FindByPhoneNumberAsync(this UserManager<AppUser> userManager, string PhoneNumber, CancellationToken cancellationToken = default(CancellationToken))
        {
            cancellationToken.ThrowIfCancellationRequested();
            return await userManager.Users.SingleOrDefaultAsync(u => u.PhoneNumber == PhoneNumber, cancellationToken);
        }
    }
}
