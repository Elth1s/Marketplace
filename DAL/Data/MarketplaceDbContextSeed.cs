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

            var defaultUser1 = new AppUser
            {
                UserName = UsersInfo.DefaultUserNameFirst,
                FirstName = UsersInfo.DefaultUserNameFirst,
                SecondName = UsersInfo.DefaultUserSurnameFirst,
                Email = UsersInfo.DefaultEmailFirst
            };
            await userManager.CreateAsync(defaultUser1, UsersInfo.DefaultPassword);
            defaultUser1 = await userManager.FindByEmailAsync(UsersInfo.DefaultEmailFirst);

            var defaultUser2 = new AppUser
            {
                UserName = UsersInfo.DefaultUserNameSecond,
                SecondName = UsersInfo.DefaultUserSurnameSecond,
                FirstName = UsersInfo.DefaultUserNameSecond,
                Email = UsersInfo.DefaultEmailSecond
            };
            await userManager.CreateAsync(defaultUser2, UsersInfo.DefaultPassword);
            defaultUser2 = await userManager.FindByEmailAsync(UsersInfo.DefaultEmailFirst);

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
            var WomensClothes = marketplaceDbContext.Categories.Where(c => c.Id == 58).FirstOrDefault();

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

            var deliveryType = marketplaceDbContext.DeliveryTypes.ToList();
            if (!await marketplaceDbContext.Shops.AnyAsync())
            {
                await marketplaceDbContext.Shops.AddRangeAsync(
                  GetPreconfiguredMarketplaceShops(defaultUser1.Id, defaultUser2.Id, deliveryType));

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

        static IEnumerable<Category> GetPreconfiguredMarketplaceCategories(Sale clothesAndShoesSale, Sale laptopSale)
        {
            var categories = new List<Category>
            {
 
/*1*/              new(){ UrlSlug = "technology-and-electronics",
                           Image = "TechnologyAndElectronics.png",
                           LightIcon="TechnologyAndElectronicsLight.png",
                           DarkIcon="TechnologyAndElectronicsDark.png",
                           ActiveIcon="TechnologyAndElectronicsActive.png",
                           ParentId = null,
                           CategoryTranslations=new List<CategoryTranslation>(){
                                new (){ Name = "Technology and electronics", LanguageId=LanguageId.English },
                                new (){ Name = "Техніка та електроніка", LanguageId=LanguageId.Ukrainian }
                           }
                    },
/*2*/              new(){ UrlSlug = "сonstruction",
                           Image = "Construction.png",
                           LightIcon = "ConstructionLight.png",
                           DarkIcon = "ConstructionDark.png",
                           ActiveIcon = "ConstructionActive.png",
                           ParentId = null,
                           CategoryTranslations = new List<CategoryTranslation>(){
                               new(){ Name = "Construction", LanguageId=LanguageId.English },
                               new(){ Name = "Будівництво", LanguageId=LanguageId.Ukrainian }
                           }
                    },
/*3*/              new(){ UrlSlug = "house-and-garden",
                           Image = "HouseAndGarden.png",
                           LightIcon="HouseAndGardenLight.png",DarkIcon="HouseAndGardenDark.png",
                           ActiveIcon="HouseAndGardenActive.png",
                           ParentId = null,
                            CategoryTranslations=new List<CategoryTranslation>(){
                               new(){ Name = "House and garden",  LanguageId=LanguageId.English },
                               new(){ Name = "Дім і сад", LanguageId=LanguageId.Ukrainian }
                            }
                    },
/*4*/              new(){ UrlSlug = "sports-and-recreation",
                           Image = "SportsAndRecreation.png",
                           LightIcon = "SportsAndRecreationLight.png",
                           DarkIcon = "SportsAndRecreationDark.png",
                           ActiveIcon = "SportsAndRecreationActive.png",
                           ParentId = null,
                           CategoryTranslations = new List<CategoryTranslation>(){
                               new(){ Name = "Sports and recreation", LanguageId=LanguageId.English },
                               new(){ Name = "Спорт і відпочинок", LanguageId=LanguageId.Ukrainian }
                           }
                    },
/*5*/              new(){ UrlSlug = "makeup",
                           Image = "Makeup.png",
                           LightIcon="MakeupLight.png",
                           DarkIcon="MakeupDark.png",
                           ActiveIcon="MakeupActive.png",
                           ParentId = null,
                            CategoryTranslations=new List<CategoryTranslation>(){
                               new(){ Name = "Makeup", LanguageId=LanguageId.English },
                               new(){ Name = "Косметика", LanguageId=LanguageId.Ukrainian }
                            }
                    },
/*6*/              new(){ UrlSlug = "goods-for-children",
                           Image = "GoodsForChildren.png",
                           LightIcon = "GoodsForChildrenLight.png",
                           DarkIcon = "GoodsForChildrenDark.png",
                           ActiveIcon = "GoodsForChildrenActive.png",
                           ParentId = null,
                           CategoryTranslations = new List<CategoryTranslation>(){
                               new(){ Name = "Goods for children", LanguageId=LanguageId.English },
                               new(){ Name = "Товари для дітей", LanguageId=LanguageId.Ukrainian }
                           }
                    },
/*7*/              new(){ UrlSlug = "clothes",
                           Image = "Clothes.png",
                           LightIcon="ClothesLight.png",
                           DarkIcon="ClothesDark.png",
                           ActiveIcon="ClothesActive.png",
                           ParentId = null,
                           CategoryTranslations = new List<CategoryTranslation>(){
                               new(){ Name = "Clothes", LanguageId=LanguageId.English },
                               new(){ Name = "Одяг", LanguageId=LanguageId.Ukrainian }
                           }
                    },
/*8*/              new(){ UrlSlug = "shoes",
                           Image = "Shoes.png",
                           LightIcon="ShoesLight.png",
                           DarkIcon="ShoesDark.png",
                           ActiveIcon="ShoesActive.png",
                           ParentId = null,
                            CategoryTranslations = new List<CategoryTranslation>(){
                               new(){ Name = "Shoes", LanguageId=LanguageId.English },
                               new(){ Name = "Взуття", LanguageId=LanguageId.Ukrainian }
                            }
                    },
/*9*/              new(){ UrlSlug = "stationery",
                           Image = "Stationery.png",
                           LightIcon = "StationeryLight.png",
                           DarkIcon = "StationeryDark.png",
                           ActiveIcon = "StationeryActive.png",
                           ParentId = null,
                           CategoryTranslations = new List<CategoryTranslation>(){
                               new(){ Name = "Stationery",  LanguageId=LanguageId.English },
                               new(){ Name = "Канцтовари", LanguageId=LanguageId.Ukrainian }
                           }
                    },
/*10*/             new(){ UrlSlug = "tools",
                            Image = "Tools.png",
                            LightIcon = "ToolsLight.png",
                            DarkIcon = "ToolsDark.png",
                            ActiveIcon = "ToolsActive.png",
                            ParentId = null,
                            CategoryTranslations = new List<CategoryTranslation>(){
                                new(){ Name = "Tools", LanguageId=LanguageId.English },
                                new(){ Name = "Інструменти", LanguageId=LanguageId.Ukrainian }
                            }
                    },
/*11*/             new(){ UrlSlug = "gifts",
                           Image = "Gifts.png",
                           LightIcon = "GiftsLight.png",
                           DarkIcon = "GiftsDark.png",
                           ActiveIcon = "GiftsActive.png",
                           ParentId = null,
                           CategoryTranslations = new List<CategoryTranslation>(){
                               new(){ Name = "Gifts", LanguageId=LanguageId.English },
                               new(){ Name = "Подарунки", LanguageId=LanguageId.Ukrainian }
                           }
                    },
/*12*/             new(){ UrlSlug = "accessories-and-decorations",
                           Image = "AccessoriesAndDecorations.png",
                           LightIcon = "AccessoriesAndDecorationsLight.png",
                           DarkIcon = "AccessoriesAndDecorationsDark.png",
                           ActiveIcon = "AccessoriesAndDecorationsActive.png",
                           ParentId = null,
                           CategoryTranslations = new List<CategoryTranslation>(){
                               new(){ Name = "Accessories and decorations", LanguageId=LanguageId.English },
                               new(){ Name = "Аксесуари та прикраси", LanguageId=LanguageId.Ukrainian }
                           }
                    },


/*-1- 13*/         new(){ UrlSlug = "computer-equipment-and-software",
                           Image = "ComputerEquipmentAndSoftware.png",
                           ParentId = 1,
                           CategoryTranslations = new List<CategoryTranslation>(){
                               new(){ Name = "Computer equipment and software", LanguageId=LanguageId.English },
                               new(){ Name = "Комп'ютерна техніка та ПЗ", LanguageId=LanguageId.Ukrainian }
                           }
                    },
/*-1- 14*/         new(){ UrlSlug = "household-appliances",
                           Image = "HouseholdAppliances.png",
                           ParentId = 1,
                           CategoryTranslations = new List<CategoryTranslation>(){
                               new(){ Name = "Household appliances", LanguageId=LanguageId.English },
                               new(){ Name = "Побутова техніка", LanguageId=LanguageId.Ukrainian }
                           }
                    },
/*-1- 15*/         new(){ UrlSlug = "phones-and-accessories",
                           Image = "PhonesAndAccessories.png",
                           ParentId = 1,
                           CategoryTranslations = new List<CategoryTranslation>(){
                               new(){ Name = "Phones and accessories", LanguageId=LanguageId.English },
                               new(){ Name = "Телефони та аксесуари", LanguageId=LanguageId.Ukrainian }
                           }
                    },
/*-1- 16*/         new(){ UrlSlug = "audio-equipment",
                           Image = "AudioEquipment.png",
                           ParentId = 1,
                           CategoryTranslations = new List<CategoryTranslation>(){
                               new(){ Name = "Audio equipment", LanguageId=LanguageId.English },
                               new(){ Name = "Аудіотехніка", LanguageId=LanguageId.Ukrainian }
                           }
                    },
/*-1- 17*/         new(){ UrlSlug = "tv-and-video-equipment",
                           Image = "TV.png",
                           ParentId = 1,
                           CategoryTranslations = new List<CategoryTranslation>(){
                               new(){  Name = "TV and video equipment", LanguageId=LanguageId.English },
                               new(){ Name = "TV та відеотехніка", LanguageId=LanguageId.Ukrainian }
                           }
                    },
/*-1- 18*/         new(){ UrlSlug = "car-electronics",
                           Image = "CarElectronics.png",
                           ParentId = 1,
                           CategoryTranslations = new List<CategoryTranslation>(){
                               new(){ Name = "Car electronics", LanguageId=LanguageId.English },
                               new(){ Name = "Автомобільна електроніка", LanguageId=LanguageId.Ukrainian }
                           }
                    },
/*-1- 19*/         new(){ UrlSlug = "photo-and-video",
                           Image = "PhotoAndVideo.png",
                           ParentId = 1,
                           CategoryTranslations = new List<CategoryTranslation>(){
                               new(){ Name = "Photo and video", LanguageId=LanguageId.English },
                               new(){ Name = "Фото та відео", LanguageId=LanguageId.Ukrainian }
                           }
                    },

/*-13- 20*/        new(){ UrlSlug = "tablet-computers",
                           Image = "TabletComputers.png",
                           ParentId = 13,
                           CategoryTranslations = new List<CategoryTranslation>(){
                               new(){ Name = "Tablet computers", LanguageId=LanguageId.English },
                               new(){ Name = "Планшетні комп'ютери", LanguageId=LanguageId.Ukrainian }
                           }
                    },
/*-13- 21*/        new(){ UrlSlug = "laptops",
                           Image = "Laptops.png",
                           ParentId = 13,
                           Sales=new List<Sale>(){ laptopSale },
                           CategoryTranslations = new List<CategoryTranslation>(){
                               new(){  Name = "Laptops", LanguageId=LanguageId.English },
                               new(){ Name = "Ноутбуки", LanguageId=LanguageId.Ukrainian }
                           }
                    },
/*-13- 22*/        new(){ UrlSlug = "monitors",
                           Image = "Monitors.png",
                           ParentId = 13,
                           CategoryTranslations = new List<CategoryTranslation>(){
                               new(){ Name = "Monitors", LanguageId=LanguageId.English },
                               new(){ Name = "Монітори", LanguageId=LanguageId.Ukrainian }
                           }
                    },
/*-13- 23*/        new(){ UrlSlug = "components-for-computer-equipment",
                           Image = "ComponentsForComputerEquipment.png",
                           ParentId = 13,
                           CategoryTranslations = new List<CategoryTranslation>(){
                               new(){ Name = "Components for computer equipment",LanguageId=LanguageId.English },
                               new(){ Name = "Комплектуючі для комп'ютерної техніки", LanguageId=LanguageId.Ukrainian }
                           }
                    },
/*-13- 24*/        new(){ UrlSlug = "computer-accessories",
                           Image = "ComputerAccessories.png",
                           ParentId = 13,
                           CategoryTranslations = new List<CategoryTranslation>(){
                               new(){ Name = "Computer accessories", LanguageId=LanguageId.English},
                               new(){ Name = "Комп'ютерні аксесуари", LanguageId=LanguageId.Ukrainian }
                           }
                    },
/*-13- 25*/        new(){ UrlSlug = "smart-watches-and-fitness-bracelets",
                           Image = "SmartWatchesAndFitness bracelets.png",
                           ParentId = 13,
                           CategoryTranslations = new List<CategoryTranslation>(){
                               new(){ Name = "Smart watches and fitness bracelets", LanguageId=LanguageId.English },
                               new(){ Name = "Розумні годинники та фітнес браслети", LanguageId=LanguageId.Ukrainian }
                           }
                    },
/*-13- 26*/        new(){ UrlSlug = "printers-scanners-mfps",
                           Image = "PrintersScanners.png",
                           ParentId = 13,
                           CategoryTranslations = new List<CategoryTranslation>(){
                               new(){ Name = "Printers, scanners, MFPs", LanguageId=LanguageId.English },
                               new(){ Name = "Принтери, сканери, МФУ", LanguageId=LanguageId.Ukrainian }
                           }
                    },
/*-13- 27*/        new(){ UrlSlug = "game-consoles",
                           Image = "GameConsoles.png",
                           ParentId = 13,
                           CategoryTranslations = new List<CategoryTranslation>(){
                               new(){ Name = "Game consoles", LanguageId=LanguageId.English },
                               new(){ Name = "Ігрові приставки", LanguageId=LanguageId.Ukrainian }
                           }
                    },
/*-13- 28*/        new(){ UrlSlug = "desktops",
                           Image = "Desktops.png",
                           ParentId = 13,
                           CategoryTranslations = new List<CategoryTranslation>(){
                               new(){ Name = "Desktops", LanguageId=LanguageId.English },
                               new(){ Name = "Настільні комп'ютери", LanguageId=LanguageId.Ukrainian }
                           }
                    },
/*-13- 29*/        new(){ UrlSlug = "software",
                           Image = "Software.png",
                           ParentId = 13,
                           CategoryTranslations = new List<CategoryTranslation>(){
                               new(){ Name = "Software", LanguageId=LanguageId.English },
                               new(){ Name = "Програмне забезпечення", LanguageId=LanguageId.Ukrainian }
                           }
                    },
/*-13- 30*/        new(){ UrlSlug = "server-equipment",
                           Image = "Servers.png",
                           ParentId = 13,
                           CategoryTranslations = new List<CategoryTranslation>(){
                               new(){ Name = "Server equipment", LanguageId=LanguageId.English },
                               new(){ Name = "Серверне обладнання", LanguageId=LanguageId.Ukrainian }
                           }
                    },

/*-14- 31*/        new(){ UrlSlug = "small-household-appliances-kitchen",
                           Image = "SmallHouseholdKitchen.png",
                           ParentId = 14,
                           CategoryTranslations = new List<CategoryTranslation>(){
                               new(){ Name = "Small household appliances for the kitchen", LanguageId=LanguageId.English },
                               new(){ Name = "Дрібна побутова техніка для кухні", LanguageId=LanguageId.Ukrainian }
                           }
                    },
/*-14- 32*/        new(){ UrlSlug = "large-household-appliances-kitchen",
                           Image = "LargeHouseholdKitchen.png",
                           ParentId = 14,
                           CategoryTranslations = new List<CategoryTranslation>(){
                               new(){ Name = "Large household appliances for the kitchen", LanguageId=LanguageId.English },
                               new(){ Name = "Велика побутова техніка для кухні", LanguageId=LanguageId.Ukrainian }
                           }
                    },
/*-14- 33*/        new(){ UrlSlug = "climate-technology",
                           Image = "ClimateTechnology.png",
                           ParentId = 14,
                           CategoryTranslations = new List<CategoryTranslation>(){
                               new(){ Name = "Climate technology", LanguageId=LanguageId.English },
                               new(){ Name = "Кліматична техніка", LanguageId=LanguageId.Ukrainian }
                           }
                    },
/*-14- 34*/        new(){ UrlSlug = "household-appliances-personal",
                           Image = "HouseholdPersonal.png",
                           ParentId = 14,
                           CategoryTranslations = new List<CategoryTranslation>(){
                               new(){ Name = "Household appliances for personal use", LanguageId=LanguageId.English },
                               new(){ Name = "Побутова техніка для особистого користування", LanguageId=LanguageId.Ukrainian }
                           }
                    },
/*-14- 35*/        new(){ UrlSlug = "household-appliances-home",
                           Image = "HouseholdHome.png",
                           ParentId = 14,
                           CategoryTranslations = new List<CategoryTranslation>(){
                               new(){ Name = "Household appliances for the home", LanguageId=LanguageId.English },
                               new(){ Name = "Побутова техніка для дому", LanguageId=LanguageId.Ukrainian }
                           }
                    },
            
/*-15- 36*/        new(){ UrlSlug = "mobile-phones-smartphones",
                           Image = "MobilePhones.png",
                           ParentId = 15,
                           CategoryTranslations = new List<CategoryTranslation>(){
                               new(){ Name = "Mobile phones, smartphones", LanguageId=LanguageId.English },
                               new(){ Name = "Мобільні телефони, смартфони", LanguageId=LanguageId.Ukrainian }
                           }
                    },
/*-15- 37*/        new(){ UrlSlug = "phone-cases",
                           Image = "PhoneCases.png",
                           ParentId = 15,
                           CategoryTranslations = new List<CategoryTranslation>(){
                               new(){ Name = "Phone cases", LanguageId=LanguageId.English },
                               new(){ Name = "Чохли для телефонів", LanguageId=LanguageId.Ukrainian }
                           }
                    },
/*-15- 38*/        new(){ UrlSlug = "Protective-films-and-glass",
                           Image = "ProtectiveFilmsGlass.png",
                           ParentId = 15,
                           CategoryTranslations = new List<CategoryTranslation>(){
                               new(){ Name = "Protective films and glass", LanguageId=LanguageId.English },
                               new(){ Name = "Захисні плівки та скло", LanguageId=LanguageId.Ukrainian }
                           }
                    },
            
/*-16- 39*/        new(){ UrlSlug = "headphones-and-headsets",
                           Image = "HeadphonesAndHeadsets.png",
                           ParentId = 16,
                           CategoryTranslations = new List<CategoryTranslation>(){
                               new(){ Name = "Headphones and headsets", LanguageId=LanguageId.English },
                               new(){ Name = "Навушники та гарнітури", LanguageId=LanguageId.Ukrainian }
                           }
                    },
/*-16- 40*/        new(){ UrlSlug = "portable-acoustics",
                           Image = "PortableAcoustics.png",
                           ParentId = 16,
                           CategoryTranslations = new List<CategoryTranslation>(){
                               new(){ Name = "Portable acoustics", LanguageId=LanguageId.English },
                               new(){ Name = "Портативна акустика", LanguageId=LanguageId.Ukrainian }
                           }
                    },
/*-16- 41*/        new(){ UrlSlug = "acoustic-systems",
                           Image = "AcousticSystems.png",
                           ParentId = 16,
                           CategoryTranslations = new List<CategoryTranslation>(){
                               new(){ Name = "Acoustic systems", LanguageId=LanguageId.English },
                               new(){ Name = "Акустичні системи", LanguageId=LanguageId.Ukrainian }
                           }
                    },
/*-16- 42*/        new(){ UrlSlug = "microphones",
                           Image = "Microphones.png",
                           ParentId = 16,
                           CategoryTranslations = new List<CategoryTranslation>(){
                               new(){ Name = "Microphones", LanguageId=LanguageId.English },
                               new(){ Name = "Мікрофони", LanguageId=LanguageId.Ukrainian }
                           }
                    },
/*-16- 43*/        new(){ UrlSlug = "vinyl-disc-players",
                           Image = "VinylDiscPlayers.png",
                           ParentId = 16,
                           CategoryTranslations = new List<CategoryTranslation>(){
                               new(){ Name = "Vinyl disc players", LanguageId=LanguageId.English },
                               new(){ Name = "Програвачі вінілових дисків", LanguageId=LanguageId.Ukrainian }
                           }
                    },

/*-17- 44*/        new(){ UrlSlug = "televisions",
                           Image = "Televisions.png",
                           ParentId = 17,
                           CategoryTranslations = new List<CategoryTranslation>(){
                               new(){ Name = "Televisions", LanguageId=LanguageId.English },
                               new(){ Name = "Телевізори", LanguageId=LanguageId.Ukrainian }
                           }
                    },
/*-17- 45*/        new(){ UrlSlug = "projectors",
                           Image = "Projectors.png",
                           ParentId = 17,
                           CategoryTranslations = new List<CategoryTranslation>(){
                               new(){ Name = "Projectors", LanguageId=LanguageId.English },
                               new(){ Name = "Проектори", LanguageId=LanguageId.Ukrainian }
                           }
                    },
/*-17- 46*/        new(){ UrlSlug = "accessories-televisions-and-projectors",
                           Image = "AccessoriesForTVAndProjectors.png",
                           ParentId = 17,
                           CategoryTranslations = new List<CategoryTranslation>(){
                               new(){ Name = "Accessories for TVs and projectors", LanguageId=LanguageId.English },
                               new(){ Name = "Аксесуари для телевізорів і проекторів", LanguageId=LanguageId.Ukrainian }
                           }
                    },

/*-18- 47*/        new(){ UrlSlug = "car-video-recorders",
                           Image = "CarVideoRecorders.png",
                           ParentId = 18,
                           CategoryTranslations = new List<CategoryTranslation>(){
                               new(){ Name = "Car video recorders", LanguageId=LanguageId.English },
                               new(){ Name = "Автомобільні відеореєстратори", LanguageId=LanguageId.Ukrainian }
                           }
                    },
/*-18- 48*/        new(){ UrlSlug = "gps",
                           Image = "GPS.png",
                           ParentId = 18,
                           CategoryTranslations = new List<CategoryTranslation>(){
                               new(){ Name = "GPS", LanguageId=LanguageId.English },
                               new(){ Name = "GPS", LanguageId=LanguageId.Ukrainian }
                           }
                    },
/*-18- 49*/        new(){ UrlSlug = "anti-theft-systems-and-accessories",
                           Image = "AntiTheftSystemsAndAccessories.png",
                           ParentId = 18,
                           CategoryTranslations = new List<CategoryTranslation>(){
                               new(){ Name = "Anti-theft systems and accessories", LanguageId=LanguageId.English },
                               new(){ Name = "Протиугінні системи і аксесуари", LanguageId=LanguageId.Ukrainian }
                           }
                    },

/*-19- 50*/        new(){ UrlSlug = "cameras",
                           Image = "Cameras.png",
                           ParentId = 19,
                           CategoryTranslations = new List<CategoryTranslation>(){
                               new(){ Name = "Cameras", LanguageId=LanguageId.English },
                               new(){ Name = "Фотоапарати", LanguageId=LanguageId.Ukrainian }
                           }
                    },
/*-19- 51*/        new(){ UrlSlug = "film-cameras",
                           Image = "FilmCameras.png",
                           ParentId = 19,
                           CategoryTranslations = new List<CategoryTranslation>(){
                               new(){ Name = "Film cameras", LanguageId=LanguageId.English },
                               new(){ Name = "Плівкові фотоапарати", LanguageId=LanguageId.Ukrainian }
                           }
                    },
/*-19- 52*/        new(){ UrlSlug = "video-cameras",
                           Image = "VideoCameras.png",
                           ParentId = 19,
                           CategoryTranslations = new List<CategoryTranslation>(){
                               new(){ Name = "Video cameras", LanguageId=LanguageId.English },
                               new(){ Name = "Відеокамери", LanguageId=LanguageId.Ukrainian }
                           }
                    },
/*-19- 53*/        new(){ UrlSlug = "studio-equipment",
                           Image = "StudioEquipment.png",
                           ParentId = 19,
                           CategoryTranslations = new List<CategoryTranslation>(){
                               new(){ Name = "Studio equipment", LanguageId=LanguageId.English },
                               new(){ Name = "Студійне обладнання", LanguageId=LanguageId.Ukrainian }
                           }
                    },
       

/*-7- 54*/         new(){ UrlSlug = "womens-clothes",
                          Image = "WomensClothes.png",
                          ParentId = 7,
                          Sales=new List<Sale>() {clothesAndShoesSale },
                          CategoryTranslations = new List<CategoryTranslation>(){
                              new(){ Name = "Women's clothes", LanguageId=LanguageId.English },
                              new(){ Name = "Жіночий одяг", LanguageId=LanguageId.Ukrainian }
                          }
                   },
/*-7- 55*/         new(){ UrlSlug = "mens-clothing",
                          Image = "MensClothing.png",
                          ParentId = 7,
                          Sales=new List<Sale>() {clothesAndShoesSale },
                          CategoryTranslations = new List<CategoryTranslation>(){
                              new(){ Name = "Men's clothing", LanguageId=LanguageId.English },
                              new(){ Name = "Чоловічий одяг", LanguageId=LanguageId.Ukrainian }
                          }
                    },
/*-7- 56*/         new(){ UrlSlug = "childrens-clothes",
                          Image = "ChildrensClothes.png",
                          ParentId = 7,
                          Sales=new List<Sale>() {clothesAndShoesSale },
                          CategoryTranslations = new List<CategoryTranslation>(){
                             new(){ Name = "Children's clothes", LanguageId=LanguageId.English },
                             new(){ Name = "Дитячі одяг", LanguageId=LanguageId.Ukrainian }
                          }
                   },
/*-7- 57*/         new(){ UrlSlug = "sportswear",
                         Image = "Sportswear.png",
                         ParentId = 7,
                         Sales=new List<Sale>() {clothesAndShoesSale },
                         CategoryTranslations = new List<CategoryTranslation>(){
                             new(){ Name = "Sportswear", LanguageId=LanguageId.English },
                             new(){ Name = "Спортивний одяг", LanguageId=LanguageId.Ukrainian }
                         }
                   },

/*-54- 58*/        new(){ UrlSlug = "womens-dresses",
                          Image = "WomensDresses.png",
                          ParentId = 54,
                          Sales=new List<Sale>() {clothesAndShoesSale },
                          CategoryTranslations = new List<CategoryTranslation>(){
                              new(){ Name = "Women's dresses", LanguageId=LanguageId.English },
                              new(){ Name = "Жіночі сукні", LanguageId=LanguageId.Ukrainian }
                          }
                   },
/*-54- 59*/        new(){ UrlSlug = "womens-jackets",
                          Image = "WomensJackets.png",
                          ParentId = 54,
                          Sales=new List<Sale>() {clothesAndShoesSale },
                          CategoryTranslations = new List<CategoryTranslation>(){
                              new(){ Name = "Women's jackets", LanguageId=LanguageId.English },
                              new(){ Name = "Жіночі куртки", LanguageId=LanguageId.Ukrainian }
                          }
                   },
/*-54- 60*/        new(){ UrlSlug = "womens-coats",
                          Image = "WomensCoats.png",
                          ParentId = 54,
                          Sales=new List<Sale>() {clothesAndShoesSale },
                          CategoryTranslations = new List<CategoryTranslation>(){
                              new(){ Name = "Women's coats", LanguageId=LanguageId.English },
                              new(){ Name = "Жіночі пальта", LanguageId=LanguageId.Ukrainian }
                          }
                   },
/*-54- 61*/        new(){ UrlSlug = "womens-down-jackets",
                          Image = "WomensDownJackets.png",
                          ParentId = 54,
                          Sales=new List<Sale>() {clothesAndShoesSale },
                          CategoryTranslations = new List<CategoryTranslation>(){
                              new(){ Name = "Women's down jackets", LanguageId=LanguageId.English },
                              new(){ Name = "Жіночі пуховики", LanguageId=LanguageId.Ukrainian }
                          }
                   },
/*-54- 62*/        new(){ UrlSlug = "womens-fur-coats",
                          Image = "WomensFurCoats.png",
                          ParentId = 54,
                          Sales=new List<Sale>() {clothesAndShoesSale },
                          CategoryTranslations = new List<CategoryTranslation>(){
                              new(){ Name = "Women's fur coats", LanguageId=LanguageId.English },
                              new(){ Name = "Жіночі шуби", LanguageId=LanguageId.Ukrainian }
                          }
                   },
/*-54- 63*/        new(){ UrlSlug = "womens-pajamas",
                          Image = "WomensPajamas.png",
                          ParentId = 54,
                          Sales=new List<Sale>() {clothesAndShoesSale },
                          CategoryTranslations = new List<CategoryTranslation>(){
                              new(){ Name = "Women's pajamas", LanguageId=LanguageId.English },
                              new(){ Name = "Жіночі піжами", LanguageId=LanguageId.Ukrainian }
                          }
                   },
/*-54- 64*/        new(){ UrlSlug = "womens-sweaters-and-cardigans",
                          Image = "WomensSweatersAndCardigans.png",
                          ParentId = 54,
                          Sales=new List<Sale>() {clothesAndShoesSale },
                          CategoryTranslations = new List<CategoryTranslation>(){
                              new(){ Name = "Women's sweaters and cardigans", LanguageId=LanguageId.English },
                              new(){ Name = "Жіночі светри та кардигани", LanguageId=LanguageId.Ukrainian }
                          }
                   },
/*-54- 65*/        new(){ UrlSlug = "womens-t-shirts-and-tank-tops",
                          Image = "WomensTShirtsAndTankTops.png",
                          ParentId = 54,
                          Sales=new List<Sale>() {clothesAndShoesSale },
                          CategoryTranslations = new List<CategoryTranslation>(){
                              new(){ Name = "Women's t-shirts and tank tops", LanguageId=LanguageId.English },
                              new(){ Name = "Жіночі футболки та майки", LanguageId=LanguageId.Ukrainian }
                          }
                   },
/*-54- 66*/        new(){ UrlSlug = "womens-trousers",
                          Image = "WomensTrousers.png",
                          ParentId = 54,
                          Sales=new List<Sale>() {clothesAndShoesSale },
                          CategoryTranslations = new List<CategoryTranslation>(){
                              new(){ Name = "Women's trousers", LanguageId=LanguageId.English },
                              new(){ Name = "Жіночі брюки", LanguageId=LanguageId.Ukrainian }
                          }
                   },
/*-54- 67*/        new(){ UrlSlug = "womens-blouses-and-tunics",
                          Image = "WomensBlousesAndTunics.png",
                          ParentId = 54,
                          Sales=new List<Sale>() {clothesAndShoesSale },
                          CategoryTranslations = new List<CategoryTranslation>(){
                              new(){ Name = "Women's blouses and tunics", LanguageId=LanguageId.English },
                              new(){ Name = "Жіночі блузки та туніки", LanguageId=LanguageId.Ukrainian }
                          }
                   },
/*-54- 68*/        new(){ UrlSlug = "womens-skirts",
                          Image = "WomensSkirts.png",
                          ParentId = 54,
                          Sales=new List<Sale>() {clothesAndShoesSale },
                          CategoryTranslations = new List<CategoryTranslation>(){
                              new(){ Name = "Women's skirts", LanguageId=LanguageId.English },
                              new(){ Name = "Жіночі спідниці", LanguageId=LanguageId.Ukrainian }
                          }
                   },
/*-54- 69*/        new(){ UrlSlug = "womens-shorts-and-breeches",
                          Image = "WomensShortsAndBreeches.png",
                          ParentId = 54,
                          Sales=new List<Sale>() {clothesAndShoesSale },
                          CategoryTranslations = new List<CategoryTranslation>(){
                              new(){ Name = "Women's shorts and breeches", LanguageId=LanguageId.English },
                              new(){ Name = "Жіночі шорти і бриджі", LanguageId=LanguageId.Ukrainian }
                          }
                   },
/*-54- 70*/        new(){ UrlSlug = "womens-corsages-and-corsets",
                          Image = "WomensCorsagesAndCorsets.png",
                          ParentId = 54,
                          Sales=new List<Sale>() {clothesAndShoesSale },
                          CategoryTranslations = new List<CategoryTranslation>(){
                              new(){ Name = "Women's corsages and corsets", LanguageId=LanguageId.English },
                              new(){ Name = "Жіночі корсажі і корсети", LanguageId=LanguageId.Ukrainian }
                          }
                   },

/*-55- 71*/        new(){ UrlSlug = "mens-t-shirts-and-tank-tops",
                          Image = "MensTShirtsAndTankTops.png",
                          ParentId = 55,
                          Sales=new List<Sale>() {clothesAndShoesSale },
                          CategoryTranslations = new List<CategoryTranslation>(){
                              new(){ Name = "Men's t-shirts and tank tops", LanguageId=LanguageId.English },
                              new(){ Name = "Чоловічі футболки та майки", LanguageId=LanguageId.Ukrainian }
                          }
                   },
/*-55- 72*/        new(){ UrlSlug = "men-jackets",
                          Image = "MensJackets.png",
                          ParentId = 55,
                          Sales=new List<Sale>() {clothesAndShoesSale },
                          CategoryTranslations = new List<CategoryTranslation>(){
                              new(){ Name = "Men's jackets", LanguageId=LanguageId.English },
                              new(){ Name = "Чоловічі куртки", LanguageId=LanguageId.Ukrainian }
                          }
                   },
/*-55- 73*/        new(){ UrlSlug = "mens-coats",
                          Image = "MensCoats.png",
                          ParentId = 55,
                          Sales=new List<Sale>() {clothesAndShoesSale },
                          CategoryTranslations = new List<CategoryTranslation>(){
                              new(){ Name = "Men's coats", LanguageId=LanguageId.English },
                              new(){ Name = "Чоловічі пальта", LanguageId=LanguageId.Ukrainian }
                          }
                   },
/*-55- 74*/        new(){ UrlSlug = "mens-down-jackets",
                          Image = "MensDownJackets.png",
                          ParentId = 55,
                          Sales=new List<Sale>() {clothesAndShoesSale },
                          CategoryTranslations = new List<CategoryTranslation>(){
                              new(){ Name = "Men's down jackets", LanguageId=LanguageId.English },
                              new(){ Name = "Чоловічі пуховики", LanguageId=LanguageId.Ukrainian }
                          }
                   },
/*-55- 75*/        new(){ UrlSlug = "mens-fur-coats",
                          Image = "MensFurCoats.png",
                          ParentId = 55,
                          Sales=new List<Sale>() {clothesAndShoesSale },
                          CategoryTranslations = new List<CategoryTranslation>(){
                              new(){ Name = "Men's fur coats", LanguageId=LanguageId.English },
                              new(){ Name = "Чоловічі шуби", LanguageId=LanguageId.Ukrainian }
                          }
                   },
/*-55- 76*/        new(){ UrlSlug = "mens-pajamas",
                          Image = "MensPajamas.png",
                          ParentId = 55,
                          Sales=new List<Sale>() {clothesAndShoesSale },
                          CategoryTranslations = new List<CategoryTranslation>(){
                              new(){ Name = "Men's pajamas", LanguageId=LanguageId.English },
                              new(){ Name = "Чоловічі піжами", LanguageId=LanguageId.Ukrainian }
                          }
                   },
/*-55- 77*/        new(){ UrlSlug = "mens-sweaters-and-cardigans",
                          Image = "MensSweatersAndCardigans.png",
                          ParentId = 55,
                          Sales=new List<Sale>() {clothesAndShoesSale },
                          CategoryTranslations = new List<CategoryTranslation>(){
                              new(){ Name = "Men's sweaters and cardigans", LanguageId=LanguageId.English },
                              new(){ Name = "Чоловічі светри та кардигани", LanguageId=LanguageId.Ukrainian }
                          }
                   },
/*-55- 78*/        new(){ UrlSlug = "mens-trousers",
                          Image = "MensTrousers.png",
                          ParentId = 55,
                          Sales=new List<Sale>() {clothesAndShoesSale },
                          CategoryTranslations = new List<CategoryTranslation>(){
                              new(){ Name = "Men's trousers", LanguageId=LanguageId.English },
                              new(){ Name = "Чоловічі брюки", LanguageId=LanguageId.Ukrainian }
                          }
                   },
/*-55- 79*/        new(){ UrlSlug = "mens-shirts",
                          Image = "MensShirts.png",
                          ParentId = 55,
                          Sales=new List<Sale>() {clothesAndShoesSale },
                          CategoryTranslations = new List<CategoryTranslation>(){
                              new(){ Name = "Men's shirts", LanguageId=LanguageId.English },
                              new(){ Name = "Чоловічі сорочки", LanguageId=LanguageId.Ukrainian }
                          }
                   },
/*-55- 80*/        new(){ UrlSlug = "men-suit-jackets",
                          Image = "MensSuitJackets.png",
                          ParentId = 55,
                          Sales=new List<Sale>() {clothesAndShoesSale },
                          CategoryTranslations = new List<CategoryTranslation>(){
                              new(){ Name = "Men's suit jackets", LanguageId=LanguageId.English },
                              new(){ Name = "Чоловічі піджаки", LanguageId=LanguageId.Ukrainian }
                          }
                   },
/*-55- 81*/        new(){ UrlSlug = "mens-shorts-and-breeches",
                          Image = "MensShortsAndBreeches.png",
                          ParentId = 55,
                          Sales=new List<Sale>() {clothesAndShoesSale },
                          CategoryTranslations = new List<CategoryTranslation>(){
                              new(){ Name = "Men's shorts and breeches", LanguageId=LanguageId.English },
                              new(){ Name = "Чоловічі шорти і бриджі", LanguageId=LanguageId.Ukrainian }
                          }
                   },
/*-55- 82*/        new(){ UrlSlug = "mens-suits-and-tuxedos",
                          Image = "MensSuitsAndTuxedos.png",
                          ParentId = 55,
                          Sales=new List<Sale>() {clothesAndShoesSale },
                          CategoryTranslations = new List<CategoryTranslation>(){
                              new(){ Name = "Men's suits and tuxedos", LanguageId=LanguageId.English },
                              new(){ Name = "Чоловічі костюми та смокінги", LanguageId=LanguageId.Ukrainian }
                          }
                   },

/*-56- 83*/        new(){ UrlSlug = "dresses-for-girls",
                          Image = "DressesForGirls.png",
                          ParentId = 56,
                          Sales=new List<Sale>() {clothesAndShoesSale },
                          CategoryTranslations = new List<CategoryTranslation>(){
                              new(){ Name = "Dresses and sundresses for girls", LanguageId=LanguageId.English },
                              new(){ Name = "Сукні та сарафани для дівчаток", LanguageId=LanguageId.Ukrainian }
                          }
                   },
/*-56- 84*/        new(){ UrlSlug = "cardigans-and-sweaters-for-girls",
                          Image = "CardigansSweatersForGirls.png",
                          ParentId = 56,
                          Sales=new List<Sale>() {clothesAndShoesSale },
                          CategoryTranslations = new List<CategoryTranslation>(){
                              new(){ Name = "Cardigans and sweaters for girls", LanguageId=LanguageId.English },
                              new(){ Name = "Кофти та светри для дівчаток", LanguageId=LanguageId.Ukrainian }
                          }
                   },
/*-56- 85*/        new(){ UrlSlug = "t-shirts-and-tank-tops-for-girls",
                          Image = "",
                          ParentId = 56,
                          Sales=new List<Sale>() {clothesAndShoesSale },
                          CategoryTranslations = new List<CategoryTranslation>(){
                              new(){ Name = "T-shirts and tank tops for girls", LanguageId=LanguageId.English },
                              new(){ Name = "Футболки та майки для дівчаток", LanguageId=LanguageId.Ukrainian }
                          }
                   },          
/*-56- 86*/        new(){ UrlSlug = "blouses-and-tunics-for-girls",
                          Image = "",
                          ParentId = 56,
                          Sales=new List<Sale>() {clothesAndShoesSale },
                          CategoryTranslations = new List<CategoryTranslation>(){
                              new(){ Name = "Blouses and tunics for girls", LanguageId=LanguageId.English },
                              new(){ Name = "Блузки і туніки для дівчаток", LanguageId=LanguageId.Ukrainian }
                          }
                   },
/*-56- 87*/        new(){ UrlSlug = "pants-and-jeans-for-girls",
                          Image = "",
                          ParentId = 56,
                          Sales=new List<Sale>() {clothesAndShoesSale },
                          CategoryTranslations = new List<CategoryTranslation>(){
                              new(){ Name = "Pants and jeans for girls", LanguageId=LanguageId.English },
                              new(){ Name = "Брюки і джинси для дівчаток", LanguageId=LanguageId.Ukrainian }
                          }
                   },
/*-56- 88*/        new(){ UrlSlug = "childrens-skirts",
                          Image = "",
                          ParentId = 56,
                          Sales=new List<Sale>() {clothesAndShoesSale },
                          CategoryTranslations = new List<CategoryTranslation>(){
                              new(){ Name = "Children's skirts", LanguageId=LanguageId.English },
                              new(){ Name = "Дитячі спідниці", LanguageId=LanguageId.Ukrainian }
                          }
                   },    
/*-56- 89*/        new(){ UrlSlug = "cardigans-and-sweaters-for-boys",
                          Image = "",
                          ParentId = 56,
                          Sales=new List<Sale>() {clothesAndShoesSale },
                          CategoryTranslations = new List<CategoryTranslation>(){
                              new(){ Name = "Cardigans and sweaters for boys", LanguageId=LanguageId.English },
                              new(){ Name = "Кофти та светри для хлопчиків", LanguageId=LanguageId.Ukrainian }
                          }
                   },
/*-56- 90*/        new(){ UrlSlug = "t-shirts-and-tank-tops-for-boys",
                          Image = "",
                          ParentId = 56,
                          Sales=new List<Sale>() {clothesAndShoesSale },
                          CategoryTranslations = new List<CategoryTranslation>(){
                              new(){ Name = "T-shirts and tank tops for boys", LanguageId=LanguageId.English },
                              new(){ Name = "Футболки та майки для хлопчиків", LanguageId=LanguageId.Ukrainian }
                          }
                   },
/*-56- 90*/        new(){ UrlSlug = "pants-and-jeans-for-boys",
                          Image = "",
                          ParentId = 56,
                          Sales=new List<Sale>() {clothesAndShoesSale },
                          CategoryTranslations = new List<CategoryTranslation>(){
                              new(){ Name = "Pants and jeans for boys", LanguageId=LanguageId.English },
                              new(){ Name = "Брюки і джинси для хлопчиків", LanguageId=LanguageId.Ukrainian }
                          }
                   },
/*-56- 91*/        new(){ UrlSlug = "shorts-and-breeches-for-boys",
                          Image = "",
                          ParentId = 56,
                          Sales=new List<Sale>() {clothesAndShoesSale },
                          CategoryTranslations = new List<CategoryTranslation>(){
                              new(){ Name = "Shorts and breeches for boys", LanguageId=LanguageId.English },
                              new(){ Name = "Шорти і бриджі для хлопчиків", LanguageId=LanguageId.Ukrainian }
                          }
                   },
/*-56- 92*/        new(){ UrlSlug = "shirts-for-boys",
                          Image = "",
                          ParentId = 56,
                          Sales=new List<Sale>() {clothesAndShoesSale },
                          CategoryTranslations = new List<CategoryTranslation>(){
                              new(){ Name = "Shirts for boys", LanguageId=LanguageId.English },
                              new(){ Name = "Сорочки для хлопчиків", LanguageId=LanguageId.Ukrainian }
                          }
                   },
/*-56- 93*/        new(){ UrlSlug = "suit-for-boys",
                          Image = "",
                          ParentId = 56,
                          Sales=new List<Sale>() {clothesAndShoesSale },
                          CategoryTranslations = new List<CategoryTranslation>(){
                              new(){ Name = "Suit for boys", LanguageId=LanguageId.English },
                              new(){ Name = "Піджаки для хлопчиків", LanguageId=LanguageId.Ukrainian }
                          }
                   },

/*-57- 94*/        new(){ UrlSlug = "tracksuits",
                          Image = "",
                          ParentId = 57,
                          Sales=new List<Sale>() {clothesAndShoesSale },
                          CategoryTranslations = new List<CategoryTranslation>(){
                              new(){ Name = "Tracksuits", LanguageId=LanguageId.English },
                              new(){ Name = "Спортивні костюми", LanguageId=LanguageId.Ukrainian }
                          }
                   },
/*-57- 95*/        new(){ UrlSlug = "baseball-caps-and-caps",
                          Image = "",
                          ParentId = 57,
                          Sales=new List<Sale>() {clothesAndShoesSale },
                          CategoryTranslations = new List<CategoryTranslation>(){
                              new(){ Name = "Baseball caps and caps", LanguageId=LanguageId.English },
                              new(){ Name = "Бейсболки і кепки", LanguageId=LanguageId.Ukrainian }
                          }
                   },
/*-57- 96*/        new(){ UrlSlug = "sport-pants",
                          Image = "",
                          ParentId = 57,
                          Sales=new List<Sale>() {clothesAndShoesSale },
                          CategoryTranslations = new List<CategoryTranslation>(){
                              new(){ Name = "Sport pants", LanguageId=LanguageId.English },
                              new(){ Name = "Спортивні штани", LanguageId=LanguageId.Ukrainian }
                          }
                   },
/*-57- 97*/        new(){ UrlSlug = "sports-t-shirts-and-tank-tops",
                          Image = "",
                          ParentId = 57,
                          Sales=new List<Sale>() {clothesAndShoesSale },
                          CategoryTranslations = new List<CategoryTranslation>(){
                              new(){ Name = "Sports T-shirts and tank tops", LanguageId=LanguageId.English },
                              new(){ Name = "Спортивні футболки та майки", LanguageId=LanguageId.Ukrainian }
                          }
                   },
/*-57- 98*/        new(){ UrlSlug = "clothes-for-choreography-and-gymnastics",
                          Image = "",
                          ParentId = 57,
                          Sales=new List<Sale>() {clothesAndShoesSale },
                          CategoryTranslations = new List<CategoryTranslation>(){
                              new(){ Name = "Clothes for choreography and gymnastics", LanguageId=LanguageId.English },
                              new(){ Name = "Одяг для хореографії і гімнастики", LanguageId=LanguageId.Ukrainian }
                          }
                   },
/*-57- 99*/        new(){ UrlSlug = "clothing-for-yoga-and-fitness",
                          Image = "",
                          ParentId = 57,
                          Sales=new List<Sale>() {clothesAndShoesSale },
                          CategoryTranslations = new List<CategoryTranslation>(){
                              new(){ Name = "Clothing for yoga and fitness", LanguageId=LanguageId.English },
                              new(){ Name = "Одяг для йоги та фітнесу", LanguageId=LanguageId.Ukrainian }
                          }
                   },
/*-57- 100*/       new(){ UrlSlug = "sports-jackets-and-sweaters",
                          Image = "",
                          ParentId = 57,
                          Sales=new List<Sale>() {clothesAndShoesSale },
                          CategoryTranslations = new List<CategoryTranslation>(){
                              new(){ Name = "Sports jackets and sweaters", LanguageId=LanguageId.English },
                              new(){ Name = "Спортивні кофти та светри", LanguageId=LanguageId.Ukrainian }
                          }
                   },
/*-57- 101*/       new(){ UrlSlug = "sports-jackets",
                          Image = "",
                          ParentId = 57,
                          Sales=new List<Sale>() {clothesAndShoesSale },
                          CategoryTranslations = new List<CategoryTranslation>(){
                              new(){ Name = "Sports jackets", LanguageId=LanguageId.English },
                              new(){ Name = "Спортивні куртки", LanguageId=LanguageId.Ukrainian }
                          }
                   },
/*-57- 102*/       new(){ UrlSlug = "sports-shorts",
                          Image = "",
                          ParentId = 57,
                          Sales=new List<Sale>() {clothesAndShoesSale },
                          CategoryTranslations = new List<CategoryTranslation>(){
                              new(){ Name = "Sports shorts", LanguageId=LanguageId.English },
                              new(){ Name = "Спортивні шорти", LanguageId=LanguageId.Ukrainian }
                          }
                   },

/*2- 103*/         new(){ UrlSlug = "building-materials",
                          Image = "",
                          ParentId = 2,
                          CategoryTranslations = new List<CategoryTranslation>(){
                              new(){ Name = "Building materials", LanguageId=LanguageId.English },
                              new(){ Name = "Будівельні матеріали", LanguageId=LanguageId.Ukrainian }
                          }
                   },
/*2- 104*/         new(){ UrlSlug = "water-gas-heat-supply",
                          Image = "",
                          ParentId = 2,
                          CategoryTranslations = new List<CategoryTranslation>(){
                              new(){ Name = "Water, gas, heat supply", LanguageId=LanguageId.English },
                              new(){ Name = "Водо-, газо-, теплозабезпечення", LanguageId=LanguageId.Ukrainian }
                          }
                   },
/*2- 105*/         new(){ UrlSlug = "finishing-materials",
                          Image = "",
                          ParentId = 2,
                          CategoryTranslations = new List<CategoryTranslation>(){
                              new(){ Name = "Finishing materials", LanguageId=LanguageId.English },
                              new(){ Name = "Оздоблювальні матеріали", LanguageId=LanguageId.Ukrainian }
                          }
                   },         
/*2- 106*/         new(){ UrlSlug = "construction-machinery-and-equipment",
                          Image = "",
                          ParentId = 2,
                          CategoryTranslations = new List<CategoryTranslation>(){
                              new(){ Name = "Construction machinery and equipment", LanguageId=LanguageId.English },
                              new(){ Name = "Будівельна техніка й устаткування", LanguageId=LanguageId.Ukrainian }
                          }
                   },
/*2- 107*/         new(){ UrlSlug = "ventilation-systems",
                          Image = "",
                          ParentId = 2,
                          CategoryTranslations = new List<CategoryTranslation>(){
                              new(){ Name = "Ventilation systems", LanguageId=LanguageId.English },
                              new(){ Name = "Вентиляційні системи", LanguageId=LanguageId.Ukrainian }
                          }
                   },
/*2- 108*/         new(){ UrlSlug = "swimming-pools",
                          Image = "",
                          ParentId = 2,
                          CategoryTranslations = new List<CategoryTranslation>(){
                              new(){ Name = "Swimming pools", LanguageId=LanguageId.English },
                              new(){ Name = "Басейни", LanguageId=LanguageId.Ukrainian }
                          }
                   },
/*2- 109*/         new(){ UrlSlug = "landscape-structures",
                          Image = "",
                          ParentId = 2,
                          CategoryTranslations = new List<CategoryTranslation>(){
                              new(){ Name = "Landscape structures", LanguageId=LanguageId.English },
                              new(){ Name = "Ландшафтні будови", LanguageId=LanguageId.Ukrainian }
                          }
                   },

/*3- 110*/         new(){ UrlSlug = "accessories-for-saunas-and-baths",
                          Image = "",
                          ParentId = 3,
                          CategoryTranslations = new List<CategoryTranslation>(){
                              new(){ Name = "Accessories for saunas and baths", LanguageId=LanguageId.English },
                              new(){ Name = "Аксесуари для саун і лазень", LanguageId=LanguageId.Ukrainian }
                          }
                   },
/*3- 111*/         new(){ UrlSlug = "decor",
                          Image = "",
                          ParentId = 3,
                          CategoryTranslations = new List<CategoryTranslation>(){
                              new(){ Name = "Decor", LanguageId=LanguageId.English },
                              new(){ Name = "Декор", LanguageId=LanguageId.Ukrainian }
                          }
                   },
/*3- 112*/         new(){ UrlSlug = "textile",
                          Image = "",
                          ParentId = 3,
                          CategoryTranslations = new List<CategoryTranslation>(){
                              new(){ Name = "Textile", LanguageId=LanguageId.English },
                              new(){ Name = "Текстиль", LanguageId=LanguageId.Ukrainian }
                          }
                   },         
/*3- 113*/         new(){ UrlSlug = "dishes",
                          Image = "",
                          ParentId = 3,
                          CategoryTranslations = new List<CategoryTranslation>(){
                              new(){ Name = "Dishes", LanguageId=LanguageId.English },
                              new(){ Name = "Посуд", LanguageId=LanguageId.Ukrainian }
                          }
                   },
/*3- 114*/         new(){ UrlSlug = "house-cares",
                          Image = "",
                          ParentId = 3,
                          CategoryTranslations = new List<CategoryTranslation>(){
                              new(){ Name = "House care", LanguageId=LanguageId.English },
                              new(){ Name = "Догляд за будинком", LanguageId=LanguageId.Ukrainian }
                          }
                   },
/*3- 115*/         new(){ UrlSlug = "all-for-preservation",
                          Image = "",
                          ParentId = 3,
                          CategoryTranslations = new List<CategoryTranslation>(){
                              new(){ Name = "All for preservation", LanguageId=LanguageId.English },
                              new(){ Name = "Все для консервації", LanguageId=LanguageId.Ukrainian }
                          }
                   },
/*3- 116*/         new(){ UrlSlug = "garden",
                          Image = "",
                          ParentId = 3,
                          CategoryTranslations = new List<CategoryTranslation>(){
                              new(){ Name = "Garden", LanguageId=LanguageId.English },
                              new(){ Name = "Сад", LanguageId=LanguageId.Ukrainian }
                          }
                   },


/*4- 117*/         new(){ UrlSlug = "bicycles-and-accessories",
                          Image = "",
                          ParentId = 4,
                          CategoryTranslations = new List<CategoryTranslation>(){
                              new(){ Name = "Bicycles and accessories", LanguageId=LanguageId.English },
                              new(){ Name = "Велосипеди та аксесуари", LanguageId=LanguageId.Ukrainian }
                          }
                   },
/*4- 118*/         new(){ UrlSlug = "sports-goods",
                          Image = "",
                          ParentId = 4,
                          CategoryTranslations = new List<CategoryTranslation>(){
                              new(){ Name = "Sports goods", LanguageId=LanguageId.English },
                              new(){ Name = "Спортивні товари", LanguageId=LanguageId.Ukrainian }
                          }
                   },
/*4- 119*/         new(){ UrlSlug = "goods-for-fishing",
                          Image = "",
                          ParentId = 4,
                          CategoryTranslations = new List<CategoryTranslation>(){
                              new(){ Name = "Goods for fishing", LanguageId=LanguageId.English },
                              new(){ Name = "Товари для риболовлі", LanguageId=LanguageId.Ukrainian }
                          }
                   },         
/*4- 120*/         new(){ UrlSlug = "goods-for-tourism-and-travel",
                          Image = "",
                          ParentId = 4,
                          CategoryTranslations = new List<CategoryTranslation>(){
                              new(){ Name = "Goods for tourism and travel", LanguageId=LanguageId.English },
                              new(){ Name = "Товари для туризму і подорожей", LanguageId=LanguageId.Ukrainian }
                          }
                   },
/*4- 121*/         new(){ UrlSlug = "trainers ",
                          Image = "",
                          ParentId = 4,
                          CategoryTranslations = new List<CategoryTranslation>(){
                              new(){ Name = "Trainers", LanguageId=LanguageId.English },
                              new(){ Name = "Тренажери", LanguageId=LanguageId.Ukrainian }
                          }
                   },
/*4- 122*/         new(){ UrlSlug = "extreme-sports",
                          Image = "",
                          ParentId = 4,
                          CategoryTranslations = new List<CategoryTranslation>(){
                              new(){ Name = "Extreme sports", LanguageId=LanguageId.English },
                              new(){ Name = "Екстремальні види спорту", LanguageId=LanguageId.Ukrainian }
                          }
                   },
/*4- 123*/         new(){ UrlSlug = "winter-sports",
                          Image = "",
                          ParentId = 4,
                          CategoryTranslations = new List<CategoryTranslation>(){
                              new(){ Name = "Winter sports", LanguageId=LanguageId.English },
                              new(){ Name = "Зимові види спорту", LanguageId=LanguageId.Ukrainian }
                          }
                   },
/*4- 124*/         new(){ UrlSlug = "goods-for-hunting",
                          Image = "",
                          ParentId = 4,
                          CategoryTranslations = new List<CategoryTranslation>(){
                              new(){ Name = "Goods for hunting", LanguageId=LanguageId.English },
                              new(){ Name = "Товари для полювання", LanguageId=LanguageId.Ukrainian }
                          }
                   },
/*4- 125*/         new(){ UrlSlug = "team-sports",
                          Image = "",
                          ParentId = 4,
                          CategoryTranslations = new List<CategoryTranslation>(){
                              new(){ Name = "Team sports", LanguageId=LanguageId.English },
                              new(){ Name = "Командні види спорту", LanguageId=LanguageId.Ukrainian }
                          }
                   },
/*4- 126*/         new(){ UrlSlug = "water-sports",
                          Image = "",
                          ParentId = 4,
                          CategoryTranslations = new List<CategoryTranslation>(){
                              new(){ Name = "Water sports", LanguageId=LanguageId.English },
                              new(){ Name = "Водні види спорту", LanguageId=LanguageId.Ukrainian }
                          }
                   },
/*4- 127*/         new(){ UrlSlug = "everything-for-the-beach",
                          Image = "",
                          ParentId = 4,
                          CategoryTranslations = new List<CategoryTranslation>(){
                              new(){ Name = "Everything for the beach", LanguageId=LanguageId.English },
                              new(){ Name = "Все для пляжу", LanguageId=LanguageId.Ukrainian }
                          }
                   },

/*5- 128*/         new(){ UrlSlug = "care-cosmetics",
                          Image = "",
                          ParentId = 5,
                          CategoryTranslations = new List<CategoryTranslation>(){
                              new(){ Name = "Care cosmetics", LanguageId=LanguageId.English },
                              new(){ Name = "Косметика по догляду", LanguageId=LanguageId.Ukrainian }
                          }
                   },
/*5- 129*/         new(){ UrlSlug = "everything-for-manicure-and-pedicure",
                          Image = "",
                          ParentId = 5,
                          CategoryTranslations = new List<CategoryTranslation>(){
                              new(){ Name = "Everything for manicure and pedicure", LanguageId=LanguageId.English },
                              new(){ Name = "Все для манікюру та педикюру", LanguageId=LanguageId.Ukrainian }
                          }
                   },
/*5- 130*/         new(){ UrlSlug = "perfumery",
                          Image = "",
                          ParentId = 5,
                          CategoryTranslations = new List<CategoryTranslation>(){
                              new(){ Name = "Perfumery", LanguageId=LanguageId.English },
                              new(){ Name = "Парфумерія", LanguageId=LanguageId.Ukrainian }
                          }
                   },         
/*5- 131*/         new(){ UrlSlug = "decorative-cosmetics",
                          Image = "",
                          ParentId = 5,
                          CategoryTranslations = new List<CategoryTranslation>(){
                              new(){ Name = "Decorative cosmetics", LanguageId=LanguageId.English },
                              new(){ Name = "Декоративна косметика", LanguageId=LanguageId.Ukrainian }
                          }
                   },
/*5- 132*/         new(){ UrlSlug = "childrens-cosmetics",
                          Image = "",
                          ParentId = 5,
                          CategoryTranslations = new List<CategoryTranslation>(){
                              new(){ Name = "Children's cosmetics", LanguageId=LanguageId.English },
                              new(){ Name = "Дитяча косметика", LanguageId=LanguageId.Ukrainian }
                          }
                   },
/*5- 133*/         new(){ UrlSlug = "mens-cosmetics",
                          Image = "",
                          ParentId = 5,
                          CategoryTranslations = new List<CategoryTranslation>(){
                              new(){ Name = "Men's cosmetics", LanguageId=LanguageId.English },
                              new(){ Name = "Чоловіча косметика", LanguageId=LanguageId.Ukrainian }
                          }
                   },

/*6- 134*/         new(){ UrlSlug = "toys",
                          Image = "",
                          ParentId = 6,
                          CategoryTranslations = new List<CategoryTranslation>(){
                              new(){ Name = "Toys", LanguageId=LanguageId.English },
                              new(){ Name = "Іграшки", LanguageId=LanguageId.Ukrainian }
                          }
                   },
/*6- 135*/         new(){ UrlSlug = "childrens-furniture",
                          Image = "",
                          ParentId = 6,
                          CategoryTranslations = new List<CategoryTranslation>(){
                              new(){ Name = "Children's furniture", LanguageId=LanguageId.English },
                              new(){ Name = "Дитячі меблі", LanguageId=LanguageId.Ukrainian }
                          }
                   },
/*6- 136*/         new(){ UrlSlug = "childrens-textiles",
                          Image = "",
                          ParentId = 6,
                          CategoryTranslations = new List<CategoryTranslation>(){
                              new(){ Name = "Children's textiles", LanguageId=LanguageId.English },
                              new(){ Name = "Дитячий текстиль", LanguageId=LanguageId.Ukrainian }
                          }
                   },         
/*6- 137*/         new(){ UrlSlug = "books-for-children",
                          Image = "",
                          ParentId = 6,
                          CategoryTranslations = new List<CategoryTranslation>(){
                              new(){ Name = "Books for children", LanguageId=LanguageId.English },
                              new(){ Name = "Книги для дітей", LanguageId=LanguageId.Ukrainian }
                          }
                   },
/*6- 138*/         new(){ UrlSlug = "childrens-games",
                          Image = "",
                          ParentId = 6,
                          CategoryTranslations = new List<CategoryTranslation>(){
                              new(){ Name = "Children's games", LanguageId=LanguageId.English },
                              new(){ Name = "Дитячі ігри", LanguageId=LanguageId.Ukrainian }
                          }
                   },

/*8- 139*/         new(){ UrlSlug = "womens-shoes",
                          Image = "",
                          ParentId = 8,
                          Sales=new List<Sale>() {clothesAndShoesSale },
                          CategoryTranslations = new List<CategoryTranslation>(){
                              new(){ Name = "Women's shoes", LanguageId=LanguageId.English },
                              new(){ Name = "Жіноче взуття", LanguageId=LanguageId.Ukrainian }
                          }
                   },
/*8- 140*/         new(){ UrlSlug = "mens-shoes",
                          Image = "",
                          ParentId = 8,
                          Sales=new List<Sale>() {clothesAndShoesSale },
                          CategoryTranslations = new List<CategoryTranslation>(){
                              new(){ Name = "Men's shoes", LanguageId=LanguageId.English },
                              new(){ Name = "Чоловіче взуття", LanguageId=LanguageId.Ukrainian }
                          }
                   },
/*8- 141*/         new(){ UrlSlug = "shoes-for-sports-and-active-recreation",
                          Image = "",
                          ParentId = 8,
                          Sales=new List<Sale>() {clothesAndShoesSale },
                          CategoryTranslations = new List<CategoryTranslation>(){
                              new(){ Name = "Shoes for sports and active recreation", LanguageId=LanguageId.English },
                              new(){ Name = "Взуття для спорту і активного відпочинку", LanguageId=LanguageId.Ukrainian }
                          }
                   },         
/*8- 142*/         new(){ UrlSlug = "childrens-and-teenagers-shoes",
                          Image = "",
                          ParentId = 8,
                          Sales=new List<Sale>() {clothesAndShoesSale },
                          CategoryTranslations = new List<CategoryTranslation>(){
                              new(){ Name = "Children's and teenagers' shoes", LanguageId=LanguageId.English },
                              new(){ Name = "Дитяче та підліткове взуття", LanguageId=LanguageId.Ukrainian }
                          }
                   },

/*9- 143*/         new(){ UrlSlug = "childrens-creativity-and-drawing",
                          Image = "",
                          ParentId = 9,
                          CategoryTranslations = new List<CategoryTranslation>(){
                              new(){ Name = "Children's creativity and drawing", LanguageId=LanguageId.English },
                              new(){ Name = "Дитяча творчість і малювання", LanguageId=LanguageId.Ukrainian }
                          }
                   },
/*9- 144*/         new(){ UrlSlug = "paper-and-paper-products",
                          Image = "",
                          ParentId = 9,
                          CategoryTranslations = new List<CategoryTranslation>(){
                              new(){ Name = "Paper and paper products", LanguageId=LanguageId.English },
                              new(){ Name = "Папір та паперові вироби", LanguageId=LanguageId.Ukrainian }
                          }
                   },
/*9- 145*/         new(){ UrlSlug = "stationery-accessories",
                          Image = "",
                          ParentId = 9,
                          CategoryTranslations = new List<CategoryTranslation>(){
                              new(){ Name = "Stationery accessories", LanguageId=LanguageId.English },
                              new(){ Name = "Письмове приладдя", LanguageId=LanguageId.Ukrainian }
                          }
                   },         
/*9- 146*/         new(){ UrlSlug = "school-supplies",
                          Image = "",
                          ParentId = 9,
                          CategoryTranslations = new List<CategoryTranslation>(){
                              new(){ Name = "School supplies", LanguageId=LanguageId.English },
                              new(){ Name = "Шкільне приладдя", LanguageId=LanguageId.Ukrainian }
                          }
                   },
/*9- 147*/         new(){ UrlSlug = "everything-for-business-administration",
                          Image = "",
                          ParentId = 9,
                          CategoryTranslations = new List<CategoryTranslation>(){
                              new(){ Name = "Everything for business administration", LanguageId=LanguageId.English },
                              new(){ Name = "Все для діловодства", LanguageId=LanguageId.Ukrainian }
                          }
                   },
/*9- 148*/         new(){ UrlSlug = "consumables-for-the-office",
                          Image = "",
                          ParentId = 9,
                          CategoryTranslations = new List<CategoryTranslation>(){
                              new(){ Name = "Consumables for the office", LanguageId=LanguageId.English },
                              new(){ Name = "Витратні матеріали для офісу", LanguageId=LanguageId.Ukrainian }
                          }
                   },

/*10- 149*/        new(){ UrlSlug = "hand-tool",
                          Image = "",
                          ParentId = 10,
                          CategoryTranslations = new List<CategoryTranslation>(){
                              new(){ Name = "Hand tool", LanguageId=LanguageId.English },
                              new(){ Name = "Ручний інструмент", LanguageId=LanguageId.Ukrainian }
                          }
                   },
/*10- 150*/        new(){ UrlSlug = "power-tools",
                          Image = "",
                          ParentId = 10,
                          CategoryTranslations = new List<CategoryTranslation>(){
                              new(){ Name = "Power tool", LanguageId=LanguageId.English },
                              new(){ Name = "Електроінструмент", LanguageId=LanguageId.Ukrainian }
                          }
                   },
/*10- 151*/        new(){ UrlSlug = "construction-tool",
                          Image = "",
                          ParentId = 10,
                          CategoryTranslations = new List<CategoryTranslation>(){
                              new(){ Name = "Construction tool", LanguageId=LanguageId.English },
                              new(){ Name = "Будівельний інструмент", LanguageId=LanguageId.Ukrainian }
                          }
                   },         
/*10- 152*/        new(){ UrlSlug = "pruning-tools",
                          Image = "",
                          ParentId = 10,
                          CategoryTranslations = new List<CategoryTranslation>(){
                              new(){ Name = "Pruning tools", LanguageId=LanguageId.English },
                              new(){ Name = "Інструменти для обрізки", LanguageId=LanguageId.Ukrainian }
                          }
                   },
/*10- 153*/        new(){ UrlSlug = "pneumatic-tool",
                          Image = "",
                          ParentId = 10,
                          CategoryTranslations = new List<CategoryTranslation>(){
                              new(){ Name = "Pneumatic tool", LanguageId=LanguageId.English },
                              new(){ Name = "Пневматичний інструмент", LanguageId=LanguageId.Ukrainian }
                          }
                   },
/*10- 154*/        new(){ UrlSlug = "metal-cutting-tool",
                          Image = "",
                          ParentId = 10,
                          CategoryTranslations = new List<CategoryTranslation>(){
                              new(){ Name = "Metal cutting tool", LanguageId=LanguageId.English },
                              new(){ Name = "Металорізальний інструмент", LanguageId=LanguageId.Ukrainian }
                          }
                   },

/*11- 155*/        new(){ UrlSlug = "books-magazines-printed-products",
                          Image = "",
                          ParentId = 11,
                          CategoryTranslations = new List<CategoryTranslation>(){
                              new(){ Name = "Books, magazines, printed products", LanguageId=LanguageId.English },
                              new(){ Name = "Книги, журнали, друкована продукція", LanguageId=LanguageId.Ukrainian }
                          }
                   },
/*11- 156*/        new(){ UrlSlug = "festive-decorations-and-fireworks",
                          Image = "",
                          ParentId = 11,
                          CategoryTranslations = new List<CategoryTranslation>(){
                              new(){ Name = "Festive decorations and fireworks", LanguageId=LanguageId.English },
                              new(){ Name = "Святкове оформлення та феєрверки", LanguageId=LanguageId.Ukrainian }
                          }
                   },
/*11- 157*/        new(){ UrlSlug = "gifts-for-the-home",
                          Image = "",
                          ParentId = 11,
                          CategoryTranslations = new List<CategoryTranslation>(){
                              new(){ Name = "Gifts for the home", LanguageId=LanguageId.English },
                              new(){ Name = "Подарунки для дому", LanguageId=LanguageId.Ukrainian }
                          }
                   },         
/*11- 158*/        new(){ UrlSlug = "gift-packaging",
                          Image = "",
                          ParentId = 11,
                          CategoryTranslations = new List<CategoryTranslation>(){
                              new(){ Name = "Gift packaging", LanguageId=LanguageId.English },
                              new(){ Name = "Подарункова упаковка", LanguageId=LanguageId.Ukrainian }
                          }
                   },
/*11- 159*/        new(){ UrlSlug = "business-gifts",
                          Image = "",
                          ParentId = 11,
                          CategoryTranslations = new List<CategoryTranslation>(){
                              new(){ Name = "Business gifts", LanguageId=LanguageId.English },
                              new(){ Name = "Бізнес-подарунки", LanguageId=LanguageId.Ukrainian }
                          }
                   },
/*11- 160*/        new(){ UrlSlug = "delicious-gifts",
                          Image = "",
                          ParentId = 11,
                          CategoryTranslations = new List<CategoryTranslation>(){
                              new(){ Name = "Delicious gifts", LanguageId=LanguageId.English },
                              new(){ Name = "Смачні подарунки", LanguageId=LanguageId.Ukrainian }
                          }
                   },
/*11- 161*/        new(){ UrlSlug = "original-gifts",
                          Image = "",
                          ParentId = 11,
                          CategoryTranslations = new List<CategoryTranslation>(){
                              new(){ Name = "Original gifts", LanguageId=LanguageId.English },
                              new(){ Name = "Оригінальні подарунки", LanguageId=LanguageId.Ukrainian }
                          }
                   },
/*11- 162*/        new(){ UrlSlug = "handmade-gifts-and-souvenirs",
                          Image = "",
                          ParentId = 11,
                          CategoryTranslations = new List<CategoryTranslation>(){
                              new(){ Name = "Handmade gifts and souvenirs", LanguageId=LanguageId.English },
                              new(){ Name = "Подарунки та сувеніри ручної роботи", LanguageId=LanguageId.Ukrainian }
                          }
                   },

/*12- 163*/        new(){ UrlSlug = "wrist-and-pocket-watches",
                          Image = "",
                          ParentId = 12,
                          CategoryTranslations = new List<CategoryTranslation>(){
                              new(){ Name = "Wrist and pocket watches", LanguageId=LanguageId.English },
                              new(){ Name = "Наручні і кишенькові годинники", LanguageId=LanguageId.Ukrainian }
                          }
                   },
/*12- 164*/        new(){ UrlSlug = "accessories ",
                          Image = "",
                          ParentId = 12,
                          CategoryTranslations = new List<CategoryTranslation>(){
                              new(){ Name = "Accessories ", LanguageId=LanguageId.English },
                              new(){ Name = "Аксесуари", LanguageId=LanguageId.Ukrainian }
                          }
                   },
/*12- 165*/        new(){ UrlSlug = "jewelry",
                          Image = "",
                          ParentId = 12,
                          CategoryTranslations = new List<CategoryTranslation>(){
                              new(){ Name = "Jewelry", LanguageId=LanguageId.English },
                              new(){ Name = "Ювелірні вироби", LanguageId=LanguageId.Ukrainian }
                          }
                   },         
/*12- 166*/        new(){ UrlSlug = "imitation-jewelry",
                          Image = "",
                          ParentId = 12,
                          CategoryTranslations = new List<CategoryTranslation>(){
                              new(){ Name = "Imitation jewelry", LanguageId=LanguageId.English },
                              new(){ Name = "Біжутерія", LanguageId=LanguageId.Ukrainian }
                          }
                   },
/*12- 167*/        new(){ UrlSlug = "childrens-accessories",
                          Image = "",
                          ParentId = 12,
                          CategoryTranslations = new List<CategoryTranslation>(){
                              new(){ Name = "Children's accessories", LanguageId=LanguageId.English },
                              new(){ Name = "Дитячі аксесуари", LanguageId=LanguageId.Ukrainian }
                          }
                   },
/*12- 168*/        new(){ UrlSlug = "natural-stone-jewelry",
                          Image = "",
                          ParentId = 12,
                          CategoryTranslations = new List<CategoryTranslation>(){
                              new(){ Name = "Natural stone jewelry", LanguageId=LanguageId.English },
                              new(){ Name = "Прикраси з натурального каменю", LanguageId=LanguageId.Ukrainian }
                          }
                   },



            };

            return categories;
        }

        #region OldCategory

        //        static IEnumerable<Category> GetPreconfiguredMarketplaceCategories(Sale clothesSale, Sale laptopSale)
        //        {
        //            var categories = new List<Category>
        //            {
        ///*1*/           new(){
        //                    UrlSlug = "beauty-and-health",
        //                    Image = "BeautyAndHealth.png",
        //                    LightIcon="BeautyAndHealthLight.png",
        //                    DarkIcon="BeautyAndHealthDark.png",
        //                    ActiveIcon="BeautyAndHealthActive.png",
        //                    ParentId = null,
        //                     CategoryTranslations=new List<CategoryTranslation>(){
        //                        new(){ Name = "Beauty and health", LanguageId=LanguageId.English },
        //                        new(){ Name = "Краса і здоров'я", LanguageId=LanguageId.Ukrainian } } },
        ///*2*/           new(){
        //                    UrlSlug = "house-and-garden",
        //                    Image = "HouseAndGarden.png",
        //                    LightIcon="HouseAndGardenLight.png",DarkIcon="HouseAndGardenDark.png",
        //                    ActiveIcon="HouseAndGardenActive.png",
        //                    ParentId = null,
        //                     CategoryTranslations=new List<CategoryTranslation>(){
        //                        new(){ Name = "House and garden",  LanguageId=LanguageId.English },
        //                        new(){ Name = "Дім і сад", LanguageId=LanguageId.Ukrainian } } },
        ///*3*/           new(){
        //                    UrlSlug = "clothes-and-shoes",
        //                    Image = "ClothesAndShoes.png",
        //                    LightIcon="ClothesAndShoesLight.png",
        //                    DarkIcon="ClothesAndShoesDark.png",
        //                    ActiveIcon="ClothesAndShoesActive.png",
        //                    ParentId = null,
        //                    Sales=new List<Sale>(){clothesSale},
        //                     CategoryTranslations = new List<CategoryTranslation>(){
        //                        new(){ Name = "Clothes and shoes", LanguageId=LanguageId.English },
        //                        new(){ Name = "Одяг та взуття", LanguageId=LanguageId.Ukrainian } } },
        ///*4*/           new(){ UrlSlug = "technology-and-electronics", Image = "TechnologyAndElectronics.png",LightIcon="TechnologyAndElectronicsLight.png",DarkIcon="TechnologyAndElectronicsDark.png",ActiveIcon="TechnologyAndElectronicsActive.png", ParentId = null,
        //                     CategoryTranslations=new List<CategoryTranslation>(){
        //                        new (){ Name = "Technology and electronics", LanguageId=LanguageId.English},
        //                        new (){ Name = "Техніка та електроніка", LanguageId=LanguageId.Ukrainian} } },
        ///*5*/           new()
        //                {
        //                    UrlSlug = "goods-for-children",
        //                    Image = "GoodsForChildren.png",
        //                    LightIcon = "GoodsForChildrenLight.png",
        //                    DarkIcon = "GoodsForChildrenDark.png",
        //                    ActiveIcon = "GoodsForChildrenActive.png",
        //                    ParentId = null,
        //                    CategoryTranslations = new List<CategoryTranslation>(){
        //                        new(){ Name = "Goods for children", LanguageId=LanguageId.English },
        //                        new(){ Name = "Товари для дітей", LanguageId=LanguageId.Ukrainian } }
        //                },
        ///*6*/           new()
        //                {
        //                    UrlSlug = "auto",
        //                    Image = "Auto.png",
        //                    ParentId = null,
        //                    CategoryTranslations = new List<CategoryTranslation>(){
        //                        new(){ Name = "Auto", LanguageId=LanguageId.English },
        //                        new(){ Name = "Авто", LanguageId=LanguageId.Ukrainian } }
        //                },
        ///*7*/           new()
        //                {
        //                    UrlSlug = "gifts-hobbies-books",
        //                    Image = "GiftsHobbiesBooks.png",
        //                    ParentId = null,
        //                    CategoryTranslations = new List<CategoryTranslation>(){
        //                        new(){ Name = "Gifts, hobbies, books", LanguageId=LanguageId.English },
        //                        new(){ Name = "Подарунки, хобі, книги", LanguageId=LanguageId.Ukrainian } }
        //                },
        ///*8*/           new()
        //                {
        //                    UrlSlug = "accessories-and-decorations",
        //                    Image = "AccessoriesAndDecorations.png",
        //                    ParentId = null,
        //                    CategoryTranslations = new List<CategoryTranslation>(){
        //                        new(){ Name = "Accessories and decorations", LanguageId=LanguageId.English },
        //                        new(){ Name = "Аксесуари та прикраси", LanguageId=LanguageId.Ukrainian } }
        //                },
        ///*9*/           new()
        //                {
        //                    UrlSlug = "materials-for-repair",
        //                    Image = "",
        //                    ParentId = null,
        //                    CategoryTranslations = new List<CategoryTranslation>(){
        //                        new(){ Name = "Materials for repair", LanguageId=LanguageId.English },
        //                        new(){ Name = "Матеріали для ремонту", LanguageId=LanguageId.Ukrainian } }
        //                },
        ///*10*/          new()
        //                {
        //                    UrlSlug = "sports-and-recreation",
        //                    Image = "SportsAndRecreation.png",
        //                    LightIcon = "SportsAndRecreationLight.png",
        //                    DarkIcon = "SportsAndRecreationDark.png",
        //                    ActiveIcon = "SportsAndRecreationActive.png",
        //                    ParentId = null,
        //                    CategoryTranslations = new List<CategoryTranslation>(){
        //                        new(){ Name = "Sports and recreation", LanguageId=LanguageId.English },
        //                        new(){ Name = "Спорт і відпочинок", LanguageId=LanguageId.Ukrainian } }
        //                },
        ///*11*/          new()
        //                {
        //                    UrlSlug = "medicines-and-medical-products",
        //                    Image = "",
        //                    ParentId = null,
        //                    CategoryTranslations = new List<CategoryTranslation>(){
        //                        new(){ Name = "Medicines and medical products", LanguageId=LanguageId.English },
        //                        new(){ Name = "Медикаменти та медичні товари", LanguageId=LanguageId.Ukrainian } }
        //                },
        ///*12*/          new()
        //                {
        //                    UrlSlug = "pets-and-pet-products",
        //                    Image = "",
        //                    ParentId = null,
        //                    CategoryTranslations = new List<CategoryTranslation>(){
        //                        new(){ Name = "Pets and pet products", LanguageId=LanguageId.English },
        //                        new(){ Name = "Домашні тварини та зоотовари", LanguageId=LanguageId.Ukrainian } }
        //                },
        ///*13*/          new()
        //                {
        //                    UrlSlug = "stationery",
        //                    Image = "Stationery.png",
        //                    LightIcon = "StationeryLight.png",
        //                    DarkIcon = "StationeryDark.png",
        //                    ActiveIcon = "StationeryActive.png",
        //                    ParentId = null,
        //                    CategoryTranslations = new List<CategoryTranslation>(){
        //                        new(){ Name = "Stationery",  LanguageId=LanguageId.English },
        //                        new(){ Name = "Канцтовари", LanguageId=LanguageId.Ukrainian } }
        //                },
        ///*14*/          new()
        //                {
        //                    UrlSlug = "overalls-and-shoes",
        //                    Image = "",
        //                    ParentId = null,
        //                    CategoryTranslations = new List<CategoryTranslation>(){
        //                        new(){ Name = "Overalls and shoes", LanguageId=LanguageId.English },
        //                        new(){ Name = "Спецодяг та взуття", LanguageId=LanguageId.Ukrainian } }
        //                },
        ///*15*/          new()
        //                {
        //                    UrlSlug = "wedding-goods",
        //                    Image = "",
        //                    ParentId = null,
        //                    CategoryTranslations = new List<CategoryTranslation>(){
        //                        new(){ Name = "Wedding goods", LanguageId=LanguageId.English },
        //                        new(){ Name = "Весільні товари", LanguageId=LanguageId.Ukrainian } }
        //                },
        ///*16*/          new()
        //                {
        //                    UrlSlug = "food-products-drinks",
        //                    Image = "",
        //                    ParentId = null,
        //                    CategoryTranslations = new List<CategoryTranslation>(){
        //                        new(){ Name = "Food products, drinks", LanguageId=LanguageId.English },
        //                        new(){ Name = "Продукти харчування, напої", LanguageId=LanguageId.Ukrainian } }
        //                },
        ///*17*/          new()
        //                {
        //                    UrlSlug = "tools",
        //                    Image = "Tools.png",
        //                    LightIcon = "ToolsLight.png",
        //                    DarkIcon = "ToolsDark.png",
        //                    ActiveIcon = "ToolsActive.png",
        //                    ParentId = null,
        //                    CategoryTranslations = new List<CategoryTranslation>(){
        //                        new(){ Name = "Tools", LanguageId=LanguageId.English },
        //                        new(){ Name = "Інструменти", LanguageId=LanguageId.Ukrainian } }
        //                },
        ///*18*/          new()
        //                {
        //                    UrlSlug = "antiques-and-collectibles",
        //                    Image = "",
        //                    ParentId = null,
        //                    CategoryTranslations = new List<CategoryTranslation>(){
        //                        new(){ Name = "Antiques and collectibles", LanguageId=LanguageId.English },
        //                        new(){ Name = "Антикваріат і колекціонування", LanguageId=LanguageId.Ukrainian } }
        //                },
        ///*19*/          new()
        //                {
        //                    UrlSlug = "сonstruction",
        //                    Image = "Construction.png",
        //                    LightIcon = "ConstructionLight.png",
        //                    DarkIcon = "ConstructionDark.png",
        //                    ActiveIcon = "ConstructionActive.png",
        //                    ParentId = null,
        //                    CategoryTranslations = new List<CategoryTranslation>(){
        //                        new(){ Name = "Construction", LanguageId=LanguageId.English },
        //                        new(){ Name = "Будівництво", LanguageId=LanguageId.Ukrainian } }
        //                },

        ///*20*/          new()
        //                {
        //                    UrlSlug = "mens-clothing",
        //                    Image = "",
        //                    ParentId = 3,
        //                    Sales=new List<Sale>(){clothesSale},
        //                    CategoryTranslations = new List<CategoryTranslation>(){
        //                        new(){ Name = "Men's clothing", LanguageId=LanguageId.English },
        //                        new(){ Name = "Чоловічий одяг", LanguageId=LanguageId.Ukrainian } }
        //                },
        ///*21*/          new()
        //                {
        //                    UrlSlug = "womens-clothes",
        //                    Image = "",
        //                    ParentId = 3,
        //                    Sales=new List<Sale>(){clothesSale},
        //                    CategoryTranslations = new List<CategoryTranslation>(){
        //                        new(){ Name = "Women's clothes", LanguageId=LanguageId.English },
        //                        new(){ Name = "Жіночий одяг", LanguageId=LanguageId.Ukrainian } }
        //                },
        ///*22*/          new()
        //                {
        //                    UrlSlug = "Childrens-clothes-shoes-accessories",
        //                    Image = "",
        //                    ParentId = 3,
        //                    CategoryTranslations = new List<CategoryTranslation>(){
        //                        new(){ Name = "Children's clothes, shoes, accessories", LanguageId=LanguageId.English },
        //                        new(){ Name = "Дитячі одяг, взуття, аксесуари", LanguageId=LanguageId.Ukrainian } }
        //                },
        ///*23*/          new()
        //                {
        //                    UrlSlug = "sportswear-and-footwear",
        //                    Image = "",
        //                    ParentId = 3,
        //                    Sales=new List<Sale>(){clothesSale},
        //                    CategoryTranslations = new List<CategoryTranslation>(){
        //                        new(){ Name = "Sportswear and footwear", LanguageId=LanguageId.English },
        //                        new(){ Name = "Спортивний одяг та взуття", LanguageId=LanguageId.Ukrainian } }
        //                },
        ///*24*/          new()
        //                {
        //                    UrlSlug = "footwear",
        //                    Image = "",
        //                    ParentId = 3,
        //                    Sales=new List<Sale>(){clothesSale},
        //                    CategoryTranslations = new List<CategoryTranslation>(){
        //                        new(){ Name = "Footwear", LanguageId=LanguageId.English },
        //                        new(){ Name = "Взуття", LanguageId=LanguageId.Ukrainian } }
        //                },
        ///*25*/          new()
        //                {
        //                    UrlSlug = "carnival-costumes",
        //                    Image = "",
        //                    ParentId = 3,
        //                    Sales=new List<Sale>(){clothesSale},
        //                    CategoryTranslations = new List<CategoryTranslation>(){
        //                        new(){ Name = "Carnival costumes", LanguageId=LanguageId.English },
        //                        new(){ Name = "Карнавальні костюми", LanguageId=LanguageId.Ukrainian } }
        //                },
        ///*26*/          new()
        //                {
        //                    UrlSlug = "ethnic-clothing",
        //                    Image = "",
        //                    ParentId = 3,
        //                    Sales=new List<Sale>(){clothesSale},
        //                    CategoryTranslations = new List<CategoryTranslation>(){
        //                        new(){ Name = "Ethnic clothing", LanguageId=LanguageId.English },
        //                        new(){ Name = "Етнічний одяг", LanguageId=LanguageId.Ukrainian } }
        //                },

        ///*27*/          new()
        //                {
        //                    UrlSlug = "computer-equipment-and-software",
        //                    Image = "",
        //                    ParentId = 4,
        //                    CategoryTranslations = new List<CategoryTranslation>(){
        //                        new(){ Name = "Computer equipment and software", LanguageId=LanguageId.English },
        //                        new(){ Name = "Комп'ютерна техніка та ПЗ", LanguageId=LanguageId.Ukrainian } }
        //                },
        ///*28*/          new()
        //                {
        //                    UrlSlug = "household-appliances",
        //                    Image = "",
        //                    ParentId = 4,
        //                    CategoryTranslations = new List<CategoryTranslation>(){
        //                        new(){ Name = "Household appliances", LanguageId=LanguageId.English },
        //                        new(){ Name = "Побутова техніка", LanguageId=LanguageId.Ukrainian } }
        //                },
        ///*29*/          new()
        //                {
        //                    UrlSlug = "phones-and-accessories",
        //                    Image = "",
        //                    ParentId = 4,
        //                    CategoryTranslations = new List<CategoryTranslation>(){
        //                        new(){ Name = "Phones and accessories", LanguageId=LanguageId.English },
        //                        new(){ Name = "Телефони та аксесуари", LanguageId=LanguageId.Ukrainian } }
        //                },
        ///*30*/          new()
        //                {
        //                    UrlSlug = "audio-equipment-and-accessories",
        //                    Image = "",
        //                    ParentId = 4,
        //                    CategoryTranslations = new List<CategoryTranslation>(){
        //                        new(){ Name = "Audio equipment and accessories", LanguageId=LanguageId.English },
        //                        new(){ Name = "Аудіотехніка і аксесуари", LanguageId=LanguageId.Ukrainian } }
        //                },
        ///*31*/          new()
        //                {
        //                    UrlSlug = "spare-parts-for-machinery-and-electronics",
        //                    Image = "",
        //                    ParentId = 4,
        //                    CategoryTranslations = new List<CategoryTranslation>(){
        //                        new(){ Name = "Spare parts for machinery and electronics", LanguageId=LanguageId.English},
        //                        new(){ Name = "Запчастини для техніки та електроніки", LanguageId=LanguageId.Ukrainian } }
        //                },
        ///*32*/          new()
        //                {
        //                    UrlSlug = "tv-and-video-equipment",
        //                    Image = "",
        //                    ParentId = 4,
        //                    CategoryTranslations = new List<CategoryTranslation>(){
        //                        new(){  Name = "TV and video equipment", LanguageId=LanguageId.English },
        //                        new(){ Name = "TV та відеотехніка", LanguageId=LanguageId.Ukrainian } }
        //                },
        ///*33*/          new()
        //                {
        //                    UrlSlug = "car-electronics",
        //                    Image = "",
        //                    ParentId = 4,
        //                    CategoryTranslations = new List<CategoryTranslation>(){
        //                        new(){ Name = "Car electronics", LanguageId=LanguageId.English },
        //                        new(){ Name = "Автомобільна електроніка", LanguageId=LanguageId.Ukrainian } }
        //                },
        ///*34*/          new()
        //                {
        //                    UrlSlug = "photos-camcorders-and-accessories",
        //                    Image = "",
        //                    ParentId = 4,
        //                    CategoryTranslations = new List<CategoryTranslation>(){
        //                        new(){ Name = "Photos, camcorders and accessories", LanguageId=LanguageId.English },
        //                        new(){ Name = "Фото, відеокамери та аксесуари", LanguageId=LanguageId.Ukrainian } }
        //                },
        ///*35*/          new()
        //                {
        //                    UrlSlug = "3d-devices",
        //                    Image = "",
        //                    ParentId = 4,
        //                    CategoryTranslations = new List<CategoryTranslation>(){
        //                        new(){Name = "3d devices", LanguageId=LanguageId.English },
        //                        new(){ Name = "3d пристрої", LanguageId=LanguageId.Ukrainian } }
        //                },
        ///*36*/          new()
        //                {
        //                    UrlSlug = "equipment-for-satellite-internet",
        //                    Image = "",
        //                    ParentId = 4,
        //                    CategoryTranslations = new List<CategoryTranslation>(){
        //                        new(){Name = "Equipment for satellite internet",  LanguageId=LanguageId.English },
        //                        new(){ Name = "Обладнання для супутникового інтернету", LanguageId=LanguageId.Ukrainian } }
        //                },

        ///*37*/          new()
        //                {
        //                    UrlSlug = "tablet-computers",
        //                    Image = "",
        //                    ParentId = 27,
        //                    CategoryTranslations = new List<CategoryTranslation>(){
        //                        new(){ Name = "Tablet computers", LanguageId=LanguageId.English },
        //                        new(){ Name = "Планшетні комп'ютери", LanguageId=LanguageId.Ukrainian } }
        //                },
        ///*38*/          new()
        //                {
        //                    UrlSlug = "laptops",
        //                    Image = "",
        //                    ParentId = 27,
        //                    Sales=new List<Sale>(){laptopSale},
        //                    CategoryTranslations = new List<CategoryTranslation>(){
        //                        new(){  Name = "Laptops", LanguageId=LanguageId.English },
        //                        new(){ Name = "Ноутбуки", LanguageId=LanguageId.Ukrainian } }
        //                },
        ///*39*/          new()
        //                {
        //                    UrlSlug = "monitors",
        //                    Image = "",
        //                    ParentId = 27,
        //                    CategoryTranslations = new List<CategoryTranslation>(){
        //                        new(){ Name = "Monitors", LanguageId=LanguageId.English },
        //                        new(){ Name = "Монітори", LanguageId=LanguageId.Ukrainian } }
        //                },
        ///*40*/          new()
        //                {
        //                    UrlSlug = "components-for-computer-equipment",
        //                    Image = "",
        //                    ParentId = 27,
        //                    CategoryTranslations = new List<CategoryTranslation>(){
        //                        new(){ Name = "Components for computer equipment",LanguageId=LanguageId.English },
        //                        new(){ Name = "Комплектуючі для комп'ютерної техніки", LanguageId=LanguageId.Ukrainian } }
        //                },
        ///*41*/          new()
        //                {
        //                    UrlSlug = "computer-accessories",
        //                    Image = "",
        //                    ParentId = 27,
        //                    CategoryTranslations = new List<CategoryTranslation>(){
        //                        new(){ Name = "Computer accessories", LanguageId=LanguageId.English},
        //                        new(){ Name = "Комп'ютерні аксесуари", LanguageId=LanguageId.Ukrainian } }
        //                },
        ///*42*/          new()
        //                {
        //                    UrlSlug = "smart-watches-and-fitness-bracelets",
        //                    Image = "",
        //                    ParentId = 27,
        //                    CategoryTranslations = new List<CategoryTranslation>(){
        //                        new(){ Name = "Smart watches and fitness bracelets", LanguageId=LanguageId.English },
        //                        new(){ Name = "Розумні годинники та фітнес браслети", LanguageId=LanguageId.Ukrainian } }
        //                },
        ///*43*/          new()
        //                {
        //                    UrlSlug = "printers-scanners-mfps-and-components",
        //                    Image = "",
        //                    ParentId = 27,
        //                    CategoryTranslations = new List<CategoryTranslation>(){
        //                        new(){ Name = "Printers, scanners, MFPs and components", LanguageId=LanguageId.English },
        //                        new(){ Name = "Принтери, сканери, МФУ та комплектуючі", LanguageId=LanguageId.Ukrainian } }
        //                },
        ///*44*/          new()
        //                {
        //                    UrlSlug = "information-carriers",
        //                    Image = "",
        //                    ParentId = 27,
        //                    CategoryTranslations = new List<CategoryTranslation>(){
        //                        new(){ Name = "Information carriers", LanguageId=LanguageId.English },
        //                        new(){ Name = "Носії інформації", LanguageId=LanguageId.Ukrainian } }
        //                },
        ///*45*/          new()
        //                {
        //                    UrlSlug = "game-consoles",
        //                    Image = "",
        //                    ParentId = 27,
        //                    CategoryTranslations = new List<CategoryTranslation>(){
        //                        new(){ Name = "Game consoles", LanguageId=LanguageId.English },
        //                        new(){ Name = "Ігрові приставки", LanguageId=LanguageId.Ukrainian } }
        //                },
        ///*46*/          new()
        //                {
        //                    UrlSlug = "desktops",
        //                    Image = "",
        //                    ParentId = 27,
        //                    CategoryTranslations = new List<CategoryTranslation>(){
        //                        new(){ Name = "Desktops", LanguageId=LanguageId.English },
        //                        new(){ Name = "Настільні комп'ютери", LanguageId=LanguageId.Ukrainian } }
        //                },
        ///*47*/          new()
        //                {
        //                    UrlSlug = "software",
        //                    Image = "",
        //                    ParentId = 27,
        //                    CategoryTranslations = new List<CategoryTranslation>(){
        //                        new(){ Name = "Software", LanguageId=LanguageId.English },
        //                        new(){ Name = "Програмне забезпечення", LanguageId=LanguageId.Ukrainian } }
        //                },
        ///*48*/          new()
        //                {
        //                    UrlSlug = "server-equipment",
        //                    Image = "",
        //                    ParentId = 27,
        //                    CategoryTranslations = new List<CategoryTranslation>(){
        //                        new(){ Name = "Server equipment", LanguageId=LanguageId.English },
        //                        new(){ Name = "Серверне обладнання", LanguageId=LanguageId.Ukrainian } }
        //                },
        ///*49*/          new()
        //                {
        //                    UrlSlug = "mining-equipment",
        //                    Image = "",
        //                    ParentId = 27,
        //                    CategoryTranslations = new List<CategoryTranslation>(){
        //                        new(){ Name = "Mining equipment", LanguageId=LanguageId.English },
        //                        new(){ Name = "Обладнання для майнінгу", LanguageId=LanguageId.Ukrainian } }
        //                },
        ///*50*/          new()
        //                {
        //                    UrlSlug = "e-books",
        //                    Image = "",
        //                    ParentId = 27,
        //                    CategoryTranslations = new List<CategoryTranslation>(){
        //                        new(){ Name = "E-books",  LanguageId=LanguageId.English },
        //                        new(){ Name = "Електронні книги (пристрій)", LanguageId=LanguageId.Ukrainian } }
        //                },
        ///*51*/          new()
        //                {
        //                    UrlSlug = "single-board-computers-and-nettops",
        //                    Image = "",
        //                    ParentId = 27,
        //                    CategoryTranslations = new List<CategoryTranslation>(){
        //                        new(){ Name = "Single board computers and nettops", LanguageId=LanguageId.English },
        //                        new(){ Name = "Одноплатні комп'ютери та неттопи", LanguageId=LanguageId.Ukrainian } }
        //                },
        ///*52*/          new()
        //                {
        //                    UrlSlug = "portable-electronic-translators",
        //                    Image = "",
        //                    ParentId = 27,
        //                    CategoryTranslations = new List<CategoryTranslation>(){
        //                        new(){ Name = "Portable electronic translators", LanguageId=LanguageId.English },
        //                        new(){ Name = "Портативні електронні перекладачі", LanguageId=LanguageId.Ukrainian } }
        //                },

        //                new()
        //                {
        //                    UrlSlug = "cables-for-electronics",
        //                    Image = "",
        //                    ParentId = 40,
        //                    CategoryTranslations = new List<CategoryTranslation>(){
        //                        new(){ Name = "Cables for electronics", LanguageId=LanguageId.English },
        //                        new(){ Name = "Кабелі для електроніки", LanguageId=LanguageId.Ukrainian } }
        //                },
        //                new()
        //                {
        //                    UrlSlug = "hdd-ssd",
        //                    Image = "",
        //                    ParentId = 40,
        //                    CategoryTranslations = new List<CategoryTranslation>(){
        //                        new(){ Name = "HDD, SSD", LanguageId=LanguageId.English },
        //                        new(){ Name = "Внутрішні та зовнішні жорсткі диски, HDD, SSD", LanguageId=LanguageId.Ukrainian } }
        //                },
        //                new()
        //                {
        //                    UrlSlug = "batteries-for-laptops-tablets-e-books-translators",
        //                    Image = "",
        //                    ParentId = 40,
        //                    CategoryTranslations = new List<CategoryTranslation>(){
        //                        new(){ Name = "Batteries for laptops, tablets, e-books, translators", LanguageId=LanguageId.English },
        //                        new(){ Name = "Акумулятори для ноутбуків, планшетів, електронних книг, перекладачів", LanguageId=LanguageId.Ukrainian } }
        //                },
        //                new()
        //                {
        //                    UrlSlug = "laptop-chargers",
        //                    Image = "",
        //                    ParentId = 40,
        //                    CategoryTranslations = new List<CategoryTranslation>(){
        //                        new(){ Name = "Laptop chargers", LanguageId=LanguageId.English },
        //                        new(){ Name = "Зарядні пристрої для ноутбуків", LanguageId=LanguageId.Ukrainian } }
        //                },
        //                new()
        //                {
        //                    UrlSlug = "laptop-body-parts",
        //                    Image = "",
        //                    ParentId = 40,
        //                    CategoryTranslations = new List<CategoryTranslation>(){
        //                        new(){ Name = "Laptop body parts", LanguageId=LanguageId.English },
        //                        new(){ Name = "Частини корпусу ноутбука", LanguageId=LanguageId.Ukrainian } }
        //                },
        //                new()
        //                {
        //                    UrlSlug = "memory-modules",
        //                    Image = "",
        //                    ParentId = 40,
        //                    CategoryTranslations = new List<CategoryTranslation>(){
        //                        new(){ Name = "Memory modules", LanguageId=LanguageId.English },
        //                        new(){ Name = "Модулі пам'яті", LanguageId=LanguageId.Ukrainian } }
        //                },
        //                new()
        //                {
        //                    UrlSlug = "processors",
        //                    Image = "",
        //                    ParentId = 40,
        //                    CategoryTranslations = new List<CategoryTranslation>(){
        //                        new(){ Name = "Processors", LanguageId=LanguageId.English },
        //                        new(){ Name = "Процесори", LanguageId=LanguageId.Ukrainian } }
        //                },
        //                new()
        //                {
        //                    UrlSlug = "coolers-and-cooling-systems",
        //                    Image = "",
        //                    ParentId = 40,
        //                    CategoryTranslations = new List<CategoryTranslation>(){
        //                        new(){ Name = "Coolers and cooling systems", LanguageId=LanguageId.English },
        //                        new(){ Name = "Кулери і системи охолодження", LanguageId=LanguageId.Ukrainian } }
        //                },
        //                new()
        //                {
        //                    UrlSlug = "matrixes-for-laptops-tablets-and-monitors",
        //                    Image = "",
        //                    ParentId = 40,
        //                    CategoryTranslations = new List<CategoryTranslation>(){
        //                        new(){ Name = "Matrixes for laptops, tablets and monitors", LanguageId=LanguageId.English },
        //                        new(){ Name = "Матриці для ноутбуків, планшетів і моніторів", LanguageId=LanguageId.Ukrainian } }
        //                },
        //                new()
        //                {
        //                    UrlSlug = "cables-and-connectors-for-laptops-computers-tablets",
        //                    Image = "",
        //                    ParentId = 40,
        //                    CategoryTranslations = new List<CategoryTranslation>(){
        //                        new(){ Name = "Cables and connectors for laptops, computers, tablets", LanguageId=LanguageId.English },
        //                        new(){ Name = "Шлейфи та роз'єми для ноутбуків, комп'ютерів, планшетів", LanguageId=LanguageId.Ukrainian } }
        //                },
        //                new()
        //                {
        //                    UrlSlug = "keyboard-blocks-for-laptops",
        //                    Image = "",
        //                    ParentId = 40,
        //                    CategoryTranslations = new List<CategoryTranslation>(){
        //                        new(){ Name = "Keyboard blocks for laptops", LanguageId=LanguageId.English },
        //                        new(){ Name = "Клавіатурні блоки для ноутбуків", LanguageId=LanguageId.Ukrainian } }
        //                },
        //                new()
        //                {
        //                    UrlSlug = "touchscreen-for-displays",
        //                    Image = "",
        //                    ParentId = 40,
        //                    CategoryTranslations = new List<CategoryTranslation>(){
        //                        new(){ Name = "Touchscreen for displays", LanguageId=LanguageId.English },
        //                        new(){ Name = "Touchscreen для дисплеїв", LanguageId=LanguageId.Ukrainian } }
        //                },
        //                new()
        //                {
        //                    UrlSlug = "microcircuits",
        //                    Image = "",
        //                    ParentId = 40,
        //                    CategoryTranslations = new List<CategoryTranslation>(){
        //                        new(){ Name = "Microcircuits", LanguageId=LanguageId.English },
        //                        new(){ Name = "Мікросхеми", LanguageId=LanguageId.Ukrainian } }
        //                },
        //                new()
        //                {
        //                    UrlSlug = "spare-parts-for-tvs-and-monitors",
        //                    Image = "",
        //                    ParentId = 40,
        //                    CategoryTranslations = new List<CategoryTranslation>(){
        //                        new(){ Name = "Spare parts for TVs and monitors", LanguageId=LanguageId.English },
        //                        new(){ Name = "Запчастини для телевізорів і моніторів", LanguageId=LanguageId.Ukrainian } }
        //                },
        //                new()
        //                {
        //                    UrlSlug = "motherboards",
        //                    Image = "",
        //                    ParentId = 40,
        //                    CategoryTranslations = new List<CategoryTranslation>(){
        //                        new(){ Name = "Motherboards", LanguageId=LanguageId.English },
        //                        new(){ Name = "Материнські плати", LanguageId=LanguageId.Ukrainian } }
        //                },
        //                new()
        //                {
        //                    UrlSlug = "video-cards",
        //                    Image = "",
        //                    ParentId = 40,
        //                    CategoryTranslations = new List<CategoryTranslation>(){
        //                        new(){ Name = "Video cards", LanguageId=LanguageId.English},
        //                        new(){ Name = "Відеокарти", LanguageId=LanguageId.Ukrainian } }
        //                },
        //                new()
        //                {
        //                    UrlSlug = "enclosures-for-computers",
        //                    Image = "",
        //                    ParentId = 40,
        //                    CategoryTranslations = new List<CategoryTranslation>(){
        //                        new(){ Name = "Enclosures for computers", LanguageId=LanguageId.English },
        //                        new(){ Name = "Корпуси для комп'ютерів", LanguageId=LanguageId.Ukrainian } }
        //                },
        //                new()
        //                {
        //                    UrlSlug = "power-supplies-for-computers",
        //                    Image = "",
        //                    ParentId = 40,
        //                    CategoryTranslations = new List<CategoryTranslation>(){
        //                        new(){ Name = "Power supplies for computers", LanguageId=LanguageId.English },
        //                        new(){ Name = "Блоки живлення для комп'ютерів", LanguageId=LanguageId.Ukrainian } }
        //                },
        //                new()
        //                {
        //                    UrlSlug = "patch-cord",
        //                    Image = "",
        //                    ParentId = 40,
        //                    CategoryTranslations = new List<CategoryTranslation>(){
        //                        new(){ Name = "Patch cord", LanguageId=LanguageId.English },
        //                        new(){ Name = "Патч-корди", LanguageId=LanguageId.Ukrainian } }
        //                },
        //                new()
        //                {
        //                    UrlSlug = "pockets-for-hard-drives",
        //                    Image = "",
        //                    ParentId = 40,
        //                    CategoryTranslations = new List<CategoryTranslation>(){
        //                        new(){ Name = "Pockets for hard drives", LanguageId=LanguageId.English },
        //                        new(){ Name = "Кишені для жорстких дисків", LanguageId=LanguageId.Ukrainian } }
        //                },
        //                new()
        //                {
        //                    UrlSlug = "adapters-and-port-expansion-cards",
        //                    Image = "",
        //                    ParentId = 40,
        //                    CategoryTranslations = new List<CategoryTranslation>(){
        //                        new(){ Name = "Adapters and port expansion cards", LanguageId=LanguageId.English },
        //                        new(){ Name = "Адаптери і плати розширення портів", LanguageId=LanguageId.Ukrainian } }
        //                },
        //                new()
        //                {
        //                    UrlSlug = "audio-parts-for-laptops",
        //                    Image = "",
        //                    ParentId = 40,
        //                    CategoryTranslations = new List<CategoryTranslation>(){
        //                        new(){ Name = "Audio parts for laptops", LanguageId=LanguageId.English },
        //                        new(){ Name = "Звукові запчастини для портативних ПК", LanguageId=LanguageId.Ukrainian } }
        //                },
        //                new()
        //                {
        //                    UrlSlug = "thermal-paste",
        //                    Image = "",
        //                    ParentId = 40,
        //                    CategoryTranslations = new List<CategoryTranslation>(){
        //                        new(){ Name = "Thermal paste", LanguageId=LanguageId.English },
        //                        new(){ Name = "Термопаста", LanguageId=LanguageId.Ukrainian } }
        //                },
        //                new()
        //                {
        //                    UrlSlug = "sound-cards",
        //                    Image = "",
        //                    ParentId = 40,
        //                    CategoryTranslations = new List<CategoryTranslation>(){
        //                        new(){ Name = "Sound cards", LanguageId=LanguageId.English },
        //                        new(){ Name = "Звукові карти", LanguageId=LanguageId.Ukrainian } }
        //                },
        //                new()
        //                {
        //                    UrlSlug = "network-cards",
        //                    Image = "",
        //                    ParentId = 40,
        //                    CategoryTranslations = new List<CategoryTranslation>(){
        //                        new(){ Name = "Network cards", LanguageId=LanguageId.English },
        //                        new(){ Name = "Мережеві карти", LanguageId=LanguageId.Ukrainian } }
        //                },
        //                new()
        //                {
        //                    UrlSlug = "optical-drives",
        //                    Image = "",
        //                    ParentId = 40,
        //                    CategoryTranslations = new List<CategoryTranslation>(){
        //                        new(){ Name = "Optical drives", LanguageId=LanguageId.English },
        //                        new(){ Name = "Оптичні приводи", LanguageId=LanguageId.Ukrainian } }
        //                },
        //                new()
        //                {
        //                    UrlSlug = "cases-for-tablets",
        //                    Image = "",
        //                    ParentId = 40,
        //                    CategoryTranslations = new List<CategoryTranslation>(){
        //                        new(){ Name = "Cases for tablets", LanguageId=LanguageId.English },
        //                        new(){ Name = "Корпуси для планшетів", LanguageId=LanguageId.Ukrainian } }
        //                },
        //                new()
        //                {
        //                    UrlSlug = "accessories-for-matrices-and-displays",
        //                    Image = "",
        //                    ParentId = 40,
        //                    CategoryTranslations = new List<CategoryTranslation>(){
        //                        new(){ Name = "Accessories for matrices and displays", LanguageId=LanguageId.English },
        //                        new(){ Name = "Комплектуючі для матриць та дисплеїв", LanguageId=LanguageId.Ukrainian } }
        //                },
        //                new()
        //                {
        //                    UrlSlug = "cameras-for-laptops",
        //                    Image = "",
        //                    ParentId = 40,
        //                    CategoryTranslations = new List<CategoryTranslation>(){
        //                        new(){ Name = "Cameras for laptops", LanguageId=LanguageId.English },
        //                        new(){ Name = "Камери для портативних ПК", LanguageId=LanguageId.Ukrainian } }
        //                },
        //                new()
        //                {
        //                    UrlSlug = "cooling-systems-for-laptops",
        //                    Image = "",
        //                    ParentId = 40,
        //                    CategoryTranslations = new List<CategoryTranslation>(){
        //                        new(){ Name = "Cooling systems for laptops", LanguageId=LanguageId.English },
        //                        new(){ Name = "Системи охолодження для ноутбуків", LanguageId=LanguageId.Ukrainian } }
        //                },
        //                new()
        //                {
        //                    UrlSlug = "tv-and-fm-tuners",
        //                    Image = "",
        //                    ParentId = 40,
        //                    CategoryTranslations = new List<CategoryTranslation>(){
        //                        new(){ Name = "TV and FM tuners", LanguageId=LanguageId.English },
        //                        new(){ Name = "TV-тюнери і FM-тюнери", LanguageId=LanguageId.Ukrainian } }
        //                },
        //                new()
        //                {
        //                    UrlSlug = "postcards",
        //                    Image = "",
        //                    ParentId = 40,
        //                    CategoryTranslations = new List<CategoryTranslation>(){
        //                        new(){ Name = "Postcards", LanguageId=LanguageId.English },
        //                        new(){ Name = "Post-карти", LanguageId=LanguageId.Ukrainian } }
        //                },
        //                new()
        //                {
        //                    UrlSlug = "accessories-for-routers",
        //                    Image = "",
        //                    ParentId = 40,
        //                    CategoryTranslations = new List<CategoryTranslation>(){
        //                        new(){ Name = "Accessories for routers", LanguageId=LanguageId.English },
        //                        new(){ Name = "Комплектуючі для маршрутизаторів", LanguageId=LanguageId.Ukrainian } }
        //                },
        //            };
        //            return categories;
        //        }
        #endregion

        static IEnumerable<Shop> GetPreconfiguredMarketplaceShops(string userIdFashion, string userIdElectronics, List<DeliveryType> deliveryTypes)
        {
            var shops = new List<Shop>
            {
                new(){
                FullName="Emily Rain",
                Name = "Fashion",
                Description="Our purpose at Fashion is to empower person to lead bold and full lives. We believe that if you look good, you feel good. And when you feel good you can do good for others around you. Fashion brings you a wide range of trendy shoes, beautiful scarves, and statement-making jewelry, all at affordable prices to make them accessible to you.",
                Photo="Fashion.png",
                Email="fashion@gmail.com",
                SiteUrl="https://fashion.com/",
                CountryId=60,
                CityId=1,
                UserId=userIdFashion,
                DeliveryTypes=deliveryTypes,
                ShopSchedule=new List<ShopScheduleItem>(){
                    new(){ DayOfWeekId=DayOfWeekId.Monday, Start=new DateTime(1,1,1,8,0,0), End=new DateTime(1,1,1,18,0,0) },
                    new(){ DayOfWeekId=DayOfWeekId.Tuesday, Start=new DateTime(1,1,1,8,0,0), End=new DateTime(1,1,1,18,0,0) },
                    new(){ DayOfWeekId=DayOfWeekId.Wednesday, Start=new DateTime(1,1,1,9,0,0), End=new DateTime(1,1,1,19,0,0) },
                    new(){ DayOfWeekId=DayOfWeekId.Thursday, Start=new DateTime(1,1,1,9,0,0), End=new DateTime(1,1,1,19,0,0) },
                    new(){ DayOfWeekId=DayOfWeekId.Friday, Start=new DateTime(1,1,1,8,0,0), End=new DateTime(1,1,1,18,0,0) },
                    new(){ DayOfWeekId=DayOfWeekId.Saturday, IsWeekend=true },
                    new(){ DayOfWeekId=DayOfWeekId.Sunday, IsWeekend=true },
                },
                },
                new(){
                FullName="Leon Erlane",
                Name = "Electronics",
                Description="The Electronics store presents a wide selection of electronics, both new and used, with a guarantee of quality and speed of delivery",
                Photo="Electronics.jpg",
                Email="electronics@gmail.com",
                SiteUrl="https://electronics.com/",
                CountryId=60,
                CityId=2,
                UserId=userIdElectronics,
                DeliveryTypes=deliveryTypes,
                ShopSchedule=new List<ShopScheduleItem>(){
                    new(){ DayOfWeekId=DayOfWeekId.Monday, Start=new DateTime(1,1,1,8,0,0), End=new DateTime(1,1,1,18,0,0) },
                    new(){ DayOfWeekId=DayOfWeekId.Tuesday, Start=new DateTime(1,1,1,8,0,0), End=new DateTime(1,1,1,18,0,0) },
                    new(){ DayOfWeekId=DayOfWeekId.Wednesday, Start=new DateTime(1,1,1,8,0,0), End=new DateTime(1,1,1,18,0,0) },
                    new(){ DayOfWeekId=DayOfWeekId.Thursday, Start=new DateTime(1,1,1,8,0,0), End=new DateTime(1,1,1,18,0,0) },
                    new(){ DayOfWeekId=DayOfWeekId.Friday, Start=new DateTime(1,1,1,8,0,0), End=new DateTime(1,1,1,18,0,0) },
                    new(){ DayOfWeekId=DayOfWeekId.Saturday, IsWeekend=true },
                    new(){ DayOfWeekId=DayOfWeekId.Sunday, IsWeekend=true },
                }
                },

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
                //new(){
                //    Name = "Nike Festive dress Hex Wednesday Dress",
                //    Description="",
                //    Price=1000f,
                //    Count=100,
                //    ShopId=1,
                //    StatusId=1,
                //    CategoryId=58,
                //    UrlSlug=Guid.NewGuid(),
                //    SaleId=1,
                //    Discount=10
                //},
                //new(){ Name = "Puma Festive dress Hex Wednesday Dress",
                //    Description="",
                //    Price=1500f,
                //    Count=100,
                //    ShopId=1,
                //    StatusId=1,
                //    CategoryId=58,
                //    UrlSlug=Guid.NewGuid(),
                //    SaleId=1,
                //    Discount=15
                //},
                //new(){
                //    Name = "Zara Festive dress Hex Wednesday Dress",
                //    Description="",
                //    Price=1700f,
                //    Count=100,
                //    ShopId=1,
                //    StatusId=1,
                //    CategoryId=58,
                //    UrlSlug=Guid.NewGuid(),
                //    SaleId=1,
                //    Discount=25
                //},
                //new(){
                //    Name = "H&M Festive dress Hex Wednesday Dress",
                //    Description="",
                //    Price=1500f,
                //    Count=100,
                //    ShopId=1,
                //    StatusId=1,
                //    CategoryId=58,
                //    UrlSlug=Guid.NewGuid(),
                //    SaleId=1,
                //    Discount=10
                //},
                //new(){
                //    Name = "AAA Festive dress Hex Wednesday Dress",
                //    Description="",
                //    Price=3500f,
                //    Count=100,
                //    ShopId=1,
                //    StatusId=1,
                //    CategoryId=58,
                //    UrlSlug=Guid.NewGuid(),
                //    SaleId=1,
                //    Discount=35
                //},
                //new(){
                //    Name = "Nike Festive dress Hex Wednesday Dress",
                //    Description="",
                //    Price=1000f,
                //    Count=100,
                //    ShopId=1,
                //    StatusId=1,
                //    CategoryId=58,
                //    UrlSlug=Guid.NewGuid(),
                //    SaleId=1,
                //    Discount=25
                //},
                //new(){ Name = "AAA Festive dress Hex Wednesday Dress",Description="",Price=1000f,Count=100,ShopId=1,StatusId=1,CategoryId=58,UrlSlug=Guid.NewGuid() },
                //new(){ Name = "Puma Festive dress Hex Wednesday Dress",Description="",Price=1000f,Count=100,ShopId=1,StatusId=1,CategoryId=58,UrlSlug=Guid.NewGuid() },
                //new(){ Name = "H&M Festive dress Hex Wednesday Dress",Description="",Price=1000f,Count=100,ShopId=1,StatusId=1,CategoryId=58,UrlSlug=Guid.NewGuid() },
                //new(){ Name = "Zara Festive dress Hex Wednesday Dress",Description="",Price=1000f,Count=100,ShopId=1,StatusId=1,CategoryId=58,UrlSlug=Guid.NewGuid() },
                //new(){ Name = "Zara Festive dress Hex Wednesday Dress",Description="",Price=1000f,Count=100,ShopId=1,StatusId=1,CategoryId=58,UrlSlug=Guid.NewGuid() },
                //new(){ Name = "AAA Festive dress Hex Wednesday Dress",Description="",Price=1000f,Count=100,ShopId=1,StatusId=1,CategoryId=58,UrlSlug=Guid.NewGuid() },
                //new(){ Name = "Nike Festive dress Hex Wednesday Dress",Description="",Price=1000f,Count=100,ShopId=1,StatusId=1,CategoryId=58,UrlSlug=Guid.NewGuid() },
                //new(){ Name = "H&M Festive dress Hex Wednesday Dress",Description="",Price=1000f,Count=100,ShopId=1,StatusId=1,CategoryId=58,UrlSlug=Guid.NewGuid() },
                //new(){ Name = "Puma Festive dress Hex Wednesday Dress",Description="",Price=1000f,Count=100,ShopId=1,StatusId=1,CategoryId=58,UrlSlug=Guid.NewGuid() },
                //new(){ Name = "Festive dress Hex Wednesday Dress",Description="",Price=1000f,Count=100,ShopId=1,StatusId=1,CategoryId=58,UrlSlug=Guid.NewGuid() },
                //new(){ Name = "Festive dress Hex Wednesday Dress",Description="",Price=1000f,Count=100,ShopId=1,StatusId=1,CategoryId=58,UrlSlug=Guid.NewGuid() },
                //new(){ Name = "Festive dress Hex Wednesday Dress",Description="",Price=1000f,Count=100,ShopId=1,StatusId=1,CategoryId=58,UrlSlug=Guid.NewGuid() },
                //new(){ Name = "Festive dress Hex Wednesday Dress",Description="",Price=1000f,Count=100,ShopId=1,StatusId=1,CategoryId=58,UrlSlug=Guid.NewGuid() },
                //new(){ Name = "Festive dress Hex Wednesday Dress",Description="",Price=1000f,Count=100,ShopId=1,StatusId=1,CategoryId=58,UrlSlug=Guid.NewGuid() },
                //new(){ Name = "Festive dress Hex Wednesday Dress",Description="",Price=1000f,Count=100,ShopId=1,StatusId=1,CategoryId=58,UrlSlug=Guid.NewGuid() },
                //new(){ Name = "Festive dress Hex Wednesday Dress",Description="",Price=1000f,Count=100,ShopId=1,StatusId=1,CategoryId=58,UrlSlug=Guid.NewGuid() },
                //new(){ Name = "Festive dress Hex Wednesday Dress",Description="",Price=1000f,Count=100,ShopId=1,StatusId=1,CategoryId=58,UrlSlug=Guid.NewGuid() },
                //new(){ Name = "Festive dress Hex Wednesday Dress",Description="",Price=1000f,Count=100,ShopId=1,StatusId=1,CategoryId=58,UrlSlug=Guid.NewGuid() },
                //new(){ Name = "Festive dress Hex Wednesday Dress",Description="",Price=1000f,Count=100,ShopId=1,StatusId=1,CategoryId=58,UrlSlug=Guid.NewGuid() },
                //new(){ Name = "Festive dress Hex Wednesday Dress",Description="",Price=1000f,Count=100,ShopId=1,StatusId=1,CategoryId=58,UrlSlug=Guid.NewGuid() },
                //new(){ Name = "Festive dress Hex Wednesday Dress",Description="",Price=1000f,Count=100,ShopId=1,StatusId=1,CategoryId=58,UrlSlug=Guid.NewGuid() },
                //new(){ Name = "Festive dress Hex Wednesday Dress",Description="",Price=1000f,Count=100,ShopId=1,StatusId=1,CategoryId=58,UrlSlug=Guid.NewGuid() },
                //new(){ Name = "Festive dress Hex Wednesday Dress",Description="",Price=1000f,Count=100,ShopId=1,StatusId=1,CategoryId=58,UrlSlug=Guid.NewGuid() },
                //new(){ Name = "Festive dress Hex Wednesday Dress",Description="",Price=1000f,Count=100,ShopId=1,StatusId=1,CategoryId=58,UrlSlug=Guid.NewGuid() },
                //new(){ Name = "Festive dress Hex Wednesday Dress",Description="",Price=1000f,Count=100,ShopId=1,StatusId=1,CategoryId=58,UrlSlug=Guid.NewGuid() },
                //new(){ Name = "Festive dress Hex Wednesday Dress",Description="",Price=1000f,Count=100,ShopId=1,StatusId=1,CategoryId=58,UrlSlug=Guid.NewGuid() },
                //new(){ Name = "Festive dress Hex Wednesday Dress",Description="",Price=1000f,Count=100,ShopId=1,StatusId=1,CategoryId=58,UrlSlug=Guid.NewGuid() },
                //new(){ Name = "Festive dress Hex Wednesday Dress",Description="",Price=1000f,Count=100,ShopId=1,StatusId=1,CategoryId=58,UrlSlug=Guid.NewGuid() },
                //new(){ Name = "Festive dress Hex Wednesday Dress",Description="",Price=1000f,Count=100,ShopId=1,StatusId=1,CategoryId=58,UrlSlug=Guid.NewGuid() },
                //new(){ Name = "Festive dress Hex Wednesday Dress",Description="",Price=1000f,Count=100,ShopId=1,StatusId=1,CategoryId=58,UrlSlug=Guid.NewGuid() },
                //new(){ Name = "Festive dress Hex Wednesday Dress",Description="",Price=1000f,Count=100,ShopId=1,StatusId=1,CategoryId=58,UrlSlug=Guid.NewGuid() },
                //new(){ Name = "Festive dress Hex Wednesday Dress",Description="",Price=1000f,Count=100,ShopId=1,StatusId=1,CategoryId=58,UrlSlug=Guid.NewGuid() },
                //new(){ Name = "Festive dress Hex Wednesday Dress",Description="",Price=1000f,Count=100,ShopId=1,StatusId=1,CategoryId=58,UrlSlug=Guid.NewGuid() },
                //new(){ Name = "Festive dress Hex Wednesday Dress",Description="",Price=1000f,Count=100,ShopId=1,StatusId=1,CategoryId=58,UrlSlug=Guid.NewGuid() },
                //new(){ Name = "Festive dress Hex Wednesday Dress",Description="",Price=1000f,Count=100,ShopId=1,StatusId=1,CategoryId=58,UrlSlug=Guid.NewGuid() },
                //new(){ Name = "Festive dress Hex Wednesday Dress",Description="",Price=1000f,Count=100,ShopId=1,StatusId=1,CategoryId=58,UrlSlug=Guid.NewGuid() },
                //new(){ Name = "Festive dress Hex Wednesday Dress",Description="",Price=1000f,Count=100,ShopId=1,StatusId=1,CategoryId=58,UrlSlug=Guid.NewGuid() },
                //new(){ Name = "Festive dress Hex Wednesday Dress",Description="",Price=1000f,Count=100,ShopId=1,StatusId=1,CategoryId=58,UrlSlug=Guid.NewGuid() },
                //new(){ Name = "Festive dress Hex Wednesday Dress",Description="",Price=1000f,Count=100,ShopId=1,StatusId=1,CategoryId=58,UrlSlug=Guid.NewGuid() },
                //new(){ Name = "Festive dress Hex Wednesday Dress",Description="",Price=1000f,Count=100,ShopId=1,StatusId=1,CategoryId=58,UrlSlug=Guid.NewGuid() },
                //new(){ Name = "Festive dress Hex Wednesday Dress",Description="",Price=1000f,Count=100,ShopId=1,StatusId=1,CategoryId=58,UrlSlug=Guid.NewGuid() },
                //new(){ Name = "Festive dress Hex Wednesday Dress",Description="",Price=1000f,Count=100,ShopId=1,StatusId=1,CategoryId=58,UrlSlug=Guid.NewGuid() },
                //new(){ Name = "Festive dress Hex Wednesday Dress",Description="",Price=1000f,Count=100,ShopId=1,StatusId=1,CategoryId=58,UrlSlug=Guid.NewGuid() },
                //new(){ Name = "Festive dress Hex Wednesday Dress",Description="",Price=1000f,Count=100,ShopId=1,StatusId=1,CategoryId=58,UrlSlug=Guid.NewGuid() },
                //new(){ Name = "Festive dress Hex Wednesday Dress",Description="",Price=1000f,Count=100,ShopId=1,StatusId=1,CategoryId=58,UrlSlug=Guid.NewGuid() },

/*1*/           new(){ Name = "Плаття H&M XAZ309535KOJO M Чорне",
                       ShopId=1,
                       StatusId=1,
                       UrlSlug=Guid.NewGuid(),
                       Description="Hennes & Mauritz (H&M) — відомий шведський бренд, який виробляє одяг, взуття, спідню білизну, косметику, аксесуари у невисокому ціновому сегменті. Над колекціями бренду H&M працюють креативні дизайнери, а у виробництві використовуються сучасні технології. Сьогодні H&M – це найбільша мережа в Європі. Її покупці – це молоді люди, які хочуть недорого та стильно одягатися, виглядати на всі 100% та з кожною новою колекцією змінювати свій образ",
                       Price=721f,
                       Count=5,
                       CategoryId=58,
                    },
/*2*/           new(){ Name = "Плаття H&M 0716597_01 S Сріблясте",
                       ShopId=1,
                       StatusId=1,
                       UrlSlug=Guid.NewGuid(),
                       Description="Приталене плаття довжиною до ікри в рубчик з блискучими нитками. Шия, довгі рукави та прямий низ з розрізами з боків.",
                       Price=1139f,
                       Count=7,
                       SaleId=1,
                       Discount=15,
                       CategoryId=58,
                    },
/*3*/           new(){ Name = "Плаття H&M 0726226 S Молочне",
                       ShopId=1,
                       StatusId=1,
                       UrlSlug=Guid.NewGuid(),
                       Description="Особливості моделі:\r\nКоротке плаття з відкритими плечима з бавовняного переплетення з англійською вишивкою.\r\nШирокий еластичний волан зверху та над рукавами, прихована змійка з одного боку та розкльошена крайка.\r\nТрикотажна підкладка.",
                       Price=1199f,
                       Count=10,
                       SaleId=1,
                       Discount=15,
                       CategoryId=58,
                    },
/*4*/           new(){ Name = "Плаття H&M 2001-617838 S Червоне",
                       ShopId=1,
                       StatusId=1,
                       UrlSlug=Guid.NewGuid(),
                       Description="Плаття завдовжки до середини литки з віскозної тканини, яка м'яко драпірується, з розрізом і обтягнутим тканиною ґудзиком на комірі ззаду та декоративною задрапірованою складкою спереду. Занижена лінія плеча, довгі рукави із широкими манжетами на обтягнутих ґудзиках і знімний пояс із м'якої тканини з металевою фурнітурою у формі півкілець. На підкладці. ***Розмір — 38 Довжина рукава — 56,00 см Довжина виробу — 120,00 см Ширина в плечах — 50,00 см Обсяг грудей — 112,00 см",
                       Price=1560f,
                       Count=10,
                       SaleId=1,
                       Discount=10,
                       CategoryId=58,
                    },
/*5*/           new(){ Name = "Плаття H&M 9468002sdm S Чорне",
                       ShopId=1,
                       StatusId=1,
                       UrlSlug=Guid.NewGuid(),
                       Description="Коротке пряме плаття-футболка з м'якого матеріалу з аплікаціями. Круглий виріз горловини та злегка занижена лінія плеча.",
                       Price=729f,
                       Count=10,
                       SaleId=1,
                       Discount=7,
                       CategoryId=58,
                    },
/*6*/           new(){ Name = "Плаття H&M 9275192dm XS Чорне",
                       ShopId=1,
                       StatusId=1,
                       UrlSlug=Guid.NewGuid(),
                       Description="Особливості моделі:\r\nКоротке плаття з довгими призбираними рукавами.\r\nМаленький розріз із ґудзиком ззаду біля горловини та знизу на рукавах.\r\nТрикотажна підкладка.",
                       Price=999f,
                       Count=10,
                       SaleId=1,
                       Discount=5,
                       CategoryId=58,
                    },
/*7*/           new(){ Name = "Плаття H&M 9399860abr M Чорне",
                       ShopId=1,
                       StatusId=1,
                       UrlSlug=Guid.NewGuid(),
                       Description="Обтисле плаття завдовжки до литок з еластичного трикотажу в рубчик із віскози. Комір-стійка та довгі рукави. Відкрита спина з перекрученою деталлю.\r\nHennes & Mauritz (H&M) — відомий шведський бренд, який виготовляє одяг, взуття, спідню білизну, косметику, аксесуари в невисокому ціновому сегменті. Над колекціями бренду H&M працюють креативні дизайнери, а для виробництва використовуються сучасні технології. Сьогодні H&M — це найбільша мережа в Європі. Її покупці — це молоді люди, які хочуть недорого та стильно одягатися, мати вигляд на всі 100% та з кожною новою колекцією міняти свій образ.",
                       Price=1199f,
                       Count=15,
                       SaleId=1,
                       Discount=10,
                       CategoryId=58,
                    },
/*8*/           new(){ Name = "Сукня H&M 9328882bar L Чорна",
                       ShopId=1,
                       StatusId=1,
                       UrlSlug=Guid.NewGuid(),
                       Description="Коротка сукня з атласу, що м'яко драпірується, з вирізом «серце» і формованими чашками на підкладці. Ззаду збірка дрібними буфами, пишні рукави довжиною три чверті на вузькій гумці і з невеликою оборкою на плечах, а також манжетами, що облягають, складанням дрібними буфами з оборкою. Потаємна блискавка з одного боку, відрізна по талії, злегка розкльошена спідниця. Без підкладки.",
                       Price=390f,
                       Count=2,
                       CategoryId=58,
                    },
/*9*/           new(){ Name = "Сукня H&M 08714922 L Чорна",
                       ShopId=1,
                       StatusId=1,
                       UrlSlug=Guid.NewGuid(),
                       Description="Hennes & Mauritz (H&M) — відомий шведський бренд, який виготовляє одяг, взуття, спідню білизну, косметику, аксесуари в невисокому ціновому сегменті. Над колекціями бренда H&M працюють креативні дизайнери, а у виробництві використовуються сучасні технології. Сьогодні H&M — це найбільша мережа в Європі. Її покупці — це молоді люди, які хочуть недорого та стильно одягатися, мати вигляд на всі 100% та з кожною новою колекцією міняти свій образ.",
                       Price=1199f,
                       Count=5,
                       SaleId=1,
                       Discount=15,
                       CategoryId=58,
                    },
/*10*/          new(){ Name = "Плаття H&M 9025092bar L Фіолетове",
                       ShopId=1,
                       StatusId=1,
                       UrlSlug=Guid.NewGuid(),
                       Description="Обтисле плаття із супереластичного жатого велюру з блиском. Маленький комір-стійка та довгі рукави. Круглий виріз ззаду та застібка на великий гачок ззаду біля горловини. Без підкладки.",
                       Price=489f,
                       Count=5,
                       SaleId=1,
                       Discount=5,
                       CategoryId=58,
                    },
/*11*/          new(){ Name = "Плаття H&M 9025092bar S Фіолетове",
                       ShopId=1,
                       StatusId=1,
                       UrlSlug=Guid.NewGuid(),
                       Description="Обтисле плаття із супереластичного жатого велюру з блиском. Маленький комір-стійка та довгі рукави. Круглий виріз ззаду та застібка на великий гачок ззаду біля горловини. Без підкладки.",
                       Price=489f,
                       Count=5,
                       SaleId=1,
                       Discount=5,
                       CategoryId=58,
                    },
/*12*/          new(){ Name = "Сукня H&M XAZ333627BFEG L Рожева",
                       ShopId=1,
                       StatusId=1,
                       UrlSlug=Guid.NewGuid(),
                       Description="Hennes & Mauritz (H&M) — відомий шведський бренд, який виробляє одяг, взуття, спідню білизну, косметику, аксесуари у невисокому ціновому сегменті. Над колекціями бренду H&M працюють креативні дизайнери, а у виробництві використовуються сучасні технології. Сьогодні H&M – це найбільша мережа в Європі. Її покупці – це молоді люди, які хочуть недорого та стильно одягатися, виглядати на всі 100% та з кожною новою колекцією змінювати свій образ.",
                       Price=1139f,
                       Count=5,
                       SaleId=1,
                       Discount=10,
                       CategoryId=58,
                    },
/*13*/          new(){ Name = "Плаття H&M 0220094 S Біле з чорним",
                       ShopId=1,
                       StatusId=1,
                       UrlSlug=Guid.NewGuid(),
                       Description="Hennes & Mauritz (H&M) — відомий шведський бренд, який виробляє одяг, взуття, спідню білизну, косметику, аксесуари у невисокому ціновому сегменті. Над колекціями бренду H&M працюють креативні дизайнери, а у виробництві використовуються сучасні технології. Сьогодні H&M – це найбільша мережа в Європі. Її покупці – це молоді люди, які хочуть недорого та стильно одягатися, виглядати на всі 100% та з кожною новою колекцією змінювати свій образ.",
                       Price=600f,
                       Count=5,
                       SaleId=1,
                       Discount=25,
                       CategoryId=58,
                    },
/*14*/          new(){ Name = "Плаття H&M 112-586796 L Блакитне",
                       ShopId=1,
                       StatusId=1,
                       UrlSlug=Guid.NewGuid(),
                       Description="Hennes & Mauritz (H&M) — відомий шведський бренд, який виробляє одяг, взуття, спідню білизну, косметику, аксесуари у невисокому ціновому сегменті. Над колекціями бренду H&M працюють креативні дизайнери, а у виробництві використовуються сучасні технології. Сьогодні H&M – це найбільша мережа в Європі. Її покупці – це молоді люди, які хочуть недорого та стильно одягатися, виглядати на всі 100% та з кожною новою колекцією змінювати свій образ.",
                       Price=1365f,
                       Count=5,
                       CategoryId=58,
                    },
/*15*/          new(){ Name = "Плаття H&M 0614423 XL Блакитне",
                       ShopId=1,
                       StatusId=1,
                       UrlSlug=Guid.NewGuid(),
                       Description="Особливості моделі:\r\nДовге плаття без спинки з повітряного крепу з приталеним верхом, поясним швом та вузькими бретелями.\r\nСпереду отвір із вставленим вістрям та вільна спинка з ажурними виступами.\r\nРегульована застібка на гачок на шиї.\r\nРозкльошена спідниця з потайною блискавкою ззаду.\r\nНа підкладці.",
                       Price=1199f,
                       SaleId=1,
                       Discount=20,
                       Count=5,
                       CategoryId=58,
                    },
/*16*/          new(){ Name = "Плаття H&M 060928277 XS Червоне",
                       ShopId=1,
                       StatusId=1,
                       UrlSlug=Guid.NewGuid(),
                       Description="Особливості моделі:\r\nДовге плаття без спинки з повітряного крепу з приталеним верхом, поясним швом та вузькими бретелями.\r\nСпереду отвір із вставленим вістрям та вільна спинка з ажурними виступами.\r\nРегульована застібка на гачок на шиї.\r\nРозкльошена спідниця з потайною блискавкою ззаду.\r\nНа підкладці.",
                       Price=1140f,
                       SaleId=1,
                       Discount=5,
                       Count=5,
                       CategoryId=58,
                    },
/*17*/          new(){ Name = "Плаття H&M 0879298 XXL Бежеве",
                       ShopId=1,
                       StatusId=1,
                       UrlSlug=Guid.NewGuid(),
                       Description="Особливості моделі:\r\nДовге плаття без спинки з повітряного крепу з приталеним верхом, поясним швом та вузькими бретелями.\r\nСпереду отвір із вставленим вістрям та вільна спинка з ажурними виступами.\r\nРегульована застібка на гачок на шиї.\r\nРозкльошена спідниця з потайною блискавкою ззаду.\r\nНа підкладці.",
                       Price=1105f,
                       SaleId=1,
                       Discount=35,
                       Count=10,
                       CategoryId=58,
                    },
/*18*/          new(){ Name = "Плаття H&M 0788247 S Молочне",
                       ShopId=1,
                       StatusId=1,
                       UrlSlug=Guid.NewGuid(),
                       Description="Максіплаття з жатої тканини із блиском. V-подібний виріз і вузькі подвійні бретелі, що перехрещуються ззаду. Еластичний шов під грудьми та розрізи з боків. Напівпідкладка.",
                       Price=1105f,
                       SaleId=1,
                       Discount=25,
                       Count=10,
                       CategoryId=58,
                    },
/*19*/          new(){ Name = "Плаття H&M 0785861-0 XS Зелене",
                       ShopId=1,
                       StatusId=1,
                       UrlSlug=Guid.NewGuid(),
                       Description="Плаття завдовжки до коліна з м'якого бавовняного твілу. Комір, а також застібка спереду на всій довжині. Нагрудні кишені з клапаном на ґудзику, а також знімний пояс на талії. Довгі рукави. Кокетка із зустрічними зборками ззаду. Без підкладки. Можуть бути пошкоджені бирки.",
                       Price=1523f,
                       SaleId=1,
                       Discount=60,
                       Count=10,
                       CategoryId=58,
                    },
/*20*/          new(){ Name = "Сукня джинсова H&M XAZ310585WFTV S Синя",
                       ShopId=1,
                       StatusId=1,
                       UrlSlug=Guid.NewGuid(),
                       Description="Hennes & Mauritz (H&M) — відомий шведський бренд, який виробляє одяг, взуття, спідню білизну, косметику, аксесуари у невисокому ціновому сегменті. Над колекціями бренду H&M працюють креативні дизайнери, а у виробництві використовуються сучасні технології. Сьогодні H&M – це найбільша мережа в Європі. Її покупці – це молоді люди, які хочуть недорого та стильно одягатися, виглядати на всі 100% та з кожною новою колекцією змінювати свій образ",
                       Price=959f,
                       SaleId=1,
                       Discount=15,
                       Count=10,
                       CategoryId=58,
                    },


/*21*/          new(){ Name = "Плаття Zara 7200/055/800 M Чорне",
                       ShopId=1,
                       StatusId=1,
                       UrlSlug=Guid.NewGuid(),
                       Description="Сукня з V-подібним вирізом на запах. Волани внизу та на рукавах. Еластична лінія талії. Має підкладку.",
                       Price=1109f,
                       SaleId=1,
                       Discount=35,
                       Count=10,
                       CategoryId=58,
                    },
/*22*/          new(){ Name = "Плаття Zara 5644/438/611 S Рожеве",
                       ShopId=1,
                       StatusId=1,
                       UrlSlug=Guid.NewGuid(),
                       Description="Сукня міні зі зборками по довжині.\r\nV-подібний виріз, короткі рукави.",
                       Price=1230f,
                       SaleId=1,
                       Discount=65,
                       Count=15,
                       CategoryId=58,
                    },
/*23*/          new(){ Name = "Плаття Zara XAZ281339XUVQ S Бузкове",
                       ShopId=1,
                       StatusId=1,
                       UrlSlug=Guid.NewGuid(),
                       Description="Довге плаття-майка без рукавів, вільний крій, круглий виріз горловини.",
                       Price=829f,
                       SaleId=1,
                       Discount=65,
                       Count=5,
                       CategoryId=58,
                    },
/*24*/          new(){ Name = "Плаття Zara 8342-338-050 L Чорне",
                       ShopId=1,
                       StatusId=1,
                       UrlSlug=Guid.NewGuid(),
                       Description="Особливості моделі:\r\nКоротке плаття з коміром та V-подібним вирізом.\r\nДовгі рукави з воланами.\r\nЕластичні складки з боків.\r\nЗ підкладкою.\r\nЗастібка на ґудзики спереду.\r\nРозмір: L.\r\nДовжина рукава: 58 см.\r\nДовжина виробу: 91 см.\r\nШирина за плечима: 52 см.\r\nОбсяг грудей: 86 см.",
                       Price=2126f,
                       Count=5,
                       CategoryId=58,
                    },
/*25*/          new(){ Name = "Плаття зі штучної шкіри Zara 1937/242/505 XL Темно-зелене",
                       ShopId=1,
                       StatusId=1,
                       UrlSlug=Guid.NewGuid(),
                       Description="Коротке плаття зі штучної шкіри, довгі рукави, застібка на металеві ґудзики, дві нагрудні кишені. У комплекті пояс",
                       Price=1950f,
                       SaleId=1,
                       Discount=5,
                       Count=5,
                       CategoryId=58,
                    },
/*26*/          new(){ Name = "Плаття Zara 1165-652-250 S Біле",
                       ShopId=1,
                       StatusId=1,
                       UrlSlug=Guid.NewGuid(),
                       Description="Мідіплаття з горловиною халтер та декоративними складками ціна 19,95 євро ***Розмір — S Довжина виробу — 114,00 см Обсяг грудей — 70,00 см",
                       Price=1750f,
                       SaleId=1,
                       Discount=10,
                       Count=15,
                       CategoryId=58,
                    },
/*27*/          new(){ Name = "Сукня Zara 2674-254-712 L Біла з синім",
                       ShopId=1,
                       StatusId=1,
                       UrlSlug=Guid.NewGuid(),
                       Description="Сукня з м'якої та приємної на дотик тканини.\r\n\r\nРозмір - L\r\nДовжина рукава – 66,00 см\r\nДовжина виробу – 92,00 см\r\nШирина по плечах – 47,00 см\r\nОбхват грудей – 108,00 см",
                       Price=1799f,
                       SaleId=1,
                       Discount=15,
                       Count=15,
                       CategoryId=58,
                    },
/*28*/          new(){ Name = "Сукня Zara XAZ282046MFRJ S Фіолетова",
                       ShopId=1,
                       StatusId=1,
                       UrlSlug=Guid.NewGuid(),
                       Description="Сукня Фіолетова",
                       Price=1700f,
                       SaleId=1,
                       Discount=15,
                       Count=7,
                       CategoryId=58,
                    },
/*29*/          new(){ Name = "Сукня Zara 7740-583-251 S Молочна",
                       ShopId=1,
                       StatusId=1,
                       UrlSlug=Guid.NewGuid(),
                       Description="Коротка сукня з глибоким вирізом.\r\n\r\nРозмір - S\r\nДовжина виробу – 88,00 см\r\nОбхват грудей – 88,00 см",
                       Price=1417f,
                       Count=7,
                       CategoryId=58,
                    },
/*30*/          new(){ Name = "Сукня Zara 8778-308-700 XS Коричнева",
                       ShopId=1,
                       StatusId=1,
                       UrlSlug=Guid.NewGuid(),
                       Description="Атласна сукня-міді з трикотажним відрізним рукавом тонкої в'язки.\r\n\r\nРозмір - XS\r\nДовжина виробу – 105,00 см\r\nОбхват грудей – 74,00 см",
                       Price=1830f,
                       SaleId=1,
                       Discount=15,
                       Count=10,
                       CategoryId=58,
                    },
/*31*/          new(){ Name = "Плаття Zara 8342/129/403 M Блакитне",
                       ShopId=1,
                       StatusId=1,
                       UrlSlug=Guid.NewGuid(),
                       Description="Коротке плаття в горошок із драпірованими рукавами-ліхтариками",
                       Price=1500f,
                       SaleId=1,
                       Discount=5,
                       Count=10,
                       CategoryId=58,
                    },
/*32*/          new(){ Name = "Плаття Zara XAZ265147CIYR L Жовте",
                       ShopId=1,
                       StatusId=1,
                       UrlSlug=Guid.NewGuid(),
                       Description="Обтисле плаття з короткими рукавами, трикутний виріз, без застібки.",
                       Price=1200f,
                       SaleId=1,
                       Discount=5,
                       Count=15,
                       CategoryId=58,
                    },
/*33*/          new(){ Name = "Плаття Zara 7969/036/646 S Фіолетове",
                       ShopId=1,
                       StatusId=1,
                       UrlSlug=Guid.NewGuid(),
                       Description="Плаття коротке з V-подібним вирізом. Короткі рукави-ліхтарики. Драпірована тканина. Ззаду потайна застібка-змійка у шві",
                       Price=1780f,
                       SaleId=1,
                       Discount=5,
                       Count=15,
                       CategoryId=58,
                    },
/*34*/          new(){ Name = "Плаття Zara 1971-166-603 XS Фіолетове",
                       ShopId=1,
                       StatusId=1,
                       UrlSlug=Guid.NewGuid(),
                       Description="Плаття для жінок із віскози із застібкою ззаду на змійку. Спереду декоративні ґудзики. З боків на всю довжину плаття і на бретелях вшиті гумки. ***Розмір — M Довжина виробу — 83,00 см Обсяг грудей — 82,00 см ***Розмір — XS Довжина виробу — 83,00 см Обсяг грудей — 74,00 см",
                       Price=2430f,
                       SaleId=1,
                       Discount=25,
                       Count=15,
                       CategoryId=58,
                    },
/*35*/          new(){ Name = "Плаття Zara XAZ264765MQPM M Чорне",
                       ShopId=1,
                       StatusId=1,
                       UrlSlug=Guid.NewGuid(),
                       Description="Довге плаття без рукавів, розширюється донизу, трикутний виріз горловини, без застібки.",
                       Price=1200f,
                       SaleId=1,
                       Discount=10,
                       Count=10,
                       CategoryId=58,
                    },
/*36*/          new(){ Name = "Сукня з капюшоном Zara 1044/155 M зелена",
                       ShopId=1,
                       StatusId=1,
                       UrlSlug=Guid.NewGuid(),
                       Description="ПЛАТТЯ МІДІ З КРУГЛИМ ВИРІЗОМ І КАПЮШОНОМ. ДОВГІ РУКАВИ, МАНЖЕТИ В РУБЧИК. БІЧНІ КИШЕНІ НА ШВАХ.",
                       Price=1250f,
                       SaleId=1,
                       Discount=10,
                       Count=10,
                       CategoryId=58,
                    },
/*37*/          new(){ Name = "Сукня-сарафан ZARA 9413 XL Фіолетова",
                       ShopId=1,
                       StatusId=1,
                       UrlSlug=Guid.NewGuid(),
                       Description="Сукня-сарафан міді фіолетова ZARA",
                       Price=850f,
                       Count=10,
                       CategoryId=58,
                    },
/*38*/          new(){ Name = "СУКНЯ ТРИКОТАЖНА ZARA 9415 S Синя",
                       ShopId=1,
                       StatusId=1,
                       UrlSlug=Guid.NewGuid(),
                       Description="Трикотажна сукня А-силуету з рукавами 7/8 вдало обігрує фігуру. Геометричні рельєфні шви і виточки створюють легкий акцент на талії та грудях. Данина сучасній моді – вшита відкрито металева застібка-блискавка на спині.",
                       Price=500f,
                       Count=10,
                       CategoryId=58,
                    },
/*39*/          new(){ Name = "Тепла трикотажна сукня ZARA 19415 S Чорна",
                       ShopId=1,
                       StatusId=1,
                       UrlSlug=Guid.NewGuid(),
                       Description="Тепла трикотажна сукня",
                       Price=2500f,
                       Count=10,
                       CategoryId=58,
                    },
/*40*/          new(){ Name = "Сукня ZARA 3812321 S Різнокольорова",
                       ShopId=1,
                       StatusId=1,
                       UrlSlug=Guid.NewGuid(),
                       Description="Красива смугаста сукня з вираженим ефектом льону. Пояс, що зав'язується на талії, надає сукні більш жіночного і сучасного вигляду. • з поясом, що зав'язується • смугастий Склад: 50% бавовна, 50% льон. М'яке прання до 30 градусів. Відбілювання хлором неможливе. Сушіння в барабані неможливе. Чи не гладити гарячим.",
                       Price=5300f,
                       Count=10,
                       CategoryId=58,
                    },


/*41*/          new(){ Name = "Сукня Bershka 9694293abr XXL Чорна",
                       ShopId=1,
                       StatusId=1,
                       UrlSlug=Guid.NewGuid(),
                       Description="Коротка сукня без рукавів із щільного трикотажу. Облягаючий силует із закладеними з одного боку складками для створення легкого ефекту драпірування. Круглий виріз горловини і таємна блискавка на спині. На розмір XXL: напівобхват грудей 58 см, напівобхват талії 50 см, напівобхват стегон 62 см, довжина спинки 98 см.",
                       Price=585f,
                       Count=10,
                       CategoryId=58,
                    },
/*42*/          new(){ Name = "Плаття Bershka 0437418-4 XS Сіре",
                       ShopId=1,
                       StatusId=1,
                       UrlSlug=Guid.NewGuid(),
                       Description="Плаття Bershka Сіре",
                       Price=1585f,
                       SaleId=1,
                       Discount=50,
                       Count=10,
                       CategoryId=58,
                    },
/*43*/          new(){ Name = "Плаття Bershka 1603-9216822 L Чорне",
                       ShopId=1,
                       StatusId=1,
                       UrlSlug=Guid.NewGuid(),
                       Description="Коротке плаття з тканого матеріалу з глибоким трикутним вирізом, лацканами, вираженою лінією плеча та довгими рукавами. Драпірування, що підкреслює талію, формовані шви ззаду та потайна блискавка з одного боку. Без підкладки. Поліестер у складі сукні частково перероблений. ***Розмір — 38 Довжина рукава — 61,00 см Довжина виробу — 100,00 см Ширина по плечах — 39,00 см Зріст — 165 см ***Розмір — 46 Довжина рукава — 61,00 см Довжина виробу — 90,00 см Ширина по плечах — 41,00 см Зріст — 175 см ***Розмір — 44 Довжина рукава — 61,00 см Довжина виробу — 95,00 см Ширина по плечах — 42,00 см Обхват грудей — 102,00 см",
                       Price=900f,
                       SaleId=1,
                       Discount=5,
                       Count=10,
                       CategoryId=58,
                    },
/*44*/          new(){ Name = "Плаття Bershka XAZ303662NTME L Чорне",
                       ShopId=1,
                       StatusId=1,
                       UrlSlug=Guid.NewGuid(),
                       Description="Bershka є дочірньою компанією корпорації Inditex Group, створеної у 1975 році підприємцем Амансіо Ортегом Гаона. Марка була заснована у квітні 1998 року з метою завоювання тінейджерської аудиторії як найдоступніший бренд портфеля Inditex.\r\n\r\nЗручний, модний, яскравий і при цьому недорогий одяг швидко завоював визнання підлітків. Колекції не були копією виробів дорожчих брендів – команда дизайнерів створювала власні моделі на основі актуальних модних трендів. Спочатку стиль бренду позиціювався як вуличний, але згодом був розширений елегантнішими моделями.",
                       Price=770f,
                       SaleId=1,
                       Discount=10,
                       Count=16,
                       CategoryId=58,
                    },


            };
            return products;
        }
        static IEnumerable<ProductImage> GetPreconfiguredMarketplaceProductImages()
        {
            var productImages = new List<ProductImage>
            {
/*1*/          new(){ Name = "HM_XAZ309535KOJO_1.jpg", ProductId=1 },
               new(){ Name = "HM_XAZ309535KOJO_2.jpg", ProductId=1 },

/*2*/          new(){ Name = "HM_0716597_01_1.jpg", ProductId=2 },
               new(){ Name = "HM_0716597_01_2.jpg", ProductId=2 },

/*3*/          new(){ Name = "HM_0726226.jpg", ProductId=3 },

/*4*/          new(){ Name = "HM_2001-617838.jpg", ProductId=4 },

/*5*/          new(){ Name = "HM_9468002sdm_1.jpg", ProductId=5 },
               new(){ Name = "HM_9468002sdm_2.jpg", ProductId=5 },

/*6*/          new(){ Name = "HM_9275192dm_1.jpg", ProductId=6 },
               new(){ Name = "HM_9275192dm_2.jpg", ProductId=6 },

/*7*/          new(){ Name = "HM_9399860abr_1.jpg", ProductId=7 },
               new(){ Name = "HM_9399860abr_2.jpg", ProductId=7 },

/*8*/          new(){ Name = "HM_9328882bar_1.jpg", ProductId=8 },
               new(){ Name = "HM_9328882bar_2.jpg", ProductId=8 },

/*9*/          new(){ Name = "HM_08714922_1.jpg", ProductId=9 },
               new(){ Name = "HM_08714922_2.jpg", ProductId=9 },

/*10*/         new(){ Name = "HM_9025092bar_1.jpg", ProductId=10 },
               new(){ Name = "HM_9025092bar_2.jpg", ProductId=10 },

/*11*/         new(){ Name = "HM_9025092bar_1.jpg", ProductId=11 },
               new(){ Name = "HM_9025092bar_2.jpg", ProductId=11 },

/*12*/         new(){ Name = "HM_XAZ333627BFEG.jpg", ProductId=12 },

/*13*/         new(){ Name = "HM_0220094_1.jpg", ProductId=13 },
               new(){ Name = "HM_0220094_2.jpg", ProductId=13 },

/*14*/         new(){ Name = "HM_112-586796_1.jpg", ProductId=14 },
               new(){ Name = "HM_112-586796_2.jpg", ProductId=14 },

/*15*/         new(){ Name = "HM_0614423.jpg", ProductId=15 },

/*16*/         new(){ Name = "HM_060928277_1.jpg", ProductId=16 },
               new(){ Name = "HM_060928277_2.jpg", ProductId=16 },

/*17*/         new(){ Name = "HM_0879298_1.jpg", ProductId=17 },
               new(){ Name = "HM_0879298_2.jpg", ProductId=17 },

/*18*/         new(){ Name = "HM_0788247_1.jpg", ProductId=18 },
               new(){ Name = "HM_0788247_2.jpg", ProductId=18 },

/*19*/         new(){ Name = "HM_0785861-0 32_1.jpg", ProductId=19 },
               new(){ Name = "HM_0785861-0 32_2.jpg", ProductId=19 },

/*20*/         new(){ Name = "HM_XAZ310585WFTV_1.jpg", ProductId=20 },
               new(){ Name = "HM_XAZ310585WFTV_2.jpg", ProductId=20 },



/*21*/         new(){ Name = "Zara_7200055800_1.jpg", ProductId=21 },
               new(){ Name = "Zara_7200055800_2.jpg", ProductId=21 },
               new(){ Name = "Zara_7200055800_3.jpg", ProductId=21 },

/*22*/         new(){ Name = "Zara _5644438611_1.jpg", ProductId=22 },
               new(){ Name = "Zara _5644438611_2.jpg", ProductId=22 },
               new(){ Name = "Zara _5644438611_3.jpg", ProductId=22 },

/*23*/         new(){ Name = "Zara_XAZ281339XUVQ.jpg", ProductId=23 },

/*24*/         new(){ Name = "Zara_8342-338-050_1.jpg", ProductId=24 },
               new(){ Name = "Zara_8342-338-050_2.jpg", ProductId=24 },

/*25*/         new(){ Name = "Zara_1937242505_1.jpg", ProductId=25 },
               new(){ Name = "Zara_1937242505_2.jpg", ProductId=25 },
               new(){ Name = "Zara_1937242505_3.jpg", ProductId=25 },

/*26*/         new(){ Name = "Zara_1165-652-250_1.jpg", ProductId=26 },
               new(){ Name = "Zara_1165-652-250_2.jpg", ProductId=26 },

/*27*/         new(){ Name = "Zara_2674-254-712_1.jpg", ProductId=27 },
               new(){ Name = "Zara_2674-254-712_2.jpg", ProductId=27 },
               new(){ Name = "Zara_2674-254-712_3.jpg", ProductId=27 },

/*28*/         new(){ Name = "Zara_XAZ282046MFRJ_1.jpg", ProductId=28 },
               new(){ Name = "Zara_XAZ282046MFRJ_2.jpg", ProductId=28 },
               new(){ Name = "Zara_XAZ282046MFRJ_3.jpg", ProductId=28 },

/*29*/         new(){ Name = "Zara_7740-583-251.jpg", ProductId=29 },

/*30*/         new(){ Name = "Zara_8778-308-700.jpg", ProductId=30 },

/*31*/         new(){ Name = "Zara_8342129403_1.jpg", ProductId=31 },
               new(){ Name = "Zara_8342129403_2.jpg", ProductId=31 },
               new(){ Name = "Zara_8342129403_3.jpg", ProductId=31 },

/*32*/         new(){ Name = "Zara_XAZ265147CIYR_1.jpg", ProductId=32 },
               new(){ Name = "Zara_XAZ265147CIYR_2.jpg", ProductId=32 },
 
/*33*/         new(){ Name = "Zara_7969036646_1.jpg", ProductId=33 },
               new(){ Name = "Zara_7969036646_2.jpg", ProductId=33 },
               new(){ Name = "Zara_7969036646_3.jpg", ProductId=33 },

/*34*/         new(){ Name = "Zara_1971-166-603_1.jpg", ProductId=34 },
               new(){ Name = "Zara_1971-166-603_2.jpg", ProductId=34 },
               new(){ Name = "Zara_1971-166-603_3.jpg", ProductId=34 },

/*35*/         new(){ Name = "Zara_XAZ264765MQPM_1.jpg", ProductId=35 },
               new(){ Name = "Zara_XAZ264765MQPM_2.jpg", ProductId=35 },

/*36*/         new(){ Name = "Zara_1044155_1.jpg", ProductId=36 },
               new(){ Name = "Zara_1044155_2.jpg", ProductId=36 },
               new(){ Name = "Zara_1044155_3.jpg", ProductId=36 },

/*37*/         new(){ Name = "ZARA_9413_1.jpg", ProductId=37 },
               new(){ Name = "ZARA_9413_2.jpg", ProductId=37 },
               new(){ Name = "ZARA_9413_3.jpg", ProductId=37 },

/*38*/         new(){ Name = "ZARA_9415.jpg", ProductId=38 },

/*39*/         new(){ Name = "ZARA_19415.jpg", ProductId=39 },

/*40*/         new(){ Name = "ZARA_3812321_1.jpg", ProductId=40 },
               new(){ Name = "ZARA_3812321_2.jpg", ProductId=40 },


/*41*/         new(){ Name = "Bershka_9694293abr_1.jpg", ProductId=41 },
               new(){ Name = "Bershka_9694293abr_2.jpg", ProductId=41 },

/*42*/         new(){ Name = "Bershka_0437418-4_1.jpg", ProductId=42 },
               new(){ Name = "Bershka_0437418-4_2.jpg", ProductId=42 },
               new(){ Name = "Bershka_0437418-4_3.jpg", ProductId=42 },

/*43*/         new(){ Name = "Bershka_1603-9216822_1.jpg", ProductId=43 },
               new(){ Name = "Bershka_1603-9216822_2.jpg", ProductId=43 },

/*44*/         new(){ Name = "Bershka_XAZ303662NTME_1.jpg", ProductId=44 },
               new(){ Name = "Bershka_XAZ303662NTME_2.jpg", ProductId=44 },










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
/*1*/           new(){ FilterGroupId=1,
                       FilterNameTranslations=new List<FilterNameTranslation>(){
                            new() { LanguageId=LanguageId.English, Name = "International size"} ,
                            new() { LanguageId=LanguageId.Ukrainian, Name="Міжнародний розмір" }
                       }
                },
/*2*/           new(){ FilterGroupId=1,
                       FilterNameTranslations=new List<FilterNameTranslation>(){
                                    new() { LanguageId=LanguageId.English, Name = "Color"} ,
                                    new() {LanguageId=LanguageId.Ukrainian, Name="Колір" }
                       }
                },
/*3*/           new(){ FilterGroupId=1,
                       FilterNameTranslations=new List<FilterNameTranslation>(){
                                    new() { LanguageId=LanguageId.English, Name = "Sleeve length"} ,
                                    new() {LanguageId=LanguageId.Ukrainian, Name="Довжина рукава" }
                       }
                },
/*4*/           new(){ FilterGroupId=1,
                       FilterNameTranslations=new List<FilterNameTranslation>(){
                            new() { LanguageId=LanguageId.English, Name = "Length" } ,
                            new() {LanguageId=LanguageId.Ukrainian, Name="Довжина" }
                       }
                },
/*5*/           new(){ FilterGroupId=1,
                       FilterNameTranslations=new List<FilterNameTranslation>(){
                            new() { LanguageId=LanguageId.English, Name = "Dress neckline" } ,
                            new() {LanguageId=LanguageId.Ukrainian, Name="Виріз" }
                       }
                },
/*6*/           new(){ FilterGroupId=1,
                       FilterNameTranslations=new List<FilterNameTranslation>(){
                                    new() { LanguageId=LanguageId.English, Name = "Brand" } ,
                                    new() { LanguageId=LanguageId.Ukrainian, Name="Бренд" }
                       }
                },
/*7*/           new(){ FilterGroupId=1,
                       FilterNameTranslations=new List<FilterNameTranslation>(){
                                    new() { LanguageId=LanguageId.English, Name = "Dress style" } ,
                                    new() { LanguageId=LanguageId.Ukrainian, Name="Фасон плаття" }
                       }
                },

                ///* 1 */         new(){ FilterGroupId=1, FilterNameTranslations=new List<FilterNameTranslation>(){
                //                    new() { LanguageId=LanguageId.English, Name = "Сondition"} ,
                //                    new() {LanguageId=LanguageId.Ukrainian, Name="Стан" } } },
                ///* 2 */         new(){ FilterGroupId=1, FilterNameTranslations=new List<FilterNameTranslation>(){
                //                    new() { LanguageId=LanguageId.English, Name = "Purpose" } ,
                //                    new() {LanguageId=LanguageId.Ukrainian, Name="Призначення" } } },
                ///* 3 */         new(){ FilterGroupId=1, FilterNameTranslations=new List<FilterNameTranslation>(){
                //                    new() { LanguageId=LanguageId.English, Name = "Video memory type" } ,
                //                    new() {LanguageId=LanguageId.Ukrainian, Name="Тип відеопам'яті" } } },
                ///* 4 */         new(){ FilterGroupId=1, FilterNameTranslations=new List<FilterNameTranslation>(){
                //                    new() { LanguageId=LanguageId.English, Name = "Graphics chipset"} ,
                //                    new() {LanguageId=LanguageId.Ukrainian, Name="Графічний чіпсет" } } },
                ///* 5 */         new(){ FilterGroupId = 1, FilterNameTranslations=new List<FilterNameTranslation>(){
                //                    new() { LanguageId=LanguageId.English, Name = "Memory bus width" } ,
                //                    new() {LanguageId=LanguageId.Ukrainian, Name="Ширина шини пам'яті" } } },
                ///* 6 */         new(){ FilterGroupId = 1, FilterNameTranslations=new List<FilterNameTranslation>(){
                //                    new() { LanguageId=LanguageId.English, Name = "Producer" } ,
                //                    new() {LanguageId=LanguageId.Ukrainian, Name="Виробник" } } },
                ///* 7 */         new(){ FilterGroupId = 1, FilterNameTranslations=new List<FilterNameTranslation>(){
                //                    new() { LanguageId=LanguageId.English, Name = "Connection type" } ,
                //                    new() {LanguageId=LanguageId.Ukrainian, Name="Тип підключення" } } },
                ///* 8 */         new(){ FilterGroupId = 1, FilterNameTranslations = new List < FilterNameTranslation >() {
                //                    new() { LanguageId = LanguageId.English, Name = "Interfaces" } ,
                //                    new() {LanguageId=LanguageId.Ukrainian, Name="Інтерфейси" } } },
                ///* 9 */         new(){ FilterGroupId = 1, FilterNameTranslations=new List<FilterNameTranslation>(){
                //                    new() { LanguageId=LanguageId.English, Name = "Cooling system" } ,
                //                    new() {LanguageId=LanguageId.Ukrainian, Name="Система охолодження" } } },
                ///* 10 */        new(){ FilterGroupId = 1, FilterNameTranslations=new List<FilterNameTranslation>(){
                //                    new() { LanguageId=LanguageId.English, Name = "Peculiarities" } ,
                //                    new() {LanguageId=LanguageId.Ukrainian, Name="Особливості" } } },
                ///* 11 */        new(){ FilterGroupId = 1, FilterNameTranslations=new List<FilterNameTranslation>(){
                //                    new() { LanguageId=LanguageId.English, Name = "Producing country"} ,
                //                    new() {LanguageId=LanguageId.Ukrainian, Name="Країна-виробник" } } },
                ///* 12 */        new(){ FilterGroupId = 1, FilterNameTranslations=new List<FilterNameTranslation>(){
                //                    new() { LanguageId=LanguageId.English, Name = "Quality class" } ,
                //                    new() {LanguageId=LanguageId.Ukrainian, Name="Клас якості" } } },
                ///* 13 */        new(){ FilterGroupId = 1, FilterNameTranslations=new List<FilterNameTranslation>(){
                //                    new() { LanguageId=LanguageId.English, Name = "Warranty period, months" } ,
                //                    new() {LanguageId=LanguageId.Ukrainian, Name="Гарантійний термін, міс" } } },
                ///* 14 */        new(){ FilterGroupId = 1, FilterNameTranslations=new List<FilterNameTranslation>(){
                //                    new() { LanguageId=LanguageId.English, Name = "Processor frequency, MHz" } ,
                //                    new() {LanguageId=LanguageId.Ukrainian, Name="Частота процесора, МГц" } } },
                ///* 15 */        new(){ FilterGroupId = 1, FilterNameTranslations=new List<FilterNameTranslation>(){
                //                    new() { LanguageId=LanguageId.English, Name = "Video memory frequency, MHz" } ,
                //                    new() {LanguageId=LanguageId.Ukrainian, Name="Частота відеопам'яті, МГц" } } },
                ///* 16 */        new(){ FilterGroupId = 1, FilterNameTranslations=new List<FilterNameTranslation>(){
                //                    new() { LanguageId=LanguageId.English, Name = "Video memory size, MB" } ,
                //                    new() {LanguageId=LanguageId.Ukrainian, Name="Обсяг відеопам'яті, Мб" } } },
            };
            return filterNames;
        }

        static IEnumerable<FilterValue> GetPreconfiguredMarketplaceFilterValues(Category category)
        {
            var filterValues = new List<FilterValue>
            {
/*-1- 1*/          new(){ FilterNameId=1,
                       FilterValueTranslations=new List<FilterValueTranslation>(){
                            new(){ LanguageId=LanguageId.English, Value = "M" },
                            new(){ LanguageId=LanguageId.Ukrainian, Value="M"}
                       }
                },
/*-1- 2*/          new(){ FilterNameId=1,
                       FilterValueTranslations=new List<FilterValueTranslation>(){
                            new(){ LanguageId=LanguageId.English, Value = "S" },
                            new(){ LanguageId=LanguageId.Ukrainian, Value="S"}
                       }
                },
/*-1- 3*/          new(){ FilterNameId=1,
                       FilterValueTranslations=new List<FilterValueTranslation>(){
                            new(){ LanguageId=LanguageId.English, Value = "L" },
                            new(){ LanguageId=LanguageId.Ukrainian, Value="L"}
                       }
                },
/*-1- 4*/          new(){ FilterNameId=1,
                       FilterValueTranslations=new List<FilterValueTranslation>(){
                            new(){ LanguageId=LanguageId.English, Value = "XL" },
                            new(){ LanguageId=LanguageId.Ukrainian, Value="XL"}
                       }
                },
/*-1- 5*/          new(){ FilterNameId=1,
                       FilterValueTranslations=new List<FilterValueTranslation>(){
                            new(){ LanguageId=LanguageId.English, Value = "XS" },
                            new(){ LanguageId=LanguageId.Ukrainian, Value="XS"}
                       }
                },

/*-2- 6*/          new(){ FilterNameId=2,
                       FilterValueTranslations=new List<FilterValueTranslation>(){
                            new(){ LanguageId=LanguageId.English, Value = "Black" },
                            new(){ LanguageId=LanguageId.Ukrainian, Value="Чорний"}
                       }
                   },
/*-2- 7*/          new(){ FilterNameId=2,
                       FilterValueTranslations=new List<FilterValueTranslation>(){
                            new(){ LanguageId=LanguageId.English, Value = "Yellow" },
                            new(){ LanguageId=LanguageId.Ukrainian, Value="Жовтий"}
                       }
                   },
/*-2- 8*/          new(){ FilterNameId=2,
                       FilterValueTranslations=new List<FilterValueTranslation>(){
                            new(){ LanguageId=LanguageId.English, Value = "Red" },
                            new(){ LanguageId=LanguageId.Ukrainian, Value="Червоний"}
                       }
                   },
/*-2- 9*/          new(){ FilterNameId=2,
                       FilterValueTranslations=new List<FilterValueTranslation>(){
                            new(){ LanguageId=LanguageId.English, Value = "Blue" },
                            new(){ LanguageId=LanguageId.Ukrainian, Value="Синій"}
                       }
                   },
/*-2- 10*/         new(){ FilterNameId=2,
                       FilterValueTranslations=new List<FilterValueTranslation>(){
                            new(){ LanguageId=LanguageId.English, Value = "Green" },
                            new(){ LanguageId=LanguageId.Ukrainian, Value="Зелений"}
                       }
                   },

/*-3- 11*/         new(){ FilterNameId=3,
                       FilterValueTranslations=new List<FilterValueTranslation>(){
                            new(){ LanguageId=LanguageId.English, Value = "Without sleeves" },
                            new(){ LanguageId=LanguageId.Ukrainian, Value="Без рукавів"}
                       }
                   },
/*-3- 12*/         new(){ FilterNameId=3,
                          FilterValueTranslations=new List<FilterValueTranslation>(){
                                new(){ LanguageId=LanguageId.English, Value = "With long sleeves" },
                                new(){ LanguageId=LanguageId.Ukrainian, Value="З довгими рукавами"}
                          }
                   },
/*-3- 13*/         new(){ FilterNameId=3,
                       FilterValueTranslations=new List<FilterValueTranslation>(){
                            new(){ LanguageId=LanguageId.English, Value = "With short sleeves" },
                            new(){ LanguageId=LanguageId.Ukrainian, Value="З короткими рукавами"}
                       }
                   },
/*-3- 14*/         new(){ FilterNameId=3,
                       FilterValueTranslations=new List<FilterValueTranslation>(){
                            new(){ LanguageId=LanguageId.English, Value = "Seven eighths" },
                            new(){ LanguageId=LanguageId.Ukrainian, Value="Сім восьмих"}
                       }
                   },
/*-3- 15*/         new(){ FilterNameId=3,
                       FilterValueTranslations=new List<FilterValueTranslation>(){
                            new(){ LanguageId=LanguageId.English, Value = "Three quarters" },
                            new(){ LanguageId=LanguageId.Ukrainian, Value="Три чверті"}
                       }
                   },

/*-4- 16*/         new(){ FilterNameId=4,
                      FilterValueTranslations=new List<FilterValueTranslation>(){
                           new(){ LanguageId=LanguageId.English, Value = "Maxi" },
                           new(){ LanguageId=LanguageId.Ukrainian, Value="Максі"}
                      }
                   },
/*-4- 17*/         new(){ FilterNameId=4,
                       FilterValueTranslations=new List<FilterValueTranslation>(){
                            new(){ LanguageId=LanguageId.English, Value = "Midi" },
                            new(){ LanguageId=LanguageId.Ukrainian, Value="Міді"}
                       }
                   },
/*-4- 18*/         new(){ FilterNameId=4,
                       FilterValueTranslations=new List<FilterValueTranslation>(){
                            new(){ LanguageId=LanguageId.English, Value = "Mini" },
                            new(){ LanguageId=LanguageId.Ukrainian, Value="Міні"}
                       }
                   },
/*-4- 19*/         new(){ FilterNameId=4,
                       FilterValueTranslations=new List<FilterValueTranslation>(){
                            new(){ LanguageId=LanguageId.English, Value = "Extended" },
                            new(){ LanguageId=LanguageId.Ukrainian, Value="Подовжена"}
                       }
                   },

/*-5- 20*/         new(){ FilterNameId=5,
                      FilterValueTranslations=new List<FilterValueTranslation>(){
                           new(){ LanguageId=LanguageId.English, Value = "V-neck" },
                           new(){ LanguageId=LanguageId.Ukrainian, Value="V-подібний виріз"}
                      }
                   },
/*-5- 21*/         new(){ FilterNameId=5,
                       FilterValueTranslations=new List<FilterValueTranslation>(){
                            new(){ LanguageId=LanguageId.English, Value = "No collar" },
                            new(){ LanguageId=LanguageId.Ukrainian, Value="Без коміра"}
                       }
                   },
/*-5- 22*/         new(){ FilterNameId=5,
                       FilterValueTranslations=new List<FilterValueTranslation>(){
                            new(){ LanguageId=LanguageId.English, Value = "Deep" },
                            new(){ LanguageId=LanguageId.Ukrainian, Value="Глибокий"}
                       }
                   },
/*-5- 23*/         new(){ FilterNameId=5,
                       FilterValueTranslations=new List<FilterValueTranslation>(){
                            new(){ LanguageId=LanguageId.English, Value = "With a collar" },
                            new(){ LanguageId=LanguageId.Ukrainian, Value="З коміром"}
                       }
                   },
/*-5- 24*/         new(){ FilterNameId=5,
                       FilterValueTranslations=new List<FilterValueTranslation>(){
                            new(){ LanguageId=LanguageId.English, Value = "Круглий виріз" },
                            new(){ LanguageId=LanguageId.Ukrainian, Value="Круглий виріз"}
                       }
                   },
/*-5- 25*/         new(){ FilterNameId=5,
                       FilterValueTranslations=new List<FilterValueTranslation>(){
                            new(){ LanguageId=LanguageId.English, Value = "Under the neck" },
                            new(){ LanguageId=LanguageId.Ukrainian, Value="Під горло"}
                       }
                   },

/*-6- 26*/        new(){ FilterNameId = 6,
                      FilterValueTranslations=new List<FilterValueTranslation>(){
                    new(){ LanguageId=LanguageId.English, Value = "Bershka" },
                    new(){ LanguageId=LanguageId.Ukrainian, Value="Bershka"} } },
/*-6- 27*/        new(){ FilterNameId = 6,
                      FilterValueTranslations=new List<FilterValueTranslation>(){
                    new(){ LanguageId=LanguageId.English, Value = "Puma" },
                    new(){ LanguageId=LanguageId.Ukrainian, Value="Puma"} } },
/*-6- 28*/        new(){ FilterNameId = 6,
                      FilterValueTranslations=new List<FilterValueTranslation>(){
                    new(){ LanguageId=LanguageId.English, Value = "Zara"},
                    new(){ LanguageId=LanguageId.Ukrainian, Value="Zara"} } },
/*-6- 29*/        new(){ FilterNameId = 6,
                      FilterValueTranslations=new List<FilterValueTranslation>(){
                    new(){ LanguageId=LanguageId.English, Value = "H&M" },
                    new(){ LanguageId=LanguageId.Ukrainian, Value="H&M"} } },


/*-7- 30*/        new(){ FilterNameId = 7,
                         FilterValueTranslations=new List<FilterValueTranslation>(){
                            new(){ LanguageId=LanguageId.English, Value = "A-line dress" },
                            new(){ LanguageId=LanguageId.Ukrainian, Value="Плаття А-силуета"}
                         }
                  },
/*-7- 31*/        new(){ FilterNameId = 7,
                         FilterValueTranslations=new List<FilterValueTranslation>(){
                            new(){ LanguageId=LanguageId.English, Value = "Wrap dress" },
                            new(){ LanguageId=LanguageId.Ukrainian, Value="Плаття на запах"}
                         }
                  },
/*-7- 32*/        new(){ FilterNameId = 7,
                         FilterValueTranslations=new List<FilterValueTranslation>(){
                            new(){ LanguageId=LanguageId.English, Value = "A dress with a lush skirt" },
                            new(){ LanguageId=LanguageId.Ukrainian, Value="Плаття із пишною спідницею"}
                         }
                  },
/*-7- 33*/        new(){ FilterNameId = 7,
                         FilterValueTranslations=new List<FilterValueTranslation>(){
                            new(){ LanguageId=LanguageId.English, Value = "Blazer dresses" },
                            new(){ LanguageId=LanguageId.Ukrainian, Value="Плаття-блейзери"}
                         }
                  },
/*-7- 34*/        new(){ FilterNameId = 7,
                         FilterValueTranslations=new List<FilterValueTranslation>(){
                            new(){ LanguageId=LanguageId.English, Value = "Golf dress" },
                            new(){ LanguageId=LanguageId.Ukrainian, Value="Плаття-гольф"}
                         }
                  },
/*-7- 35*/        new(){ FilterNameId = 7,
                         FilterValueTranslations=new List<FilterValueTranslation>(){
                            new(){ LanguageId=LanguageId.English, Value = "Dresses-combinations" },
                            new(){ LanguageId=LanguageId.Ukrainian, Value="Плаття-комбінації"}
                         }
                  },
/*-7- 36*/        new(){ FilterNameId = 7,
                         FilterValueTranslations=new List<FilterValueTranslation>(){
                            new(){ LanguageId=LanguageId.English, Value = "Dress-shirts" },
                            new(){ LanguageId=LanguageId.Ukrainian, Value="Плаття-сорочки"}
                         }
                  },
/*-7- 37*/        new(){ FilterNameId = 7,
                         FilterValueTranslations=new List<FilterValueTranslation>(){
                            new(){ LanguageId=LanguageId.English, Value = "Shift dress" },
                            new(){ LanguageId=LanguageId.Ukrainian, Value="Плаття-трапеції"}
                         }
                  },
/*-7- 38*/        new(){ FilterNameId = 7,
                         FilterValueTranslations=new List<FilterValueTranslation>(){
                            new(){ LanguageId=LanguageId.English, Value = "Tunic dresses" },
                            new(){ LanguageId=LanguageId.Ukrainian, Value="Плаття-туніки"}
                         }
                  },
/*-7- 39*/        new(){ FilterNameId = 7,
                         FilterValueTranslations=new List<FilterValueTranslation>(){
                            new(){ LanguageId=LanguageId.English, Value = "Tulip dress" },
                            new(){ LanguageId=LanguageId.Ukrainian, Value="Плаття-тюльпани"}
                         }
                  },
/*-7- 40*/        new(){ FilterNameId = 7,
                         FilterValueTranslations=new List<FilterValueTranslation>(){
                            new(){ LanguageId=LanguageId.English, Value = "T-shirt dresses" },
                            new(){ LanguageId=LanguageId.Ukrainian, Value="Плаття-футболки"}
                         }
                  },
/*-7- 41*/        new(){ FilterNameId = 7,
                         FilterValueTranslations=new List<FilterValueTranslation>(){
                            new(){ LanguageId=LanguageId.English, Value = "Sheath dresses" },
                            new(){ LanguageId=LanguageId.Ukrainian, Value="Плаття-футляри"}
                         }
                  },
           
/*-2- 42*/        new(){ FilterNameId = 2,
                         FilterValueTranslations=new List<FilterValueTranslation>(){
                            new(){ LanguageId=LanguageId.English, Value = "Gray" },
                            new(){ LanguageId=LanguageId.Ukrainian, Value="Сірий"}
                         }
                  },
/*-2- 43*/        new(){ FilterNameId = 2,
                         FilterValueTranslations=new List<FilterValueTranslation>(){
                            new(){ LanguageId=LanguageId.English, Value = "Milky color" },
                            new(){ LanguageId=LanguageId.Ukrainian, Value="Молочний"}
                         }
                  },
/*-2- 44*/        new(){ FilterNameId = 2,
                         FilterValueTranslations=new List<FilterValueTranslation>(){
                            new(){ LanguageId=LanguageId.English, Value = "Violet" },
                            new(){ LanguageId=LanguageId.Ukrainian, Value="Фіолетовий"}
                         }
                  },
/*-2- 45*/        new(){ FilterNameId = 2,
                         FilterValueTranslations=new List<FilterValueTranslation>(){
                            new(){ LanguageId=LanguageId.English, Value = "Pink" },
                            new(){ LanguageId=LanguageId.Ukrainian, Value="Рожевий"}
                         }
                  },
/*-2- 46*/        new(){ FilterNameId = 2,
                         FilterValueTranslations=new List<FilterValueTranslation>(){
                            new(){ LanguageId=LanguageId.English, Value = "White" },
                            new(){ LanguageId=LanguageId.Ukrainian, Value="Білий"}
                         }
                  },
/*-2- 47*/        new(){ FilterNameId = 2,
                         FilterValueTranslations=new List<FilterValueTranslation>(){
                            new(){ LanguageId=LanguageId.English, Value = "Beige" },
                            new(){ LanguageId=LanguageId.Ukrainian, Value="Бежевий"}
                         }
                  },

/*-1- 48*/        new(){ FilterNameId = 1,
                         FilterValueTranslations=new List<FilterValueTranslation>(){
                            new(){ LanguageId=LanguageId.English, Value = "2XL" },
                            new(){ LanguageId=LanguageId.Ukrainian, Value="2XL"}
                         }
                  },

/*-7- 49*/        new(){ FilterNameId = 7,
                         FilterValueTranslations=new List<FilterValueTranslation>(){
                            new(){ LanguageId=LanguageId.English, Value = "Tank top dresses" },
                            new(){ LanguageId=LanguageId.Ukrainian, Value="Плаття-майки"}
                         }
                  },
/*-2- 50*/        new(){ FilterNameId = 2,
                         FilterValueTranslations=new List<FilterValueTranslation>(){
                            new(){ LanguageId=LanguageId.English, Value = "Brown" },
                            new(){ LanguageId=LanguageId.Ukrainian, Value="Коричневий"}
                         }
                  },

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
/*1*/             new(){ FilterValueId = 1,  ProductId= 1 },
                  new(){ FilterValueId = 6,  ProductId= 1 },
                  new(){ FilterValueId = 12, ProductId= 1 },
                  new(){ FilterValueId = 17, ProductId= 1 },
                  new(){ FilterValueId = 24, ProductId= 1 },
                  new(){ FilterValueId = 29, ProductId= 1 },
                  new(){ FilterValueId = 30, ProductId= 1 },

/*2*/             new(){ FilterValueId = 2,  ProductId= 2 },
                  new(){ FilterValueId = 12, ProductId= 2 },
                  new(){ FilterValueId = 16, ProductId= 2 },
                  new(){ FilterValueId = 29, ProductId= 2 },
                  new(){ FilterValueId = 42, ProductId= 2 },
                  new(){ FilterValueId = 24, ProductId= 2 },
                  new(){ FilterValueId = 39, ProductId= 2 },

/*3*/             new(){ FilterValueId = 2,  ProductId= 3 },
                  new(){ FilterValueId = 43,  ProductId= 3 },
                  new(){ FilterValueId = 11,  ProductId= 3 },
                  new(){ FilterValueId = 18,  ProductId= 3 },
                  new(){ FilterValueId = 24,  ProductId= 3 },
                  new(){ FilterValueId = 29,  ProductId= 3 },
                  new(){ FilterValueId = 30,  ProductId= 3 },

/*4*/             new(){ FilterValueId = 2,  ProductId= 4 },
                  new(){ FilterValueId = 8,  ProductId= 4 },
                  new(){ FilterValueId = 12,  ProductId= 4 },
                  new(){ FilterValueId = 17,  ProductId= 4 },
                  new(){ FilterValueId = 24, ProductId= 4 },
                  new(){ FilterValueId = 29, ProductId= 4 },
                  new(){ FilterValueId = 30, ProductId= 4 },

/*5*/             new(){ FilterValueId = 2,  ProductId= 5 },
                  new(){ FilterValueId = 6,  ProductId= 5 },
                  new(){ FilterValueId = 13,  ProductId= 5 },
                  new(){ FilterValueId = 18,  ProductId= 5 },
                  new(){ FilterValueId = 24,  ProductId= 5 },
                  new(){ FilterValueId = 29,  ProductId= 5 },
                  new(){ FilterValueId = 40,  ProductId= 5 },

/*6*/             new(){ FilterValueId = 5,  ProductId= 6 },
                  new(){ FilterValueId = 6,  ProductId= 6 },
                  new(){ FilterValueId = 12,  ProductId= 6 },
                  new(){ FilterValueId = 18,  ProductId= 6 },
                  new(){ FilterValueId = 24,  ProductId= 6 },
                  new(){ FilterValueId = 29,  ProductId= 6 },
                  new(){ FilterValueId = 38,  ProductId= 6 },

/*7*/             new(){ FilterValueId = 1,   ProductId= 7 },
                  new(){ FilterValueId = 6,   ProductId= 7 },
                  new(){ FilterValueId = 12,  ProductId= 7 },
                  new(){ FilterValueId = 17,  ProductId= 7 },
                  new(){ FilterValueId = 29,  ProductId= 7 },
                  new(){ FilterValueId = 34,  ProductId= 7 },

/*8*/             new(){ FilterValueId = 3,   ProductId= 8 },
                  new(){ FilterValueId = 6,   ProductId= 8 },
                  new(){ FilterValueId = 12,  ProductId= 8 },
                  new(){ FilterValueId = 18,  ProductId= 8 },
                  new(){ FilterValueId = 22,  ProductId= 8 },
                  new(){ FilterValueId = 29,  ProductId= 8 },
                  new(){ FilterValueId = 30,  ProductId= 8 },

/*9*/             new(){ FilterValueId = 3,   ProductId= 9 },
                  new(){ FilterValueId = 6,   ProductId= 9 },
                  new(){ FilterValueId = 15,  ProductId= 9 },
                  new(){ FilterValueId = 18,  ProductId= 9 },
                  new(){ FilterValueId = 22,  ProductId= 9 },
                  new(){ FilterValueId = 29,  ProductId= 9 },
                  new(){ FilterValueId = 30,  ProductId= 9 },

/*10*/             new(){ FilterValueId = 3,  ProductId= 10 },
                  new(){ FilterValueId = 44,  ProductId= 10 },
                  new(){ FilterValueId = 12,  ProductId= 10 },
                  new(){ FilterValueId = 18,  ProductId= 10 },
                  new(){ FilterValueId = 24,  ProductId= 10 },
                  new(){ FilterValueId = 29,  ProductId= 10 },
                  new(){ FilterValueId = 38,  ProductId= 10 },

/*11*/            new(){ FilterValueId = 2,  ProductId= 11 },
                  new(){ FilterValueId = 44,  ProductId= 11 },
                  new(){ FilterValueId = 12,  ProductId= 11 },
                  new(){ FilterValueId = 18,  ProductId= 11 },
                  new(){ FilterValueId = 24,  ProductId= 11 },
                  new(){ FilterValueId = 29,  ProductId= 11 },
                  new(){ FilterValueId = 38,  ProductId= 11 },

/*12*/            new(){ FilterValueId = 3,  ProductId= 12 },
                  new(){ FilterValueId = 44,  ProductId= 12 },
                  new(){ FilterValueId = 13,  ProductId= 12 },
                  new(){ FilterValueId = 16,  ProductId= 12 },
                  new(){ FilterValueId = 20,  ProductId= 12 },
                  new(){ FilterValueId = 29,  ProductId= 12 },
                  new(){ FilterValueId = 30,  ProductId= 12 },

/*13*/            new(){ FilterValueId = 2,  ProductId= 13 },
                  new(){ FilterValueId = 46,  ProductId= 13 },
                  new(){ FilterValueId = 11,  ProductId= 13 },
                  new(){ FilterValueId = 16,  ProductId= 13 },
                  new(){ FilterValueId = 22,  ProductId= 13 },
                  new(){ FilterValueId = 29,  ProductId= 13 },
                  new(){ FilterValueId = 37,  ProductId= 13 },

/*14*/            new(){ FilterValueId = 3,   ProductId= 14 },
                  new(){ FilterValueId = 9,   ProductId= 14 },
                  new(){ FilterValueId = 13,  ProductId= 14 },
                  new(){ FilterValueId = 16,  ProductId= 14 },
                  new(){ FilterValueId = 24,  ProductId= 14 },
                  new(){ FilterValueId = 29,  ProductId= 14 },
                  new(){ FilterValueId = 30,  ProductId= 14 },

/*15*/            new(){ FilterValueId = 4,   ProductId= 15 },
                  new(){ FilterValueId = 9,   ProductId= 15 },
                  new(){ FilterValueId = 11,  ProductId= 15 },
                  new(){ FilterValueId = 16,  ProductId= 15 },
                  new(){ FilterValueId = 24,  ProductId= 15 },
                  new(){ FilterValueId = 29,  ProductId= 15 },
                  new(){ FilterValueId = 37,  ProductId= 15 },

/*16*/            new(){ FilterValueId = 5,   ProductId= 16 },
                  new(){ FilterValueId = 8,   ProductId= 16 },
                  new(){ FilterValueId = 12,  ProductId= 16 },
                  new(){ FilterValueId = 16,  ProductId= 16 },
                  new(){ FilterValueId = 24,  ProductId= 16 },
                  new(){ FilterValueId = 29,  ProductId= 16 },
                  new(){ FilterValueId = 38,  ProductId= 16 },

/*17*/            new(){ FilterValueId = 1,   ProductId= 17 },
                  new(){ FilterValueId = 47,  ProductId= 17 },
                  new(){ FilterValueId = 11,  ProductId= 17 },
                  new(){ FilterValueId = 16,  ProductId= 17 },
                  new(){ FilterValueId = 24,  ProductId= 17 },
                  new(){ FilterValueId = 29,  ProductId= 17 },
                  new(){ FilterValueId = 49,  ProductId= 17 },

/*18*/            new(){ FilterValueId = 2,   ProductId= 18 },
                  new(){ FilterValueId = 43,  ProductId= 18 },
                  new(){ FilterValueId = 11,  ProductId= 18 },
                  new(){ FilterValueId = 16,  ProductId= 18 },
                  new(){ FilterValueId = 20,  ProductId= 18 },
                  new(){ FilterValueId = 29,  ProductId= 18 },
                  new(){ FilterValueId = 30,  ProductId= 18 },

/*19*/            new(){ FilterValueId = 5,   ProductId= 19 },
                  new(){ FilterValueId = 10,  ProductId= 19 },
                  new(){ FilterValueId = 12,  ProductId= 19 },
                  new(){ FilterValueId = 17,  ProductId= 19 },
                  new(){ FilterValueId = 23,  ProductId= 19 },
                  new(){ FilterValueId = 29,  ProductId= 19 },
                  new(){ FilterValueId = 36,  ProductId= 19 },


/*20*/            new(){ FilterValueId = 2,   ProductId= 20 },
                  new(){ FilterValueId = 9,   ProductId= 20 },
                  new(){ FilterValueId = 12,  ProductId= 20 },
                  new(){ FilterValueId = 17,  ProductId= 20 },
                  new(){ FilterValueId = 23,  ProductId= 20 },
                  new(){ FilterValueId = 29,  ProductId= 20 },
                  new(){ FilterValueId = 36,  ProductId= 20 },



/*21*/            new(){ FilterValueId = 1,   ProductId= 21 },
                  new(){ FilterValueId = 6,   ProductId= 21 },
                  new(){ FilterValueId = 13,  ProductId= 21 },
                  new(){ FilterValueId = 18,  ProductId= 21 },
                  new(){ FilterValueId = 20,  ProductId= 21 },
                  new(){ FilterValueId = 28,  ProductId= 21 },
                  new(){ FilterValueId = 37,  ProductId= 21 },

/*22*/            new(){ FilterValueId = 2,   ProductId= 22 },
                  new(){ FilterValueId = 45,  ProductId= 22 },
                  new(){ FilterValueId = 13,  ProductId= 22 },
                  new(){ FilterValueId = 18,  ProductId= 22 },
                  new(){ FilterValueId = 20,  ProductId= 22 },
                  new(){ FilterValueId = 28,  ProductId= 22 },
                  new(){ FilterValueId = 39,  ProductId= 22 },

/*23*/            new(){ FilterValueId = 2,   ProductId= 23 },
                  new(){ FilterValueId = 44,  ProductId= 23 },
                  new(){ FilterValueId = 11,  ProductId= 23 },
                  new(){ FilterValueId = 17,  ProductId= 23 },
                  new(){ FilterValueId = 24,  ProductId= 23 },
                  new(){ FilterValueId = 28,  ProductId= 23 },
                  new(){ FilterValueId = 49,  ProductId= 23 },

/*24*/            new(){ FilterValueId = 3,   ProductId= 24 },
                  new(){ FilterValueId = 6,   ProductId= 24 },
                  new(){ FilterValueId = 12,  ProductId= 24 },
                  new(){ FilterValueId = 18,  ProductId= 24 },
                  new(){ FilterValueId = 20,  ProductId= 24 },
                  new(){ FilterValueId = 28,  ProductId= 24 },
                  new(){ FilterValueId = 30,  ProductId= 24 },

/*25*/            new(){ FilterValueId = 4,   ProductId= 25 },
                  new(){ FilterValueId = 10,  ProductId= 25 },
                  new(){ FilterValueId = 12,  ProductId= 25 },
                  new(){ FilterValueId = 18,  ProductId= 25 },
                  new(){ FilterValueId = 23,  ProductId= 25 },
                  new(){ FilterValueId = 28,  ProductId= 25 },
                  new(){ FilterValueId = 36,  ProductId= 25 },

/*26*/            new(){ FilterValueId = 2,   ProductId= 26 },
                  new(){ FilterValueId = 46,  ProductId= 26 },
                  new(){ FilterValueId = 11,  ProductId= 26 },
                  new(){ FilterValueId = 17,  ProductId= 26 },
                  new(){ FilterValueId = 24,  ProductId= 26 },
                  new(){ FilterValueId = 28,  ProductId= 26 },
                  new(){ FilterValueId = 49,  ProductId= 26 },

/*27*/            new(){ FilterValueId = 3,   ProductId= 27 },
                  new(){ FilterValueId = 46,  ProductId= 27 },
                  new(){ FilterValueId = 12,  ProductId= 27 },
                  new(){ FilterValueId = 18,  ProductId= 27 },
                  new(){ FilterValueId = 24,  ProductId= 27 },
                  new(){ FilterValueId = 28,  ProductId= 27 },
                  new(){ FilterValueId = 36,  ProductId= 27 },

/*28*/            new(){ FilterValueId = 2,   ProductId= 28 },
                  new(){ FilterValueId = 44,  ProductId= 28 },
                  new(){ FilterValueId = 12,  ProductId= 28 },
                  new(){ FilterValueId = 18,  ProductId= 28 },
                  new(){ FilterValueId = 20,  ProductId= 28 },
                  new(){ FilterValueId = 28,  ProductId= 28 },
                  new(){ FilterValueId = 31,  ProductId= 28 },

/*29*/            new(){ FilterValueId = 2,   ProductId= 29 },
                  new(){ FilterValueId = 43,  ProductId= 29 },
                  new(){ FilterValueId = 11,  ProductId= 29 },
                  new(){ FilterValueId = 18,  ProductId= 29 },
                  new(){ FilterValueId = 22,  ProductId= 29 },
                  new(){ FilterValueId = 28,  ProductId= 29 },
                  new(){ FilterValueId = 35,  ProductId= 29 },

/*30*/            new(){ FilterValueId = 5,   ProductId= 30 },
                  new(){ FilterValueId = 50,  ProductId= 30 },
                  new(){ FilterValueId = 12,  ProductId= 30 },
                  new(){ FilterValueId = 17,  ProductId= 30 },
                  new(){ FilterValueId = 24,  ProductId= 30 },
                  new(){ FilterValueId = 28,  ProductId= 30 },
                  new(){ FilterValueId = 30,  ProductId= 30 },

/*31*/            new(){ FilterValueId = 1,   ProductId= 31 },
                  new(){ FilterValueId = 9,   ProductId= 31 },
                  new(){ FilterValueId = 13,  ProductId= 31 },
                  new(){ FilterValueId = 18,  ProductId= 31 },
                  new(){ FilterValueId = 22,  ProductId= 31 },
                  new(){ FilterValueId = 28,  ProductId= 31 },
                  new(){ FilterValueId = 39,  ProductId= 31 },

/*32*/            new(){ FilterValueId = 3,   ProductId= 32 },
                  new(){ FilterValueId = 7,   ProductId= 32 },
                  new(){ FilterValueId = 13,  ProductId= 32 },
                  new(){ FilterValueId = 17,  ProductId= 32 },
                  new(){ FilterValueId = 20,  ProductId= 32 },
                  new(){ FilterValueId = 28,  ProductId= 32 },
                  new(){ FilterValueId = 40,  ProductId= 32 },

/*33*/            new(){ FilterValueId = 2,   ProductId= 33 },
                  new(){ FilterValueId = 44,  ProductId= 33 },
                  new(){ FilterValueId = 13,  ProductId= 33 },
                  new(){ FilterValueId = 18,  ProductId= 33 },
                  new(){ FilterValueId = 22,  ProductId= 33 },
                  new(){ FilterValueId = 28,  ProductId= 33 },
                  new(){ FilterValueId = 39,  ProductId= 33 },

/*34*/            new(){ FilterValueId = 5,   ProductId= 34 },
                  new(){ FilterValueId = 44,  ProductId= 34 },
                  new(){ FilterValueId = 13,  ProductId= 34 },
                  new(){ FilterValueId = 18,  ProductId= 34 },
                  new(){ FilterValueId = 22,  ProductId= 34 },
                  new(){ FilterValueId = 28,  ProductId= 34 },
                  new(){ FilterValueId = 39,  ProductId= 34 },

/*35*/            new(){ FilterValueId = 1,   ProductId= 35 },
                  new(){ FilterValueId = 6,   ProductId= 35 },
                  new(){ FilterValueId = 11,  ProductId= 35 },
                  new(){ FilterValueId = 17,  ProductId= 35 },
                  new(){ FilterValueId = 20,  ProductId= 35 },
                  new(){ FilterValueId = 28,  ProductId= 35 },
                  new(){ FilterValueId = 30,  ProductId= 35 },

/*36*/            new(){ FilterValueId = 1,   ProductId= 36 },
                  new(){ FilterValueId = 10,  ProductId= 36 },
                  new(){ FilterValueId = 12,  ProductId= 36 },
                  new(){ FilterValueId = 17,  ProductId= 36 },
                  new(){ FilterValueId = 20,  ProductId= 36 },
                  new(){ FilterValueId = 28,  ProductId= 36 },
                  new(){ FilterValueId = 38,  ProductId= 36 },

/*37*/            new(){ FilterValueId = 4,   ProductId= 37 },
                  new(){ FilterValueId = 44,  ProductId= 37 },
                  new(){ FilterValueId = 11,  ProductId= 37 },
                  new(){ FilterValueId = 17,  ProductId= 37 },
                  new(){ FilterValueId = 20,  ProductId= 37 },
                  new(){ FilterValueId = 28,  ProductId= 37 },
                  new(){ FilterValueId = 37,  ProductId= 37 },

/*38*/            new(){ FilterValueId = 2,   ProductId= 38 },
                  new(){ FilterValueId = 9,  ProductId= 38 },
                  new(){ FilterValueId = 14,  ProductId= 38 },
                  new(){ FilterValueId = 17,  ProductId= 38 },
                  new(){ FilterValueId = 24,  ProductId= 38 },
                  new(){ FilterValueId = 28,  ProductId= 38 },
                  new(){ FilterValueId = 30,  ProductId= 38 },

/*39*/            new(){ FilterValueId = 2,   ProductId= 39 },
                  new(){ FilterValueId = 6,   ProductId= 39 },
                  new(){ FilterValueId = 12,  ProductId= 39 },
                  new(){ FilterValueId = 19,  ProductId= 39 },
                  new(){ FilterValueId = 25,  ProductId= 39 },
                  new(){ FilterValueId = 28,  ProductId= 39 },
                  new(){ FilterValueId = 34,  ProductId= 39 },

/*40*/            new(){ FilterValueId = 2,   ProductId= 40 },
                  new(){ FilterValueId = 46,  ProductId= 40 },
                  new(){ FilterValueId = 15,  ProductId= 40 },
                  new(){ FilterValueId = 17,  ProductId= 40 },
                  new(){ FilterValueId = 21,  ProductId= 40 },
                  new(){ FilterValueId = 28,  ProductId= 40 },
                  new(){ FilterValueId = 37,  ProductId= 40 },



/*41*/            new(){ FilterValueId = 48,   ProductId= 41 },
                  new(){ FilterValueId = 6,  ProductId= 41 },
                  new(){ FilterValueId = 11,  ProductId= 41 },
                  new(){ FilterValueId = 18,  ProductId= 41 },
                  new(){ FilterValueId = 24,  ProductId= 41 },
                  new(){ FilterValueId = 26,  ProductId= 41 },
                  new(){ FilterValueId = 41,  ProductId= 41 },

/*42*/            new(){ FilterValueId = 5,  ProductId= 42 },
                  new(){ FilterValueId = 42,  ProductId= 42 },
                  new(){ FilterValueId = 11,  ProductId= 42 },
                  new(){ FilterValueId = 16,  ProductId= 42 },
                  new(){ FilterValueId = 20,  ProductId= 42 },
                  new(){ FilterValueId = 26,  ProductId= 42 },
                  new(){ FilterValueId = 32,  ProductId= 42 },

/*43*/            new(){ FilterValueId = 3,   ProductId= 43 },
                  new(){ FilterValueId = 6,  ProductId= 43 },
                  new(){ FilterValueId = 12,  ProductId= 43 },
                  new(){ FilterValueId = 18,  ProductId= 43 },
                  new(){ FilterValueId = 20,  ProductId= 43 },
                  new(){ FilterValueId = 26,  ProductId= 43 },
                  new(){ FilterValueId = 33,  ProductId= 43 },

/*44*/            new(){ FilterValueId = 3,   ProductId= 44 },
                  new(){ FilterValueId = 6,   ProductId= 44 },
                  new(){ FilterValueId = 12,  ProductId= 44 },
                  new(){ FilterValueId = 17,  ProductId= 44 },
                  new(){ FilterValueId = 24,  ProductId= 44 },
                  new(){ FilterValueId = 26,  ProductId= 44 },
                  new(){ FilterValueId = 39,  ProductId= 44 },


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
                  new(){ Id=1, DarkIcon="truck.png",LightIcon="truck.png", DeliveryTypeTranslations=new List<DeliveryTypeTranslation>(){
                        new (){ LanguageId=LanguageId.English, Name="Nova Poshta"},
                        new (){ LanguageId=LanguageId.Ukrainian, Name="Нова Пошта"} } },
                  new(){ Id=2, DarkIcon="truck.png",LightIcon="truck.png", DeliveryTypeTranslations=new List<DeliveryTypeTranslation>(){
                        new (){ LanguageId=LanguageId.English, Name="Ukrposhta"},
                        new (){ LanguageId=LanguageId.Ukrainian, Name="Укрпошта"} } },
                  new(){ Id=3, DarkIcon="package.png",LightIcon="package.png", DeliveryTypeTranslations=new List<DeliveryTypeTranslation>(){
                        new (){ LanguageId=LanguageId.English, Name="Pickup from the post office"},
                        new (){ LanguageId=LanguageId.Ukrainian, Name="Самовивіз з пошти"} } }
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
