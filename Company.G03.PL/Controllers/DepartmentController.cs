using Company.G03.BLL.Interfaces;
using Company.G03.DAL.Models;
using Microsoft.AspNetCore.Mvc;

namespace Company.G03.PL.Controllers
{
    public class DepartmentController : Controller
    {
        //private readonly IDepartmentRepository _departmentRepository;
        private readonly IUnitOfWork _unitOfWork;

        public DepartmentController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var Department = await _unitOfWork.DepartmentRepository.GetAllAsync();
            return View(Department);
        }

        [HttpGet]
        public IActionResult create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> create(Department model)
        {
            if (ModelState.IsValid)
            {
                var Count = await _unitOfWork.DepartmentRepository.AddAsync(model);
                if (Count > 0)
                {
                    return RedirectToAction(nameof(Index));
                }
            }
           
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> details(int? id, string ViewName = "details")
        {
            if (id is null) return BadRequest();

           var Department = await _unitOfWork.DepartmentRepository.GetAsync(id.Value);
            
            if(Department is null) return NotFound();

            return View(ViewName,Department);
        }

        [HttpGet]
        public async Task<IActionResult> update(int? id)
        {
            //if(id is null) return BadRequest();

            //var Department = _departmentRepository.Get(id.Value);

            //if(Department is null) return NotFound();

            //return View(Department);

            return await details(id,"update");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> update([FromRoute]int? id, Department model)
        {
            try
            {
                if (id != model.Id) return BadRequest();
                if (ModelState.IsValid)
                {
                    var Count = await _unitOfWork.DepartmentRepository.UpdateAsync(model);
                    if (Count > 0)
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

        [HttpGet]
        public async Task<IActionResult> delete(int? id)
        {
            //if (id is null) return BadRequest();

            //var Department = _departmentRepository.Get(id.Value);

            //if (Department is null) return NotFound();

            //return View(Department);
            return await details(id, "delete");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> delete([FromRoute]int? id,Department model)
        {
            try
            {
                if (id != model.Id) return BadRequest();
                if (ModelState.IsValid)
                {
                    var Count = await _unitOfWork.DepartmentRepository.DeleteAsync(model);
                    if (Count > 0)
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
    }
}
