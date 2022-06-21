using DAL;
using DAL.Data;
using DAL.Entities.Identity;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SpaServices.ReactDevelopmentServer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Reflection;
using System.Text;
using WebAPI.Constants;
using WebAPI.Interfaces;
using WebAPI.Interfaces.Characteristics;
using WebAPI.Interfaces.Emails;
using WebAPI.Interfaces.Filters;
using WebAPI.Interfaces.Products;
using WebAPI.Interfaces.Users;
using WebAPI.Mapper;
using WebAPI.Middlewares;
using WebAPI.Services;
using WebAPI.Services.Characteristcs;
using WebAPI.Services.Emails;
using WebAPI.Services.Filters;
using WebAPI.Services.Products;
using WebAPI.Services.Users;
using WebAPI.Settings;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

builder.Services.Configure<JwtSettings>(builder.Configuration.GetSection("JwtSettings"));
builder.Services.Configure<GoogleAuthSettings>(builder.Configuration.GetSection("GoogleAuthSettings"));
builder.Services.Configure<MailSettings>(builder.Configuration.GetSection("MailSettings"));
builder.Services.Configure<ReCaptchaSettings>(builder.Configuration.GetSection("ReCaptcha"));
builder.Services.Configure<ClientUrl>(builder.Configuration.GetSection("ClientServer"));

// Database & Identity
builder.Services.AddDbContext<MarketplaceDbContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("MarketplaceConnection")));

builder.Services.AddIdentity<AppUser, IdentityRole>()
                .AddEntityFrameworkStores<MarketplaceDbContext>().AddDefaultTokenProviders();


//Services
builder.Services.AddScoped(typeof(IRepository<>), typeof(EfRepository<>));
builder.Services.AddScoped(typeof(IReadRepository<>), typeof(EfRepository<>));
builder.Services.AddScoped<IJwtTokenService, JwtTokenService>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<ICharacteristicGroupService, CharacteristicGroupService>();
builder.Services.AddScoped<ICharacteristicNameService, CharacteristicNameService>();
builder.Services.AddScoped<ICharacteristicValueService, CharacteristicValueService>();
builder.Services.AddScoped<ICountryService, CountryService>();
builder.Services.AddScoped<ICityService, CityService>();
builder.Services.AddScoped<IFilterGroupService, FilterGroupService>();
builder.Services.AddScoped<IFilterNameService, FilterNameService>();
builder.Services.AddScoped<IFilterValueService, FilterValueService>();
builder.Services.AddScoped<IUnitService, UnitService>();
builder.Services.AddScoped<IShopService, ShopService>();
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<IProductStatusService, ProductStatusService>();
builder.Services.AddScoped<ITemplateService, TemplateService>();
builder.Services.AddScoped<IEmailSenderService, EmailSenderService>();
builder.Services.AddScoped<IConfirmEmailService, ConfirmEmailService>();
builder.Services.AddScoped<IResetPasswordService, ResetPasswordService>();

//reCaptcha
builder.Services.AddTransient<IRecaptchaService, RecaptchaService>();

//Mapper
builder.Services.AddAutoMapper(typeof(AppMappingProfile));

//Validation
builder.Services.AddFluentValidation(x => x.RegisterValidatorsFromAssembly(Assembly.GetExecutingAssembly()));

builder.Services.AddSpaStaticFiles(configuration =>
{
    configuration.RootPath = "client-app/build";
});

builder.Services.AddEndpointsApiExplorer();

//Authentication
var jwtSetting = builder.Configuration.GetSection("JwtSettings").Get<JwtSettings>();
var signinKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSetting.Key));
builder.Services.AddAuthentication(options =>
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

// Swagger
var assemblyName = Assembly.GetExecutingAssembly().GetName().Name;
builder.Services.AddSwaggerGen(c =>
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

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var scopedProvider = scope.ServiceProvider;
    try
    {
        var catalogContext = scopedProvider.GetRequiredService<MarketplaceDbContext>();
        var userManager = scopedProvider.GetRequiredService<UserManager<AppUser>>();
        var roleManager = scopedProvider.GetRequiredService<RoleManager<IdentityRole>>();
        await MarketplaceDbContextSeed.SeedAsync(catalogContext, userManager, roleManager);
    }
    catch (Exception ex)
    {
        app.Logger.LogError(ex, "An error occurred seeding the DB.");
    }
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseStaticFiles();
app.UseSpaStaticFiles();

app.UseRouting();

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

//Middleware
app.UseMiddleware<ErrorHandlerMiddleware>();



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


app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
        name: "default",
        pattern: "{controller}/{action=Index}/{id?}");
});

app.UseSpa(spa =>
{
    spa.Options.SourcePath = "client-app";
    if (app.Environment.IsDevelopment())
    {
        spa.UseReactDevelopmentServer(npmScript: "start");
    }
});


app.Run();
