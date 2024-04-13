using Assign1.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Assign1.Services;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;
using Assign1.Models;
using Assign1.Services.Filters;
using Assign1.Services.Middleware;
using System;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

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

// Middleware for handling exceptions, this is only added in non-development environments
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}
else
{
    app.UseDeveloperExceptionPage();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

// Endpoint configuration
app.UseEndpoints(endpoints =>
{
    endpoints.MapRazorPages();
    endpoints.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}");
});

// Handle 404 - NotFound
app.Use(async (context, next) =>
{
    await next();

    if (context.Response.StatusCode == 404 && !context.Response.HasStarted)
    {
        // Set the original path in the request features so it can be used in the error handler
        context.Features.Set<IStatusCodeReExecuteFeature>(new StatusCodeReExecuteFeature()
        {
            OriginalPath = context.Request.Path
        });

        // Re-execute the request so the error handler can generate the response
        context.Request.Path = "/Home/ErrorWithCode";
        await next();
    }
});

app.Run();
