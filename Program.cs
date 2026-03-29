using Microsoft.EntityFrameworkCore;
using MvcApp.Data;
using MvcApp.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// Регистрация контекста базы данных
builder.Services.AddDbContext<AppDbContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
.LogTo(Console.WriteLine, LogLevel.Information) // Логирование SQL
.EnableSensitiveDataLogging() // Показывать параметры
);

//Регистрируем репозиторий 
// AddScoped означает, что один экземпляр репозитория будет создан на каждый HTTP-запрос.
// Это оптимально для веб-приложений.
builder.Services.AddScoped<IProductRepository, EfProductRepository>();
builder.Services.AddScoped<ITaskRepository, EfTaskRepository>();

//Сборка приложения
var app = builder.Build();

//Инициализация базы данных тестовыми данными
using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    await SeedData.InitializeAsync(dbContext);
}

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

//Кастомный маршрут 1
app.MapControllerRoute(
    name: "about",
    pattern: "about-us",
    defaults: new { controller = "Home", action = "Privacy" });

//Кастомный маршрут 2
app.MapControllerRoute(
    name: "userProfile",
    pattern: "user/{username}/{action=Profile}",
    defaults: new { controller = "Demo" });

//Кастомный маршрут 3
app.MapControllerRoute(
    name: "product",
    pattern: "product/{id:int}",
    defaults: new { controller = "Demo", action = "ProductDetails" });

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
