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
    //  dotnet ef migrations add marketplacecontext --context appidentitydbcontext -p../DAL/DAL.csproj -s WebAPI.csproj -o Data/Migrations
    //  dotnet ef database update -c marketplacecontext -p../DAL/DAL.csproj -s WebAPI.csproj
    public class MarketplaceContext : DbContext
    {
        public MarketplaceContext(DbContextOptions<MarketplaceContext> options) :
            base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

        }
    }
}
