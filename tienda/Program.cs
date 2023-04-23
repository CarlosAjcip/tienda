using tienda.Interface;
using tienda.Servicios;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddTransient<IRepositorioFabricante, RepositorioFabricante>();
builder.Services.AddTransient<IRepositorioProducto, RepositorioProducto>();
builder.Services.AddAutoMapper(typeof(Program));
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
