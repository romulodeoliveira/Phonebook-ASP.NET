using Microsoft.EntityFrameworkCore;
using ContactRegister.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

string mySqlConnection = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContextPool<DataBaseContext>(options => 
                options.UseMySql(mySqlConnection, 
                    ServerVersion.AutoDetect(mySqlConnection)));

builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
