using DAL.Data;
using DAL.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);



// Database & Identity
builder.Services.AddDbContext<MarketplaceDbContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("MarketplaceConnection")));


builder.Services.AddIdentity<AppUser, IdentityRole>()
                .AddEntityFrameworkStores<MarketplaceDbContext>().AddDefaultTokenProviders();


builder.Services.AddEndpointsApiExplorer();

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

app.UseHttpsRedirection();

app.UseAuthentication();
// app.UseAuthorization();

app.Run();
