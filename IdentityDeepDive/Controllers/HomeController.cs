using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using IdentityDeepDive.Models;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;

namespace IdentityDeepDive.Controllers
{
    public class HomeController : Controller
    {
        private readonly UserManager<PluralSightUser> userManager;

        private IUserClaimsPrincipalFactory<PluralSightUser> claimsPrincipalFactory;
        public HomeController(UserManager<PluralSightUser> userManager , IUserClaimsPrincipalFactory<PluralSightUser> claimsPrincipalFactory)
        {
            this.claimsPrincipalFactory = claimsPrincipalFactory;
            this.userManager = userManager;

        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterModel registerModel)
        {
            if (ModelState.IsValid)
            {
                var user = await userManager.FindByNameAsync(registerModel.UserName);

                if (user == null)
                {
                    user = new PluralSightUser
                    {
                        Id = Guid.NewGuid().ToString(),
                        UserName = registerModel.UserName,
                        Email = registerModel.Email
                    };



                    var result = await userManager.CreateAsync(user, registerModel.Password);

                    if(result.Succeeded)
                    {
                        var token = await userManager.GenerateEmailConfirmationTokenAsync(user);
                        var confirmationUrl = Url.Action("ConfirmEmail", "Home", new { token = token, email = user.Email }, Request.Scheme);
                        System.IO.File.WriteAllText("confirmationemail.txt", confirmationUrl);
                        return View("success");
                    }
                    else
                    {
                        foreach(var error in result.Errors)
                        {
                            ModelState.AddModelError("", error.Description);
                        }
                    }
                    
                }

               
            }
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> ConfirmEmail(string email,  string token)
        {
            var user = await userManager.FindByEmailAsync(email);
            if(user!=null)
            {
                var result = await userManager.ConfirmEmailAsync(user, token);

                if(result.Succeeded)
                {
                    return View("Success");
                }
            }
            return View("Error");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginModel login)
        {
            if(ModelState.IsValid)
            {
                var user = await userManager.FindByNameAsync(login.UserName);

                if(user!=null && !await userManager.IsLockedOutAsync(user))
                {
                    if (await userManager.CheckPasswordAsync(user, login.Password))
                    {
                        //var identity = new ClaimsIdentity("Identity.Application");

                        //identity.AddClaim(new Claim(ClaimTypes.NameIdentifier, user.Id));
                        //identity.AddClaim(new Claim(ClaimTypes.Name, user.UserName));
                        if (!await userManager.IsEmailConfirmedAsync(user))
                        {
                            ModelState.AddModelError("", "Email is not confirmed");
                            return View();
                        }
                        await userManager.ResetAccessFailedCountAsync(user);


                        var principal = await claimsPrincipalFactory.CreateAsync(user);



                        await HttpContext.SignInAsync("Identity.Application", new ClaimsPrincipal(principal));
                       return RedirectToAction("Index");
                    }
                    await userManager.AccessFailedAsync(user);
                }
                if(await userManager.IsLockedOutAsync(user))
                {
                   
                    return View("Lockedoutmessage");
                }



                ModelState.AddModelError("", "Username or password didnot match");
            }

            return View();
        }


        [HttpGet]
    
        public IActionResult ForgotPassword()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ForgotPassword(ForgotPasswordModel model)
        {
           if(ModelState.IsValid)
            {
                var user = await userManager.FindByEmailAsync(model.Email);

                if(user!=null)
                {
                    var token = await userManager.GeneratePasswordResetTokenAsync(user);
                    var resetUrl = Url.Action("ResetPassword", "Home", new { Token = token, email = user.Email }, Request.Scheme);
                    System.IO.File.WriteAllText("resetLink.txt", resetUrl);


                }
                else
                {
                    //email user and informthem they do not have an acoount
                }
                return View("Success");

            }
            return View();
        }

        [HttpGet]
        public IActionResult ResetPassword(string Token, string email)
        {
            return View(new ResetPasswordModel { Token = Token, Email = email });
        }

        [HttpPost]
        public async Task<IActionResult> ResetPassword(ResetPasswordModel model)
        {
            if(ModelState.IsValid)
            {
                var user = await userManager.FindByEmailAsync(model.Email);
                if(user!=null)
                {

                    var result = await userManager.ResetPasswordAsync(user, model.Token, model.Password);
                    if(!result.Succeeded)
                    {
                      foreach(var error in result.Errors)
                        {
                            ModelState.AddModelError("", error.Description);
                        }
                        return View();
                    }

                    if(await userManager.IsLockedOutAsync(user))
                    {
                        await userManager.SetLockoutEndDateAsync(user, DateTime.UtcNow);
                       
                    }

                    return View("Success");
                }
                ModelState.AddModelError("", "Invalid REquest");
            }

            return View();
        }

    }

}


