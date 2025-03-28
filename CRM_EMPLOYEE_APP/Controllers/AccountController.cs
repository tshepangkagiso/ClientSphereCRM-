using CRM_EMPLOYEE_APP.Http.Interfaces;
using CRM_EMPLOYEE_APP.Models;
using CRM_EMPLOYEE_APP.Models.DTOs;
using CRM_EMPLOYEE_APP.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace CRM_EMPLOYEE_APP.Controllers
{
    public class AccountController : Controller
    {
        [BindProperty]
        public AuthUserDTO user { get; set; } = new AuthUserDTO();

        private readonly IEmployeeWebExecutor webExecutor;

        public AccountController(IEmployeeWebExecutor webExecutor)
        {
            this.webExecutor = webExecutor;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpGet]
        public IActionResult AccessDenied()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> LoginPost()
        {
            try
            {
                if (!ModelState.IsValid) return View(ModelState);

                if (user == null)
                {
                    ModelState.AddModelError("Login", "Please fill all fields.");
                    return View(ModelState);
                }
                AuthUser _user = new AuthUser()
                {
                    UserName = user.UserName,
                    Password = user.Password,
                };

                var response = await this.webExecutor.Login<LoginResponse>(_user);
                if (response == null)
                {
                    ModelState.AddModelError("Login", "Failed to login.");
                    return View(ModelState);
                }
                else
                {
                    ClaimsPrincipal claimsPrincipal = LoginService.SignInEmployee(_user.UserName, response);

                    if (claimsPrincipal == null)
                    {
                        ModelState.AddModelError("Login", "Sign in of user faild.");
                        return View(ModelState);
                    }

                    var properties = new AuthenticationProperties
                    {
                        IsPersistent = user.RememberMe,
                        ExpiresUtc = DateTime.UtcNow.AddDays(1)
                    };
                    await HttpContext.SignInAsync("MyCookieAuth", claimsPrincipal, properties);
                    HttpContext.Session.SetString("JwtToken", response.Token);

                    return RedirectToAction("Index", "Home");

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return RedirectToAction("Error", "Home");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            try
            {
                await HttpContext.SignOutAsync("MyCookieAuth");
                return RedirectToAction("Index", "Home");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return RedirectToAction("Error", "Home");
            }
        }

    }
}
