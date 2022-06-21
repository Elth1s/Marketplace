using DAL.Constants;
using DAL.Entities;
using DAL.Entities.Identity;
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

            var defaultUser = new AppUser { UserName = UsersInfo.DefaultUserName, Email = UsersInfo.DefaultUserName };
            await userManager.CreateAsync(defaultUser, UsersInfo.DefaultPassword);
            defaultUser = await userManager.FindByNameAsync(UsersInfo.DefaultUserName);

            var adminUser = new AppUser { UserName = UsersInfo.AdminUserName, Email = UsersInfo.AdminUserName };
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

        static IEnumerable<FilterName> GetPreconfiguredMarketplaceFilterGroup()
        {
            var filterGroups = new List<FilterName>
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
