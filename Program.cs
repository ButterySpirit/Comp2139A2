using Assign1.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Assign1.Services;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;
using Assign1.Models;
using Assign1.Services.Filters;
using Assign1.Services.Middleware;

var builder = WebApplication.CreateBuilder(args);

// Database and Identity configuration
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseMySql(
        builder.Configuration.GetConnectionString("DefaultConnection"),
        ServerVersion.AutoDetect(builder.Configuration.GetConnectionString("DefaultConnection")),
        mySqlOptions => mySqlOptions.SchemaBehavior(MySqlSchemaBehavior.Ignore)
    )
);
builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options =>
{
    options.SignIn.RequireConfirmedAccount = true;
})
.AddEntityFrameworkStores<ApplicationDbContext>()
.AddDefaultUI()
.AddDefaultTokenProviders();

// Filters and services
builder.Services.AddScoped<LogUserActivityFilter>();
builder.Services.AddControllersWithViews(options =>
{
    options.Filters.AddService<LogUserActivityFilter>();
});
builder.Services.AddRazorPages();
builder.Services.AddSingleton<IEmailSender, EmailSender>();
builder.Services.AddAuthorization();
builder.Logging.AddConsole();

var app = builder.Build();

// Error handling middleware
app.UseMiddleware<ErrorHandlingMiddleware>();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

// Static files and routing
app.UseStaticFiles();
app.UseRouting();

// Request logging middleware
app.UseMiddleware<RequestResponseLoggingMiddleware>();

// Authentication and authorization
app.UseAuthentication();
app.UseAuthorization();

// Endpoints
app.MapRazorPages();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapControllerRoute(
    name: "areas",
    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

app.Run();
