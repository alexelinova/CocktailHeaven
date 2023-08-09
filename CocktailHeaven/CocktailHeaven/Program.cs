using CocktailHeaven.Infrastructure.Data;
using CocktailHeaven.Infrastructure.Models.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);


var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<CocktailHeavenDbContext>(options =>
	options.UseSqlServer(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddDefaultIdentity<ApplicationUser>(options =>
{
	options.SignIn.RequireConfirmedAccount = false;
	options.Password.RequireNonAlphanumeric = false;
	options.Password.RequiredLength = 5;
	options.Password.RequireUppercase = false;
})
	.AddRoles<IdentityRole<Guid>>()
	.AddEntityFrameworkStores<CocktailHeavenDbContext>();

builder.Services.ConfigureApplicationCookie(options =>
{
      options.LoginPath = "/Account/Login";
});

builder.Services.AddControllersWithViews(options =>
{
	options.Filters.Add(new AutoValidateAntiforgeryTokenAttribute());
});

builder.Services.AddApplicationServices();

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


app.UseEndpoints(endpoints =>
{
	endpoints.MapControllerRoute(
		name: "areas",
		pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

	endpoints.MapControllerRoute(
		name: "default",
		pattern: "{controller=Home}/{action=Index}/{id?}");

	endpoints.MapRazorPages();
});

app.Run();
