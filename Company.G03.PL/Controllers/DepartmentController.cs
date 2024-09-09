using Company.G03.BLL.Interfaces;
using Company.G03.DAL.Models;
using Microsoft.AspNetCore.Mvc;

namespace Company.G03.PL.Controllers
{
    public class DepartmentController : Controller
    {
        private readonly IDepartmentRepository _departmentRepository;

        public DepartmentController(IDepartmentRepository departmentRepository)
        {
            _departmentRepository = departmentRepository;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var Department = _departmentRepository.GetAll();
            return View(Department);
        }

        [HttpGet]
        public IActionResult create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult create(Department model)
        {
            if (ModelState.IsValid)
            {
                var Count = _departmentRepository.Add(model);
                if (Count > 0)
                {
                    return RedirectToAction(nameof(Index));
                }
            }
           
            return View(model);
        }

        [HttpGet]
        public IActionResult details(int? id)
        {
            if (id is null) return BadRequest();

           var Department =  _departmentRepository.Get(id.Value);
            
            if(Department is null) return NotFound();

            return View(Department);
        }

        [HttpGet]
        public IActionResult update(int? id)
        {
            if(id is null) return BadRequest();

            var Department = _departmentRepository.Get(id.Value);

            if(Department is null) return NotFound();

            return View(Department);
        }

        [HttpPost]
        public IActionResult update(Department model)
        {
            if (ModelState.IsValid)
            {
                var Count = _departmentRepository.Update(model);
                if (Count > 0)
                {
                    return RedirectToAction(nameof(Index));
                }
            }

            return View(model);
        }
    }
}
