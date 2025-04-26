using EcommerceProject.DAL.IdentityApplication;
using EcommerceProject.DAL.Interfaces;
using EcommerceProject.Presentation.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace EcommerceProject.Presentation.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly SignInManager<ApplicationUser> signInManager;

        public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterUserViewModel model)
        {
            if (ModelState.IsValid)
            {
                // Maping
                ApplicationUser applicationUser = new ApplicationUser();
                applicationUser.UserName = model.UserName;
                applicationUser.Email = model.Email;
                //applicationUser.PasswordHash = model.Password;
                applicationUser.Address = model.Address;
                applicationUser.PhoneNumber = model.PhoneNumber;
                applicationUser.Gender = model.Gender == Gender.Male ? false : true; // male = 0 , female = 1
                // save Db
                IdentityResult identityResult = await userManager.CreateAsync(applicationUser, model.Password); // if any errors happend returns list of errors
                if (identityResult.Succeeded) // means there is no errors
                {
                    //Cookie
                    await signInManager.SignInAsync(applicationUser, false); // false means it's not a presistent cookie it's for the seesion why=> to make the user make login
                    return RedirectToAction("Index", "Home");
                }

                // send errors to end-user to resolve them
                foreach (var item in identityResult.Errors)
                {
                    ModelState.AddModelError("", item.Description);
                }
            }
            return View(model);
        }
    }
}
