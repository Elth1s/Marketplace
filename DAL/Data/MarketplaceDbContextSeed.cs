using DAL.Constants;
using DAL.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using DayOfWeek = DAL.Entities.DayOfWeek;

namespace DAL.Data
{
    public class MarketplaceDbContextSeed
    {
        public static async Task SeedAsync(
            MarketplaceDbContext marketplaceDbContext,
            UserManager<AppUser> userManager,
            RoleManager<IdentityRole> roleManager)
        {

            if (marketplaceDbContext.Database.IsSqlServer())
            {
                marketplaceDbContext.Database.Migrate();
            }

            await roleManager.CreateAsync(new IdentityRole(Roles.Admin));
            await roleManager.CreateAsync(new IdentityRole(Roles.Seller));

            var defaultUser = new AppUser { UserName = UsersInfo.DefaultUserName, FirstName = UsersInfo.DefaultUserName, Email = UsersInfo.DefaultEmail };
            await userManager.CreateAsync(defaultUser, UsersInfo.DefaultPassword);
            defaultUser = await userManager.FindByNameAsync(UsersInfo.DefaultUserName);

            var adminUser = new AppUser { UserName = UsersInfo.AdminUserName, FirstName = UsersInfo.AdminUserName, Email = UsersInfo.AdminEmail };
            await userManager.CreateAsync(adminUser, UsersInfo.DefaultPassword);
            adminUser = await userManager.FindByNameAsync(UsersInfo.AdminUserName);
            await userManager.AddToRoleAsync(adminUser, Roles.Admin);

            if (!await marketplaceDbContext.Languages.AnyAsync())
            {
                using var transaction = marketplaceDbContext.Database.BeginTransaction();
                await marketplaceDbContext.Languages.AddRangeAsync(
                  GetPreconfiguredMarketplaceLanguages());
                marketplaceDbContext.Database.ExecuteSqlRaw("SET IDENTITY_INSERT Languages ON");
                await marketplaceDbContext.SaveChangesAsync();
                marketplaceDbContext.Database.ExecuteSqlRaw("SET IDENTITY_INSERT Languages OFF");
                transaction.Commit();
            }


            if (!await marketplaceDbContext.Countries.AnyAsync())
            {
                await marketplaceDbContext.Countries.AddRangeAsync(
                  GetPreconfiguredCountries());

                await marketplaceDbContext.SaveChangesAsync();
            }

            if (!await marketplaceDbContext.Sales.AnyAsync())
            {
                await marketplaceDbContext.Sales.AddRangeAsync(
                  GetPreconfiguredSales());

                await marketplaceDbContext.SaveChangesAsync();
            }

            if (!await marketplaceDbContext.Cities.AnyAsync())
            {
                await marketplaceDbContext.Cities.AddRangeAsync(
                  GetPreconfiguredCities());

                await marketplaceDbContext.SaveChangesAsync();
            }

            var clothersSale = marketplaceDbContext.Sales.Where(s => s.Id == 1).FirstOrDefault();
            var laptopsSale = marketplaceDbContext.Sales.Where(s => s.Id == 2).FirstOrDefault();
            if (!await marketplaceDbContext.Categories.AnyAsync())
            {
                await marketplaceDbContext.Categories.AddRangeAsync(
                  GetPreconfiguredMarketplaceCategories(clothersSale, laptopsSale));

                await marketplaceDbContext.SaveChangesAsync();
            }
            var WomensClothes = marketplaceDbContext.Categories.Where(c => c.Id == 21).FirstOrDefault();

            if (!await marketplaceDbContext.DaysOfWeek.AnyAsync())
            {
                using var transaction = marketplaceDbContext.Database.BeginTransaction();
                await marketplaceDbContext.DaysOfWeek.AddRangeAsync(
                  GetPreconfiguredMarketplaceDayOfWeeks());
                marketplaceDbContext.Database.ExecuteSqlRaw("SET IDENTITY_INSERT DaysOfWeek ON");
                await marketplaceDbContext.SaveChangesAsync();
                marketplaceDbContext.Database.ExecuteSqlRaw("SET IDENTITY_INSERT DaysOfWeek OFF");
                transaction.Commit();
            }

            if (!await marketplaceDbContext.Shops.AnyAsync())
            {
                await marketplaceDbContext.Shops.AddRangeAsync(
                  GetPreconfiguredMarketplaceShops(defaultUser.Id));

                await marketplaceDbContext.SaveChangesAsync();
            }

            adminUser.ShopId = 1;
            await userManager.UpdateAsync(adminUser);

            if (!await marketplaceDbContext.ProductStatuses.AnyAsync())
            {
                using var transaction = marketplaceDbContext.Database.BeginTransaction();
                await marketplaceDbContext.ProductStatuses.AddRangeAsync(
                  GetPreconfiguredMarketplaceProductStatus());
                marketplaceDbContext.Database.ExecuteSqlRaw("SET IDENTITY_INSERT ProductStatuses ON");
                await marketplaceDbContext.SaveChangesAsync();
                marketplaceDbContext.Database.ExecuteSqlRaw("SET IDENTITY_INSERT ProductStatuses OFF");
                transaction.Commit();
            }

            if (!await marketplaceDbContext.Products.AnyAsync())
            {
                await marketplaceDbContext.Products.AddRangeAsync(
                  GetPreconfiguredMarketplaceProducts());

                await marketplaceDbContext.SaveChangesAsync();
            }

            if (!await marketplaceDbContext.ProductImages.AnyAsync())
            {
                await marketplaceDbContext.ProductImages.AddRangeAsync(
                  GetPreconfiguredMarketplaceProductImages());

                await marketplaceDbContext.SaveChangesAsync();
            }

            if (!await marketplaceDbContext.Units.AnyAsync())
            {
                await marketplaceDbContext.Units.AddRangeAsync(
                  GetPreconfiguredMarketplaceUnits());

                await marketplaceDbContext.SaveChangesAsync();
            }

            if (!await marketplaceDbContext.FilterGroups.AnyAsync())
            {
                await marketplaceDbContext.FilterGroups.AddRangeAsync(
                  GetPreconfiguredMarketplaceFilterGroups());

                await marketplaceDbContext.SaveChangesAsync();
            }

            if (!await marketplaceDbContext.FilterNames.AnyAsync())
            {
                await marketplaceDbContext.FilterNames.AddRangeAsync(
                  GetPreconfiguredMarketplaceFilterNames());

                await marketplaceDbContext.SaveChangesAsync();
            }

            if (!await marketplaceDbContext.FilterValues.AnyAsync())
            {
                await marketplaceDbContext.FilterValues.AddRangeAsync(
                  GetPreconfiguredMarketplaceFilterValues(WomensClothes));

                await marketplaceDbContext.SaveChangesAsync();
            }

            if (!await marketplaceDbContext.FilterValueProducts.AnyAsync())
            {
                await marketplaceDbContext.FilterValueProducts.AddRangeAsync(
                  GetPreconfiguredMarketplaceFilterValueProducts());

                await marketplaceDbContext.SaveChangesAsync();
            }

            if (!await marketplaceDbContext.OrderStatuses.AnyAsync())
            {
                using var transaction = marketplaceDbContext.Database.BeginTransaction();
                await marketplaceDbContext.OrderStatuses.AddRangeAsync(
                  GetPreconfiguredMarketplaceOrderStatuses());
                marketplaceDbContext.Database.ExecuteSqlRaw("SET IDENTITY_INSERT OrderStatuses ON");
                await marketplaceDbContext.SaveChangesAsync();
                marketplaceDbContext.Database.ExecuteSqlRaw("SET IDENTITY_INSERT OrderStatuses OFF");
                transaction.Commit();
            }

            if (!await marketplaceDbContext.DeliveryTypes.AnyAsync())
            {
                using var transaction = marketplaceDbContext.Database.BeginTransaction();
                await marketplaceDbContext.DeliveryTypes.AddRangeAsync(
                  GetPreconfiguredMarketplaceDeliveryTypes());
                marketplaceDbContext.Database.ExecuteSqlRaw("SET IDENTITY_INSERT DeliveryTypes ON");
                await marketplaceDbContext.SaveChangesAsync();
                marketplaceDbContext.Database.ExecuteSqlRaw("SET IDENTITY_INSERT DeliveryTypes OFF");
                transaction.Commit();
            }

            if (!await marketplaceDbContext.Genders.AnyAsync())
            {
                await marketplaceDbContext.Genders.AddRangeAsync(
                  GetPreconfiguredMarketplaceGenders());

                await marketplaceDbContext.SaveChangesAsync();
            }
        }


        static IEnumerable<Country> GetPreconfiguredCountries()
        {
            var countries = new List<Country>()
            {
                    new(){ Code= "AF", CountryTranslations=new List<CountryTranslation>(){
                        new CountryTranslation(){ LanguageId=LanguageId.English, Name= "Afghanistan" } ,
                        new CountryTranslation(){ LanguageId=LanguageId.Ukrainian, Name= "Афганістан" } } },
                    new(){Code= "AX", CountryTranslations=new List<CountryTranslation>(){
                        new CountryTranslation(){ LanguageId=LanguageId.English, Name= "Alland Islands" },
                        new CountryTranslation(){ LanguageId=LanguageId.Ukrainian, Name= "Аландські острови" } } },
                    new() {  Code = "AL", CountryTranslations=new List<CountryTranslation>(){
                        new CountryTranslation(){ LanguageId=LanguageId.English, Name = "Albania" } ,
                        new CountryTranslation(){ LanguageId=LanguageId.Ukrainian, Name= "Албанія" } } },
                    new() {  Code = "DZ", CountryTranslations=new List<CountryTranslation>(){
                        new CountryTranslation(){ LanguageId=LanguageId.English, Name = "Algeria" } ,
                        new CountryTranslation(){ LanguageId=LanguageId.Ukrainian, Name= "Алжир" } } },
                    new() {  Code = "AS" , CountryTranslations=new List<CountryTranslation>(){
                        new CountryTranslation(){ LanguageId=LanguageId.English, Name = "American Samoa" } ,
                        new CountryTranslation(){ LanguageId=LanguageId.Ukrainian, Name= "Американське Самоа" } } },
                    new() { Code = "AD", CountryTranslations=new List<CountryTranslation>(){
                        new CountryTranslation(){ LanguageId=LanguageId.English, Name = "Andorra" } ,
                        new CountryTranslation(){ LanguageId=LanguageId.Ukrainian, Name= "Андорра" } } },
                    new() {  Code = "AO", CountryTranslations=new List<CountryTranslation>(){
                        new CountryTranslation(){ LanguageId=LanguageId.English, Name = "Angola" } ,
                        new CountryTranslation(){ LanguageId=LanguageId.Ukrainian, Name= "Ангола" } } },
                    new() {  Code = "AI", CountryTranslations=new List<CountryTranslation>(){
                        new CountryTranslation(){ LanguageId=LanguageId.English, Name = "Anguilla"} ,
                        new CountryTranslation(){ LanguageId=LanguageId.Ukrainian, Name= "Ангілья" } } },
                    new() {  Code = "AQ", CountryTranslations=new List<CountryTranslation>(){
                        new CountryTranslation(){ LanguageId=LanguageId.English, Name = "Antarctica" } ,
                        new CountryTranslation(){ LanguageId=LanguageId.Ukrainian, Name= "Антарктида" } } },
                    new() {  Code = "AG" , CountryTranslations=new List<CountryTranslation>(){
                        new CountryTranslation(){ LanguageId=LanguageId.English, Name = "Antigua and Barbuda" } ,
                        new CountryTranslation(){ LanguageId=LanguageId.Ukrainian, Name= "Антигуа і Барбуда" } } },
                    new() { Code = "AR", CountryTranslations=new List<CountryTranslation>(){
                        new CountryTranslation(){ LanguageId=LanguageId.English, Name = "Argentina" } ,
                        new CountryTranslation(){ LanguageId=LanguageId.Ukrainian, Name= "Аргентина" } } },
                    new() {  Code = "AM" , CountryTranslations=new List<CountryTranslation>(){
                        new CountryTranslation(){ LanguageId=LanguageId.English, Name = "Armenia" } ,
                        new CountryTranslation(){ LanguageId=LanguageId.Ukrainian, Name= "Вірменія" } } },
                    new() { Code = "AW" , CountryTranslations=new List<CountryTranslation>(){
                        new CountryTranslation(){ LanguageId=LanguageId.English, Name = "Aruba" } ,
                        new CountryTranslation(){ LanguageId=LanguageId.Ukrainian, Name= "Аруба" } } },
                    new() {  Code = "AU", CountryTranslations=new List<CountryTranslation>(){
                        new CountryTranslation(){ LanguageId=LanguageId.English, Name = "Australia" } ,
                        new CountryTranslation(){ LanguageId=LanguageId.Ukrainian, Name= "Австралія" } } },
                    new() { Code = "AT" , CountryTranslations=new List<CountryTranslation>(){
                        new CountryTranslation(){ LanguageId=LanguageId.English, Name = "Austria" } ,
                        new CountryTranslation(){ LanguageId=LanguageId.Ukrainian, Name= "Австрія" } } },
                    new() {  Code = "AZ", CountryTranslations=new List<CountryTranslation>(){
                        new CountryTranslation(){ LanguageId=LanguageId.English, Name = "Azerbaijan" } ,
                        new CountryTranslation(){ LanguageId=LanguageId.Ukrainian, Name= "Азербайджан" } } },
                    new() {  Code = "BS", CountryTranslations=new List<CountryTranslation>(){
                        new CountryTranslation(){ LanguageId=LanguageId.English, Name = "Bahamas" } ,
                        new CountryTranslation(){ LanguageId=LanguageId.Ukrainian, Name= "Багамські острови" } } },
                    new() { Code = "BH", CountryTranslations=new List<CountryTranslation>(){
                        new CountryTranslation(){ LanguageId=LanguageId.English, Name = "Bahrain" } ,
                        new CountryTranslation(){ LanguageId=LanguageId.Ukrainian, Name= "Бахрейн" } } },
                    new() {  Code = "BD" , CountryTranslations=new List<CountryTranslation>(){
                        new CountryTranslation(){ LanguageId=LanguageId.English, Name = "Bangladesh" } ,
                        new CountryTranslation(){ LanguageId=LanguageId.Ukrainian, Name= "Бангладеш" } } },
                    new() { Code = "BB", CountryTranslations=new List<CountryTranslation>(){
                        new CountryTranslation(){ LanguageId=LanguageId.English, Name = "Barbados" } ,
                        new CountryTranslation(){ LanguageId=LanguageId.Ukrainian, Name= "Барбадос" } } },
                    new() { Code = "BY" , CountryTranslations=new List<CountryTranslation>(){
                        new CountryTranslation(){ LanguageId=LanguageId.English, Name = "Belarus" } ,
                        new CountryTranslation(){ LanguageId=LanguageId.Ukrainian, Name= "Білорусь" } } },
                    new() { Code = "BE", CountryTranslations=new List<CountryTranslation>(){
                        new CountryTranslation(){ LanguageId=LanguageId.English, Name = "Belgium" } ,
                        new CountryTranslation(){ LanguageId=LanguageId.Ukrainian, Name= "Бельгія" } } },
                    new() {  Code = "BZ", CountryTranslations=new List<CountryTranslation>(){
                        new CountryTranslation(){ LanguageId=LanguageId.English, Name = "Belize" } ,
                        new CountryTranslation(){ LanguageId=LanguageId.Ukrainian, Name= "Беліз" } } },
                    new() { Code = "BJ" , CountryTranslations=new List<CountryTranslation>(){
                        new CountryTranslation(){ LanguageId=LanguageId.English, Name = "Benin" } ,
                        new CountryTranslation(){ LanguageId=LanguageId.Ukrainian, Name= "Бенін" } } },
                    new() {  Code = "BM" , CountryTranslations=new List<CountryTranslation>(){
                        new CountryTranslation(){ LanguageId=LanguageId.English, Name = "Bermuda" } ,
                        new CountryTranslation(){ LanguageId=LanguageId.Ukrainian, Name= "Бермудські острови" } } },
                    new() {  Code = "BT", CountryTranslations=new List<CountryTranslation>(){
                        new CountryTranslation(){ LanguageId=LanguageId.English, Name = "Bhutan" } ,
                        new CountryTranslation(){ LanguageId=LanguageId.Ukrainian, Name= "Бутан" } } },
                    new() { Code = "BO" , CountryTranslations=new List<CountryTranslation>(){
                        new CountryTranslation(){ LanguageId=LanguageId.English, Name = "Bolivia" } ,
                        new CountryTranslation(){ LanguageId=LanguageId.Ukrainian, Name= "Болівія" } } },
                    new() { Code = "BA", CountryTranslations=new List<CountryTranslation>(){
                        new CountryTranslation(){ LanguageId=LanguageId.English, Name = "Bosnia and Herzegovina" } ,
                        new CountryTranslation(){ LanguageId=LanguageId.Ukrainian, Name= "Боснія і Герцеговина" } } },
                    new() {  Code = "BW" , CountryTranslations=new List<CountryTranslation>(){
                        new CountryTranslation(){ LanguageId=LanguageId.English, Name = "Botswana" } ,
                        new CountryTranslation(){ LanguageId=LanguageId.Ukrainian, Name= "Ботсвана" } } },
                    new() {  Code = "BV" , CountryTranslations=new List<CountryTranslation>(){
                        new CountryTranslation(){ LanguageId=LanguageId.English, Name = "Bouvet Island" } ,
                        new CountryTranslation(){ LanguageId=LanguageId.Ukrainian, Name= "Острів Буве" } } },
                    new() {  Code = "BR" , CountryTranslations=new List<CountryTranslation>(){
                        new CountryTranslation(){ LanguageId=LanguageId.English, Name = "Brazil" } ,
                        new CountryTranslation(){ LanguageId=LanguageId.Ukrainian, Name= "Бразилія" } } },
                    new() { Code = "IO", CountryTranslations=new List<CountryTranslation>(){
                        new CountryTranslation(){ LanguageId=LanguageId.English, Name = "British Indian Ocean Territory" } ,
                        new CountryTranslation(){ LanguageId=LanguageId.Ukrainian, Name= "Британська територія в Індійському океані" } } },
                    new() { Code = "VG" , CountryTranslations=new List<CountryTranslation>(){
                        new CountryTranslation(){ LanguageId=LanguageId.English, Name = "British Virgin Islands" } ,
                        new CountryTranslation(){ LanguageId=LanguageId.Ukrainian, Name= "Британські Віргінські острови" } } },
                    new() { Code = "BN" , CountryTranslations=new List<CountryTranslation>(){
                        new CountryTranslation(){ LanguageId=LanguageId.English, Name = "Brunei Darussalam" } ,
                        new CountryTranslation(){ LanguageId=LanguageId.Ukrainian, Name= "Бруней Даруссалам" } } },
                    new() { Code = "BG", CountryTranslations=new List<CountryTranslation>(){
                        new CountryTranslation(){ LanguageId=LanguageId.English, Name = "Bulgaria" } ,
                        new CountryTranslation(){ LanguageId=LanguageId.Ukrainian, Name= "Болгарія" } } },
                    new() {  Code = "BF" , CountryTranslations=new List<CountryTranslation>(){
                        new CountryTranslation(){ LanguageId=LanguageId.English, Name = "Burkina Faso" } ,
                        new CountryTranslation(){ LanguageId=LanguageId.Ukrainian, Name= "Буркіна Фасо" } } },
                    new() {  Code = "BI" , CountryTranslations=new List<CountryTranslation>(){
                        new CountryTranslation(){ LanguageId=LanguageId.English, Name = "Burundi" } ,
                        new CountryTranslation(){ LanguageId=LanguageId.Ukrainian, Name= "Бурунді" } } },
                    new() {  Code = "KH", CountryTranslations=new List<CountryTranslation>(){
                        new CountryTranslation(){ LanguageId=LanguageId.English, Name = "Cambodia" } ,
                        new CountryTranslation(){ LanguageId=LanguageId.Ukrainian, Name= "Камбоджа" } } },
                    new() {  Code = "CM", CountryTranslations=new List<CountryTranslation>(){
                        new CountryTranslation(){ LanguageId=LanguageId.English, Name = "Cameroon" } ,
                        new CountryTranslation(){ LanguageId=LanguageId.Ukrainian, Name= "Камерун" } } },
                    new() {  Code = "CA" , CountryTranslations=new List<CountryTranslation>(){
                        new CountryTranslation(){ LanguageId=LanguageId.English, Name = "Canada" } ,
                        new CountryTranslation(){ LanguageId=LanguageId.Ukrainian, Name= "Канада" } } },
                    new() { Code = "CV" , CountryTranslations=new List<CountryTranslation>(){
                        new CountryTranslation(){ LanguageId=LanguageId.English, Name = "Cape Verde" } ,
                        new CountryTranslation(){ LanguageId=LanguageId.Ukrainian, Name= "Кабо-Верде" } } },
                    new() {  Code = "KY" , CountryTranslations=new List<CountryTranslation>(){
                        new CountryTranslation(){ LanguageId=LanguageId.English, Name = "Cayman Islands" } ,
                        new CountryTranslation(){ LanguageId=LanguageId.Ukrainian, Name= "Кайманові острови" } } },
                    new() { Code = "CF" , CountryTranslations=new List<CountryTranslation>(){
                        new CountryTranslation(){ LanguageId=LanguageId.English, Name = "Central African Republic" } ,
                        new CountryTranslation(){ LanguageId=LanguageId.Ukrainian, Name= "Центральноафриканська Республіка" } } },
                    new() {  Code = "TD" , CountryTranslations=new List<CountryTranslation>(){
                        new CountryTranslation(){ LanguageId=LanguageId.English, Name = "Chad" } ,
                        new CountryTranslation(){ LanguageId=LanguageId.Ukrainian, Name= "Чад" } } },
                    new() { Code = "CL", CountryTranslations=new List<CountryTranslation>(){
                        new CountryTranslation(){ LanguageId=LanguageId.English, Name = "Chile" } ,
                        new CountryTranslation(){ LanguageId=LanguageId.Ukrainian, Name= "Чілі" } } },
                    new() { Code = "CN" , CountryTranslations=new List<CountryTranslation>(){
                        new CountryTranslation(){ LanguageId=LanguageId.English, Name = "China" } ,
                        new CountryTranslation(){ LanguageId=LanguageId.Ukrainian, Name= "Китай" } } },
                    new() {  Code = "CX" , CountryTranslations=new List<CountryTranslation>(){
                        new CountryTranslation(){ LanguageId=LanguageId.English, Name = "Christmas Island" } ,
                        new CountryTranslation(){ LanguageId=LanguageId.Ukrainian, Name= "Острів Різдва" } } },
                    new() { Code = "CC" , CountryTranslations=new List<CountryTranslation>(){
                        new CountryTranslation(){ LanguageId=LanguageId.English, Name = "Cocos (Keeling) Islands" } ,
                        new CountryTranslation(){ LanguageId=LanguageId.Ukrainian, Name= "Кокосові (Кілінгові) острови" } } },
                    new() {  Code = "CO", CountryTranslations=new List<CountryTranslation>(){
                        new CountryTranslation(){ LanguageId=LanguageId.English, Name = "Colombia" } ,
                        new CountryTranslation(){ LanguageId=LanguageId.Ukrainian, Name= "Колумбія" } } },
                    new() { Code = "KM" , CountryTranslations=new List<CountryTranslation>(){
                        new CountryTranslation(){ LanguageId=LanguageId.English, Name = "Comoros" } ,
                        new CountryTranslation(){ LanguageId=LanguageId.Ukrainian, Name= "Коморські острови" } } },
                    new() {Code = "CG" , CountryTranslations=new List<CountryTranslation>(){
                        new CountryTranslation(){ LanguageId=LanguageId.English, Name = "Democratic Republic of the Congo" } ,
                        new CountryTranslation(){ LanguageId=LanguageId.Ukrainian, Name= "Демократична Республіка Конго" } } },
                    new() { Code = "CD" , CountryTranslations=new List<CountryTranslation>(){
                        new CountryTranslation(){ LanguageId=LanguageId.English, Name = "Republic of the Congo" } ,
                        new CountryTranslation(){ LanguageId=LanguageId.Ukrainian, Name= "Республіка Конго" } } },
                    new() {  Code = "CK" , CountryTranslations=new List<CountryTranslation>(){
                        new CountryTranslation(){ LanguageId=LanguageId.English, Name = "Cook Islands" } ,
                        new CountryTranslation(){ LanguageId=LanguageId.Ukrainian, Name= "Острови Кука" } } },
                    new() {  Code = "CR", CountryTranslations=new List<CountryTranslation>(){
                        new CountryTranslation(){ LanguageId=LanguageId.English, Name = "Costa Rica" } ,
                        new CountryTranslation(){ LanguageId=LanguageId.Ukrainian, Name= "Коста-Ріка" } } },
                    new() { Code = "CI" , CountryTranslations=new List<CountryTranslation>(){
                        new CountryTranslation(){ LanguageId=LanguageId.English, Name = "Cote d'Ivoire" } ,
                        new CountryTranslation(){ LanguageId=LanguageId.Ukrainian, Name= "Кот-д'Івуар" } } },
                    new() {  Code = "HR" , CountryTranslations=new List<CountryTranslation>(){
                        new CountryTranslation(){ LanguageId=LanguageId.English, Name = "Croatia" } ,
                        new CountryTranslation(){ LanguageId=LanguageId.Ukrainian, Name= "Хорватія" } } },
                    new() {  Code = "CU" , CountryTranslations=new List<CountryTranslation>(){
                        new CountryTranslation(){ LanguageId=LanguageId.English, Name = "Cuba" } ,
                        new CountryTranslation(){ LanguageId=LanguageId.Ukrainian, Name= "Куба" } } },
                    new() {  Code = "CW" , CountryTranslations=new List<CountryTranslation>(){
                        new CountryTranslation(){ LanguageId=LanguageId.English, Name = "Curacao" } ,
                        new CountryTranslation(){ LanguageId=LanguageId.Ukrainian, Name= "Кюрасао" } } },
                    new() {  Code = "CY" , CountryTranslations=new List<CountryTranslation>(){
                        new CountryTranslation(){ LanguageId=LanguageId.English, Name = "Cyprus" } ,
                        new CountryTranslation(){ LanguageId=LanguageId.Ukrainian, Name= "Кіпр" } } },
                    new() {  Code = "CZ" , CountryTranslations=new List<CountryTranslation>(){
                        new CountryTranslation(){ LanguageId=LanguageId.English, Name = "Czech Republic" } ,
                        new CountryTranslation(){ LanguageId=LanguageId.Ukrainian, Name= "Чехія" } } },
                    new() {  Code = "DK" , CountryTranslations=new List<CountryTranslation>(){
                        new CountryTranslation(){ LanguageId=LanguageId.English, Name = "Denmark" } ,
                        new CountryTranslation(){ LanguageId=LanguageId.Ukrainian, Name= "Данія" } } },
                    new() {  Code = "DJ" , CountryTranslations=new List<CountryTranslation>(){
                        new CountryTranslation(){ LanguageId=LanguageId.English, Name = "Djibouti" } ,
                        new CountryTranslation(){ LanguageId=LanguageId.Ukrainian, Name= "Джібуті" } } },
                    new() {  Code = "DM" , CountryTranslations=new List<CountryTranslation>(){
                        new CountryTranslation(){ LanguageId=LanguageId.English, Name = "Dominica" } ,
                        new CountryTranslation(){ LanguageId=LanguageId.Ukrainian, Name= "Домініка" } } },
                    new() {  Code = "DO" , CountryTranslations=new List<CountryTranslation>(){
                        new CountryTranslation(){ LanguageId=LanguageId.English, Name = "Dominican Republic" } ,
                        new CountryTranslation(){ LanguageId=LanguageId.Ukrainian, Name= "Домініканська республіка" } } },
                    new() {  Code = "EC", CountryTranslations=new List<CountryTranslation>(){
                        new CountryTranslation(){ LanguageId=LanguageId.English, Name = "Ecuador" } ,
                        new CountryTranslation(){ LanguageId=LanguageId.Ukrainian, Name= "Еквадор" } } },
                    new() {  Code = "EG" , CountryTranslations=new List<CountryTranslation>(){
                        new CountryTranslation(){ LanguageId=LanguageId.English, Name = "Egypt" } ,
                        new CountryTranslation(){ LanguageId=LanguageId.Ukrainian, Name= "Єгипет" } } },
                    new() {  Code = "SV", CountryTranslations=new List<CountryTranslation>(){
                        new CountryTranslation(){ LanguageId=LanguageId.English, Name = "El Salvador" } ,
                        new CountryTranslation(){ LanguageId=LanguageId.Ukrainian, Name= "Сальвадор" } } },
                    new() {  Code = "GQ" , CountryTranslations=new List<CountryTranslation>(){
                        new CountryTranslation(){ LanguageId=LanguageId.English, Name = "Equatorial Guinea" } ,
                        new CountryTranslation(){ LanguageId=LanguageId.Ukrainian, Name= "Екваторіальна Гвінея" } } },
                    new() {  Code = "ER" , CountryTranslations=new List<CountryTranslation>(){
                        new CountryTranslation(){ LanguageId=LanguageId.English, Name = "Eritrea" } ,
                        new CountryTranslation(){ LanguageId=LanguageId.Ukrainian, Name= "Еритрея" } } },
                    new() {  Code = "EE", CountryTranslations=new List<CountryTranslation>(){
                        new CountryTranslation(){ LanguageId=LanguageId.English, Name = "Estonia" } ,
                        new CountryTranslation(){ LanguageId=LanguageId.Ukrainian, Name= "Естонія" } } },
                    new() {  Code = "ET" , CountryTranslations=new List<CountryTranslation>(){
                        new CountryTranslation(){ LanguageId=LanguageId.English, Name = "Ethiopia" } ,
                        new CountryTranslation(){ LanguageId=LanguageId.Ukrainian, Name= "Ефіопія" } } },
                    new() {  Code = "FK" , CountryTranslations=new List<CountryTranslation>(){
                        new CountryTranslation(){ LanguageId=LanguageId.English, Name = "Falkland Islands (Malvinas)" } ,
                        new CountryTranslation(){ LanguageId=LanguageId.Ukrainian, Name= "Фолклендські (Мальвінські) острови" } } },
                    new() {  Code = "FO" , CountryTranslations=new List<CountryTranslation>(){
                        new CountryTranslation(){ LanguageId=LanguageId.English, Name = "Faroe Islands" } ,
                        new CountryTranslation(){ LanguageId=LanguageId.Ukrainian, Name= "Фарерські острови" } } },
                    new() { Code = "FJ" , CountryTranslations=new List<CountryTranslation>(){
                        new CountryTranslation(){ LanguageId=LanguageId.English, Name = "Fiji" } ,
                        new CountryTranslation(){ LanguageId=LanguageId.Ukrainian, Name= "Фіджі" } } },
                    new() {  Code = "FI" , CountryTranslations=new List<CountryTranslation>(){
                        new CountryTranslation(){ LanguageId=LanguageId.English, Name = "Finland" } ,
                        new CountryTranslation(){ LanguageId=LanguageId.Ukrainian, Name= "Фінляндія" } } },
                    new() {  Code = "FR", CountryTranslations=new List<CountryTranslation>(){
                        new CountryTranslation(){ LanguageId=LanguageId.English, Name = "France" } ,
                        new CountryTranslation(){ LanguageId=LanguageId.Ukrainian, Name= "Франція" } } },
                    new() {  Code = "GF" , CountryTranslations=new List<CountryTranslation>(){
                        new CountryTranslation(){ LanguageId=LanguageId.English, Name = "French Guiana" } ,
                        new CountryTranslation(){ LanguageId=LanguageId.Ukrainian, Name= "Французька Гвіана" } } },
                    new() { Code = "PF", CountryTranslations=new List<CountryTranslation>(){
                        new CountryTranslation(){ LanguageId=LanguageId.English, Name = "French Polynesia" } ,
                        new CountryTranslation(){ LanguageId=LanguageId.Ukrainian, Name= "Французька Полінезія" } } },
                    new() {  Code = "TF" , CountryTranslations=new List<CountryTranslation>(){
                        new CountryTranslation(){ LanguageId=LanguageId.English, Name = "French Southern Territories" } ,
                        new CountryTranslation(){ LanguageId=LanguageId.Ukrainian, Name= "Французькі Південні території" } } },
                    new() { Code = "GA", CountryTranslations=new List<CountryTranslation>(){
                        new CountryTranslation(){ LanguageId=LanguageId.English, Name = "Gabon" } ,
                        new CountryTranslation(){ LanguageId=LanguageId.Ukrainian, Name= "Габон" } } },
                    new() { Code = "GM" , CountryTranslations=new List<CountryTranslation>(){
                        new CountryTranslation(){ LanguageId=LanguageId.English, Name = "Gambia" } ,
                        new CountryTranslation(){ LanguageId=LanguageId.Ukrainian, Name= "Гамбія" } } },
                    new() {  Code = "GE" , CountryTranslations=new List<CountryTranslation>(){
                        new CountryTranslation(){ LanguageId=LanguageId.English, Name = "Georgia" } ,
                        new CountryTranslation(){ LanguageId=LanguageId.Ukrainian, Name= "Грузія" } } },
                    new() {  Code = "DE" , CountryTranslations=new List<CountryTranslation>(){
                        new CountryTranslation(){ LanguageId=LanguageId.English, Name = "Germany" } ,
                        new CountryTranslation(){ LanguageId=LanguageId.Ukrainian, Name= "Німеччина" } } },
                    new() {  Code = "GH" , CountryTranslations=new List<CountryTranslation>(){
                        new CountryTranslation(){ LanguageId=LanguageId.English, Name = "Ghana" } ,
                        new CountryTranslation(){ LanguageId=LanguageId.Ukrainian, Name= "Гана" } } },
                    new() {  Code = "GI" , CountryTranslations=new List<CountryTranslation>(){
                        new CountryTranslation(){ LanguageId=LanguageId.English, Name = "Gibraltar" } ,
                        new CountryTranslation(){ LanguageId=LanguageId.Ukrainian, Name= "Гібралтар" } } },
                    new() {  Code = "GR", CountryTranslations=new List<CountryTranslation>(){
                        new CountryTranslation(){ LanguageId=LanguageId.English, Name = "Greece" } ,
                        new CountryTranslation(){ LanguageId=LanguageId.Ukrainian, Name= "Греція" } } },
                    new() {  Code = "GL" , CountryTranslations=new List<CountryTranslation>(){
                        new CountryTranslation(){ LanguageId=LanguageId.English, Name = "Greenland" } ,
                        new CountryTranslation(){ LanguageId=LanguageId.Ukrainian, Name= "Гренландія" } } },
                    new() { Code = "GD" , CountryTranslations=new List<CountryTranslation>(){
                        new CountryTranslation(){ LanguageId=LanguageId.English, Name = "Grenada"  } ,
                        new CountryTranslation(){ LanguageId=LanguageId.Ukrainian, Name= "Гренада" } } },
                    new() {  Code = "GP" , CountryTranslations=new List<CountryTranslation>(){
                        new CountryTranslation(){ LanguageId=LanguageId.English, Name = "Guadeloupe"  } ,
                        new CountryTranslation(){ LanguageId=LanguageId.Ukrainian, Name= "Гваделупа" } } },
                    new() {  Code = "GU" , CountryTranslations=new List<CountryTranslation>(){
                        new CountryTranslation(){ LanguageId=LanguageId.English, Name = "Guam"  } ,
                        new CountryTranslation(){ LanguageId=LanguageId.Ukrainian, Name= "Гуам" } } },
                    new() { Code = "GT", CountryTranslations=new List<CountryTranslation>(){
                        new CountryTranslation(){ LanguageId=LanguageId.English, Name = "Guatemala"  } ,
                        new CountryTranslation(){ LanguageId=LanguageId.Ukrainian, Name= "Гватемала" } } },
                    new() {  Code = "GG" , CountryTranslations=new List<CountryTranslation>(){
                        new CountryTranslation(){ LanguageId=LanguageId.English, Name = "Guernsey"  } ,
                        new CountryTranslation(){ LanguageId=LanguageId.Ukrainian, Name= "Гернсі" } } },
                    new() {  Code = "GW" , CountryTranslations=new List<CountryTranslation>(){
                        new CountryTranslation(){ LanguageId=LanguageId.English, Name = "Guinea-Bissau"  } ,
                        new CountryTranslation(){ LanguageId=LanguageId.Ukrainian, Name= "Гвінея-Бісау" } } },
                    new() {  Code = "GN" , CountryTranslations=new List<CountryTranslation>(){
                        new CountryTranslation(){ LanguageId=LanguageId.English, Name = "Guinea"  } ,
                        new CountryTranslation(){ LanguageId=LanguageId.Ukrainian, Name= "Гвінея" } } },
                    new() {  Code = "GY" , CountryTranslations=new List<CountryTranslation>(){
                        new CountryTranslation(){ LanguageId=LanguageId.English, Name = "Guyana"  } ,
                        new CountryTranslation(){ LanguageId=LanguageId.Ukrainian, Name= "Гайана" } } },
                    new() {  Code = "HT" , CountryTranslations=new List<CountryTranslation>(){
                        new CountryTranslation(){ LanguageId=LanguageId.English, Name = "Haiti"  } ,
                        new CountryTranslation(){ LanguageId=LanguageId.Ukrainian, Name= "Гаїті" } } },
                    new() {  Code = "HM" , CountryTranslations=new List<CountryTranslation>(){
                        new CountryTranslation(){ LanguageId=LanguageId.English, Name = "Heard Island and McDonald Islands"  } ,
                        new CountryTranslation(){ LanguageId=LanguageId.Ukrainian, Name= "Острів Херд і Макдональд" } } },
                    new() {  Code = "VA" , CountryTranslations=new List<CountryTranslation>(){
                        new CountryTranslation(){ LanguageId=LanguageId.English, Name = "Holy See (Vatican City State)"  } ,
                        new CountryTranslation(){ LanguageId=LanguageId.Ukrainian, Name= "Святий Престол (місто-держава Ватикан)" } } },
                    new() {  Code = "HN", CountryTranslations=new List<CountryTranslation>(){
                        new CountryTranslation(){ LanguageId=LanguageId.English, Name = "Honduras"  } ,
                        new CountryTranslation(){ LanguageId=LanguageId.Ukrainian, Name= "Гондурас" } } },
                    new() {  Code = "HK" , CountryTranslations=new List<CountryTranslation>(){
                        new CountryTranslation(){ LanguageId=LanguageId.English, Name = "Hong Kong"  } ,
                        new CountryTranslation(){ LanguageId=LanguageId.Ukrainian, Name= "Гонконг" } } },
                    new() {  Code = "HU" , CountryTranslations=new List<CountryTranslation>(){
                        new CountryTranslation(){ LanguageId=LanguageId.English, Name = "Hungary"  } ,
                        new CountryTranslation(){ LanguageId=LanguageId.Ukrainian, Name= "Угорщина" } } },
                    new() {  Code = "IS" , CountryTranslations=new List<CountryTranslation>(){
                        new CountryTranslation(){ LanguageId=LanguageId.English, Name = "Iceland"  } ,
                        new CountryTranslation(){ LanguageId=LanguageId.Ukrainian, Name= "Ісландія" } } },
                    new() {  Code = "IN", CountryTranslations=new List<CountryTranslation>(){
                        new CountryTranslation(){ LanguageId=LanguageId.English, Name = "India"  } ,
                        new CountryTranslation(){ LanguageId=LanguageId.Ukrainian, Name= "Індія" } } },
                    new() {  Code = "ID" , CountryTranslations=new List<CountryTranslation>(){
                        new CountryTranslation(){ LanguageId=LanguageId.English, Name = "Indonesia"  } ,
                        new CountryTranslation(){ LanguageId=LanguageId.Ukrainian, Name= "Індонезія" } } },
                    new() {  Code = "IR" , CountryTranslations=new List<CountryTranslation>(){
                        new CountryTranslation(){ LanguageId=LanguageId.English, Name = "The Islamic Republic of Iran"  } ,
                        new CountryTranslation(){ LanguageId=LanguageId.Ukrainian, Name= "Ісламська Республіка Іран" } } },
                    new() {  Code = "IQ" , CountryTranslations=new List<CountryTranslation>(){
                        new CountryTranslation(){ LanguageId=LanguageId.English, Name = "Iraq"  } ,
                        new CountryTranslation(){ LanguageId=LanguageId.Ukrainian, Name= "Ірак" } } },
                    new() { Code = "IE" , CountryTranslations=new List<CountryTranslation>(){
                        new CountryTranslation(){ LanguageId=LanguageId.English, Name = "Ireland"  } ,
                        new CountryTranslation(){ LanguageId=LanguageId.Ukrainian, Name= "Ірландія" } } },
                    new() {  Code = "IM" , CountryTranslations=new List<CountryTranslation>(){
                        new CountryTranslation(){ LanguageId=LanguageId.English, Name = "Isle of Man"  } ,
                        new CountryTranslation(){ LanguageId=LanguageId.Ukrainian, Name= "Острів Мен" } } },
                    new() { Code = "IL" , CountryTranslations=new List<CountryTranslation>(){
                        new CountryTranslation(){ LanguageId=LanguageId.English, Name = "Israel"  } ,
                        new CountryTranslation(){ LanguageId=LanguageId.Ukrainian, Name= "Ізраїль" } } },
                    new() { Code = "IT", CountryTranslations=new List<CountryTranslation>(){
                        new CountryTranslation(){ LanguageId=LanguageId.English, Name = "Italy"  } ,
                        new CountryTranslation(){ LanguageId=LanguageId.Ukrainian, Name= "Італія" } } },
                    new() {  Code = "JM" , CountryTranslations=new List<CountryTranslation>(){
                        new CountryTranslation(){ LanguageId=LanguageId.English, Name = "Jamaica"  } ,
                        new CountryTranslation(){ LanguageId=LanguageId.Ukrainian, Name= "Ямайка" } } },
                    new() {  Code = "JP" , CountryTranslations=new List<CountryTranslation>(){
                        new CountryTranslation(){ LanguageId=LanguageId.English, Name = "Japan"  } ,
                        new CountryTranslation(){ LanguageId=LanguageId.Ukrainian, Name= "Японія" } } },
                    new() { Code = "JE" , CountryTranslations=new List<CountryTranslation>(){
                        new CountryTranslation(){ LanguageId=LanguageId.English, Name = "Jersey"  } ,
                        new CountryTranslation(){ LanguageId=LanguageId.Ukrainian, Name= "Джерсі" } } },
                    new() {  Code = "JO" , CountryTranslations=new List<CountryTranslation>(){
                        new CountryTranslation(){ LanguageId=LanguageId.English, Name = "Jordan"  } ,
                        new CountryTranslation(){ LanguageId=LanguageId.Ukrainian, Name= "Йорданія" } } },
                    new() {  Code = "KZ" , CountryTranslations=new List<CountryTranslation>(){
                        new CountryTranslation(){ LanguageId=LanguageId.English, Name = "Kazakhstan"  } ,
                        new CountryTranslation(){ LanguageId=LanguageId.Ukrainian, Name= "Казахстан" } } },
                    new() {  Code = "KE" , CountryTranslations=new List<CountryTranslation>(){
                        new CountryTranslation(){ LanguageId=LanguageId.English, Name = "Kenya"  } ,
                        new CountryTranslation(){ LanguageId=LanguageId.Ukrainian, Name= "Кенія" } } },
                    new() { Code = "KI" , CountryTranslations=new List<CountryTranslation>(){
                        new CountryTranslation(){ LanguageId=LanguageId.English, Name = "Kiribati"  } ,
                        new CountryTranslation(){ LanguageId=LanguageId.Ukrainian, Name= "Кірібаті" } } },
                    new() {  Code = "KP" , CountryTranslations=new List<CountryTranslation>(){
                        new CountryTranslation(){ LanguageId=LanguageId.English, Name = "Democratic People's Republic of Korea"  } ,
                        new CountryTranslation(){ LanguageId=LanguageId.Ukrainian, Name= "Корейська Народно-Демократична Республіка" } } },
                    new() {  Code = "KR" , CountryTranslations=new List<CountryTranslation>(){
                        new CountryTranslation(){ LanguageId=LanguageId.English, Name = "Republic of Korea"  } ,
                        new CountryTranslation(){ LanguageId=LanguageId.Ukrainian, Name= "Республіка Корея" } } },
                    new() {  Code = "XK" , CountryTranslations=new List<CountryTranslation>(){
                        new CountryTranslation(){ LanguageId=LanguageId.English, Name = "Kosovo"  } ,
                        new CountryTranslation(){ LanguageId=LanguageId.Ukrainian, Name= "Косово" } } },
                    new() {  Code = "KW", CountryTranslations=new List<CountryTranslation>(){
                        new CountryTranslation(){ LanguageId=LanguageId.English, Name = "Kuwait"  } ,
                        new CountryTranslation(){ LanguageId=LanguageId.Ukrainian, Name= "Кувейт" } } },
                    new() {  Code = "KG" , CountryTranslations=new List<CountryTranslation>(){
                        new CountryTranslation(){ LanguageId=LanguageId.English, Name = "Kyrgyzstan"  } ,
                        new CountryTranslation(){ LanguageId=LanguageId.Ukrainian, Name= "Киргизстан " } } },
                    new() { Code = "LA" , CountryTranslations=new List<CountryTranslation>(){
                        new CountryTranslation(){ LanguageId=LanguageId.English, Name = "Lao People's Democratic Republic"  } ,
                        new CountryTranslation(){ LanguageId=LanguageId.Ukrainian, Name= "Лаоська Народно-Демократична Республіка" } } },
                    new() {  Code = "LV" , CountryTranslations=new List<CountryTranslation>(){
                        new CountryTranslation(){ LanguageId=LanguageId.English, Name = "Latvia"  } ,
                        new CountryTranslation(){ LanguageId=LanguageId.Ukrainian, Name= "Латвія" } } },
                    new() {  Code = "LB" , CountryTranslations=new List<CountryTranslation>(){
                        new CountryTranslation(){ LanguageId=LanguageId.English, Name = "Lebanon"  } ,
                        new CountryTranslation(){ LanguageId=LanguageId.Ukrainian, Name= "Ліван" } } },
                    new() {  Code = "LS" , CountryTranslations=new List<CountryTranslation>(){
                        new CountryTranslation(){ LanguageId=LanguageId.English, Name = "Lesotho"  } ,
                        new CountryTranslation(){ LanguageId=LanguageId.Ukrainian, Name= "Ліван" } } },
                    new() {  Code = "LR" , CountryTranslations=new List<CountryTranslation>(){
                        new CountryTranslation(){ LanguageId=LanguageId.English, Name = "Liberia"  } ,
                        new CountryTranslation(){ LanguageId=LanguageId.Ukrainian, Name= "Ліберія" } } },
                    new() { Code = "LY" , CountryTranslations=new List<CountryTranslation>(){
                        new CountryTranslation(){ LanguageId=LanguageId.English, Name = "Libya"  } ,
                        new CountryTranslation(){ LanguageId=LanguageId.Ukrainian, Name= "Лівія" } } },
                    new() {  Code = "LI", CountryTranslations=new List<CountryTranslation>(){
                        new CountryTranslation(){ LanguageId=LanguageId.English, Name = "Liechtenstein"  } ,
                        new CountryTranslation(){ LanguageId=LanguageId.Ukrainian, Name= "Ліхтенштейн" } } },
                    new() { Code = "LT" , CountryTranslations=new List<CountryTranslation>(){
                        new CountryTranslation(){ LanguageId=LanguageId.English, Name = "Lithuania"  } ,
                        new CountryTranslation(){ LanguageId=LanguageId.Ukrainian, Name= "Литва" } } },
                    new() {  Code = "LU" , CountryTranslations=new List<CountryTranslation>(){
                        new CountryTranslation(){ LanguageId=LanguageId.English, Name = "Luxembourg"  } ,
                        new CountryTranslation(){ LanguageId=LanguageId.Ukrainian, Name= "Люксембург" } } },
                    new() {  Code = "MO" , CountryTranslations=new List<CountryTranslation>(){
                        new CountryTranslation(){ LanguageId=LanguageId.English, Name = "Macao"  } ,
                        new CountryTranslation(){ LanguageId=LanguageId.Ukrainian, Name= "Макао" } } },
                    new() {  Code = "MK" , CountryTranslations=new List<CountryTranslation>(){
                        new CountryTranslation(){ LanguageId=LanguageId.English, Name = "Former Yugoslav Republic of Macedonia" } ,
                        new CountryTranslation(){ LanguageId=LanguageId.Ukrainian, Name= "Колишня Югославська Республіка Македонія" } } },
                    new() { Code = "MG", CountryTranslations=new List<CountryTranslation>(){
                        new CountryTranslation(){ LanguageId=LanguageId.English, Name = "Madagascar"  } ,
                        new CountryTranslation(){ LanguageId=LanguageId.Ukrainian, Name= "Мадагаскар" } } },
                    new() { Code = "MW" , CountryTranslations=new List<CountryTranslation>(){
                        new CountryTranslation(){ LanguageId=LanguageId.English, Name = "Malawi"  } ,
                        new CountryTranslation(){ LanguageId=LanguageId.Ukrainian, Name= "Малаві" } } },
                    new() {  Code = "MY" , CountryTranslations=new List<CountryTranslation>(){
                        new CountryTranslation(){ LanguageId=LanguageId.English, Name = "Malaysia"  } ,
                        new CountryTranslation(){ LanguageId=LanguageId.Ukrainian, Name= "Малайзія" } } },
                    new() {  Code = "MV" , CountryTranslations=new List<CountryTranslation>(){
                        new CountryTranslation(){ LanguageId=LanguageId.English, Name = "Maldives"  } ,
                        new CountryTranslation(){ LanguageId=LanguageId.Ukrainian, Name= "Мальдіви" } } },
                    new() {  Code = "ML" , CountryTranslations=new List<CountryTranslation>(){
                        new CountryTranslation(){ LanguageId=LanguageId.English, Name = "Mali"  } ,
                        new CountryTranslation(){ LanguageId=LanguageId.Ukrainian, Name= "Малі" } } },
                    new() {  Code = "MT" , CountryTranslations=new List<CountryTranslation>(){
                        new CountryTranslation(){ LanguageId=LanguageId.English, Name = "Malta"  } ,
                        new CountryTranslation(){ LanguageId=LanguageId.Ukrainian, Name= "Мальта" } } },
                    new() {  Code = "MH" , CountryTranslations=new List<CountryTranslation>(){
                        new CountryTranslation(){ LanguageId=LanguageId.English, Name = "Marshall Islands"  } ,
                        new CountryTranslation(){ LanguageId=LanguageId.Ukrainian, Name= "Маршаллові острови" } } },
                    new() { Code = "MQ" , CountryTranslations=new List<CountryTranslation>(){
                        new CountryTranslation(){ LanguageId=LanguageId.English, Name = "Martinique"  } ,
                        new CountryTranslation(){ LanguageId=LanguageId.Ukrainian, Name= "Мартініка" } } },
                    new() {  Code = "MR" , CountryTranslations=new List<CountryTranslation>(){
                        new CountryTranslation(){ LanguageId=LanguageId.English, Name = "Mauritania"  } ,
                        new CountryTranslation(){ LanguageId=LanguageId.Ukrainian, Name= "Мавританія" } } },
                    new() { Code = "MU" , CountryTranslations=new List<CountryTranslation>(){
                        new CountryTranslation(){ LanguageId=LanguageId.English, Name = "Mauritius"  } ,
                        new CountryTranslation(){ LanguageId=LanguageId.Ukrainian, Name= "Маврикій" } } },
                    new() {  Code = "YT" , CountryTranslations=new List<CountryTranslation>(){
                        new CountryTranslation(){ LanguageId=LanguageId.English, Name = "Mayotte"  } ,
                        new CountryTranslation(){ LanguageId=LanguageId.Ukrainian, Name= "Майотта" } } },
                    new() {  Code = "MX" , CountryTranslations=new List<CountryTranslation>(){
                        new CountryTranslation(){ LanguageId=LanguageId.English, Name = "Mexico"  } ,
                        new CountryTranslation(){ LanguageId=LanguageId.Ukrainian, Name= "Мексика" } } },
                    new() {  Code = "FM" , CountryTranslations=new List<CountryTranslation>(){
                        new CountryTranslation(){ LanguageId=LanguageId.English, Name = "Federated States of Micronesia"  } ,
                        new CountryTranslation(){ LanguageId=LanguageId.Ukrainian, Name= "Федеративні Штати Мікронезії" } } },
                    new() {  Code = "MD" , CountryTranslations=new List<CountryTranslation>(){
                        new CountryTranslation(){ LanguageId=LanguageId.English, Name = "Republic of Moldova"  } ,
                        new CountryTranslation(){ LanguageId=LanguageId.Ukrainian, Name= "Республіка Молдова" } } },
                    new() {  Code = "MC" , CountryTranslations=new List<CountryTranslation>(){
                        new CountryTranslation(){ LanguageId=LanguageId.English, Name = "Monaco"  } ,
                        new CountryTranslation(){ LanguageId=LanguageId.Ukrainian, Name= "Монако" } } },
                    new() {  Code = "MN" , CountryTranslations=new List<CountryTranslation>(){
                        new CountryTranslation(){ LanguageId=LanguageId.English, Name = "Mongolia"  } ,
                        new CountryTranslation(){ LanguageId=LanguageId.Ukrainian, Name= "Монголія" } } },
                    new() {  Code = "ME", CountryTranslations=new List<CountryTranslation>(){
                        new CountryTranslation(){ LanguageId=LanguageId.English, Name = "Montenegro"  } ,
                        new CountryTranslation(){ LanguageId=LanguageId.Ukrainian, Name= "Чорногорія" } } },
                    new() { Code = "MS" , CountryTranslations=new List<CountryTranslation>(){
                        new CountryTranslation(){ LanguageId=LanguageId.English, Name = "Montserrat"  } ,
                        new CountryTranslation(){ LanguageId=LanguageId.Ukrainian, Name= "Монсеррат" } } },
                    new() {  Code = "MA" , CountryTranslations=new List<CountryTranslation>(){
                        new CountryTranslation(){ LanguageId=LanguageId.English, Name = "Morocco" } ,
                        new CountryTranslation(){ LanguageId=LanguageId.Ukrainian, Name= "Марокко" } } },
                    new() { Code = "MZ" , CountryTranslations=new List<CountryTranslation>(){
                        new CountryTranslation(){ LanguageId=LanguageId.English, Name = "Mozambique" } ,
                        new CountryTranslation(){ LanguageId=LanguageId.Ukrainian, Name= "Мозамбік" } } },
                    new() {  Code = "MM" , CountryTranslations=new List<CountryTranslation>(){
                        new CountryTranslation(){ LanguageId=LanguageId.English, Name = "Myanmar" } ,
                        new CountryTranslation(){ LanguageId=LanguageId.Ukrainian, Name= "М'янма" } } },
                    new() {  Code = "NA" , CountryTranslations=new List<CountryTranslation>(){
                        new CountryTranslation(){ LanguageId=LanguageId.English, Name = "Namibia" } ,
                        new CountryTranslation(){ LanguageId=LanguageId.Ukrainian, Name= "Намібія" } } },
                    new() {  Code = "NR" , CountryTranslations=new List<CountryTranslation>(){
                        new CountryTranslation(){ LanguageId=LanguageId.English, Name = "Nauru" } ,
                        new CountryTranslation(){ LanguageId=LanguageId.Ukrainian, Name= "Науру" } } },
                    new() {  Code = "NP" , CountryTranslations=new List<CountryTranslation>(){
                        new CountryTranslation(){ LanguageId=LanguageId.English, Name = "Nepal" } ,
                        new CountryTranslation(){ LanguageId=LanguageId.Ukrainian, Name= "Непал" } } },
                    new() {  Code = "NL" , CountryTranslations=new List<CountryTranslation>(){
                        new CountryTranslation(){ LanguageId=LanguageId.English, Name = "Netherlands" } ,
                        new CountryTranslation(){ LanguageId=LanguageId.Ukrainian, Name= "Нідерланди" } } },
                    new() {  Code = "NC" , CountryTranslations=new List<CountryTranslation>(){
                        new CountryTranslation(){ LanguageId=LanguageId.English, Name = "New Caledonia" } ,
                        new CountryTranslation(){ LanguageId=LanguageId.Ukrainian, Name= "Нова Каледонія" } } },
                    new() { Code = "NZ" , CountryTranslations=new List<CountryTranslation>(){
                        new CountryTranslation(){ LanguageId=LanguageId.English, Name = "New Zealand" } ,
                        new CountryTranslation(){ LanguageId=LanguageId.Ukrainian, Name= "Нова Зеландія" } } },
                    new() { Code = "NI" , CountryTranslations=new List<CountryTranslation>(){
                        new CountryTranslation(){ LanguageId=LanguageId.English, Name = "Nicaragua" } ,
                        new CountryTranslation(){ LanguageId=LanguageId.Ukrainian, Name= "Нікарагуа" } } },
                    new() {  Code = "NE" , CountryTranslations=new List<CountryTranslation>(){
                        new CountryTranslation(){ LanguageId=LanguageId.English, Name = "Niger" } ,
                        new CountryTranslation(){ LanguageId=LanguageId.Ukrainian, Name= "Нігер" } } },
                    new() {  Code = "NG" , CountryTranslations=new List<CountryTranslation>(){
                        new CountryTranslation(){ LanguageId=LanguageId.English, Name = "Nigeria" } ,
                        new CountryTranslation(){ LanguageId=LanguageId.Ukrainian, Name= "Нігерія" } } },
                    new() {  Code = "NU" , CountryTranslations=new List<CountryTranslation>(){
                        new CountryTranslation(){ LanguageId=LanguageId.English, Name = "Niue" } ,
                        new CountryTranslation(){ LanguageId=LanguageId.Ukrainian, Name= "Ніуе " } } },
                    new() { Code = "NF", CountryTranslations=new List<CountryTranslation>(){
                        new CountryTranslation(){ LanguageId=LanguageId.English, Name = "Norfolk Island" } ,
                        new CountryTranslation(){ LanguageId=LanguageId.Ukrainian, Name= "Острів Норфолк" } } },
                    new() {  Code = "MP", CountryTranslations=new List<CountryTranslation>(){
                        new CountryTranslation(){ LanguageId=LanguageId.English, Name = "Northern Mariana Islands" } ,
                        new CountryTranslation(){ LanguageId=LanguageId.Ukrainian, Name= "Північні Маріанські острови" } } },
                    new() {  Code = "NO" , CountryTranslations=new List<CountryTranslation>(){
                        new CountryTranslation(){ LanguageId=LanguageId.English, Name = "Norway" } ,
                        new CountryTranslation(){ LanguageId=LanguageId.Ukrainian, Name= "Норвегія" } } },
                    new() {  Code = "OM" , CountryTranslations=new List<CountryTranslation>(){
                        new CountryTranslation(){ LanguageId=LanguageId.English, Name = "Oman" } ,
                        new CountryTranslation(){ LanguageId=LanguageId.Ukrainian, Name= "Оман" } } },
                    new() { Code = "PK" , CountryTranslations=new List<CountryTranslation>(){
                        new CountryTranslation(){ LanguageId=LanguageId.English, Name = "Pakistan" } ,
                        new CountryTranslation(){ LanguageId=LanguageId.Ukrainian, Name= "Пакістан" } } },
                    new() {  Code = "PW" , CountryTranslations=new List<CountryTranslation>(){
                        new CountryTranslation(){ LanguageId=LanguageId.English, Name = "Palau" } ,
                        new CountryTranslation(){ LanguageId=LanguageId.Ukrainian, Name= "Палау " } } },
                    new() {  Code = "PS" , CountryTranslations=new List<CountryTranslation>(){
                        new CountryTranslation(){ LanguageId=LanguageId.English, Name = "State of Palestine" } ,
                        new CountryTranslation(){ LanguageId=LanguageId.Ukrainian, Name= "Держава Палестина" } } },
                    new() {  Code = "PA", CountryTranslations=new List<CountryTranslation>(){
                        new CountryTranslation(){ LanguageId=LanguageId.English, Name = "Panama" } ,
                        new CountryTranslation(){ LanguageId=LanguageId.Ukrainian, Name= "Панама" } } },
                    new() {  Code = "PG" , CountryTranslations=new List<CountryTranslation>(){
                        new CountryTranslation(){ LanguageId=LanguageId.English, Name = "Papua New Guinea" } ,
                        new CountryTranslation(){ LanguageId=LanguageId.Ukrainian, Name= "Папуа-Нова Гвінея" } } },
                    new() {  Code = "PY", CountryTranslations=new List<CountryTranslation>(){
                        new CountryTranslation(){ LanguageId=LanguageId.English, Name = "Paraguay" } ,
                        new CountryTranslation(){ LanguageId=LanguageId.Ukrainian, Name= "Парагвай" } } },
                    new() {  Code = "PE" , CountryTranslations=new List<CountryTranslation>(){
                        new CountryTranslation(){ LanguageId=LanguageId.English, Name = "Peru" } ,
                        new CountryTranslation(){ LanguageId=LanguageId.Ukrainian, Name= "Перу" } } },
                    new() {  Code = "PH" , CountryTranslations=new List<CountryTranslation>(){
                        new CountryTranslation(){ LanguageId=LanguageId.English, Name = "Philippines" } ,
                        new CountryTranslation(){ LanguageId=LanguageId.Ukrainian, Name= "Філіппіни" } } },
                    new() {  Code = "PN" , CountryTranslations=new List<CountryTranslation>(){
                        new CountryTranslation(){ LanguageId=LanguageId.English, Name = "Pitcairn" } ,
                        new CountryTranslation(){ LanguageId=LanguageId.Ukrainian, Name= "Піткерн" } } },
                    new() {  Code = "PL" , CountryTranslations=new List<CountryTranslation>(){
                        new CountryTranslation(){ LanguageId=LanguageId.English, Name = "Poland" } ,
                        new CountryTranslation(){ LanguageId=LanguageId.Ukrainian, Name= "Польща" } } },
                    new() {  Code = "PT" , CountryTranslations=new List<CountryTranslation>(){
                        new CountryTranslation(){ LanguageId=LanguageId.English, Name = "Portugal" } ,
                        new CountryTranslation(){ LanguageId=LanguageId.Ukrainian, Name= "Португалія" } } },
                    new() {  Code = "PR" , CountryTranslations=new List<CountryTranslation>(){
                        new CountryTranslation(){ LanguageId=LanguageId.English, Name = "Puerto Rico" } ,
                        new CountryTranslation(){ LanguageId=LanguageId.Ukrainian, Name= "Пуерто-Рико" } } },
                    new() { Code = "QA" , CountryTranslations=new List<CountryTranslation>(){
                        new CountryTranslation(){ LanguageId=LanguageId.English, Name = "Qatar" } ,
                        new CountryTranslation(){ LanguageId=LanguageId.Ukrainian, Name= "Катар" } } },
                    new() {  Code = "RE" , CountryTranslations=new List<CountryTranslation>(){
                        new CountryTranslation(){ LanguageId=LanguageId.English, Name = "Reunion" } ,
                        new CountryTranslation(){ LanguageId=LanguageId.Ukrainian, Name= "Реюньйон" } } },
                    new() { Code = "RO" , CountryTranslations=new List<CountryTranslation>(){
                        new CountryTranslation(){ LanguageId=LanguageId.English, Name = "Romania" } ,
                        new CountryTranslation(){ LanguageId=LanguageId.Ukrainian, Name= "Румунія" } } },
                    new() { Code = "RU" , CountryTranslations=new List<CountryTranslation>(){
                        new CountryTranslation(){ LanguageId=LanguageId.English, Name = "Russian Federation" } ,
                        new CountryTranslation(){ LanguageId=LanguageId.Ukrainian, Name= "Російська Федерація" } } },
                    new() {  Code = "RW" , CountryTranslations=new List<CountryTranslation>(){
                        new CountryTranslation(){ LanguageId=LanguageId.English, Name = "Rwanda" } ,
                        new CountryTranslation(){ LanguageId=LanguageId.Ukrainian, Name= "Руанда" } } },
                    new() {  Code = "BL" , CountryTranslations=new List<CountryTranslation>(){
                        new CountryTranslation(){ LanguageId=LanguageId.English, Name = "Saint Barthelemy" } ,
                        new CountryTranslation(){ LanguageId=LanguageId.Ukrainian, Name= "Сен-Бартелемі" } } },
                    new() {  Code = "SH" , CountryTranslations=new List<CountryTranslation>(){
                        new CountryTranslation(){ LanguageId=LanguageId.English, Name = "Saint Helena" } ,
                        new CountryTranslation(){ LanguageId=LanguageId.Ukrainian, Name= "Острів Святої Єлени" } } },
                    new() {  Code = "KN" , CountryTranslations=new List<CountryTranslation>(){
                        new CountryTranslation(){ LanguageId=LanguageId.English, Name = "Saint Kitts and Nevis" } ,
                        new CountryTranslation(){ LanguageId=LanguageId.Ukrainian, Name= "Сент-Кіттс і Невіс" } } },
                    new() { Code = "LC" , CountryTranslations=new List<CountryTranslation>(){
                        new CountryTranslation(){ LanguageId=LanguageId.English, Name = "Saint Lucia" } ,
                        new CountryTranslation(){ LanguageId=LanguageId.Ukrainian, Name= "Сент-Люсія" } } },
                    new() {  Code = "MF" , CountryTranslations=new List<CountryTranslation>(){
                        new CountryTranslation(){ LanguageId=LanguageId.English, Name = "Saint Martin (French part)" } ,
                        new CountryTranslation(){ LanguageId=LanguageId.Ukrainian, Name= "Сен-Мартен (французька частина)" } } },
                    new() {  Code = "PM" , CountryTranslations=new List<CountryTranslation>(){
                        new CountryTranslation(){ LanguageId=LanguageId.English, Name = "Saint Pierre and Miquelon" } ,
                        new CountryTranslation(){ LanguageId=LanguageId.Ukrainian, Name= "Сен-П'єр і Мікелон" } } },
                    new() {  Code = "VC", CountryTranslations=new List<CountryTranslation>(){
                        new CountryTranslation(){ LanguageId=LanguageId.English, Name = "Saint Vincent and the Grenadines" } ,
                        new CountryTranslation(){ LanguageId=LanguageId.Ukrainian, Name= "Сент-Вінсент і Гренадини" } } },
                    new() {  Code = "WS" , CountryTranslations=new List<CountryTranslation>(){
                        new CountryTranslation(){ LanguageId=LanguageId.English, Name = "Samoa" } ,
                        new CountryTranslation(){ LanguageId=LanguageId.Ukrainian, Name= "Самоа" } } },
                    new() {  Code = "SM" , CountryTranslations=new List<CountryTranslation>(){
                        new CountryTranslation(){ LanguageId=LanguageId.English, Name = "San Marino" } ,
                        new CountryTranslation(){ LanguageId=LanguageId.Ukrainian, Name= "Сан-Марино" } } },
                    new() {  Code = "ST" , CountryTranslations=new List<CountryTranslation>(){
                        new CountryTranslation(){ LanguageId=LanguageId.English, Name = "Sao Tome and Principe" } ,
                        new CountryTranslation(){ LanguageId=LanguageId.Ukrainian, Name= "Сан-Томе і Принсіпі" } } },
                    new() { Code = "SA" , CountryTranslations=new List<CountryTranslation>(){
                        new CountryTranslation(){ LanguageId=LanguageId.English, Name = "Saudi Arabia" } ,
                        new CountryTranslation(){ LanguageId=LanguageId.Ukrainian, Name= "Саудівська Аравія" } } },
                    new() {  Code = "SN" , CountryTranslations=new List<CountryTranslation>(){
                        new CountryTranslation(){ LanguageId=LanguageId.English, Name = "Senegal" } ,
                        new CountryTranslation(){ LanguageId=LanguageId.Ukrainian, Name= "Сенегал" } } },
                    new() { Code = "RS" , CountryTranslations=new List<CountryTranslation>(){
                        new CountryTranslation(){ LanguageId=LanguageId.English, Name = "Serbia" } ,
                        new CountryTranslation(){ LanguageId=LanguageId.Ukrainian, Name= "Сербія" } } },
                    new() {  Code = "SC" , CountryTranslations=new List<CountryTranslation>(){
                        new CountryTranslation(){ LanguageId=LanguageId.English, Name = "Seychelles" } ,
                        new CountryTranslation(){ LanguageId=LanguageId.Ukrainian, Name= "Сейшельські острови" } } },
                    new() {  Code = "SL" , CountryTranslations=new List<CountryTranslation>(){
                        new CountryTranslation(){ LanguageId=LanguageId.English, Name = "Sierra Leone" } ,
                        new CountryTranslation(){ LanguageId=LanguageId.Ukrainian, Name= "Сьєрра-Леоне" } } },
                    new() {  Code = "SG" , CountryTranslations=new List<CountryTranslation>(){
                        new CountryTranslation(){ LanguageId=LanguageId.English, Name = "Singapore" } ,
                        new CountryTranslation(){ LanguageId=LanguageId.Ukrainian, Name= "Сінгапур" } } },
                    new() {  Code = "SX" , CountryTranslations=new List<CountryTranslation>(){
                        new CountryTranslation(){ LanguageId=LanguageId.English, Name = "Sint Maarten (Dutch part)" } ,
                        new CountryTranslation(){ LanguageId=LanguageId.Ukrainian, Name= "Сінт-Мартен (голландська частина)" } } },
                    new() {  Code = "SK", CountryTranslations=new List<CountryTranslation>(){
                        new CountryTranslation(){ LanguageId=LanguageId.English, Name = "Slovakia" } ,
                        new CountryTranslation(){ LanguageId=LanguageId.Ukrainian, Name= "Словаччина" } } },
                    new() {  Code = "SI" , CountryTranslations=new List<CountryTranslation>(){
                        new CountryTranslation(){ LanguageId=LanguageId.English, Name = "Slovenia" } ,
                        new CountryTranslation(){ LanguageId=LanguageId.Ukrainian, Name= "Словенія" } } },
                    new() {  Code = "SB", CountryTranslations=new List<CountryTranslation>(){
                        new CountryTranslation(){ LanguageId=LanguageId.English, Name = "Solomon Islands" } ,
                        new CountryTranslation(){ LanguageId=LanguageId.Ukrainian, Name= "Соломонові острови" } } },
                    new() {  Code = "SO" , CountryTranslations=new List<CountryTranslation>(){
                        new CountryTranslation(){ LanguageId=LanguageId.English, Name = "Somalia" } ,
                        new CountryTranslation(){ LanguageId=LanguageId.Ukrainian, Name= "Сомалі" } } },
                    new() {  Code = "ZA" , CountryTranslations=new List<CountryTranslation>(){
                        new CountryTranslation(){ LanguageId=LanguageId.English, Name = "South Africa" } ,
                        new CountryTranslation(){ LanguageId=LanguageId.Ukrainian, Name= "Південна Африка" } } },
                    new() {  Code = "GS", CountryTranslations=new List<CountryTranslation>(){
                        new CountryTranslation(){ LanguageId=LanguageId.English, Name = "South Georgia and the South Sandwich Islands" } ,
                        new CountryTranslation(){ LanguageId=LanguageId.Ukrainian, Name= "Південна Джорджія та Південні Сандвічеві острови" } } },
                    new() {  Code = "SS" , CountryTranslations=new List<CountryTranslation>(){
                        new CountryTranslation(){ LanguageId=LanguageId.English, Name = "South Sudan" } ,
                        new CountryTranslation(){ LanguageId=LanguageId.Ukrainian, Name= "Південний Судан" } } },
                    new() {  Code = "ES" , CountryTranslations=new List<CountryTranslation>(){
                        new CountryTranslation(){ LanguageId=LanguageId.English, Name = "Spain" } ,
                        new CountryTranslation(){ LanguageId=LanguageId.Ukrainian, Name= "Іспанія" } } },
                    new() {  Code = "LK", CountryTranslations=new List<CountryTranslation>(){
                        new CountryTranslation(){ LanguageId=LanguageId.English, Name = "Sri Lanka" } ,
                        new CountryTranslation(){ LanguageId=LanguageId.Ukrainian, Name= "Шрі Ланка" } } },
                    new() { Code = "SD" , CountryTranslations=new List<CountryTranslation>(){
                        new CountryTranslation(){ LanguageId=LanguageId.English, Name = "Sudan" } ,
                        new CountryTranslation(){ LanguageId=LanguageId.Ukrainian, Name= "Судан" } } },
                    new() {  Code = "SR" , CountryTranslations=new List<CountryTranslation>(){
                        new CountryTranslation(){ LanguageId=LanguageId.English, Name = "Suriname" } ,
                        new CountryTranslation(){ LanguageId=LanguageId.Ukrainian, Name= "Сурінам" } } },
                    new() { Code = "SJ" , CountryTranslations=new List<CountryTranslation>(){
                        new CountryTranslation(){ LanguageId=LanguageId.English, Name = "Svalbard and Jan Mayen" } ,
                        new CountryTranslation(){ LanguageId=LanguageId.Ukrainian, Name= "Шпіцберген і Ян-Маєн" } } },
                    new() { Code = "SZ" , CountryTranslations=new List<CountryTranslation>(){
                        new CountryTranslation(){ LanguageId=LanguageId.English, Name = "Swaziland" } ,
                        new CountryTranslation(){ LanguageId=LanguageId.Ukrainian, Name= "Свазіленд" } } },
                    new() {  Code = "SE", CountryTranslations=new List<CountryTranslation>(){
                        new CountryTranslation(){ LanguageId=LanguageId.English, Name = "Sweden" } ,
                        new CountryTranslation(){ LanguageId=LanguageId.Ukrainian, Name= "Швеція" } } },
                    new() {  Code = "CH" , CountryTranslations=new List<CountryTranslation>(){
                        new CountryTranslation(){ LanguageId=LanguageId.English, Name = "Switzerland" } ,
                        new CountryTranslation(){ LanguageId=LanguageId.Ukrainian, Name= "Швейцарія" } } },
                    new() { Code = "SY" , CountryTranslations=new List<CountryTranslation>(){
                        new CountryTranslation(){ LanguageId=LanguageId.English, Name = "Syrian Arab Republic" } ,
                        new CountryTranslation(){ LanguageId=LanguageId.Ukrainian, Name= "Сирійська Арабська Республіка" } } },
                    new() {  Code = "TW" , CountryTranslations=new List<CountryTranslation>(){
                        new CountryTranslation(){ LanguageId=LanguageId.English, Name = "Taiwan" } ,
                        new CountryTranslation(){ LanguageId=LanguageId.Ukrainian, Name= "Тайвань" } } },
                    new() { Code = "TJ" , CountryTranslations=new List<CountryTranslation>(){
                        new CountryTranslation(){ LanguageId=LanguageId.English, Name = "Tajikistan" } ,
                        new CountryTranslation(){ LanguageId=LanguageId.Ukrainian, Name= "Таджикистан" } } },
                    new() { Code = "TH", CountryTranslations=new List<CountryTranslation>(){
                        new CountryTranslation(){ LanguageId=LanguageId.English, Name = "Thailand" } ,
                        new CountryTranslation(){ LanguageId=LanguageId.Ukrainian, Name= "Таїланд" } } },
                    new() { Code = "TL" , CountryTranslations=new List<CountryTranslation>(){
                        new CountryTranslation(){ LanguageId=LanguageId.English, Name = "Timor-Leste" } ,
                        new CountryTranslation(){ LanguageId=LanguageId.Ukrainian, Name= "Тимор-Лешті" } } },
                    new() { Code = "TG" , CountryTranslations=new List<CountryTranslation>(){
                        new CountryTranslation(){ LanguageId=LanguageId.English, Name = "Togo" } ,
                        new CountryTranslation(){ LanguageId=LanguageId.Ukrainian, Name= "Того" } } },
                    new() { Code = "TK" , CountryTranslations=new List<CountryTranslation>(){
                        new CountryTranslation(){ LanguageId=LanguageId.English, Name = "Tokelau" } ,
                        new CountryTranslation(){ LanguageId=LanguageId.Ukrainian, Name= "Токелау" } } },
                    new() { Code = "TO" , CountryTranslations=new List<CountryTranslation>(){
                        new CountryTranslation(){ LanguageId=LanguageId.English, Name = "Tonga" } ,
                        new CountryTranslation(){ LanguageId=LanguageId.Ukrainian, Name= "Тонга" } } },
                    new() { Code = "TT" , CountryTranslations=new List<CountryTranslation>(){
                        new CountryTranslation(){ LanguageId=LanguageId.English, Name = "Trinidad and Tobago" } ,
                        new CountryTranslation(){ LanguageId=LanguageId.Ukrainian, Name= "Тринідад і Тобаго" } } },
                    new() { Code = "TN", CountryTranslations=new List<CountryTranslation>(){
                        new CountryTranslation(){ LanguageId=LanguageId.English, Name = "Tunisia" } ,
                        new CountryTranslation(){ LanguageId=LanguageId.Ukrainian, Name= "Туніс" } } },
                    new() { Code = "TR", CountryTranslations=new List<CountryTranslation>(){
                        new CountryTranslation(){ LanguageId=LanguageId.English, Name = "Turkey" } ,
                        new CountryTranslation(){ LanguageId=LanguageId.Ukrainian, Name= "Туреччина" } } },
                    new() {  Code = "TM", CountryTranslations=new List<CountryTranslation>(){
                        new CountryTranslation(){ LanguageId=LanguageId.English, Name = "Turkmenistan" } ,
                        new CountryTranslation(){ LanguageId=LanguageId.Ukrainian, Name= "Туркменістан" } } },
                    new() { Code = "TC" , CountryTranslations=new List<CountryTranslation>(){
                        new CountryTranslation(){ LanguageId=LanguageId.English, Name = "Turks and Caicos Islands" } ,
                        new CountryTranslation(){ LanguageId=LanguageId.Ukrainian, Name= "Острови Теркс і Кайкос" } } },
                    new() { Code = "TV" , CountryTranslations=new List<CountryTranslation>(){
                        new CountryTranslation(){ LanguageId=LanguageId.English, Name = "Tuvalu" } ,
                        new CountryTranslation(){ LanguageId=LanguageId.Ukrainian, Name= "Тувалу" } } },
                    new() {  Code = "UG", CountryTranslations=new List<CountryTranslation>(){
                        new CountryTranslation(){ LanguageId=LanguageId.English, Name = "Uganda" } ,
                        new CountryTranslation(){ LanguageId=LanguageId.Ukrainian, Name= "Уганда " } } },
                    new() {  Code = "UA" , CountryTranslations=new List<CountryTranslation>(){
                        new CountryTranslation(){ LanguageId=LanguageId.English, Name = "Ukraine" } ,
                        new CountryTranslation(){ LanguageId=LanguageId.Ukrainian, Name= "Україна" } } },
                    new() {  Code = "AE" , CountryTranslations=new List<CountryTranslation>(){
                        new CountryTranslation(){ LanguageId=LanguageId.English, Name = "United Arab Emirates" } ,
                        new CountryTranslation(){ LanguageId=LanguageId.Ukrainian, Name= "Об'єднані Арабські Емірати" } } },
                    new() { Code = "GB" , CountryTranslations=new List<CountryTranslation>(){
                        new CountryTranslation(){ LanguageId=LanguageId.English, Name = "United Kingdom" } ,
                        new CountryTranslation(){ LanguageId=LanguageId.Ukrainian, Name= "Об'єднане Королівство" } } },
                    new() { Code = "TZ" , CountryTranslations=new List<CountryTranslation>(){
                        new CountryTranslation(){ LanguageId=LanguageId.English, Name = "United Republic of Tanzania" } ,
                        new CountryTranslation(){ LanguageId=LanguageId.Ukrainian, Name= "Об'єднана Республіка Танзанія" } } },
                    new() { Code = "US" , CountryTranslations=new List<CountryTranslation>(){
                        new CountryTranslation(){ LanguageId=LanguageId.English, Name = "United States" } ,
                        new CountryTranslation(){ LanguageId=LanguageId.Ukrainian, Name= "Сполучені Штати" } } },
                    new() { Code = "UY" , CountryTranslations=new List<CountryTranslation>(){
                        new CountryTranslation(){ LanguageId=LanguageId.English, Name = "Uruguay" } ,
                        new CountryTranslation(){ LanguageId=LanguageId.Ukrainian, Name= "Уругвай" } } },
                    new() { Code = "VI" , CountryTranslations=new List<CountryTranslation>(){
                        new CountryTranslation(){ LanguageId=LanguageId.English, Name = "US Virgin Islands" } ,
                        new CountryTranslation(){ LanguageId=LanguageId.Ukrainian, Name= "Віргінські острови США" } } },
                    new() { Code = "UZ" , CountryTranslations=new List<CountryTranslation>(){
                        new CountryTranslation(){ LanguageId=LanguageId.English, Name = "Uzbekistan" } ,
                        new CountryTranslation(){ LanguageId=LanguageId.Ukrainian, Name= "Узбекистан" } } },
                    new() { Code = "VU" , CountryTranslations=new List<CountryTranslation>(){
                        new CountryTranslation(){ LanguageId=LanguageId.English, Name = "Vanuatu" } ,
                        new CountryTranslation(){ LanguageId=LanguageId.Ukrainian, Name= "Вануату" } } },
                    new() { Code = "VE" , CountryTranslations=new List<CountryTranslation>(){
                        new CountryTranslation(){ LanguageId=LanguageId.English, Name = "Venezuela" } ,
                        new CountryTranslation(){ LanguageId=LanguageId.Ukrainian, Name= "Венесуела" } } },
                    new() { Code = "VN" , CountryTranslations=new List<CountryTranslation>(){
                        new CountryTranslation(){ LanguageId=LanguageId.English, Name = "Vietnam" } ,
                        new CountryTranslation(){ LanguageId=LanguageId.Ukrainian, Name= "В'єтнам" } } },
                    new() { Code = "WF" , CountryTranslations=new List<CountryTranslation>(){
                        new CountryTranslation(){ LanguageId=LanguageId.English, Name = "Wallis and Futuna" } ,
                        new CountryTranslation(){ LanguageId=LanguageId.Ukrainian, Name= "Уолліс і Футуна" } } },
                    new() { Code = "EH" , CountryTranslations=new List<CountryTranslation>(){
                        new CountryTranslation(){ LanguageId=LanguageId.English, Name = "Western Sahara" } ,
                        new CountryTranslation(){ LanguageId=LanguageId.Ukrainian, Name= "Західна Сахара" } } },
                    new() {  Code = "YE" , CountryTranslations=new List<CountryTranslation>(){
                        new CountryTranslation(){ LanguageId=LanguageId.English, Name = "Yemen" } ,
                        new CountryTranslation(){ LanguageId=LanguageId.Ukrainian, Name= "Ємен" } } },
                    new() {  Code = "ZM" , CountryTranslations=new List<CountryTranslation>(){
                        new CountryTranslation(){ LanguageId=LanguageId.English, Name = "Zambia" } ,
                        new CountryTranslation(){ LanguageId=LanguageId.Ukrainian, Name= "Замбія" } } },
                    new() { Code = "ZW" , CountryTranslations=new List<CountryTranslation>(){
                        new CountryTranslation(){ LanguageId=LanguageId.English, Name = "Zimbabwe" } ,
                        new CountryTranslation(){ LanguageId=LanguageId.Ukrainian, Name= "Зімбабве" } } },
            };
            return countries;
        }
        static IEnumerable<City> GetPreconfiguredCities()
        {
            var cities = new List<City>()
            {
                //Czech Republic
                new (){ CountryId=60 , CityTranslations=new List<CityTranslation>(){
                        new (){LanguageId=LanguageId.English, Name = "Prague" },
                        new (){LanguageId=LanguageId.Ukrainian, Name="Прага"} } },
                new (){  CountryId=60 , CityTranslations=new List<CityTranslation>(){
                        new (){LanguageId=LanguageId.English, Name = "Brno" },
                        new (){LanguageId=LanguageId.Ukrainian, Name="Брно"} } },
                new (){  CountryId=60 , CityTranslations=new List<CityTranslation>(){
                        new (){LanguageId=LanguageId.English, Name = "Ostrava" },
                        new (){LanguageId=LanguageId.Ukrainian, Name="Острава"} } },
                new (){  CountryId=60 , CityTranslations=new List<CityTranslation>(){
                        new (){LanguageId=LanguageId.English, Name = "Pilsen" },
                        new (){LanguageId=LanguageId.Ukrainian, Name="Пльзень"} } },
                new (){  CountryId=60 , CityTranslations=new List<CityTranslation>(){
                        new (){LanguageId=LanguageId.English, Name = "Liberec"},
                        new (){LanguageId=LanguageId.Ukrainian, Name="Ліберець"} } },
                new (){  CountryId=60 , CityTranslations=new List<CityTranslation>(){
                        new (){LanguageId=LanguageId.English, Name = "Olomouc" },
                        new (){LanguageId=LanguageId.Ukrainian, Name="Оломоуць"} } },

                //Poland
                new (){  CountryId=178, CityTranslations=new List<CityTranslation>(){
                        new (){LanguageId=LanguageId.English, Name = "Warsaw" },
                        new (){LanguageId=LanguageId.Ukrainian, Name="Варшава"} } },
                new (){  CountryId=178 , CityTranslations=new List<CityTranslation>(){
                        new (){LanguageId=LanguageId.English, Name = "Cracow" },
                        new (){LanguageId=LanguageId.Ukrainian, Name="Краків"} } },
                new (){ CountryId=178 , CityTranslations=new List<CityTranslation>(){
                        new (){LanguageId=LanguageId.English, Name = "Lodz"},
                        new (){LanguageId=LanguageId.Ukrainian, Name="Лодзь"} } },
                new (){  CountryId=178, CityTranslations=new List<CityTranslation>(){
                        new (){LanguageId=LanguageId.English, Name = "Wroclaw" },
                        new (){LanguageId=LanguageId.Ukrainian, Name="Вроцлав"} } },
                new (){  CountryId=178 , CityTranslations=new List<CityTranslation>(){
                        new (){LanguageId=LanguageId.English, Name = "Poznan" },
                        new (){LanguageId=LanguageId.Ukrainian, Name="Познань"} } },
                new (){  CountryId=178 , CityTranslations=new List<CityTranslation>(){
                        new (){LanguageId=LanguageId.English, Name = "Gdansk" },
                        new (){LanguageId=LanguageId.Ukrainian, Name="Гданськ"} } },

                //Ukraine
                new (){  CountryId=233 , CityTranslations=new List<CityTranslation>(){
                        new (){LanguageId=LanguageId.English, Name = "Kyiv" },
                        new (){LanguageId=LanguageId.Ukrainian, Name="Київ"} } },
                new (){  CountryId=233 , CityTranslations=new List<CityTranslation>(){
                        new (){LanguageId=LanguageId.English, Name = "Kharkiv" },
                        new (){LanguageId=LanguageId.Ukrainian, Name="Харків"} } },
                new (){  CountryId=233 , CityTranslations=new List<CityTranslation>(){
                        new (){LanguageId=LanguageId.English, Name = "Odesa" },
                        new (){LanguageId=LanguageId.Ukrainian, Name="Одеса"} } },
                new (){  CountryId=233 , CityTranslations=new List<CityTranslation>(){
                        new (){LanguageId=LanguageId.English, Name = "Dnipro" },
                        new (){LanguageId=LanguageId.Ukrainian, Name="Дніпро"} } },
                new (){ CountryId=233 , CityTranslations=new List<CityTranslation>(){
                        new (){LanguageId=LanguageId.English, Name = "Lviv" },
                        new (){LanguageId=LanguageId.Ukrainian, Name="Львів"} } },
                new (){  CountryId=233, CityTranslations=new List<CityTranslation>(){
                        new (){LanguageId=LanguageId.English, Name = "Mariupol" },
                        new (){LanguageId=LanguageId.Ukrainian, Name="Маріуполь"} } },
            };
            return cities;
        }

        static IEnumerable<Sale> GetPreconfiguredSales()
        {
            var sales = new List<Sale>()
            {
                new (){
                    Name ="Clothes and shoes",
                    DiscountMin =10,
                    DiscountMax=60,
                    DateStart=DateTime.SpecifyKind(DateTime.Now, DateTimeKind.Utc),
                    DateEnd=DateTime.SpecifyKind(DateTime.Now, DateTimeKind.Utc).AddDays(30),
                    SaleTranslations=new List<SaleTranslation>()
                    {
                        new(){ LanguageId=LanguageId.Ukrainian,
                               VerticalImage="uk-vertical-clothes-and-shoes.png",
                               HorizontalImage="uk-horizontal-clothes-and-shoes.png"
                        },
                        new(){ LanguageId=LanguageId.English,
                               VerticalImage="en-vertical-clothes-and-shoes.png",
                               HorizontalImage="en-horizontal-clothes-and-shoes.png"
                        }
                    }
                },
                new (){
                    Name="Laptops",
                    DiscountMin =10,
                    DiscountMax=60,
                    DateStart=DateTime.SpecifyKind(DateTime.Now, DateTimeKind.Utc),
                    DateEnd=DateTime.SpecifyKind(DateTime.Now, DateTimeKind.Utc).AddDays(30),
                    SaleTranslations=new List<SaleTranslation>()
                    {
                        new(){ LanguageId=LanguageId.Ukrainian,
                               VerticalImage="uk-vertical-laptop.png",
                               HorizontalImage="uk-horizontal-laptops.png"
                        },
                        new(){ LanguageId=LanguageId.English,
                               VerticalImage="en-vertical-laptop.png",
                               HorizontalImage="en-horizontal-laptops.png"
                        }
                    }
                },
                };
            return sales;
        }
        static IEnumerable<Category> GetPreconfiguredMarketplaceCategories(Sale clothesSale, Sale laptopSale)
        {
            var categories = new List<Category>
            {
/*1*/           new(){
                    UrlSlug = "beauty-and-health",
                    Image = "BeautyAndHealth.png",
                    LightIcon="BeautyAndHealthLight.png",
                    DarkIcon="BeautyAndHealthDark.png",
                    ActiveIcon="BeautyAndHealthActive.png",
                    ParentId = null,
                     CategoryTranslations=new List<CategoryTranslation>(){
                        new(){ Name = "Beauty and health", LanguageId=LanguageId.English },
                        new(){ Name = "Краса і здоров'я", LanguageId=LanguageId.Ukrainian } } },
/*2*/           new(){
                    UrlSlug = "house-and-garden",
                    Image = "HouseAndGarden.png",
                    LightIcon="HouseAndGardenLight.png",DarkIcon="HouseAndGardenDark.png",
                    ActiveIcon="HouseAndGardenActive.png",
                    ParentId = null,
                     CategoryTranslations=new List<CategoryTranslation>(){
                        new(){ Name = "House and garden",  LanguageId=LanguageId.English },
                        new(){ Name = "Дім і сад", LanguageId=LanguageId.Ukrainian } } },
/*3*/           new(){
                    UrlSlug = "clothes-and-shoes",
                    Image = "ClothesAndShoes.png",
                    LightIcon="ClothesAndShoesLight.png",
                    DarkIcon="ClothesAndShoesDark.png",
                    ActiveIcon="ClothesAndShoesActive.png",
                    ParentId = null,
                    Sales=new List<Sale>(){clothesSale},
                     CategoryTranslations = new List<CategoryTranslation>(){
                        new(){ Name = "Clothes and shoes", LanguageId=LanguageId.English },
                        new(){ Name = "Одяг та взуття", LanguageId=LanguageId.Ukrainian } } },
/*4*/           new (){ UrlSlug = "technology-and-electronics", Image = "TechnologyAndElectronics.png",LightIcon="TechnologyAndElectronicsLight.png",DarkIcon="TechnologyAndElectronicsDark.png",ActiveIcon="TechnologyAndElectronicsActive.png", ParentId = null,
                     CategoryTranslations=new List<CategoryTranslation>(){
                        new (){ Name = "Technology and electronics", LanguageId=LanguageId.English},
                        new (){ Name = "Техніка та електроніка", LanguageId=LanguageId.Ukrainian} } },
/*5*/           new()
                {
                    UrlSlug = "goods-for-children",
                    Image = "GoodsForChildren.png",
                    LightIcon = "GoodsForChildrenLight.png",
                    DarkIcon = "GoodsForChildrenDark.png",
                    ActiveIcon = "GoodsForChildrenActive.png",
                    ParentId = null,
                    CategoryTranslations = new List<CategoryTranslation>(){
                        new(){ Name = "Goods for children", LanguageId=LanguageId.English },
                        new(){ Name = "Товари для дітей", LanguageId=LanguageId.Ukrainian } }
                },
/*6*/           new()
                {
                    UrlSlug = "auto",
                    Image = "Auto.png",
                    ParentId = null,
                    CategoryTranslations = new List<CategoryTranslation>(){
                        new(){ Name = "Auto", LanguageId=LanguageId.English },
                        new(){ Name = "Авто", LanguageId=LanguageId.Ukrainian } }
                },
/*7*/           new()
                {
                    UrlSlug = "gifts-hobbies-books",
                    Image = "GiftsHobbiesBooks.png",
                    ParentId = null,
                    CategoryTranslations = new List<CategoryTranslation>(){
                        new(){ Name = "Gifts, hobbies, books", LanguageId=LanguageId.English },
                        new(){ Name = "Подарунки, хобі, книги", LanguageId=LanguageId.Ukrainian } }
                },
/*8*/           new()
                {
                    UrlSlug = "accessories-and-decorations",
                    Image = "AccessoriesAndDecorations.png",
                    ParentId = null,
                    CategoryTranslations = new List<CategoryTranslation>(){
                        new(){ Name = "Accessories and decorations", LanguageId=LanguageId.English },
                        new(){ Name = "Аксесуари та прикраси", LanguageId=LanguageId.Ukrainian } }
                },
/*9*/           new()
                {
                    UrlSlug = "materials-for-repair",
                    Image = "",
                    ParentId = null,
                    CategoryTranslations = new List<CategoryTranslation>(){
                        new(){ Name = "Materials for repair", LanguageId=LanguageId.English },
                        new(){ Name = "Матеріали для ремонту", LanguageId=LanguageId.Ukrainian } }
                },
/*10*/          new()
                {
                    UrlSlug = "sports-and-recreation",
                    Image = "SportsAndRecreation.png",
                    LightIcon = "SportsAndRecreationLight.png",
                    DarkIcon = "SportsAndRecreationDark.png",
                    ActiveIcon = "SportsAndRecreationActive.png",
                    ParentId = null,
                    CategoryTranslations = new List<CategoryTranslation>(){
                        new(){ Name = "Sports and recreation", LanguageId=LanguageId.English },
                        new(){ Name = "Спорт і відпочинок", LanguageId=LanguageId.Ukrainian } }
                },
/*11*/          new()
                {
                    UrlSlug = "medicines-and-medical-products",
                    Image = "",
                    ParentId = null,
                    CategoryTranslations = new List<CategoryTranslation>(){
                        new(){ Name = "Medicines and medical products", LanguageId=LanguageId.English },
                        new(){ Name = "Медикаменти та медичні товари", LanguageId=LanguageId.Ukrainian } }
                },
/*12*/          new()
                {
                    UrlSlug = "pets-and-pet-products",
                    Image = "",
                    ParentId = null,
                    CategoryTranslations = new List<CategoryTranslation>(){
                        new(){ Name = "Pets and pet products", LanguageId=LanguageId.English },
                        new(){ Name = "Домашні тварини та зоотовари", LanguageId=LanguageId.Ukrainian } }
                },
/*13*/          new()
                {
                    UrlSlug = "stationery",
                    Image = "Stationery.png",
                    LightIcon = "StationeryLight.png",
                    DarkIcon = "StationeryDark.png",
                    ActiveIcon = "StationeryActive.png",
                    ParentId = null,
                    CategoryTranslations = new List<CategoryTranslation>(){
                        new(){ Name = "Stationery",  LanguageId=LanguageId.English },
                        new(){ Name = "Канцтовари", LanguageId=LanguageId.Ukrainian } }
                },
/*14*/          new()
                {
                    UrlSlug = "overalls-and-shoes",
                    Image = "",
                    ParentId = null,
                    CategoryTranslations = new List<CategoryTranslation>(){
                        new(){ Name = "Overalls and shoes", LanguageId=LanguageId.English },
                        new(){ Name = "Спецодяг та взуття", LanguageId=LanguageId.Ukrainian } }
                },
/*15*/          new()
                {
                    UrlSlug = "wedding-goods",
                    Image = "",
                    ParentId = null,
                    CategoryTranslations = new List<CategoryTranslation>(){
                        new(){ Name = "Wedding goods", LanguageId=LanguageId.English },
                        new(){ Name = "Весільні товари", LanguageId=LanguageId.Ukrainian } }
                },
/*16*/          new()
                {
                    UrlSlug = "food-products-drinks",
                    Image = "",
                    ParentId = null,
                    CategoryTranslations = new List<CategoryTranslation>(){
                        new(){ Name = "Food products, drinks", LanguageId=LanguageId.English },
                        new(){ Name = "Продукти харчування, напої", LanguageId=LanguageId.Ukrainian } }
                },
/*17*/          new()
                {
                    UrlSlug = "tools",
                    Image = "Tools.png",
                    LightIcon = "ToolsLight.png",
                    DarkIcon = "ToolsDark.png",
                    ActiveIcon = "ToolsActive.png",
                    ParentId = null,
                    CategoryTranslations = new List<CategoryTranslation>(){
                        new(){ Name = "Tools", LanguageId=LanguageId.English },
                        new(){ Name = "Інструменти", LanguageId=LanguageId.Ukrainian } }
                },
/*18*/          new()
                {
                    UrlSlug = "antiques-and-collectibles",
                    Image = "",
                    ParentId = null,
                    CategoryTranslations = new List<CategoryTranslation>(){
                        new(){ Name = "Antiques and collectibles", LanguageId=LanguageId.English },
                        new(){ Name = "Антикваріат і колекціонування", LanguageId=LanguageId.Ukrainian } }
                },
/*19*/          new()
                {
                    UrlSlug = "сonstruction",
                    Image = "Construction.png",
                    LightIcon = "ConstructionLight.png",
                    DarkIcon = "ConstructionDark.png",
                    ActiveIcon = "ConstructionActive.png",
                    ParentId = null,
                    CategoryTranslations = new List<CategoryTranslation>(){
                        new(){ Name = "Construction", LanguageId=LanguageId.English },
                        new(){ Name = "Будівництво", LanguageId=LanguageId.Ukrainian } }
                },

/*20*/          new()
                {
                    UrlSlug = "mens-clothing",
                    Image = "",
                    ParentId = 3,
                    Sales=new List<Sale>(){clothesSale},
                    CategoryTranslations = new List<CategoryTranslation>(){
                        new(){ Name = "Men's clothing", LanguageId=LanguageId.English },
                        new(){ Name = "Чоловічий одяг", LanguageId=LanguageId.Ukrainian } }
                },
/*21*/          new()
                {
                    UrlSlug = "womens-clothes",
                    Image = "",
                    ParentId = 3,
                    Sales=new List<Sale>(){clothesSale},
                    CategoryTranslations = new List<CategoryTranslation>(){
                        new(){ Name = "Women's clothes", LanguageId=LanguageId.English },
                        new(){ Name = "Жіночий одяг", LanguageId=LanguageId.Ukrainian } }
                },
/*22*/          new()
                {
                    UrlSlug = "Childrens-clothes-shoes-accessories",
                    Image = "",
                    ParentId = 3,
                    CategoryTranslations = new List<CategoryTranslation>(){
                        new(){ Name = "Children's clothes, shoes, accessories", LanguageId=LanguageId.English },
                        new(){ Name = "Дитячі одяг, взуття, аксесуари", LanguageId=LanguageId.Ukrainian } }
                },
/*23*/          new()
                {
                    UrlSlug = "sportswear-and-footwear",
                    Image = "",
                    ParentId = 3,
                    Sales=new List<Sale>(){clothesSale},
                    CategoryTranslations = new List<CategoryTranslation>(){
                        new(){ Name = "Sportswear and footwear", LanguageId=LanguageId.English },
                        new(){ Name = "Спортивний одяг та взуття", LanguageId=LanguageId.Ukrainian } }
                },
/*24*/          new()
                {
                    UrlSlug = "footwear",
                    Image = "",
                    ParentId = 3,
                    Sales=new List<Sale>(){clothesSale},
                    CategoryTranslations = new List<CategoryTranslation>(){
                        new(){ Name = "Footwear", LanguageId=LanguageId.English },
                        new(){ Name = "Взуття", LanguageId=LanguageId.Ukrainian } }
                },
/*25*/          new()
                {
                    UrlSlug = "carnival-costumes",
                    Image = "",
                    ParentId = 3,
                    Sales=new List<Sale>(){clothesSale},
                    CategoryTranslations = new List<CategoryTranslation>(){
                        new(){ Name = "Carnival costumes", LanguageId=LanguageId.English },
                        new(){ Name = "Карнавальні костюми", LanguageId=LanguageId.Ukrainian } }
                },
/*26*/          new()
                {
                    UrlSlug = "ethnic-clothing",
                    Image = "",
                    ParentId = 3,
                    Sales=new List<Sale>(){clothesSale},
                    CategoryTranslations = new List<CategoryTranslation>(){
                        new(){ Name = "Ethnic clothing", LanguageId=LanguageId.English },
                        new(){ Name = "Етнічний одяг", LanguageId=LanguageId.Ukrainian } }
                },

/*27*/          new()
                {
                    UrlSlug = "computer-equipment-and-software",
                    Image = "",
                    ParentId = 4,
                    CategoryTranslations = new List<CategoryTranslation>(){
                        new(){ Name = "Computer equipment and software", LanguageId=LanguageId.English },
                        new(){ Name = "Комп'ютерна техніка та ПЗ", LanguageId=LanguageId.Ukrainian } }
                },
/*28*/          new()
                {
                    UrlSlug = "household-appliances",
                    Image = "",
                    ParentId = 4,
                    CategoryTranslations = new List<CategoryTranslation>(){
                        new(){ Name = "Household appliances", LanguageId=LanguageId.English },
                        new(){ Name = "Побутова техніка", LanguageId=LanguageId.Ukrainian } }
                },
/*29*/          new()
                {
                    UrlSlug = "phones-and-accessories",
                    Image = "",
                    ParentId = 4,
                    CategoryTranslations = new List<CategoryTranslation>(){
                        new(){ Name = "Phones and accessories", LanguageId=LanguageId.English },
                        new(){ Name = "Телефони та аксесуари", LanguageId=LanguageId.Ukrainian } }
                },
/*30*/          new()
                {
                    UrlSlug = "audio-equipment-and-accessories",
                    Image = "",
                    ParentId = 4,
                    CategoryTranslations = new List<CategoryTranslation>(){
                        new(){ Name = "Audio equipment and accessories", LanguageId=LanguageId.English },
                        new(){ Name = "Аудіотехніка і аксесуари", LanguageId=LanguageId.Ukrainian } }
                },
/*31*/          new()
                {
                    UrlSlug = "spare-parts-for-machinery-and-electronics",
                    Image = "",
                    ParentId = 4,
                    CategoryTranslations = new List<CategoryTranslation>(){
                        new(){ Name = "Spare parts for machinery and electronics", LanguageId=LanguageId.English},
                        new(){ Name = "Запчастини для техніки та електроніки", LanguageId=LanguageId.Ukrainian } }
                },
/*32*/          new()
                {
                    UrlSlug = "tv-and-video-equipment",
                    Image = "",
                    ParentId = 4,
                    CategoryTranslations = new List<CategoryTranslation>(){
                        new(){  Name = "TV and video equipment", LanguageId=LanguageId.English },
                        new(){ Name = "TV та відеотехніка", LanguageId=LanguageId.Ukrainian } }
                },
/*33*/          new()
                {
                    UrlSlug = "car-electronics",
                    Image = "",
                    ParentId = 4,
                    CategoryTranslations = new List<CategoryTranslation>(){
                        new(){ Name = "Car electronics", LanguageId=LanguageId.English },
                        new(){ Name = "Автомобільна електроніка", LanguageId=LanguageId.Ukrainian } }
                },
/*34*/          new()
                {
                    UrlSlug = "photos-camcorders-and-accessories",
                    Image = "",
                    ParentId = 4,
                    CategoryTranslations = new List<CategoryTranslation>(){
                        new(){ Name = "Photos, camcorders and accessories", LanguageId=LanguageId.English },
                        new(){ Name = "Фото, відеокамери та аксесуари", LanguageId=LanguageId.Ukrainian } }
                },
/*35*/          new()
                {
                    UrlSlug = "3d-devices",
                    Image = "",
                    ParentId = 4,
                    CategoryTranslations = new List<CategoryTranslation>(){
                        new(){Name = "3d devices", LanguageId=LanguageId.English },
                        new(){ Name = "3d пристрої", LanguageId=LanguageId.Ukrainian } }
                },
/*36*/          new()
                {
                    UrlSlug = "equipment-for-satellite-internet",
                    Image = "",
                    ParentId = 4,
                    CategoryTranslations = new List<CategoryTranslation>(){
                        new(){Name = "Equipment for satellite internet",  LanguageId=LanguageId.English },
                        new(){ Name = "Обладнання для супутникового інтернету", LanguageId=LanguageId.Ukrainian } }
                },

/*37*/          new()
                {
                    UrlSlug = "tablet-computers",
                    Image = "",
                    ParentId = 27,
                    CategoryTranslations = new List<CategoryTranslation>(){
                        new(){ Name = "Tablet computers", LanguageId=LanguageId.English },
                        new(){ Name = "Планшетні комп'ютери", LanguageId=LanguageId.Ukrainian } }
                },
/*38*/          new()
                {
                    UrlSlug = "laptops",
                    Image = "",
                    ParentId = 27,
                    Sales=new List<Sale>(){laptopSale},
                    CategoryTranslations = new List<CategoryTranslation>(){
                        new(){  Name = "Laptops", LanguageId=LanguageId.English },
                        new(){ Name = "Ноутбуки", LanguageId=LanguageId.Ukrainian } }
                },
/*39*/          new()
                {
                    UrlSlug = "monitors",
                    Image = "",
                    ParentId = 27,
                    CategoryTranslations = new List<CategoryTranslation>(){
                        new(){ Name = "Monitors", LanguageId=LanguageId.English },
                        new(){ Name = "Монітори", LanguageId=LanguageId.Ukrainian } }
                },
/*40*/          new()
                {
                    UrlSlug = "components-for-computer-equipment",
                    Image = "",
                    ParentId = 27,
                    CategoryTranslations = new List<CategoryTranslation>(){
                        new(){ Name = "Components for computer equipment",LanguageId=LanguageId.English },
                        new(){ Name = "Комплектуючі для комп'ютерної техніки", LanguageId=LanguageId.Ukrainian } }
                },
/*41*/          new()
                {
                    UrlSlug = "computer-accessories",
                    Image = "",
                    ParentId = 27,
                    CategoryTranslations = new List<CategoryTranslation>(){
                        new(){ Name = "Computer accessories", LanguageId=LanguageId.English},
                        new(){ Name = "Комп'ютерні аксесуари", LanguageId=LanguageId.Ukrainian } }
                },
/*42*/          new()
                {
                    UrlSlug = "smart-watches-and-fitness-bracelets",
                    Image = "",
                    ParentId = 27,
                    CategoryTranslations = new List<CategoryTranslation>(){
                        new(){ Name = "Smart watches and fitness bracelets", LanguageId=LanguageId.English },
                        new(){ Name = "Розумні годинники та фітнес браслети", LanguageId=LanguageId.Ukrainian } }
                },
/*43*/          new()
                {
                    UrlSlug = "printers-scanners-mfps-and-components",
                    Image = "",
                    ParentId = 27,
                    CategoryTranslations = new List<CategoryTranslation>(){
                        new(){ Name = "Printers, scanners, MFPs and components", LanguageId=LanguageId.English },
                        new(){ Name = "Принтери, сканери, МФУ та комплектуючі", LanguageId=LanguageId.Ukrainian } }
                },
/*44*/          new()
                {
                    UrlSlug = "information-carriers",
                    Image = "",
                    ParentId = 27,
                    CategoryTranslations = new List<CategoryTranslation>(){
                        new(){ Name = "Information carriers", LanguageId=LanguageId.English },
                        new(){ Name = "Носії інформації", LanguageId=LanguageId.Ukrainian } }
                },
/*45*/          new()
                {
                    UrlSlug = "game-consoles-and-components",
                    Image = "",
                    ParentId = 27,
                    CategoryTranslations = new List<CategoryTranslation>(){
                        new(){ Name = "Game consoles and components", LanguageId=LanguageId.English },
                        new(){ Name = "Ігрові приставки та компоненти", LanguageId=LanguageId.Ukrainian } }
                },
/*46*/          new()
                {
                    UrlSlug = "desktops",
                    Image = "",
                    ParentId = 27,
                    CategoryTranslations = new List<CategoryTranslation>(){
                        new(){ Name = "Desktops", LanguageId=LanguageId.English },
                        new(){ Name = "Настільні комп'ютери", LanguageId=LanguageId.Ukrainian } }
                },
/*47*/          new()
                {
                    UrlSlug = "software",
                    Image = "",
                    ParentId = 27,
                    CategoryTranslations = new List<CategoryTranslation>(){
                        new(){ Name = "Software", LanguageId=LanguageId.English },
                        new(){ Name = "Програмне забезпечення", LanguageId=LanguageId.Ukrainian } }
                },
/*48*/          new()
                {
                    UrlSlug = "server-equipment",
                    Image = "",
                    ParentId = 27,
                    CategoryTranslations = new List<CategoryTranslation>(){
                        new(){ Name = "Server equipment", LanguageId=LanguageId.English },
                        new(){ Name = "Серверне обладнання", LanguageId=LanguageId.Ukrainian } }
                },
/*49*/          new()
                {
                    UrlSlug = "mining-equipment",
                    Image = "",
                    ParentId = 27,
                    CategoryTranslations = new List<CategoryTranslation>(){
                        new(){ Name = "Mining equipment", LanguageId=LanguageId.English },
                        new(){ Name = "Обладнання для майнінгу", LanguageId=LanguageId.Ukrainian } }
                },
/*50*/          new()
                {
                    UrlSlug = "e-books",
                    Image = "",
                    ParentId = 27,
                    CategoryTranslations = new List<CategoryTranslation>(){
                        new(){ Name = "E-books",  LanguageId=LanguageId.English },
                        new(){ Name = "Електронні книги (пристрій)", LanguageId=LanguageId.Ukrainian } }
                },
/*51*/          new()
                {
                    UrlSlug = "single-board-computers-and-nettops",
                    Image = "",
                    ParentId = 27,
                    CategoryTranslations = new List<CategoryTranslation>(){
                        new(){ Name = "Single board computers and nettops", LanguageId=LanguageId.English },
                        new(){ Name = "Одноплатні комп'ютери та неттопи", LanguageId=LanguageId.Ukrainian } }
                },
/*52*/          new()
                {
                    UrlSlug = "portable-electronic-translators",
                    Image = "",
                    ParentId = 27,
                    CategoryTranslations = new List<CategoryTranslation>(){
                        new(){ Name = "Portable electronic translators", LanguageId=LanguageId.English },
                        new(){ Name = "Портативні електронні перекладачі", LanguageId=LanguageId.Ukrainian } }
                },

                new()
                {
                    UrlSlug = "cables-for-electronics",
                    Image = "",
                    ParentId = 40,
                    CategoryTranslations = new List<CategoryTranslation>(){
                        new(){ Name = "Cables for electronics", LanguageId=LanguageId.English },
                        new(){ Name = "Кабелі для електроніки", LanguageId=LanguageId.Ukrainian } }
                },
                new()
                {
                    UrlSlug = "hdd-ssd",
                    Image = "",
                    ParentId = 40,
                    CategoryTranslations = new List<CategoryTranslation>(){
                        new(){ Name = "HDD, SSD", LanguageId=LanguageId.English },
                        new(){ Name = "Внутрішні та зовнішні жорсткі диски, HDD, SSD", LanguageId=LanguageId.Ukrainian } }
                },
                new()
                {
                    UrlSlug = "batteries-for-laptops-tablets-e-books-translators",
                    Image = "",
                    ParentId = 40,
                    CategoryTranslations = new List<CategoryTranslation>(){
                        new(){ Name = "Batteries for laptops, tablets, e-books, translators", LanguageId=LanguageId.English },
                        new(){ Name = "Акумулятори для ноутбуків, планшетів, електронних книг, перекладачів", LanguageId=LanguageId.Ukrainian } }
                },
                new()
                {
                    UrlSlug = "laptop-chargers",
                    Image = "",
                    ParentId = 40,
                    CategoryTranslations = new List<CategoryTranslation>(){
                        new(){ Name = "Laptop chargers", LanguageId=LanguageId.English },
                        new(){ Name = "Зарядні пристрої для ноутбуків", LanguageId=LanguageId.Ukrainian } }
                },
                new()
                {
                    UrlSlug = "laptop-body-parts",
                    Image = "",
                    ParentId = 40,
                    CategoryTranslations = new List<CategoryTranslation>(){
                        new(){ Name = "Laptop body parts", LanguageId=LanguageId.English },
                        new(){ Name = "Частини корпусу ноутбука", LanguageId=LanguageId.Ukrainian } }
                },
                new()
                {
                    UrlSlug = "memory-modules",
                    Image = "",
                    ParentId = 40,
                    CategoryTranslations = new List<CategoryTranslation>(){
                        new(){ Name = "Memory modules", LanguageId=LanguageId.English },
                        new(){ Name = "Модулі пам'яті", LanguageId=LanguageId.Ukrainian } }
                },
                new()
                {
                    UrlSlug = "processors",
                    Image = "",
                    ParentId = 40,
                    CategoryTranslations = new List<CategoryTranslation>(){
                        new(){ Name = "Processors", LanguageId=LanguageId.English },
                        new(){ Name = "Процесори", LanguageId=LanguageId.Ukrainian } }
                },
                new()
                {
                    UrlSlug = "coolers-and-cooling-systems",
                    Image = "",
                    ParentId = 40,
                    CategoryTranslations = new List<CategoryTranslation>(){
                        new(){ Name = "Coolers and cooling systems", LanguageId=LanguageId.English },
                        new(){ Name = "Кулери і системи охолодження", LanguageId=LanguageId.Ukrainian } }
                },
                new()
                {
                    UrlSlug = "matrixes-for-laptops-tablets-and-monitors",
                    Image = "",
                    ParentId = 40,
                    CategoryTranslations = new List<CategoryTranslation>(){
                        new(){ Name = "Matrixes for laptops, tablets and monitors", LanguageId=LanguageId.English },
                        new(){ Name = "Матриці для ноутбуків, планшетів і моніторів", LanguageId=LanguageId.Ukrainian } }
                },
                new()
                {
                    UrlSlug = "cables-and-connectors-for-laptops-computers-tablets",
                    Image = "",
                    ParentId = 40,
                    CategoryTranslations = new List<CategoryTranslation>(){
                        new(){ Name = "Cables and connectors for laptops, computers, tablets", LanguageId=LanguageId.English },
                        new(){ Name = "Шлейфи та роз'єми для ноутбуків, комп'ютерів, планшетів", LanguageId=LanguageId.Ukrainian } }
                },
                new()
                {
                    UrlSlug = "keyboard-blocks-for-laptops",
                    Image = "",
                    ParentId = 40,
                    CategoryTranslations = new List<CategoryTranslation>(){
                        new(){ Name = "Keyboard blocks for laptops", LanguageId=LanguageId.English },
                        new(){ Name = "Клавіатурні блоки для ноутбуків", LanguageId=LanguageId.Ukrainian } }
                },
                new()
                {
                    UrlSlug = "touchscreen-for-displays",
                    Image = "",
                    ParentId = 40,
                    CategoryTranslations = new List<CategoryTranslation>(){
                        new(){ Name = "Touchscreen for displays", LanguageId=LanguageId.English },
                        new(){ Name = "Touchscreen для дисплеїв", LanguageId=LanguageId.Ukrainian } }
                },
                new()
                {
                    UrlSlug = "microcircuits",
                    Image = "",
                    ParentId = 40,
                    CategoryTranslations = new List<CategoryTranslation>(){
                        new(){ Name = "Microcircuits", LanguageId=LanguageId.English },
                        new(){ Name = "Мікросхеми", LanguageId=LanguageId.Ukrainian } }
                },
                new()
                {
                    UrlSlug = "spare-parts-for-tvs-and-monitors",
                    Image = "",
                    ParentId = 40,
                    CategoryTranslations = new List<CategoryTranslation>(){
                        new(){ Name = "Spare parts for TVs and monitors", LanguageId=LanguageId.English },
                        new(){ Name = "Запчастини для телевізорів і моніторів", LanguageId=LanguageId.Ukrainian } }
                },
                new()
                {
                    UrlSlug = "motherboards",
                    Image = "",
                    ParentId = 40,
                    CategoryTranslations = new List<CategoryTranslation>(){
                        new(){ Name = "Motherboards", LanguageId=LanguageId.English },
                        new(){ Name = "Материнські плати", LanguageId=LanguageId.Ukrainian } }
                },
                new()
                {
                    UrlSlug = "video-cards",
                    Image = "",
                    ParentId = 40,
                    CategoryTranslations = new List<CategoryTranslation>(){
                        new(){ Name = "Video cards", LanguageId=LanguageId.English},
                        new(){ Name = "Відеокарти", LanguageId=LanguageId.Ukrainian } }
                },
                new()
                {
                    UrlSlug = "enclosures-for-computers",
                    Image = "",
                    ParentId = 40,
                    CategoryTranslations = new List<CategoryTranslation>(){
                        new(){ Name = "Enclosures for computers", LanguageId=LanguageId.English },
                        new(){ Name = "Корпуси для комп'ютерів", LanguageId=LanguageId.Ukrainian } }
                },
                new()
                {
                    UrlSlug = "power-supplies-for-computers",
                    Image = "",
                    ParentId = 40,
                    CategoryTranslations = new List<CategoryTranslation>(){
                        new(){ Name = "Power supplies for computers", LanguageId=LanguageId.English },
                        new(){ Name = "Блоки живлення для комп'ютерів", LanguageId=LanguageId.Ukrainian } }
                },
                new()
                {
                    UrlSlug = "patch-cord",
                    Image = "",
                    ParentId = 40,
                    CategoryTranslations = new List<CategoryTranslation>(){
                        new(){ Name = "Patch cord", LanguageId=LanguageId.English },
                        new(){ Name = "Патч-корди", LanguageId=LanguageId.Ukrainian } }
                },
                new()
                {
                    UrlSlug = "pockets-for-hard-drives",
                    Image = "",
                    ParentId = 40,
                    CategoryTranslations = new List<CategoryTranslation>(){
                        new(){ Name = "Pockets for hard drives", LanguageId=LanguageId.English },
                        new(){ Name = "Кишені для жорстких дисків", LanguageId=LanguageId.Ukrainian } }
                },
                new()
                {
                    UrlSlug = "adapters-and-port-expansion-cards",
                    Image = "",
                    ParentId = 40,
                    CategoryTranslations = new List<CategoryTranslation>(){
                        new(){ Name = "Adapters and port expansion cards", LanguageId=LanguageId.English },
                        new(){ Name = "Адаптери і плати розширення портів", LanguageId=LanguageId.Ukrainian } }
                },
                new()
                {
                    UrlSlug = "audio-parts-for-laptops",
                    Image = "",
                    ParentId = 40,
                    CategoryTranslations = new List<CategoryTranslation>(){
                        new(){ Name = "Audio parts for laptops", LanguageId=LanguageId.English },
                        new(){ Name = "Звукові запчастини для портативних ПК", LanguageId=LanguageId.Ukrainian } }
                },
                new()
                {
                    UrlSlug = "thermal-paste",
                    Image = "",
                    ParentId = 40,
                    CategoryTranslations = new List<CategoryTranslation>(){
                        new(){ Name = "Thermal paste", LanguageId=LanguageId.English },
                        new(){ Name = "Термопаста", LanguageId=LanguageId.Ukrainian } }
                },
                new()
                {
                    UrlSlug = "sound-cards",
                    Image = "",
                    ParentId = 40,
                    CategoryTranslations = new List<CategoryTranslation>(){
                        new(){ Name = "Sound cards", LanguageId=LanguageId.English },
                        new(){ Name = "Звукові карти", LanguageId=LanguageId.Ukrainian } }
                },
                new()
                {
                    UrlSlug = "network-cards",
                    Image = "",
                    ParentId = 40,
                    CategoryTranslations = new List<CategoryTranslation>(){
                        new(){ Name = "Network cards", LanguageId=LanguageId.English },
                        new(){ Name = "Мережеві карти", LanguageId=LanguageId.Ukrainian } }
                },
                new()
                {
                    UrlSlug = "optical-drives",
                    Image = "",
                    ParentId = 40,
                    CategoryTranslations = new List<CategoryTranslation>(){
                        new(){ Name = "Optical drives", LanguageId=LanguageId.English },
                        new(){ Name = "Оптичні приводи", LanguageId=LanguageId.Ukrainian } }
                },
                new()
                {
                    UrlSlug = "cases-for-tablets",
                    Image = "",
                    ParentId = 40,
                    CategoryTranslations = new List<CategoryTranslation>(){
                        new(){ Name = "Cases for tablets", LanguageId=LanguageId.English },
                        new(){ Name = "Корпуси для планшетів", LanguageId=LanguageId.Ukrainian } }
                },
                new()
                {
                    UrlSlug = "accessories-for-matrices-and-displays",
                    Image = "",
                    ParentId = 40,
                    CategoryTranslations = new List<CategoryTranslation>(){
                        new(){ Name = "Accessories for matrices and displays", LanguageId=LanguageId.English },
                        new(){ Name = "Комплектуючі для матриць та дисплеїв", LanguageId=LanguageId.Ukrainian } }
                },
                new()
                {
                    UrlSlug = "cameras-for-laptops",
                    Image = "",
                    ParentId = 40,
                    CategoryTranslations = new List<CategoryTranslation>(){
                        new(){ Name = "Cameras for laptops", LanguageId=LanguageId.English },
                        new(){ Name = "Камери для портативних ПК", LanguageId=LanguageId.Ukrainian } }
                },
                new()
                {
                    UrlSlug = "cooling-systems-for-laptops",
                    Image = "",
                    ParentId = 40,
                    CategoryTranslations = new List<CategoryTranslation>(){
                        new(){ Name = "Cooling systems for laptops", LanguageId=LanguageId.English },
                        new(){ Name = "Системи охолодження для ноутбуків", LanguageId=LanguageId.Ukrainian } }
                },
                new()
                {
                    UrlSlug = "tv-and-fm-tuners",
                    Image = "",
                    ParentId = 40,
                    CategoryTranslations = new List<CategoryTranslation>(){
                        new(){ Name = "TV and FM tuners", LanguageId=LanguageId.English },
                        new(){ Name = "TV-тюнери і FM-тюнери", LanguageId=LanguageId.Ukrainian } }
                },
                new()
                {
                    UrlSlug = "postcards",
                    Image = "",
                    ParentId = 40,
                    CategoryTranslations = new List<CategoryTranslation>(){
                        new(){ Name = "Postcards", LanguageId=LanguageId.English },
                        new(){ Name = "Post-карти", LanguageId=LanguageId.Ukrainian } }
                },
                new()
                {
                    UrlSlug = "accessories-for-routers",
                    Image = "",
                    ParentId = 40,
                    CategoryTranslations = new List<CategoryTranslation>(){
                        new(){ Name = "Accessories for routers", LanguageId=LanguageId.English },
                        new(){ Name = "Комплектуючі для маршрутизаторів", LanguageId=LanguageId.Ukrainian } }
                },
            };
            return categories;
        }

        static IEnumerable<Shop> GetPreconfiguredMarketplaceShops(string userId)
        {
            var shops = new List<Shop>
            {
                new(){ Name = "Mall",Description="",Photo="4918050.jpg",Email="dg646726@gmail.com",SiteUrl="https://mall.novakvova.com/",CityId=1,UserId=userId,
                ShopSchedule=new List<ShopScheduleItem>(){
                    new(){ DayOfWeekId=DayOfWeekId.Monday, Start=new DateTime(1,1,1,8,0,0), End=new DateTime(1,1,1,18,0,0) },
                    new(){ DayOfWeekId=DayOfWeekId.Tuesday, Start=new DateTime(1,1,1,8,0,0), End=new DateTime(1,1,1,18,0,0) },
                    new(){ DayOfWeekId=DayOfWeekId.Wednesday, Start=new DateTime(1,1,1,9,0,0), End=new DateTime(1,1,1,19,0,0) },
                    new(){ DayOfWeekId=DayOfWeekId.Thursday, Start=new DateTime(1,1,1,9,0,0), End=new DateTime(1,1,1,19,0,0) },
                    new(){ DayOfWeekId=DayOfWeekId.Friday, Start=new DateTime(1,1,1,8,0,0), End=new DateTime(1,1,1,18,0,0) },
                    new(){ DayOfWeekId=DayOfWeekId.Saturday, IsWeekend=true },
                    new(){ DayOfWeekId=DayOfWeekId.Sunday, IsWeekend=true },
                } }
            };
            return shops;
        }

        static IEnumerable<ProductStatus> GetPreconfiguredMarketplaceProductStatus()
        {
            var productStatuses = new List<ProductStatus>
            {
                  new(){ Id=ProductStatusId.InStock,
                         ProductStatusTranslations=new List<ProductStatusTranslation>(){
                            new() { LanguageId=LanguageId.English, Name = "In stock" },
                            new() { LanguageId=LanguageId.Ukrainian, Name="В наявності"} } },
                  new(){ Id=ProductStatusId.NotAvailable,
                         ProductStatusTranslations=new List<ProductStatusTranslation>(){
                            new() { LanguageId=LanguageId.English, Name = "Not available" },
                            new() { LanguageId=LanguageId.Ukrainian, Name="Не має в наявності"} } },

            };
            return productStatuses;
        }

        static IEnumerable<Product> GetPreconfiguredMarketplaceProducts()
        {
            var products = new List<Product>
            {
                new(){
                    Name = "Nike Festive dress Hex Wednesday Dress",
                    Description="",
                    Price=1000f,
                    Count=100,
                    ShopId=1,
                    StatusId=1,
                    CategoryId=21,
                    UrlSlug=Guid.NewGuid(),
                    SaleId=1,
                    Discount=10
                },
                new(){ Name = "Puma Festive dress Hex Wednesday Dress",
                    Description="",
                    Price=1500f,
                    Count=100,
                    ShopId=1,
                    StatusId=1,
                    CategoryId=21,
                    UrlSlug=Guid.NewGuid(),
                    SaleId=1,
                    Discount=15
                },
                new(){
                    Name = "Zara Festive dress Hex Wednesday Dress",
                    Description="",
                    Price=1700f,
                    Count=100,
                    ShopId=1,
                    StatusId=1,
                    CategoryId=21,
                    UrlSlug=Guid.NewGuid(),
                    SaleId=1,
                    Discount=25
                },
                new(){
                    Name = "H&M Festive dress Hex Wednesday Dress",
                    Description="",
                    Price=1500f,
                    Count=100,
                    ShopId=1,
                    StatusId=1,
                    CategoryId=21,
                    UrlSlug=Guid.NewGuid(),
                    SaleId=1,
                    Discount=10
                },
                new(){
                    Name = "AAA Festive dress Hex Wednesday Dress",
                    Description="",
                    Price=3500f,
                    Count=100,
                    ShopId=1,
                    StatusId=1,
                    CategoryId=21,
                    UrlSlug=Guid.NewGuid(),
                    SaleId=1,
                    Discount=35
                },
                new(){
                    Name = "Nike Festive dress Hex Wednesday Dress",
                    Description="",
                    Price=1000f,
                    Count=100,
                    ShopId=1,
                    StatusId=1,
                    CategoryId=21,
                    UrlSlug=Guid.NewGuid(),
                    SaleId=1,
                    Discount=25
                },
                new(){ Name = "AAA Festive dress Hex Wednesday Dress",Description="",Price=1000f,Count=100,ShopId=1,StatusId=1,CategoryId=21,UrlSlug=Guid.NewGuid() },
                new(){ Name = "Puma Festive dress Hex Wednesday Dress",Description="",Price=1000f,Count=100,ShopId=1,StatusId=1,CategoryId=21,UrlSlug=Guid.NewGuid() },
                new(){ Name = "H&M Festive dress Hex Wednesday Dress",Description="",Price=1000f,Count=100,ShopId=1,StatusId=1,CategoryId=21,UrlSlug=Guid.NewGuid() },
                new(){ Name = "Zara Festive dress Hex Wednesday Dress",Description="",Price=1000f,Count=100,ShopId=1,StatusId=1,CategoryId=21,UrlSlug=Guid.NewGuid() },
                new(){ Name = "Zara Festive dress Hex Wednesday Dress",Description="",Price=1000f,Count=100,ShopId=1,StatusId=1,CategoryId=21,UrlSlug=Guid.NewGuid() },
                new(){ Name = "AAA Festive dress Hex Wednesday Dress",Description="",Price=1000f,Count=100,ShopId=1,StatusId=1,CategoryId=21,UrlSlug=Guid.NewGuid() },
                new(){ Name = "Nike Festive dress Hex Wednesday Dress",Description="",Price=1000f,Count=100,ShopId=1,StatusId=1,CategoryId=21,UrlSlug=Guid.NewGuid() },
                new(){ Name = "H&M Festive dress Hex Wednesday Dress",Description="",Price=1000f,Count=100,ShopId=1,StatusId=1,CategoryId=21,UrlSlug=Guid.NewGuid() },
                new(){ Name = "Puma Festive dress Hex Wednesday Dress",Description="",Price=1000f,Count=100,ShopId=1,StatusId=1,CategoryId=21,UrlSlug=Guid.NewGuid() },
                new(){ Name = "Festive dress Hex Wednesday Dress",Description="",Price=1000f,Count=100,ShopId=1,StatusId=1,CategoryId=21,UrlSlug=Guid.NewGuid() },
                new(){ Name = "Festive dress Hex Wednesday Dress",Description="",Price=1000f,Count=100,ShopId=1,StatusId=1,CategoryId=21,UrlSlug=Guid.NewGuid() },
                new(){ Name = "Festive dress Hex Wednesday Dress",Description="",Price=1000f,Count=100,ShopId=1,StatusId=1,CategoryId=21,UrlSlug=Guid.NewGuid() },
                new(){ Name = "Festive dress Hex Wednesday Dress",Description="",Price=1000f,Count=100,ShopId=1,StatusId=1,CategoryId=21,UrlSlug=Guid.NewGuid() },
                new(){ Name = "Festive dress Hex Wednesday Dress",Description="",Price=1000f,Count=100,ShopId=1,StatusId=1,CategoryId=21,UrlSlug=Guid.NewGuid() },
                new(){ Name = "Festive dress Hex Wednesday Dress",Description="",Price=1000f,Count=100,ShopId=1,StatusId=1,CategoryId=21,UrlSlug=Guid.NewGuid() },
                new(){ Name = "Festive dress Hex Wednesday Dress",Description="",Price=1000f,Count=100,ShopId=1,StatusId=1,CategoryId=21,UrlSlug=Guid.NewGuid() },
                new(){ Name = "Festive dress Hex Wednesday Dress",Description="",Price=1000f,Count=100,ShopId=1,StatusId=1,CategoryId=21,UrlSlug=Guid.NewGuid() },
                new(){ Name = "Festive dress Hex Wednesday Dress",Description="",Price=1000f,Count=100,ShopId=1,StatusId=1,CategoryId=21,UrlSlug=Guid.NewGuid() },
                new(){ Name = "Festive dress Hex Wednesday Dress",Description="",Price=1000f,Count=100,ShopId=1,StatusId=1,CategoryId=21,UrlSlug=Guid.NewGuid() },
                new(){ Name = "Festive dress Hex Wednesday Dress",Description="",Price=1000f,Count=100,ShopId=1,StatusId=1,CategoryId=21,UrlSlug=Guid.NewGuid() },
                new(){ Name = "Festive dress Hex Wednesday Dress",Description="",Price=1000f,Count=100,ShopId=1,StatusId=1,CategoryId=21,UrlSlug=Guid.NewGuid() },
                new(){ Name = "Festive dress Hex Wednesday Dress",Description="",Price=1000f,Count=100,ShopId=1,StatusId=1,CategoryId=21,UrlSlug=Guid.NewGuid() },
                new(){ Name = "Festive dress Hex Wednesday Dress",Description="",Price=1000f,Count=100,ShopId=1,StatusId=1,CategoryId=21,UrlSlug=Guid.NewGuid() },
                new(){ Name = "Festive dress Hex Wednesday Dress",Description="",Price=1000f,Count=100,ShopId=1,StatusId=1,CategoryId=21,UrlSlug=Guid.NewGuid() },
                new(){ Name = "Festive dress Hex Wednesday Dress",Description="",Price=1000f,Count=100,ShopId=1,StatusId=1,CategoryId=21,UrlSlug=Guid.NewGuid() },
                new(){ Name = "Festive dress Hex Wednesday Dress",Description="",Price=1000f,Count=100,ShopId=1,StatusId=1,CategoryId=21,UrlSlug=Guid.NewGuid() },
                new(){ Name = "Festive dress Hex Wednesday Dress",Description="",Price=1000f,Count=100,ShopId=1,StatusId=1,CategoryId=21,UrlSlug=Guid.NewGuid() },
                new(){ Name = "Festive dress Hex Wednesday Dress",Description="",Price=1000f,Count=100,ShopId=1,StatusId=1,CategoryId=21,UrlSlug=Guid.NewGuid() },
                new(){ Name = "Festive dress Hex Wednesday Dress",Description="",Price=1000f,Count=100,ShopId=1,StatusId=1,CategoryId=21,UrlSlug=Guid.NewGuid() },
                new(){ Name = "Festive dress Hex Wednesday Dress",Description="",Price=1000f,Count=100,ShopId=1,StatusId=1,CategoryId=21,UrlSlug=Guid.NewGuid() },
                new(){ Name = "Festive dress Hex Wednesday Dress",Description="",Price=1000f,Count=100,ShopId=1,StatusId=1,CategoryId=21,UrlSlug=Guid.NewGuid() },
                new(){ Name = "Festive dress Hex Wednesday Dress",Description="",Price=1000f,Count=100,ShopId=1,StatusId=1,CategoryId=21,UrlSlug=Guid.NewGuid() },
                new(){ Name = "Festive dress Hex Wednesday Dress",Description="",Price=1000f,Count=100,ShopId=1,StatusId=1,CategoryId=21,UrlSlug=Guid.NewGuid() },
                new(){ Name = "Festive dress Hex Wednesday Dress",Description="",Price=1000f,Count=100,ShopId=1,StatusId=1,CategoryId=21,UrlSlug=Guid.NewGuid() },
                new(){ Name = "Festive dress Hex Wednesday Dress",Description="",Price=1000f,Count=100,ShopId=1,StatusId=1,CategoryId=21,UrlSlug=Guid.NewGuid() },
                new(){ Name = "Festive dress Hex Wednesday Dress",Description="",Price=1000f,Count=100,ShopId=1,StatusId=1,CategoryId=21,UrlSlug=Guid.NewGuid() },
                new(){ Name = "Festive dress Hex Wednesday Dress",Description="",Price=1000f,Count=100,ShopId=1,StatusId=1,CategoryId=21,UrlSlug=Guid.NewGuid() },
                new(){ Name = "Festive dress Hex Wednesday Dress",Description="",Price=1000f,Count=100,ShopId=1,StatusId=1,CategoryId=21,UrlSlug=Guid.NewGuid() },
                new(){ Name = "Festive dress Hex Wednesday Dress",Description="",Price=1000f,Count=100,ShopId=1,StatusId=1,CategoryId=21,UrlSlug=Guid.NewGuid() },
                new(){ Name = "Festive dress Hex Wednesday Dress",Description="",Price=1000f,Count=100,ShopId=1,StatusId=1,CategoryId=21,UrlSlug=Guid.NewGuid() },
                new(){ Name = "Festive dress Hex Wednesday Dress",Description="",Price=1000f,Count=100,ShopId=1,StatusId=1,CategoryId=21,UrlSlug=Guid.NewGuid() },
                new(){ Name = "Festive dress Hex Wednesday Dress",Description="",Price=1000f,Count=100,ShopId=1,StatusId=1,CategoryId=21,UrlSlug=Guid.NewGuid() },
                new(){ Name = "Festive dress Hex Wednesday Dress",Description="",Price=1000f,Count=100,ShopId=1,StatusId=1,CategoryId=21,UrlSlug=Guid.NewGuid() },
                new(){ Name = "Festive dress Hex Wednesday Dress",Description="",Price=1000f,Count=100,ShopId=1,StatusId=1,CategoryId=21,UrlSlug=Guid.NewGuid() },
                new(){ Name = "Festive dress Hex Wednesday Dress",Description="",Price=1000f,Count=100,ShopId=1,StatusId=1,CategoryId=21,UrlSlug=Guid.NewGuid() },
            };
            return products;
        }
        static IEnumerable<ProductImage> GetPreconfiguredMarketplaceProductImages()
        {
            var productImages = new List<ProductImage>
            {
                new(){ Name = "BlackDress.jpg",ProductId=1 },
                new(){ Name = "BlueDress2.jpg",ProductId=2 },
                new(){ Name = "BlueDress.jpg",ProductId=2 },
                new(){ Name = "GreenDress.jpg",ProductId=3 },
                new(){ Name = "RedDress.jpg",ProductId=4 },
                new(){ Name = "YellowDress2.jpg",ProductId=5 },
                new(){ Name = "YellowDress.jpg",ProductId=5 },
                new(){ Name = "YellowDress3.jpg",ProductId=5 },

                new(){ Name = "BlueDress.jpg",ProductId=6 },
                new(){ Name = "BlueDress2.jpg",ProductId=6 },
                new(){ Name = "YellowDress3.jpg",ProductId=7 },
                new(){ Name = "YellowDress.jpg",ProductId=7 },
                new(){ Name = "YellowDress2.jpg",ProductId=7 },
                new(){ Name = "RedDress.jpg",ProductId=8 },
                new(){ Name = "GreenDress.jpg",ProductId=9 },
                new(){ Name = "BlackDress.jpg",ProductId=10 },

                new(){ Name = "GreenDress.jpg",ProductId=11 },
                new(){ Name = "YellowDress.jpg",ProductId=12 },
                new(){ Name = "YellowDress2.jpg",ProductId=12 },
                new(){ Name = "YellowDress3.jpg",ProductId=12 },
                new(){ Name = "BlackDress.jpg",ProductId=13 },
                new(){ Name = "RedDress.jpg",ProductId=14 },
                new(){ Name = "BlueDress.jpg",ProductId=15 },
                new(){ Name = "BlueDress2.jpg",ProductId=15 },
            };
            return productImages;
        }
        static IEnumerable<Unit> GetPreconfiguredMarketplaceUnits()
        {
            var units = new List<Unit>
            {
/* 1 */         new(){ UnitTranslations= new List<UnitTranslation>(){
                        new (){LanguageId=LanguageId.English, Measure = "UA" },
                        new (){LanguageId=LanguageId.Ukrainian, Measure="UA"} } },
            };
            return units;
        }
        static IEnumerable<FilterGroup> GetPreconfiguredMarketplaceFilterGroups()
        {
            var filterGroups = new List<FilterGroup>
            {
/* 1 */         new(){FilterGroupTranslations= new List<FilterGroupTranslation>(){
                        new (){LanguageId=LanguageId.English, Name = "The main" },
                        new (){LanguageId=LanguageId.Ukrainian, Name="Основна"} } },

            };
            return filterGroups;
        }
        static IEnumerable<FilterName> GetPreconfiguredMarketplaceFilterNames()
        {
            var filterNames = new List<FilterName>
            {
/* 1 */         new(){ FilterGroupId=1, FilterNameTranslations=new List<FilterNameTranslation>(){
                    new() { LanguageId=LanguageId.English, Name = "Сondition"} ,
                    new() {LanguageId=LanguageId.Ukrainian, Name="Стан" } } },
/* 2 */         new(){ FilterGroupId=1, FilterNameTranslations=new List<FilterNameTranslation>(){
                    new() { LanguageId=LanguageId.English, Name = "Purpose" } ,
                    new() {LanguageId=LanguageId.Ukrainian, Name="Призначення" } } },
/* 3 */         new(){ FilterGroupId=1, FilterNameTranslations=new List<FilterNameTranslation>(){
                    new() { LanguageId=LanguageId.English, Name = "Video memory type" } ,
                    new() {LanguageId=LanguageId.Ukrainian, Name="Тип відеопам'яті" } } },
/* 4 */         new(){ FilterGroupId=1, FilterNameTranslations=new List<FilterNameTranslation>(){
                    new() { LanguageId=LanguageId.English, Name = "Graphics chipset"} ,
                    new() {LanguageId=LanguageId.Ukrainian, Name="Графічний чіпсет" } } },
/* 5 */         new(){ FilterGroupId = 1, FilterNameTranslations=new List<FilterNameTranslation>(){
                    new() { LanguageId=LanguageId.English, Name = "Memory bus width" } ,
                    new() {LanguageId=LanguageId.Ukrainian, Name="Ширина шини пам'яті" } } },
/* 6 */         new(){ FilterGroupId = 1, FilterNameTranslations=new List<FilterNameTranslation>(){
                    new() { LanguageId=LanguageId.English, Name = "Producer" } ,
                    new() {LanguageId=LanguageId.Ukrainian, Name="Виробник" } } },
/* 7 */         new(){ FilterGroupId = 1, FilterNameTranslations=new List<FilterNameTranslation>(){
                    new() { LanguageId=LanguageId.English, Name = "Connection type" } ,
                    new() {LanguageId=LanguageId.Ukrainian, Name="Тип підключення" } } },
/* 8 */         new(){ FilterGroupId = 1, FilterNameTranslations = new List < FilterNameTranslation >() {
                    new() { LanguageId = LanguageId.English, Name = "Interfaces" } ,
                    new() {LanguageId=LanguageId.Ukrainian, Name="Інтерфейси" } } },
/* 9 */         new(){ FilterGroupId = 1, FilterNameTranslations=new List<FilterNameTranslation>(){
                    new() { LanguageId=LanguageId.English, Name = "Cooling system" } ,
                    new() {LanguageId=LanguageId.Ukrainian, Name="Система охолодження" } } },
/* 10 */        new(){ FilterGroupId = 1, FilterNameTranslations=new List<FilterNameTranslation>(){
                    new() { LanguageId=LanguageId.English, Name = "Peculiarities" } ,
                    new() {LanguageId=LanguageId.Ukrainian, Name="Особливості" } } },
/* 11 */        new(){ FilterGroupId = 1, FilterNameTranslations=new List<FilterNameTranslation>(){
                    new() { LanguageId=LanguageId.English, Name = "Producing country"} ,
                    new() {LanguageId=LanguageId.Ukrainian, Name="Країна-виробник" } } },
/* 12 */        new(){ FilterGroupId = 1, FilterNameTranslations=new List<FilterNameTranslation>(){
                    new() { LanguageId=LanguageId.English, Name = "Quality class" } ,
                    new() {LanguageId=LanguageId.Ukrainian, Name="Клас якості" } } },
/* 13 */        new(){ FilterGroupId = 1, FilterNameTranslations=new List<FilterNameTranslation>(){
                    new() { LanguageId=LanguageId.English, Name = "Warranty period, months" } ,
                    new() {LanguageId=LanguageId.Ukrainian, Name="Гарантійний термін, міс" } } },
/* 14 */        new(){ FilterGroupId = 1, FilterNameTranslations=new List<FilterNameTranslation>(){
                    new() { LanguageId=LanguageId.English, Name = "Processor frequency, MHz" } ,
                    new() {LanguageId=LanguageId.Ukrainian, Name="Частота процесора, МГц" } } },
/* 15 */        new(){ FilterGroupId = 1, FilterNameTranslations=new List<FilterNameTranslation>(){
                    new() { LanguageId=LanguageId.English, Name = "Video memory frequency, MHz" } ,
                    new() {LanguageId=LanguageId.Ukrainian, Name="Частота відеопам'яті, МГц" } } },
/* 16 */        new(){ FilterGroupId = 1, FilterNameTranslations=new List<FilterNameTranslation>(){
                    new() { LanguageId=LanguageId.English, Name = "Video memory size, MB" } ,
                    new() {LanguageId=LanguageId.Ukrainian, Name="Обсяг відеопам'яті, Мб" } } },
/* 17 */        new(){ FilterGroupId=1, FilterNameTranslations=new List<FilterNameTranslation>(){
                    new() { LanguageId=LanguageId.English, Name = "Color"} ,
                    new() {LanguageId=LanguageId.Ukrainian, Name="Колір" } } },
/* 18 */        new(){ FilterGroupId=1, UnitId=1, FilterNameTranslations=new List<FilterNameTranslation>(){
                    new() { LanguageId=LanguageId.English, Name = "Women's clothing size" } ,
                    new() {LanguageId=LanguageId.Ukrainian, Name="Розмір жіночого одягу" } } },
/* 19 */        new(){ FilterGroupId=1, FilterNameTranslations=new List<FilterNameTranslation>(){
                    new() { LanguageId=LanguageId.English, Name = "Brand" } ,
                    new() {LanguageId=LanguageId.Ukrainian, Name="Бренд" } } },

            };
            return filterNames;
        }

        static IEnumerable<FilterValue> GetPreconfiguredMarketplaceFilterValues(Category category)
        {
            var filterValues = new List<FilterValue>
            {
/* 1 */         new(){ FilterNameId=17, FilterValueTranslations=new List<FilterValueTranslation>(){
                    new(){ LanguageId=LanguageId.English, Value = "Yellow" },
                    new(){ LanguageId=LanguageId.Ukrainian, Value="Жовтий "} } },
/* 2 */         new(){ FilterNameId=17, FilterValueTranslations=new List<FilterValueTranslation>(){
                    new(){ LanguageId=LanguageId.English, Value = "Black" },
                    new(){ LanguageId=LanguageId.Ukrainian, Value="Чорний"} } },
/* 3 */         new(){ FilterNameId=17, FilterValueTranslations=new List<FilterValueTranslation>(){
                    new(){ LanguageId=LanguageId.English, Value = "Blue" },
                    new(){ LanguageId=LanguageId.Ukrainian, Value="Синій"} } },
/* 4 */         new(){ FilterNameId=17, FilterValueTranslations=new List<FilterValueTranslation>(){
                    new(){ LanguageId=LanguageId.English, Value = "Red" },
                    new(){ LanguageId=LanguageId.Ukrainian, Value="Червоний"} } },
/* 5 */         new(){ FilterNameId = 17, FilterValueTranslations=new List<FilterValueTranslation>(){
                    new(){ LanguageId=LanguageId.English, Value = "Green" },
                    new(){ LanguageId=LanguageId.Ukrainian, Value="Зелений"} } },
/* 6 */         new(){ FilterNameId = 18, FilterValueTranslations=new List<FilterValueTranslation>(){
                    new(){ LanguageId=LanguageId.English, Value = "34" },
                    new(){ LanguageId=LanguageId.Ukrainian, Value="34"} } },
/* 7 */         new(){ FilterNameId = 18, FilterValueTranslations=new List<FilterValueTranslation>(){
                    new(){ LanguageId=LanguageId.English, Value = "36" },
                    new(){ LanguageId=LanguageId.Ukrainian, Value="36"} } },
/* 8 */         new(){ FilterNameId = 18, FilterValueTranslations=new List<FilterValueTranslation>(){
                    new(){ LanguageId=LanguageId.English, Value = "38" },
                    new(){ LanguageId=LanguageId.Ukrainian, Value="38"} } },
/* 9 */         new(){ FilterNameId = 18, FilterValueTranslations=new List<FilterValueTranslation>(){
                    new(){ LanguageId=LanguageId.English, Value = "40" },
                    new(){ LanguageId=LanguageId.Ukrainian, Value="40"} } },
/* 10 */        new(){ FilterNameId = 18, FilterValueTranslations=new List<FilterValueTranslation>(){
                    new(){ LanguageId=LanguageId.English, Value = "40/42" },
                    new(){ LanguageId=LanguageId.Ukrainian, Value="40/42"} } },
/* 11 */        new(){ FilterNameId = 18, FilterValueTranslations=new List<FilterValueTranslation>(){
                    new(){ LanguageId=LanguageId.English, Value = "40/44" },
                    new(){ LanguageId=LanguageId.Ukrainian, Value="40/44"} } },
/* 12 */        new(){ FilterNameId = 19, FilterValueTranslations=new List<FilterValueTranslation>(){
                    new(){ LanguageId=LanguageId.English, Value = "Nike" },
                    new(){ LanguageId=LanguageId.Ukrainian, Value="Nike"} } },
/* 13 */        new(){ FilterNameId = 19, FilterValueTranslations=new List<FilterValueTranslation>(){
                    new(){ LanguageId=LanguageId.English, Value = "Puma" },
                    new(){ LanguageId=LanguageId.Ukrainian, Value="Puma"} } },
/* 14 */        new(){ FilterNameId = 19, FilterValueTranslations=new List<FilterValueTranslation>(){
                    new(){ LanguageId=LanguageId.English, Value = "Zara"},
                    new(){ LanguageId=LanguageId.Ukrainian, Value="Zara"} } },
/* 15 */        new(){ FilterNameId = 19, FilterValueTranslations=new List<FilterValueTranslation>(){
                    new(){ LanguageId=LanguageId.English, Value = "H&M" },
                    new(){ LanguageId=LanguageId.Ukrainian, Value="H&M"} } },
/* 16 */        new(){ FilterNameId = 19, FilterValueTranslations=new List<FilterValueTranslation>(){
                    new(){ LanguageId=LanguageId.English, Value = "AAA"},
                    new(){ LanguageId=LanguageId.Ukrainian, Value="AAA"} } },
            };

            var test = new List<Category>
            {
                category
            };
            foreach (var item in filterValues)
            {
                item.Categories = test;
            }
            return filterValues;
        }

        static IEnumerable<FilterValueProduct> GetPreconfiguredMarketplaceFilterValueProducts()
        {
            var filterValueProducts = new List<FilterValueProduct>
            {
/* 1 */         new(){ FilterValueId = 2, ProductId=1},
/* 2 */         new(){ FilterValueId = 12, ProductId=1},

/* 3 */         new(){ FilterValueId = 3, ProductId=2},
/* 4 */         new(){ FilterValueId = 13, ProductId=2},

/* 5 */         new(){ FilterValueId = 5, ProductId=3},
/* 6 */         new(){ FilterValueId = 14, ProductId=3},

/* 7 */         new(){ FilterValueId = 4, ProductId=4},
/* 8 */         new(){ FilterValueId = 15, ProductId=4},

/* 9 */         new(){ FilterValueId = 1, ProductId=5},
/* 10 */        new(){ FilterValueId = 16, ProductId=5},

/* 11 */        new(){ FilterValueId = 3, ProductId=6},
/* 12 */        new(){ FilterValueId = 12, ProductId=6},
   
/* 13 */        new(){ FilterValueId = 1, ProductId=7},
/* 14 */        new(){ FilterValueId = 16, ProductId=7},
   
/* 15 */        new(){ FilterValueId = 4, ProductId=8},
/* 16 */        new(){ FilterValueId = 13, ProductId=8},
   
/* 17 */        new(){ FilterValueId = 5, ProductId=9},
/* 18 */        new(){ FilterValueId = 15, ProductId=9},
   
/* 19 */        new(){ FilterValueId = 2, ProductId=10},
/* 20 */        new(){ FilterValueId = 14, ProductId=10},
           
/* 21 */        new(){ FilterValueId = 5, ProductId=11},
/* 22 */        new(){ FilterValueId = 14, ProductId=11},
           
/* 23 */        new(){ FilterValueId = 1, ProductId=12},
/* 24 */        new(){ FilterValueId = 16, ProductId=12},
           
/* 25 */        new(){ FilterValueId = 2, ProductId=13},
/* 26 */        new(){ FilterValueId = 12, ProductId=13},
           
/* 27 */        new(){ FilterValueId = 4, ProductId=14},
/* 28 */        new(){ FilterValueId = 15, ProductId=14},
           
/* 29 */        new(){ FilterValueId = 3, ProductId=15},
/* 30 */        new(){ FilterValueId = 13, ProductId=15},

            };
            return filterValueProducts;
        }


        static IEnumerable<OrderStatus> GetPreconfiguredMarketplaceOrderStatuses()
        {
            var statuses = new List<OrderStatus>
            {
                  new(){ Id=OrderStatusId.InProcess, OrderStatusTranslations=new List<OrderStatusTranslation>(){
                        new (){ LanguageId=LanguageId.English, Name="In Process" },
                        new (){LanguageId=LanguageId.Ukrainian, Name="В процесі"} } },
                  new(){ Id=OrderStatusId.PendingPayment, OrderStatusTranslations=new List<OrderStatusTranslation>(){
                        new (){ LanguageId=LanguageId.English, Name="Pending Payment"},
                        new (){LanguageId=LanguageId.Ukrainian, Name="Очікування платежу"} } },
                  new(){ Id=OrderStatusId.Completed, OrderStatusTranslations=new List<OrderStatusTranslation>(){
                        new (){ LanguageId=LanguageId.English, Name="Completed"},
                        new (){LanguageId=LanguageId.Ukrainian, Name="Виконано"} } },
                  new(){ Id=OrderStatusId.Canceled, OrderStatusTranslations=new List<OrderStatusTranslation>(){
                        new (){ LanguageId=LanguageId.English, Name="Canceled",},
                        new (){LanguageId=LanguageId.Ukrainian, Name="Скасовано"} } },
            };
            return statuses;
        }

        static IEnumerable<Language> GetPreconfiguredMarketplaceLanguages()
        {
            var statuses = new List<Language>
            {
             new(){ Id=LanguageId.Ukrainian, Name="Ukrainian", Culture=LanguageCulture.Ukrainian},
             new(){ Id=LanguageId.English, Name="English", Culture=LanguageCulture.English},
            };
            return statuses;
        }

        static IEnumerable<DayOfWeek> GetPreconfiguredMarketplaceDayOfWeeks()
        {
            var daysOfWeek = new List<DayOfWeek>
            {
                  new(){ Id=DayOfWeekId.Monday, DayOfWeekTranslations=new List<DayOfWeekTranslation>(){
                        new (){ LanguageId=LanguageId.English, Name="Monday", ShortName="Mo"},
                        new (){ LanguageId=LanguageId.Ukrainian, Name="Понеділок", ShortName="Пн"} } },
                  new(){ Id=DayOfWeekId.Tuesday, DayOfWeekTranslations=new List<DayOfWeekTranslation>(){
                        new (){ LanguageId=LanguageId.English, Name="Tuesday", ShortName="Tu"},
                        new (){ LanguageId=LanguageId.Ukrainian, Name="Вівторок", ShortName="Вт"} } },
                  new(){ Id=DayOfWeekId.Wednesday, DayOfWeekTranslations=new List<DayOfWeekTranslation>(){
                        new (){ LanguageId=LanguageId.English, Name="Wednesday", ShortName="We"},
                        new (){ LanguageId=LanguageId.Ukrainian, Name="Середа", ShortName="Ср"} } },
                  new(){ Id=DayOfWeekId.Thursday, DayOfWeekTranslations=new List<DayOfWeekTranslation>(){
                        new (){ LanguageId=LanguageId.English, Name="Thursday", ShortName="Th"},
                        new (){ LanguageId=LanguageId.Ukrainian, Name="Четвер", ShortName="Чт"} } },
                  new(){ Id=DayOfWeekId.Friday, DayOfWeekTranslations=new List<DayOfWeekTranslation>(){
                        new (){ LanguageId=LanguageId.English, Name="Friday", ShortName="Fr"},
                        new (){ LanguageId=LanguageId.Ukrainian, Name="П'ятниця", ShortName="Пт"} } },
                  new(){ Id=DayOfWeekId.Saturday, DayOfWeekTranslations=new List<DayOfWeekTranslation>(){
                        new (){ LanguageId=LanguageId.English, Name="Saturday", ShortName="Sa"},
                        new (){ LanguageId=LanguageId.Ukrainian, Name="Субота", ShortName="Сб"} } },
                  new(){ Id=DayOfWeekId.Sunday, DayOfWeekTranslations=new List<DayOfWeekTranslation>(){
                        new (){ LanguageId=LanguageId.English, Name="Sunday", ShortName="Su"},
                        new (){ LanguageId=LanguageId.Ukrainian, Name="Неділя", ShortName="Нд"} } },
            };

            return daysOfWeek;
        }

        static IEnumerable<DeliveryType> GetPreconfiguredMarketplaceDeliveryTypes()
        {
            var deliveryTypes = new List<DeliveryType>
            {
                  new(){ Id=1, DarkIcon="",LightIcon="", DeliveryTypeTranslations=new List<DeliveryTypeTranslation>(){
                        new (){ LanguageId=LanguageId.English, Name="Nova Poshta"},
                        new (){ LanguageId=LanguageId.Ukrainian, Name="Нова пошта"} } }
            };

            return deliveryTypes;
        }

        static IEnumerable<Gender> GetPreconfiguredMarketplaceGenders()
        {
            var genders = new List<Gender>
            {
                  new(){  GenderTranslations=new List<GenderTranslation>(){
                        new (){ LanguageId=LanguageId.English, Name="Man"},
                        new (){ LanguageId=LanguageId.Ukrainian, Name="Чоловіча"} } },
                  new(){  GenderTranslations=new List<GenderTranslation>(){
                        new (){ LanguageId=LanguageId.English, Name="Woman"},
                        new (){ LanguageId=LanguageId.Ukrainian, Name="Жіноча"} } },
                  new(){  GenderTranslations=new List<GenderTranslation>(){
                        new (){ LanguageId=LanguageId.English, Name="Non-binary"},
                        new (){ LanguageId=LanguageId.Ukrainian, Name="Небінарна"} } },
            };

            return genders;
        }

    }
}
