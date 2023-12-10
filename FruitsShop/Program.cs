using FruitsShop.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using Microsoft.AspNetCore.Mvc.Razor.RuntimeCompilation;

var builder = WebApplication.CreateBuilder(args);
var conection = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<DataContext>(options => options.UseSqlServer(conection));
// Add services to the container.
builder.Services.AddControllersWithViews().AddRazorRuntimeCompilation();

builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(cfg => {                    // Đăng ký dịch vụ Session
	cfg.Cookie.Name = "context";             // Đặt tên Session - tên này sử dụng ở Browser (Cookie)
	cfg.IdleTimeout = new TimeSpan(0, 30, 0);    // Thời gian tồn tại của Session
});
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseSession();

app.UseRouting();

app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
	endpoints.MapControllerRoute(
	  name: "areas",
	  pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
	);
});

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
