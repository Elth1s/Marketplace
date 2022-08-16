using DAL.Constants;
using DAL.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace DAL.Data
{
    public class MarketplaceDbContextSeed
    {
        public static async Task SeedAsync(MarketplaceDbContext marketplaceDbContext, UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager)
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


            if (!await marketplaceDbContext.Cities.AnyAsync())
            {
                await marketplaceDbContext.Cities.AddRangeAsync(
                  GetPreconfiguredCities());

                await marketplaceDbContext.SaveChangesAsync();
            }

            if (!await marketplaceDbContext.Categories.AnyAsync())
            {
                await marketplaceDbContext.Categories.AddRangeAsync(
                  GetPreconfiguredMarketplaceCategories());

                await marketplaceDbContext.SaveChangesAsync();
            }
            var WomensClothes = marketplaceDbContext.Categories.Where(c => c.Id == 21).FirstOrDefault();

            if (!await marketplaceDbContext.Shops.AnyAsync())
            {
                await marketplaceDbContext.Shops.AddRangeAsync(
                  GetPreconfiguredMarketplaceShops(defaultUser.Id));

                await marketplaceDbContext.SaveChangesAsync();
            }

            if (!await marketplaceDbContext.ProductStatuses.AnyAsync())
            {
                await marketplaceDbContext.ProductStatuses.AddRangeAsync(
                  GetPreconfiguredMarketplaceProductStatus());

                await marketplaceDbContext.SaveChangesAsync();
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

        }

        static IEnumerable<Country> GetPreconfiguredCountries()
        {
            var countries = new List<Country>()
            {
                    new(){ Code= "AF", CountryTranslations=new List<CountryTranslation>(){
                        new CountryTranslation(){LanguageId=LanguageId.English, Name= "Afghanistan" } ,
                        new CountryTranslation(){LanguageId=LanguageId.Ukrainian, Name= "Афганістан " }} },
                    new(){Code= "AX", CountryTranslations=new List<CountryTranslation>(){
                        new CountryTranslation(){LanguageId=LanguageId.English, Name= "Alland Islands" },
                        new CountryTranslation(){LanguageId=LanguageId.Ukrainian, Name= "Аландські острови" }} },
                    new() {  Code = "AL", CountryTranslations=new List<CountryTranslation>(){
                        new CountryTranslation(){LanguageId=LanguageId.English, Name = "Albania" } } },
                    new() {  Code = "DZ", CountryTranslations=new List<CountryTranslation>(){
                        new CountryTranslation(){LanguageId=LanguageId.English, Name = "Algeria" } } },
                    new() {  Code = "AS" , CountryTranslations=new List<CountryTranslation>(){
                        new CountryTranslation(){LanguageId=LanguageId.English, Name = "American Samoa" } } },
                    new() { Code = "AD", CountryTranslations=new List<CountryTranslation>(){
                        new CountryTranslation(){LanguageId=LanguageId.English, Name = "Andorra" } } },
                    new() {  Code = "AO", CountryTranslations=new List<CountryTranslation>(){
                        new CountryTranslation(){LanguageId=LanguageId.English, Name = "Angola" } } },
                    new() {  Code = "AI", CountryTranslations=new List<CountryTranslation>(){
                        new CountryTranslation(){LanguageId=LanguageId.English, Name = "Anguilla" } } },
                    new() {  Code = "AQ", CountryTranslations=new List<CountryTranslation>(){
                        new CountryTranslation(){LanguageId=LanguageId.English, Name = "Antarctica" } } },
                    new() {  Code = "AG" , CountryTranslations=new List<CountryTranslation>(){
                        new CountryTranslation(){LanguageId=LanguageId.English, Name = "Antigua and Barbuda" } } },
                    new() { Code = "AR", CountryTranslations=new List<CountryTranslation>(){
                        new CountryTranslation(){LanguageId=LanguageId.English, Name = "Argentina" } } },
                    new() {  Code = "AM" , CountryTranslations=new List<CountryTranslation>(){
                        new CountryTranslation(){LanguageId=LanguageId.English, Name = "Armenia" } } },
                    new() { Code = "AW" , CountryTranslations=new List<CountryTranslation>(){
                        new CountryTranslation(){LanguageId=LanguageId.English, Name = "Aruba" } } },
                    new() {  Code = "AU", CountryTranslations=new List<CountryTranslation>(){
                        new CountryTranslation(){LanguageId=LanguageId.English, Name = "Australia" } } },
                    new() { Code = "AT" , CountryTranslations=new List<CountryTranslation>(){
                        new CountryTranslation(){LanguageId=LanguageId.English, Name = "Austria" } } },
                    new() {  Code = "AZ", CountryTranslations=new List<CountryTranslation>(){
                        new CountryTranslation(){LanguageId=LanguageId.English, Name = "Azerbaijan" } } },
                    new() {  Code = "BS", CountryTranslations=new List<CountryTranslation>(){
                        new CountryTranslation(){LanguageId=LanguageId.English, Name = "Bahamas" } } },
                    new() { Code = "BH", CountryTranslations=new List<CountryTranslation>(){
                        new CountryTranslation(){LanguageId=LanguageId.English, Name = "Bahrain" } } },
                    new() {  Code = "BD" , CountryTranslations=new List<CountryTranslation>(){
                        new CountryTranslation(){LanguageId=LanguageId.English, Name = "Bangladesh" } } },
                    new() { Code = "BB", CountryTranslations=new List<CountryTranslation>(){
                        new CountryTranslation(){LanguageId=LanguageId.English, Name = "Barbados" } } },
                    new() { Code = "BY" , CountryTranslations=new List<CountryTranslation>(){
                        new CountryTranslation(){LanguageId=LanguageId.English, Name = "Belarus" } } },
                    new() { Code = "BE", CountryTranslations=new List<CountryTranslation>(){
                        new CountryTranslation(){LanguageId=LanguageId.English, Name = "Belgium" } } },
                    new() {  Code = "BZ", CountryTranslations=new List<CountryTranslation>(){
                        new CountryTranslation(){LanguageId=LanguageId.English, Name = "Belize" } } },
                    new() { Code = "BJ" , CountryTranslations=new List<CountryTranslation>(){
                        new CountryTranslation(){LanguageId=LanguageId.English, Name = "Benin" } } },
                    new() {  Code = "BM" , CountryTranslations=new List<CountryTranslation>(){
                        new CountryTranslation(){LanguageId=LanguageId.English, Name = "Bermuda" } } },
                    new() {  Code = "BT", CountryTranslations=new List<CountryTranslation>(){
                        new CountryTranslation(){LanguageId=LanguageId.English, Name = "Bhutan" } } },
                    new() { Code = "BO" , CountryTranslations=new List<CountryTranslation>(){
                        new CountryTranslation(){LanguageId=LanguageId.English, Name = "Bolivia" } } },
                    new() { Code = "BA", CountryTranslations=new List<CountryTranslation>(){
                        new CountryTranslation(){LanguageId=LanguageId.English, Name = "Bosnia and Herzegovina" } } },
                    new() {  Code = "BW" , CountryTranslations=new List<CountryTranslation>(){
                        new CountryTranslation(){LanguageId=LanguageId.English, Name = "Botswana" } } },
                    new() {  Code = "BV" , CountryTranslations=new List<CountryTranslation>(){
                        new CountryTranslation(){LanguageId=LanguageId.English, Name = "Bouvet Island" } } },
                    new() {  Code = "BR" , CountryTranslations=new List<CountryTranslation>(){
                        new CountryTranslation(){LanguageId=LanguageId.English, Name = "Brazil" } } },
                    new() { Code = "IO", CountryTranslations=new List<CountryTranslation>(){
                        new CountryTranslation(){LanguageId=LanguageId.English, Name = "British Indian Ocean Territory" } } },
                    new() { Code = "VG" , CountryTranslations=new List<CountryTranslation>(){
                        new CountryTranslation(){LanguageId=LanguageId.English, Name = "British Virgin Islands" } } },
                    new() { Code = "BN" , CountryTranslations=new List<CountryTranslation>(){
                        new CountryTranslation(){LanguageId=LanguageId.English, Name = "Brunei Darussalam" } } },
                    new() { Code = "BG", CountryTranslations=new List<CountryTranslation>(){
                        new CountryTranslation(){LanguageId=LanguageId.English, Name = "Bulgaria" } } },
                    new() {  Code = "BF" , CountryTranslations=new List<CountryTranslation>(){
                        new CountryTranslation(){LanguageId=LanguageId.English, Name = "Burkina Faso" } } },
                    new() {  Code = "BI" , CountryTranslations=new List<CountryTranslation>(){
                        new CountryTranslation(){LanguageId=LanguageId.English, Name = "Burundi" } } },
                    new() {  Code = "KH", CountryTranslations=new List<CountryTranslation>(){
                        new CountryTranslation(){LanguageId=LanguageId.English, Name = "Cambodia" } } },
                    new() {  Code = "CM", CountryTranslations=new List<CountryTranslation>(){
                        new CountryTranslation(){LanguageId=LanguageId.English, Name = "Cameroon" } } },
                    new() {  Code = "CA" , CountryTranslations=new List<CountryTranslation>(){
                        new CountryTranslation(){LanguageId=LanguageId.English, Name = "Canada" } } },
                    new() { Code = "CV" , CountryTranslations=new List<CountryTranslation>(){
                        new CountryTranslation(){LanguageId=LanguageId.English, Name = "Cape Verde" } } },
                    new() {  Code = "KY" , CountryTranslations=new List<CountryTranslation>(){
                        new CountryTranslation(){LanguageId=LanguageId.English, Name = "Cayman Islands" } } },
                    new() { Code = "CF" , CountryTranslations=new List<CountryTranslation>(){
                        new CountryTranslation(){LanguageId=LanguageId.English, Name = "Central African Republic" } } },
                    new() {  Code = "TD" , CountryTranslations=new List<CountryTranslation>(){
                        new CountryTranslation(){LanguageId=LanguageId.English, Name = "Chad" } } },
                    new() { Code = "CL", CountryTranslations=new List<CountryTranslation>(){
                        new CountryTranslation(){LanguageId=LanguageId.English, Name = "Chile" } } },
                    new() { Code = "CN" , CountryTranslations=new List<CountryTranslation>(){
                        new CountryTranslation(){LanguageId=LanguageId.English, Name = "China" } } },
                    new() {  Code = "CX" , CountryTranslations=new List<CountryTranslation>(){
                        new CountryTranslation(){LanguageId=LanguageId.English, Name = "Christmas Island" } } },
                    new() { Code = "CC" , CountryTranslations=new List<CountryTranslation>(){
                        new CountryTranslation(){LanguageId=LanguageId.English, Name = "Cocos (Keeling) Islands" } } },
                    new() {  Code = "CO", CountryTranslations=new List<CountryTranslation>(){
                        new CountryTranslation(){LanguageId=LanguageId.English, Name = "Colombia" } } },
                    new() { Code = "KM" , CountryTranslations=new List<CountryTranslation>(){
                        new CountryTranslation(){LanguageId=LanguageId.English, Name = "Comoros" } } },
                    new() {Code = "CG" , CountryTranslations=new List<CountryTranslation>(){
                        new CountryTranslation(){LanguageId=LanguageId.English, Name = "Congo, Democratic Republic of the" } } },
                    new() { Code = "CD" , CountryTranslations=new List<CountryTranslation>(){
                        new CountryTranslation(){LanguageId=LanguageId.English, Name = "Congo, Republic of the" } } },
                    new() {  Code = "CK" , CountryTranslations=new List<CountryTranslation>(){
                        new CountryTranslation(){LanguageId=LanguageId.English, Name = "Cook Islands" } } },
                    new() {  Code = "CR", CountryTranslations=new List<CountryTranslation>(){
                        new CountryTranslation(){LanguageId=LanguageId.English, Name = "Costa Rica" } } },
                    new() { Code = "CI" , CountryTranslations=new List<CountryTranslation>(){
                        new CountryTranslation(){LanguageId=LanguageId.English, Name = "Cote d'Ivoire" } } },
                    new() {  Code = "HR" , CountryTranslations=new List<CountryTranslation>(){
                        new CountryTranslation(){LanguageId=LanguageId.English, Name = "Croatia" } } },
                    new() {  Code = "CU" , CountryTranslations=new List<CountryTranslation>(){
                        new CountryTranslation(){LanguageId=LanguageId.English, Name = "Cuba" } } },
                    new() {  Code = "CW" , CountryTranslations=new List<CountryTranslation>(){
                        new CountryTranslation(){LanguageId=LanguageId.English, Name = "Curacao" } } },
                    new() {  Code = "CY" , CountryTranslations=new List<CountryTranslation>(){
                        new CountryTranslation(){LanguageId=LanguageId.English, Name = "Cyprus" } } },
                    new() {  Code = "CZ" , CountryTranslations=new List<CountryTranslation>(){
                        new CountryTranslation(){LanguageId=LanguageId.English, Name = "Czech Republic" } } },
                    new() {  Code = "DK" , CountryTranslations=new List<CountryTranslation>(){
                        new CountryTranslation(){LanguageId=LanguageId.English, Name = "Denmark" } } },
                    new() {  Code = "DJ" , CountryTranslations=new List<CountryTranslation>(){
                        new CountryTranslation(){LanguageId=LanguageId.English, Name = "Djibouti" } } },
                    new() {  Code = "DM" , CountryTranslations=new List<CountryTranslation>(){
                        new CountryTranslation(){LanguageId=LanguageId.English, Name = "Dominica" } } },
                    new() {  Code = "DO" , CountryTranslations=new List<CountryTranslation>(){
                        new CountryTranslation(){LanguageId=LanguageId.English, Name = "Dominican Republic" } } },
                    new() {  Code = "EC", CountryTranslations=new List<CountryTranslation>(){
                        new CountryTranslation(){LanguageId=LanguageId.English, Name = "Ecuador" } } },
                    new() {  Code = "EG" , CountryTranslations=new List<CountryTranslation>(){
                        new CountryTranslation(){LanguageId=LanguageId.English, Name = "Egypt" } } },
                    new() {  Code = "SV", CountryTranslations=new List<CountryTranslation>(){
                        new CountryTranslation(){LanguageId=LanguageId.English, Name = "El Salvador" } } },
                    new() {  Code = "GQ" , CountryTranslations=new List<CountryTranslation>(){
                        new CountryTranslation(){LanguageId=LanguageId.English, Name = "Equatorial Guinea" } } },
                    new() {  Code = "ER" , CountryTranslations=new List<CountryTranslation>(){
                        new CountryTranslation(){LanguageId=LanguageId.English, Name = "Eritrea" } } },
                    new() {  Code = "EE", CountryTranslations=new List<CountryTranslation>(){
                        new CountryTranslation(){LanguageId=LanguageId.English, Name = "Estonia" } } },
                    new() {  Code = "ET" , CountryTranslations=new List<CountryTranslation>(){
                        new CountryTranslation(){LanguageId=LanguageId.English, Name = "Ethiopia" } } },
                    new() {  Code = "FK" , CountryTranslations=new List<CountryTranslation>(){
                        new CountryTranslation(){LanguageId=LanguageId.English, Name = "Falkland Islands (Malvinas)" } } },
                    new() {  Code = "FO" , CountryTranslations=new List<CountryTranslation>(){
                        new CountryTranslation(){LanguageId=LanguageId.English, Name = "Faroe Islands" } } },
                    new() { Code = "FJ" , CountryTranslations=new List<CountryTranslation>(){
                        new CountryTranslation(){LanguageId=LanguageId.English, Name = "Fiji" } } },
                    new() {  Code = "FI" , CountryTranslations=new List<CountryTranslation>(){
                        new CountryTranslation(){LanguageId=LanguageId.English, Name = "Finland" } } },
                    new() {  Code = "FR", CountryTranslations=new List<CountryTranslation>(){
                        new CountryTranslation(){LanguageId=LanguageId.English, Name = "France" } } },
                    new() {  Code = "GF" , CountryTranslations=new List<CountryTranslation>(){
                        new CountryTranslation(){LanguageId=LanguageId.English, Name = "French Guiana" } } },
                    new() { Code = "PF", CountryTranslations=new List<CountryTranslation>(){
                        new CountryTranslation(){LanguageId=LanguageId.English, Name = "French Polynesia" } } },
                    new() {  Code = "TF" , CountryTranslations=new List<CountryTranslation>(){
                        new CountryTranslation(){LanguageId=LanguageId.English, Name = "French Southern Territories" } } },
                    new() { Code = "GA", CountryTranslations=new List<CountryTranslation>(){
                        new CountryTranslation(){LanguageId=LanguageId.English, Name = "Gabon" } } },
                    new() { Code = "GM" , CountryTranslations=new List<CountryTranslation>(){
                        new CountryTranslation(){LanguageId=LanguageId.English, Name = "Gambia" } } },
                    new() {  Code = "GE" , CountryTranslations=new List<CountryTranslation>(){
                        new CountryTranslation(){LanguageId=LanguageId.English, Name = "Georgia" } } },
                    new() {  Code = "DE" , CountryTranslations=new List<CountryTranslation>(){
                        new CountryTranslation(){LanguageId=LanguageId.English, Name = "Germany" } } },
                    new() {  Code = "GH" , CountryTranslations=new List<CountryTranslation>(){
                        new CountryTranslation(){LanguageId=LanguageId.English, Name = "Ghana" } } },
                    new() {  Code = "GI" , CountryTranslations=new List<CountryTranslation>(){
                        new CountryTranslation(){LanguageId=LanguageId.English, Name = "Gibraltar" } } },
                    new() {  Code = "GR", CountryTranslations=new List<CountryTranslation>(){
                        new CountryTranslation(){LanguageId=LanguageId.English, Name = "Greece" } } },
                    new() {  Code = "GL" , CountryTranslations=new List<CountryTranslation>(){
                        new CountryTranslation(){LanguageId=LanguageId.English, Name = "Greenland" } } },
                    new() { Code = "GD" , CountryTranslations=new List<CountryTranslation>(){
                        new CountryTranslation(){LanguageId=LanguageId.English, Name = "Grenada" } } },
                    new() {  Code = "GP" , CountryTranslations=new List<CountryTranslation>(){
                        new CountryTranslation(){LanguageId=LanguageId.English, Name = "Guadeloupe" } } },
                    new() {  Code = "GU" , CountryTranslations=new List<CountryTranslation>(){
                        new CountryTranslation(){LanguageId=LanguageId.English, Name = "Guam" } } },
                    new() { Code = "GT", CountryTranslations=new List<CountryTranslation>(){
                        new CountryTranslation(){LanguageId=LanguageId.English, Name = "Guatemala" } } },
                    new() {  Code = "GG" , CountryTranslations=new List<CountryTranslation>(){
                        new CountryTranslation(){LanguageId=LanguageId.English, Name = "Guernsey" } } },
                    new() {  Code = "GW" , CountryTranslations=new List<CountryTranslation>(){
                        new CountryTranslation(){LanguageId=LanguageId.English, Name = "Guinea-Bissau" } } },
                    new() {  Code = "GN" , CountryTranslations=new List<CountryTranslation>(){
                        new CountryTranslation(){LanguageId=LanguageId.English, Name = "Guinea" } } },
                    new() {  Code = "GY" , CountryTranslations=new List<CountryTranslation>(){
                        new CountryTranslation(){LanguageId=LanguageId.English, Name = "Guyana" } } },
                    new() {  Code = "HT" , CountryTranslations=new List<CountryTranslation>(){
                        new CountryTranslation(){LanguageId=LanguageId.English, Name = "Haiti" } } },
                    new() {  Code = "HM" , CountryTranslations=new List<CountryTranslation>(){
                        new CountryTranslation(){LanguageId=LanguageId.English, Name = "Heard Island and McDonald Islands" } } },
                    new() {  Code = "VA" , CountryTranslations=new List<CountryTranslation>(){
                        new CountryTranslation(){LanguageId=LanguageId.English, Name = "Holy See (Vatican City State)" } } },
                    new() {  Code = "HN", CountryTranslations=new List<CountryTranslation>(){
                        new CountryTranslation(){LanguageId=LanguageId.English, Name = "Honduras" } } },
                    new() {  Code = "HK" , CountryTranslations=new List<CountryTranslation>(){
                        new CountryTranslation(){LanguageId=LanguageId.English, Name = "Hong Kong" } } },
                    new() {  Code = "HU" , CountryTranslations=new List<CountryTranslation>(){
                        new CountryTranslation(){LanguageId=LanguageId.English, Name = "Hungary" } } },
                    new() {  Code = "IS" , CountryTranslations=new List<CountryTranslation>(){
                        new CountryTranslation(){LanguageId=LanguageId.English, Name = "Iceland" } } },
                    new() {  Code = "IN", CountryTranslations=new List<CountryTranslation>(){
                        new CountryTranslation(){LanguageId=LanguageId.English, Name = "India" } } },
                    new() {  Code = "ID" , CountryTranslations=new List<CountryTranslation>(){
                        new CountryTranslation(){LanguageId=LanguageId.English, Name = "Indonesia" } } },
                    new() {  Code = "IR" , CountryTranslations=new List<CountryTranslation>(){
                        new CountryTranslation(){LanguageId=LanguageId.English, Name = "Iran, Islamic Republic of" } } },
                    new() {  Code = "IQ" , CountryTranslations=new List<CountryTranslation>(){
                        new CountryTranslation(){LanguageId=LanguageId.English, Name = "Iraq" } } },
                    new() { Code = "IE" , CountryTranslations=new List<CountryTranslation>(){
                        new CountryTranslation(){LanguageId=LanguageId.English, Name = "Ireland" } } },
                    new() {  Code = "IM" , CountryTranslations=new List<CountryTranslation>(){
                        new CountryTranslation(){LanguageId=LanguageId.English, Name = "Isle of Man" } } },
                    new() { Code = "IL" , CountryTranslations=new List<CountryTranslation>(){
                        new CountryTranslation(){LanguageId=LanguageId.English, Name = "Israel" } } },
                    new() { Code = "IT", CountryTranslations=new List<CountryTranslation>(){
                        new CountryTranslation(){LanguageId=LanguageId.English, Name = "Italy" } } },
                    new() {  Code = "JM" , CountryTranslations=new List<CountryTranslation>(){
                        new CountryTranslation(){LanguageId=LanguageId.English, Name = "Jamaica" } } },
                    new() {  Code = "JP" , CountryTranslations=new List<CountryTranslation>(){
                        new CountryTranslation(){LanguageId=LanguageId.English, Name = "Japan" } } },
                    new() { Code = "JE" , CountryTranslations=new List<CountryTranslation>(){
                        new CountryTranslation(){LanguageId=LanguageId.English, Name = "Jersey" } } },
                    new() {  Code = "JO" , CountryTranslations=new List<CountryTranslation>(){
                        new CountryTranslation(){LanguageId=LanguageId.English, Name = "Jordan" } } },
                    new() {  Code = "KZ" , CountryTranslations=new List<CountryTranslation>(){
                        new CountryTranslation(){LanguageId=LanguageId.English, Name = "Kazakhstan" } } },
                    new() {  Code = "KE" , CountryTranslations=new List<CountryTranslation>(){
                        new CountryTranslation(){LanguageId=LanguageId.English, Name = "Kenya" } } },
                    new() { Code = "KI" , CountryTranslations=new List<CountryTranslation>(){
                        new CountryTranslation(){LanguageId=LanguageId.English, Name = "Kiribati" } } },
                    new() {  Code = "KP" , CountryTranslations=new List<CountryTranslation>(){
                        new CountryTranslation(){LanguageId=LanguageId.English, Name = "Korea, Democratic People's Republic of" } } },
                    new() {  Code = "KR" , CountryTranslations=new List<CountryTranslation>(){
                        new CountryTranslation(){LanguageId=LanguageId.English, Name = "Korea, Republic of" } } },
                    new() {  Code = "XK" , CountryTranslations=new List<CountryTranslation>(){
                        new CountryTranslation(){LanguageId=LanguageId.English, Name = "Kosovo" } } },
                    new() {  Code = "KW", CountryTranslations=new List<CountryTranslation>(){
                        new CountryTranslation(){LanguageId=LanguageId.English, Name = "Kuwait" } } },
                    new() {  Code = "KG" , CountryTranslations=new List<CountryTranslation>(){
                        new CountryTranslation(){LanguageId=LanguageId.English, Name = "Kyrgyzstan" } } },
                    new() { Code = "LA" , CountryTranslations=new List<CountryTranslation>(){
                        new CountryTranslation(){LanguageId=LanguageId.English, Name = "Lao People's Democratic Republic" } } },
                    new() {  Code = "LV" , CountryTranslations=new List<CountryTranslation>(){
                        new CountryTranslation(){LanguageId=LanguageId.English, Name = "Latvia" } } },
                    new() {  Code = "LB" , CountryTranslations=new List<CountryTranslation>(){
                        new CountryTranslation(){LanguageId=LanguageId.English, Name = "Lebanon" } } },
                    new() {  Code = "LS" , CountryTranslations=new List<CountryTranslation>(){
                        new CountryTranslation(){LanguageId=LanguageId.English, Name = "Lesotho" } } },
                    new() {  Code = "LR" , CountryTranslations=new List<CountryTranslation>(){
                        new CountryTranslation(){LanguageId=LanguageId.English, Name = "Liberia" } } },
                    new() { Code = "LY" , CountryTranslations=new List<CountryTranslation>(){
                        new CountryTranslation(){LanguageId=LanguageId.English, Name = "Libya" } } },
                    new() {  Code = "LI", CountryTranslations=new List<CountryTranslation>(){
                        new CountryTranslation(){LanguageId=LanguageId.English, Name = "Liechtenstein" } } },
                    new() { Code = "LT" , CountryTranslations=new List<CountryTranslation>(){
                        new CountryTranslation(){LanguageId=LanguageId.English, Name = "Lithuania" } } },
                    new() {  Code = "LU" , CountryTranslations=new List<CountryTranslation>(){
                        new CountryTranslation(){LanguageId=LanguageId.English, Name = "Luxembourg" } } },
                    new() {  Code = "MO" , CountryTranslations=new List<CountryTranslation>(){
                        new CountryTranslation(){LanguageId=LanguageId.English, Name = "Macao" } } },
                    new() {  Code = "MK" , CountryTranslations=new List<CountryTranslation>(){
                        new CountryTranslation(){LanguageId=LanguageId.English, Name = "Macedonia, the Former Yugoslav Republic of" } } },
                    new() { Code = "MG", CountryTranslations=new List<CountryTranslation>(){
                        new CountryTranslation(){LanguageId=LanguageId.English, Name = "Madagascar" } } },
                    new() { Code = "MW" , CountryTranslations=new List<CountryTranslation>(){
                        new CountryTranslation(){LanguageId=LanguageId.English, Name = "Malawi" } } },
                    new() {  Code = "MY" , CountryTranslations=new List<CountryTranslation>(){
                        new CountryTranslation(){LanguageId=LanguageId.English, Name = "Malaysia" } } },
                    new() {  Code = "MV" , CountryTranslations=new List<CountryTranslation>(){
                        new CountryTranslation(){LanguageId=LanguageId.English, Name = "Maldives" } } },
                    new() {  Code = "ML" , CountryTranslations=new List<CountryTranslation>(){
                        new CountryTranslation(){LanguageId=LanguageId.English, Name = "Mali" } } },
                    new() {  Code = "MT" , CountryTranslations=new List<CountryTranslation>(){
                        new CountryTranslation(){LanguageId=LanguageId.English, Name = "Malta" } } },
                    new() {  Code = "MH" , CountryTranslations=new List<CountryTranslation>(){
                        new CountryTranslation(){LanguageId=LanguageId.English, Name = "Marshall Islands" } } },
                    new() { Code = "MQ" , CountryTranslations=new List<CountryTranslation>(){
                        new CountryTranslation(){LanguageId=LanguageId.English, Name = "Martinique" } } },
                    new() {  Code = "MR" , CountryTranslations=new List<CountryTranslation>(){
                        new CountryTranslation(){LanguageId=LanguageId.English, Name = "Mauritania" } } },
                    new() { Code = "MU" , CountryTranslations=new List<CountryTranslation>(){
                        new CountryTranslation(){LanguageId=LanguageId.English, Name = "Mauritius" } } },
                    new() {  Code = "YT" , CountryTranslations=new List<CountryTranslation>(){
                        new CountryTranslation(){LanguageId=LanguageId.English, Name = "Mayotte" } } },
                    new() {  Code = "MX" , CountryTranslations=new List<CountryTranslation>(){
                        new CountryTranslation(){LanguageId=LanguageId.English, Name = "Mexico" } } },
                    new() {  Code = "FM" , CountryTranslations=new List<CountryTranslation>(){
                        new CountryTranslation(){LanguageId=LanguageId.English, Name = "Micronesia, Federated States of" } } },
                    new() {  Code = "MD" , CountryTranslations=new List<CountryTranslation>(){
                        new CountryTranslation(){LanguageId=LanguageId.English, Name = "Moldova, Republic of" } } },
                    new() {  Code = "MC" , CountryTranslations=new List<CountryTranslation>(){
                        new CountryTranslation(){LanguageId=LanguageId.English, Name = "Monaco" } } },
                    new() {  Code = "MN" , CountryTranslations=new List<CountryTranslation>(){
                        new CountryTranslation(){LanguageId=LanguageId.English, Name = "Mongolia" } } },
                    new() {  Code = "ME", CountryTranslations=new List<CountryTranslation>(){
                        new CountryTranslation(){LanguageId=LanguageId.English, Name = "Montenegro" } } },
                    new() { Code = "MS" , CountryTranslations=new List<CountryTranslation>(){
                        new CountryTranslation(){LanguageId=LanguageId.English, Name = "Montserrat" } } },
                    new() {  Code = "MA" , CountryTranslations=new List<CountryTranslation>(){
                        new CountryTranslation(){LanguageId=LanguageId.English, Name = "Morocco" } } },
                    new() { Code = "MZ" , CountryTranslations=new List<CountryTranslation>(){
                        new CountryTranslation(){LanguageId=LanguageId.English, Name = "Mozambique" } } },
                    new() {  Code = "MM" , CountryTranslations=new List<CountryTranslation>(){
                        new CountryTranslation(){LanguageId=LanguageId.English, Name = "Myanmar" } } },
                    new() {  Code = "NA" , CountryTranslations=new List<CountryTranslation>(){
                        new CountryTranslation(){LanguageId=LanguageId.English, Name = "Namibia" } } },
                    new() {  Code = "NR" , CountryTranslations=new List<CountryTranslation>(){
                        new CountryTranslation(){LanguageId=LanguageId.English, Name = "Nauru" } } },
                    new() {  Code = "NP" , CountryTranslations=new List<CountryTranslation>(){
                        new CountryTranslation(){LanguageId=LanguageId.English, Name = "Nepal" } } },
                    new() {  Code = "NL" , CountryTranslations=new List<CountryTranslation>(){
                        new CountryTranslation(){LanguageId=LanguageId.English, Name = "Netherlands" } } },
                    new() {  Code = "NC" , CountryTranslations=new List<CountryTranslation>(){
                        new CountryTranslation(){LanguageId=LanguageId.English, Name = "New Caledonia" } } },
                    new() { Code = "NZ" , CountryTranslations=new List<CountryTranslation>(){
                        new CountryTranslation(){LanguageId=LanguageId.English, Name = "New Zealand" } } },
                    new() { Code = "NI" , CountryTranslations=new List<CountryTranslation>(){
                        new CountryTranslation(){LanguageId=LanguageId.English, Name = "Nicaragua" } } },
                    new() {  Code = "NE" , CountryTranslations=new List<CountryTranslation>(){
                        new CountryTranslation(){LanguageId=LanguageId.English, Name = "Niger" } } },
                    new() {  Code = "NG" , CountryTranslations=new List<CountryTranslation>(){
                        new CountryTranslation(){LanguageId=LanguageId.English, Name = "Nigeria" } } },
                    new() {  Code = "NU" , CountryTranslations=new List<CountryTranslation>(){
                        new CountryTranslation(){LanguageId=LanguageId.English, Name = "Niue" } } },
                    new() { Code = "NF", CountryTranslations=new List<CountryTranslation>(){
                        new CountryTranslation(){LanguageId=LanguageId.English, Name = "Norfolk Island" } } },
                    new() {  Code = "MP", CountryTranslations=new List<CountryTranslation>(){
                        new CountryTranslation(){LanguageId=LanguageId.English, Name = "Northern Mariana Islands" } } },
                    new() {  Code = "NO" , CountryTranslations=new List<CountryTranslation>(){
                        new CountryTranslation(){LanguageId=LanguageId.English, Name = "Norway" } } },
                    new() {  Code = "OM" , CountryTranslations=new List<CountryTranslation>(){
                        new CountryTranslation(){LanguageId=LanguageId.English, Name = "Oman" } } },
                    new() { Code = "PK" , CountryTranslations=new List<CountryTranslation>(){
                        new CountryTranslation(){LanguageId=LanguageId.English, Name = "Pakistan" } } },
                    new() {  Code = "PW" , CountryTranslations=new List<CountryTranslation>(){
                        new CountryTranslation(){LanguageId=LanguageId.English, Name = "Palau" } } },
                    new() {  Code = "PS" , CountryTranslations=new List<CountryTranslation>(){
                        new CountryTranslation(){LanguageId=LanguageId.English, Name = "Palestine, State of" } } },
                    new() {  Code = "PA", CountryTranslations=new List<CountryTranslation>(){
                        new CountryTranslation(){LanguageId=LanguageId.English, Name = "Panama" } } },
                    new() {  Code = "PG" , CountryTranslations=new List<CountryTranslation>(){
                        new CountryTranslation(){LanguageId=LanguageId.English, Name = "Papua New Guinea" } } },
                    new() {  Code = "PY", CountryTranslations=new List<CountryTranslation>(){
                        new CountryTranslation(){LanguageId=LanguageId.English, Name = "Paraguay" } } },
                    new() {  Code = "PE" , CountryTranslations=new List<CountryTranslation>(){
                        new CountryTranslation(){LanguageId=LanguageId.English, Name = "Peru" } } },
                    new() {  Code = "PH" , CountryTranslations=new List<CountryTranslation>(){
                        new CountryTranslation(){LanguageId=LanguageId.English, Name = "Philippines" } } },
                    new() {  Code = "PN" , CountryTranslations=new List<CountryTranslation>(){
                        new CountryTranslation(){LanguageId=LanguageId.English, Name = "Pitcairn" } } },
                    new() {  Code = "PL" , CountryTranslations=new List<CountryTranslation>(){
                        new CountryTranslation(){LanguageId=LanguageId.English, Name = "Poland" } } },
                    new() {  Code = "PT" , CountryTranslations=new List<CountryTranslation>(){
                        new CountryTranslation(){LanguageId=LanguageId.English, Name = "Portugal" } } },
                    new() {  Code = "PR" , CountryTranslations=new List<CountryTranslation>(){
                        new CountryTranslation(){LanguageId=LanguageId.English, Name = "Puerto Rico" } } },
                    new() { Code = "QA" , CountryTranslations=new List<CountryTranslation>(){
                        new CountryTranslation(){LanguageId=LanguageId.English, Name = "Qatar" } } },
                    new() {  Code = "RE" , CountryTranslations=new List<CountryTranslation>(){
                        new CountryTranslation(){LanguageId=LanguageId.English, Name = "Reunion" } } },
                    new() { Code = "RO" , CountryTranslations=new List<CountryTranslation>(){
                        new CountryTranslation(){LanguageId=LanguageId.English, Name = "Romania" } } },
                    new() { Code = "RU" , CountryTranslations=new List<CountryTranslation>(){
                        new CountryTranslation(){LanguageId=LanguageId.English, Name = "Russian Federation" } } },
                    new() {  Code = "RW" , CountryTranslations=new List<CountryTranslation>(){
                        new CountryTranslation(){LanguageId=LanguageId.English, Name = "Rwanda" } } },
                    new() {  Code = "BL" , CountryTranslations=new List<CountryTranslation>(){
                        new CountryTranslation(){LanguageId=LanguageId.English, Name = "Saint Barthelemy" } } },
                    new() {  Code = "SH" , CountryTranslations=new List<CountryTranslation>(){
                        new CountryTranslation(){LanguageId=LanguageId.English, Name = "Saint Helena" } } },
                    new() {  Code = "KN" , CountryTranslations=new List<CountryTranslation>(){
                        new CountryTranslation(){LanguageId=LanguageId.English, Name = "Saint Kitts and Nevis" } } },
                    new() { Code = "LC" , CountryTranslations=new List<CountryTranslation>(){
                        new CountryTranslation(){LanguageId=LanguageId.English, Name = "Saint Lucia" } } },
                    new() {  Code = "MF" , CountryTranslations=new List<CountryTranslation>(){
                        new CountryTranslation(){LanguageId=LanguageId.English, Name = "Saint Martin (French part)" } } },
                    new() {  Code = "PM" , CountryTranslations=new List<CountryTranslation>(){
                        new CountryTranslation(){LanguageId=LanguageId.English, Name = "Saint Pierre and Miquelon" } } },
                    new() {  Code = "VC", CountryTranslations=new List<CountryTranslation>(){
                        new CountryTranslation(){LanguageId=LanguageId.English, Name = "Saint Vincent and the Grenadines" } } },
                    new() {  Code = "WS" , CountryTranslations=new List<CountryTranslation>(){
                        new CountryTranslation(){LanguageId=LanguageId.English, Name = "Samoa" } } },
                    new() {  Code = "SM" , CountryTranslations=new List<CountryTranslation>(){
                        new CountryTranslation(){LanguageId=LanguageId.English, Name = "San Marino" } } },
                    new() {  Code = "ST" , CountryTranslations=new List<CountryTranslation>(){
                        new CountryTranslation(){LanguageId=LanguageId.English, Name = "Sao Tome and Principe" } } },
                    new() { Code = "SA" , CountryTranslations=new List<CountryTranslation>(){
                        new CountryTranslation(){LanguageId=LanguageId.English, Name = "Saudi Arabia" } } },
                    new() {  Code = "SN" , CountryTranslations=new List<CountryTranslation>(){
                        new CountryTranslation(){LanguageId=LanguageId.English, Name = "Senegal" } } },
                    new() { Code = "RS" , CountryTranslations=new List<CountryTranslation>(){
                        new CountryTranslation(){LanguageId=LanguageId.English, Name = "Serbia" } } },
                    new() {  Code = "SC" , CountryTranslations=new List<CountryTranslation>(){
                        new CountryTranslation(){LanguageId=LanguageId.English, Name = "Seychelles" } } },
                    new() {  Code = "SL" , CountryTranslations=new List<CountryTranslation>(){
                        new CountryTranslation(){LanguageId=LanguageId.English, Name = "Sierra Leone" } } },
                    new() {  Code = "SG" , CountryTranslations=new List<CountryTranslation>(){
                        new CountryTranslation(){LanguageId=LanguageId.English, Name = "Singapore" } } },
                    new() {  Code = "SX" , CountryTranslations=new List<CountryTranslation>(){
                        new CountryTranslation(){LanguageId=LanguageId.English, Name = "Sint Maarten (Dutch part)" } } },
                    new() {  Code = "SK", CountryTranslations=new List<CountryTranslation>(){
                        new CountryTranslation(){LanguageId=LanguageId.English, Name = "Slovakia" } } },
                    new() {  Code = "SI" , CountryTranslations=new List<CountryTranslation>(){
                        new CountryTranslation(){LanguageId=LanguageId.English, Name = "Slovenia" } } },
                    new() {  Code = "SB", CountryTranslations=new List<CountryTranslation>(){
                        new CountryTranslation(){LanguageId=LanguageId.English, Name = "Solomon Islands" } } },
                    new() {  Code = "SO" , CountryTranslations=new List<CountryTranslation>(){
                        new CountryTranslation(){LanguageId=LanguageId.English, Name = "Somalia" } } },
                    new() {  Code = "ZA" , CountryTranslations=new List<CountryTranslation>(){
                        new CountryTranslation(){LanguageId=LanguageId.English, Name = "South Africa" } } },
                    new() {  Code = "GS", CountryTranslations=new List<CountryTranslation>(){
                        new CountryTranslation(){LanguageId=LanguageId.English, Name = "South Georgia and the South Sandwich Islands" } } },
                    new() {  Code = "SS" , CountryTranslations=new List<CountryTranslation>(){
                        new CountryTranslation(){LanguageId=LanguageId.English, Name = "South Sudan" } } },
                    new() {  Code = "ES" , CountryTranslations=new List<CountryTranslation>(){
                        new CountryTranslation(){LanguageId=LanguageId.English, Name = "Spain" } } },
                    new() {  Code = "LK", CountryTranslations=new List<CountryTranslation>(){
                        new CountryTranslation(){LanguageId=LanguageId.English, Name = "Sri Lanka" } } },
                    new() { Code = "SD" , CountryTranslations=new List<CountryTranslation>(){
                        new CountryTranslation(){LanguageId=LanguageId.English, Name = "Sudan" } } },
                    new() {  Code = "SR" , CountryTranslations=new List<CountryTranslation>(){
                        new CountryTranslation(){LanguageId=LanguageId.English, Name = "Suriname" } } },
                    new() { Code = "SJ" , CountryTranslations=new List<CountryTranslation>(){
                        new CountryTranslation(){LanguageId=LanguageId.English, Name = "Svalbard and Jan Mayen" } } },
                    new() { Code = "SZ" , CountryTranslations=new List<CountryTranslation>(){
                        new CountryTranslation(){LanguageId=LanguageId.English, Name = "Swaziland" } } },
                    new() {  Code = "SE", CountryTranslations=new List<CountryTranslation>(){
                        new CountryTranslation(){LanguageId=LanguageId.English, Name = "Sweden" } } },
                    new() {  Code = "CH" , CountryTranslations=new List<CountryTranslation>(){
                        new CountryTranslation(){LanguageId=LanguageId.English, Name = "Switzerland" } } },
                    new() { Code = "SY" , CountryTranslations=new List<CountryTranslation>(){
                        new CountryTranslation(){LanguageId=LanguageId.English, Name = "Syrian Arab Republic" } } },
                    new() {  Code = "TW" , CountryTranslations=new List<CountryTranslation>(){
                        new CountryTranslation(){LanguageId=LanguageId.English, Name = "Taiwan, Province of China" } } },
                    new() { Code = "TJ" , CountryTranslations=new List<CountryTranslation>(){
                        new CountryTranslation(){LanguageId=LanguageId.English, Name = "Tajikistan" } } },
                    new() { Code = "TH", CountryTranslations=new List<CountryTranslation>(){
                        new CountryTranslation(){LanguageId=LanguageId.English, Name = "Thailand" } } },
                    new() { Code = "TL" , CountryTranslations=new List<CountryTranslation>(){
                        new CountryTranslation(){LanguageId=LanguageId.English, Name = "Timor-Leste" } } },
                    new() { Code = "TG" , CountryTranslations=new List<CountryTranslation>(){
                        new CountryTranslation(){LanguageId=LanguageId.English, Name = "Togo" } } },
                    new() { Code = "TK" , CountryTranslations=new List<CountryTranslation>(){
                        new CountryTranslation(){LanguageId=LanguageId.English, Name = "Tokelau" } } },
                    new() { Code = "TO" , CountryTranslations=new List<CountryTranslation>(){
                        new CountryTranslation(){LanguageId=LanguageId.English, Name = "Tonga" } } },
                    new() { Code = "TT" , CountryTranslations=new List<CountryTranslation>(){
                        new CountryTranslation(){LanguageId=LanguageId.English, Name = "Trinidad and Tobago" } } },
                    new() { Code = "TN", CountryTranslations=new List<CountryTranslation>(){
                        new CountryTranslation(){LanguageId=LanguageId.English, Name = "Tunisia" } } },
                    new() { Code = "TR", CountryTranslations=new List<CountryTranslation>(){
                        new CountryTranslation(){LanguageId=LanguageId.English, Name = "Turkey" } } },
                    new() {  Code = "TM", CountryTranslations=new List<CountryTranslation>(){
                        new CountryTranslation(){LanguageId=LanguageId.English, Name = "Turkmenistan" } } },
                    new() { Code = "TC" , CountryTranslations=new List<CountryTranslation>(){
                        new CountryTranslation(){LanguageId=LanguageId.English, Name = "Turks and Caicos Islands" } } },
                    new() { Code = "TV" , CountryTranslations=new List<CountryTranslation>(){
                        new CountryTranslation(){LanguageId=LanguageId.English, Name = "Tuvalu" } } },
                    new() {  Code = "UG", CountryTranslations=new List<CountryTranslation>(){
                        new CountryTranslation(){LanguageId=LanguageId.English, Name = "Uganda" } } },
                    new() {  Code = "UA" , CountryTranslations=new List<CountryTranslation>(){
                        new CountryTranslation(){LanguageId=LanguageId.English, Name = "Ukraine" } } },
                    new() {  Code = "AE" , CountryTranslations=new List<CountryTranslation>(){
                        new CountryTranslation(){LanguageId=LanguageId.English, Name = "United Arab Emirates" } } },
                    new() { Code = "GB" , CountryTranslations=new List<CountryTranslation>(){
                        new CountryTranslation(){LanguageId=LanguageId.English, Name = "United Kingdom" } } },
                    new() { Code = "TZ" , CountryTranslations=new List<CountryTranslation>(){
                        new CountryTranslation(){LanguageId=LanguageId.English, Name = "United Republic of Tanzania" } } },
                    new() { Code = "US" , CountryTranslations=new List<CountryTranslation>(){
                        new CountryTranslation(){LanguageId=LanguageId.English, Name = "United States" } } },
                    new() { Code = "UY" , CountryTranslations=new List<CountryTranslation>(){
                        new CountryTranslation(){LanguageId=LanguageId.English, Name = "Uruguay" } } },
                    new() { Code = "VI" , CountryTranslations=new List<CountryTranslation>(){
                        new CountryTranslation(){LanguageId=LanguageId.English, Name = "US Virgin Islands" } } },
                    new() { Code = "UZ" , CountryTranslations=new List<CountryTranslation>(){
                        new CountryTranslation(){LanguageId=LanguageId.English, Name = "Uzbekistan" } } },
                    new() { Code = "VU" , CountryTranslations=new List<CountryTranslation>(){
                        new CountryTranslation(){LanguageId=LanguageId.English, Name = "Vanuatu" } } },
                    new() { Code = "VE" , CountryTranslations=new List<CountryTranslation>(){
                        new CountryTranslation(){LanguageId=LanguageId.English, Name = "Venezuela" } } },
                    new() { Code = "VN" , CountryTranslations=new List<CountryTranslation>(){
                        new CountryTranslation(){LanguageId=LanguageId.English, Name = "Vietnam" } } },
                    new() { Code = "WF" , CountryTranslations=new List<CountryTranslation>(){
                        new CountryTranslation(){LanguageId=LanguageId.English, Name = "Wallis and Futuna" } } },
                    new() { Code = "EH" , CountryTranslations=new List<CountryTranslation>(){
                        new CountryTranslation(){LanguageId=LanguageId.English, Name = "Western Sahara" } } },
                    new() {  Code = "YE" , CountryTranslations=new List<CountryTranslation>(){
                        new CountryTranslation(){LanguageId=LanguageId.English, Name = "Yemen" } } },
                    new() {  Code = "ZM" , CountryTranslations=new List<CountryTranslation>(){
                        new CountryTranslation(){LanguageId=LanguageId.English, Name = "Zambia" } } },
                    new() { Code = "ZW" , CountryTranslations=new List<CountryTranslation>(){
                        new CountryTranslation(){LanguageId=LanguageId.English, Name = "Zimbabwe" } } },
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


        static IEnumerable<Category> GetPreconfiguredMarketplaceCategories()
        {
            var categories = new List<Category>
            {
/*1*/           new(){ UrlSlug = "beauty-and-health", Image = "BeautyAndHealth.png", ParentId = null,
                     CategoryTranslations=new List<CategoryTranslation>(){
                        new(){ Name = "Beauty and health", LanguageId=LanguageId.English },
                        new(){ Name = "Краса і здоров'я", LanguageId=LanguageId.Ukrainian } } },
/*2*/           new(){ UrlSlug = "house-and-garden", Image = "HouseAndGarden.png", ParentId = null,
                     CategoryTranslations=new List<CategoryTranslation>(){
                        new(){ Name = "House and garden",  LanguageId=LanguageId.English },
                        new(){ Name = "Дім і сад", LanguageId=LanguageId.Ukrainian } } },
/*3*/           new(){ UrlSlug = "clothes-and-shoes", Image = "ClothesAndShoes.png", ParentId = null,
                     CategoryTranslations=new List<CategoryTranslation>(){
                        new(){ Name = "Clothes and shoes", LanguageId=LanguageId.English },
                        new(){ Name = "Одяг та взуття", LanguageId=LanguageId.Ukrainian } } },
/*4*/           new(){ UrlSlug = "technology-and-electronics", Image = "TechnologyAndElectronics.png", ParentId = null,
                     CategoryTranslations=new List<CategoryTranslation>(){
                        new(){ Name = "Technology and electronics", LanguageId=LanguageId.English },
                        new(){ Name = "Техніка та електроніка", LanguageId=LanguageId.Ukrainian } } },
/*5*/           new(){ UrlSlug = "goods-for-children", Image = "GoodsForChildren.png", ParentId = null,
                     CategoryTranslations=new List<CategoryTranslation>(){
                        new(){ Name = "Goods for children", LanguageId=LanguageId.English },
                        new(){ Name = "Товари для дітей", LanguageId=LanguageId.Ukrainian } } },
/*6*/           new(){ UrlSlug = "auto", Image = "Auto.png", ParentId = null,
                     CategoryTranslations=new List<CategoryTranslation>(){
                        new(){ Name = "Auto", LanguageId=LanguageId.English },
                        new(){ Name = "Авто", LanguageId=LanguageId.Ukrainian } } },
/*7*/           new(){ UrlSlug = "gifts-hobbies-books", Image = "GiftsHobbiesBooks.png", ParentId = null,
                     CategoryTranslations=new List<CategoryTranslation>(){
                        new(){ Name = "Gifts, hobbies, books", LanguageId=LanguageId.English },
                        new(){ Name = "Подарунки, хобі, книги", LanguageId=LanguageId.Ukrainian } } },
/*8*/           new(){ UrlSlug = "accessories-and-decorations", Image = "AccessoriesAndDecorations.png", ParentId = null,
                    CategoryTranslations=new List<CategoryTranslation>(){
                        new(){ Name = "Accessories and decorations", LanguageId=LanguageId.English },
                        new(){ Name = "Аксесуари та прикраси", LanguageId=LanguageId.Ukrainian } } },
/*9*/           new(){ UrlSlug = "materials-for-repair", Image = "", ParentId = null,
                    CategoryTranslations=new List<CategoryTranslation>(){
                        new(){ Name = "Materials for repair", LanguageId=LanguageId.English },
                        new(){ Name = "Матеріали для ремонту", LanguageId=LanguageId.Ukrainian } } },
/*10*/          new(){ UrlSlug = "sports-and-recreation", Image = "SportsAndRecreation.png", ParentId = null,
                    CategoryTranslations=new List<CategoryTranslation>(){
                        new(){ Name = "Sports and recreation", LanguageId=LanguageId.English },
                        new(){ Name = "Спорт і відпочинок", LanguageId=LanguageId.Ukrainian } } },
/*11*/          new(){ UrlSlug = "medicines-and-medical-products", Image = "", ParentId = null,
                    CategoryTranslations=new List<CategoryTranslation>(){
                        new(){ Name = "Medicines and medical products", LanguageId=LanguageId.English },
                        new(){ Name = "Медикаменти та медичні товари", LanguageId=LanguageId.Ukrainian } } },
/*12*/          new(){ UrlSlug = "pets-and-pet-products", Image = "", ParentId = null,
                    CategoryTranslations=new List<CategoryTranslation>(){
                        new(){ Name = "Pets and pet products", LanguageId=LanguageId.English },
                        new(){ Name = "Домашні тварини та зоотовари", LanguageId=LanguageId.Ukrainian } } },
/*13*/          new(){ UrlSlug = "stationery", Image = "Stationery.png", ParentId = null,
                    CategoryTranslations=new List<CategoryTranslation>(){
                        new(){ Name = "Stationery",  LanguageId=LanguageId.English },
                        new(){ Name = "Канцтовари", LanguageId=LanguageId.Ukrainian } } },
/*14*/          new(){ UrlSlug = "overalls-and-shoes", Image = "", ParentId = null,
                    CategoryTranslations=new List<CategoryTranslation>(){
                        new(){ Name = "Overalls and shoes", LanguageId=LanguageId.English },
                        new(){ Name = "Спецодяг та взуття", LanguageId=LanguageId.Ukrainian } } },
/*15*/          new(){ UrlSlug = "wedding-goods", Image = "", ParentId = null,
                    CategoryTranslations=new List<CategoryTranslation>(){
                        new(){ Name = "Wedding goods", LanguageId=LanguageId.English },
                        new(){ Name = "Весільні товари", LanguageId=LanguageId.Ukrainian } } },
/*16*/          new(){ UrlSlug = "food-products-drinks", Image = "", ParentId = null,
                    CategoryTranslations=new List<CategoryTranslation>(){
                        new(){ Name = "Food products, drinks", LanguageId=LanguageId.English },
                        new(){ Name = "Продукти харчування, напої", LanguageId=LanguageId.Ukrainian } } },
/*17*/          new(){ UrlSlug = "tools", Image = "Tools.png", ParentId = null,
                    CategoryTranslations=new List<CategoryTranslation>(){
                        new(){ Name = "Tools", LanguageId=LanguageId.English },
                        new(){ Name = "Інструменти", LanguageId=LanguageId.Ukrainian } } },
/*18*/          new(){ UrlSlug = "antiques-and-collectibles", Image = "", ParentId = null,
                    CategoryTranslations=new List<CategoryTranslation>(){
                        new(){ Name = "Antiques and collectibles", LanguageId=LanguageId.English },
                        new(){ Name = "Антикваріат і колекціонування", LanguageId=LanguageId.Ukrainian } } },
/*19*/          new(){ UrlSlug = "сonstruction", Image = "Construction.png", ParentId = null,
                    CategoryTranslations=new List<CategoryTranslation>(){
                        new(){ Name = "Construction", LanguageId=LanguageId.English },
                        new(){ Name = "Будівництво", LanguageId=LanguageId.Ukrainian } } },

/*20*/          new(){ UrlSlug = "mens-clothing", Image = "", ParentId = 3,
                    CategoryTranslations=new List<CategoryTranslation>(){
                        new(){ Name = "Men's clothing", LanguageId=LanguageId.English },
                        new(){ Name = "Чоловічий одяг", LanguageId=LanguageId.Ukrainian } } },
/*21*/          new(){ UrlSlug = "womens-clothes", Image = "", ParentId = 3,
                    CategoryTranslations=new List<CategoryTranslation>(){
                        new(){ Name = "Women's clothes", LanguageId=LanguageId.English },
                        new(){ Name = "Жіночий одяг", LanguageId=LanguageId.Ukrainian } } },
/*22*/          new(){ UrlSlug = "Childrens-clothes-shoes-accessories", Image = "", ParentId = 3,
                    CategoryTranslations=new List<CategoryTranslation>(){
                        new(){ Name = "Children's clothes, shoes, accessories", LanguageId=LanguageId.English },
                        new(){ Name = "Дитячі одяг, взуття, аксесуари", LanguageId=LanguageId.Ukrainian } } },
/*23*/          new(){ UrlSlug = "sportswear-and-footwear", Image = "", ParentId = 3,
                    CategoryTranslations=new List<CategoryTranslation>(){
                        new(){ Name = "Sportswear and footwear", LanguageId=LanguageId.English },
                        new(){ Name = "Спортивний одяг та взуття", LanguageId=LanguageId.Ukrainian } } },
/*24*/          new(){ UrlSlug = "footwear", Image = "", ParentId = 3,
                    CategoryTranslations=new List<CategoryTranslation>(){
                        new(){ Name = "Footwear", LanguageId=LanguageId.English },
                        new(){ Name = "Взуття", LanguageId=LanguageId.Ukrainian } } },
/*25*/          new(){ UrlSlug = "overalls-and-shoes", Image = "", ParentId = 3,
                    CategoryTranslations=new List<CategoryTranslation>(){
                        new(){ Name = "Overalls and shoes", LanguageId=LanguageId.English },
                        new(){ Name = "Спецодяг та взуття", LanguageId=LanguageId.Ukrainian } } },
/*26*/          new(){ UrlSlug = "carnival-costumes", Image = "", ParentId = 3,
                    CategoryTranslations=new List<CategoryTranslation>(){
                        new(){ Name = "Carnival costumes", LanguageId=LanguageId.English },
                        new(){ Name = "Карнавальні костюми", LanguageId=LanguageId.Ukrainian } } },
/*27*/          new(){ UrlSlug = "ethnic-clothing", Image = "", ParentId = 3,
                    CategoryTranslations=new List<CategoryTranslation>(){
                        new(){ Name = "Ethnic clothing", LanguageId=LanguageId.English },
                        new(){ Name = "Етнічний одяг", LanguageId=LanguageId.Ukrainian } } },

/*28*/          new(){ UrlSlug = "computer-equipment-and-software", Image = "", ParentId = 4,
                    CategoryTranslations=new List<CategoryTranslation>(){
                        new(){ Name = "Computer equipment and software", LanguageId=LanguageId.English },
                        new(){ Name = "Комп'ютерна техніка та ПЗ", LanguageId=LanguageId.Ukrainian } } },
/*29*/          new(){ UrlSlug = "household-appliances", Image = "", ParentId = 4,
                    CategoryTranslations=new List<CategoryTranslation>(){
                        new(){ Name = "Household appliances", LanguageId=LanguageId.English },
                        new(){ Name = "Побутова техніка", LanguageId=LanguageId.Ukrainian } } },
/*30*/          new(){ UrlSlug = "phones-and-accessories", Image = "", ParentId = 4,
                    CategoryTranslations=new List<CategoryTranslation>(){
                        new(){ Name = "Phones and accessories", LanguageId=LanguageId.English },
                        new(){ Name = "Телефони та аксесуари", LanguageId=LanguageId.Ukrainian } } },
/*31*/          new(){ UrlSlug = "audio-equipment-and-accessories", Image = "", ParentId = 4,
                    CategoryTranslations=new List<CategoryTranslation>(){
                        new(){ Name = "Audio equipment and accessories", LanguageId=LanguageId.English },
                        new(){ Name = "Аудіотехніка і аксесуари", LanguageId=LanguageId.Ukrainian } } },
/*32*/          new(){ UrlSlug = "spare-parts-for-machinery-and-electronics", Image = "", ParentId = 4,
                    CategoryTranslations=new List<CategoryTranslation>(){
                        new(){ Name = "Spare parts for machinery and electronics", LanguageId=LanguageId.English},
                        new(){ Name = "Запчастини для техніки та електроніки", LanguageId=LanguageId.Ukrainian } } },
/*33*/          new(){ UrlSlug = "tv-and-video-equipment", Image = "", ParentId = 4,
                    CategoryTranslations=new List<CategoryTranslation>(){
                        new(){  Name = "TV and video equipment", LanguageId=LanguageId.English },
                        new(){ Name = "TV та відеотехніка", LanguageId=LanguageId.Ukrainian } } },
/*34*/          new(){ UrlSlug = "car-electronics", Image = "", ParentId = 4,
                    CategoryTranslations=new List<CategoryTranslation>(){
                        new(){ Name = "Car electronics", LanguageId=LanguageId.English },
                        new(){ Name = "Автомобільна електроніка", LanguageId=LanguageId.Ukrainian } } },
/*35*/          new(){ UrlSlug = "photos-camcorders-and-accessories", Image = "", ParentId = 4,
                    CategoryTranslations=new List<CategoryTranslation>(){
                        new(){ Name = "Photos, camcorders and accessories", LanguageId=LanguageId.English },
                        new(){ Name = "Фото, відеокамери та аксесуари", LanguageId=LanguageId.Ukrainian } } },
/*36*/          new(){ UrlSlug = "3d-devices", Image = "", ParentId = 4,
                    CategoryTranslations=new List<CategoryTranslation>(){
                        new(){Name = "3d devices", LanguageId=LanguageId.English },
                        new(){ Name = "3d пристрої", LanguageId=LanguageId.Ukrainian } } },
/*37*/          new(){ UrlSlug = "equipment-for-satellite-internet", Image = "", ParentId = 4,
                    CategoryTranslations=new List<CategoryTranslation>(){
                        new(){Name = "Equipment for satellite internet",  LanguageId=LanguageId.English },
                        new(){ Name = "Обладнання для супутникового інтернету", LanguageId=LanguageId.Ukrainian } } },

/*38*/          new(){ UrlSlug = "tablet-computers", Image = "", ParentId = 28,
                    CategoryTranslations=new List<CategoryTranslation>(){
                        new(){ Name = "Tablet computers", LanguageId=LanguageId.English },
                        new(){ Name = "Планшетні комп'ютери", LanguageId=LanguageId.Ukrainian } } },
/*39*/          new(){ UrlSlug = "laptops", Image = "", ParentId = 28,
                    CategoryTranslations=new List<CategoryTranslation>(){
                        new(){  Name = "Laptops", LanguageId=LanguageId.English },
                        new(){ Name = "Ноутбуки", LanguageId=LanguageId.Ukrainian } } },
/*40*/          new(){ UrlSlug = "monitors", Image = "", ParentId = 28,
                    CategoryTranslations=new List<CategoryTranslation>(){
                        new(){ Name = "Monitors", LanguageId=LanguageId.English },
                        new(){ Name = "Монітори", LanguageId=LanguageId.Ukrainian } } },
/*41*/          new(){ UrlSlug = "components-for-computer-equipment", Image = "", ParentId = 28,
                    CategoryTranslations=new List<CategoryTranslation>(){
                        new(){ Name = "Components for computer equipment",LanguageId=LanguageId.English },
                        new(){ Name = "Комплектуючі для комп'ютерної техніки", LanguageId=LanguageId.Ukrainian } } },
/*42*/          new(){ UrlSlug = "computer-accessories", Image = "", ParentId = 28,
                    CategoryTranslations=new List<CategoryTranslation>(){
                        new(){ Name = "Computer accessories", LanguageId=LanguageId.English},
                        new(){ Name = "Комп'ютерні аксесуари", LanguageId=LanguageId.Ukrainian } } },
/*43*/          new(){ UrlSlug = "smart-watches-and-fitness-bracelets", Image = "", ParentId = 28,
                    CategoryTranslations=new List<CategoryTranslation>(){
                        new(){ Name = "Smart watches and fitness bracelets", LanguageId=LanguageId.English },
                        new(){ Name = "Розумні годинники та фітнес браслети", LanguageId=LanguageId.Ukrainian } } },
/*44*/          new(){ UrlSlug = "printers-scanners-mfps-and-components", Image = "", ParentId = 28,
                    CategoryTranslations=new List<CategoryTranslation>(){
                        new(){ Name = "Printers, scanners, MFPs and components", LanguageId=LanguageId.English },
                        new(){ Name = "Принтери, сканери, МФУ та комплектуючі", LanguageId=LanguageId.Ukrainian } } },
/*45*/          new(){ UrlSlug = "information-carriers", Image = "", ParentId = 28,
                    CategoryTranslations=new List<CategoryTranslation>(){
                        new(){ Name = "Information carriers", LanguageId=LanguageId.English },
                        new(){ Name = "Носії інформації", LanguageId=LanguageId.Ukrainian } } },
/*46*/          new(){ UrlSlug = "game-consoles-and-components", Image = "", ParentId = 28,
                    CategoryTranslations=new List<CategoryTranslation>(){
                        new(){ Name = "Game consoles and components", LanguageId=LanguageId.English },
                        new(){ Name = "Ігрові приставки та компоненти", LanguageId=LanguageId.Ukrainian } } },
/*47*/          new(){ UrlSlug = "desktops", Image = "", ParentId = 28,
                    CategoryTranslations=new List<CategoryTranslation>(){
                        new(){ Name = "Desktops", LanguageId=LanguageId.English },
                        new(){ Name = "Настільні комп'ютери", LanguageId=LanguageId.Ukrainian } } },
/*48*/          new(){ UrlSlug = "software", Image = "", ParentId = 28,
                    CategoryTranslations=new List<CategoryTranslation>(){
                        new(){ Name = "Software", LanguageId=LanguageId.English },
                        new(){ Name = "Програмне забезпечення", LanguageId=LanguageId.Ukrainian } } },
/*49*/          new(){ UrlSlug = "server-equipment", Image = "", ParentId = 28,
                    CategoryTranslations=new List<CategoryTranslation>(){
                        new(){ Name = "Server equipment", LanguageId=LanguageId.English },
                        new(){ Name = "Серверне обладнання", LanguageId=LanguageId.Ukrainian } } },
/*50*/          new(){ UrlSlug = "mining-equipment", Image = "", ParentId = 28,
                    CategoryTranslations=new List<CategoryTranslation>(){
                        new(){ Name = "Mining equipment", LanguageId=LanguageId.English },
                        new(){ Name = "Обладнання для майнінгу", LanguageId=LanguageId.Ukrainian } } },
/*51*/          new(){ UrlSlug = "e-books", Image = "", ParentId = 28,
                    CategoryTranslations=new List<CategoryTranslation>(){
                        new(){ Name = "E-books",  LanguageId=LanguageId.English },
                        new(){ Name = "Електронні книги (пристрій)", LanguageId=LanguageId.Ukrainian } } },
/*52*/          new(){ UrlSlug = "single-board-computers-and-nettops", Image = "", ParentId = 28,
                    CategoryTranslations=new List<CategoryTranslation>(){
                        new(){ Name = "Single board computers and nettops", LanguageId=LanguageId.English },
                        new(){ Name = "Одноплатні комп'ютери та неттопи", LanguageId=LanguageId.Ukrainian } } },
/*53*/          new(){ UrlSlug = "portable-electronic-translators", Image = "", ParentId = 28,
                    CategoryTranslations=new List<CategoryTranslation>(){
                        new(){ Name = "Portable electronic translators", LanguageId=LanguageId.English },
                        new(){ Name = "Портативні електронні перекладачі", LanguageId=LanguageId.Ukrainian } } },

                new(){ UrlSlug = "cables-for-electronics", Image = "", ParentId = 41,
                    CategoryTranslations=new List<CategoryTranslation>(){
                        new(){ Name = "Cables for electronics", LanguageId=LanguageId.English },
                        new(){ Name = "Кабелі для електроніки", LanguageId=LanguageId.Ukrainian } } },
                new(){ UrlSlug = "hdd-ssd", Image = "", ParentId = 41,
                    CategoryTranslations=new List<CategoryTranslation>(){
                        new(){ Name = "HDD, SSD", LanguageId=LanguageId.English },
                        new(){ Name = "Внутрішні та зовнішні жорсткі диски, HDD, SSD", LanguageId=LanguageId.Ukrainian } } },
                new(){ UrlSlug = "batteries-for-laptops-tablets-e-books-translators", Image = "", ParentId = 41,
                    CategoryTranslations=new List<CategoryTranslation>(){
                        new(){ Name = "Batteries for laptops, tablets, e-books, translators", LanguageId=LanguageId.English },
                        new(){ Name = "Акумулятори для ноутбуків, планшетів, електронних книг, перекладачів", LanguageId=LanguageId.Ukrainian } } },
                new(){ UrlSlug = "laptop-chargers", Image = "", ParentId = 41,
                    CategoryTranslations=new List<CategoryTranslation>(){
                        new(){ Name = "Laptop chargers", LanguageId=LanguageId.English },
                        new(){ Name = "Зарядні пристрої для ноутбуків", LanguageId=LanguageId.Ukrainian } } },
                new(){ UrlSlug = "laptop-body-parts", Image = "", ParentId = 41,
                    CategoryTranslations=new List<CategoryTranslation>(){
                        new(){ Name = "Laptop body parts", LanguageId=LanguageId.English },
                        new(){ Name = "Частини корпусу ноутбука", LanguageId=LanguageId.Ukrainian } } },
                new(){ UrlSlug = "memory-modules", Image = "", ParentId = 41,
                    CategoryTranslations=new List<CategoryTranslation>(){
                        new(){ Name = "Memory modules", LanguageId=LanguageId.English },
                        new(){ Name = "Модулі пам'яті", LanguageId=LanguageId.Ukrainian } } },
                new(){ UrlSlug = "processors", Image = "", ParentId = 41,
                    CategoryTranslations=new List<CategoryTranslation>(){
                        new(){ Name = "Processors", LanguageId=LanguageId.English },
                        new(){ Name = "Процесори", LanguageId=LanguageId.Ukrainian } } },
                new(){ UrlSlug = "coolers-and-cooling-systems", Image = "", ParentId = 41,
                    CategoryTranslations=new List<CategoryTranslation>(){
                        new(){ Name = "Coolers and cooling systems", LanguageId=LanguageId.English },
                        new(){ Name = "Кулери і системи охолодження", LanguageId=LanguageId.Ukrainian } } },
                new(){ UrlSlug = "matrixes-for-laptops-tablets-and-monitors", Image = "", ParentId = 41,
                    CategoryTranslations=new List<CategoryTranslation>(){
                        new(){ Name = "Matrixes for laptops, tablets and monitors", LanguageId=LanguageId.English },
                        new(){ Name = "Матриці для ноутбуків, планшетів і моніторів", LanguageId=LanguageId.Ukrainian } } },
                new(){ UrlSlug = "cables-and-connectors-for-laptops-computers-tablets", Image = "", ParentId = 41,
                    CategoryTranslations=new List<CategoryTranslation>(){
                        new(){ Name = "Cables and connectors for laptops, computers, tablets", LanguageId=LanguageId.English },
                        new(){ Name = "Шлейфи та роз'єми для ноутбуків, комп'ютерів, планшетів", LanguageId=LanguageId.Ukrainian } } },
                new(){ UrlSlug = "keyboard-blocks-for-laptops", Image = "", ParentId = 41,
                    CategoryTranslations=new List<CategoryTranslation>(){
                        new(){ Name = "Keyboard blocks for laptops", LanguageId=LanguageId.English },
                        new(){ Name = "Клавіатурні блоки для ноутбуків", LanguageId=LanguageId.Ukrainian } } },
                new(){ UrlSlug = "touchscreen-for-displays", Image = "", ParentId = 41,
                    CategoryTranslations=new List<CategoryTranslation>(){
                        new(){ Name = "Touchscreen for displays", LanguageId=LanguageId.English },
                        new(){ Name = "Touchscreen для дисплеїв", LanguageId=LanguageId.Ukrainian } } },
                new(){ UrlSlug = "microcircuits", Image = "", ParentId = 41,
                    CategoryTranslations=new List<CategoryTranslation>(){
                        new(){ Name = "Microcircuits", LanguageId=LanguageId.English },
                        new(){ Name = "Мікросхеми", LanguageId=LanguageId.Ukrainian } } },
                new(){ UrlSlug = "spare-parts-for-tvs-and-monitors", Image = "", ParentId = 41,
                    CategoryTranslations=new List<CategoryTranslation>(){
                        new(){ Name = "Spare parts for TVs and monitors", LanguageId=LanguageId.English },
                        new(){ Name = "Запчастини для телевізорів і моніторів", LanguageId=LanguageId.Ukrainian } } },
                new(){ UrlSlug = "motherboards", Image = "", ParentId = 41,
                    CategoryTranslations=new List<CategoryTranslation>(){
                        new(){ Name = "Motherboards", LanguageId=LanguageId.English },
                        new(){ Name = "Материнські плати", LanguageId=LanguageId.Ukrainian } } },
                new(){ UrlSlug = "video-cards", Image = "", ParentId = 41,
                    CategoryTranslations=new List<CategoryTranslation>(){
                        new(){ Name = "Video cards", LanguageId=LanguageId.English},
                        new(){ Name = "Відеокарти", LanguageId=LanguageId.Ukrainian } } },
                new(){ UrlSlug = "enclosures-for-computers", Image = "", ParentId = 41,
                    CategoryTranslations=new List<CategoryTranslation>(){
                        new(){ Name = "Enclosures for computers", LanguageId=LanguageId.English },
                        new(){ Name = "Корпуси для комп'ютерів", LanguageId=LanguageId.Ukrainian } } },
                new(){UrlSlug = "power-supplies-for-computers", Image = "", ParentId = 41,
                    CategoryTranslations=new List<CategoryTranslation>(){
                        new(){ Name = "Power supplies for computers", LanguageId=LanguageId.English },
                        new(){ Name = "Блоки живлення для комп'ютерів", LanguageId=LanguageId.Ukrainian } } },
                new(){ UrlSlug = "patch-cord", Image = "", ParentId = 41,
                    CategoryTranslations=new List<CategoryTranslation>(){
                        new(){ Name = "Patch cord", LanguageId=LanguageId.English },
                        new(){ Name = "Патч-корди", LanguageId=LanguageId.Ukrainian } } },
                new(){ UrlSlug = "pockets-for-hard-drives", Image = "", ParentId = 41,
                    CategoryTranslations=new List<CategoryTranslation>(){
                        new(){ Name = "Pockets for hard drives", LanguageId=LanguageId.English },
                        new(){ Name = "Кишені для жорстких дисків", LanguageId=LanguageId.Ukrainian } } },
                new(){ UrlSlug = "adapters-and-port-expansion-cards", Image = "", ParentId = 41,
                    CategoryTranslations=new List<CategoryTranslation>(){
                        new(){ Name = "Adapters and port expansion cards", LanguageId=LanguageId.English },
                        new(){ Name = "Адаптери і плати розширення портів", LanguageId=LanguageId.Ukrainian } } },
                new(){ UrlSlug = "audio-parts-for-laptops", Image = "", ParentId = 41,
                    CategoryTranslations=new List<CategoryTranslation>(){
                        new(){ Name = "Audio parts for laptops", LanguageId=LanguageId.English },
                        new(){ Name = "Звукові запчастини для портативних ПК", LanguageId=LanguageId.Ukrainian } } },
                new(){ UrlSlug = "thermal-paste", Image = "", ParentId = 41,
                    CategoryTranslations=new List<CategoryTranslation>(){
                        new(){ Name = "Thermal paste", LanguageId=LanguageId.English },
                        new(){ Name = "Термопаста", LanguageId=LanguageId.Ukrainian } } },
                new(){ UrlSlug = "sound-cards", Image = "", ParentId = 41,
                    CategoryTranslations=new List<CategoryTranslation>(){
                        new(){ Name = "Sound cards", LanguageId=LanguageId.English },
                        new(){ Name = "Звукові карти", LanguageId=LanguageId.Ukrainian } } },
                new(){ UrlSlug = "network-cards", Image = "", ParentId = 41,
                    CategoryTranslations=new List<CategoryTranslation>(){
                        new(){ Name = "Network cards", LanguageId=LanguageId.English },
                        new(){ Name = "Мережеві карти", LanguageId=LanguageId.Ukrainian } } },
                new(){ UrlSlug = "optical-drives", Image = "", ParentId = 41,
                    CategoryTranslations=new List<CategoryTranslation>(){
                        new(){ Name = "Optical drives", LanguageId=LanguageId.English },
                        new(){ Name = "Оптичні приводи", LanguageId=LanguageId.Ukrainian } } },
                new(){ UrlSlug = "cases-for-tablets", Image = "", ParentId = 41,
                    CategoryTranslations=new List<CategoryTranslation>(){
                        new(){ Name = "Cases for tablets", LanguageId=LanguageId.English },
                        new(){ Name = "Корпуси для планшетів", LanguageId=LanguageId.Ukrainian } } },
                new(){ UrlSlug = "accessories-for-matrices-and-displays", Image = "", ParentId = 41,
                    CategoryTranslations=new List<CategoryTranslation>(){
                        new(){ Name = "Accessories for matrices and displays", LanguageId=LanguageId.English },
                        new(){ Name = "Комплектуючі для матриць та дисплеїв", LanguageId=LanguageId.Ukrainian } } },
                new(){ UrlSlug = "cameras-for-laptops", Image = "", ParentId = 41,
                    CategoryTranslations=new List<CategoryTranslation>(){
                        new(){ Name = "Cameras for laptops", LanguageId=LanguageId.English },
                        new(){ Name = "Камери для портативних ПК", LanguageId=LanguageId.Ukrainian } } },
                new(){ UrlSlug = "cooling-systems-for-laptops", Image = "", ParentId = 41,
                    CategoryTranslations=new List<CategoryTranslation>(){
                        new(){ Name = "Cooling systems for laptops", LanguageId=LanguageId.English },
                        new(){ Name = "Системи охолодження для ноутбуків", LanguageId=LanguageId.Ukrainian } } },
                new(){ UrlSlug = "tv-and-fm-tuners", Image = "", ParentId = 41,
                    CategoryTranslations=new List<CategoryTranslation>(){
                        new(){ Name = "TV and FM tuners", LanguageId=LanguageId.English },
                        new(){ Name = "TV-тюнери і FM-тюнери", LanguageId=LanguageId.Ukrainian } } },
                new(){ UrlSlug = "postcards", Image = "", ParentId = 41,
                    CategoryTranslations=new List<CategoryTranslation>(){
                        new(){ Name = "Postcards", LanguageId=LanguageId.English },
                        new(){ Name = "Post-карти", LanguageId=LanguageId.Ukrainian } } },
                new(){ UrlSlug = "accessories-for-routers", Image = "", ParentId = 41,
                    CategoryTranslations=new List<CategoryTranslation>(){
                        new(){ Name = "Accessories for routers", LanguageId=LanguageId.English },
                        new(){ Name = "Комплектуючі для маршрутизаторів", LanguageId=LanguageId.Ukrainian } } },
            };
            return categories;
        }

        static IEnumerable<Shop> GetPreconfiguredMarketplaceShops(string userId)
        {
            var shops = new List<Shop>
            {
                new(){ Name = "Mall", Description="", Photo="", Email="dg646726@gmail.com",
                       SiteUrl="https://http://mall.novakvova.com/", CityId=1, UserId=userId},
            };
            return shops;
        }

        static IEnumerable<ProductStatus> GetPreconfiguredMarketplaceProductStatus()
        {
            var productStatuses = new List<ProductStatus>
            {
                  new(){ ProductStatusTranslations=new List<ProductStatusTranslation>(){
                        new() {LanguageId=LanguageId.English, Name = "In stock" },
                        new (){LanguageId=LanguageId.Ukrainian, Name="В наявності"} } },
            };
            return productStatuses;
        }

        static IEnumerable<Product> GetPreconfiguredMarketplaceProducts()
        {
            var products = new List<Product>
            {
                new(){ Name = "Nike Festive dress Hex Wednesday Dress",Description="",Price=1000f,Count=100,ShopId=1,StatusId=1,CategoryId=21,UrlSlug=Guid.NewGuid() },
                new(){ Name = "Puma Festive dress Hex Wednesday Dress",Description="",Price=1000f,Count=100,ShopId=1,StatusId=1,CategoryId=21,UrlSlug=Guid.NewGuid() },
                new(){ Name = "Zara Festive dress Hex Wednesday Dress",Description="",Price=1000f,Count=100,ShopId=1,StatusId=1,CategoryId=21,UrlSlug=Guid.NewGuid() },
                new(){ Name = "H&M Festive dress Hex Wednesday Dress",Description="",Price=1000f,Count=100,ShopId=1,StatusId=1,CategoryId=21,UrlSlug=Guid.NewGuid() },
                new(){ Name = "AAA Festive dress Hex Wednesday Dress",Description="",Price=1000f,Count=100,ShopId=1,StatusId=1,CategoryId=21,UrlSlug=Guid.NewGuid() },
                new(){ Name = "Nike Festive dress Hex Wednesday Dress",Description="",Price=1000f,Count=100,ShopId=1,StatusId=1,CategoryId=21,UrlSlug=Guid.NewGuid() },
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
                new(){ Name = "BlueDress.jpg",ProductId=2 },
                new(){ Name = "BlueDress2.jpg",ProductId=2 },
                new(){ Name = "GreenDress.jpg",ProductId=3 },
                new(){ Name = "RedDress.jpg",ProductId=4 },
                new(){ Name = "YellowDress.jpg",ProductId=5 },
                new(){ Name = "YellowDress2.jpg",ProductId=5 },
                new(){ Name = "YellowDress3.jpg",ProductId=5 },

                new(){ Name = "BlueDress.jpg",ProductId=6 },
                new(){ Name = "BlueDress2.jpg",ProductId=6 },
                new(){ Name = "YellowDress.jpg",ProductId=7 },
                new(){ Name = "YellowDress2.jpg",ProductId=7 },
                new(){ Name = "YellowDress3.jpg",ProductId=7 },
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
    }
}
