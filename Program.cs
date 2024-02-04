using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using VUA.Core.IRepositories;
using VUA.Core.IRepositories.IService;
using VUA.Core.Models;
using VUA.EF;
using VUA.EF.Repositories;
using VUA.EF.Repositories.Service;
using LiveChat.signalr.hubs;


var builder = WebApplication.CreateBuilder(args);
var _configuration = builder.Configuration;

builder.Services.AddAuthentication().AddGoogle(options =>
{
	options.ClientId = builder.Configuration["Authentication:Google:ClientId"]!;
	options.ClientSecret = builder.Configuration["Authentication:Google:ClientSecret"]!;
});


builder.Services.AddAuthentication(options =>
{
    options.DefaultScheme = IdentityConstants.ApplicationScheme;
    options.DefaultSignInScheme = IdentityConstants.ExternalScheme;
})
;

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
}, ServiceLifetime.Scoped);


builder.Services.AddIdentity<AppllicationUser, IdentityRole>().AddEntityFrameworkStores<AppDbContext>().AddDefaultTokenProviders();
builder.Services.AddTransient(typeof(IUserService), typeof(UserService));
builder.Services.AddTransient(typeof(IAccountRepositorory), typeof(AccountRepository));
builder.Services.AddTransient(typeof(IBaseRepository<>),typeof(BaseRepository<>));
builder.Services.AddTransient(typeof(IDashboardRipository), typeof(DashboardRipository));
builder.Services.AddTransient(typeof(IEmailService),typeof(EmailService));


builder.Services.AddRazorPages().AddMvcOptions(options => options.MaxModelValidationErrors = 50)
        .AddViewOptions(options => options.HtmlHelperOptions.ClientValidationEnabled = true);

builder.Services.Configure<IISServerOptions>(options =>
{
    options.MaxRequestBodySize = long.MaxValue; // Set to the maximum allowed value
});

builder.Services.AddDbContextFactory<AppDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
}, ServiceLifetime.Transient);
builder.Services.AddTransient<SMTPConfigration>();
builder.Services.Configure<IdentityOptions>(options =>
{
    options.SignIn.RequireConfirmedEmail = false;
    
});
builder.Services.Configure<SMTPConfigration>(builder.Configuration.GetSection("SMTPConfig"));
builder.Services.AddRazorPages();
builder.Services.AddSignalR();
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
app.UseAuthentication();
app.UseAuthorization();

app.MapHub<ChatHub>("/chatHub");
app.MapControllerRoute(
    name: "success",
    pattern: "Success",
    defaults: new {area = "AppUsers", controller = "Dashboard", action = "Success" });
app.MapControllerRoute(
    name: "areas",
    pattern: "{area:exists}/{controller=Dashboard}/{action=Index}/{id?}");
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();

app.Run();
