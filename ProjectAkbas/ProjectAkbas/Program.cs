using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.Cookies;
using ProjectAkbas.Data;

var builder = WebApplication.CreateBuilder(args);

// Veritaban� ba�lant�lar�
builder.Services.AddDbContext<ApplicationDbContext1>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("AKBASLAR"))); // "AKBASLAR" ba�lant� dizesi kullan�lacak

builder.Services.AddDbContext<ApplicationDbContext2>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("Akb_Proforma_Test"))); // "Akb_Proforma_Test" ba�lant� dizesi kullan�lacak

builder.Services.AddDbContext<ApplicationDbContext3>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("AEP"))); // "AEP" ba�lant� dizesi kullan�lacak

// Kimlik do�rulama ve yetkilendirme
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Account/Login"; // Giri� yolu
        options.LogoutPath = "/Account/Logout"; // ��k�� yolu
        options.AccessDeniedPath = "/Account/AccessDenied"; // �ste�e ba�l�: Eri�im reddedildi yolu
    });

builder.Services.AddAuthorization(options =>
{
    // Burada yetkilendirme politikalar� ekleyebilirsiniz
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

// Kimlik do�rulama ve yetkilendirme middleware'ini ekle
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Account}/{action=Login}/{id?}");

app.Run();
