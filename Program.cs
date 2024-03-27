using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using LoanBook.Data;
using LoanBook.Models;
using Microsoft.Extensions.Options;
using Microsoft.CodeAnalysis.Options;
using LoanBook.Components;
using LoanBook.Services;
// using Microsoft.AspNetCore.Components.QuickGrid.EntityFrameworkAdapter;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlite(connectionString));

// builder.Services.AddDefaultIdentity<XtendUser>(options => options.SignIn.RequireConfirmedAccount = true).AddEntityFrameworkStores<ApplicationDbContext>();
builder.Services.AddDatabaseDeveloperPageExceptionFilter();
builder.Services
.AddRazorComponents()
.AddInteractiveServerComponents()
.AddCircuitOptions(options => options.DetailedErrors = true);
// builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
//     .AddEntityFrameworkStores<ApplicationDbContext>();
builder.Services.AddIdentity<XtendUser, IdentityRole>(
    Options => {
        Options.Stores.MaxLengthForKeys = 128;
    }
)
.AddEntityFrameworkStores<ApplicationDbContext>()
.AddDefaultUI()
.AddDefaultTokenProviders();

builder.Services.AddControllersWithViews();
builder.Services.AddScoped<BookServices>();
// builder.Services.AddQuickGridEntityFrameworkAdapter();

builder.Services.AddControllers().AddNewtonsoftJson(options =>
  options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseAntiforgery();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();

// using (var scope = app.Services.CreateScope()) {
//     var services = scope.ServiceProvider;

//     var context = services.GetRequiredService<ApplicationDbContext>();    
//     context.Database.Migrate();

//     var userMgr = services.GetRequiredService<UserManager<XtendUser>>();  
//     var roleMgr = services.GetRequiredService<RoleManager<IdentityRole>>();  

//     IdentitySeedData.Initialize(context, userMgr, roleMgr).Wait();
// }
// app.MapRazorComponents<App>()
// .AddInteractiveServerRenderMode();

app.Run();
