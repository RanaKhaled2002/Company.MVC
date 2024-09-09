using Company.G03.BLL.Interfaces;
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

        public IActionResult Index()
        {
            var Department = _departmentRepository.GetAll();
            return View(Department);
        }
    }
}
