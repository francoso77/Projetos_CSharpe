using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using AppSales.Data;
using AppSales.Services;
using System.Globalization;
using Microsoft.AspNetCore.Localization;


var builder = WebApplication.CreateBuilder(args);
//builder.Services.AddDbContext<AppSalesContext>(options =>
//    options.UseSqlServer(builder.Configuration.GetConnectionString("AppSalesContext") ?? throw new InvalidOperationException("Connection string 'AppSalesContext' not found.")));


//criando conexão como Mysql
builder.Services.AddDbContext<AppSalesContext>(options =>
    options.UseMySql("server=localhost;initial catalog=CRUD_MVC_MYSQL;uid=root;pwd=1234567",
        Microsoft.EntityFrameworkCore.ServerVersion.Parse("8.0.32-mysql")));


// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddScoped<SeedingService>(); //servico para pupular o banco de dados
builder.Services.AddScoped<SellerServices>();
builder.Services.AddScoped<DepartmentService>();

var app = builder.Build();

//definindo configurações de localização para valores e datas
CultureInfo enUS = new CultureInfo("pt-BR");
var localizationOptions = new RequestLocalizationOptions
{
    DefaultRequestCulture = new RequestCulture(enUS),
    SupportedCultures = new List<CultureInfo> { enUS },
    SupportedUICultures = new List<CultureInfo> { enUS }
};

app.UseRequestLocalization(localizationOptions);
//app.Services.GetService<SeedingService>();
// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    //sr.Seed();
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
  
}else
{
   // sr.Seed();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
