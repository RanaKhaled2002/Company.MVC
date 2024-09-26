using Company.G03.DAL.Models;
using Company.G03.PL.Helpers;
using Company.G03.PL.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Company.G03.PL.Controllers
{
    [Authorize(Roles = "Admin")]
    public class UserController : Controller
	{
		public UserManager<AppUser> _UserManager { get; }

		public UserController(UserManager<AppUser> userManager) 
		{
			_UserManager = userManager;
		}


		#region Index
		public async Task<IActionResult> Index(string searchInput)
		{
			var user = Enumerable.Empty<UserViewModel>();

			if (string.IsNullOrEmpty(searchInput))
			{
				user = await _UserManager.Users.Select(U => new UserViewModel
				{
					Id = U.Id,
					FirstName = U.Fname,
					LastName = U.Lname,
					Email = U.Email,
					Roles = _UserManager.GetRolesAsync(U).Result
				}).ToListAsync();
			}
			else
			{
				user = await _UserManager.Users.Where(U => U.Email.ToLower()
								  .Contains(searchInput.ToLower()))
								  .Select(U => new UserViewModel
								  {
									  Id = U.Id,
									  FirstName = U.Fname,
									  LastName = U.Lname,
									  Email = U.Email,
									  Roles = _UserManager.GetRolesAsync(U).Result
								  }).ToListAsync();
			}
			return View(user);
		}
        #endregion

        #region Details
        public async Task<IActionResult> Details(string? id, string ViewName = "Details")
        {
            if (id is null) return BadRequest();

			var userFromDB = await _UserManager.FindByIdAsync(id);

            if (userFromDB is null) return NotFound();

			var user = new UserViewModel()
			{
				Id = userFromDB.Id,
				FirstName = userFromDB.Fname,
				LastName = userFromDB.Lname,
				Email = userFromDB.Email,
				Roles = _UserManager.GetRolesAsync(userFromDB).Result
			};

            return View(ViewName, user);
        }
		#endregion

		#region Update
		[HttpGet]
        public async Task<IActionResult> Update(string? id)
        {
            return await Details(id, "Update");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update([FromRoute] string? id, UserViewModel model)
        {
            try
			{ 
                if (id != model.Id) return BadRequest();

                if (ModelState.IsValid)
                {
					var userFromDB = await _UserManager.FindByIdAsync(id);

					if (userFromDB is null) return NotFound();

					userFromDB.Fname = model.FirstName;
					userFromDB.Lname = model.LastName;
					userFromDB.Email = model.Email;

					var result = await _UserManager.UpdateAsync(userFromDB);
                    
					if(result.Succeeded)
					{
                        return RedirectToAction(nameof(Index));

                    }
                }
            }
            catch (Exception Ex)
            {
                ModelState.AddModelError(string.Empty, Ex.Message);
            }

            return View(model);
        }
        #endregion

        #region Delete
        [HttpGet]
        public async Task<IActionResult> Delete(string? id)
        {
            return await Details(id, "Delete");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete([FromRoute] string? id, UserViewModel model)
        {
            try
            {
                if (id != model.Id) return BadRequest();

                if (ModelState.IsValid)
                {
                    var userFromDB = await _UserManager.FindByIdAsync(id);

                    if (userFromDB is null) return NotFound();

                    var result = await _UserManager.DeleteAsync(userFromDB);

                    if (result.Succeeded)
                    {
                        return RedirectToAction(nameof(Index));

                    }
                }
            }
            catch (Exception Ex)
            {
                ModelState.AddModelError(string.Empty, Ex.Message);
            }

            return View(model);
        }
        #endregion
    }
}
