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
        public IActionResult Index()
        {
            var Department = _unitOfWork.DepartmentRepository.GetAll();
            return View(Department);
        }

        [HttpGet]
        public IActionResult create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult create(Department model)
        {
            if (ModelState.IsValid)
            {
                var Count = _unitOfWork.DepartmentRepository.Add(model);
                if (Count > 0)
                {
                    return RedirectToAction(nameof(Index));
                }
            }
           
            return View(model);
        }

        [HttpGet]
        public IActionResult details(int? id, string ViewName = "details")
        {
            if (id is null) return BadRequest();

           var Department = _unitOfWork.DepartmentRepository.Get(id.Value);
            
            if(Department is null) return NotFound();

            return View(ViewName,Department);
        }

        [HttpGet]
        public IActionResult update(int? id)
        {
            //if(id is null) return BadRequest();

            //var Department = _departmentRepository.Get(id.Value);

            //if(Department is null) return NotFound();

            //return View(Department);

            return details(id,"update");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult update([FromRoute]int? id, Department model)
        {
            try
            {
                if (id != model.Id) return BadRequest();
                if (ModelState.IsValid)
                {
                    var Count = _unitOfWork.DepartmentRepository.Update(model);
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
        public IActionResult delete(int? id)
        {
            //if (id is null) return BadRequest();

            //var Department = _departmentRepository.Get(id.Value);

            //if (Department is null) return NotFound();

            //return View(Department);
            return details(id, "delete");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult delete([FromRoute]int? id,Department model)
        {
            try
            {
                if (id != model.Id) return BadRequest();
                if (ModelState.IsValid)
                {
                    var Count = _unitOfWork.DepartmentRepository.Delete(model);
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
