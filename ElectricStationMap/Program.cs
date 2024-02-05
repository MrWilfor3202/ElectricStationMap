using ElectricStationMap;
using ElectricStationMap.Models.EF;
using ElectricStationMap.Repository;
using ElectricStationMap.Repository.EF;
using ElectricStationMap.Services;
using ElectricStationMap.Services.Email;
using ElectricStationMap.Services.Guid;
using MailKitSimplified.Sender;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);


//DB
builder.Services.AddDbContext<ElectricStationMapDBContext>(options =>
                    options.UseLazyLoadingProxies()
                    .UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException()));

//Identity
builder.Services.AddIdentity<ApplicationUser, ApplicationRole>(
    options =>
    {
        options.SignIn.RequireConfirmedAccount = true;
    })
    .AddEntityFrameworkStores<ElectricStationMapDBContext>()
    .AddSignInManager()
    .AddDefaultTokenProviders();

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
builder.Services.AddTransient<IEmailSender, MailKitEmailSender>();


//Services
builder.Services.AddTransient<IActionContextAccessor, ActionContextAccessor>();
builder.Services.AddScoped<IRazorRenderService, RazorRenderService>();
builder.Services.AddSingleton<ISequentialGuidGenerator, CustomSequentialGuidGenerator>();
builder.Services.AddHttpContextAccessor();
builder.Services.AddMailKitSimplifiedEmailSender(builder.Configuration);

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

app.UseAuthentication();
app.UseAuthorization();

app.MapRazorPages();

app.Run();
