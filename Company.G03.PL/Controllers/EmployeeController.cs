using AutoMapper;
using Company.G03.BLL.Interfaces;
using Company.G03.DAL.Models;
using Company.G03.PL.Helpers;
using Company.G03.PL.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.ObjectModel;
using System.Reflection.Metadata;

namespace Company.G03.PL.Controllers
{
    public class EmployeeController : Controller
    {
        //private readonly IEmployeeRepository _employeeRepository;
        //private readonly IDepartmentRepository _departmentRepository;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public EmployeeController(/*IEmployeeRepository employeeRepository, IDepartmentRepository departmentRepository,*/ 
            IMapper mapper,
            IUnitOfWork unitOfWork) 
        {
            //_employeeRepository = employeeRepository;
            //_departmentRepository = departmentRepository;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index(string searchInput)
        {
            #region View Dictionary
            //string Message = "Hello World";
            //ViewData["Message"] = Message + " From View Data";
            //ViewBag.Message01 = Message + " From View Bag";
            //TempData["Message02"] = Message + " From Temp Data"; 
            #endregion

            var Employee = Enumerable.Empty<Employee>();
            if(string.IsNullOrEmpty(searchInput))
            {
                Employee = _unitOfWork.EmployeeRepository.GetAll();
            }
            else
            {
                Employee = _unitOfWork.EmployeeRepository.GetByName(searchInput);
            }
            var emp = _mapper.Map<IEnumerable<EmployeeViewModel>>(Employee);
            return View(emp);
        }

        [HttpGet]
        public IActionResult Create()
        {
            var Departments = _unitOfWork.DepartmentRepository.GetAll();
            ViewData["Department"] = Departments;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(EmployeeViewModel model)
        {
            if (ModelState.IsValid)
            {
                model.ImageName = DocumentSetting.UploadFile(model.Image, "images");

                #region Manual Mapping
                // Manual Mapping

                //Employee emp = new Employee()
                //{
                //    Id = model.Id,
                //    Name = model.Name,
                //    Age = model.Age,
                //    Address = model.Address,
                //    Salary = model.Salary,
                //    Email = model.Email,
                //    PhoneNumber = model.PhoneNumber,
                //    IsActive = model.IsActive,
                //    HiringDate = model.HiringDate,
                //    WorkFor = model.WorkFor,
                //    WorkForId = model.WorkForId
                //}; 
                #endregion

                // Auto Mapping
                var emp = _mapper.Map<Employee>(model);

                var Count = _unitOfWork.EmployeeRepository.Add(emp);
                if (Count > 0)
                {
                    TempData["Message"] = "Employee Is Created Successfully";
                }
                else
                {
                    TempData["Message"] = "Employee Is Not Created Successfully";
                }
                return RedirectToAction(nameof(Index));
            }

            return View(model);
        }

        //[HttpGet]
        public IActionResult Details(int? id, string ViewName= "Details")
        {
            if (id is null) return BadRequest();

            var Employee = _unitOfWork.EmployeeRepository.Get(id.Value);

            if (Employee is null) return NotFound();

            var emp = _mapper.Map<EmployeeViewModel>(Employee);

            return View(ViewName, emp);
        }

        [HttpGet]
        public IActionResult Update(int? id)
        {
            var Departments = _unitOfWork.DepartmentRepository.GetAll();
            ViewData["Department"] = Departments;
            return Details(id, "Update");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Update([FromRoute] int? id, EmployeeViewModel model)
        {
            try
            {
                #region Manual Mapping
                //Employee emp = new Employee()
                //{
                //    Id = model.Id,
                //    Name = model.Name,
                //    Age = model.Age,
                //    Address = model.Address,
                //    Salary = model.Salary,
                //    Email = model.Email,
                //    PhoneNumber = model.PhoneNumber,
                //    IsActive = model.IsActive,
                //    HiringDate = model.HiringDate,
                //    WorkFor = model.WorkFor,
                //    WorkForId = model.WorkForId
                //}; 
                #endregion

                // Auto Mapping
                var emp = _mapper.Map<Employee>(model);

                if (id != emp.Id) return BadRequest();
                if (ModelState.IsValid)
                {
                    var Count = _unitOfWork.EmployeeRepository.Update(emp);
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
        public IActionResult Delete(int? id)
        {
            return Details(id, "Delete");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete([FromRoute] int? id, EmployeeViewModel model)
        {
            try
            {
                #region Manual Mapping
                //Employee emp = new Employee()
                //{
                //    Id = model.Id,
                //    Name = model.Name,
                //    Age = model.Age,
                //    Address = model.Address,
                //    Salary = model.Salary,
                //    Email = model.Email,
                //    PhoneNumber = model.PhoneNumber,
                //    IsActive = model.IsActive,
                //    HiringDate = model.HiringDate,
                //    WorkFor = model.WorkFor,
                //    WorkForId = model.WorkForId
                //}; 
                #endregion

                // Auto Mapping
                var emp = _mapper.Map<Employee>(model);

                if (id != emp.Id) return BadRequest();
                if (ModelState.IsValid)
                {
                    var Count = _unitOfWork.EmployeeRepository.Delete(emp);
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
