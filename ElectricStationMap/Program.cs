using ElectricStationMap;
using ElectricStationMap.Repository;
using ElectricStationMap.Repository.EF;
using ElectricStationMap.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);


//DB
builder.Services.AddDbContext<ElectricStationMapDBContext>(options =>
                    options.UseLazyLoadingProxies()
                    .UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException()));

//Identity
builder.Services.AddIdentity<IdentityUser, IdentityRole>(
    options => 
    {
        options.SignIn.RequireConfirmedAccount = false;
    })
    .AddEntityFrameworkStores<ElectricStationMapDBContext>();

builder.Services.ConfigureApplicationCookie(options =>
{
    options.Cookie.HttpOnly = true;
    options.ExpireTimeSpan = TimeSpan.FromMinutes(5);
    options.LoginPath = "/Identity/Account/Login";
    options.LogoutPath = "/Identity/Account/Logout";
    options.AccessDeniedPath= "/Identity/Account/AccessDenied";
});


//Razor pages.
builder.Services.AddRazorPages();

//Repositories
builder.Services.AddTransient(typeof(IGenericRepositoryAsync<>), typeof(GenericRepositoryAsync<>));
builder.Services.AddTransient<IRequestRepositoryAsync, RequestInfoRepositoryAsync>();
builder.Services.AddTransient<IIconRepositoryAsync, IconRepositoryAsync>();
builder.Services.AddTransient<IRequirementRepositoryAsync, RequirementRepositoryAsync>();
builder.Services.AddTransient<IUnitOfWork, UnitOfWork>();


//Services
builder.Services.AddTransient<IActionContextAccessor, ActionContextAccessor>();
builder.Services.AddScoped<IRazorRenderService, RazorRenderService>();
builder.Services.AddHttpContextAccessor();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.Run();
