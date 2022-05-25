using Knigochei.Services.BookService;
using Knigochei.Services.GenreService;
using Knigochei.Services.UserService;
using Knigochei.UnitOfWorkDapper;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.Extensions.Configuration;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

//var config = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
var connectionString = builder.Configuration.GetConnectionString("MainConnection");
Console.WriteLine($"Connection String: {connectionString}");

// UOW Singleton
builder.Services.AddSingleton<IUnitOfWork, UnitOfWork>(uow => new UnitOfWork(connectionString));

// Services DI
builder.Services.AddSingleton<IBookService, BookService>();
builder.Services.AddSingleton<IGenreService, GenreService>();
builder.Services.AddSingleton<IUserService, UserService>();

builder.Services.AddCookiePolicy(options =>
{
    options.MinimumSameSitePolicy = SameSiteMode.None;
});


builder.Services.ConfigureApplicationCookie(options =>
  {
      // Cookie settings
      options.Cookie.Name = ".AspNetCore.Cookies";
      options.Cookie.HttpOnly = true;

      options.LoginPath = "/Account/Login";
      options.LogoutPath = "/Account/Logout";
      options.AccessDeniedPath = "/Account/AccessDenied";

  }
);

//builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie();
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(
    CookieAuthenticationDefaults.AuthenticationScheme,
    options =>
    {
        options.ExpireTimeSpan = TimeSpan.FromMinutes(15);
        options.SlidingExpiration = true;

    }
    
);


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
} else
{
    app.UseDeveloperExceptionPage();
}

app.UseHttpsRedirection();


app.UseStaticFiles();

app.UseRouting();

app.UseCookiePolicy();
app.UseAuthentication();
app.UseAuthorization();


app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();