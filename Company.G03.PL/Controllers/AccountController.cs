using Company.G03.DAL.Models;
using Company.G03.PL.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Company.G03.PL.Controllers
{
	public class AccountController : Controller
	{
		private readonly UserManager<AppUser> _UserManager;

		public AccountController(UserManager<AppUser> userManager)
		{
			_UserManager = userManager;
		}

		#region SignUp
		[HttpGet]
		public IActionResult SignUp()
		{
			return View();
		}

		[HttpPost]
		public async Task<IActionResult> SignUp(SignUpViewModel model)
		{
			if (ModelState.IsValid)
			{
				var User = await _UserManager.FindByNameAsync(model.UserName);

				if (User is null)
				{
					User = await _UserManager.FindByEmailAsync(model.Email);

					if (User is null)
					{
						// Mapping SignUpViewModel To AppUser

						User = new AppUser()
						{
							UserName = model.UserName,
							Email = model.Email,
							Fname = model.FirstName,
							Lname = model.LastName,
							IsAgree = model.IsAgree,
						};
						var Result = await _UserManager.CreateAsync(User, model.Password);

						if (Result.Succeeded)
						{
							return RedirectToAction("SignIn");
						}

						foreach (var error in Result.Errors)
						{
							ModelState.AddModelError(string.Empty, error.Description);
						}
					}

					ModelState.AddModelError(string.Empty, "Email Is Already Exist !");
					return View(model);

				}

				ModelState.AddModelError(string.Empty, "User Name Is Already Exist !");
			}
			return View(model);
		}

		#endregion

		#region SignIn
		[HttpGet]
		public IActionResult SignIn()
		{
			return View();
		}

		[HttpPost]
		public async Task<IActionResult> SignIn(SignInViewModel model)
		{
            if (ModelState.IsValid)
            {
				try
				{
					var user = await _UserManager.FindByEmailAsync(model.Email);
					if (user is not null)
					{
						var flag = await _UserManager.CheckPasswordAsync(user, model.Password);

						if (flag)
						{
							return RedirectToAction("Index", "Home");
						}
					}
					ModelState.AddModelError(string.Empty, "Login Is Invalid !!");

				}
				catch (Exception ex)
				{
					ModelState.AddModelError(string.Empty, ex.Message);
				}

            }
            return View();
		}
		#endregion


	}
}
