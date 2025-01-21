using Microsoft.EntityFrameworkCore;
using NLog.Web;
using OurShop.MiddleWares;
using Repositories;
using Services;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddControllers();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IProductrepository, ProductRepository>();
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<IOrderRepository, OrderRepository>();
builder.Services.AddScoped<IOrderService, OrderService>();
builder.Services.AddScoped<IRatingRepository, RatingRepository>();
builder.Services.AddScoped<IRatingService, RatingService>();

builder.Services.AddDbContext<_328177589ShopApiContext>(options => options.UseSqlServer("Server=DESKTOP-E0FAPSB\\SQLEXPRESS;Database=MyShop_Api; Trusted_Connection=True; TrustServerCertificate=True"));
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Host.UseNLog();

var app = builder.Build();

app.UseHandleErrorMiddleWare();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseAuthorization();

app.UseRatingMiddleWare();


app.MapControllers();

app.Run();
