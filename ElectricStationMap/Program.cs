using ElectricStationMap;
using ElectricStationMap.Repository;
using ElectricStationMap.Repository.EF;
using ElectricStationMap.Services;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

//Razor pages.
builder.Services.AddRazorPages();

//DB
builder.Services.AddDbContext<ElectricStationMapDBContext>(options =>
                    options.UseLazyLoadingProxies()
                    .UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException()));


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
