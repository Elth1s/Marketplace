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
                    new(){ Name= "Afghanistan", Code= "AF" },
                    new(){ Name= "Alland Islands", Code= "AX"},
                    new(){ Name= "Albania", Code= "AL" },
                    new(){ Name= "Algeria", Code= "DZ"},
                    new(){ Name= "American Samoa", Code= "AS"},
                    new(){ Name= "Andorra", Code= "AD"},
                    new(){ Name= "Angola", Code= "AO"},
                    new(){ Name= "Anguilla", Code= "AI"},
                    new(){ Name= "Antarctica", Code= "AQ"},
                    new(){ Name= "Antigua and Barbuda", Code= "AG"},
                    new(){ Name= "Argentina", Code= "AR"},
                    new(){ Name= "Armenia", Code= "AM"},
                    new(){ Name= "Aruba", Code= "AW" },
                    new(){ Name= "Australia", Code= "AU"},
                    new(){ Name= "Austria", Code= "AT"},
                    new(){ Name= "Azerbaijan", Code= "AZ"},
                    new(){ Name= "Bahamas", Code= "BS"},
                    new(){ Name= "Bahrain", Code= "BH"},
                    new(){ Name= "Bangladesh", Code= "BD"},
                    new(){ Name= "Barbados", Code= "BB"},
                    new(){ Name= "Belarus", Code= "BY"},
                    new(){ Name= "Belgium", Code= "BE"},
                    new(){ Name= "Belize", Code= "BZ"},
                    new(){ Name= "Benin", Code= "BJ"},
                    new(){ Name= "Bermuda", Code= "BM"},
                    new(){ Name= "Bhutan", Code= "BT"},
                    new(){ Name= "Bolivia", Code= "BO"},
                    new(){ Name= "Bosnia and Herzegovina", Code= "BA"},
                    new(){ Name= "Botswana", Code= "BW"},
                    new(){ Name= "Bouvet Island", Code= "BV"},
                    new(){ Name= "Brazil", Code= "BR"},
                    new(){ Name= "British Indian Ocean Territory", Code= "IO"},
                    new(){ Name= "British Virgin Islands", Code= "VG"},
                    new(){ Name= "Brunei Darussalam", Code= "BN"},
                    new(){ Name= "Bulgaria", Code= "BG"},
                    new(){ Name= "Burkina Faso", Code= "BF"},
                    new(){ Name= "Burundi", Code= "BI"},
                    new(){ Name= "Cambodia", Code= "KH"},
                    new(){ Name= "Cameroon", Code= "CM"},
                    new(){ Name= "Canada", Code= "CA"},
                    new(){ Name= "Cape Verde", Code= "CV"},
                    new(){ Name= "Cayman Islands", Code= "KY"},
                    new(){ Name= "Central African Republic", Code= "CF"},
                    new(){ Name= "Chad", Code= "TD"},
                    new(){ Name= "Chile", Code= "CL"},
                    new(){ Name= "China", Code= "CN"},
                    new(){ Name= "Christmas Island", Code= "CX"},
                    new(){ Name= "Cocos (Keeling) Islands", Code= "CC"},
                    new(){ Name= "Colombia", Code= "CO"},
                    new(){ Name= "Comoros", Code= "KM"},
                    new(){ Name= "Congo, Democratic Republic of the", Code= "CG"},
                    new(){ Name= "Congo, Republic of the", Code= "CD"},
                    new(){ Name= "Cook Islands", Code= "CK"},
                    new(){ Name= "Costa Rica", Code= "CR"},
                    new(){ Name= "Cote d'Ivoire", Code= "CI"},
                    new(){ Name= "Croatia", Code= "HR"},
                    new(){ Name= "Cuba", Code= "CU"},
                    new(){ Name= "Curacao", Code= "CW"},
                    new(){ Name= "Cyprus", Code= "CY"},
                    new(){ Name= "Czech Republic", Code= "CZ"},
                    new(){ Name= "Denmark", Code= "DK"},
                    new(){ Name= "Djibouti", Code= "DJ"},
                    new(){ Name= "Dominica", Code= "DM"},
                    new(){ Name= "Dominican Republic", Code= "DO"},
                    new(){ Name= "Ecuador", Code= "EC"},
                    new(){ Name= "Egypt", Code= "EG"},
                    new(){ Name= "El Salvador", Code= "SV"},
                    new(){ Name= "Equatorial Guinea", Code= "GQ"},
                    new(){ Name= "Eritrea", Code= "ER"},
                    new(){ Name= "Estonia", Code= "EE"},
                    new(){ Name= "Ethiopia", Code= "ET"},
                    new(){ Name= "Falkland Islands (Malvinas)", Code= "FK"},
                    new(){ Name= "Faroe Islands", Code= "FO"},
                    new(){ Name= "Fiji", Code= "FJ"},
                    new(){ Name= "Finland", Code= "FI"},
                    new(){ Name= "France", Code= "FR"},
                    new(){ Name= "French Guiana", Code= "GF"},
                    new(){ Name= "French Polynesia", Code= "PF"},
                    new(){ Name= "French Southern Territories", Code= "TF"},
                    new(){ Name= "Gabon", Code= "GA"},
                    new(){ Name= "Gambia", Code= "GM"},
                    new(){ Name= "Georgia", Code= "GE"},
                    new(){ Name= "Germany", Code= "DE"},
                    new(){ Name= "Ghana", Code= "GH"},
                    new(){ Name= "Gibraltar", Code= "GI"},
                    new(){ Name= "Greece", Code= "GR"},
                    new(){ Name= "Greenland", Code= "GL"},
                    new(){ Name= "Grenada", Code= "GD"},
                    new(){ Name= "Guadeloupe", Code= "GP"},
                    new(){ Name= "Guam", Code= "GU"},
                    new(){ Name= "Guatemala", Code= "GT"},
                    new(){ Name= "Guernsey", Code= "GG"},
                    new(){ Name= "Guinea-Bissau", Code= "GW"},
                    new(){ Name= "Guinea", Code= "GN"},
                    new(){ Name= "Guyana", Code= "GY"},
                    new(){ Name= "Haiti", Code= "HT"},
                    new(){ Name= "Heard Island and McDonald Islands", Code= "HM"},
                    new(){ Name= "Holy See (Vatican City State)",Code= "VA"},
                    new(){ Name= "Honduras", Code= "HN"},
                    new(){ Name= "Hong Kong", Code= "HK"},
                    new(){ Name= "Hungary", Code= "HU"},
                    new(){ Name= "Iceland", Code= "IS"},
                    new(){ Name= "India", Code= "IN"},
                    new(){ Name= "Indonesia", Code= "ID"},
                    new(){ Name= "Iran, Islamic Republic of", Code= "IR"},
                    new(){ Name= "Iraq", Code= "IQ"},
                    new(){ Name= "Ireland", Code= "IE"},
                    new(){ Name= "Isle of Man", Code= "IM"},
                    new(){ Name= "Israel", Code= "IL"},
                    new(){ Name= "Italy", Code= "IT"},
                    new(){ Name= "Jamaica", Code= "JM"},
                    new(){ Name= "Japan", Code= "JP"},
                    new(){ Name= "Jersey", Code= "JE"},
                    new(){ Name= "Jordan", Code= "JO"},
                    new(){ Name= "Kazakhstan", Code= "KZ"},
                    new(){ Name= "Kenya", Code= "KE"},
                    new(){ Name= "Kiribati", Code= "KI"},
                    new(){ Name= "Korea, Democratic People's Republic of",Code= "KP"},
                    new(){ Name= "Korea, Republic of", Code= "KR"},
                    new(){ Name= "Kosovo", Code= "XK"},
                    new(){ Name= "Kuwait", Code= "KW"},
                    new(){ Name= "Kyrgyzstan", Code= "KG"},
                    new(){ Name= "Lao People's Democratic Republic", Code= "LA"},
                    new(){ Name= "Latvia", Code= "LV"},
                    new(){ Name= "Lebanon", Code= "LB"},
                    new(){ Name= "Lesotho", Code= "LS"},
                    new(){ Name= "Liberia", Code= "LR"},
                    new(){ Name= "Libya", Code= "LY"},
                    new(){ Name= "Liechtenstein", Code= "LI"},
                    new(){ Name= "Lithuania", Code= "LT"},
                    new(){ Name= "Luxembourg", Code= "LU"},
                    new(){ Name= "Macao", Code= "MO"},
                    new(){ Name= "Macedonia, the Former Yugoslav Republic of", Code= "MK"},
                    new(){ Name= "Madagascar", Code= "MG"},
                    new(){ Name= "Malawi", Code= "MW"},
                    new(){ Name= "Malaysia", Code= "MY"},
                    new(){ Name= "Maldives", Code= "MV"},
                    new(){ Name= "Mali", Code= "ML"},
                    new(){ Name= "Malta", Code= "MT"},
                    new(){ Name= "Marshall Islands", Code= "MH"},
                    new(){ Name= "Martinique", Code= "MQ"},
                    new(){ Name= "Mauritania", Code= "MR"},
                    new(){ Name= "Mauritius", Code= "MU"},
                    new(){ Name= "Mayotte", Code= "YT"},
                    new(){ Name= "Mexico", Code= "MX"},
                    new(){ Name= "Micronesia, Federated States of", Code= "FM"},
                    new(){ Name= "Moldova, Republic of", Code= "MD"},
                    new(){ Name= "Monaco", Code= "MC"},
                    new(){ Name= "Mongolia", Code= "MN"},
                    new(){ Name= "Montenegro", Code= "ME"},
                    new(){ Name= "Montserrat", Code= "MS"},
                    new(){ Name= "Morocco", Code= "MA"},
                    new(){ Name= "Mozambique", Code= "MZ"},
                    new(){ Name= "Myanmar", Code= "MM"},
                    new(){ Name= "Namibia", Code= "NA"},
                    new(){ Name= "Nauru", Code= "NR"},
                    new(){ Name= "Nepal", Code= "NP"},
                    new(){ Name= "Netherlands", Code= "NL"},
                    new(){ Name= "New Caledonia", Code= "NC"},
                    new(){ Name= "New Zealand", Code= "NZ"},
                    new(){ Name= "Nicaragua", Code= "NI"},
                    new(){ Name= "Niger", Code= "NE"},
                    new(){ Name= "Nigeria", Code= "NG"},
                    new(){ Name= "Niue", Code= "NU"},
                    new(){ Name= "Norfolk Island", Code= "NF"},
                    new(){ Name= "Northern Mariana Islands", Code= "MP"},
                    new(){ Name= "Norway", Code= "NO"},
                    new(){ Name= "Oman", Code= "OM"},
                    new(){ Name= "Pakistan", Code= "PK"},
                    new(){ Name= "Palau", Code= "PW"},
                    new(){ Name= "Palestine, State of", Code= "PS"},
                    new(){ Name= "Panama", Code= "PA"},
                    new(){ Name= "Papua New Guinea", Code= "PG"},
                    new(){ Name= "Paraguay", Code= "PY"},
                    new(){ Name= "Peru", Code= "PE"},
                    new(){ Name= "Philippines", Code= "PH"},
                    new(){ Name= "Pitcairn", Code= "PN"},
                    new(){ Name= "Poland", Code= "PL"},
                    new(){ Name= "Portugal", Code= "PT"},
                    new(){ Name= "Puerto Rico", Code= "PR"},
                    new(){ Name= "Qatar", Code= "QA"},
                    new(){ Name= "Reunion", Code= "RE"},
                    new(){ Name= "Romania", Code= "RO"},
                    new(){ Name= "Russian Federation", Code= "RU"},
                    new(){ Name= "Rwanda", Code= "RW"},
                    new(){ Name= "Saint Barthelemy", Code= "BL"},
                    new(){ Name= "Saint Helena", Code= "SH"},
                    new(){ Name= "Saint Kitts and Nevis", Code= "KN"},
                    new(){ Name= "Saint Lucia", Code= "LC"},
                    new(){ Name= "Saint Martin (French part)", Code= "MF"},
                    new(){ Name= "Saint Pierre and Miquelon", Code= "PM"},
                    new(){ Name= "Saint Vincent and the Grenadines", Code= "VC"},
                    new(){ Name= "Samoa", Code= "WS"},
                    new(){ Name= "San Marino", Code= "SM"},
                    new(){ Name= "Sao Tome and Principe", Code= "ST"},
                    new(){ Name= "Saudi Arabia", Code= "SA"},
                    new(){ Name= "Senegal", Code= "SN"},
                    new(){ Name= "Serbia", Code= "RS"},
                    new(){ Name= "Seychelles", Code= "SC"},
                    new(){ Name= "Sierra Leone", Code= "SL"},
                    new(){ Name= "Singapore", Code= "SG"},
                    new(){ Name= "Sint Maarten (Dutch part)", Code= "SX"},
                    new(){ Name= "Slovakia", Code= "SK"},
                    new(){ Name= "Slovenia", Code= "SI"},
                    new(){ Name= "Solomon Islands", Code= "SB"},
                    new(){ Name= "Somalia", Code= "SO"},
                    new(){ Name= "South Africa", Code= "ZA"},
                    new(){ Name= "South Georgia and the South Sandwich Islands", Code= "GS"},
                    new(){ Name= "South Sudan", Code= "SS"},
                    new(){ Name= "Spain", Code= "ES"},
                    new(){ Name= "Sri Lanka", Code= "LK"},
                    new(){ Name= "Sudan", Code= "SD"},
                    new(){ Name= "Suriname", Code= "SR"},
                    new(){ Name= "Svalbard and Jan Mayen", Code= "SJ"},
                    new(){ Name= "Swaziland", Code= "SZ"},
                    new(){ Name= "Sweden", Code= "SE"},
                    new(){ Name= "Switzerland", Code= "CH"},
                    new(){ Name= "Syrian Arab Republic", Code= "SY"},
                    new(){ Name= "Taiwan, Province of China", Code= "TW"},
                    new(){ Name= "Tajikistan", Code= "TJ" },
                    new(){ Name= "Thailand", Code= "TH"},
                    new(){ Name= "Timor-Leste", Code= "TL"},
                    new(){ Name= "Togo", Code= "TG"},
                    new(){ Name= "Tokelau", Code= "TK"},
                    new(){ Name= "Tonga", Code= "TO"},
                    new(){ Name= "Trinidad and Tobago", Code= "TT"},
                    new(){ Name= "Tunisia", Code= "TN"},
                    new(){ Name= "Turkey", Code= "TR"},
                    new(){ Name= "Turkmenistan", Code= "TM"},
                    new(){ Name= "Turks and Caicos Islands", Code= "TC"},
                    new(){ Name= "Tuvalu", Code= "TV"},
                    new(){ Name= "Uganda", Code= "UG"},
                    new(){ Name= "Ukraine", Code= "UA"},
                    new(){ Name= "United Arab Emirates", Code= "AE"},
                    new(){ Name= "United Kingdom", Code= "GB"},
                    new(){ Name= "United Republic of Tanzania", Code= "TZ"},
                    new(){ Name= "United States", Code= "US"},
                    new(){ Name= "Uruguay", Code= "UY"},
                    new(){ Name= "US Virgin Islands", Code= "VI"},
                    new(){ Name= "Uzbekistan", Code= "UZ"},
                    new(){ Name= "Vanuatu", Code= "VU"},
                    new(){ Name= "Venezuela", Code= "VE"},
                    new(){ Name= "Vietnam", Code= "VN"},
                    new(){ Name= "Wallis and Futuna", Code= "WF"},
                    new(){ Name= "Western Sahara", Code= "EH"},
                    new(){ Name= "Yemen", Code= "YE"},
                    new(){ Name= "Zambia", Code= "ZM"},
                    new(){ Name= "Zimbabwe", Code= "ZW"}
            };
            return countries;
        }
        static IEnumerable<City> GetPreconfiguredCities()
        {
            var cities = new List<City>()
            {
                //Czech Republic
                new (){ Name="Prague", CountryId=60 },
                new (){ Name="Brno", CountryId=60 },
                new (){ Name="Ostrava", CountryId=60 },
                new (){ Name="Pilsen", CountryId=60 },
                new (){ Name="Liberec", CountryId=60 },
                new (){ Name="Olomouc", CountryId=60 },

                //Poland
                new (){ Name="Warsaw", CountryId=178 },
                new (){ Name="Cracow", CountryId=178 },
                new (){ Name="Lodz", CountryId=178 },
                new (){ Name="Wroclaw", CountryId=178 },
                new (){ Name="Poznan", CountryId=178 },
                new (){ Name="Gdansk", CountryId=178 },

                //Ukraine
                new (){ Name="Kyiv", CountryId=233 },
                new (){ Name="Kharkiv", CountryId=233 },
                new (){ Name="Odesa", CountryId=233 },
                new (){ Name="Dnipro", CountryId=233 },
                new (){ Name="Lviv", CountryId=233 },
                new (){ Name="Mariupol", CountryId=233 },
            };
            return cities;
        }


        static IEnumerable<Category> GetPreconfiguredMarketplaceCategories()
        {
            var categories = new List<Category>
            {
/*1*/           new(){ Name = "Beauty and health", UrlSlug = "beauty-and-health", Image = "BeautyAndHealth.png", ParentId = null},
/*2*/           new(){ Name = "House and garden", UrlSlug = "house-and-garden", Image = "HouseAndGarden.png", ParentId = null},
/*3*/           new(){ Name = "Clothes and shoes", UrlSlug = "clothes-and-shoes", Image = "ClothesAndShoes.png", ParentId = null},
/*4*/           new(){ Name = "Technology and electronics", UrlSlug = "technology-and-electronics", Image = "TechnologyAndElectronics.png", ParentId = null},
/*5*/           new(){ Name = "Goods for children", UrlSlug = "goods-for-children", Image = "GoodsForChildren.png", ParentId = null},
/*6*/           new(){ Name = "Auto", UrlSlug = "auto", Image = "Auto.png", ParentId = null},
/*7*/           new(){ Name = "Gifts, hobbies, books", UrlSlug = "gifts-hobbies-books", Image = "GiftsHobbiesBooks.png", ParentId = null},
/*8*/           new(){ Name = "Accessories and decorations", UrlSlug = "accessories-and-decorations", Image = "AccessoriesAndDecorations.png", ParentId = null},
/*9*/           new(){ Name = "Materials for repair", UrlSlug = "materials-for-repair", Image = "", ParentId = null},
/*10*/          new(){ Name = "Sports and recreation", UrlSlug = "sports-and-recreation", Image = "SportsAndRecreation.png", ParentId = null},
/*11*/          new(){ Name = "Medicines and medical products", UrlSlug = "medicines-and-medical-products", Image = "", ParentId = null},
/*12*/          new(){ Name = "Pets and pet products", UrlSlug = "pets-and-pet-products", Image = "", ParentId = null},
/*13*/          new(){ Name = "Stationery", UrlSlug = "stationery", Image = "Stationery.png", ParentId = null},
/*14*/          new(){ Name = "Overalls and shoes", UrlSlug = "overalls-and-shoes", Image = "", ParentId = null},
/*15*/          new(){ Name = "Wedding goods", UrlSlug = "wedding-goods", Image = "", ParentId = null},
/*16*/          new(){ Name = "Food products, drinks", UrlSlug = "food-products-drinks", Image = "", ParentId = null},
/*17*/          new(){ Name = "Tools", UrlSlug = "tools", Image = "Tools.png", ParentId = null},
/*18*/          new(){ Name = "Antiques and collectibles", UrlSlug = "antiques-and-collectibles", Image = "", ParentId = null},
/*19*/          new(){ Name = "Construction", UrlSlug = "сonstruction", Image = "Construction.png", ParentId = null},

/*20*/          new(){ Name = "Men's clothing", UrlSlug = "mens-clothing", Image = "", ParentId = 3},
/*21*/          new(){ Name = "Women's clothes", UrlSlug = "womens-clothes", Image = "", ParentId = 3},
/*22*/          new(){ Name = "Children's clothes, shoes, accessories", UrlSlug = "Childrens-clothes-shoes-accessories", Image = "", ParentId = 3},
/*23*/          new(){ Name = "Sportswear and footwear", UrlSlug = "sportswear-and-footwear", Image = "", ParentId = 3},
/*24*/          new(){ Name = "Footwear", UrlSlug = "footwear", Image = "", ParentId = 3},
/*25*/          new(){ Name = "Overalls and shoes", UrlSlug = "overalls-and-shoes", Image = "", ParentId = 3},
/*26*/          new(){ Name = "Carnival costumes", UrlSlug = "carnival-costumes", Image = "", ParentId = 3},
/*27*/          new(){ Name = "Ethnic clothing", UrlSlug = "ethnic-clothing", Image = "", ParentId = 3},

/*28*/          new(){ Name = "Computer equipment and software", UrlSlug = "computer-equipment-and-software", Image = "", ParentId = 4},
/*29*/          new(){ Name = "Household appliances", UrlSlug = "household-appliances", Image = "", ParentId = 4},
/*30*/          new(){ Name = "Phones and accessories", UrlSlug = "phones-and-accessories", Image = "", ParentId = 4},
/*31*/          new(){ Name = "Audio equipment and accessories", UrlSlug = "audio-equipment-and-accessories", Image = "", ParentId = 4},
/*32*/          new(){ Name = "Spare parts for machinery and electronics", UrlSlug = "spare-parts-for-machinery-and-electronics", Image = "", ParentId = 4},
/*33*/          new(){ Name = "TV and video equipment", UrlSlug = "tv-and-video-equipment", Image = "", ParentId = 4},
/*34*/          new(){ Name = "Car electronics", UrlSlug = "car-electronics", Image = "", ParentId = 4},
/*35*/          new(){ Name = "Photos, camcorders and accessories", UrlSlug = "photos-camcorders-and-accessories", Image = "", ParentId = 4},
/*36*/          new(){ Name = "3d devices", UrlSlug = "3d-devices", Image = "", ParentId = 4},
/*37*/          new(){ Name = "Equipment for satellite internet", UrlSlug = "equipment-for-satellite-internet", Image = "", ParentId = 4},

/*38*/          new(){ Name = "Tablet computers", UrlSlug = "tablet-computers", Image = "", ParentId = 28},
/*39*/          new(){ Name = "Laptops", UrlSlug = "laptops", Image = "", ParentId = 28},
/*40*/          new(){ Name = "Monitors", UrlSlug = "monitors", Image = "", ParentId = 28},
/*41*/          new(){ Name = "Components for computer equipment", UrlSlug = "components-for-computer-equipment", Image = "", ParentId = 28},
/*42*/          new(){ Name = "Computer accessories", UrlSlug = "computer-accessories", Image = "", ParentId = 28},
/*43*/          new(){ Name = "Smart watches and fitness bracelets", UrlSlug = "smart-watches-and-fitness-bracelets", Image = "", ParentId = 28},
/*44*/          new(){ Name = "Printers, scanners, MFPs and components", UrlSlug = "printers-scanners-mfps-and-components", Image = "", ParentId = 28},
/*45*/          new(){ Name = "Information carriers", UrlSlug = "information-carriers", Image = "", ParentId = 28},
/*46*/          new(){ Name = "Game consoles and components", UrlSlug = "game-consoles-and-components", Image = "", ParentId = 28},
/*47*/          new(){ Name = "Desktops", UrlSlug = "desktops", Image = "", ParentId = 28},
/*48*/          new(){ Name = "Software", UrlSlug = "software", Image = "", ParentId = 28},
/*49*/          new(){ Name = "Server equipment", UrlSlug = "server-equipment", Image = "", ParentId = 28},
/*50*/          new(){ Name = "Mining equipment", UrlSlug = "mining-equipment", Image = "", ParentId = 28},
/*51*/          new(){ Name = "E-books", UrlSlug = "e-books", Image = "", ParentId = 28},
/*52*/          new(){ Name = "Single board computers and nettops", UrlSlug = "single-board-computers-and-nettops", Image = "", ParentId = 28},
/*53*/          new(){ Name = "Portable electronic translators", UrlSlug = "portable-electronic-translators", Image = "", ParentId = 28},

                new(){ Name = "Cables for electronics", UrlSlug = "cables-for-electronics", Image = "", ParentId = 41},
                new(){ Name = "HDD, SSD", UrlSlug = "hdd-ssd", Image = "", ParentId = 41},
                new(){ Name = "Batteries for laptops, tablets, e-books, translators", UrlSlug = "batteries-for-laptops-tablets-e-books-translators", Image = "", ParentId = 41},
                new(){ Name = "Laptop chargers", UrlSlug = "laptop-chargers", Image = "", ParentId = 41},
                new(){ Name = "Laptop body parts", UrlSlug = "laptop-body-parts", Image = "", ParentId = 41},
                new(){ Name = "Memory modules", UrlSlug = "memory-modules", Image = "", ParentId = 41},
                new(){ Name = "Processors", UrlSlug = "processors", Image = "", ParentId = 41},
                new(){ Name = "Coolers and cooling systems", UrlSlug = "coolers-and-cooling-systems", Image = "", ParentId = 41},
                new(){ Name = "Matrixes for laptops, tablets and monitors", UrlSlug = "matrixes-for-laptops-tablets-and-monitors", Image = "", ParentId = 41},
                new(){ Name = "Cables and connectors for laptops, computers, tablets", UrlSlug = "cables-and-connectors-for-laptops-computers-tablets", Image = "", ParentId = 41},
                new(){ Name = "Keyboard blocks for laptops", UrlSlug = "keyboard-blocks-for-laptops", Image = "", ParentId = 41},
                new(){ Name = "Touchscreen for displays", UrlSlug = "touchscreen-for-displays", Image = "", ParentId = 41},
                new(){ Name = "Microcircuits", UrlSlug = "microcircuits", Image = "", ParentId = 41},
                new(){ Name = "Spare parts for TVs and monitors", UrlSlug = "spare-parts-for-tvs-and-monitors", Image = "", ParentId = 41},
                new(){ Name = "Motherboards", UrlSlug = "motherboards", Image = "", ParentId = 41},
                new(){ Name = "Video cards", UrlSlug = "video-cards", Image = "", ParentId = 41},
                new(){ Name = "Enclosures for computers", UrlSlug = "enclosures-for-computers", Image = "", ParentId = 41},
                new(){ Name = "Power supplies for computers", UrlSlug = "power-supplies-for-computers", Image = "", ParentId = 41},
                new(){ Name = "Patch cord", UrlSlug = "patch-cord", Image = "", ParentId = 41},
                new(){ Name = "Pockets for hard drives", UrlSlug = "pockets-for-hard-drives", Image = "", ParentId = 41},
                new(){ Name = "Adapters and port expansion cards", UrlSlug = "adapters-and-port-expansion-cards", Image = "", ParentId = 41},
                new(){ Name = "Audio parts for laptops", UrlSlug = "audio-parts-for-laptops", Image = "", ParentId = 41},
                new(){ Name = "Thermal paste", UrlSlug = "thermal-paste", Image = "", ParentId = 41},
                new(){ Name = "Sound cards", UrlSlug = "sound-cards", Image = "", ParentId = 41},
                new(){ Name = "Network cards", UrlSlug = "network-cards", Image = "", ParentId = 41},
                new(){ Name = "Optical drives", UrlSlug = "optical-drives", Image = "", ParentId = 41},
                new(){ Name = "Cases for tablets", UrlSlug = "cases-for-tablets", Image = "", ParentId = 41},
                new(){ Name = "Accessories for matrices and displays", UrlSlug = "accessories-for-matrices-and-displays", Image = "", ParentId = 41},
                new(){ Name = "Cameras for laptops", UrlSlug = "cameras-for-laptops", Image = "", ParentId = 41},
                new(){ Name = "Cooling systems for laptops", UrlSlug = "cooling-systems-for-laptops", Image = "", ParentId = 41},
                new(){ Name = "TV and FM tuners", UrlSlug = "tv-and-fm-tuners", Image = "", ParentId = 41},
                new(){ Name = "Postcards", UrlSlug = "postcards", Image = "", ParentId = 41},
                new(){ Name = "Accessories for routers", UrlSlug = "accessories-for-routers", Image = "", ParentId = 41},


            };
            return categories;
        }

        static IEnumerable<Shop> GetPreconfiguredMarketplaceShops(string userId)
        {
            var shops = new List<Shop>
            {
                new(){ Name = "Mall",Description="",Photo="4918050.jpg",Email="dg646726@gmail.com",SiteUrl="http://mall.novakvova.com/",CityId=1,UserId=userId},
            };
            return shops;
        }

        static IEnumerable<ProductStatus> GetPreconfiguredMarketplaceProductStatus()
        {
            var productStatuses = new List<ProductStatus>
            {
                new(){ Name = "In stock" },
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
/* 1 */         new(){ Measure = "UA"},

            };
            return units;
        }
        static IEnumerable<FilterGroup> GetPreconfiguredMarketplaceFilterGroups()
        {
            var filterGroups = new List<FilterGroup>
            {
/* 1 */         new(){ Name = "The main"},

            };
            return filterGroups;
        }
        static IEnumerable<FilterName> GetPreconfiguredMarketplaceFilterNames()
        {
            var filterNames = new List<FilterName>
            {
/* 1 */         new(){ Name = "Сondition",FilterGroupId=1},
/* 2 */         new(){ Name = "Purpose",FilterGroupId=1},
/* 3 */         new(){ Name = "Video memory type",FilterGroupId=1},
/* 4 */         new(){ Name = "Graphics chipset",FilterGroupId=1},
/* 5 */         new(){ Name = "Memory bus width", FilterGroupId = 1},
/* 6 */         new(){ Name = "Producer", FilterGroupId = 1},  
/* 7 */         new(){ Name = "Connection type", FilterGroupId = 1},
/* 8 */         new(){ Name = "Interfaces", FilterGroupId = 1},
/* 9 */         new(){ Name = "Cooling system", FilterGroupId = 1},
/* 10 */        new(){ Name = "Peculiarities", FilterGroupId = 1},
/* 11 */        new(){ Name = "Producing country", FilterGroupId = 1},
/* 12 */        new(){ Name = "Quality class", FilterGroupId = 1},
/* 13 */        new(){ Name = "Warranty period, months", FilterGroupId = 1},
/* 14 */        new(){ Name = "Processor frequency, MHz", FilterGroupId = 1},
/* 15 */        new(){ Name = "Video memory frequency, MHz", FilterGroupId = 1},
/* 16 */        new(){ Name = "Video memory size, MB", FilterGroupId = 1},
/* 17 */        new(){ Name = "Color",FilterGroupId=1},
/* 18 */        new(){ Name = "Women's clothing size",FilterGroupId=1,UnitId=1},
/* 19 */        new(){ Name = "Brand",FilterGroupId=1},

            };
            return filterNames;
        }

        static IEnumerable<FilterValue> GetPreconfiguredMarketplaceFilterValues(Category category)
        {
            var filterValues = new List<FilterValue>
            {
/* 1 */         new(){ Value = "Yellow",FilterNameId=17},
/* 2 */         new(){ Value = "Black",FilterNameId=17},
/* 3 */         new(){ Value = "Blue",FilterNameId=17},
/* 4 */         new(){ Value = "Red",FilterNameId=17},
/* 5 */         new(){ Value = "Green", FilterNameId = 17},
/* 6 */         new(){ Value = "34", FilterNameId = 18},  
/* 7 */         new(){ Value = "36", FilterNameId = 18},
/* 8 */         new(){ Value = "38", FilterNameId = 18},
/* 9 */         new(){ Value = "40", FilterNameId = 18},
/* 10 */        new(){ Value = "40/42", FilterNameId = 18},
/* 11 */        new(){ Value = "40/44", FilterNameId = 18},
/* 12 */        new(){ Value = "Nike", FilterNameId = 19},
/* 13 */        new(){ Value = "Puma", FilterNameId = 19},
/* 14 */        new(){ Value = "Zara", FilterNameId = 19},
/* 15 */        new(){ Value = "H&M", FilterNameId = 19},
/* 16 */        new(){ Value = "AAA", FilterNameId = 19},
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
             new(){ Id=OrderStatusId.InProcess, Name="In Process"},
             new(){ Id=OrderStatusId.PendingPayment, Name="Pending Payment"},
             new(){ Id=OrderStatusId.Completed, Name="Completed"},
             new(){ Id=OrderStatusId.Canceled, Name="Canceled"},
            };
            return statuses;
        }
    }
}
