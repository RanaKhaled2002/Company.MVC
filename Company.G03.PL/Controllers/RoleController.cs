using Company.G03.DAL.Models;
using Company.G03.PL.Helpers;
using Company.G03.PL.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Company.G03.PL.Controllers
{
    [Authorize]
    public class RoleController : Controller
    {
        public RoleManager<IdentityRole> _RoleManager { get; }

        public RoleController(RoleManager<IdentityRole> roleManager)
        {
            _RoleManager = roleManager;
        }

        #region Index
        public async Task<IActionResult> Index(string searchInput)
        {
            var role = Enumerable.Empty<RoleViewModel>();

            if(string.IsNullOrEmpty(searchInput))
            {
               role = await _RoleManager.Roles.Select(R => new RoleViewModel
                {
                    Id = R.Id,
                    RoleName = R.Name
                }).ToListAsync();
            }
            else
            {
                role = await _RoleManager.Roles.Where(R => R.Name.ToLower()
                                  .Contains(searchInput.ToLower()))
                                  .Select(R => new RoleViewModel
                                  {
                                      Id = R.Id,
                                      RoleName = R.Name
                                  }).ToListAsync();
            }

            return View(role);
        }
        #endregion

        #region Create
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(RoleViewModel model)
        {
            if (ModelState.IsValid)
            {
                var role = new IdentityRole()
                {
                    Name = model.RoleName
                };
               var result = await _RoleManager.CreateAsync(role);
                if(result.Succeeded)
                {
                    return RedirectToAction(nameof(Index));
                }
            }

            return View(model);
        }
        #endregion

        #region Details
        public async Task<IActionResult> Details(string? id, string ViewName = "Details")
        {
            if (id is null) return BadRequest();

            var roleFromDB = await _RoleManager.FindByIdAsync(id);

            if (roleFromDB is null) return NotFound();

            var role = new RoleViewModel()
            {
                Id = roleFromDB.Id,
                RoleName = roleFromDB.Name
            };

            return View(ViewName, role);
        }
        #endregion

        #region Update
        [HttpGet]
        public async Task<IActionResult> Update(string? id)
        {
            if (id is null) return BadRequest();

            return await Details(id,"Update");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update([FromRoute]string? id, RoleViewModel model)
        {
            try
            {
                if(id is null) return BadRequest();

                if (ModelState.IsValid)
                {
                    var roleFromDB = await _RoleManager.FindByIdAsync(id);

                    if(roleFromDB is null) return NotFound();

                    roleFromDB.Name = model.RoleName;

                    var result = await _RoleManager.UpdateAsync(roleFromDB);

                    if (result.Succeeded) { return RedirectToAction(nameof(Index)); }
                }
            }
            catch (Exception ex) 
            {
                ModelState.AddModelError(string.Empty, ex.Message);
            }
            return View(model);
        }
        #endregion

        #region Delete
        [HttpGet]
        public async Task<IActionResult> Delete(string? id)
        {
            if (id is null) return BadRequest();

            return await Details(id, "Delete");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete([FromRoute] string? id, RoleViewModel model)
        {
            try
            {
                if (id is null) return BadRequest();

                if (ModelState.IsValid)
                {
                    var roleFromDB = await _RoleManager.FindByIdAsync(id);

                    if (roleFromDB is null) return NotFound();

                    var result = await _RoleManager.DeleteAsync(roleFromDB);

                    if (result.Succeeded) { return RedirectToAction(nameof(Index)); }
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
            }
            return View(model);
        }
        #endregion
    }
}
