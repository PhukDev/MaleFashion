using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MaleFashion.Models;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
namespace MaleFashion.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly ILogger<AccountController> _logger;
        public AccountController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, ILogger<AccountController> logger)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
        }

        [HttpGet]
        public IActionResult Login(string returnUrl = null)
        {
            return View(new LoginViewModel { ReturnUrl = returnUrl });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            ModelState.Remove("ReturnUrl");
            if (ModelState.IsValid)
            {
                try
                {
                    var user = await _userManager.FindByEmailAsync(model.Email);
                    if (user == null)
                    {
                        ModelState.AddModelError(string.Empty, "Email không tồn tại.");
                        _logger.LogWarning("Login attempt with non-existent email: {Email}", model.Email);
                        return View(model);
                    }

                    var result = await _signInManager.PasswordSignInAsync(user.UserName, model.Password, model.RememberMe, lockoutOnFailure: true);
                    if (result.Succeeded)
                    {
                        _logger.LogInformation("User {Email} logged in successfully", model.Email);
                        return RedirectToLocal(model.ReturnUrl);
                    }
                    else if (result.IsLockedOut)
                    {
                        ModelState.AddModelError(string.Empty, "Tài khoản đã bị khóa do nhập sai mật khẩu quá nhiều lần. Vui lòng thử lại sau 5 phút.");
                        _logger.LogWarning("User {Email} is locked out", model.Email);
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, "Mật khẩu không đúng.");
                        _logger.LogWarning("Failed login attempt for {Email}", model.Email);
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error during login for {Email}", model.Email);
                    ModelState.AddModelError(string.Empty, "Đã xảy ra lỗi hệ thống. Vui lòng thử lại sau.");
                }
            }
            return View(model);
        }
        [HttpGet]
        public IActionResult Register(string returnUrl = null)
        {
            return View(new RegisterViewModel { ReturnUrl = returnUrl });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            ModelState.Remove("ReturnUrl");
            if (ModelState.IsValid)
            {
                try
                {
                    var user = new IdentityUser { UserName = model.UserName, Email = model.Email };
                    var result = await _userManager.CreateAsync(user, model.Password);
                    if (result.Succeeded)
                    {
                        _logger.LogInformation("User {Email} registered successfully", model.Email);
                        await _signInManager.SignInAsync(user, isPersistent: false);
                        return RedirectToLocal(model.ReturnUrl);
                    }
                    else
                    {
                        foreach (var error in result.Errors)
                        {
                            ModelState.AddModelError(string.Empty, error.Code switch
                            {
                                "DuplicateUserName" => "Tên người dùng đã được sử dụng. Vui lòng chọn tên khác.",
                                "DuplicateEmail" => "Email đã được sử dụng. Vui lòng sử dụng email khác.",
                                "InvalidEmail" => "Email không hợp lệ.",
                                "PasswordRequiresNonAlphanumeric" => "Mật khẩu phải chứa ít nhất một ký tự không phải chữ số.",
                                "PasswordRequiresDigit" => "Mật khẩu phải chứa ít nhất một số.",
                                "PasswordRequiresUpper" => "Mật khẩu phải chứa ít nhất một chữ cái in hoa.",
                                "PasswordTooShort" => "Mật khẩu phải có ít nhất 6 ký tự.",
                                _ => error.Description
                            });
                        }
                        _logger.LogWarning("Failed registration attempt for {Email}", model.Email);
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error during registration for {Email}", model.Email);
                    ModelState.AddModelError(string.Empty, "Đã xảy ra lỗi hệ thống. Vui lòng thử lại sau.");
                }
            }
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            _logger.LogInformation("User logged out");
            return RedirectToAction("Index", "Home");
        }

        private IActionResult RedirectToLocal(string returnUrl)
        {
            return !string.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl)
                ? Redirect(returnUrl)
                : RedirectToAction("Index", "Home");
        }
    }
}

