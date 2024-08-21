using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.Cookies;
using ProjectAkbas.Data;

var builder = WebApplication.CreateBuilder(args);

// Veritabaný baðlantýlarý
builder.Services.AddDbContext<ApplicationDbContext1>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("AKBASLAR"))); // "AKBASLAR" baðlantý dizesi kullanýlacak

builder.Services.AddDbContext<ApplicationDbContext2>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("Akb_Proforma_Test"))); // "Akb_Proforma_Test" baðlantý dizesi kullanýlacak

builder.Services.AddDbContext<ApplicationDbContext3>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("AEP"))); // "AEP" baðlantý dizesi kullanýlacak

// Kimlik doðrulama ve yetkilendirme
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Account/Login"; // Giriþ yolu
        options.LogoutPath = "/Account/Logout"; // Çýkýþ yolu
        options.AccessDeniedPath = "/Account/AccessDenied"; // Ýsteðe baðlý: Eriþim reddedildi yolu
    });

builder.Services.AddAuthorization(options =>
{
    // Burada yetkilendirme politikalarý ekleyebilirsiniz
    // options.AddPolicy("AdminOnly", policy => policy.RequireRole("Admin"));
});

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

// Kimlik doðrulama ve yetkilendirme middleware'ini ekle
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Account}/{action=Login}/{id?}");

app.Run();
