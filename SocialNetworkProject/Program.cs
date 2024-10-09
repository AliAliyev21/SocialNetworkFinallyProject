using Microsoft.EntityFrameworkCore;
using SocialNetworkProject.Data;
using SocialNetworkProject.Hubs;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddHttpClient();
builder.Services.AddSignalR();

// Configure the database connection
builder.Services.AddDbContext<SocialNetworkDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Add authentication
builder.Services.AddAuthentication("YourCookieScheme")
    .AddCookie("YourCookieScheme", options =>
    {
        options.LoginPath = "/Account/Login";
        options.LogoutPath = "/Account/Logout";
    });

// Build the application
var app = builder.Build();

// Apply database migrations
using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<SocialNetworkDbContext>();
    dbContext.Database.Migrate(); // Applies any pending migrations
}

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

// Enable CORS
app.UseCors("corsapp");

app.UseAuthentication(); // Make sure to add this line
app.UseAuthorization();

// Configure endpoint routing
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");


app.Run();
