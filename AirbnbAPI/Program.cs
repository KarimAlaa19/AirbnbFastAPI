using Microsoft.EntityFrameworkCore;
using AirbnbDAL;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using AirbnbBL;
using System.Security.Claims;
using Microsoft.Extensions.FileProviders;


var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.AddCors(options =>
{
    options.AddPolicy(name: "AllowAny",
                      policy =>
                      {
                          policy
                          .AllowAnyOrigin()
                          .AllowAnyHeader()
                          .AllowAnyMethod();
                      });
});

#region Dependency Register

////DbContext
builder.Services.AddDbContext<AirbnbContext>(options => options
    .UseSqlServer(builder.Configuration.GetConnectionString("AirBnbDefaultDbConnection")));

////Repos
builder.Services.AddScoped<IAmenityRepo, AmenityRepo>();
builder.Services.AddScoped<IImageRepo, ImageRepo>();
builder.Services.AddScoped<IRuleRepo, RuleRepo>();
builder.Services.AddScoped<ICountryRepo, CountryRepo>();
builder.Services.AddScoped<ICityRepo, CityRepo>();
builder.Services.AddScoped<IPropertyRepo, PropertyRepo>();
builder.Services.AddScoped<IReservationRepo, ReservationRepo>();

////UnitOfWork
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

////Managers
builder.Services.AddScoped<IAmenityManager, AmenityManager>();
builder.Services.AddScoped<IImageManager, ImageManager>();
builder.Services.AddScoped<IRuleManager, RuleManager>();
builder.Services.AddScoped<ICountryManager, CountryManager>();
builder.Services.AddScoped<ICityManager, CityManager>();
builder.Services.AddScoped<IPropertyManager, PropertyManager>();
builder.Services.AddScoped<IReservationManager, ReservationManager>();
#endregion

#region Identity Register & Configs


builder.Services.AddIdentity<User, IdentityRole>(options =>
{
    options.Password.RequireUppercase = true;
    options.Password.RequireLowercase = true;
    options.Password.RequireNonAlphanumeric = true;
    options.Password.RequireDigit = true;
    options.Password.RequiredLength = 8;

    options.User.RequireUniqueEmail = true;

    options.Lockout.MaxFailedAccessAttempts = 3;
    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
}).AddEntityFrameworkStores<AirbnbContext>();
#endregion

#region Authentication Schemas
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = "AirbnbDefaultAuthSchema";
    options.DefaultChallengeScheme = "AirbnbDefaultAuthSchema";
}).AddJwtBearer("AirbnbDefaultAuthSchema", options =>
            {
                SecurityKey key = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(builder.Configuration.GetValue<string>("JwtSecretKey")));
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    IssuerSigningKey = key
                };
            });
#endregion

#region Authorization Policies
builder.Services.AddAuthorization(
    options => options.AddPolicy("Admin", policy =>
    policy.RequireClaim(ClaimTypes.Role, "Admin"))
    );
#endregion


var app = builder.Build();

#region Images Handling
var staticFilesPath = Path.Combine(Environment.CurrentDirectory, "Images");
//Configuration to let app use static files
app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(staticFilesPath),
    RequestPath = "/Images" //Localhost:5073/(Request Path)/Capture.PNG(Static File Name)

});
#endregion


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors("AllowAny");

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
