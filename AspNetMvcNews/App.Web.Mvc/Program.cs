using App.Data;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddSession();
builder.Services.AddDbContext<AppDbContext>(options =>
{
	string? AppDbStr = builder.Configuration.GetConnectionString("DbStr");
	options.UseSqlServer(AppDbStr);
});
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(x =>
{
	x.LoginPath = "/Auth/Login";
	x.LogoutPath = "/Auth/Logout";
	x.Cookie.Name = "Login";
	x.Cookie.MaxAge = TimeSpan.FromDays(1);
});
builder.Services.AddAuthorization(x =>
{
	x.AddPolicy("AdminPolicy", p => p.RequireClaim("Role", "Admin"));
	x.AddPolicy("ModeratorPolicy", p => p.RequireClaim("Role", "Moderator"));
	x.AddPolicy("UserPolicy", p => p.RequireClaim("Role", "Visitor"));
});
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
	app.UseExceptionHandler("/Home/Error");
	// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
	app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseSession();

app.UseAuthentication();
app.UseAuthorization();
app.MapControllerRoute(
    name: "NewsRoute",
    pattern: "News/{title}-{id}/{action=Detail}/",
	new {controller= "News", action= "Detail", title=""},
	new {id=@"^\d+$"}
	);
app.MapControllerRoute(
    name: "CategoryRoute",
    pattern: "Category/{title}-{id}/{action=Index}/",
    new { controller = "Category", action = "Index", title = "" },
    new {id=@"^\d+$"}
    );
app.MapControllerRoute(
    name: "PageRoute",
    pattern: "Page/{title}-{id}/{action=Detail}/",
    new { controller = "Page", action = "Detail", title = "" },
    new { id = @"^\d+$" }
    );
app.MapControllerRoute(
	name: "default",
	pattern: "{controller=Home}/{action=Index}/{id?}");

using (var scope = app.Services.CreateScope())
{
	var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();

	if (await dbContext.Database.EnsureCreatedAsync())
	{
		

		await DbSeeder.Seed(dbContext);
	}
}

app.Run();
