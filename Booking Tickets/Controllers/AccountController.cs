using Booking_Tickets.Data;
using Booking_Tickets.Data.Entites;
using Booking_Tickets.Helper;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Booking_Tickets.ViewModels.Account;

namespace Booking_Tickets.Controllers
{
    public class AccountController : Controller
    {
        private ApplicationDbContext _context;

        public AccountController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Register(ResgisterModelView model)
        {
            if (ModelState.IsValid is false)
            {
                return View(model);
            }

            var foundEmployee = _context.Users.FirstOrDefault(x => x.Phone == model.Phone);
            if (foundEmployee != null)
            {
                ModelState.AddModelError("Phone", "There is already an User with the same phone number");
                return View(model);
            }
            var user = new User
            {
                Username=model.Username,
                Phone = model.Phone,
                FullName = model.FullName,
                Password = HashPassword.Hash(model.Password),
                Role = Role.UserRole.User,
                CreatedAt=DateTime.UtcNow,
                
            };

            _context.Users.Add(user);
            _context.SaveChanges();
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginModelView request)
        {

            if (ModelState.IsValid is false)
            {
                return View(request);
            }
            var user = _context.Users.FirstOrDefault(x => x.Password == HashPassword.Hash(request.Password) && x.Phone == request.Phone);

            if (user is null)
            {
                ModelState.AddModelError("", "Phone Number or Password is Wrong. Please Try Again!");
                return View(request);
            }

            // data in cookie 
            List<Claim> claims = [
                new Claim(ClaimTypes.Role,user.Role.ToString()),
                new Claim(ClaimTypes.Sid,user.ID.ToString()),
                new Claim(ClaimTypes.MobilePhone,user.Phone),
                new Claim(ClaimTypes.Name,user.FullName),
                ];

            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var princiable = new ClaimsPrincipal(identity);
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, princiable);
            return RedirectToAction("Index", "Home");
        }
    }
}
