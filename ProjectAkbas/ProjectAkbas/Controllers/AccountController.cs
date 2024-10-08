﻿using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using ProjectAkbas.Data;

namespace ProjectAkbas.Controllers
{
    public class AccountController : Controller
    {
        private readonly ApplicationDbContext2 _context2;

        public AccountController(ApplicationDbContext2 context2)
        {
            _context2 = context2;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View(); // Login formunu gösterir
        }

        [HttpPost]
        public async Task<IActionResult> Login(string username, string password, bool rememberMe)
        {
            // Kullanıcı adı ve şifre doğrulaması yap
            var user = _context2.Users.SingleOrDefault(u => u.Username == username && u.Password == password);

            if (user == null)
            {
                // Geçersiz kullanıcı adı veya şifre
                ModelState.AddModelError("", "Geçersiz kullanıcı adı veya şifre");
                return View(); // Login formunu tekrar göster
            }

            // Kullanıcı geçerli ise, kullanıcı bilgilerini ve rolünü ayarla
            var claims = new[]
            {
            new Claim(ClaimTypes.Name, username),
            new Claim(ClaimTypes.Role, user.Role)
        };

            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

            var authProperties = new AuthenticationProperties
            {
                IsPersistent = rememberMe // Oturumun hatırlanması seçeneği
            };

            // Kullanıcıyı oturum açtır
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimsIdentity), authProperties);

            // Başarılı girişten sonra Fabrics/Index sayfasına yönlendir
            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            // Kullanıcıyı oturumdan çıkar
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            // Çıkış işleminden sonra Login sayfasına yönlendir
            return RedirectToAction("Login");
        }

        // Kullanıcıların oturum açıp açmadığını kontrol etme
        private bool IsUserAuthenticated()
        {
            return User.Identity.IsAuthenticated;
        }
    }
}
