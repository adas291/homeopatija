using homeopatija;
using homeopatija.Entities;
using homeopatija.Repos;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using homeopatija.Data;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("AuthContextConnection") ?? throw new InvalidOperationException("Connection string 'AuthContextConnection' not found.");

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddLocalization();

builder.Services.AddAutoMapper(typeof(AutoMapperProfile));

builder.Services.AddDbContext<HomeopatijaContext>(options =>
{
  options.UseSqlite(builder.Configuration["ConnectionStrings:Default"]);
}, ServiceLifetime.Singleton);

builder.Services.AddDefaultIdentity<User>(options => options.SignIn.RequireConfirmedAccount = false)
        .AddEntityFrameworkStores<HomeopatijaContext>()
        .AddDefaultTokenProviders();

builder.Services.AddSingleton<EmailRepo>();
builder.Services.AddScoped<CommentRepo>();

builder.Logging.ClearProviders();
builder.Logging.AddConsole();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.

    app.UseHsts();
}

//app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();

app.Run();
