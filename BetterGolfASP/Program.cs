using Microsoft.EntityFrameworkCore;
using BetterGolfASP.Infrastructure.DB;
using BetterGolfASP.Application.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

//DBContext
var connection = builder.Environment.IsDevelopment()
    ? builder.Configuration.GetConnectionString("DefaultConnection")
    : Environment.GetEnvironmentVariable("DefaultConnection");

builder.Services.AddDbContext<Context>(options =>
    options.UseSqlServer(connection));


builder.Services.AddDistributedMemoryCache();


builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

//Services
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddScoped<ShoppingCartService>();
builder.Services.AddScoped<ProductService>();
builder.Services.AddScoped<AdminService>();
builder.Services.AddScoped<CheckOutService>();
builder.Services.AddOpenApi();

var app = builder.Build();


if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();


app.UseSession();
app.UseAuthorization();


app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/openapi/v1.json", "v1");
    });
}
//using (var scope = app.Services.CreateScope())
//{
//    var context = scope.ServiceProvider.GetRequiredService<Context>();
//    Seed seed = new Seed();
//    seed.SeedDB(context);
//}
app.Run();


