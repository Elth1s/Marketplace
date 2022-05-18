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
using WebAPI.Mapper;
using WebAPI.Middlewares;
using WebAPI.Services;
using WebAPI.Settings;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

builder.Services.Configure<JwtSettings>(builder.Configuration.GetSection("JwtSettings"));

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
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Marketplace", Version = "v1" });
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
});

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var scopedProvider = scope.ServiceProvider;
    try
    {
        var catalogContext = scopedProvider.GetRequiredService<MarketplaceDbContext>();
        await MarketplaceDbContextSeed.SeedAsync(catalogContext);

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
