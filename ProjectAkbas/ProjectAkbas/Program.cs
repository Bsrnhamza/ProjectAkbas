using Microsoft.EntityFrameworkCore;
using ProjectAkbas.Data;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<ApplicationDbContext1>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("AKBASLAR"))); // "AKBASLAR" baðlantý dizesi kullanýlacak
builder.Services.AddDbContext<ApplicationDbContext2>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("Akb_Proforma_Test"))); // "Akb_Proforma_Test" baðlantý dizesi kullanýlacak
builder.Services.AddDbContext<ApplicationDbContext3>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("AEP"))); // "Mara" baðlantý dizesi kullanýlacak

builder.Services.AddControllersWithViews();

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

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=ProformaMaliyet}/{action=Index}/{id?}");

app.Run();
