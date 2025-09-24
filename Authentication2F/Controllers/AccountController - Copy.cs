//using Authentication2F.Models;
//using Microsoft.AspNetCore.Identity;
//using Microsoft.AspNetCore.Identity.UI.V4.Pages.Account.Internal;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.AspNetCore.Identity;
//using Microsoft.AspNetCore.Mvc;
//using Authentication2F.Models; // ApplicationUser
//using System.Threading.Tasks;
//using Authentication2F.ViewModels;

//namespace Authentication2F.Controllers
//{
//    public class AccountController : Controller
//    {
//        private readonly UserManager<ApplicationUser> _userManager;
//        private readonly SignInManager<ApplicationUser> _signInManager;
//        private readonly Microsoft.AspNetCore.Identity.UI.Services.IEmailSender _emailSender;

//        public AccountController(UserManager<ApplicationUser> userManager,
//                                 SignInManager<ApplicationUser> signInManager,
//                                 Microsoft.AspNetCore.Identity.UI.Services.IEmailSender emailSender)
//        {
//            _userManager = userManager;
//            _signInManager = signInManager;
//            _emailSender = emailSender;
//        }

//        // 🔹 Register
//        [HttpGet]
//        public IActionResult Register() => View();

//        [HttpPost]
//        public async Task<IActionResult> Register(RegisterViewModel model)
//        {
//            if (ModelState.IsValid)
//            {
//                var user = new ApplicationUser { UserName = model.Email, Email = model.Email, FullName = model.FullName };
//                var result = await _userManager.CreateAsync(user, model.Password);

//                if (result.Succeeded)
//                {
//                    var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
//                    var confirmationLink = Url.Action("ConfirmEmail", "Account",
//                        new { userId = user.Id, token = token }, Request.Scheme);

//                    await _emailSender.SendEmailAsync(user.Email, "Confirm your email",
//                        $"Please confirm your account by <a href='{confirmationLink}'>clicking here</a>.");

//                    TempData["Message"] = "Registration successful! Please check your email to confirm your account.";
//                    return RedirectToAction("Login");
//                }

//                foreach (var error in result.Errors)
//                    ModelState.AddModelError("", error.Description);
//            }
//            return View(model);
//        }

//        // 🔹 Confirm Email
//        [HttpGet]
//        public async Task<IActionResult> ConfirmEmail(string userId, string token)
//        {
//            if (userId == null || token == null) return RedirectToAction("Index", "Home");

//            var user = await _userManager.FindByIdAsync(userId);
//            if (user == null) return NotFound("User not found");

//            var result = await _userManager.ConfirmEmailAsync(user, token);
//            return View(result.Succeeded ? "ConfirmEmail" : "Error");
//        }

//        // 🔹 Login
//        [HttpGet]
//        public IActionResult Login() => View();

//        [HttpPost]
//        public async Task<IActionResult> Login(LoginViewModel model)
//        {
//            if (ModelState.IsValid)
//            {
//                var result = await _signInManager.PasswordSignInAsync(
//                    model.Email, model.Password, model.RememberMe, lockoutOnFailure: false);

//                if (result.Succeeded)
//                    return RedirectToAction("Index", "Home");

//                ModelState.AddModelError("", "Invalid login attempt.");
//            }
//            return View(model);
//        }

//        // 🔹 Logout
//        [HttpPost]
//        public async Task<IActionResult> Logout()
//        {
//            await _signInManager.SignOutAsync();
//            return RedirectToAction("Index", "Home");
//        }

//        // 🔹 Forgot Password
//        [HttpGet]
//        public IActionResult ForgotPassword() => View();

//        [HttpPost]
//        public async Task<IActionResult> ForgotPassword(ForgotPasswordViewModel model)
//        {
//            if (ModelState.IsValid)
//            {
//                var user = await _userManager.FindByEmailAsync(model.Email);
//                if (user == null || !(await _userManager.IsEmailConfirmedAsync(user)))
//                {
//                    return RedirectToAction("ForgotPasswordConfirmation");
//                }

//                var token = await _userManager.GeneratePasswordResetTokenAsync(user);
//                var resetLink = Url.Action("ResetPassword", "Account",
//                    new { token, email = user.Email }, Request.Scheme);

//                await _emailSender.SendEmailAsync(user.Email, "Reset Password",
//                    $"Reset your password by <a href='{resetLink}'>clicking here</a>.");

//                return RedirectToAction("ForgotPasswordConfirmation");
//            }
//            return View(model);
//        }

//        [HttpGet]
//        public IActionResult ForgotPasswordConfirmation() => View();

//        // 🔹 Reset Password
//        [HttpGet]
//        public IActionResult ResetPassword(string token, string email) =>
//            View(new ResetPasswordViewModel { Token = token, Email = email });

//        [HttpPost]
//        public async Task<IActionResult> ResetPassword(ResetPasswordViewModel model)
//        {
//            if (!ModelState.IsValid) return View(model);

//            var user = await _userManager.FindByEmailAsync(model.Email);
//            if (user == null) return RedirectToAction("ResetPasswordConfirmation");

//            var result = await _userManager.ResetPasswordAsync(user, model.Token, model.Password);
//            if (result.Succeeded) return RedirectToAction("ResetPasswordConfirmation");

//            foreach (var error in result.Errors)
//                ModelState.AddModelError("", error.Description);

//            return View();
//        }

//        [HttpGet]
//        public IActionResult ResetPasswordConfirmation() => View();
//    }
//}
