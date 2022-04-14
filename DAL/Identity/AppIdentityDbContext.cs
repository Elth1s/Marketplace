using DAL.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Data
{
    //  dotnet tool update --global dotnet-ef
    //  dotnet restore
    //  dotnet tool restore
    //  dotnet ef migrations add InitialIdentityModel --context appidentitydbcontext -p../DAL/DAL.csproj -s WebAPI.csproj -o Identity/Migrations
    //  dotnet ef database update -c appidentitydbcontext -p../DAL/DAL.csproj -s WebAPI.csproj
    public class AppIdentityDbContext : IdentityDbContext<AppUser>
    {
        public AppIdentityDbContext(DbContextOptions<AppIdentityDbContext> options) :
            base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

        }
    }
}
