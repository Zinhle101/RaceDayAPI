
using Microsoft.AspNetCore.Authentication.Cookies;
using RaceDayMVC.Services;

var builder = WebApplication.CreateBuilder(args);

// Allows MVC to use controllers and views.
builder.Services.AddControllersWithViews();

// Allows the application to access the current user's information.
builder.Services.AddHttpContextAccessor();

// Adds session storage.
builder.Services.AddDistributedMemoryCache();

builder.Services.AddSession(options =>
{
    // The user stays logged in for up to 60 minutes without activity.
    options.IdleTimeout = TimeSpan.FromMinutes(60);

    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

// Sets up cookie authentication for logged-in users.
builder.Services.AddAuthentication(
    CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Account/Login";
        options.AccessDeniedPath = "/Home/AccessDenied";
        options.ExpireTimeSpan = TimeSpan.FromMinutes(60);
        options.SlidingExpiration = true;
    });

// Adds role-based access control.
builder.Services.AddAuthorization();

// Connects the MVC application to the Part 2 API.
builder.Services.AddHttpClient<ApiService>(client =>
{
    client.BaseAddress = new Uri(
        builder.Configuration["ApiSettings:BaseUrl"]!
    );
});

var app = builder.Build();

// Configures the website.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

// Session must be enabled before authentication.
app.UseSession();

app.UseAuthentication();
app.UseAuthorization();

// Sets the default page when the website opens.
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}"
);

app.Run();
