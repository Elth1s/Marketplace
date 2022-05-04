using DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace DAL.Data
{
    public class MarketplaceDbContextSeed
    {
        public static async Task SeedAsync(MarketplaceDbContext marketplaceDbContext/*, UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager*/)
        {

            if (marketplaceDbContext.Database.IsSqlServer())
            {
                marketplaceDbContext.Database.Migrate();
            }

            //await roleManager.CreateAsync(new IdentityRole(BlazorShared.Authorization.Constants.Roles.ADMINISTRATORS));

            //var defaultUser = new ApplicationUser { UserName = "demouser@microsoft.com", Email = "demouser@microsoft.com" };
            //await userManager.CreateAsync(defaultUser, AuthorizationConstants.DEFAULT_PASSWORD);

            //string adminUserName = "admin@microsoft.com";
            //var adminUser = new ApplicationUser { UserName = adminUserName, Email = adminUserName };
            //await userManager.CreateAsync(adminUser, AuthorizationConstants.DEFAULT_PASSWORD);
            //adminUser = await userManager.FindByNameAsync(adminUserName);
            //await userManager.AddToRoleAsync(adminUser, BlazorShared.Authorization.Constants.Roles.ADMINISTRATORS);
        }

        static IEnumerable<Category> GetPreconfiguredMarketplaceCategory()
        {
            var categories = new List<Category>
            {
/* 1 */         new(){ Name = "Technology and electronics", ParentId = null},  
/* 2 */         new(){ Name = "Clothes and shoes", ParentId = null},

/* 3 */         new(){ Name = "Computer equipment and software", ParentId = 1},
/* 4 */         new(){ Name = "Household appliances", ParentId = 1},
/* 5 */         new(){ Name = "Phones and accessories", ParentId = 1},
/* 6 */         new(){ Name = "Audio equipment and accessories", ParentId = 1},
/* 7 */         new(){ Name = "Spare parts for machinery and electronics", ParentId = 1},
/* 8 */         new(){ Name = "TV and video equipment", ParentId = 1},
/* 9 */         new(){ Name = "Car electronics", ParentId = 1},
/* 10 */        new(){ Name = "Photos, camcorders and accessories", ParentId = 1},
/* 11 */        new(){ Name = "3d devices", ParentId = 1},
/* 12 */        new(){ Name = "Equipment for satellite internet", ParentId = 1},
                
/* 13 */        new(){ Name = "Men's clothing", ParentId = 2},
/* 14 */        new(){ Name = "Women's clothes", ParentId = 2},
/* 15 */        new(){ Name = "Children's clothes, shoes, accessories", ParentId = 2},
/* 16 */        new(){ Name = "Sportswear and footwear", ParentId = 2},
/* 17 */        new(){ Name = "Footwear", ParentId = 2},
/* 18 */        new(){ Name = "Overalls and shoes", ParentId = 2},
/* 19 */        new(){ Name = "Carnival costumes", ParentId = 2},
/* 20 */        new(){ Name = "Ethnic clothing", ParentId = 2},

/* 21 */        new(){ Name = "Tablet computers", ParentId = 3},
/* 22 */        new(){ Name = "Laptops", ParentId = 3},
/* 23 */        new(){ Name = "Monitors", ParentId = 3},
/* 24 */        new(){ Name = "Accessories for computer equipment", ParentId = 3},
/* 25 */        new(){ Name = "Computer accessories", ParentId = 3},
/* 26 */        new(){ Name = "Smart watches and fitness bracelets", ParentId = 3},
/* 27 */        new(){ Name = "Printers, scanners, MFPs and components", ParentId = 3},
/* 28 */        new(){ Name = "Information carriers", ParentId = 3},
/* 29 */        new(){ Name = "Game consoles and components", ParentId = 3},
/* 30 */        new(){ Name = "Desktops", ParentId = 3},
/* 31 */        new(){ Name = "Software", ParentId = 3},
/* 32 */        new(){ Name = "Server equipment", ParentId = 3},
/* 33 */        new(){ Name = "Mining equipment", ParentId = 3},
/* 34 */        new(){ Name = "E-books", ParentId = 3},
/* 35 */        new(){ Name = "Single board computers and nettops", ParentId = 3},
/* 36 */        new(){ Name = "Portable electronic translators", ParentId = 3},

/* 37 */        new(){ Name = "Cables for electronics", ParentId = 24},
/* 38 */        new(){ Name = "HDD, SSD", ParentId = 24},
/* 39 */        new(){ Name = "Batteries for laptops, tablets, e-books, translators", ParentId = 24},
/* 40 */        new(){ Name = "Laptop chargers", ParentId = 24},
/* 41 */        new(){ Name = "Laptop body parts", ParentId = 24},
/* 42 */        new(){ Name = "Memory modules", ParentId = 24},
/* 43 */        new(){ Name = "Processors", ParentId = 24},
/* 44 */        new(){ Name = "Coolers and cooling systems", ParentId = 24},
/* 45 */        new(){ Name = "Matrixes for laptops, tablets and monitors", ParentId = 24},
/* 46 */        new(){ Name = "Cables and connectors for laptops, computers, tablets", ParentId = 24},
/* 47 */        new(){ Name = "Keyboard blocks for laptops", ParentId = 24},
/* 48 */        new(){ Name = "Touchscreen for displays", ParentId = 24},
/* 49 */        new(){ Name = "Chips", ParentId = 24},
/* 50 */        new(){ Name = "Spare parts for TVs and monitors", ParentId = 24},
/* 51 */        new(){ Name = "Motherboards", ParentId = 24},
/* 52 */        new(){ Name = "Video cards", ParentId = 24},
/* 53 */        new(){ Name = "Enclosures for computers", ParentId = 24},
/* 54 */        new(){ Name = "Power supplies for computers", ParentId = 24},
/* 55 */        new(){ Name = "Patch cord", ParentId = 24},
/* 56 */        new(){ Name = "Pockets for hard drives", ParentId = 24},
/* 57 */        new(){ Name = "Adapters and port expansion cards", ParentId = 24},
/* 58 */        new(){ Name = "Audio parts for laptops", ParentId = 24},
/* 59 */        new(){ Name = "Thermal paste", ParentId = 24},
/* 60 */        new(){ Name = "Sound cards", ParentId = 24},
/* 61 */        new(){ Name = "Network cards", ParentId = 24},
/* 62 */        new(){ Name = "Optical drives", ParentId = 24},
/* 63 */        new(){ Name = "Cases for tablets", ParentId = 24},
/* 64 */        new(){ Name = "Accessories for matrices and displays", ParentId = 24},
/* 65 */        new(){ Name = "Cameras for laptops", ParentId = 24},
/* 66 */        new(){ Name = "Cooling systems for laptops", ParentId = 24},
/* 67 */        new(){ Name = "TV and FM tuners", ParentId = 24},
/* 68 */        new(){ Name = "Postcards", ParentId = 24},
/* 69 */        new(){ Name = "Accessories for routers", ParentId = 24},


            };
            return categories;
        }

        static IEnumerable<FilterGroup> GetPreconfiguredMarketplaceFilterGroup()
        {
            var filterGroups = new List<FilterGroup>
            {
/* 1 */         new(){ Name = "Сondition"},
/* 2 */         new(){ Name = "Purpose"},
/* 3 */         new(){ Name = "Video memory type"},
/* 4 */         new(){ Name = "Graphics chipset"},
/* 5 */         new(){ Name = "Memory bus width"},
/* 6 */         new(){ Name = "Producer"},  
/* 7 */         new(){ Name = "Connection type"},
/* 8 */         new(){ Name = "Interfaces"},
/* 9 */         new(){ Name = "Cooling system"},
/* 10 */        new(){ Name = "Peculiarities"},
/* 11 */        new(){ Name = "Producing country"},
/* 12 */        new(){ Name = "Quality class"},
/* 13 */        new(){ Name = "Warranty period, months"},
/* 14 */        new(){ Name = "Processor frequency, MHz"},
/* 15 */        new(){ Name = "Video memory frequency, MHz"},
/* 16 */        new(){ Name = "Video memory size, MB"},

            };
            return filterGroups;
        }

    }
}
