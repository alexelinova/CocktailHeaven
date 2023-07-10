using CocktailHeaven.Core;
using CocktailHeaven.Core.Contracts;
using CocktailHeaven.Infrastructure.Data;
using CocktailHeaven.Infrastructure.Data.Common;
using CocktailHeaven.Infrastructure.Models.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);


var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<CocktailHeavenDbContext>(options =>
	options.UseSqlServer(connectionString));

builder.Services.AddDefaultIdentity<ApplicationUser>(options =>
{
	options.SignIn.RequireConfirmedAccount = false;
	options.Password.RequireNonAlphanumeric = false;
	options.Password.RequiredLength = 5;
	options.Password.RequireUppercase = false;
})
	.AddEntityFrameworkStores<CocktailHeavenDbContext>();

builder.Services.ConfigureApplicationCookie(options =>
{
      options.LoginPath = "/Account/Login";
});

builder.Services.AddControllersWithViews();

builder.Services.AddScoped<IRepository, CocktailHeavenRepository>();
builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<ICocktailService, CocktailService>();
builder.Services.AddScoped<IAccountService, AccountService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
	app.UseDeveloperExceptionPage();
	app.UseMigrationsEndPoint();
}
else
{
	app.UseExceptionHandler("/Home/Error");

	app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapDefaultControllerRoute();
app.MapRazorPages();

app.Run();
