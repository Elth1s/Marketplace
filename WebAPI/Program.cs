using DAL;
using DAL.Data;
using DAL.Entities;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SpaServices.ReactDevelopmentServer;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using WebAPI.Extensions;
using WebAPI.Helpers;
using WebAPI.Mapper;
using WebAPI.Middlewares;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllersWithViews(opts =>
{
    opts.Conventions.Insert(0, new LocalizationConvention());
    opts.Filters.Add(new MiddlewareFilterAttribute(typeof(LocalizationPipeline)));
});

//Localization
builder.Services.ConfigureLocalization();

//Settings
builder.Services.ConfigureSettings(builder.Configuration);

// Database & Identity
builder.Services.AddDbContext<MarketplaceDbContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("MarketplaceConnection")));

builder.Services.AddIdentity<AppUser, IdentityRole>(o => o.User.RequireUniqueEmail = false)
                .AddEntityFrameworkStores<MarketplaceDbContext>().AddDefaultTokenProviders();


//Services
builder.Services.AddScoped(typeof(IRepository<>), typeof(EfRepository<>));
builder.Services.AddScoped(typeof(IReadRepository<>), typeof(EfRepository<>));
builder.Services.AddServices();

//Mapper
builder.Services.AddAutoMapper(typeof(AppMappingProfile));

//Validation
builder.Services.AddFluentValidation(x =>
{
    x.RegisterValidatorsFromAssembly(Assembly.GetExecutingAssembly());
    x.DisableDataAnnotationsValidation = true;
    x.ValidatorOptions.LanguageManager = new CustomLanguageManager();
});


builder.Services.AddSpaStaticFiles(configuration =>
{
    configuration.RootPath = "client-app/build";
});

builder.Services.AddEndpointsApiExplorer();

//Authentication
builder.Services.AddJwtAuthentication(builder.Configuration);

// Swagger
builder.Services.AddSwagger();

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

//Static Files
app.AddStaticFiles();


app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
        name: "default",
        pattern: "{culture}/{controller}/{action=Index}/{id?}");
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
