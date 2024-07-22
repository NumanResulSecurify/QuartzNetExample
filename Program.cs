using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Quartz;
using Quartzmin;
using QuartzNetExample.Configurations;
using QuartzNetExample.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// Quartz.NET konfigürasyonunu ekleyin
builder.Services.AddQuartzServices();
// Quartzmin konfigürasyonunu ekleyin
builder.Services.AddQuartzmin();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();

// Quartzmin middleware'ini ekleyin
app.UseQuartzmin(new QuartzminOptions
{
    Scheduler = builder.Services.BuildServiceProvider().GetRequiredService<ISchedulerFactory>().GetScheduler().Result
});

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
