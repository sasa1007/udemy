using Microsoft.EntityFrameworkCore;
using Udemy.DataAccess.Data;
using udemy.Udemy.DataAccess.Repository;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Stripe;
using udemy.Udemy.Utility;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<AplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
);

builder.Services.Configure<StripeSettings>(builder.Configuration.GetSection("Stripe"));

builder.Services.AddIdentity<IdentityUser, IdentityRole>().AddEntityFrameworkStores<AplicationDbContext>()
    .AddDefaultTokenProviders();

builder.Services.ConfigureApplicationCookie(options =>
    {
        options.LoginPath = $"/Identity/Account/Login";
        options.LogoutPath = $"/Identity/Account/Logout";
        options.AccessDeniedPath = $"/Identity/Account/AccessDenied";
    }
);
builder.Services.AddRazorPages();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IEmailSender, EmailSender>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();
StripeConfiguration.ApiKey = builder.Configuration.GetSection("Stripe:SecretKey").Get<string>();
app.UseAuthentication();
app.UseAuthorization();
app.MapRazorPages();


//net 9
// app.MapStaticAssets();
//
//
// //when you go to "/"
// //he will get HOMEController
// //and find index anction
// //in index action he will return onlu return View();
// //so he will try to find in views folder with name as controller {home} some index.cshtml (same name as action)
// app.MapControllerRoute(
//         name: "default",
//         pattern: "{controller=Home}/{action=Index}/{id?}")
//     .WithStaticAssets();
//net 9


app.UseStaticFiles();

// Serve static files from custom folders, if needed
// app.UseStaticFiles(new StaticFileOptions
// {
//     FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), "YourStaticFilesFolder")),
//     RequestPath = "/YourStaticPath"
// });

// Configure routes
app.MapControllerRoute(
    name: "default",
    pattern: "{area=Customer}/{controller=Home}/{action=Index}/{id?}");

app.Run();