using Knigochei.Services.BookService;
using Knigochei.Services.GenreService;
using Knigochei.UnitOfWorkDapper;
using Microsoft.Extensions.Configuration;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

var config = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();

// UOW Singleton
builder.Services.AddSingleton<IUnitOfWork, UnitOfWork>(uow => new UnitOfWork(config.GetConnectionString("MainConnection")));

// Services DI
builder.Services.AddSingleton<IBookService, BookService>();
builder.Services.AddSingleton<IGenreService, GenreService>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();


app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();