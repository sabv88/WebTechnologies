using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using WebTechnologies.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.builder.Services.AddDbContext<ApplicationDbContext>(options =>
builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
	options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});
builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = $"/Identity/Account/Login";
    options.LogoutPath = $"/Identity/Account/Logout";
});
builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = false)
	.AddRoles<IdentityRole>().AddEntityFrameworkStores<ApplicationDbContext>();
builder.Services.AddControllersWithViews();



var app = builder.Build();

var serviceProvider = builder.Services.BuildServiceProvider();
var context = serviceProvider.GetService<ApplicationDbContext>();
var userManager = serviceProvider.GetService<UserManager<IdentityUser>>();
var roleManager = serviceProvider.GetService<RoleManager<IdentityRole>>();

DbInitializer.Seed(context, userManager, roleManager)
 .GetAwaiter()
 .GetResult();
// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseStaticFiles();
app.MapRazorPages();
app.UseRouting();
app.UseAuthentication();;

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
