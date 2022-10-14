using ApplicationCore.Context;
using AutoMapper.Features;
using Google.Apis.Auth.AspNetCore3;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using Service;
using Service.Helper;
using FoodyAPI.Helper.Azure.Blob;
using FoodyAPI.Helper.Azure.IBlob;
using Service.Services.IService;
using Service.Services.Service;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddMvc();
//trungnt 10-10-2022 add start
//add auth google
builder.Services.AddAuthentication(o =>
{
    // This forces challenge results to be handled by Google OpenID Handler, so there's no
    // need to add an AccountController that emits challenges for Login.
    o.DefaultChallengeScheme = GoogleOpenIdConnectDefaults.AuthenticationScheme;
    // This forces forbid results to be handled by Google OpenID Handler, which checks if
    // extra scopes are required and does automatic incremental auth.
    o.DefaultForbidScheme = GoogleOpenIdConnectDefaults.AuthenticationScheme;
    // Default scheme that will handle everything else.
    // Once a user is authenticated, the OAuth2 token info is stored in cookies.
    o.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
})
        .AddCookie()
        .AddGoogleOpenIdConnect(options =>
        {
            options.ClientId = Constants.GOOGLE_CLIENT_ID;
            options.ClientSecret = Constants.GOOGLE_CLIENT_SECRET;
        }
);
//trungnt 10-10-2022 add end
//trungnt 10-10-2022 add start
//add session services
builder.Services.AddDistributedMemoryCache();

builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(120);
    options.Cookie.HttpOnly = true;
    //options.Cookie.IsEssential = true;
});
//trungnt 10-10-2022 add end
//trungnt 11-10-2022 add start
//enable cors for all
var allowedOrigin = "AllowedOrigin";
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: allowedOrigin,
                      policy =>
                      {
                          policy
                        .AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader();
                      });
});
//trungnt 11-10-2022 add end
using (var config = builder.Configuration)
{
    string connectionString = config.GetConnectionString("DefaultConnection");
    //builder.Services.AddDbContext<FoodyContext>(options => options.UseLazyLoadingProxies().UseSqlServer(connectionString)) ;
    builder.Services.AddDbContext<FoodyContext>(options => options.UseSqlServer(connectionString));
    builder.Services.AddAutoMapper(typeof(Mapping));
    builder.Services.AddControllers().AddNewtonsoftJson(options =>
    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Error
    );
}
//trungnt 10-10-2022 add start
//add transient of services
builder.Services.AddTransient<IUserService, UserService>();
builder.Services.AddTransient<ICategoryService, CategoryService>();
//builder.Services.AddTransient<ILocationService, ILocationService>();
builder.Services.AddTransient<IMenuDetailService, MenuDetailService>();
builder.Services.AddTransient<IMenuService, MenuService>();
builder.Services.AddTransient<IProductService, ProductService>();
builder.Services.AddTransient<IRoleService, RoleService>();
//builder.Services.AddTransient<IShipperService, ShipperService>();
builder.Services.AddTransient<IStoreService, StoreService>();

//trungnt 10-10-2022 add end

//trungnt 14-10-2022 add start
//add transient of Product Blob
builder.Services.AddTransient<IProductBlob, ProductBlob>();
//trungnt 14-10-2022 add end

//anhtn 10-10-2022 add start
builder.Services.AddTransient<ILocationService, LocationService>();

//anhtn 10-10-2022 add end

//builder.Services.AddScoped<IProductService, ProductService>();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment() || app.Environment.IsProduction())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseHttpsRedirection();

app.UseAuthorization();
//trungnt 11-10-2022 add start
//enable and use cors for all client
app.UseCors(allowedOrigin);
//trungnt 11-10-2022 add end
//trungnt 09-10-2022 add start
//for google authentication
app.UseAuthentication();
//trungnt 09-10-2022 add end
//trungnt 10-10-2022 add start
//add session service
app.UseSession();
//trungnt 10-10-2022 add end

app.MapControllers();

app.Run();
