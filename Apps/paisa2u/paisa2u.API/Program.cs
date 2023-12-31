//for logging added my Mohtashim on 18-Jun-2023
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using paisa2u.common.Models;
using paisa2u.common.Services;
using Serilog;
using System.Configuration;



var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<PaisaDbContext>(
       options => options.UseSqlServer(
           builder.Configuration.GetConnectionString("paisa2udb")));
builder.Services.AddCors(options =>
{
    options.AddPolicy("CorsApi", builder => builder.WithOrigins("https://localhost:7257/")
    .AllowAnyHeader()
    .AllowAnyMethod()
    
    );
});
 
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IRegUserService, RegUserService>();
builder.Services.AddScoped<IVendorService, VendorService>();
builder.Services.AddScoped<IVendorProductService, VendorProductService>();
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<IAddressService, AddressService>();
builder.Services.AddScoped<ISubscriptionService, SubscriptionService>();
//Added by Shazia on JUl 23- Aug 2, 2023 register services
builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<ITransactionsService, TransactionsService>();
builder.Services.AddScoped<ISubscriptionSetupService, SubscriptionSetupService>();


Log.Logger=new LoggerConfiguration().WriteTo.File("C:\\users\\smali\\Vendor_Projects\\DiscountApp\\Apps\\paisa2u\\Logs\\log*.log", rollingInterval:RollingInterval.Day).CreateLogger();
//for logging added my Mohtashim on 18-Jun-2023
var app = builder.Build();
//for ajax, cors call by mohtashim
app.Use((ctx, next) =>
{
    ctx.Response.Headers["Access-Control-Allow-Origin"] = "https://localhost:7257";
    ctx.Response.Headers["Access-Control-Allow-Headers"] = "Origin, X-Requested-With, Content-Type, Accept, Authorization";
    return next();
}
);
// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
    app.UseSwagger();
    app.UseSwaggerUI();
//}
app.UseHttpsRedirection();
app.UseRouting();

app.UseCors("CorsApi");
app.UseAuthorization();
app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers()
    .RequireCors("CorsApi");
});


app.MapControllers();

app.Run();
