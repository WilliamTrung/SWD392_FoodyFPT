using ApplicationCore.Context;
using Microsoft.EntityFrameworkCore;
using Service;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddMvc();

using (var config = builder.Configuration)
{
    string connectionString = config.GetConnectionString("DefaultConnection");
    //builder.Services.AddDbContext<FoodyContext>(options => options.UseLazyLoadingProxies().UseSqlServer(connectionString)) ;
    builder.Services.AddDbContext<FoodyContext>(options => options.UseSqlServer(connectionString));
    builder.Services.AddAutoMapper(typeof(Mapping));
    //builder.Services.AddControllers().AddNewtonsoftJson(options =>
    //options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
    //);
}
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

app.MapControllers();

app.Run();
