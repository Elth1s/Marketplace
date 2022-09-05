using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Localization.Routing;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Localization;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Globalization;
using System.Reflection;
using System.Text;
using WebAPI.Constants;
using WebAPI.Helpers;
using WebAPI.Interfaces;
using WebAPI.Interfaces.Characteristics;
using WebAPI.Interfaces.Emails;
using WebAPI.Interfaces.Filters;
using WebAPI.Interfaces.Orders;
using WebAPI.Interfaces.Products;
using WebAPI.Interfaces.Questions;
using WebAPI.Interfaces.Reviews;
using WebAPI.Interfaces.Shops;
using WebAPI.Interfaces.Users;
using WebAPI.Services;
using WebAPI.Services.Characteristcs;
using WebAPI.Services.Emails;
using WebAPI.Services.Filters;
using WebAPI.Services.Orders;
using WebAPI.Services.Products;
using WebAPI.Services.Questions;
using WebAPI.Services.Reviews;
using WebAPI.Services.Shops;
using WebAPI.Services.Users;
using WebAPI.Settings;

namespace WebAPI.Extensions
{
    public static partial class ServiceExtension
    {
        public static void AddSwagger(this IServiceCollection services)
        {
            var assemblyName = Assembly.GetExecutingAssembly().GetName().Name;
            services.AddSwaggerGen(c =>
           {
               c.SwaggerDoc("v1", new OpenApiInfo { Title = assemblyName, Version = "v1" });
               c.AddSecurityDefinition("Bearer",
                   new OpenApiSecurityScheme
                   {
                       Description = "JWT Authorization header using the Bearer scheme.",
                       Type = SecuritySchemeType.Http,
                       Scheme = "bearer"
                   });
               c.AddSecurityRequirement(new OpenApiSecurityRequirement{
                    {
                        new OpenApiSecurityScheme{
                            Reference = new OpenApiReference{
                                Id = "Bearer",
                                Type = ReferenceType.SecurityScheme
                            }
                        },new List<string>()
                    }
               });
               var fileDoc = Path.Combine(System.AppContext.BaseDirectory, $"{assemblyName}.xml");
               c.IncludeXmlComments(fileDoc);
           });
        }
        public static void AddJwtAuthentication(this IServiceCollection services, IConfiguration configuration)
        {

            var jwtSetting = configuration.GetSection("JwtSettings").Get<JwtSettings>();
            var signinKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSetting.Key));
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(cfg =>
            {
                cfg.RequireHttpsMetadata = false;
                cfg.SaveToken = false;
                cfg.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateAudience = true,
                    ValidateIssuer = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ClockSkew = TimeSpan.Zero,

                    IssuerSigningKey = signinKey,
                    ValidIssuer = jwtSetting.Issuer,
                    ValidAudience = jwtSetting.Audience,
                };
            });
        }
        public static void AddServices(this IServiceCollection services)
        {
            services.AddScoped<IJwtTokenService, JwtTokenService>();
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IUserService, UserService>();

            services.AddScoped<ICategoryService, CategoryService>();

            services.AddScoped<ICharacteristicGroupService, CharacteristicGroupService>();
            services.AddScoped<ICharacteristicNameService, CharacteristicNameService>();
            services.AddScoped<ICharacteristicValueService, CharacteristicValueService>();

            services.AddScoped<ICountryService, CountryService>();
            services.AddScoped<ICityService, CityService>();

            services.AddScoped<IFilterGroupService, FilterGroupService>();
            services.AddScoped<IFilterNameService, FilterNameService>();
            services.AddScoped<IFilterValueService, FilterValueService>();

            services.AddScoped<IUnitService, UnitService>();

            services.AddScoped<IShopService, ShopService>();
            services.AddScoped<IShopReviewService, ShopReviewService>();

            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<IProductStatusService, ProductStatusService>();
            services.AddScoped<IProductImageService, ProductImageService>();

            services.AddScoped<ITemplateService, TemplateService>();
            services.AddScoped<IEmailSenderService, EmailSenderService>();
            services.AddScoped<IConfirmEmailService, ConfirmEmailService>();
            services.AddScoped<IResetPasswordService, ResetPasswordService>();
            services.AddScoped<IPhoneCodeSenderService, PhoneCodeSenderService>();
            services.AddScoped<IConfirmPhoneService, ConfirmPhoneService>();

            services.AddScoped<IBasketItemService, BasketItemService>();

            services.AddScoped<IQuestionService, QuestionService>();
            services.AddScoped<IQuestionImageService, QuestionImageService>();
            services.AddScoped<IQuestionReplyService, QuestionReplyService>();

            services.AddScoped<IOrderService, OrderService>();
            services.AddScoped<IOrderStatusService, OrderStatusService>();
            services.AddScoped<IDeliveryTypeService, DeliveryTypeService>();

            services.AddScoped<IReviewService, ReviewService>();
            services.AddScoped<IReviewReplyService, ReviewReplyService>();
            services.AddScoped<IReviewImageService, ReviewImageService>();

            services.AddScoped<PhoneNumberManager>();

            services.AddScoped<ISaleService, SaleService>();

            //reCaptcha
            services.AddTransient<IRecaptchaService, RecaptchaService>();


            ExtensionMethods.StringLocalizerFactory = services.BuildServiceProvider().GetService<IStringLocalizerFactory>();
        }
        public static void ConfigureLocalization(this IServiceCollection services)
        {
            services.AddLocalization(options => options.ResourcesPath = "Resources");

            var supportedCultures = new[]
            {
                 new CultureInfo("en-US"),
                 new CultureInfo("uk"),
            };

            var options = new RequestLocalizationOptions()
            {
                DefaultRequestCulture = new RequestCulture(culture: "uk", uiCulture: "uk"),
                SupportedCultures = supportedCultures,
                SupportedUICultures = supportedCultures,
                ApplyCurrentCultureToResponseHeaders = true
            };

            RequestCultureProvider requestProvider = options.RequestCultureProviders.OfType<CookieRequestCultureProvider>().First();
            options.RequestCultureProviders.Remove(requestProvider);

            var provider = new RouteDataRequestCultureProvider() { Options = options };

            options.RequestCultureProviders.Insert(
                0, provider);


            services.AddSingleton(options);
        }

        public static void ConfigureSettings(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<JwtSettings>(configuration.GetSection("JwtSettings"));
            services.Configure<GoogleAuthSettings>(configuration.GetSection("GoogleAuthSettings"));
            services.Configure<MailSettings>(configuration.GetSection("MailSettings"));
            services.Configure<ReCaptchaSettings>(configuration.GetSection("ReCaptcha"));
            services.Configure<ClientUrl>(configuration.GetSection("ClientServer"));
            services.Configure<TwilioSettings>(configuration.GetSection("Twilio"));
        }
        public static void AddStaticFiles(this WebApplication app)
        {
            var root = Path.Combine(Directory.GetCurrentDirectory(), ImagePath.RootImagePath);
            if (!Directory.Exists(root))
            {
                Directory.CreateDirectory(root);
            }
            app.UseStaticFiles(new StaticFileOptions
            {
                FileProvider = new PhysicalFileProvider(root),
                RequestPath = ImagePath.RequestRootImagePath
            });

            var usersImages = Path.Combine(Directory.GetCurrentDirectory(), ImagePath.UsersImagePath);
            if (!Directory.Exists(usersImages))
            {
                Directory.CreateDirectory(usersImages);
            }
            app.UseStaticFiles(new StaticFileOptions
            {
                FileProvider = new PhysicalFileProvider(usersImages),
                RequestPath = ImagePath.RequestUsersImagePath
            });


            var categoriesImages = Path.Combine(Directory.GetCurrentDirectory(), ImagePath.CategoriesImagePath);
            if (!Directory.Exists(categoriesImages))
            {
                Directory.CreateDirectory(categoriesImages);
            }
            app.UseStaticFiles(new StaticFileOptions
            {
                FileProvider = new PhysicalFileProvider(categoriesImages),
                RequestPath = ImagePath.RequestCategoriesImagePath
            });


            var shopsImages = Path.Combine(Directory.GetCurrentDirectory(), ImagePath.ShopsImagePath);
            if (!Directory.Exists(shopsImages))
            {
                Directory.CreateDirectory(shopsImages);
            }
            app.UseStaticFiles(new StaticFileOptions
            {
                FileProvider = new PhysicalFileProvider(shopsImages),
                RequestPath = ImagePath.RequestShopsImagePath
            });

            var productsImages = Path.Combine(Directory.GetCurrentDirectory(), ImagePath.ProductsImagePath);
            if (!Directory.Exists(productsImages))
            {
                Directory.CreateDirectory(productsImages);
            }
            app.UseStaticFiles(new StaticFileOptions
            {
                FileProvider = new PhysicalFileProvider(productsImages),
                RequestPath = ImagePath.RequestProductsImagePath
            });

            var reviewImages = Path.Combine(Directory.GetCurrentDirectory(), ImagePath.ReviewsImagePath);
            if (!Directory.Exists(reviewImages))
            {
                Directory.CreateDirectory(reviewImages);
            }
            app.UseStaticFiles(new StaticFileOptions
            {
                FileProvider = new PhysicalFileProvider(reviewImages),
                RequestPath = ImagePath.RequestReviewsImagePath
            });

            var questionImages = Path.Combine(Directory.GetCurrentDirectory(), ImagePath.QuestionsImagePath);
            if (!Directory.Exists(questionImages))
            {
                Directory.CreateDirectory(questionImages);
            }
            app.UseStaticFiles(new StaticFileOptions
            {
                FileProvider = new PhysicalFileProvider(questionImages),
                RequestPath = ImagePath.RequestQuestionsImagePath
            });

            var deliveryTypeImages = Path.Combine(Directory.GetCurrentDirectory(), ImagePath.DeliveryTypesImagePath);
            if (!Directory.Exists(deliveryTypeImages))
            {
                Directory.CreateDirectory(deliveryTypeImages);
            }
            app.UseStaticFiles(new StaticFileOptions
            {
                FileProvider = new PhysicalFileProvider(deliveryTypeImages),
                RequestPath = ImagePath.RequestDeliveryTypesImagePath
            });

            var saleImages = Path.Combine(Directory.GetCurrentDirectory(), ImagePath.SalesImagePath);
            if (!Directory.Exists(saleImages))
            {
                Directory.CreateDirectory(saleImages);
            }
            app.UseStaticFiles(new StaticFileOptions
            {
                FileProvider = new PhysicalFileProvider(saleImages),
                RequestPath = ImagePath.RequestSalesImagePath
            });

            var backgroundImages = Path.Combine(Directory.GetCurrentDirectory(), ImagePath.BackgroundImagePath);
            if (!Directory.Exists(backgroundImages))
            {
                Directory.CreateDirectory(backgroundImages);
            }
            app.UseStaticFiles(new StaticFileOptions
            {
                FileProvider = new PhysicalFileProvider(backgroundImages),
                RequestPath = ImagePath.BackgroundAssetsPath
            });

            var iconsImages = Path.Combine(Directory.GetCurrentDirectory(), ImagePath.IconsImagePath);
            if (!Directory.Exists(iconsImages))
            {
                Directory.CreateDirectory(iconsImages);
            }
            app.UseStaticFiles(new StaticFileOptions
            {
                FileProvider = new PhysicalFileProvider(iconsImages),
                RequestPath = ImagePath.IconsAssetsPath
            });

            var logosImages = Path.Combine(Directory.GetCurrentDirectory(), ImagePath.LogosImagePath);
            if (!Directory.Exists(logosImages))
            {
                Directory.CreateDirectory(logosImages);
            }
            app.UseStaticFiles(new StaticFileOptions
            {
                FileProvider = new PhysicalFileProvider(logosImages),
                RequestPath = ImagePath.LogosAssetsPath
            });
        }

    }
}
